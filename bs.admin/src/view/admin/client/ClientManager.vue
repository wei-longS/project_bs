<script lang="ts" setup>
import { reactive, ref, onMounted, toRaw } from 'vue'
import type { ElTable } from 'element-plus'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage, ElMessageBox } from 'element-plus'
import type { Action } from 'element-plus'
import { Search, RefreshRight } from '@element-plus/icons-vue'
import addVue from './components/add.vue'
import { ClientModel } from './class/ClientModel'
import { getClientDataNew, delClient, batchDelClient, addEmaillog } from '../../../http/index'
import { getEmaillogDataNew, delEmaillog, batchDelEmaillog } from '../../../http/index'





const ruleFormRef = ref<FormInstance>()
const multipleTableRef = ref<InstanceType<typeof ElTable>>()
const form = reactive({

    clientname: "",
    clienttel: "",
    clientaddress: "",
    clientemail: "",
    description: "",
    pageIndex: 1,
    pageSize: 10,
    Total: 0
})

const rules = reactive<FormRules>({
    clientname: [{ required: false, message: '请输入姓名', trigger: 'blur' }]
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
    form.clientname = ""
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
const handleEdit = (index: number, row: ClientModel) => {
    info.value = JSON.stringify(row)
    addVisible.value = true
}

//单个删除
const handleDelete = async (index: number, row: ClientModel) => {
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
            const res = delClient(row.id) as any as boolean
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
                const res = batchDelClient(ids) as any as boolean
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
const tableData = ref<Array<ClientModel>>()
onMounted(async () => {
    LoadTableData()

})
const LoadTableData = async (name: string = "") => {
    let parms = {
        Clientname: form.clientname,
        Clienttel: form.clienttel,
        Clientaddress: form.clientaddress,
        Clientemail: form.clientemail,
        Description: form.description,
        PageIndex: form.pageIndex,
        PageSize: form.pageSize

    }
    let res = await getClientDataNew(parms) as any
    // console.log("后端数据信息")
    //console.log(res)
    form.Total = res.total

    tableData.value = res.data as ClientModel[]
    loading.value = false
}

//分页
const handleCurrentChange = (val: number) => {
    form.pageIndex = val
    LoadTableData()

}

const loading = ref(true)

const loading1 = ref(false)
// 发送邮件
const addVisible1 = ref(false)

const email = () => {
    addVisible1.value = true
}

const sender = async () => {
    loading1.value = true
    var parms = {

        Emaillogaddr: form1.emaillogaddr,
        Emaillogname: form1.emaillogname,
        Emaillogtitle: form1.emaillogtitle,
        Emaillogcontent: form1.emaillogcontent
    }
    console.log(parms)

    // 执行添加逻辑 
    const res = await addEmaillog(parms) as any as boolean
    if (res) {
        ElMessage({
            message: '发送成功！',
            type: 'success',
        })
        form1.emaillogaddr = '',
        form1.emaillogname = '',
        form1.emaillogtitle = '',
        form1.emaillogcontent = '',
        loading1.value = false
        addVisible1.value = false

    }

}

const close = () => {
    addVisible1.value = false
}

const form1 = reactive({
    emaillogaddr: "",
    emaillogname: "",
    emaillogtitle: "",
    emaillogcontent: "",
    pageIndex: 1,
    pageSize: 10,
    Total: 0
})

</script>

<!-- 发送邮件 -->
<template>
    <el-dialog v-model="addVisible1" title="邮件编辑" width="30%">
        <el-form v-loading="loading1" :model="form" label-width="120px" class="form" :rules="rules" ref="ruleFormRef">
            <!-- <el-form-item label="客户名称" prop="clientname">
                                        <el-input v-model="form.clientname" />
                                    </el-form-item> -->
            <el-form-item label="邮箱地址" prop="emaillogaddr">
                <el-input v-model="form1.emaillogaddr" />
            </el-form-item>
            <el-form-item label="邮件发送人" prop="emaillogname">
                <el-input v-model="form1.emaillogname" />
            </el-form-item>
            <el-form-item label="邮件标题" prop="emaillogtitle">
                <el-input v-model="form1.emaillogtitle" />
            </el-form-item>
            <el-form-item label="邮件内容" prop="emaillogcontent">
                <el-input v-model="form1.emaillogcontent" />
            </el-form-item>
            <!-- 按钮操作组 -->
            <el-form-item class="btn">
                <el-button type="primary" @click="sender">确定</el-button>
                <el-button @click="close">取消</el-button>
            </el-form-item>
        </el-form>
    </el-dialog>






    <!-- 本身 -->
    <el-card class="box-card">
        <template #header>
            <div class="card-header">
                <el-form :inline="true" :model="form" class="demo-form-inline" :rules="rules" ref="ruleFormRef">
                    <el-form-item label="客户名" prop="name">
                        <el-input v-model="form.clientname" placeholder="请输入客户名" />
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
                    <el-button type="success" @click="email">给客户发送邮件</el-button>
                </p>
            </div>
        </template>
        <el-table v-loading="loading" :data="tableData" style="width: 100%" ref="multipleTableRef" row-key="id">
            <el-table-column type="selection" width="55" />
            <el-table-column prop="id" label="序号" width="60" />
            <el-table-column label="客户名称" width="300">
                <template #default="scope">
                    <div>{{ scope.row.clientname }}</div>
                </template>
            </el-table-column>
            <el-table-column label="客户电话">
                <template #default="scope">
                    <div>{{ scope.row.clienttel }}</div>
                </template>
            </el-table-column>
            <el-table-column label="客户地址">
                <template #default="scope">
                    <div>{{ scope.row.clientaddress }}</div>
                </template>
            </el-table-column>
            <el-table-column label="客户邮箱">
                <template #default="scope">
                    <div>{{ scope.row.clientemail }}</div>
                </template>
            </el-table-column>
            <el-table-column label="描述">
                <template #default="scope">
                    <div>{{ scope.row.description }}</div>
                </template>
            </el-table-column>
            <el-table-column label="操作" align="right" width="200">
                <template #default="scope">
                    <el-button size="small" type="primary" @click="handleEdit(scope.$index, scope.row)">编辑</el-button>
                    <!-- <el-button size="small" type="danger" @click="handleDelete(scope.$index, scope.row)">删除</el-button> -->
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