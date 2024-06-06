<template>
    <div class="settlement-bill">
        <img src="../../assets/cashier.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 前台收银 </label>
            <label class="title-content"> 结算帐单 </label>
        </div>

        <div style="height: 50px;"></div>

        <el-row :gutter="16">
            <el-col :span="8">
                <div class="statistic-card">
                    <el-statistic :value="todayRevenue">
                        <template #title>
                            <div class="statistic-card-header">
                                今日收银额
                                <el-tooltip
                                    effect="dark"
                                    content="单位：元"
                                    placement="top"
                                >
                                    <el-icon class="statistic-card-header-icon" size="20">
                                        <Warning />
                                    </el-icon>
                                </el-tooltip>
                            </div>
                        </template>
                    </el-statistic>
                    <div class="statistic-footer">
                        <div class="footer-item">
                            <span> 较昨日 </span>
                            <template v-if="dayRevenueRate >= 0">
                                <span class="statistic-increase">
                                    {{dayRevenueRate}}%
                                    <el-icon>
                                        <CaretTop />
                                    </el-icon>
                                </span>
                            </template>
                            <template v-else>
                                <span class="statistic-decrease">
                                    {{dayRevenueRate}}%
                                    <el-icon>
                                        <CaretBottom />
                                    </el-icon>
                                </span>
                            </template>
                        </div>
                    </div>
                </div>
            </el-col>
            <el-col :span="8">
                <div class="statistic-card">
                    <el-statistic :value="currentMonthRevenue">
                        <template #title>
                            <div class="statistic-card-header">
                                本月收银额
                                <el-tooltip
                                    effect="dark"
                                    content="单位：元"
                                    placement="top"
                                >
                                    <el-icon class="statistic-card-header-icon" size="20">
                                        <Warning />
                                    </el-icon>
                                </el-tooltip>
                            </div>
                        </template>
                    </el-statistic>
                    <div class="statistic-footer">
                        <div class="footer-item">
                            <span>较上月</span>
                            <template v-if="monthRevenueRate >= 0">
                                <span class="statistic-increase">
                                    {{monthRevenueRate}}%
                                    <el-icon>
                                        <CaretTop />
                                    </el-icon>
                                </span>
                            </template>
                            <template v-else>
                                <span class="statistic-decrease">
                                    {{monthRevenueRate}}%
                                    <el-icon>
                                        <CaretBottom />
                                    </el-icon>
                                </span>
                            </template>
                        </div>
                    </div>
                </div>
            </el-col>
            <el-col :span="8">
                <div class="statistic-card">
                    <el-statistic :value="todayPaymentCount" title="New transactions today">
                        <template #title>
                            <div class="statistic-card-header">
                                今日收银单量
                            </div>
                        </template>
                    </el-statistic>
                    <div class="statistic-footer">
                        <div class="footer-item">
                            <span>较昨日</span>
                            <template v-if="dayPaymentRate >= 0">
                                <span class="statistic-increase">
                                    {{dayPaymentRate}}%
                                    <el-icon>
                                        <CaretTop />
                                    </el-icon>
                                </span>
                            </template>
                            <template v-else>
                                <span class="statistic-decrease">
                                    {{dayOrderRate}}%
                                    <el-icon>
                                        <CaretBottom />
                                    </el-icon>
                                </span>
                            </template>
                        </div>
                    </div>
                </div>
            </el-col>
        </el-row>

        <div class="form-container">
            <!--前台收银-结算帐单-->
            <el-form ref="formRef" :model="form" :rules="rules" label-width="210px" class="form">
                <el-form-item label="查询方式" prop="select">
                    <el-select v-model="form.select" placeholder="请选择" class="form-select" aria-required="true">
                        <el-option label="房间号" value="房间号"></el-option>
                        <el-option label="身份证号" value="身份证"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="房间号/身份证号" prop="searchNo">
                    <el-input v-model="form.searchNo" placeholder="请输入房间号/身份证号" class="form-input">
                    </el-input>
                </el-form-item>

                <el-button icon="search" @click="submitForm()" class="search-button"> 查询 </el-button>
            </el-form>
        </div>

        <!--展示消费记录-->
        <el-dialog v-model="consumptionDialogVisible" title="消费单" width="75%">
            <div class="dialog-table-container">
                <el-table :data="consumptionData" class="dialog-table" :header-cell-style="{
                    'padding': '20px',
                    'font-size': 'x-large',
                    'font-weight': 'lighter',
                    'color': '#ffffff',
                    'background-color': '#196883'
                }">
                    <el-table-column prop="ConsumeId" label="消费ID" width="140">
                        <template #default="scope">
                            <label class="dialog-row-consume-id"> {{ scope.row.ConsumeId }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="RoomId" label="房间ID" width="140">
                        <template #default="scope">
                            <label class="dialog-row-room-id"> {{ scope.row.RoomId }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="EmployeeId" label="接待员工ID">
                        <template #default="scope">
                            <label class="dialog-row-employee-id"> {{ scope.row.EmployeeId }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="ConsumeType" label="消费类型" width="160">
                        <template #default="scope">
                            <label class="dialog-row-consume-type"> {{ scope.row.ConsumeType }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="ConsumeTime" label="消费时间">
                        <template #default="scope">
                            <label class="dialog-row-consume-time"> {{ scope.row.ConsumeTime }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="ConsumeAmount" label="消费金额" show-summary="true" width="160">
                        <template #default="scope">
                            <label class="dialog-row-consume-amount"> {{ scope.row.ConsumeAmount }} </label>
                        </template>
                    </el-table-column>
                </el-table>
            </div>

            <p>总消费金额：{{ totalConsumeAmount }}</p>
            <div class="dialog-buttons">
                <button class="dialog-checkout-button" @click="settleDialogVisible = true"> 开始结账 </button>
                <button class="dialog-cancel-button" @click="consumptionDialogVisible = false"> 取消 </button>
            </div>
        </el-dialog>

        <!--结帐中界面-->
        <el-dialog v-model="settleDialogVisible" title="结账" width="35%">
            <el-form ref="settleFormRef" :model="settleForm" label-width="140px" :rules="settleRules">
                <el-form-item label="支付方式" prop="PayWay" required>
                    <el-select v-model="settleForm.PayWay"  placeholder="请选择支付方式" class="dialog-form-select">
                        <el-option label="微信" value="微信"></el-option>
                        <el-option label="支付宝" value="支付宝"></el-option>
                        <el-option label="现金" value="现金"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="类别" prop="BillType" required>
                    <el-select v-model="settleForm.BillType"  placeholder="请选择账单类别" class="dialog-form-select">
                        <el-option label="住房" value="住房"></el-option>
                        <el-option label="餐饮" value="餐饮"></el-option>
                        <el-option label="其它" value="其它"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="发票号" prop="InvoiceNumber">
                    <el-input v-model="settleForm.InvoiceNumber" class="dialog-form-input"></el-input>
                </el-form-item>
            </el-form>
            
            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="SettleBill"> 确认结账 </button>
                <button class="dialog-cancel-button" @click="settleDialogVisible = false"> 取消 </button>
            </div>
        </el-dialog>
    </div>

        
                
</template>

<script>
import { ElMessage, ElMessageBox } from 'element-plus';
import { getConsumptionRecord,Statistics } from '../../axios/settlement_bill.js';
import { get, post, put,userId,getUserInfo  } from '../../axios/axiosConfig.js'
import {ArrowRight,CaretBottom,CaretTop,Warning,} from '@element-plus/icons-vue'
import moment from 'moment';
    export default {
        data() {
            return {
                form: {
                    select: '',
                    searchNo: null
                },
                rules: {
                    select: [{required: true, message: '请选择查询方式', trigger: 'blur' }],
                    searchNo: [
                        { required: true, message: '请输入房间号或身份证号', trigger: 'blur' },
                        {
                            pattern: /(^\d{8}(0\d|10|11|12)([0-2]\d|30|31)\d{3}$)|(^\d{6}(18|19|20)\d{2}(0\d|10|11|12)([0-2]\d|30|31)\d{3}(\d|X|x)$)/,
                            message: '请输入正确的证件号',
                            validator: this.validSjh,
                            trigger: 'blur',
                        },
                    ],
                },
                settleForm:{
                    CustomerId:'',
                    PayWay:'',
                    PayCount:null,
                    EmpId:userId,
                    BillType:'',
                    InvoiceNumber:'',
                },
                settleRules: {
                    PayWay:[{ required: true, message: '请选择支付方式', trigger: 'blur' }],
                    BillType:[{ required: true, message: '请选择账单类别', trigger: 'blur' }],
                },
                consumptionDialogVisible:false,
                totalConsumeAmount:0,
                consumptionData:[],
                settleDialogVisible:false,

                todayRevenue:0,//今日收银额
                dayRevenueRate:0,//日收银额增率
                currentMonthRevenue:0,//当月收银额
                monthRevenueRate:0,//月收银额增率
                todayPaymentCount:0,//当日收银单量
                dayPaymentRate:0,//日收银单量增率
            };
        },
        methods: {

            /*检查是否更新收益额统计数据 */
            async checkNewStatisticData() {
                const currentTime = new Date();
                const currentDay = currentTime.getDate(); 
                const currentHour = currentTime.getHours();
                const currentMinute = currentTime.getMinutes();
                
                // 每天0点 每月第一天0点更新昨日、上月数据
                // if (currentHour === 0 && currentMinute === 0) {
                //     this.yesterdayRevenue = todayRevenue;
                //     this.todayRevenue = 0;
                //     this.dayRevenueRate = 0;
                //     this.yesterdayPaymentCount = todayPaymentCount;
                //     this.todayPaymentCount = 0;
                //     this.dayPaymentRate = 0;
                //     if(currentDay === 1){
                //         this.lastmonthRevenue = currentMonthRevenue;
                //         this.currentMonthRevenue = 0;
                //     }
                // }

                const [
                    dayRevenueRate,
                    todayRevenue,
                    monthRevenueRate,
                    currentMonthRevenue,
                    dayPaymentRate,
                    todayPaymentCount
                ] = await Statistics();

                this.todayRevenue = todayRevenue;
                this.dayRevenueRate = dayRevenueRate.toFixed(2);
                this.currentMonthRevenue = currentMonthRevenue;
                this.monthRevenueRate = monthRevenueRate.toFixed(2);
                this.todayPaymentCount = todayPaymentCount;
                this.dayPaymentRate = dayPaymentRate.toFixed(2);
            },

            // 身份证验证
            async validSjh(rule, value, callback) {
                console.log('rule');
            // 身份证号码为15位或者18位，15位时全为数字，18位前17位为数字，最后一位是校验位，可能为数字或字符X
                if(this.form.select == '身份证'){
                    let reg = /(^\d{8}(0\d|10|11|12)([0-2]\d|30|31)\d{3}$)|(^\d{6}(18|19|20)\d{2}(0\d|10|11|12)([0-2]\d|30|31)\d{3}(\d|X|x)$)/;
                    if (!value) {
                        return callback(new Error("身份证号不能为空"));
                    } else if (!reg.test(value)) {
                        callback(new Error("你输入的身份证长度或格式错误"));
                    } else {
                        //身份证城市
                        var aCity = {
                        11: "北京",
                        12: "天津",
                        13: "河北",
                        14: "山西",
                        15: "内蒙古",
                        21: "辽宁",
                        22: "吉林",
                        23: "黑龙江",
                        31: "上海",
                        32: "江苏",
                        33: "浙江",
                        34: "安徽",
                        35: "福建",
                        36: "江西",
                        37: "山东",
                        41: "河南",
                        42: "湖北",
                        43: "湖南",
                        44: "广东",
                        45: "广西",
                        46: "海南",
                        50: "重庆",
                        51: "四川",
                        52: "贵州",
                        53: "云南",
                        54: "西藏",
                        61: "陕西",
                        62: "甘肃",
                        63: "青海",
                        64: "宁夏",
                        65: "新疆",
                        71: "台湾",
                        81: "香港",
                        82: "澳门",
                        91: "国外"
                        };
                        if (!aCity[parseInt(value.substr(0, 2))]) {
                            callback(new Error("你的身份证地区非法"));
                        }
                        // 出生日期验证
                        var sBirthday = (
                            value.substr(6, 4) + "-" +
                            Number(value.substr(10, 2)) + "-" +
                            Number(value.substr(12, 2))
                        ).replace(/-/g, "/"),
                        d = new Date(sBirthday);
                        if (sBirthday != d.getFullYear() + "/" + (d.getMonth() + 1) + "/" + d.getDate()) {
                            callback(new Error("身份证上的出生日期非法"));
                        }
                
                        // 身份证号码校验
                        var sum = 0,
                        weights = [7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2],
                        codes = "10X98765432";
                        for (var i = 0; i < value.length - 1; i++) {
                            sum += value[i] * weights[i];
                        }
                        var last = codes[sum % 11]; //计算出来的最后一位身份证号码
                        if (value[value.length - 1] != last) {
                            callback(new Error("你输入的身份证号非法"));
                        } 
                        callback();
                    }
                }
            },



            submitForm(){
                /*向后端传数据*/
                /*获取后端数据并弹窗*/
                
                this.$refs.formRef.validate(valid => {
                    if (valid) {
                    // 表单验证通过，可以进行提交操作
                        getConsumptionRecord(this.form.select, this.form.searchNo)
                            .then(response => {
                                if (response) {
                                    const allPaid = response.data.every(item => item.BillId !== null);
                                    if(allPaid){
                                        if(response.data.length == 0)
                                            ElMessage('未查询到任何消费记录');
                                        else
                                            ElMessage.success('账单已支付！');
                                    }
                                    else{
                                        this.consumptionData = response.data.filter(item => item.BillId === null);
                                        for(const data of this.consumptionData){
                                            data.ConsumeTime = moment(data.ConsumeTime).format('YYYY-MM-DD HH:mm:ss'); //时间格式转换
                                        }
                                        
                                        //计算消费总额
                                        this.totalConsumeAmount = this.consumptionData.reduce((total, row) => total + row.ConsumeAmount, 0);
                                        this.settleForm.PayCount=this.totalConsumeAmount;
                                        //打开消费单对话框
                                        this.consumptionDialogVisible=true;
                                    }
                                }
                                else{
                                    ElMessage('未查询到任何消费记录');
                                }
                            })
                            .catch(error => {
                                console.log(error);
                            });
                    } else {
                        // 表单验证不通过，显示错误提示
                        this.$message.error('请填写正确的表单数据');
                    }
                    
                });
            },

            async SettleBill(){
                const res=getUserInfo();
                this.settleForm.EmpId = res.employeeId;
                this.$refs.settleFormRef.validate(async valid => {
                    if (valid) {
                        var myDate = new Date();
                        console.log(myDate.toISOString());
                        console.log(typeof myDate.toISOString());
                        const roomBill = this.consumptionData.filter(item => item.ConsumeType === '住房');
                        const foodBill = this.consumptionData.filter(item => item.ConsumeType === '餐饮'||item.ConsumeType === '夜宵');
                        const roomcost = roomBill.reduce((total, row) => total + row.ConsumeAmount, 0);
                        const foodcost = foodBill.reduce((total, row) => total + row.ConsumeAmount, 0);
                        console.log(this.form.select)
                        /*获取CustomerId、RoomNum、RoomId*/
                        var RoomNum,RoomId;
                        if(this.form.select === '房间号'){
                            console.log(this.form.searchNo)
                            const response = await get('/api/Customer/GetCustomerAndTimeInfo/'+this.form.searchNo,{});
                            console.log(response.data);
                            if(response.data.length!==0)
                                this.settleForm.CustomerId = response.data[response.data.length-1].customerId;
                            console.log('CustomerId'+this.settleForm.CustomerId);

                            RoomNum = this.form.searchNo;

                            RoomId = this.consumptionData[0].RoomId;
                        }
                        else if(this.form.select === '身份证'){
                            const response = await get('/api/Customer/GetById/'+this.form.searchNo,{});
                            this.settleForm.CustomerId = response.data.customerId;

                            RoomId = this.consumptionData[0].RoomId;
                            const res = await get('/api/Room/Get/'+RoomId,{});
                            RoomNum = res.data.roomNumber;
                        }
                        console.log('CustomerId'+this.settleForm.CustomerId);

                        //账单表增
                        const billId = await post('/api/Bill/Add',{
                            "roomCost": roomcost,
                            "foodCost": foodcost,
                            "otherCost": this.totalConsumeAmount-roomcost-foodcost,
                            "sumCost": this.totalConsumeAmount,
                            "billType": this.settleForm.BillType,
                            "invoiceNumber": this.settleForm.InvoiceNumber
                        })

                        //支付表增
                        await post('/api/Pay/Add',{
                            "customerId": this.settleForm.CustomerId,
                            "billId": billId,
                            "payMethod": this.settleForm.PayWay,
                            "payTime": myDate.toISOString(),
                            "payAmount": this.settleForm.PayCount,
                            "payStatus": '已支付',
                        }).then(response => {
                                console.log(response.data);    
                                })
                                .catch(error => {
                                    console.error(error);
                                });

                        //BillId回填消费记录表
                        for (const data of this.consumptionData) {
                            try {
                                // 在每次循环中，使用 async/await 发送更新请求
                                const response = await put('/api/ConsumptionRecord/Update', {
                                consumeId: data.ConsumeId, 
                                billId: billId,
                                });
                                console.log(response.data);
                            } catch (error) {
                                console.error(error);
                            }
                        }

                        //退房 checkin表update退房时间
                        const checkoutRoom = await get('/api/Checkin/GetByRoomNum/'+RoomNum,{});
                        var lasttime;
                        if(checkoutRoom.length!==0)
                            lasttime = checkoutRoom[checkoutRoom.length-1].InTime;
                        const CustomerIdArray = checkoutRoom.filter(item => item.InTime === lasttime).map(item => item.CustomerId);

                        for(const CustomerId of CustomerIdArray){
                            await put('/api/Checkin/Update',{
                                "customerId": CustomerId,
                                "roomId": RoomId,
                                "checkInTime": lasttime,
                                "checkOutTime": myDate.toISOString(),
                            })
                            .then(response => {
                                console.log(response.data);
                            })
                            .catch(error => {
                                console.error(error);
                            })
                        }
                        this.settleForm={};
                        this.settleDialogVisible=false;
                        this.consumptionDialogVisible=false;
                        ElMessage.success('结账成功！');
                        // 提交完成后清空表单数据
                        this.form = {};
                    }
                    else{
                        // 表单验证不通过，显示错误提示
                        this.$message.error('请填写正确的表单数据');
                    }  
                });  
            }
        },
        mounted() {
            this.checkNewStatisticData
            
        },
        async created() {
            const [
                dayRevenueRate,
                todayRevenue,
                monthRevenueRate,
                currentMonthRevenue,
                dayPaymentRate,
                todayPaymentCount
            ] = await Statistics();

            this.todayRevenue = todayRevenue;
            this.dayRevenueRate = dayRevenueRate.toFixed(2);
            this.currentMonthRevenue = currentMonthRevenue;
            this.monthRevenueRate = monthRevenueRate.toFixed(2);
            this.todayPaymentCount = todayPaymentCount;
            this.dayPaymentRate = dayPaymentRate.toFixed(2);

            console.log('收银数据：', this.todayRevenue, this.dayRevenueRate);
            }

    }

    

</script>

<style scoped>
.settlement-bill {
    padding: 20px;
    background-image: linear-gradient(-45deg, #97cba7, #9ab7e3);
    height:95vh
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
    margin-bottom: 20px;
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
.statistic-card {
    height: 100%;
    margin-right: 20px;
    padding: 20px;
    border-radius: 15px;
    border:1px solid#ddd;
    background-color:#e6ebf3;
    display: grid;
    text-align: center;
    align-content: space-evenly;
    box-shadow: 0 0 5px rgba(0, 0, 0, 0.1),
                5px 0 5px rgba(0, 0, 0, 0.1),
                0 5px 5px rgba(0, 0, 0, 0.1),
                -5px 0 5px rgba(0, 0, 0, 0.1);
}
.statistic-card-header {
    display: inline-flex;
    align-items: center;
    font-size: x-large;
    color: #196883;
}
.statistic-card-header-icon {
    margin-left: 4px;
    size: 16;
    color: #68868f;
}
::v-deep .el-statistic__content {
    font-size: 40px;
    font-weight: lighter;
    color: #00929b;
    margin-top: 10px;
}
.statistic-footer {
    display: flex;
    justify-content: center;
    align-items: center;
    flex-wrap: wrap;
    font-size: 18px;
    color: var(--el-text-color-regular);
}
.statistic-footer .footer-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.statistic-footer .footer-item span:last-child {
  display: inline-flex;
  align-items: center;
  margin-left: 4px;
}
.statistic-increase {
  color: var(--el-color-success);
}
.statistic-decrease {
  color: var(--el-color-error);
}
.form-container {
    width: 40%;
    height: max-content;
    background-color: rgb(255, 255, 255, 0.7);
    margin: auto;
    margin-top: 100px;
    text-align: center;
    border-radius: 25px;
    padding: 50px 50px;
}
.form {
    max-width: 500px;
    margin: 0 auto;
}
::v-deep .el-form-item {
    margin-bottom: 40px;
}
::v-deep .el-form-item__label {
    font-size: x-large;
    font-weight: lighter;
    color: #196883;
}
.form-input {
    width: 100%;
}
.form-select {
    width: 100%;
}
::v-deep .el-input {
    --el-input-border-color: rgb(212, 212, 212);
    --el-input-hover-border-color: #324157;
    --el-input-focus-border-color: #2aba98;
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
    border: 2px solid #2aba98;
}
::v-deep .el-input__inner {
    font-size: x-large;
    color: #196883;
}
::v-deep .el-select {
    --el-select-border-color-hover: #324157;
    --el-select-input-focus-border-color: #2aba98;
}
::v-deep .el-select .el-select__caret {
    font-size: 30px;
    color: #324157;
}
.search-button {
    margin-top: 10px;
    width: 50%;
    height: 50px;
    border-radius: 10px;
    border: 0;
    background-image: linear-gradient(to right, #30cfd0, #330867);
}
.search-button:hover {
    background-image: linear-gradient(to right, #90de83, #00929b);
}
::v-deep .el-button {
    color: white;
    font-size: 25px;
    font-weight: lighter;
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
.dialog-table-container {
    background-color: #196883;
    padding: 20px;
    border-radius: 15px;
}
.dialog-table {
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
.dialog-row-consume-id {
    font-size: large;
    color: #196883;
}
.dialog-row-room-id {
    font-size: large;
    color: #a76200;
}
.dialog-row-employee-id {
    font-size: large;
    color: #196883;
}
.dialog-row-consume-type {
    font-size: large;
    color: #196883;
}
.dialog-row-consume-time {
    font-size: large;
    color: #196883;
}
.dialog-row-consume-amount {
    font-size: large;
    color: #a76200;
}
::v-deep .el-dialog p {
    font-size: x-large;
    font-weight: bold;
    color: #324157;
}
.dialog-form-input {
    width: 80%;
}
.dialog-form-select {
    width: 80%;
}
.dialog-buttons {
    display: flex;
    justify-content: center;
    margin-top: 30px;
}
.dialog-checkout-button {
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
.dialog-checkout-button:hover {
    background-color: #ccffc3;
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