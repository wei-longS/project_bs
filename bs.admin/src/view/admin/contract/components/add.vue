<template>
    <el-dialog v-model="addVisible" :title="info == undefined ? '新增' : '修改'" width="30%" :before-close="handleClose">
        <el-form :model="form" label-width="120px" class="form" :rules="rules" ref="ruleFormRef">
            <el-form-item label="合同名称" prop="contractname">
                <el-input v-model="form.contractname" />
            </el-form-item>
            <el-form-item label="合同内容" prop="contractcontent">
                <el-input v-model="form.contractcontent" :rows="2" type="textarea" placeholder="请输入内容" />
            </el-form-item>
            <el-form-item label="合同生效时间" prop="contractstart">
                <el-date-picker v-model="form.contractstart" type="datetime" placeholder="选择日期"
                    value-format="YYYY-MM-DD HH:mm:ss" />
            </el-form-item>
            <el-form-item label="合同结束时间" prop="contractend">
                <el-date-picker v-model="form.contractend" type="datetime" placeholder="选择日期"
                    value-format="YYYY-MM-DD HH:mm:ss" />
            </el-form-item>
            <el-form-item label="客户名称" prop="clientname">
                <el-tree-select :props="{ value: 'clientname', label: 'staffname', children: 'children' }"
                    placeholder="选择客户" size="large" v-model="form.staffname" :data="treedata1" check-strictly />
            </el-form-item>
            <!-- <el-form-item label="客户名称" prop="staffname">
                <el-input v-model="form.staffname" />
            </el-form-item> -->
            <el-form-item label="合同金额" prop="amount">
                <el-input-number v-model="form.amount" :precision="2" :stenpp="0.1" />
            </el-form-item>
            <el-form-item label="备注" prop="description">
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
import { addContract, editContract, getContractDataNew, getClientDataNew } from '../../../../http'
import { ContractModel } from '../class/ContractModel'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage } from 'element-plus'

//日期选择框
const date1 = () => {
    const date = new Date()
    date.setTime(date.getTime() - 3600 * 1000 * 24)
    return date
}


const ruleFormRef = ref<FormInstance>()
const props = defineProps({
    addVisible: Boolean,
    info: ContractModel
})
const form = ref({
    id: props.info?.id,
    contractid: "",
    contractname: "",
    contractcontent: "",
    contractstart: "",
    contractend: "",
    staffname: "",
    amount: 0,
    description: "",
})
const rules = reactive<FormRules>({
    positionname: [{ required: true, message: '请输入职务名称', trigger: 'blur' }]
})

//监听
watch(
    () => props.info,
    (newInfo, oldInfo) => {
        if (newInfo != undefined) {
            let currInfo: ContractModel = (JSON.parse(newInfo as any)) as ContractModel
            form.value = {
                id: currInfo.id,
                contractid: currInfo.contractid,
                contractname: currInfo.contractname,
                contractcontent: currInfo.contractcontent,
                contractstart: currInfo.contractstart,
                contractend: currInfo.contractend,
                staffname: currInfo.staffname,
                amount: currInfo.amount,
                description: currInfo.description
            }
        } else {
            form.value = {
                id: 0,
                contractid: "",
                contractname: "",
                contractcontent: "",
                contractstart: "",
                contractend: "",
                staffname: "",
                amount: 0,
                description: ""
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
    LoadContractData()
    LoadClientData()

})
const LoadContractData = async () => {
    let parms = {
        contractid: "",
        contractname: "",
        contractcontent: "",
        contractstart: "",
        contractend: "",
        staffname: "",
        amount: 0,
        description: "",
        PageIndex: 1,
        PageSize: 10
    }
    treedata.value = (await getContractDataNew(parms)).data

}


//读取下拉数据
const treedata1 = ref()
const LoadClientData = async () => {
    let parms = {
        clientname: "",
        clienttel: "",
        clientaddress: "",
        clientemail: "",
        description: "",
        PageIndex: 1,
        PageSize: 10

    }
    treedata1.value = (await getClientDataNew(parms)).data
    // console.log("后端数据信息")
    //console.log(res)

}


const submit = async (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return
    var parms = {
        Id: form.value.id
        , Contractid: form.value.contractid
        , Contractname: form.value.contractname
        , Contractcontent: form.value.contractcontent
        , Contractstart: form.value.contractstart
        , Contractend: form.value.contractend
        , Staffname: form.value.staffname
        , Amount: form.value.amount
        , Description: form.value.description
    }
    console.log(parms)
    if (props.info == undefined) {
        // 执行添加逻辑 
        const res = await addContract(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '添加成功！',
                type: 'success',
            })
            LoadContractData()

        }
    } else {
        // 执行修改逻辑
        const res = await editContract(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '编辑成功！',
                type: 'success',
            })
            //  LoadMenuData()
            LoadContractData()

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

const value1 = ref('')
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