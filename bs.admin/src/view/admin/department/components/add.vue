<template>
    <el-dialog v-model="addVisible" :title="info == undefined ? '新增' : '修改'" width="30%" :before-close="handleClose">
        <el-form :model="form" label-width="120px" class="form" :rules="rules" ref="ruleFormRef">
            <!-- <el-form-item label="部门编号" prop="departmentnameid">
                <el-input v-model="form.departmentnameid" />
            </el-form-item> -->
            <el-form-item label="部门名称" prop="departmentname">
                <el-input v-model="form.departmentname" />
            </el-form-item>
            <!-- <el-form-item label="部门人数" prop="age">
                <el-input v-model="form.count" />
            </el-form-item> -->
            <!-- <el-form-item label="部门" prop="department">
                <el-tree-select :props="{ value: 'name', label: 'department', children: 'children' }" placeholder="选择部门"
                    size="large" v-model="form.department" :data="treedata" check-strictly />
            </el-form-item> -->
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
import { addDepartment, editDepartment, getDepartmentDataNew } from '../../../../http';
import { DepartmentModel } from '../class/DepartmentModel'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage } from 'element-plus'

const ruleFormRef = ref<FormInstance>()
const props = defineProps({
    addVisible: Boolean,
    info: DepartmentModel
})
const form = ref({
    id: props.info?.id,
    departmentnameid: '',
    departmentname: '',
    count: '',
    order: 0,
    description: '',
})
const rules = reactive<FormRules>({
    departmentname: [{ required: true, message: '请输入部门名称', trigger: 'blur' }]
})

//监听
watch(
    () => props.info,
    (newInfo, oldInfo) => {
        if (newInfo != undefined) {
            let currInfo: DepartmentModel = (JSON.parse(newInfo as any)) as DepartmentModel
            form.value = {
                id: currInfo.id,
                departmentnameid: currInfo.departmentnameid,
                departmentname: currInfo.departmentname,
                count: currInfo.count,
                order: currInfo.order,
                description: currInfo.description
            }
        } else {
            form.value = {
                id: 0,
                departmentnameid: '',
                departmentname: '',
                count: '',
                order: 0,
                description: ''
            }
            console.log("置空"+form.value)
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
    LoadDepartmentData()
 //   LoadMenuData()
})
const LoadDepartmentData = async () => {
    let parms = {
        Departmentnameid: "",
        Departmentname: "",
        Count: "",
        Order: 1,
        Description: "",
        PageIndex: 1,
        PageSize: 10
    }
    treedata.value = (await getDepartmentDataNew(parms)).data

}


const submit = async (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return
    var parms = {
        Id: form.value.id
        ,Departmentnameid: form.value.departmentnameid
        , Departmentname: form.value.departmentname
        , Count: form.value.count
        , Order: form.value.order
        , Description: form.value.description
    }
    console.log(parms)
    if (props.info == undefined) {
        // 执行添加逻辑 
        const res = await addDepartment(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '添加成功！',
                type: 'success',
            })
            LoadDepartmentData()
            
        }
    } else {
        // 执行修改逻辑
        const res = await editDepartment(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '编辑成功！',
                type: 'success',
            })
          //  LoadMenuData()
            LoadDepartmentData()
            
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