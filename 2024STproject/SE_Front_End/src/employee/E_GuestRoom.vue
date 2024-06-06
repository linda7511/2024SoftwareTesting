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
                    index:'/e_home/guestroom/room_clean',
                    key:'4.1',
                    title:'客房清洁',
                },
                {
                    index:'/e_home/guestroom/repair_tasks',
                    key:'4.2',
                    title:'维修任务',
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