<template>
    <div class="process-order">
        <img src="../../assets/reception.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 前台接待 </label>
            <label class="title-content"> 订单处理 </label>
        </div>

        <div class="search-container">
            <label> 筛选方式： </label>
            <el-select v-model="searchType" class="search-select">
                <el-option label="身份证号" value="身份证号"></el-option>
                <el-option label="电话号码" value="电话号码"></el-option>
                <el-option label="姓名" value="姓名"></el-option>
                <el-option label="订单状态" value="订单状态"></el-option>
            </el-select>

            <label> 关键字： </label>
            <el-input v-if="searchType != '订单状态'" v-model="searchValue" :placeholder="defaultPlaceholder" class="search-input"></el-input>
            <el-select v-else v-model="searchValue" placeholder="请选择订单状态" class="search-else-select">
                <el-option label="未处理" value="未处理"></el-option>
                <el-option label="已处理" value="已处理"></el-option>
                <el-option label="已取消" value="已取消"></el-option>
            </el-select>
            <el-button icon="refresh" class="search-button" @click="getRoomOrders"> 刷新 </el-button>
        </div>

        <div class="table-container">
            <label class="table-header-orderscount"> 当前订单总数：{{ show_ordersCount }} </label>

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
                <el-table-column prop="orderGuestId" label="身份证号">
                    <template #default="scope">
                        <label class="row-customer-id"> {{ scope.row.orderGuestId }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="orderGuestPhone" label="电话号码">
                    <template #default="scope">
                        <label class="row-customer-phone"> {{ scope.row.orderGuestPhone }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="orderGuestName" label="姓名" width="140">
                    <template #default="scope">
                        <label class="row-customer-name"> {{ scope.row.orderGuestName }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="orderRoomType" label="预定房型" width="160">
                    <template #default="scope">
                        <label class="row-order-type"> {{ scope.row.orderRoomType }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="orderStatus" label="订单状态">
                    <template #default="scope">
                        <button :class="'order-status-' + translateOrderStatus(scope.row.orderStatus)"> {{ scope.row.orderStatus }} </button>
                        <el-button icon="document-delete" class="checkin-button" v-show="scope.row.orderStatus == '未处理'" @click="cancelingOrder = scope.row; cancelDialogueVisible = true;"> 取消 </el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>

        <!-- 点击取消后，再次确认的对话框 -->
        <el-dialog v-model="cancelDialogueVisible" title="订单取消">
            <p> 请确定选中订单信息： </p>

            <el-form label-width="140px">
                <el-form-item label="订单ID：">
                    <label class="dialog-info"> {{ cancelingOrder.orderId }} </label>
                </el-form-item>
                <el-form-item label="身份证号：">
                    <label class="dialog-info"> {{ cancelingOrder.orderGuestId }} </label>
                </el-form-item>
                <el-form-item label="电话号码：">
                    <label class="dialog-info"> {{ cancelingOrder.orderGuestPhone }} </label>
                </el-form-item>
                <el-form-item label="姓名：">
                    <label class="dialog-info"> {{ cancelingOrder.orderGuestName }} </label>
                </el-form-item>
                <el-form-item label="预定房型：">
                    <label class="dialog-info"> {{ cancelingOrder.orderRoomType }} </label>
                </el-form-item>
                <el-form-item label="订单状态：">
                    <label class="dialog-info"> {{ cancelingOrder.orderStatus }} </label>
                </el-form-item>
            </el-form>
            
            <p> 确保客人要取消订单，继续操作？ </p>

            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="cancelOrder"> 确定 </button>
                <button class="dialog-cancel-button" @click="cancelDialogueVisible = false"> 取消 </button>
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
                orders: [],               //从后端取得并处理好的数据
                show_ordersCount: 0,      //当前表头数据
                show_orderCustomers: [],  //当前表内数据
                
                searchType: "",   //搜索方式
                searchValue: "",  //搜索关键字

                cancelDialogueVisible: false,  //取消对话框是否显示
                cancelingOrder: {  //当前编辑的订单信息
                    orderId: 0,           //订单id
                    orderGuestId: 0,      //下单客人id
                    orderGuestPhone: "",  //下单客人电话号码
                    orderGuestName: "",   //下单客人姓名
                    orderRoomType: "",    //预定房型
                    orderStatus: ""       //当前订单状态
                }
            }
        },
        computed: {
            //根据搜索栏信息筛选订单
            filteredOrders: function() {
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
                    case "订单状态":
                        if (this.searchValue) {
                            fOrders = this.orders.filter(order => order.orderStatus == null || order.orderStatus.includes(this.searchValue));
                            this.show_ordersCount = fOrders.length;
                            return fOrders;
                        }
                        this.show_ordersCount = this.orders.length;
                        return this.orders;
                    case "":
                        this.show_ordersCount = this.orders.length;
                        return this.orders;
                    default:
                        break;
                }
            },
            //搜索方式的placeholder内容
            defaultPlaceholder: function() {
                if (this.searchType)
                    return "请输入" + this.searchType;
                return "请选择搜索方式";
            }
        },
        methods: {
            //翻译订单状态
            translateOrderStatus: function(status) {
                switch (status) {
                    case "已处理":
                        return "processed";
                        break;
                    case "未处理":
                        return "created";
                        break;
                    case "已取消":
                        return "canceled";
                        break;
                    default:
                        break;
                }
            },
            //取消订单
            cancelOrder: async function() {
                var that = this;

                await put("/api/Roomorder/Update", {
                    orderId: this.cancelingOrder.orderId,
                    orderStatus: "已取消"
                }).then(async function(res) {
                    ElMessage.warning("订单已取消");
                    that.cancelingOrder.orderStatus = "已取消";
                    that.cancelDialogueVisible = false;
                    that.getRoomOrders();
                }, function(err) {
                    ElMessage.error("订单取消失败！");
                });
            },
            //房间类型id转换为房间信息
            typeToInfo: function(roomTypeId) {
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
            infoToType: function(roomType) {
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
            getRoomOrders: async function() {
                var that = this;

                await get("/api/Roomorder/GetAll").then(function(orderRes) {
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
                        that.show_orderCustomers.push(orderRes.data[i].customerId);
                        that.orders.push({
                            orderId: orderRes.data[i].orderId,
                            orderGuestId: "",
                            orderGuestPhone: "",
                            orderGuestName: "",
                            orderRoomType: that.typeToInfo(orderRes.data[i].typeId).roomType,
                            orderStatus: orderRes.data[i].orderStatus
                        });
                    }

                    if (that.show_orderCustomers.length)
                        ElMessage.success("订单查询成功");
                }, function(orderErr) {
                    ElMessage.error(orderErr);
                });

                setTimeout(async function() {
                    for (let i = 0; i < that.show_orderCustomers.length; i++) {
                        await get("/api/Customer/Get/" + that.show_orderCustomers[i].toString()).then(function(customerRes) {
                            console.log("customer get");

                            that.orders[i].orderGuestId = customerRes.data.id;
                            that.orders[i].orderGuestPhone = customerRes.data.phone;
                            that.orders[i].orderGuestName = customerRes.data.name;
                        }, function(customerErr) {
                            console.log(customerErr);
                        });
                    }
                }, 10);
            }
        },
        async mounted() {
            await this.getRoomOrders();
        }
    }
</script>

<style scoped>
.process-order {
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
.search-else-select {
    width: 25%;
    margin-right: 10px;
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
    border: 2px solid #ffffff;
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
::v-deep .el-table td,.building-top .el-table th.is-leaf {
    border-bottom: 10px solid #196883;
}
.row-order-id {
    font-size: large;
    color: #196883;
}
.row-customer-id {
    font-size: large;
    color: #196883;
}
.row-customer-phone {
    font-size: large;
    color: #196883;
}
.row-customer-name {
    font-size: large;
    color: #196883;
}
.row-order-type {
    font-size: large;
    color: #a76200;
}
.order-status-processed {
    color: green;
    border-style: solid;
    border-color: green;
    border-width: 2px;
    border-radius: 2px;
    background-color: #ccffc3;
    font-size: large;
    margin-right: 20px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.order-status-created {
    color: #20a0ff;
    border-style: solid;
    border-color: #20a0ff;
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
.order-status-canceled {
    color: #6d6d6d;
    border-style: solid;
    border-color: #6d6d6d;
    border-width: 2px;
    border-radius: 2px;
    background-color: #c1cbd9;
    font-size: large;
    margin-right: 20px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 10px;
    padding-bottom: 10px;
}
.checkin-button {
    margin-left: 10px;
    width: 37%;
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
.dialog-info {
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
.dialog-confirm-button:hover {
    background-color: #ccffc3;
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