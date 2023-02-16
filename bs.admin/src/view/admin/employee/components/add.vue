<template>
    <el-dialog v-model="addVisible" :title="info == undefined ? '新增' : '修改'" width="30%" :before-close="handleClose">
        <el-form :model="form" label-width="120px" class="form" :rules="rules" ref="ruleFormRef">
            <el-form-item label="姓名" prop="name">
                <el-input v-model="form.name" />
            </el-form-item>
            <el-form-item label="年龄" prop="age">
                <el-input v-model="form.age" />
            </el-form-item>
            <el-form-item label="性别" prop="sex">
                <el-select v-model="form.sex" class="m-2" placeholder="选择性别" size="large">
                    <el-option v-for="item in options3" :key="item.value" :label="item.label" :value="item.value" />
                </el-select>
            </el-form-item>
            <!-- <el-form-item label="部门" prop="department">
                <el-select v-model="form.department" class="m-2" placeholder="选择部门" size="large">
                    <el-option v-for="item in options" :key="item.value" :label="item.label" :value="item.value" />
                </el-select>
            </el-form-item> -->
            <el-form-item label="部门" prop="department">
                <el-tree-select :props="{ value: 'departmentname', label: 'department', children: 'children' }"
                    placeholder="选择部门" size="large" v-model="form.department" :data="treedata" check-strictly />
            </el-form-item>
            <el-form-item label="职务" prop="position">
                <el-tree-select :props="{ value: 'positionname', label: 'position', children: 'children' }"
                    placeholder="选择职务" size="large" v-model="form.position" :data="treedata1" check-strictly />
            </el-form-item>
            <!-- <el-form-item label="职位" prop="position">
                <el-select v-model="form.position" class="m-2" placeholder="选择" size="large">
                    <el-option v-for="item in options2" :key="item.value" :label="item.label" :value="item.value" />
                </el-select>
            </el-form-item> -->
            <el-form-item label="联系电话" prop="tel">
                <el-input v-model="form.tel" />
            </el-form-item>
            <!-- <el-form-item label="联系电话" prop="tel">
                <el-switch v-model="form.tel" />
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
import { addEmployee, editEmployee, getEmployeeDataNew, getDepartmentDataNew ,getPositionDataNew} from '../../../../http';
import { EmployeeModel } from '../class/EmployeeModel'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage } from 'element-plus'



const options3 = [
    {
        value: '男',
        label: '男',
    },
    {
        value: '女',
        label: '女',
    },
]
const ruleFormRef = ref<FormInstance>()
const props = defineProps({
    addVisible: Boolean,
    info: EmployeeModel
})
const form = ref({
    id: props.info?.id,
    nameid: "",
    name: "",
    sex: "",
    age: 0,
    department: "",
    position: "",
    tel: 0,
    order: 0,
    description: "",
})
const rules = reactive<FormRules>({
    name: [{ required: true, message: '请输入名称', trigger: 'blur' }]
})

//监听
watch(
    () => props.info,
    (newInfo, oldInfo) => {
        if (newInfo != undefined) {
            let currInfo: EmployeeModel = (JSON.parse(newInfo as any)) as EmployeeModel
            form.value = {
                id: currInfo.id,
                nameid: currInfo.nameid,
                name: currInfo.name,
                sex: currInfo.sex,
                age: currInfo.age,
                department: currInfo.department,
                position: currInfo.position,
                tel: currInfo.tel,
                order: currInfo.order,
                description: currInfo.description
            }
        } else {
            form.value = {
                id: 0,
                nameid: '',
                name: '',
                sex: '',
                age: 0,
                department: "",
                position: "",
                tel: 0,
                order: 0,
                description: ''
            }
            console.log("置空" + form.value)
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
//获取部门信息
    LoadDepartmentData()
//获取职务信息
    LoadPositionData()
})


const LoadDepartmentData = async () => {
    let parms = {
        departmentnameid: "",
        departmentname: "",
        count: "",
        order: 0,
        description: "",
        PageIndex: 1,
        PageSize: 10
    }
    treedata.value = (await getDepartmentDataNew(parms)).data
    // console.log("employee add")
    // console.log(treedata.value)
}
//读取下拉数据
const treedata1 = ref()
const LoadPositionData = async () => {
    let parms = {
        Positionid: "",
        Positionname: "",
        Description: "",
        PageIndex: 1,
        PageSize: 10

    }
    treedata1.value =  (await getPositionDataNew(parms)).data
    // console.log("后端数据信息")
    //console.log(res)

}


const submit = async (ruleFormRef: FormInstance | undefined) => {
    if (!ruleFormRef) return
    var parms = {
        Id: form.value.id
        , Nameid: form.value.nameid
        , Name: form.value.name
        , Sex: form.value.sex
        , Age: form.value.age
        , Department: form.value.department
        , Position: form.value.position
        , Order: form.value.order
        , Tel: form.value.tel
        , Description: form.value.description
    }
    console.log(parms)
    if (props.info == undefined) {
        // 执行添加逻辑 
        const res = await addEmployee(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '添加成功！',
                type: 'success',
            })
            LoadDepartmentData()
        }
    } else {
        // 执行修改逻辑
        const res = await editEmployee(parms) as any as boolean
        if (res) {
            ElMessage({
                message: '编辑成功！',
                type: 'success',
            })
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