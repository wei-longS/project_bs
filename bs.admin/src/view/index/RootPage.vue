<script setup lang="ts">
import { ref, onMounted, toRef } from 'vue'
import HeaderCom from '../../components/HeaderCom.vue'
import TreeMenuVue from '../../components/TreeMenu.vue';
import { useRouter, useRoute } from 'vue-router'
import { useStore } from 'vuex'
import { TagModel } from '../../class/TagModel';
import { getUserMenus } from '../../http/index'
import { MenuModel } from '../admin/menu/class/MenuModel';
const url = ref('/images/logo.ico')
url.value = "/images/logo.0606fdd2.png"
// const res = ref([
//   {
//     "id": 1,
//     "index": "rolemanager",
//     "name": "角色管理",
//     "filePath": "../view/admin/role/RoleManager",
//     "parent": "rolemanager",
//     "order": 99,
//     "date": "2016-05-02",
//     "isEnable": false,
//     "children": null
//   },
//   {
//     "id": 2,
//     "index": "personmanager",
//     "name": "人员管理",
//     "filePath": "../view/admin/person/PersonManager",
//     "parent": "rolemanager",
//     "order": 99,
//     "date": "2016-05-02",
//     "isEnable": false,
//     "children": null
//   },
//   {
//     "id": 3,
//     "index": "menumanager",
//     "name": "菜单管理",
//     "filePath": "../view/admin/menu/MenuManager",
//     "parent": "rolemanager",
//     "order": 99,
//     "date": "2016-05-02",
//     "isEnable": false,
//     "children": null
//   },
//   {
//     "id": 5,
//     "index": "test",
//     "name": "多层级菜单测试1",
//     "parent": "rolemanager",
//     "order": 99,
//     "date": "2016-05-02",
//     "isEnable": false,
//     "filePath": "test",
//     "children": [
//       {
//         "index": "test2",
//         "name": "权限列表2",
//         "children": [
//           {
//             "index": "test3",
//             "name": "权限列表2-1"
//           }
//         ]
//       },
//       {
//         "index": "test23",
//         "name": "权限列表3"
//       },
//       {
//         "index": "test24",
//         "name": "权限列表4"
//       }
//     ]
//   }
// ])
const res = ref()
onMounted(async () => {
  res.value = await getUserMenus() as any as MenuModel[]
})
const store = useStore()
const router = useRouter()
const handleSelect = (index: string) => {
  //根据index从路由列表中获取name
  let name = router.getRoutes().filter(item => item.path == "/" + index)[0].name as string
  //修改vuex的值
  let model = new TagModel()
  model.index = index
  model.name = name
  store.commit("AddTag", model)
}
</script>
<template>
  <div class="common-layout">
    <el-container>
      <el-aside class="aside-menu">
        <el-row>
          <el-col :span="24">
            <div class="homepageLogo">
              <ul>
                <li>
                  <el-image style="width: 50px; height: 50px" :src="url" fit="fill" />
                </li>
                <li><span>信息管理系统</span></li>
              </ul>
            </div>
          </el-col>
        </el-row>
        <el-row class="tac">
          <el-col :span="24">
            <el-menu active-text-color="#ffd04b" background-color="#545c64" class="el-menu-vertical-demo"
              default-active="2" text-color="#fff" router @select="handleSelect">
              <!-- 默认会有个首页的入口 -->
              <el-menu-item index='/desktop'>
                <template #title>
                  <el-icon>
                    <list />
                  </el-icon>
                  <span>首页</span>
                </template>
              </el-menu-item>
              <el-menu-item index='/person'>
                <template #title>
                  <el-icon>
                    <list />
                  </el-icon>
                  <span>信息编辑</span>
                </template>
              </el-menu-item>
          <!--   <el-menu-item index='/menumanager'>
                <template #title>
                  <el-icon>
                    <list />
                  </el-icon>
                  <span>菜单管理</span>
                </template>
              </el-menu-item>
                <el-menu-item index='/rolemanager'>
                <template #title>
                  <el-icon>
                    <list />
                  </el-icon>
                  <span>角色管理</span>
                </template>
              </el-menu-item>
              <el-menu-item index='/personmanager'>
                <template #title>
                  <el-icon>
                    <list />
                  </el-icon>
                  <span>用户管理</span>
                </template>
              </el-menu-item> -->
              <TreeMenuVue :list="res"></TreeMenuVue>
            </el-menu>
          </el-col>
        </el-row>
      </el-aside>
      <el-container>
        <el-header>
          <HeaderCom></HeaderCom>
        </el-header>
        <el-main>
          <router-view></router-view>
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>
<style lang="scss" scoped>
.homepageLogo {
  height: 50px;
  line-height: 50px;

  span {
    color: white;
    font-size: 24px;
  }

  ul {
    li {
      float: left;
      margin-left: 5px;
    }
  }
}
</style>
