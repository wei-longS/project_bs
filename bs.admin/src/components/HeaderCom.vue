<template>
    <el-row>
        <el-col :span="12">
            <el-breadcrumb separator="/">
                <el-breadcrumb-item><a href="/desktop">
                        <el-icon>
                            <house />
                        </el-icon>首页
                    </a></el-breadcrumb-item>
                <el-breadcrumb-item>这是一个管理中小型企业信息的管理系统</el-breadcrumb-item>
            </el-breadcrumb>
        </el-col>
        <el-col :span="12">
            <div class="dropdown">
                <el-avatar :size="30" :src="circleUrl" />
                <el-dropdown>
                    <span class="el-dropdown-link">
                        {{ NickName }}
                        <el-icon class="el-icon--right">
                            <arrow-down />
                        </el-icon>
                    </span>
                    <template #dropdown>
                        <el-dropdown-menu>
                            <el-dropdown-item>
                                <el-link :underline="false"><span @click="goToPerson">个人主页</span></el-link>
                            </el-dropdown-item>
                            <el-dropdown-item><span @click="logOut">退出</span></el-dropdown-item>
                        </el-dropdown-menu>
                    </template>
                </el-dropdown>
            </div>
        </el-col>
    </el-row>
    <el-row>
        <el-col :span="24">
            <el-divider />
            <TagComVue></TagComVue>
        </el-col>
    </el-row>
</template>
<script lang="ts" setup>
import { ref, onMounted } from 'vue'
import TagComVue from './TagCom.vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';
import Tool from '../global';
const circleUrl = ref('/images/Person.jpg')
const NickName = ref()
const router = useRouter()
onMounted(() => {
    NickName.value = useStore().state.NickName
})
const goToPerson = () => {
    router.push({ path: "/person" })
}
const logOut = () => {
    new Tool().ClearLocalStorage()
    router.push({ path: '/login' });
}
</script>
<style lang="scss" scoped>
.el-header {
    .el-col {
        height: 50px;
        line-height: 50px;

        .el-breadcrumb {
            line-height: inherit;
        }

        .el-icon {
            margin-right: 5px;
        }

        .el-divider {
            margin: 0;
        }
    }
}
</style>