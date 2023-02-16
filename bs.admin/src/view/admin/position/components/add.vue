<template>
    <el-dialog v-model="addVisible" :title="info == undefined ? '新增' : '修改'" width="30%" :before-close="handleClose">
        <el-form :model="form" label-width="120px" class="form" :rules="rules" ref="ruleFormRef">
            <el-form-item label="职务名称" prop="positionname">
                <el-input v-model="form.positionname" />
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
import { addPosition, editPosition, getPositionDataNew } from '../../../../http'
import { PositionModel } from '../class/PositionModel'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage } from 'element-plus'

const ruleFormRef = ref<FormInstance>()
const props = defineProps({
    addVisible: Boolean,
    info: PositionModel
})
const form = ref({
    id: props.info?.id,
    positionid: '',
    positionname: '',
    description: '',
})
const rules = reactive<FormRules>({
    positionname: [{ required: true, message: '请输入职务名称', trigger: 'blur' }]
})

//监听
watch(
    () => props.info,
    (newInfo, oldInfo) => {
        if (newInfo != undefined) {
            let currInfo: PositionModel = (JSON.parse(newInfo as any)) as PositionModel
            form.value = {
                id: currInfo.id,
                positionid: currInfo.positionid,
                positionname: currInfo.positionname,
                description: currInfo.description
            }
        } else {
            form.value = {
                id: 0,
                positionid: '',
                positionname: '',
                description: ''
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
    LoadPositionData()

})
const LoadPositionData = async () => {
    let parms = {
        Positionid: "",
        Positionname: "",
        Description: "",
        PageIndex: 1,
        PageSize: 10
    }
    treedata.value = (await getPositionDataNew(parms)).data

}


const submit = async (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return
    var parms = {
        Id: form.value.id
        , Positionid: form.value.positionid
        , Positionname: form.value.positionname
        , Description: form.value.description
    }
    console.log(parms)
    if (props.info == undefined) {
        // 执行添加逻辑 
        const res = await addPosition(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '添加成功！',
                type: 'success',
            })
            LoadPositionData()

        }
    } else {
        // 执行修改逻辑
        const res = await editPosition(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '编辑成功！',
                type: 'success',
            })
            //  LoadMenuData()
            LoadPositionData()

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