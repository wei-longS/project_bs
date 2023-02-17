<template>
    <div class="login">
        <div class="relative">
            <div class="left">
                <el-row>
                    <el-col :span="24">
                        <div class="homepageLogo">
                            <ul>
                                <li>
                                    <el-image style="width: 50px; height: 50px" :src="url" fit="fit" />
                                </li>
                                <li><span>企业信息管理系统</span></li>
                            </ul>
                        </div>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-image class="boxbg" :src="boxbg" fit="fit" />
                        <p class="p1">欢迎使用本系统</p>
                        <p class="p2">基于.NET CORE企业信息管理系统</p>
                    </el-col>
                </el-row>
            </div>
            <div class="right">
                <el-row>
                    <el-col :span="24">
                        <h2>登录</h2>
                    </el-col>
                </el-row>
                <el-row>
                    <el-col :span="24">
                        <el-form :model="form" label-width="120px" label-position="top" size="large" class="form"
                            :rules="rules" ref="ruleFormRef">
                            <el-form-item label="用户名" prop="userName">
                                <el-input v-model="form.userName" />
                            </el-form-item>
                            <el-form-item label="密码" prop="passWord">
                                <el-input v-model="form.passWord" type="password" show-password />
                            </el-form-item>
                            <el-form-item>
                                <el-button class="submitBtn" type="primary" @keydowm.enter="keyDown" @click="onSubmit(ruleFormRef)">登录
                                </el-button>
                            </el-form-item>
                        </el-form>
                    </el-col>
                </el-row>
            </div>
        </div>
    </div>
</template>
<script lang="ts" setup>
import { ref, reactive ,onMounted ,onUnmounted} from 'vue'
import type { FormInstance, FormRules } from 'element-plus'
import { ElMessage } from 'element-plus'
import { getToken ,addLoginlog } from '../../http/index'
import Tool from '../../global'
import { UserInfo } from './class/UserInfo'
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
const store=useStore()
const router = useRouter()
const url = ref('/images/logo.0606fdd2.png')
const boxbg = ref('/images/svgs/login-box-bg.svg')

 onMounted(()=>{
    
    //按回车登录
    window.addEventListener('keydown' ,keyDown)
 })
const keyDown=(e:any)=>{
    if(e.keyCode==13){
     // onSubmit(ruleFormRef)
     console.log("过来了")
     onSubmit

     console.log("过去了....")
    }
}



onUnmounted(()=>{
    window.removeEventListener('keydown' ,keyDown ,false)
}) 

const form = reactive({
    userName: '',
    passWord: ''
})
const rules = reactive<FormRules>({
    //用户名
    userName: [{ required: true, message: "请输入用户名", trigger: 'blur' }],
    //密码
    passWord: [{ required: true, message: "请输入密码", trigger: 'blur' }]
})
const ruleFormRef = ref<FormInstance>()

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

        Loginlogname: form.userName,
        Loginlogcontent: form.userName+'登录成功',
        Createtime: form1.createtime,
    }
    console.log(parms)

    // 执行添加逻辑 
    await addLoginlog(parms) as any as boolean
        form1.loginlogname = ''
}

const onSubmit = async (ruleFormRef: FormInstance | undefined) => {
    //console.log("执行了。。。。")

    if (!ruleFormRef) return;
    await ruleFormRef.validate(async (valid, fields) => {
        if (valid) {
            //请求后端数据，获取token，并将token放入localStorage
            const token = await getToken(form.userName, form.passWord) as any as string
            const user: UserInfo = JSON.parse(new Tool().FormatToken(token))
            localStorage["token"] = token 
            localStorage["nickname"] = user.NickName
            store.commit("SettingNickName",user.NickName)
            store.commit("SettingToken",token)
            router.push({ path: '/desktop' });
            sender()
            ElMessage({
            message: user.NickName+'登录成功！',
            type: 'success',
        })

        } else {
            console.log("校验不通过！")
            console.log(fields)
            let errors: string = "";
            fields?.userName?.forEach(element => {
                errors += element.message + ';'
            })
            fields?.passWord?.forEach(element => {
                errors += element.message + ';'
            })
            ElMessage({
                type: "warning",
                message: errors
            })
        }
    })
}


</script>
<style lang="scss" scoped>
.login {
    width: 100%;
    height: 100%;

    .relative {
        width: 100%;
        height: 100%;
        text-align: center;

        .left {
            width: 50%;
            height: 100%;
            float: left;
            background-image: url('/images/svgs/login-bg.svg');

            .boxbg {
                width: 350px;
                height: 350px;
                margin-top: 100px;
            }

            .homepageLogo {
                height: 50px;
                line-height: 50px;
                margin-top: 40px;

                span {
                    color: white;
                    font-size: 24px;
                }

                ul {
                    list-style: none;

                    li {
                        float: left;
                        margin-left: 5px;
                    }
                }
            }

            p {
                color: white;
            }

            .p1 {
                font-size: 1.875rem;
                line-height: 2.25rem;
            }

            .p2 {
                font-size: 0.875rem;
                line-height: 1.25rem;
            }
        }

        .right {
            width: 50%;
            float: left;
            padding-top: 15%;

            .form {
                width: 50%;
                margin: 0px auto;

                .submitBtn {
                    width: 100%;
                }
            }
        }
    }
}
</style>