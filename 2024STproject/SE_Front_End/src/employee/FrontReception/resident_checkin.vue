<template>
    <div class="resident-checkin">
        <img src="../../assets/reception.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 前台接待 </label>
            <label class="title-content"> 宾客入住 </label>
        </div>

        <div class="search-container">
            <label> 筛选方式： </label>
            <el-select v-model="searchType" class="search-select">
                <el-option label="身份证号" value="身份证号"></el-option>
                <el-option label="电话号码" value="电话号码"></el-option>
                <el-option label="姓名" value="姓名"></el-option>
            </el-select>

            <label> 关键字： </label>
            <el-input v-model="searchValue" :placeholder="defaultPlaceholder" class="search-input"></el-input>
            <el-button icon="refresh" class="search-button" @click="getRoomOrders"> 刷新 </el-button>
        </div>

        <div class="table-container">
            <label class="table-header-orderscount"> 当前订单数：{{ show_ordersCount }} </label>

            <el-table :data="filteredOrders" class="table" :header-cell-style="{
                'padding': '20px',
                'font-size': 'x-large',
                'font-weight': 'lighter',
                'color': '#ffffff',
                'background-color': '#196883'
            }">
                <el-table-column prop="orderId" label="订单ID" width="140">
                    <template #default="scope">
                        <label class="row-order-id"> {{ scope.row.orderId }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="orderGuestId" label="身份证号" width="250">
                    <template #default="scope">
                        <label class="row-order-guest-id"> {{ scope.row.orderGuestId }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="orderGuestPhone" label="电话号码">
                    <template #default="scope">
                        <label class="row-guest-phone"> {{ scope.row.orderGuestPhone }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="orderGuestName" label="姓名">
                    <template #default="scope">
                        <label class="row-guest-name"> {{ scope.row.orderGuestName }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="orderRoomType" label="预定房型">
                    <template #default="scope">
                        <label class="row-order-type"> {{ scope.row.orderRoomType }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="orderNum" label="预定人数" align="center" width="160">
                    <template #default="scope">
                        <label class="row-order-num"> {{ scope.row.orderNum }} </label>
                    </template>
                </el-table-column>
                <el-table-column label="操作" align="center">
                    <template #default="scope">
                        <el-button icon="suitcase" class="checkin-button" @click="checkin(scope.row); "> 入住 </el-button>
                    </template>
                </el-table-column>
                <!-- -->
            </el-table>
        </div>

        <!-- 点击入住后，输入住客信息的对话框 -->
        <el-dialog v-model="checkinDialogueVisible" title="办理已预定入住">
            <label class="dialog-room-info"> 当前分配到房号：{{ currentRoom.number }} </label>
            <label class="dialog-room-info"> 入住最大人数：{{ currentRoom.capacity }} </label>

            <!-- 显示入住和退房时间 -->
            <el-form label-width="160px">
                <el-form-item label="入住时间：">
                    <label class="dialog-form-info"> {{ trimInOutTime(currentRoom.inTime) }} </label>
                </el-form-item>
                <el-form-item label="退房时间：">
                    <label class="dialog-form-info"> {{ trimInOutTime(currentRoom.outTime) }} </label>
                </el-form-item>
                <el-form-item label="预定人数：">
                    <label class="dialog-form-info"> {{ currentRoom.orderNum }} </label>
                </el-form-item>
                <el-form-item label="实住人数：">
                    <el-input-number v-model.number="currentRoom.num" :min="1" :max="currentRoom.capacity"
                        @input="checkInput" class="dialog-input-number"></el-input-number>
                </el-form-item>
            </el-form>

            <p v-if="!ifInputPassed" class="dialog-error-message"> {{ errorMessage }} </p>

            <div class="dialog-buttons">
                <button :class="'dialog-submit-button-' + ifInputPassed.toString()" @click="submitGuestInfo"> 提交 </button>
                <button class="dialog-cancel-button" @click="checkinDialogueVisible = false"> 取消 </button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import { ElMessage, ElMessageBox } from 'element-plus';
import { get, put, post, del } from "../../axios/axiosConfig.js";

export default {
    data() {
        return {
            orders: [],               //从后端取得并处理好的数据
            show_ordersCount: 0,      //当前表头数据
            show_orderCustomers: [],  //当前表内数据

            searchType: "",           //搜索方式
            searchValue: "",          //搜索关键字
            checkinDialogueVisible: false,  //入住对话框是否显示
            currentOrderId: 0,  //当前编辑的订单id
            currentRoom: {      //当前编辑的房间信息
                id: 0,        //房间id
                orderNum: 0,  //预定人数
                num: 0,       //实住人数
                number: 0,    //房间号
                capacity: 0,  //容纳最大人数
                inTime: "",   //入住时间
                outTime: "",  //退房时间
                guestInfo: [  //要住进来的客人信息
                    { id: "", name: "", phone: "" },
                    { id: "", name: "", phone: "" },
                    { id: "", name: "", phone: "" },
                    { id: "", name: "", phone: "" },
                    { id: "", name: "", phone: "" }
                ]
            },
            ifInputPassed: true,  //输入是否正确
            errorMessage: ""       //输入错误信息
        }
    },
    computed: {
        //根据搜索栏信息筛选订单
        filteredOrders: function () {
            var fOrders = [];

            switch (this.searchType) {
                case "身份证号":
                    if (this.searchValue) {
                        fOrders = this.orders.filter(order => order.orderGuestId == null || order.orderGuestId.toString().includes(this.searchValue));
                        this.show_ordersCount = fOrders.length;
                        return fOrders;
                    }
                    this.show_ordersCount = this.orders.length;
                    return this.orders;
                    break;
                case "电话号码":
                    if (this.searchValue) {
                        fOrders = this.orders.filter(order => order.orderGuestPhone == null || order.orderGuestPhone.includes(this.searchValue));
                        this.show_ordersCount = fOrders.length;
                        return fOrders;
                    }
                    this.show_ordersCount = this.orders.length;
                    return this.orders;
                case "姓名":
                    if (this.searchValue) {
                        fOrders = this.orders.filter(order => order.orderGuestName == null || order.orderGuestName.includes(this.searchValue));
                        this.show_ordersCount = fOrders.length;
                        return fOrders;
                    }
                    this.show_ordersCount = this.orders.length;
                    return this.orders;
                case "":
                    this.show_ordersCount = this.orders.length;
                    return this.orders;
                    break;
                default:
                    break;
            }
        },
        //搜索方式的placeholder内容
        defaultPlaceholder: function () {
            if (this.searchType)
                return "请输入" + this.searchType;
            return "请选择搜索方式";
        }
    },
    methods: {
        //将身份证号转换为性别
        idToGender: function (id) {
            const byte = id.at(16);

            if (byte == "1" || byte == "3" || byte == "5" || byte == "7" || byte == "9")
                return "男";
            else
                return "女";
        },
        //取时间戳的日期部分
        trimInOutTime: function (timestamp) {
            return timestamp.slice(0, 10);
        },
        //检查字符串是否为纯数字
        checkNumber: function (string) {
            for (let i = 0; i < string.length; i++) {
                if (string[i] < '0' || string[i] > '9')
                    return false;
            }

            return true;
        },
        //检查输入是否正确
        checkInput: function () {
            this.ifInputPassed = true;

            for (let i = 0; i < this.currentRoom.num; i++) {
                if (this.currentRoom.guestInfo[i].id == "") {
                    this.errorMessage = "客人" + (i + 1).toString() + "的身份证号不能为空";
                    return;
                }
                else if (this.currentRoom.guestInfo[i].id.length != 18) {
                    this.errorMessage = "客人" + (i + 1).toString() + "的身份证号长度异常";
                    return;
                }
                else if (!this.checkNumber(this.currentRoom.guestInfo[i].id.slice(0, 17))
                    || (!this.checkNumber(this.currentRoom.guestInfo[i].id.slice(17, 18))
                        && this.currentRoom.guestInfo[i].id.at(17) != "x"
                        && this.currentRoom.guestInfo[i].id.at(17) != "X")) {
                    this.errorMessage = "客人" + (i + 1).toString() + "的身份证号格式异常";
                    return;
                }
                else if (this.currentRoom.guestInfo[i].name == "") {
                    this.errorMessage = "客人" + (i + 1).toString() + "的姓名不能为空";
                    return;
                }
                else if (this.currentRoom.guestInfo[i].phone == "") {
                    this.errorMessage = "客人" + (i + 1).toString() + "的电话号码不能为空";
                    return;
                }
                else if (!this.checkNumber(this.currentRoom.guestInfo[i].phone)) {
                    this.errorMessage = "客人" + (i + 1).toString() + "的电话号码必须为数字";
                    return;
                }
                else if (this.currentRoom.guestInfo[i].phone.length > 11) {
                    this.errorMessage = "客人" + (i + 1).toString() + "的电话号码长度异常";
                    return;
                }
            }

            this.ifInputPassed = true;
        },
        //入住准备工作
        checkin: async function (order) {
            this.currentRoom.inTime = order.orderInTime;
            this.currentRoom.outTime = order.orderOutTime;
            this.currentRoom.id = -1;
            this.currentRoom.orderNum = order.orderNum;
            this.currentOrderId = order.orderId;

            //根据所选订单的客人Id查入住表，看是否给该预定订单分配房间
            var that = this;
            await get("/api/Checkin/GetByCustomerIdAndInTime/" + order.orderGuestCustomerId.toString() + "/" + order.orderInTime,
                {
                    customerId: order.orderGuestCustomerId.toString(),
                    inTime: order.orderInTime,
                }).then(function (res) {
                    console.log(res)
                    if (res.status) {
                        console.log(88888)
                        console.log(res.data)
                        that.currentRoom.id = res.data.roomId;
                    }

                    else {
                        ElMessage.error("前台尚未给该订单分配房间");
                    }

                }, function (err) {
                    console.log(err);
                });

            setTimeout(async function () {
                if (that.currentRoom.id == -1)
                    return;
                else {
                    //若分配了房间，则拿到房间Id，去房间表中查该房间的房号，并显示对话框
                    await get("/api/Room/Get/" + that.currentRoom.id.toString()).then(function (res) {
                        that.currentRoom.number = res.data.roomNumber;
                        that.currentRoom.capacity = that.typeToInfo(res.data.typeId).roomCapacity;
                    }, function (err) {
                        console.log(err);
                    });

                    setTimeout(function () {
                        that.checkinDialogueVisible = true;
                    }, 100);
                }
            }, 100);
        },
        //提交客人信息并办理入住
        submitGuestInfo: async function () {
            if (!this.ifInputPassed)
                return;

            var that = this;
            var customerIds = [];

            //将订单状态设为已处理
            await put("/api/Roomorder/Update", {
                orderId: that.currentOrderId,
                orderStatus: "已处理"
            }).then(function (res) {
                console.log(res.data);
            }, function (err) {
                console.log(err);
            })

            ElMessage.success("入住办理成功！");
            that.checkinDialogueVisible = false;
            that.getRoomOrders();
        },
        //房间类型id转换为房间信息
        typeToInfo: function (roomTypeId) {
            switch (roomTypeId) {
                case 1:
                    return {
                        roomType: "双人间",
                        roomCapacity: 2
                    }
                    break;
                case 2:
                    return {
                        roomType: "标准间",
                        roomCapacity: 2
                    }
                    break;
                case 3:
                    return {
                        roomType: "豪华套房",
                        roomCapacity: 2
                    }
                case 4:
                    return {
                        roomType: "家庭房",
                        roomCapacity: 4
                    }
                case 5:
                    return {
                        roomType: "观景房",
                        roomCapacity: 3
                    }
                case 6:
                    return {
                        roomType: "行政套房",
                        roomCapacity: 2
                    }
                    break;
                default:
                    break;
            }
        },
        //房间名转换为房间类型id
        infoToType: function (roomType) {
            switch (roomType) {
                case "双人间":
                    return 1;
                    break;
                case "标准间":
                    return 2;
                    break;
                case "豪华套房":
                    return 3;
                    break;
                case "家庭房":
                    return 4;
                    break;
                case "观景房":
                    return 5;
                    break;
                case "行政套房":
                    return 6;
                    break;
                default:
                    break;
            }
        },
        //从后端取得订单数据并进行加工，以便表展示
        getRoomOrders: async function () {
            var that = this;

            await get("/api/Roomorder/GetAll").then(function (orderRes) {
                console.log("room orders get");

                const oldLength1 = that.orders.length;
                for (let i = 0; i < oldLength1; i++) {
                    that.orders.shift();
                }
                const oldLength2 = that.show_orderCustomers.length;
                for (let i = 0; i < oldLength2; i++) {
                    that.show_orderCustomers.shift();
                }

                for (let i = 0; i < orderRes.data.length; i++) {
                    if (orderRes.data[i].orderStatus == "未处理") {
                        that.show_orderCustomers.push(orderRes.data[i].customerId);
                        that.orders.push({
                            orderId: orderRes.data[i].orderId,
                            orderGuestId: "",
                            orderGuestCustomerId: 0,
                            orderGuestPhone: "",
                            orderGuestName: "",
                            orderRoomType: that.typeToInfo(orderRes.data[i].typeId).roomType,
                            orderNum: orderRes.data[i].customerNum,
                            orderInTime: orderRes.data[i].expectInTime,
                            orderOutTime: orderRes.data[i].expectOutTime
                        });
                    }
                }

                ElMessage.success("订单查询成功");
            }, function (orderErr) {
                console.log(orderErr);
            });

            setTimeout(async function () {
                for (let i = 0; i < that.show_orderCustomers.length; i++) {
                    await get("/api/Customer/Get/" + that.show_orderCustomers[i].toString()).then(function (customerRes) {
                        console.log("customer get");

                        that.orders[i].orderGuestCustomerId = customerRes.data.customerId;
                        that.orders[i].orderGuestId = customerRes.data.id;
                        that.orders[i].orderGuestPhone = customerRes.data.phone;
                        that.orders[i].orderGuestName = customerRes.data.name;
                    }, function (customerErr) {
                        console.log(customerErr);
                    });
                }

                console.log(that.orders);
            }, 10);
        }
    },
    async mounted() {
        await this.getRoomOrders();
    },
    beforeRouteEnter(to, from, next) {
  next(vm => {
    vm.getRoomOrders();
  });
}
}
</script>

<style scoped>
.resident-checkin {
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

.table-header-orderscount {
    font-size: xx-large;
    font-weight: lighter;
    color: #90de83;
}

.table {
    width: 100%;
    height: 470px;
    background: #196883;
    border-radius: 5px;
}

::v-deep .el-table td {
    background-color: #73bfc3;
    padding: 10px;
}

::v-deep .el-table td,
.building-top .el-table th.is-leaf {
    border-bottom: 10px solid #196883;
}

.row-order-id {
    font-size: large;
    color: #196883;
}

.row-order-guest-id {
    font-size: large;
    color: #196883;
}

.row-guest-phone {
    font-size: large;
    color: #196883;
}

.row-guest-name {
    font-size: large;
    color: #196883;
}

.row-order-num {
    font-size: large;
    color: #a76200;
}

.row-order-type {
    font-size: large;
    color: #a76200;
}

.checkin-button {
    margin-left: 10px;
    width: 60%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #196883;
}

.checkin-button:hover {
    background-color: #2aba98;
}

::v-deep .el-dialog {
    background-image: linear-gradient(-45deg, #97cba7, #9ab7e3);
    border-radius: 30px;
    width: 70%;
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

.dialog-room-info {
    display: flex;
    justify-content: center;
    font-size: x-large;
    font-weight: bold;
    color: #324157;
}

.dialog-form-info {
    display: flex;
    justify-content: center;
    font-size: x-large;
    color: #324157;
}

.dialog-input-number {
    width: 20%;
}

::v-deep .el-input-number__decrease {
    width: 20%;
    font-size: 20px;
    color: #196883;
    background: rgb(255, 255, 255, 0);
}

::v-deep .el-input-number__decrease:hover {
    color: white;
}

::v-deep .el-input-number__decrease.is-disabled {
    color: red;
}

::v-deep .el-input-number__increase {
    width: 20%;
    font-size: 20px;
    color: #196883;
    background: rgb(255, 255, 255, 0);
}

::v-deep .el-input-number__increase:hover {
    color: white;
}

::v-deep .el-input-number__increase.is-disabled {
    color: red;
}

::v-deep .el-form-item {
    margin-bottom: 40px;
}

::v-deep .el-form-item__label {
    font-size: x-large;
    font-weight: lighter;
    color: #196883;
}

.dialog-input {
    margin-right: 20px;
    margin-top: 10px;
    width: 30%;
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
</style>