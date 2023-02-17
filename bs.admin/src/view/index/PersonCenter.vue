<template>
    <el-row>
        <el-col :span="6">
            <el-form :model="form" label-width="120px">
                <el-form-item label="昵称">
                    <el-input v-model="form.name" />
                </el-form-item>
                <el-form-item label="密码">
                    <el-input v-model="form.password" />
                </el-form-item>
                <el-form-item>
                    <el-button type="primary" @click="onSubmit">修改</el-button>
                    <!-- <el-button>取消</el-button> -->
                </el-form-item>
            </el-form>
        </el-col>

        <!-- <el-calendar v-model="value" /> -->

    </el-row>
    <el-form-item>
        <!-- <h2>仅可修改账号昵称，如要修改账号名，请登录管理员进行修改</h2> -->
    </el-form-item>

</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue'
import { useStore } from 'vuex'
import { editNickNameOrPassword ,addLoginlog ,getUserData} from '../../http';
import { ElMessage, ElMessageBox } from 'element-plus';
import Tool from '../../global'
import { useRouter } from 'vue-router';
import type { UploadProps, UploadUserFile } from 'element-plus'

const value = ref(new Date())
const router = useRouter()
const form = reactive({
    name: useStore().state.NickName,
    password: '',
})
//登录日志
const form1 = reactive({
    loginlogname: "",
    loginlogcontent: "",
    createtime: "",
    pageIndex: 1,
    pageSize: 10,
    Total: 0
})

const sender = async () => {
    var parms = {

        Loginlogname: form1.loginlogname,
        Loginlogcontent: form1.loginlogcontent +'退出登录',
        Createtime: form1.createtime,
    }
    console.log(parms)

    // 执行添加逻辑 
   await addLoginlog(parms) as any as boolean
        form1.loginlogname = ''
}

const onSubmit = async () => {
    ElMessageBox.confirm(
        '你确定要修改吗?',
        {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning',
        }
    )
        .then(() => {
            // console.log('submit!')
            let res: Boolean = editNickNameOrPassword(form.name, form.password) as any as boolean
            if (res) {
                ElMessage.success("设置成功！即将退出，请重新登录...")
                sender()
                setTimeout(function () {
                    new Tool().ClearLocalStorage()
                    router.push({ path: "/login" })
                    
                }, 2000)
            } else {
                ElMessage.success("设置失败！请联系系统管理员")
            }
        })
        .catch(() => {
            ElMessage({
                type: 'info',
                message: '取消修改',
            })
        })

}

</script>
