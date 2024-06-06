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
                    index:'/e_home/restaurant/dish_manage',
                    key:'5.1',
                    title:'菜品管理',
                },
                {
                    index:'/e_home/restaurant/order_dishes',
                    key:'5.2',
                    title:'点菜功能',
                },
                {
                    index:'/e_home/restaurant/table_manage',
                    key:'5.3',
                    title:'桌位管理',
                },
                {
                    index:'/e_home/restaurant/accounts_manage',
                    key:'5.4',
                    title:'账目管理',
                },
                {
                    index:'/e_home/restaurant/senior_functions',
                    key:'5.5',
                    title:'高级功能',
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