<template>
    <div class="idle-query">
        <img src="../../assets/reception.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 前台接待 </label>
            <label class="title-content"> 空闲房间查询 </label>
        </div>

        <div class="search-container">
            <label> 筛选方式： </label>
            <el-select v-model="searchType" class="search-select">
                <el-option label="房间号" value="房间号"></el-option>
                <el-option label="房型" value="房型"></el-option>
                <el-option label="房间电话" value="房间电话"></el-option>
            </el-select>

            <label> 关键字： </label>
            <el-input v-model="searchValue" :placeholder="defaultPlaceholder" class="search-input"></el-input>
            <el-button icon="refresh" class="search-button" @click="getEmptyRooms"> 刷新 </el-button>
        </div>

        <div class="table-container">
            <label class="table-header-roomscount"> 当前空房总数：{{ show_roomsCount }} </label>

            <el-table :data="filteredEmptyRooms" class="table" :header-cell-style="{
                'padding': '20px',
                'font-size': 'x-large',
                'font-weight': 'lighter',
                'color': '#ffffff',
                'background-color': '#196883'
            }">
                <el-table-column prop="roomNumber" label="房间号">
                    <template #default="scope">
                        <label class="row-room-number"> {{ scope.row.roomNumber }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="roomType" label="房型">
                    <template #default="scope">
                        <label class="row-room-type"> {{ scope.row.roomType }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="roomPrice" label="价格">
                    <template #default="scope">
                        <label class="row-room-price"> {{ scope.row.roomPrice }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="roomCapacity" label="容纳人数">
                    <template #default="scope">
                        <label class="row-room-capacity"> {{ scope.row.roomCapacity }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="roomPhone" label="房间电话">
                    <template #default="scope">
                        <label class="row-room-phone"> {{ scope.row.roomPhone }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="roomStatus" label="房间状态" width="300">
                    <template #default="scope">
                        <button class="row-room-status-empty"> {{ scope.row.roomStatus }} </button>
                        <el-button icon="suitcase" class="checkin-button" @click="checkin(scope.row)">预定 </el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>

        <!-- 点击入住后，输入住客信息的对话框 -->
        <el-dialog v-model="checkinDialogueVisible" title="办理无预定入住">
            <label class="dialog-room-info"> 房号：{{ currentRoom.number }} </label>
            <label class="dialog-room-info"> 入住最大人数：{{ currentRoom.capacity }} </label>

            <el-form label-width="160px">
                <el-form-item label="入住时间：">
                    <el-date-picker v-model="currentRoom.inTime" type="date" value-format="YYYY-MM-DD" placeholder="选择日期"
                        @change="checkInput" class="dialog-picker"></el-date-picker>
                </el-form-item>
                <el-form-item label="退房时间：">
                    <el-date-picker v-model="currentRoom.outTime" type="date" value-format="YYYY-MM-DD" placeholder="选择日期"
                        @change="checkInput" class="dialog-picker"></el-date-picker>
                </el-form-item>
                <el-form-item label="入住人数：">
                    <el-input-number v-model.number="currentRoom.num" :min="1" :max="currentRoom.capacity"
                        @input="checkInput" class="dialog-input-number"></el-input-number>
                </el-form-item>

                <!-- 客人信息输入，根据房型和入住人数决定 -->
                <el-form-item v-for="index of currentRoom.num" :label="'住客' + index.toString() + '信息：'">
                    <el-input v-model="currentRoom.guestInfo[index - 1].id" :placeholder="'住客' + index.toString() + '身份证号'"
                        @input="checkInput" class="dialog-input"></el-input>
                    <el-input v-model="currentRoom.guestInfo[index - 1].name" :placeholder="'住客' + index.toString() + '姓名'"
                        @input="checkInput" class="dialog-input"></el-input>
                    <el-input v-model="currentRoom.guestInfo[index - 1].phone"
                        :placeholder="'住客' + index.toString() + '电话号码'" @input="checkInput" class="dialog-input"></el-input>
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
import { userId } from "../../axios/axiosConfig.js";

export default {
    data() {
        return {
            emptyRooms: [],      //从后端取得并处理好的数据
            show_roomsCount: 0,  //当前表头数据

            searchType: "",      //搜索方式
            searchValue: "",     //搜索关键字

            checkinDialogueVisible: false,  //入住对话框是否显示
            currentRoom: {    //当前编辑的房间信息
                id: 0,          //房间id
                num: 0,         //实住人数
                inTime: "",     //入住时间
                outTime: "",    //退房时间
                number: 0,      //房间号
                type: "",       //房型
                price: 0,       //价格
                capacity: 0,    //容纳最大人数
                status: "",     //状态
                guestInfo: []   //要住进来的客人信息
            },
            ifInputPassed: false,  //输入是否正确
            errorMessage: ""       //输入错误信息
        }
    },
    computed: {
        //根据搜索栏信息筛选空房
        filteredEmptyRooms: function () {
            var fEmptyRooms = [];

            switch (this.searchType) {
                case "房间号":
                    if (this.searchValue) {
                        fEmptyRooms = this.emptyRooms.filter(emptyRoom => emptyRoom.roomNumber == null || emptyRoom.roomNumber.toString().includes(this.searchValue));
                        this.show_roomsCount = fEmptyRooms.length;
                        return fEmptyRooms;
                    }
                    this.show_roomsCount = this.emptyRooms.length;
                    return this.emptyRooms;
                    break;
                case "房型":
                    if (this.searchValue) {
                        fEmptyRooms = this.emptyRooms.filter(emptyRoom => emptyRoom.roomType == null || emptyRoom.roomType.includes(this.searchValue));
                        this.show_roomsCount = fEmptyRooms.length;
                        return fEmptyRooms;
                    }
                    this.show_roomsCount = this.emptyRooms.length;
                    return this.emptyRooms;
                    break;
                case "房间电话":
                    if (this.searchValue) {
                        fEmptyRooms = this.emptyRooms.filter(emptyRoom => emptyRoom.roomPhone == null || emptyRoom.roomPhone.includes(this.searchValue));
                        this.show_roomsCount = fEmptyRooms.length;
                        return fEmptyRooms;
                    }
                    this.show_roomsCount = this.emptyRooms.length;
                    return this.emptyRooms;
                    break;
                case "":
                    this.show_roomsCount = this.emptyRooms.length;
                    return this.emptyRooms;
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
        //编辑房间前将信息装载到currentRoom中
        roomCopyLoad: function (room) {
            //console.log(room.roomType)
            this.currentRoom.id = room.roomId;
            this.currentRoom.num = 1;
            this.currentRoom.number = room.roomNumber;
            this.currentRoom.type = room.roomType;
            this.currentRoom.price = room.roomPrice;
            this.currentRoom.capacity = room.roomCapacity;
            this.currentRoom.status = room.roomStatus;
        },
        //身份证号转换为性别
        idToGender: function (id) {
            const byte = id.at(16);

            if (byte == "1" || byte == "3" || byte == "5" || byte == "7" || byte == "9")
                return "男";
            else
                return "女";
        },
        //日期转换为时间戳
        trimInOutTime: function (date) {
            return date + "T00:00:00.000Z";
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
            this.ifInputPassed = false;

            if (this.currentRoom.inTime == "") {
                this.errorMessage = "入住时间不能为空";
                return;
            }
            else if (this.currentRoom.outTime == "") {
                this.errorMessage = "退房时间不能为空";
                return;
            }
            else if (this.currentRoom.inTime >= this.currentRoom.outTime) {
                this.errorMessage = "退房时间必须晚于入住时间";
                return;
            }

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
        checkin: function (emptyRoom) {
            this.currentRoom.inTime = "";
            this.currentRoom.outTime = "";
            this.checkInput();

            for (let index = 0; index < this.currentRoom.capacity; index++) {
                this.currentRoom.guestInfo.shift();
            }

            this.roomCopyLoad(emptyRoom);
            for (let i = 0; i < this.currentRoom.capacity; i++) {
                this.currentRoom.guestInfo.push({ id: "", name: "", phone: "" });
            }

            this.checkinDialogueVisible = true;
        },
        //提交客人信息并办理入住
        submitGuestInfo: async function () {
            if (!this.ifInputPassed)
                return;

            var that = this;
            var customerIds = [];

            //客房状态设为已入住
            await put("/api/Room/Update", {
                roomId: this.currentRoom.id,
                status: "已入住"
            }).then(function (res) {
                console.log("room updated");
            }, function (err) {
                ElMessage.error("入住办理失败！");
            });

            setTimeout(async function () {
                //查填写的客人是否存在
                for (let i = 0; i < that.currentRoom.num; i++) {
                    await get("/api/Customer/GetById/" + that.currentRoom.guestInfo[i].id).then(function (res) {
                        console.log("customer get55555555555555555555555555555");
                        console.log(res)
                        //记录每个客人是否需要增加到客人表
                        if (res.status) {
                            if (JSON.stringify(res, null, 4) != '""')
                                customerIds.push(res.data.customerId);
                        }
                        else
                            customerIds.push(-1);
                    }, function (err) {
                        console.log(err);
                    });
                }

                setTimeout(async function () {
                    for (let i = 0; i < customerIds.length; i++) {
                        //遍历customerIds，根据需要增加客人或修改客人信息
                        if (customerIds[i] == -1) {
                            await post("/api/Customer/Add", {
                                id: that.currentRoom.guestInfo[i].id,
                                name: that.currentRoom.guestInfo[i].name,
                                gender: that.idToGender(that.currentRoom.guestInfo[i].id),
                                phone: that.currentRoom.guestInfo[i].phone,
                                creditGrade: "5",
                                memberGrade: "0"
                            }, function (res) {
                                console.log("customer added");
                            }, function (err) {
                                console.log(err);
                            });
                        }
                        else {
                            await put("/api/Customer/Update", {
                                customerId: customerIds[i],
                                id: that.currentRoom.guestInfo[i].id,
                                name: that.currentRoom.guestInfo[i].name,
                                gender: that.idToGender(that.currentRoom.guestInfo[i].id),
                                phone: that.currentRoom.guestInfo[i].phone
                            }, function (res) {
                                console.log("customer updated");
                            }, function (err) {
                                console.log(err);
                            });
                        }
                    }

                    setTimeout(async function () {
                        for (let i = 0; i < customerIds.length; i++) {
                            //取得新增加客人的Id
                            if (customerIds[i] == -1) {
                                await get("/api/Customer/GetById/" + that.currentRoom.guestInfo[i].id).then(function (res) {
                                    customerIds[i] = res.data.customerId;
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                        }

                        setTimeout(async function () {
                            //无论客人是否存在，都需要增加入住表
                            for (let i = 0; i < customerIds.length; i++) {
                                await post("/api/Checkin/Add", {
                                    customerId: customerIds[i],
                                    roomId: that.currentRoom.id,
                                    checkinTime: that.trimInOutTime(that.currentRoom.inTime)
                                }, function (res) {
                                    console.log("checkin added");
                                }, function (err) {
                                    console.log(err);
                                });
                            }
                            const currentDate = new Date();

                            // 获取年月日时分秒
                            const year = currentDate.getFullYear();
                            const month = String(currentDate.getMonth() + 1).padStart(2, '0');
                            const day = String(currentDate.getDate()).padStart(2, '0');
                            const hours = String(currentDate.getHours()).padStart(2, '0');
                            const minutes = String(currentDate.getMinutes()).padStart(2, '0');
                            const seconds = String(currentDate.getSeconds()).padStart(2, '0');

                            // 格式化日期时间
                            const formattedDateTime = `${year}-${month}-${day}T${hours}:${minutes}:${seconds}`;

                            console.log("11111111111111111111111111111111");
                            console.log(formattedDateTime);
                            console.log(that.currentRoom);
                            post("/api/Roomorder/Add", {
                                //roomId: that.currentRoom.id,
                                employeeId: 1,
                                customerId: customerIds[0],
                                typeName: that.currentRoom.type,
                                orderStatus: "未处理",
                                createTime: formattedDateTime,
                                expectInTime: that.trimInOutTime(that.currentRoom.inTime),
                                expectOutTime: that.trimInOutTime(that.currentRoom.outTime),
                                price: that.currentRoom.price,
                                note: "",
                                customerNum: customerIds.length,
                            }).then(function (res) {
                                if (res.status) {
                                    //同时增加消费记录表
                                    post("/api/ConsumptionRecord/Add", {
                                        roomId: that.currentRoom.id,
                                        //employeeId: userId,
                                        consumeType: "房间费",
                                        consumeTime: that.trimInOutTime(that.currentRoom.inTime),
                                        consumeAmount: that.currentRoom.price,

                                    }).then(function (res) {
                                        console.log("consumption added");
                                    }, function (err) {
                                        console.log(err);
                                    });
                                }

                            })

                            ElMessage.success("入住办理成功！");
                            that.checkinDialogueVisible = false;
                            that.getEmptyRooms();
                        }, 100);
                    }, 100);
                }, 100);
            }, 100);
        },
        //房间类型id转换为房间信息
        typeToInfo: function async(roomTypeId) {
            switch (roomTypeId) {
                case 1:
                    return {
                        roomType: "双人间",
                        roomPrice: 269,
                        roomCapacity: 2
                    }
                    break;
                case 2:
                    return {
                        roomType: "标准间",
                        roomPrice: 199,
                        roomCapacity: 2
                    }
                    break;
                case 3:
                    return {
                        roomType: "豪华套房",
                        roomPrice: 699,
                        roomCapacity: 2
                    }
                case 4:
                    return {
                        roomType: "家庭房",
                        roomPrice: 329,
                        roomCapacity: 4
                    }
                case 5:
                    return {
                        roomType: "观景房",
                        roomPrice: 499,
                        roomCapacity: 3
                    }
                case 6:
                    return {
                        roomType: "行政套房",
                        roomPrice: 799,
                        roomCapacity: 2
                    }
                    break;
                default:
                    break;
            }
        },
        //从后端取得空房数据并进行加工，以便表展示
        getEmptyRooms: async function () {
            var that = this;

            await get("/api/Room/GetEmptyRoomsAll").then(function (res) {
                console.log("empty rooms get");
                console.log(res)
                that.show_roomsCount = res.data.length;
                const oldLength = that.emptyRooms.length;
                for (let i = 0; i < oldLength; i++) {
                    that.emptyRooms.shift();
                }

                for (let i = 0; i < res.data.length; i++) {
                    const roomTypeInfo = that.typeToInfo(res.data[i].typeId);

                    that.emptyRooms.push({
                        roomId: res.data[i].roomId,
                        roomNumber: res.data[i].roomNumber,
                        roomType: roomTypeInfo.roomType,
                        roomPrice: roomTypeInfo.roomPrice,
                        roomCapacity: roomTypeInfo.roomCapacity,
                        roomPhone: res.data[i].roomPhone,
                        roomStatus: res.data[i].status
                    });
                }

                ElMessage.success("空房查询成功！");
            }, function (err) {
                console.log(err);
            });
        }
    },
    async mounted() {
        await this.getEmptyRooms();
    }
}
</script>

<style scoped>
.idle-query {
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

.table-header-roomscount {
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

.row-room-number {
    font-size: large;
    color: #196883;
}

.row-room-type {
    font-size: large;
    color: #a76200;
}

.row-room-price {
    font-size: large;
    color: #a76200;
}

.row-room-capacity {
    font-size: large;
    color: #196883;
}

.row-room-phone {
    font-size: large;
    color: #196883;
}

.row-room-status-empty {
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

::v-deep .el-form-item {
    margin-bottom: 40px;
}

::v-deep .el-form-item__label {
    font-size: x-large;
    font-weight: lighter;
    color: #196883;
}

.dialog-input {
    margin-right: 10px;
    width: 30%;
}

.dialog-picker {
    margin-left: 10px;
    margin-right: 10px;
    width: 20%;
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