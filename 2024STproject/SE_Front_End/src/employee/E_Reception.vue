<template>
    <div class="my-div">
        
    </div>
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
                    index:'/e_home/reception/resident_checkin',
                    key:'2.2',
                    title:'宾客入住',
                },
                {
                    index:'/e_home/reception/idle_query',
                    key:'2.3',
                    title:'空闲查询',
                },
                {
                    index:'/e_home/reception/guest_info',
                    key:'2.4',
                    title:'宾客信息',
                },
                {
                    index:'/e_home/reception/process_order',
                    key:'2.5',
                    title:'处理订单',
                }
               
               ],
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