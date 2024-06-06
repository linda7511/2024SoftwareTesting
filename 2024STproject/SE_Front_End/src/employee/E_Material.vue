<template>
   <div class="child-class">
        <router-view v-slot="props">
        <keep-alive>
            <component :is="props.Component"></component>
        </keep-alive>
        </router-view>
   </div>
</template>

<script>
    export default {
        data(){
            return{
               items:[
                {
                    index:'/e_home/material/purchasing_materials',
                    key:'3.1',
                    title:'采购物资',
                },
                {
                    index:'/e_home/material/view_inventory',
                    key:'3.2',
                    title:'查看库存',
                },
                {
                    index:'/e_home/material/maintain_records',
                    key:'3.3',
                    title:'维护记录',
                },
                {
                    index:'/e_home/material/material_info',
                    key:'3.4',
                    title:'物资信息',
                },
                {
                    index:'/e_home/material/consumption_records',
                    key:'3.5',
                    title:'消耗记录',
                }
               ]
            }
        },

        methods:{
            navigateTo(to) {
        this.$router.push(to);
        },
        },
    }
</script>

<style >

</style>
