<script lang="ts" setup>
import { reactive, ref, onMounted, toRaw } from 'vue'
import type { ElTable } from 'element-plus'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Search, RefreshRight } from '@element-plus/icons-vue'
import addVue from './components/add.vue'
import { EmployeeModel } from './class/EmployeeModel'
import { getEmployeeDataNew, delEmployee, batchDelEmployee } from '../../../http/index'
import { fromPairs } from 'lodash'


const loading = ref(true)
const ruleFormRef = ref<FormInstance>()
const multipleTableRef = ref<InstanceType<typeof ElTable>>()
const form = reactive({
    nameid: "",
    name: "",
    sex: "",
    age: 1,
    department: "",
    position: "",
    tel: 0,
    order: 0,
    description: "",
    pageIndex: 1,
    pageSize: 10,
    Total: 0
})
const rules = reactive<FormRules>({
    name: [{ required: false, message: '请输入姓名', trigger: 'blur' }]
})

const onSubmit = async (ruleFormRef: FormInstance | undefined) => {
    //console.log("打印了"+ruleFormRef)
    if (!ruleFormRef) return;
    await ruleFormRef.validate((valid, fields) => {
        if (valid) {
            LoadTableData()
        } else {
            console.log('error submit!', fields)
        }
    })

}


const resetForm = (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return
    ruleFormRef.resetFields()
    LoadTableData()
}


const handleQuery = (index: number, row: EmployeeModel) => {
    console.log(index, row)
    queryDialog.value = true
}
const addVisible = ref(false)
const add = () => {
    addVisible.value = true
}
const CloseAdd = () => {
    addVisible.value = false
    info.value = undefined
    LoadTableData()
}
const info = ref()
const handleEdit = (index: number, row: EmployeeModel) => {
    info.value = JSON.stringify(row)
    addVisible.value = true
}

//单个删除
const handleDelete = async (index: number, row: EmployeeModel) => {

    ElMessageBox.confirm(
        '你确定要删除吗?',
        {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning',
        }
    )
        .then(() => {
            tableData.value = tableData.value?.filter(s => s.id != row.id)
            const res = delEmployee(row.id) as any as boolean
            if (res) {
                ElMessage({
                    message: '删除成功！',
                    type: 'success',
                })
            }
        })
        .catch(() => {
            ElMessage({
                type: 'info',
                message: '取消删除',
            })
        })
}


//批量删除
const Del = async () => {
    let arr: any[] = multipleTableRef.value?.getSelectionRows()
    let ids: string = arr.map(item => { return "'" + item.id + "'" }).join(',')
    if (arr.length == 0) {
        ElMessage({
            message: '请选择要删除的员工信息！',
            type: 'warning',
        })
    } else {
        ElMessageBox.confirm(
            '你确定要删除吗?',
            {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                type: 'warning',
            }
        )
            .then(() => {
                const res = batchDelEmployee(ids) as any as boolean
                if (res) {
                    ElMessage({
                        message: '删除成功！',
                        type: 'success',
                    })
                    LoadTableData()
                }
            })
            .catch(() => {
                ElMessage({
                    type: 'info',
                    message: '取消删除',
                })
                LoadTableData()
            })
    }
}

const queryDialog = ref(false)
const queryDialogClose = () => {
    queryDialog.value = false;
}
//表格
const tableData = ref<Array<EmployeeModel>>()
onMounted(async () => {
    LoadTableData()

})
const LoadTableData = async (name: string = "") => {
    let parms = {
        Nameid: form.nameid,
        Name: form.name,
        Sex: form.sex,
        Age: form.age,
        Department: form.department,
        Position: form.position,
        Tel: form.tel,
        Order: form.order,
        Description: form.description,
        PageIndex: form.pageIndex,
        PageSize: form.pageSize
    }
    let res = await getEmployeeDataNew(parms) as any
    form.Total = res.total
    console.log(res)
    tableData.value = res.data as EmployeeModel[]
    loading.value=false



}

//分页
const handleCurrentChange = (val: number) => {
    form.pageIndex = val
    LoadTableData()
}
</script>
<template>
    <el-card class="box-card">
        <template #header>
            <div class="card-header">
                <el-form :inline="true" :model="form" class="demo-form-inline" :rules="rules" ref="ruleFormRef">
                    <el-form-item label="姓名" prop="name">
                        <el-input v-model="form.name" placeholder="请输入姓名" />
                    </el-form-item>
                    <el-form-item>
                    </el-form-item>
                    <el-form-item>
                        <el-button type="primary" @click="onSubmit(ruleFormRef)">
                            <el-icon>
                                <search />
                            </el-icon>查询
                        </el-button>
                        <el-button @click="resetForm(ruleFormRef)">
                            <el-icon>
                                <refresh-right />
                            </el-icon>重置
                        </el-button>
                    </el-form-item>
                </el-form>



                <p>
                    <el-button type="primary" @click="add">新增</el-button>
                    <el-button type="danger" @click="Del">删除</el-button>
                </p>
            </div>
        </template>
        <el-table v-loading="loading"   :data="tableData" style="width: 100%" ref="multipleTableRef" row-key="id">
            <el-table-column type="selection" width="55" />
            <el-table-column prop="id" label="序号" width="60" />
            <el-table-column label="员工编号" width="200">
                <template #default="scope">
                    <div>{{ scope.row.nameid }}</div>
                </template>
            </el-table-column>

            <el-table-column label="姓名" width="100">
                <template #default="scope">
                    <div>{{ scope.row.name }}</div>
                </template>
            </el-table-column>

            <el-table-column label="性别">
                <template #default="scope">
                    <div>{{ scope.row.sex }}</div>
                </template>
            </el-table-column>
            <el-table-column label="年龄">
                <template #default="scope">
                    <div>{{ scope.row.age }}</div>
                </template>
            </el-table-column>
            <el-table-column label="部门">
                <template #default="scope">
                    <div>{{ scope.row.department }}</div>
                </template>
            </el-table-column>
            <el-table-column label="职务">
                <template #default="scope">
                    <div>{{ scope.row.position }}</div>
                </template>
            </el-table-column>
            <el-table-column label="联系电话" width="150">
                <template #default="scope">
                    <div>{{ scope.row.tel }}</div>
                </template>
            </el-table-column>
            <!-- <el-table-column label="排序">
                <template #default="scope">
                    <div>{{ scope.row.order }}</div>
                </template>
            </el-table-column> -->
            <el-table-column label="描述">
                <template #default="scope">
                    <div>{{ scope.row.description }}</div>
                </template>
            </el-table-column>
            <el-table-column label="创建时间" width="200">
                <template #default="scope">
                    <div>
                        <el-icon>
                            <timer />
                        </el-icon>
                        <span style="margin-left: 10px">{{ scope.row.createDate }}</span>
                    </div>
                </template>
            </el-table-column>
            <el-table-column label="操作" align="right" width="200">
                <template #default="scope">
                    <!-- <el-button size="small" type="success" @click="handleQuery(scope.$index, scope.row)">详情</el-button> -->
                    <el-button size="small" type="primary" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                    <el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-pagination background layout="prev, pager, next" :total="form.Total"
            @current-change="handleCurrentChange" />
    </el-card>
    <addVue :addVisible="addVisible" :info="info" @CloseAdd="CloseAdd"></addVue>
</template>
<style lang="scss" scoped>
.el-pagination {
    margin-top: 50px;
}

.cell {
    text-align: center !important;
}

.queryTable {
    width: 500px;
    border-collapse: collapse;

    tr {
        height: 50px;

        td {
            padding: 10px;
        }

        .left {
            width: 30%;
            background-color: #F5F7FA;
        }

        .right {
            width: 80%;
        }
    }
}
</style>