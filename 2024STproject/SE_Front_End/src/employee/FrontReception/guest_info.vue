<template>
    <div class="guest-info">
        <img src="../../assets/reception.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 前台接待 </label>
            <label class="title-content"> 宾客信息 </label>
        </div>
        
        <div class="search-container">
            <label> 身份证号： </label>
            <el-input v-model="searchInfo.id" placeholder="请输入" class="search-input"></el-input>

            <label> 姓名： </label>
            <span style="margin-left: 4%;"></span>
            <el-input v-model="searchInfo.name" placeholder="请输入" class="search-input"></el-input>
            <br>
            <br>

            <label> 性别： </label>
            <span style="margin-left: 4%;"></span>
            <el-select v-model="searchInfo.gender" placeholder="请选择" class="search-select">
                <el-option label="不限" value=""></el-option>
                <el-option label="男" value="男"></el-option>
                <el-option label="女" value="女"></el-option>
            </el-select>
            <span style="margin-left: 10%;"></span>

            <label> 电话号码： </label>
            <el-input v-model="searchInfo.phone" placeholder="请输入" class="search-input"></el-input>
            <br>

            <label> 信用等级： </label>
            <el-select v-model="searchInfo.creditGrade" placeholder="请选择" class="search-select">
                <el-option label="不限" value=""></el-option>
                <el-option label="优秀" value="优秀"></el-option>
                <el-option label="良好" value="良好"></el-option>
                <el-option label="可接受" value="可接受"></el-option>
                <el-option label="较差" value="较差"></el-option>
            </el-select>
            <span style="margin-left: 10%;"></span>

            <label> 会员等级： </label>
            <el-select v-model="searchInfo.memberGrade" placeholder="请选择" class="search-select">
                <el-option label="不限" value=""></el-option>
                <el-option label="普通" value="普通"></el-option>
                <el-option label="银卡" value="银卡"></el-option>
                <el-option label="金卡" value="金卡"></el-option>
                <el-option label="白金" value="白金"></el-option>
            </el-select>
            <span style="margin-left: 8%;"></span>

            <el-button icon="refresh" class="search-button" @click="getGuests"> 刷新 </el-button>
        </div>

        <div class="table-container">
            <label class="table-header-guestscount"> 符合条件宾客数：{{ show_guestsCount }} </label>

            <el-table :data="filteredGuests" class="table" :header-cell-style="{
                'padding': '20px',
                'font-size': 'x-large',
                'font-weight': 'lighter',
                'color': '#ffffff',
                'background-color': '#196883'
            }">
                <el-table-column prop="id" label="身份证号" width="240">
                    <template #default="scope">
                        <label class="row-customer-id"> {{ scope.row.id }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="name" label="姓名" width="140">
                    <template #default="scope">
                        <label class="row-customer-name"> {{ scope.row.name }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="gender" label="性别" width="140">
                    <template #default="scope">
                        <label class="row-customer-gender"> {{ scope.row.gender }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="phone" label="电话号码">
                    <template #default="scope">
                        <label class="row-customer-phone"> {{ scope.row.phone }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="creditGrade" label="信用等级">
                    <template #default="scope">
                        <button :class="'credit-grade-' + translateCreditGradeCTE(scope.row.creditGrade)"> {{ scope.row.creditGrade }} </button>
                    </template>
                </el-table-column>
                <el-table-column prop="memberGrade" label="会员等级">
                    <template #default="scope">
                        <button :class="'member-grade-' + translateMemberGradeCTE(scope.row.memberGrade)"> {{ scope.row.memberGrade }} </button>
                    </template>
                </el-table-column>
                <el-table-column label="操作" align="center">
                    <template #default="scope">
                        <el-button icon="edit" class="change-button" @click="guestCopyLoad(scope.row); checkInput(); changeGuestDialogueVisible = true;"> 更改 </el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>

        <!-- 点击更改后的对话框 -->
        <el-dialog v-model="changeGuestDialogueVisible" title="客人信息更改">
            <el-form label-width="140px">
                <el-form-item label="姓名：">
                    <el-input v-model="currentGuest.name" class="dialog-form-input" @input="checkInput"></el-input>
                </el-form-item>
                <el-form-item label="性别：">
                    <el-select v-model="currentGuest.gender" class="dialog-form-select" @input="checkInput">
                        <el-option label="男" value="男"></el-option>
                        <el-option label="女" value="女"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="电话号码：">
                    <el-input v-model="currentGuest.phone" class="dialog-form-input" @input="checkInput"></el-input>
                </el-form-item>
                <el-form-item label="信用等级：">
                    <el-select v-model="currentGuest.creditGrade" class="dialog-form-select" @change="checkInput">
                        <el-option label="优秀" value="优秀"></el-option>
                        <el-option label="良好" value="良好"></el-option>
                        <el-option label="可接受" value="可接受"></el-option>
                        <el-option label="较差" value="较差"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="会员等级：">
                    <el-select v-model="currentGuest.memberGrade" class="dialog-form-select" @change="checkInput">
                        <el-option label="普通" value="普通"></el-option>
                        <el-option label="银卡" value="银卡"></el-option>
                        <el-option label="金卡" value="金卡"></el-option>
                        <el-option label="白金" value="白金"></el-option>
                    </el-select>
                </el-form-item>
            </el-form>

            <label v-if="!ifInputPassed" class="dialog-error-message"> {{ errorMessage }} </label>

            <div class="dialog-buttons">
                <button :class="'dialog-submit-button-' + ifInputPassed.toString()" @click="updateGuest"> 提交 </button>
                <button class="dialog-cancel-button" @click="changeGuestDialogueVisible = false"> 取消 </button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import { ElMessage, ElMessageBox } from 'element-plus';
import { get, put, post, del } from "../../axios/axiosConfig.js";

    export default {
        data () {
            return {
                guests: [],           //从后端取得并处理好的数据
                show_guestsCount: 0,  //当前表头数据

                searchInfo: {  //筛选信息
                    id: "",           //身份证号
                    name: "",         //姓名
                    gender: "",       //性别
                    phone: "",        //电话号码
                    creditGrade: "",  //信用等级
                    memberGrade: ""   //会员等级
                },

                changeGuestDialogueVisible: false,  //修改宾客信息对话框是否显示
                currentGuest: {  //当前编辑的客人信息
                    id: "",           //身份证号
                    customerId: 0,       //客人id
                    name: "",         //姓名
                    gender: "",       //性别
                    phone: "",        //电话号码
                    creditGrade: "",  //信用等级
                    memberGrade: ""   //会员等级
                },
                
                ifInputPassed: false,  //输入是否正确
                errorMessage: ""       //输入错误信息
            }
        },
        computed: {
            //根据搜索栏信息筛选客人
            filteredGuests: function() {
                let fGuests = [];
                this.guests.forEach(item => {
                    fGuests.push(item);
                });
                
                fGuests = fGuests.filter(guest => guest.id == null || guest.id.includes(this.searchInfo.id));
                fGuests = fGuests.filter(guest => guest.name == null || guest.name.includes(this.searchInfo.name));
                fGuests = fGuests.filter(guest => guest.gender == null || guest.gender.includes(this.searchInfo.gender));
                fGuests = fGuests.filter(guest => guest.phone == null || guest.phone.includes(this.searchInfo.phone));
                fGuests = fGuests.filter(guest => guest.creditGrade == null || guest.creditGrade.includes(this.searchInfo.creditGrade));
                fGuests = fGuests.filter(guest => guest.memberGrade == null || guest.memberGrade.includes(this.searchInfo.memberGrade));
                
                this.show_guestsCount = fGuests.length;
                return fGuests;
            }
        },
        methods: {
            //检查字符串是否为纯数字
            checkNumber: function(string) {
                for (let i = 0; i < string.length; i++) {
                    if (string[i] < '0' || string[i] > '9')
                        return false;
                }

                return true;
            },
            //检查输入是否正确
            checkInput: function() {
                this.ifInputPassed = false;

                if (this.currentGuest.name == "") {
                    this.errorMessage = "客人姓名不能为空";
                    return;
                }
                else if (this.currentGuest.phone == "") {
                    this.errorMessage = "客人的电话号码不能为空";
                    return;
                }
                else if (!this.checkNumber(this.currentGuest.phone)) {
                    this.errorMessage = "客人的电话号码必须为数字";
                    return;
                }
                else if (this.currentGuest.phone.length > 11) {
                    this.errorMessage = "客人的电话号码长度异常"
                    return;
                }

                this.ifInputPassed = true;
            },
            //信用等级翻译：汉译英
            translateCreditGradeCTE: function(grade) {
                switch (grade) {
                    case "优秀":
                        return "excellent";
                        break;
                    case "良好":
                        return "good";
                        break;
                    case "可接受":
                        return "medium";
                        break;
                    case "较差":
                        return "bad";
                        break;
                    default:
                        break;
                }
            },
            //信用等级翻译：数译汉
            translateCreditGradeNTC: function(grade) {
                switch (grade) {
                    case "5":
                        return "优秀";
                        break;
                    case "4":
                        return "良好";
                        break;
                    case "3":
                        return "可接受";
                        break;
                    case "2":
                        return "较差";
                        break;
                    default:
                        break;
                }
            },
            //信用等级翻译：汉译数
            translateCreditGradeCTN: function(grade) {
                switch (grade) {
                    case "优秀":
                        return "5";
                        break;
                    case "良好":
                        return "4";
                        break;
                    case "可接受":
                        return "3";
                        break;
                    case "较差":
                        return "2";
                        break;
                    default:
                        break;
                }
            },
            //会员等级翻译：汉译英
            translateMemberGradeCTE: function(grade) {
                switch (grade) {
                    case "普通":
                        return "common";
                        break;
                    case "银卡":
                        return "silver";
                        break;
                    case "金卡":
                        return "gold";
                        break;
                    case "白金":
                        return "platinum"
                        break;
                    default:
                        break;
                }
            },
            //会员等级翻译：数译汉
            translateMemberGradeNTC: function(grade) {
                switch (grade) {
                    case "0":
                        return "普通";
                        break;
                    case "1":
                        return "银卡";
                        break;
                    case "2":
                        return "金卡";
                        break;
                    case "3":
                        return "白金";
                        break;
                    default:
                        break;
                }
            },
            //会员等级翻译：汉译数
            translateMemberGradeCTN: function(grade) {
                switch (grade) {
                    case "普通":
                        return "0";
                        break;
                    case "银卡":
                        return "1";
                        break;
                    case "金卡":
                        return "2";
                        break;
                    case "白金":
                        return "3";
                        break;
                    default:
                        break;
                }
            },
            //根据客人id找到guests数组中对应项的下标
            guestFind: function(customerId) {
                for (let index = 0; index < this.guests.length; index++) {
                    if (this.guests[index].customerId == customerId)
                        return index;
                }
            },
            //编辑客人前将信息装载到currentGuest中
            guestCopyLoad: function(guest) {
                this.currentGuest.id = guest.id;
                this.currentGuest.customerId = guest.customerId;
                this.currentGuest.name = guest.name;
                this.currentGuest.gender = guest.gender;
                this.currentGuest.phone = guest.phone;
                this.currentGuest.creditGrade = guest.creditGrade;
                this.currentGuest.memberGrade = guest.memberGrade;
            },
            //编辑客人后将信息存回到guests数组中
            guestCopyStore: function(index) {
                this.guests[index].name = this.currentGuest.name;
                this.guests[index].gender = this.currentGuest.gender;
                this.guests[index].phone = this.currentGuest.phone;
                this.guests[index].creditGrade = this.currentGuest.creditGrade;
                this.guests[index].memberGrade = this.currentGuest.memberGrade;
            },
            //提交客人信息并更新
            updateGuest: async function() {
                if (!this.ifInputPassed)
                    return;

                var that = this;

                await put("/api/Customer/Update", {
                    customerId: this.currentGuest.customerId,
                    name: this.currentGuest.name,
                    gender: this.currentGuest.gender,
                    phone: this.currentGuest.phone,
                    creditGrade: this.translateCreditGradeCTN(this.currentGuest.creditGrade),
                    memberGrade: this.translateMemberGradeCTN(this.currentGuest.memberGrade)
                }).then(function(res) {
                    ElMessage.info("客人信息已修改");
                    that.guestCopyStore(that.guestFind(that.currentGuest.customerId));
                    that.changeGuestDialogueVisible = false;
                    console.log("客人信息修改成功")
                }, function(err) {
                    ElMessage.error(err);
                });
            },
            //从后端取得订单数据并进行加工，以便表展示
            getGuests: async function() {
                var that = this;

                await get("/api/Customer/GetAll").then(function(res) {
                    console.log(111)
                    const oldLength = that.guests.length;
                    for (let i = 0; i < oldLength; i++) {
                        that.guests.shift();
                    }
                    console.log(222)
                    for (let i = 0; i < res.data.length; i++) {
                        that.guests.push({
                            customerId: res.data[i].customerId,
                            id: res.data[i].id,
                            name: res.data[i].name,
                            gender: res.data[i].gender,
                            phone: res.data[i].phone,
                            creditGrade: that.translateCreditGradeNTC(res.data[i].creditGrade),
                            memberGrade: that.translateMemberGradeNTC(res.data[i].memberGrade)
                        });
                    }

                    ElMessage.success("宾客信息查询成功！")
                    console.log("宾客信息查询成功！")
                    console.log(res)
                    //console.log(that.guests)
                }, function(err) {
                    ElMessage.error("宾客信息查询失败！");
                });
            }
        },
        async mounted() {
            await this.getGuests();
        }
    }
</script>

<style scoped>
.guest-info {
    padding: 20px;
    background-image: linear-gradient(-45deg, #97cba7, #9ab7e3);
    height: 95vh;
}
.function-icon {
    position: absolute;
    opacity: 50%;
    width: 13.4%;
    height: 16.6%;
}
.title {
    display: flex;
    justify-content: center;
    margin-top: 20px;
    margin-bottom: 30px;
}
.title-function {
    margin-left: 10px;
    margin-right: 10px;
    font-size: xx-large;
    color: #196883;
    background-color: rgb(255, 255, 255, 0.7);
    border-radius: 10px;
    padding: 10px;
}
.title-content {
    font-size: xx-large;
    color: #196883;
    padding: 10px;
}
.search-container {
	margin-bottom: 20px;
    margin-left: 11%;
}
.search-container label {
    margin-left: 50px;
    margin-right: 5px;
    font-size: x-large;
    color: #196883;
}
.search-select {
	width: 16%;
}
.search-input {
	width: 25%;
    margin-right: 10px;
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
::v-deep .el-select {
    --el-select-border-color-hover: #324157;
    --el-select-input-focus-border-color: white;
}
::v-deep .el-select .el-select__caret {
    font-size: 30px;
    color: #324157;
}
.search-button {
    margin-left: 40px;
    width: 90px;
    height: 90px;
    border: 2px solid white;
    border-radius: 90px;
    background-color: #196883;
}
.search-button:hover {
    background-color: #2aba98;
    box-shadow: 0 1em 1em -1em #0e3d26;
}
::v-deep .el-button {
    color: white;
    font-size: large;
    font-weight: lighter;
}
.table-container {
    background-color: #196883;
    padding: 20px;
    border-radius: 15px;
}
.table-header-guestscount {
    font-size: xx-large;
    font-weight: lighter;
    color: #90de83;
}
.table {
    width: 100%;
    height: 400px;
    background: #196883;
    border-radius: 5px;
}
::v-deep .el-table td {
    background-color: #73bfc3;
    padding: 10px;
}
::v-deep .el-table td,.building-top .el-table th.is-leaf {
    border-bottom: 10px solid #196883;
}
.row-customer-id {
    font-size: large;
    color: #196883;
}
.row-customer-name {
    font-size: large;
    color: #196883;
}
.row-customer-gender {
    font-size: large;
    color: #196883;
}
.row-customer-phone {
    font-size: large;
    color: #196883;
}
.credit-grade-excellent {
    color: green;
    border-style: solid;
    border-color: green;
    border-width: 2px;
    border-radius: 2px;
    background-color: #ccffc3;
    font-size: large;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.credit-grade-good {
    color: yellowgreen;
    border-style: solid;
    border-color: yellowgreen;
    border-width: 2px;
    border-radius: 2px;
    background-color: #f2ffaa;
    font-size: large;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.credit-grade-medium {
    color: orange;
    border-style: solid;
    border-color: orange;
    border-width: 2px;
    border-radius: 2px;
    background-color: #ffe6a7;
    font-size: large;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.credit-grade-bad {
    color: red;
    border-style: solid;
    border-color: red;
    border-width: 2px;
    border-radius: 2px;
    background-color: #ffadad;
    font-size: large;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.member-grade-common {
    color: black;
    border-style: solid;
    border-color: black;
    border-width: 2px;
    border-radius: 2px;
    background-color: #ffffff;
    font-size: large;
    margin-right: 20px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.member-grade-silver {
    color: #586f73;
    border-style: solid;
    border-color: #586f73;
    border-width: 2px;
    border-radius: 2px;
    background-color: #b2c4c7;
    font-size: large;
    margin-right: 20px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.member-grade-gold {
    color: #d49a15;
    border-style: solid;
    border-color: #d49a15;
    border-width: 2px;
    border-radius: 2px;
    background-color: #f0f493;
    font-size: large;
    margin-right: 20px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.member-grade-platinum {
    color: #235471;
    border-style: solid;
    border-color: #235471;
    border-width: 2px;
    border-radius: 2px;
    background-color: #a8dfff;
    font-size: large;
    margin-right: 20px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.change-button {
    margin-left: 10px;
    width: 75%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #196883;
}
.change-button:hover {
    background-color: #2aba98;
}
::v-deep .el-dialog {
    background-image: linear-gradient(-45deg, #97cba7, #9ab7e3);
    border-radius: 30px;
    width: 30%;
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
    margin-bottom: 30px;
}
::v-deep .el-form-item__label {
    font-size: x-large;
    font-weight: lighter;
    color: #196883;
}
.dialog-form-input {
    width: 75%;
}
.dialog-form-select {
    width: 75%;
}
.dialog-error-message {
    height: 10px;
    display: flex;
    justify-content: center;
    font-size: medium;
    color: rgb(159, 86, 86);
}
.dialog-buttons {
    display: flex;
    justify-content: center;
    margin-top: 30px;
}
.dialog-submit-button-true {
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
.dialog-submit-button-true:hover {
    background-color: #ccffc3;
}
.dialog-submit-button-false {
    margin-left: 10px;
    margin-right: 10px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 15px;
    padding-bottom: 15px;
    font-size: x-large;
    color: gray;
    border: 2px solid gray;
    border-radius: 10px;
    background-color: rgb(211, 211, 211);
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
::v-deep .el-dialog p {
    display: flex;
    justify-content: center;
    font-size: x-large;
    font-weight: bold;
    color: #324157;
}
::v-deep .el-form-item {
    margin-top: 20px;
    margin-bottom: 20px;
}
::v-deep .el-form-item__label {
    font-size: large;
    font-weight: lighter;
    color: #196883;
}
</style>