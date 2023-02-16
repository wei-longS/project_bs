<script lang="ts" setup>
import { CoffeeCup, Apple, Drizzling, Headset } from '@element-plus/icons-vue'
import { defineProps } from 'vue';
import { CardModel } from '../class/CardModel'
defineProps({
    info: CardModel
})
</script>
<template>
    <div class="card">
        <div class="left">
            <coffee-cup v-if="info?.Icon == 'CoffeeCup'" />
            <apple v-if="info?.Icon == 'Apple'" />
            <drizzling v-if="info?.Icon == 'Drizzling'" />
            <headset v-if="info?.Icon == 'Headset'" />
        </div>
        <div class="right">
            <div><span>{{ info?.Title }}</span></div>
            <div><span>{{ info?.Count }}</span></div>
        </div>
    </div>
</template>
<style lang="scss" scoped>
.card {
    height: 50px;
    line-height: 25px;
}

.left {
    width: 50px;
    height: 50px;
    float: left;

    .el-icon {
        font-size: 50px;
    }
}

.right {
    width: 150px;
    height: 50px;
    float: right;
}
</style>