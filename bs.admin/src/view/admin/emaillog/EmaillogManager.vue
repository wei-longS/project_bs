<script lang="ts" setup>
import { reactive, ref, onMounted, toRaw } from 'vue'
import type { ElTable } from 'element-plus'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage, ElMessageBox } from 'element-plus'
import type { Action } from 'element-plus'
import { Search, RefreshRight } from '@element-plus/icons-vue'
import { EmaillogModel } from './class/EmaillogModel'
import { getEmaillogDataNew, delEmaillog, batchDelEmaillog } from '../../../http/index'


const ruleFormRef = ref<FormInstance>()
const multipleTableRef = ref<InstanceType<typeof ElTable>>()
const form = reactive({
    emaillogaddr: "",
    emaillogname: "",
    emaillogtitle: "",
    emaillogcontent: "",
    pageIndex: 1,
    pageSize: 10,
    Total: 0
})

const rules = reactive<FormRules>({
    emaillogname: [{ required: false, message: '请输入姓名', trigger: 'blur' }]
})

//查询
const onSubmit = async (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return;
    await ruleFormRef.validate((valid, fields) => {
        if (valid) {
            // tableData.value = tableDa    ta.value?.filter(s => s.name == form.name) 
            LoadTableData()
        } else {
            console.log('error submit!', fields)
        }
    })
}
//重置
const resetForm = (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return
    ruleFormRef.resetFields()
    form.emaillogname = ""
    LoadTableData()
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
const handleEdit = (index: number, row: EmaillogModel) => {
    info.value = JSON.stringify(row)
    addVisible.value = true
}

//单个删除
const handleDelete = async (index: number, row: EmaillogModel) => {
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
            const res = delEmaillog(row.id) as any as boolean
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
            message: '请选择要删除的客户信息！',
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
                const res = batchDelEmaillog(ids) as any as boolean
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
const tableData = ref<Array<EmaillogModel>>()
onMounted(async () => {
    LoadTableData()

})
const LoadTableData = async (name: string = "") => {
    let parms = {

        Emaillogaddr: form.emaillogaddr,
        Emaillogname: form.emaillogname,
        Emaillogtitle: form.emaillogtitle,
        Emaillogcontent: form.emaillogcontent,
        PageIndex: form.pageIndex,
        PageSize: form.pageSize

    }
    let res = await getEmaillogDataNew(parms) as any
    // console.log("后端数据信息")
    //console.log(res)
    form.Total = res.total

    tableData.value = res.data as EmaillogModel[]
    loading.value = false
}

//分页
const handleCurrentChange = (val: number) => {
    form.pageIndex = val
    LoadTableData()

}

const loading = ref(true)
</script>
<template>
    <el-card class="box-card">
        <template #header>
            <div class="card-header">
                <el-form :inline="true" :model="form" class="demo-form-inline" :rules="rules" ref="ruleFormRef">
                    <el-form-item label="标题" prop="name">
                        <el-input v-model="form.emaillogtitle" placeholder="请输入标题名" />
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
        <el-table v-loading="loading" :data="tableData" style="width: 100%" ref="multipleTableRef" row-key="id">
            <el-table-column type="selection" width="55" />
            <el-table-column prop="id" label="序号" width="60" />
            <el-table-column label="邮件地址" width="300">
                <template #default="scope">
                    <div>{{ scope.row.emaillogaddr }}</div>
                </template>
            </el-table-column>
            <el-table-column label="邮件发送人">
                <template #default="scope">
                    <div>{{ scope.row.emaillogname }}</div>
                </template>
            </el-table-column>
            <el-table-column label="邮件标题">
                <template #default="scope">
                    <div>{{ scope.row.emaillogtitle }}</div>
                </template>
            </el-table-column>
            <el-table-column label="邮件内容">
                <template #default="scope">
                    <div>{{ scope.row.emaillogcontent }}</div>
                </template>
            </el-table-column>
            <el-table-column label="操作" align="right" width="200">
                <template #default="scope">
                    <el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button>
                </template>
            </el-table-column>
        </el-table>
        <el-pagination background layout="prev, pager, next" :total="form.Total" @current-change="handleCurrentChange" />
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