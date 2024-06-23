<template>
    <div class="home">
        <el-container>
            <el-header>
                <div class="header">
                    <div class="title"> 济客管理系统 </div>
                    <el-button class="logout" @click="handleLogout"><span class="btn-text">退出</span></el-button>
                    <el-button class="change-pwd" @click="changePwdVisible = true"><span class="btn-text">修改密码</span></el-button>
                </div>
            </el-header>
            <el-container class="content-wrapper">
                <el-aside width="250px" style="background-color: #324157;">
                    <div class="sidebar">
                        <div class="avatar">
                            <el-avatar
                                @click="userInfoVisiblie = true"
                                :size="128"
                                shape="circle"
                                src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png"
                                class="avatar-img"
                            ></el-avatar>
                        </div>
                        <div class="avatar-info">
                            <p class="avatar-info-value"> {{ userInfo.name }} </p>
                        </div>
                        <hr style="width: 65%;">
                        <el-menu
                            class="sidebar-el-menu"
                            :default-active="onRoutes"
                            :collapse="collapse"
                            background-color="#324157"
                            text-color="#bfcbd9"
                            active-text-color="#20a0ff"
                            unique-opened
                            router
                        >
                            <template v-for="item in items" :key="item.key">
                                <template v-if="item.subs">
                                    <el-sub-menu :index="item.index">
                                        <template #title>
                                            <label class="sidebar-index"> {{ item.title }} </label>
                                        </template>
                                        <el-menu-item v-for="subItem in item.subs" :key="subItem.key" :index="subItem.index">
                                            <label class="sidebar-subs"> {{ subItem.title }} </label>
                                        </el-menu-item>
                                    </el-sub-menu>
                                </template>
                                <template v-else>
                                    <el-menu-item :index="item.index">
                                        <template #title>
                                            <label style="color: blue;"> {{ item.title }} </label>
                                        </template>
                                    </el-menu-item>
                                </template>
                            </template>
                        </el-menu>        
                    </div>
                </el-aside>
                <el-main style="padding: 0px;">
                    <div class="content">
                        <router-view v-slot="props">
                            <keep-alive>
                                <component :is="props.Component"></component>
                            </keep-alive>
                        </router-view>
                    </div>
                </el-main>
            </el-container>
        </el-container>

        <div class="home-dialogs">
            <el-dialog title="用户信息" v-model="userInfoVisiblie" width="30%">
                <el-form label-width="140">
                    <el-form-item label="姓名：">
                        <label class="view-user-dialog-info"> {{ this.userInfo.name }} </label>
                    </el-form-item>
                    <el-form-item label="账号：">
                        <label class="view-user-dialog-info"> {{ this.userInfo.account }} </label>
                    </el-form-item>
                    <el-form-item label="性别：">
                        <label class="view-user-dialog-info"> {{ this.userInfo.sex }} </label>
                    </el-form-item>
                    <el-form-item label="入职时间：">
                        <label class="view-user-dialog-info"> {{ time2Date(this.userInfo.entryTime) }} </label>
                    </el-form-item>
                    <el-form-item label="电话号码：">
                        <label class="view-user-dialog-info"> {{ this.userInfo.phoneNumber }} </label>
                    </el-form-item>
                    <el-form-item label="银行名称：">
                        <label class="view-user-dialog-info"> {{ this.userInfo.bankName }} </label>
                    </el-form-item>
                    <el-form-item label="银行账号：">
                        <label class="view-user-dialog-info"> {{ this.userInfo.creditCardNumber }} </label>
                    </el-form-item>
                </el-form>

                <div class="dialog-buttons">
                    <button class="dialog-confirm-button" @click="userInfoVisiblie = false"> 确定 </button>
                </div>
            </el-dialog>

            <el-dialog title="修改密码" v-model="changePwdVisible" width="25%">
                <el-form-item label="原密码">
                    <el-input type="password" v-model="pwdOrigin" class="dialog-form-input"></el-input>
                </el-form-item>
                <div style="height: 10px;"></div>
                <el-form-item label="新密码">
                    <el-input type="password" v-model="pwdNew" class="dialog-form-input"></el-input>
                </el-form-item>

                <div class="dialog-buttons">
                    <button class="dialog-confirm-button" @click="handleChangePwd"> 确定 </button>
                    <button class="dialog-cancel-button" @click="pwdOrigin = ''; pwdNew = ''; changePwdVisible = false"> 取消 </button>
                </div>
            </el-dialog>
        </div>
    </div>
</template>

<script>
import { ElAside, ElContainer, ElMessage, ElSlider } from 'element-plus';
import { userName, getUserInfo, get, put, post } from '../axios/axiosConfig.js';

export default {
    data() {
        return {
            collapse: false,
            items: [
                {
                    index: '/e_home/frontReception',
                    key: '2',
                    title: '前台接待',
                    subs: [
                        // {
                        //     index: '/e_home/frontReception/settlement_bill',
                        //     key: '2.1',
                        //     title: '结算帐单',
                        // },
                        {
                            index: '/e_home/frontReception/resident_checkin',
                            key: '2.2',
                            title: '宾客入住',
                        },
                        {
                            index: '/e_home/frontReception/idle_query',
                            key: '2.3',
                            title: '空闲查询',
                        },
                        {
                            index: '/e_home/frontReception/guest_info',
                            key: '2.4',
                            title: '宾客信息',
                        },
                        // {
                        //     index: '/e_home/frontReception/process_order',
                        //     key: '2.5',
                        //     title: '处理订单',
                        // },
                    ]
                },
                // {
                //     index:'/e_home/material',
                //     key:'3',
                //     title:'物资管理',
                //     subs: [
                //         {
                //             index:'/e_home/material/purchasing_materials',
                //             key:'3.1',
                //             title:'采购物资',
                //         },
                //         {
                //             index:'/e_home/material/view_inventory',
                //             key:'3.2',
                //             title:'查看库存',
                //         },
                //         {
                //             index:'/e_home/material/maintain_records',
                //             key:'3.3',
                //             title:'维护记录',
                //         },
                //         {
                //             index:'/e_home/material/consumption_records',
                //             key:'3.5',
                //             title:'消耗记录',
                //         },
                //     ],
                // },
                {
                    index:'/e_home/personnel',
                    key:'4',
                    title:'人事管理',
                    subs: [
                        {
                            index:'/e_home/personnel/check_attendance',
                            key:'4.1',
                            title:'考勤管理',
                        },
                        {
                            index:'/e_home/personnel/employee_application',
                            key:'4.2',
                            title:'员工申请',
                        },
                        {
                            index:'/e_home/personnel/job_scheduling',
                            key:'4.3',
                            title:'岗位调度',
                        },
                        {
                            index:'/e_home/personnel/salary_manage',
                            key:'4.4',
                            title:'工资管理',
                        },
                    ],
                },
                {
                    index:'/e_home/restaurant',
                    key:'5',
                    title:'餐厅接待',
                    subs: [
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
                    ],
                },
            ],
            currentName: userName,
            userInfoVisiblie: false,
            changePwdVisible: false,
            pwdOrigin: '',
            pwdNew: '',
            userInfo: {},
        };
    },
    methods: {
        handleLogout() {
            this.$router.push('/login');
        },
        time2Date(time) {
            if (time)
                return time.split('T')[0];
            return '';
        },
        handleChangePwd() {
            if (this.pwdOrigin === '' || this.pwdNew === '') {
                ElMessage.error('输入不能为空');
                return;
            }
            if (this.pwdNew === this. pwdOrigin) {
                ElMessage.error('新密码和旧密码不能相同');
                return;
            }
            post(`/api/Employee/Login/${this.userInfo.account}/${this.pwdOrigin}`)
                .then(response => {
                    console.log(response);
                    if (response.status == true) {
                        console.log('密码对了')
                        put(`/api/Employee/UpdatePassword`, {
                            employeeId: this.userInfo.employeeId,
                            password: this.pwdNew,
                        })
                            .then(response => {
                                console.log(response);
                                if (response.status === false) {
                                    ElMessage.error('修改失败，请稍后重试');
                                    return;
                                }
                                ElMessage.success('修改成功');
                                this.changePwdVisible = false;
                                return;
                            })
                            .catch(error => {
                                ElMessage.error('修改失败，请稍后重试');
                                console.log(error);
                                return;
                            })
                    } else {
                        ElMessage.error('密码错误，请稍后重试');
                        return;
                    }
                    
                })
                .catch(error => {
                    console.log(error);
                    ElMessage.error('修改密码失败，请稍后重试');
                    return;
                });
        },
    },
    mounted() {
        this.userInfo = getUserInfo();
    },
    components: {
        ElContainer,
        ElSlider,
        ElAside
    },
    
};
</script>

<style scoped>
body {
    height: 100%;
    width: 100%;
}
.el-header {
    background-color: #324157;
}

.content-wrapper {
    height: 100vh;
}

.title {
    position: absolute;
    top: 10px;
    left: 30px;
    font-size: 30px;
    color: #ebeef5;
}
.logout {
    display: inline-block;
    position: absolute;
    top: 15px;
    right: 40px;
    cursor: pointer;
}
.change-pwd {
    display: inline-block;
    position: absolute;
    top: 15px;
    right: 140px;
    width: 80px;
    cursor: pointer;
}
.change-pwd::before {
    transition: all 0.85s cubic-bezier(0.68, -0.55, 0.265, 1.55);
    content: "";
    width: 50%;
    height: 100%;
    background: black;
    position: absolute;
    top: 0;
    left: 50%;
}
.change-pwd .btn-text {
    color: white;
    mix-blend-mode: difference;
    z-index: 1;
}
.change-pwd:hover::before {
    left: 0;
    width: 100%;
}
.logout::before {
    transition: all 0.85s cubic-bezier(0.68, -0.55, 0.265, 1.55);
    content: "";
    width: 50%;
    height: 100%;
    background: black;
    position: absolute;
    top: 0;
    left: 50%;
}
.logout .btn-text {
    color: white;
    mix-blend-mode: difference;
    z-index: 1;
}
.logout:hover::before {
    left: 0;
    width: 100%;
}
.avatar {
    display: flex;
    justify-content: center;
    margin-top: 20px;
    margin-bottom: 10px;
    cursor: pointer;
}
.avatar-img:hover {
    opacity: 50%;
}
.avatar-info {
    display: flex;
    justify-content: center;
}
.avatar-info-value {
    font-size: large;
    font-weight: lighter;
    color: white;
}
.content {
    padding: 0;
}
::v-deep .el-menu-item.is-active .sidebar-subs {
    color: rgb(255, 161, 161);
}
.sidebar-index {
    margin-left: 10px;
    font-size: large;
    color: #9ab7e3;
}
.sidebar-subs {
    margin-left: 10px;
    font-size: large;
    color: #97cba7;
}
::v-deep .el-dialog {
    background-image: linear-gradient(-45deg, #97cba7, #9ab7e3);
    border-radius: 30px;
}
::v-deep .el-dialog__title {
    display: flex;
    justify-content: center;
    font-size: xx-large;
    margin-top: 20px;
    color: #196883;
}
::v-deep .el-dialog__headerbtn {
    font-size: 50px;
    top: 20px;
    right: 20px;
}
::v-deep .el-dialog__headerbtn .el-dialog__close {
    color: #196883;
}
::v-deep .el-dialog__headerbtn .el-dialog__close:hover {
    color: white;
}
::v-deep .el-form-item {
    margin-top: 10px;
    margin-bottom: 10px;
}
::v-deep .el-form-item__label {
    font-size: large;
    font-weight: lighter;
    color: #196883;
}
.view-user-dialog-info {
    font-size: large;
    color: #324157;
}
.dialog-buttons {
    display: flex;
    justify-content: center;
    margin-top: 30px;
}
.dialog-confirm-button {
    margin-left: 10px;
    margin-right: 10px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 15px;
    padding-bottom: 15px;
    font-size: x-large;
    color: green;
    border: 2px solid green;
    border-radius: 10px;
    background-color: white;
}
.dialog-cancel-button {
    margin-left: 10px;
    margin-right: 10px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 15px;
    padding-bottom: 15px;
    font-size: x-large;
    color: red;
    border: 2px solid red;
    border-radius: 10px;
    background-color: white;
}
.dialog-cancel-button:hover {
    background-color: #ffadad;
}
.dialog-confirm-button:hover {
    background-color: #ccffc3;
    cursor: pointer;
}
.dialog-form-input {
    width: 90%;
}
::v-deep .el-input {
    --el-input-border-color: rgb(212, 212, 212);
    --el-input-hover-border-color: #324157;
    --el-input-focus-border-color: white;
    --el-input-placeholder-color: #757575;
}
::v-deep .el-input__wrapper {
    border: 2px solid #324157;
    border-radius: 15px;
    background-color: rgb(255, 255, 255, 0.5);
    height: 40px;
    padding-left: 20px;
}
::v-deep .el-input__wrapper.is-focus {
    border: 2px solid white;
}
::v-deep .el-input__inner {
    font-size: x-large;
    color: #196883;
}
</style>