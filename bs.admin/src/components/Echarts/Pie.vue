<script lang="ts" setup>
import * as echarts from 'echarts'
//页面加载完成后加载我们的图标
import { onMounted ,reactive} from 'vue'
import { getEmployeeDataNew } from '../../http';


onMounted(async () => {
    LoadTableData()
    var chartDom = document.getElementById('echarsMain-Pie');
    var myChart = echarts.init(chartDom as HTMLElement);
    myChart.resize({
        width: 800,
        height: 400
    });
    //属性 option
    const option = {
        title: {
            text: 'Referer of a Website',
            subtext: 'Fake Data',
            left: 'center'
        },
        tooltip: {
            trigger: 'item'
        },
        legend: {
            orient: 'vertical',
            left: 'left'
        },
        series: [
            {
                name: 'Access From',
                type: 'pie',
                radius: '50%',
                data: [
                    { value: form.Total, name: form.department },
                    { value: 735, name: 'Direct' },
                    { value: 580, name: 'Email' },
                    { value: 484, name: 'Union Ads' },
                    { value: 300, name: 'Video Ads' }
                ],
                emphasis: {
                    itemStyle: {
                        shadowBlur: 10,
                        shadowOffsetX: 0,
                        shadowColor: 'rgba(0, 0, 0, 0.5)'
                    }
                }
            }
        ]
    };
    //绘制图标
    myChart.setOption(option)
})

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
    console.log(form.Total)
}
</script>
<template>
    <div id="echarsMain-Pie"></div>

</template>