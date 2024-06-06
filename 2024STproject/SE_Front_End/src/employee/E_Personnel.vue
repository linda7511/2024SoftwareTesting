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
    data() {
        return {
            items: [
                {
                    index: '/e_home/personnel/check_attendance',
                    key: '4.1',
                    title: '考勤管理',
                },
                {
                    index: '/e_home/personnel/employee_application',
                    key: '4.2',
                    title: '员工申请',
                },
                {
                    index: '/e_home/personnel/job_scheduling',
                    key: '4.3',
                    title: '岗位调度',
                },
                {
                    index: '/e_home/personnel/salary_manage',
                    key: '4.4',
                    title: '工资管理',
                },
            ]
        }
    },

    methods: {
        navigateTo(to) {
            this.$router.push(to);
        },
    },
}
</script>

<style></style>