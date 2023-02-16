<template>
    <el-dialog v-model="addVisible" :title="info == undefined ? '新增' : '修改'" width="30%" :before-close="handleClose">
        <el-form :model="form" label-width="120px" class="form" :rules="rules" ref="ruleFormRef">
            <el-form-item label="客户名称" prop="clientname">
                <el-input v-model="form.clientname" />
            </el-form-item>
            <el-form-item label="客户电话" prop="clienttel">
                <el-input v-model="form.clienttel" />
            </el-form-item>
            <el-form-item label="客户地址" prop="clientaddress">
                <el-input v-model="form.clientaddress" />
            </el-form-item>
            <el-form-item label="客户邮箱" prop="clientemail">
                <el-input v-model="form.clientemail" />
            </el-form-item>
            <el-form-item label="描述" prop="description">
                <el-input v-model="form.description" />
            </el-form-item>
            <!-- 按钮操作组 -->
            <el-form-item class="btn">
                <el-button type="primary" @click="submit(ruleFormRef)">确定</el-button>
                <el-button @click="close(ruleFormRef)">取消</el-button>
            </el-form-item>
        </el-form>
    </el-dialog>
</template>

<script lang="ts" setup>
import { ref, reactive, defineProps, computed, defineEmits, onMounted, watch, toRaw } from 'vue'
import { addClient, editClient, getClientDataNew, } from '../../../../http'
import { ClientModel } from '../class/ClientModel'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage } from 'element-plus'

const ruleFormRef = ref<FormInstance>()
const props = defineProps({
    addVisible: Boolean,
    info: ClientModel
})
const form = ref({
    id: props.info?.id,
    clientname: "",
    clienttel: "",
    clientaddress: "",
    clientemail: "",
    description: "",
})
const rules = reactive<FormRules>({
    clientname: [{ required: true, message: '请输入客户名称', trigger: 'blur' }]
})

//监听
watch(
    () => props.info,
    (newInfo, oldInfo) => {
        if (newInfo != undefined) {
            let currInfo: ClientModel = (JSON.parse(newInfo as any)) as ClientModel
            form.value = {
                id: currInfo.id,
                clientname: currInfo.clientname,
                clienttel: currInfo.clienttel,
                clientaddress: currInfo.clientaddress,
                clientemail: currInfo.clientemail,
                description: currInfo.description
            }
        } else {
            form.value = {
                id: 0,
                clientname: "",
                clienttel: "",
                clientaddress: "",
                clientemail: "",
                description: "",
            }
            //  console.log("置空"+form.value)
        }
    }
);
//defineEmits用于定义回调事件，里面是数组，可以定义多个事件
const emits = defineEmits(["CloseAdd"])
const handleClose = (done: () => void) => {
    emits("CloseAdd")
}
//读取下拉数据
const treedata = ref()
//初始加载
onMounted(() => {
    LoadClientData()

})
const LoadClientData = async () => {
    let parms = {
        Clientname: "",
        Clienttel: "",
        Clientaddress: "",
        Clientemail: "",
        Description: "",
        PageIndex: 1,
        PageSize: 10
    }
    treedata.value = (await getClientDataNew(parms)).data

}





const submit = async (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return
    var parms = {
        Id: form.value.id
        , Clientname: form.value.clientname
        , Clienttel: form.value.clienttel
        , Clientaddress: form.value.clientaddress
        , Clientemail: form.value.clientemail
        , Description: form.value.description
    }
    console.log(parms)
    if (props.info == undefined) {
        // 执行添加逻辑 
        const res = await addClient(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '添加成功！',
                type: 'success',
            })
            LoadClientData()

        }
    } else {
        // 执行修改逻辑
        const res = await editClient(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '编辑成功！',
                type: 'success',
            })
            //  LoadMenuData()
            LoadClientData()

        }
    }

    ruleFormRef.resetFields()
    emits("CloseAdd")
}
const close = (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return
    ruleFormRef.resetFields()
    emits("CloseAdd")
}


</script>
<style lang="scss" scoped>
.form {
    min-height: 500px;

    .btn {
        position: absolute;
        bottom: 10px;
    }
}
</style>