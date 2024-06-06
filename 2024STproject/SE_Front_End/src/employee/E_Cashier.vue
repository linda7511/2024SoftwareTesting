<template>
    <div class="child-class">
        <!--<h2>前台收银主页面</h2>-->
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
                    index:'/e_home/cashier/settlement_bill',
                    key:'3.1',
                    title:'结算帐单',
                },
               
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