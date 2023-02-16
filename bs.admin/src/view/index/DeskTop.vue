<template>
    <div class="cardContent">
        <el-card class="box-card" v-for="item in list">
            <CardCom :info="item"></CardCom>
        </el-card>
        <el-card class="left">
            <!-- 饼图 -->
            <Pie></Pie>
        </el-card>
        <el-card class="right">
            <!-- 柱状图 -->
            <Histogram></Histogram>
        </el-card>
        <el-card class="lineCard">
            <!-- 折线图 -->
            <Line></Line>
        </el-card>
    </div>
</template>
<script lang="ts" setup>

import { ref, reactive, onMounted, onBeforeUpdate, } from 'vue'
import CardCom from '../../components/CardCom.vue'
import Pie from '../../components/Echarts/Pie.vue'
import Histogram from '../../components/Echarts/Histogram.vue'
import Line from '../../components/Echarts/Line.vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getEmployeeDataNew } from '../../http'
import { useRouter, Router } from 'vue-router'

onMounted(async () => {
    LoadTableData()

  //  shua()

})
let invoke = false

const shua = (async () => {

    if (!invoke) {

        invoke = true
        
    }


})


//window.location.reload()
const form = reactive({
    nameid: "",
    name: "",
    sex: "",
    age: 1,
    department: "",
    position: "",
    tel: 0,
    order: 0,
    description: "",
    pageIndex: 1,
    pageSize: 10,
    Total: 0
})
const LoadTableData = async (name: string = "") => {
    let parms = {
        Nameid: form.nameid,
        Name: form.name,
        Sex: form.sex,
        Age: form.age,
        Department: form.department,
        Position: form.position,
        Tel: form.tel,
        Order: form.order,
        Description: form.description,
        PageIndex: form.pageIndex,
        PageSize: form.pageSize
    }
    let res = await getEmployeeDataNew(parms) as any
    form.Total = res.total
    //  console.log(form.Total)
}

const list = ref([
    {
        "Title": "公司当前人数：",
        "Icon": "CoffeeCup",
        "Count": form.Total
    },
    {
        "Title": "当前部门数量：",
        "Icon": "Apple",
        "Count": 8912
    },
    {
        "Title": "成交金额",
        "Icon": "Drizzling",
        "Count": 9280
    },
    {
        "Title": "购物总量",
        "Icon": "Headset",
        "Count": 120
    }
])


// watch(
//     route(to, from){
//         window.location.reload();

//     }


// );

</script>
<style lang="scss" scoped>
.cardContent {
    width: 100%;
    margin: 0px auto;

    .box-card {
        float: left;
        width: 24%;
        margin-right: 5px;
        margin-bottom: 20px;
    }

    .left,
    .right {
        float: left;
        width: 48%;
        margin-bottom: 20px;
    }

    .lineCard {
        width: 97.5%;
    }

    .right {
        margin-left: 20px;
    }

}
</style>