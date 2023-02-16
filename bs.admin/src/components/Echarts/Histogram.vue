<script lang="ts" setup>
import * as echarts from 'echarts'
//页面加载完成后加载我们的图标
import { onMounted } from 'vue'
onMounted(async () => {
    var chartDom = document.getElementById('echarsMain-Histogram');
    var myChart = echarts.init(chartDom as HTMLElement);
    myChart.resize({
        width: 700,
        height: 400
    });
    //属性 option
    const option = {
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'shadow'
            }
        },
        grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
        },
        xAxis: [
            {
                type: 'category',
                data: ['Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat', 'Sun'],
                axisTick: {
                    alignWithLabel: true
                }
            }
        ],
        yAxis: [
            {
                type: 'value'
            }
        ],
        series: [
            {
                name: 'Direct',
                type: 'bar',
                barWidth: '60%',
                data: [10, 52, 200, 334, 390, 330, 220]
            }
        ]
    };
    //绘制图标
    myChart.setOption(option)
   // location.reload()

})
</script>
<template>
    <div id="echarsMain-Histogram"></div>
</template>