<template>
    <div class="order-dishes">
        <el-header class="order-header">
            <span class="header-picker-container">
                <el-date-picker v-model="wantedDate" type="date" :placeholder="formattedDate" :shortcuts="shortcuts"
                    :disabled-date="disabledDate" :clearable="false" @change="handleDatePickerChange"
                    class="header-picker"></el-date-picker>
            </span>

            <span class="date-back">
                <el-button icon="back" class="date-back-button" @click="backOneDay"> 前一天 </el-button>
            </span>

            <span class="btn-row">
                <span class="one-btn" v-for="date in dates" :key="date">
                    <button class="date-btn" @click="wantedDate = date">
                        {{ date.toLocaleDateString('zh-CN', { weekday: 'long' }) }}
                        {{ date.getDate() }}
                    </button>

                    <div class="right-column-btn">
                        <button class="breakfast" @click="meal = 'breakfast'; wantedDate = date;">
                            早餐
                        </button>
                        <button class="lunch" @click="meal = 'lunch'; wantedDate = date;">
                            午餐
                        </button>
                        <button class="dinner" @click="meal = 'dinner'; wantedDate = date;">
                            晚餐
                        </button>
                    </div>
                </span>
            </span>

            <span class="week-buttons">
                <el-button icon="back" class="week-back-button" @click="backOneWeek">
                    上一周
                </el-button>
                <el-button icon="right" class="week-forward-button" @click="forwardOneWeek">
                    下一周
                </el-button>
            </span>
        </el-header>

        <div class="order-main">
            <el-card class="table-card">
                <template #header>
                    <div class="table-card-header">
                        <div class="table-card-status-container" v-for="status in tableStatus"
                            :style="{ backgroundColor: status.color }">
                            <label :key="status.value" class="table-card-status-label">
                                {{ status.value }}: {{ numOfStatus[status.value] }}桌
                            </label>
                        </div>
                        <div class="table-card-select-container">
                            <el-form label-width="110">
                                <el-form-item label="桌位位置：">
                                    <el-select v-model="wantedLocation" multiple collapse-tags collapse-tags-tooltip
                                        :max-collapse-tags="2" placeholder="选择桌位位置" class="table-card-select">
                                        <el-option v-for="location in val['tableLocation']" :key="location.value"
                                            :label="location.label" :value="location.value">
                                        </el-option>
                                    </el-select>
                                </el-form-item>
                                <el-form-item label="桌位状态：">
                                    <el-select v-model="wantedStatus" multiple collapse-tags collapse-tags-tooltip
                                        :max-collapse-tags="2" placeholder="选择桌位状态" class="table-card-select">
                                        <el-option v-for="status in tableStatus" :key="status.value" :label="status.value"
                                            :value="status.value">
                                        </el-option>
                                    </el-select>
                                </el-form-item>
                            </el-form>
                        </div>
                        <div class="table-card-meal-info">
                            <label> {{ wantedDate.toLocaleDateString('zh-CN', options) }} </label>
                            <label> {{ meal2Chinese(meal) }} </label>
                        </div>
                    </div>
                </template>
                <div class="table-card-table-list">
                    <div class="table-card-table" v-for="table in tableShow"
                        :style="{ backgroundColor: status2Color(table.tableStatus), color: status2FontColor(table.tableStatus) }"
                        @click="handleOrder(table.tableId)">
                        {{ table.tableType }}
                        <hr />
                        桌号: {{ table.tableId }}
                        <br>
                        人数: {{ table.capacity }}
                    </div>
                </div>
            </el-card>

            <el-card class="book-card">
                <template #header>
                    <div class="book-card-header">
                        <span style="font-size: xx-large; color: #196883;"> 预定信息处理 </span>
                        <el-button icon="plus" class="add-button" @click="addBook"></el-button>
                        <el-switch v-model="isAllBook" class="book-card-switch" active-text="全部预定"
                            inactive-text="未来预定"></el-switch>
                    </div>
                </template>

                <div class="book-card-table-container">
                    <el-table :data="bookShow" class="book-card-table" :header-cell-style="{
                        'font-size': '14px',
                        'font-weight': 'lighter',
                        'color': '#ffffff',
                        'background-color': '#196883'
                    }">
                        <el-table-column prop="tableId" label="桌位号" width="70">
                            <template #default="scope">
                                <label class="book-card-row-table-id"> {{ scope.row.tableId }} </label>
                            </template>
                        </el-table-column>
                        <el-table-column prop="customerId" label="顾客" width="90">
                            <template #default="scope">
                                <label class="book-card-row-customer-id"
                                    @click="handleShowCustomerInfo(scope.row.customerId)">
                                    {{ customerId2Name(scope.row.customerId) }}
                                </label>
                            </template>
                        </el-table-column>
                        <el-table-column prop="bookTime" label="预定时间" width="130">
                            <template #default="scope">
                                <label class="book-card-row-book-time"> {{ bookTime2DateStr(scope.row.bookTime) }} </label>
                                <label class="book-card-row-book-time"> {{ bookTime2TimeStr(scope.row.bookTime) }} </label>
                            </template>
                        </el-table-column>
                        <el-table-column prop="bookNumber" label="人数" width="70">
                            <template #default="scope">
                                <label class="book-card-row-num"> {{ scope.row.bookNumber }} </label>
                            </template>
                        </el-table-column>
                        <el-table-column prop="bookStatus" label="订单状态" align="center" width="160">
                            <template #default="scope">
                                <div class="book-card-row-book-status">
                                    <el-select v-model="scope.row.bookStatus" @change="handleBookChange(scope.row)">
                                        <el-option v-for="status in wantedBookStatus" :key="status" :value="status"
                                            :label="status"></el-option>
                                    </el-select>
                                </div>
                            </template>
                        </el-table-column>
                        <el-table-column prop="bookRequement" label="特殊要求">
                            <template #default="scope">
                                <label class="book-card-row-special-demand"> {{ scope.row.bookRequement }} </label>
                            </template>
                        </el-table-column>
                        <el-table-column prop="note" label="备注">
                            <template #default="scope">
                                <label class="book-card-row-note"> {{ scope.row.bookNote }} </label>
                            </template>
                        </el-table-column>
                        <el-table-column label="操作" align="center" width="150">
                            <template #default="scope">
                                <el-button icon="delete" class="delete-button"
                                    @click="handleDeleteBook(scope.$index, $event)"> 删除 </el-button>
                            </template>
                        </el-table-column>
                    </el-table>
                </div>
            </el-card>

            <el-card class="order-card">
                <template #header>
                    <div v-if="selectedTable.tableId !== ''" class="order-card-header">
                        <span style="font-size: xx-large; color: #196883;"> {{ selectedTable.tableId }} </span>
                        <span style="font-size: larger;"> 号 </span>
                        <span style="font-size: xx-large; color: #196883;"> {{ selectedTable.tableType }} </span>
                        <hr />
                        <el-form label-width="160">
                            <el-form-item label="桌位位置：">
                                <label style="font-size: x-large; color: #196883"> {{ selectedTable.tableLocation }}
                                </label>
                            </el-form-item>
                            <el-form-item label="桌位状态：">
                                <div class="order-card-select-container">
                                    <el-select v-model="selectedTable.tableStatus" placeholder="请选择桌位状态"
                                        style="width: 160px" class="order-card-select">
                                        <el-option v-for="status in tableStatus" :key="status.value" :label="status.label"
                                            :value="status.value">
                                        </el-option>
                                    </el-select>
                                </div>
                            </el-form-item>
                            <el-form-item label="是否可预订：">
                                <el-switch v-model="selectedTable.bookable" :active-value="1"
                                    :inactive-value="0"></el-switch>
                            </el-form-item>
                            <el-form-item label="是否可用：">
                                <el-switch v-model="selectedTable.available" :active-value="1"
                                    :inactive-value="0"></el-switch>
                            </el-form-item>
                            <el-form-item label="总花销：">
                                <label style="font-size: x-large; color: #196883"> {{ sumCost }}元 </label>
                            </el-form-item>
                        </el-form>

                        <el-button class="order-card-button" type="primary" @click="handleAddOrder">新增点单</el-button>
                        <el-button class="order-card-button" type="primary" @click="handleConfirmDish">点单确认</el-button>
                        <el-button class="order-card-button" type="primary" @click="handlePayOnRoom">挂账</el-button>
                    </div>
                    <div v-else>
                        <span style="font-size:xx-large;font-weight: 400;">未选择桌位</span>
                    </div>
                </template>
                <div class="order-card-table-container">
                    <el-table :data="selectedTableOrder" @selection-change="selectConfirmDish" class="order-card-table"
                        :header-cell-style="{
                            'font-size': '14px',
                            'font-weight': 'lighter',
                            'color': '#ffffff',
                            'background-color': '#196883'
                        }">
                     
                        <el-table-column type="selection" align="center"
                            :selectable="orderSelectionCheck"></el-table-column>
                        <el-table-column prop="dishName" label="菜品名称">
                            <template #default="scope">
                              
                                <label class="order-card-row-dish-name"> {{ scope.row.dishName }} </label>
                            </template>
                        </el-table-column>
                        <el-table-column prop="dishPrice" label="菜品价格">
                            <template #default="scope">
                                <label class="order-card-row-dish-price"> {{ scope.row.dishPrice }} </label>
                            </template>
                        </el-table-column>
                        <el-table-column prop="orderTime" label="点单时间" width="130">
                            <template #default="scope">
                                <label class="order-card-row-order-time"> {{ bookTime2DateStr(scope.row.orderTime) }}
                                </label>
                                <label class="order-card-row-order-time"> {{ bookTime2TimeStr(scope.row.orderTime) }}
                                </label>
                            </template>
                        </el-table-column>
                        <el-table-column prop="orderStatus" label="点单状态">
                            <template #default="scope">
                                <label class="order-card-row-order-status"> {{ scope.row.orderStatus }} </label>
                            </template>
                        </el-table-column>
                    </el-table>
                 
                </div>
            </el-card>
        </div>

        <el-dialog title="新增预定" v-model="addVisible" width="35%">
            <el-form label-width="120px" class="add-book-dialog-form">
                <el-form-item label="桌号">
                    <el-select v-model="tempBook.tableId" placeholder="选择桌号" class="add-book-dialog-select">
                        <el-option v-for="table in tableFree" :key="table.tableId" :value="table.tableId"
                            :label="table.tableId + '号 ' + table.tableType + ' 位置: ' + table.tableLocation"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="顾客">
                    <el-input v-model="customerKeyword" placeholder="搜索顾客姓名" class="add-book-dialog-input"></el-input>
                    <el-button icon="plus" @click="addCustomer" class="add-book-dialog-add-button"></el-button>

                    <div class="add-book-dialog-table-container">
                        <el-table class="add-book-dialog-table" :data="customerShow"
                            @selection-change="handleCustomerSelectionChange" :header-cell-style="{
                                'font-size': '14px',
                                'font-weight': 'lighter',
                                'color': '#ffffff',
                                'background-color': '#196883'
                            }">
                            <el-table-column type="selection" align="center"></el-table-column>
                            <el-table-column prop="name" label="姓名">
                                <template #default="scope">
                                    <template v-if="scope.row.gender === '男'">
                                        {{ scope.row.name + '先生' }}
                                    </template>
                                    <template v-else>
                                        {{ scope.row.name + '女士' }}
                                    </template>
                                </template>
                            </el-table-column>
                            <el-table-column prop="phone" label="电话"></el-table-column>
                        </el-table>
                    </div>
                </el-form-item>
                <el-form-item label="时间">
                    <el-date-picker v-model="tempBook.bookTime" type="datetime" placeholder="请选择预定时间"
                        :disabled-date="isDisabledDate"></el-date-picker>
                </el-form-item>
                <el-form-item label="人数">
                    <el-input-number v-model="tempBook.bookNumber" :min="1" placeholder="请选择人数"
                        class="add-book-dialog-input-number"></el-input-number>
                </el-form-item>
                <el-form-item label="特殊要求">
                    <el-input v-model="tempBook.bookRequement" type="textarea" placeholder="请输入特殊要求"
                        class="add-book-dialog-input"></el-input>
                </el-form-item>
                <el-form-item label="备注">
                    <el-input v-model="tempBook.bookNote" type="textarea" placeholder="请输入备注"
                        class="add-book-dialog-input"></el-input>
                </el-form-item>
            </el-form>

            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="saveAdd"> 确定 </button>
                <button class="dialog-cancel-button" @click="addVisible = false"> 取消 </button>
            </div>
        </el-dialog>

        <el-dialog title="新增点单" v-model="addOrderVisible" width="30%">
            <el-input v-model="keyword" placeholder="请输入关键词" clearable class="add-order-dialog-input"></el-input>

            <div class="add-order-dialog-table-container">
                <el-table :data="dishShow" @selection-change="handleDishSelectionChange" class="add-order-dialog-table"
                    :header-cell-style="{
                        'font-size': '14px',
                        'font-weight': 'lighter',
                        'color': '#ffffff',
                        'background-color': '#196883'
                    }">
                    <el-table-column type="selection" align="center"></el-table-column>
                    <el-table-column prop="dishName" label="菜品名称">
                        <template #default="scope">
                            <label class="add-order-dialog-row-dish-name"> {{ scope.row.dishName }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="dishPrice" label="菜品价格">
                        <template #default="scope">
                            <label class="add-order-dialog-row-dish-price"> {{ scope.row.dishPrice }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="dishTaste" label="菜品口味">
                        <template #default="scope">
                            <label class="add-order-dialog-row-dish-taste"> {{ scope.row.dishTaste }} </label>
                        </template>
                    </el-table-column>
                </el-table>
            </div>

            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="saveOrderAdd"> 确定 </button>
                <button class="dialog-cancel-button" @click="addOrderVisible = false; selectedDish = [];"> 取消 </button>
            </div>
        </el-dialog>

        <el-dialog title="新增顾客" v-model="addCustomerVisible" width="30%">
            <el-form label-width="120px">
                <el-form-item label="姓名">
                    <el-input v-model="newCustomer.name" placeholder="请输入姓名" @input="checkInput"
                        class="add-customer-form-input"></el-input>
                </el-form-item>
                <el-form-item label="身份证">
                    <el-input v-model="newCustomer.id" placeholder="请输入身份证号" @input="checkInput"
                        class="add-customer-form-input"></el-input>
                </el-form-item>
                <el-form-item label="性别">
                    <el-select v-model="newCustomer.gender" placeholder="请选择性别" @change="checkInput"
                        class="add-customer-form-select">
                        <el-option label="男" value="男"></el-option>
                        <el-option label="女" value="女"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="电话">
                    <el-input v-model="newCustomer.phone" placeholder="请输入电话号码" @input="checkInput"
                        class="add-customer-form-input"></el-input>
                </el-form-item>
            </el-form>

            <p v-if="!ifInputPassed" class="dialog-error-message"> {{ errorMessage }} </p>

            <div class="dialog-buttons">
                <button :class="'dialog-confirm-button-' + ifInputPassed.toString()" @click="saveCustomerAdd"> 确定 </button>
                <button class="dialog-cancel-button" @click="addCustomerVisible = false"> 取消 </button>
            </div>
        </el-dialog>

        <el-dialog title="顾客信息" v-model="customerInfoVisible" width="30%">
            <el-form label-width="140">
                <el-form-item label="姓名：">
                    <label class="view-customer-dialog-info"> {{ selectedCustomer.name }} </label>
                </el-form-item>
                <el-form-item label="身份证号：">
                    <label class="view-customer-dialog-info"> {{ selectedCustomer.id }} </label>
                </el-form-item>
                <el-form-item label="性别：">
                    <label class="view-customer-dialog-info"> {{ selectedCustomer.gender }} </label>
                </el-form-item>
                <el-form-item label="电话：">
                    <label class="view-customer-dialog-info"> {{ selectedCustomer.phone }} </label>
                </el-form-item>
                <el-form-item label="信用等级：">
                    <label class="view-customer-dialog-info"> {{ selectedCustomer.creditGrade }} </label>
                </el-form-item>
                <el-form-item label="会员等级：">
                    <label class="view-customer-dialog-info"> {{ selectedCustomer.memberGrade }} </label>
                </el-form-item>
            </el-form>

            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="customerInfoVisible = false"> 确定 </button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import { ElMessage } from 'element-plus';
import { ref, onMounted, onUnmounted } from 'vue';
import { get, del, put, post, getUserInfo } from '../../axios/axiosConfig.js'

export default {
    data() {
        return {
            data: [],                                           // 测试数据
            wantedDate: new Date(),                             // 想要的日期
            firstDate: new Date(),                              // 显示的七天的第一天
            wantedLocation: [],                                 // 桌位要想显示的位置
            wantedStatus: ['空闲', '预定', '使用中', '故障'],     // 桌位要想显示的状态
            selectedTable: { tableId: '' },                       // 选中的桌位
            wantedBookStatus: ['预定成功', '预定失败'],           // 订单状态
            shortcuts: [                                        // 选择日期快捷
                {
                    text: '今天',
                    value: new Date(),
                },
                {
                    text: '昨天',
                    value: () => {
                        const date = new Date();
                        date.setTime(date.getTime() - 3600 * 1000 * 24);
                        return date;
                    }
                },
                {
                    text: '前一周',
                    value: () => {
                        const date = new Date();
                        date.setTime(date.getTime() - 3600 * 1000 * 24 * 7);
                        return date;
                    },
                },
            ],
            options: {                                          // 日期转换选项
                weekday: 'long',
                year: 'numeric',
                month: 'long',
                day: 'numeric'
            },
            tableStatus: [                                      // 桌位状态
                {
                    value: '空闲',
                    label: '空闲',
                    color: '#B4EEB4',
                    fontColor: '#006400',
                },
                {
                    value: '预定',
                    label: '预定',
                    color: '#99CCFF',
                    fontColor: '#000066',
                },
                {
                    value: '使用中',
                    label: '使用中',
                    color: '#FFC0CB',
                    fontColor: '#8B0000',
                },
                {
                    value: '故障',
                    label: '故障',
                    color: '#FFD700',
                    fontColor: '#964B00',
                },
            ],
            table: [],                                          // 桌位数据信息
            dish: [],                                           // 菜品数据信息
            selectedDish: [],                                   // 记录选择的菜品
            book: [],                                           // 预定数据信息
            val: {},                                            // 记录桌位属性中不重复的值
            tempBook: {},                                       // 添加预定的临时变量
            selectedTableOrder: [],                             // 选中桌位的点单情况
            addVisible: false,                                  // 控制添加预定对话框是否可见
            addOrderVisible: false,                             // 控制点单对话框是否可见
            customerInfoVisible: false,                         // 控制顾客信息对话框是否可见
            addCustomerVisible: false,                          // 控制添加顾客对话框是否可见
            isAllBook: false,                                   // 控制是否显示所有预定
            newCustomer: {},                                    // 添加顾客临时变量
            keyword: '',                                        // 搜索菜品关键词
            customerKeyword: '',                                // 顾客顾客关键词
            confirmOrder: [],                                   // 要确定的点单
            customer: [],                                       // 顾客数据信息
            selectedCustomer: {},                               // 记录选中顾客
            ifInputPassed: false,                               // 输入是否正确
            errorMessage: ""                                    // 输入错误信息
        }
    },
    methods: {
        // 餐种英文到中文转换
        meal2Chinese(meal) {
            if (meal === 'breakfast')
                return '早餐';
            else if (meal === 'dinner')
                return '晚餐';
            else
                return '午餐';
        },
        // 禁用昨天之前的日期
        disabledDate(date) {
            let today = new Date();
            today.setDate(today.getDate() - 2);
            return date < today;
        },
        // 判断是否禁用日期
        isDisabledDate(time) {
            return time.getTime() < Date.now() - 8.64e7;
        },
        // 将想要选择的日期往前一天
        backOneDay() {
            let today = new Date();
            let currentYear = today.getFullYear();
            let currentMonth = today.getMonth();
            let currentDay = today.getDate();

            let wantedYear = this.wantedDate.getFullYear();
            let wantedMonth = this.wantedDate.getMonth();
            let wantedDay = this.wantedDate.getDate();

            if (wantedYear === currentYear && wantedMonth === currentMonth && wantedDay === currentDay) {
                ElMessage.info('已经是最早日期辣')
                return;
            }
            this.wantedDate = new Date(this.wantedDate.getTime() - 24 * 60 * 60 * 1000);
            this.firstDate = this.wantedDate;
        },
        // 将想要选择的日期往前一周
        backOneWeek() {
            let nextWeek = new Date();
            nextWeek.setDate(nextWeek.getDate() + 6);
            let nextWeekYear = nextWeek.getFullYear();
            let nextWeekMonth = nextWeek.getMonth();
            let nextWeekDay = nextWeek.getDate();

            let wantedYear = this.wantedDate.getFullYear();
            let wantedMonth = this.wantedDate.getMonth();
            let wantedDay = this.wantedDate.getDate();

            if (!(wantedYear > nextWeekYear || (wantedYear === nextWeekYear && wantedMonth > nextWeekMonth) || (wantedYear === nextWeekYear && wantedMonth === nextWeekMonth && wantedDay > nextWeekDay))) {
                ElMessage.info('不能往前辣');
                return;
            }
            this.wantedDate = new Date(this.wantedDate.getTime() - 7 * 24 * 60 * 60 * 1000);
            this.firstDate = this.wantedDate;
        },
        // 将想要选择的日期往后一周
        forwardOneWeek() {
            this.wantedDate = new Date(this.wantedDate.getTime() + 7 * 24 * 60 * 60 * 1000);
            this.firstDate = this.wantedDate;
        },
        // 选择日期改变想要日期 然后赋值给七天中第一天
        handleDatePickerChange() {
            this.firstDate = this.wantedDate;
        },
        // 获得table中属性的不重复值
        getUniqueValue() {
            if (this.table == [])
                return;
            let ans = {};
            this.table.forEach((item) => {
                for (const key in item) {
                    if (key == 'tableId' || key == 'bookable' || key == 'available')
                        continue;
                    let temp = item[key];
                    if (key == 'tableLocation') {
                        temp = item[key].replace(/[^a-zA-Z]/g, "");
                    } else {
                        temp = item[key];
                    }
                    if (ans.hasOwnProperty(key)) {
                        const allVal = ans[key].map(obj => obj.text);
                        if (!allVal.includes(temp)) {
                            ans[key].push({
                                'text': temp,
                                'value': temp,
                            });
                        }
                    } else {
                        ans[key] = [
                            {
                                'text': temp,
                                'value': temp,
                            }
                        ];
                    }
                }
            });
            return ans;
        },
        // 桌位状态转换为对应背景颜色
        status2Color(status) {
            const statusItem = this.tableStatus.find(item => item.value === status);
            return statusItem ? statusItem.color : 'white';
        },
        // 桌位状态转换为对应字体颜色
        status2FontColor(status) {
            const statusItem = this.tableStatus.find(item => item.value === status);
            return statusItem ? statusItem.fontColor : 'black';
        },
        // 获得某一个桌位的点菜信息
        getOderAndDishInfo(tableId) {
            get(`/api/MyOrder/GetOrderAndDishInfo/${tableId}`, {})
                .then(response => {
                    if (response.status === false) {
                        selectedTable.tableId = '';
                        return;
                    }
                    this.selectedTableOrder = response.data;
                    console.log("selectedTableOrder  //////////////////////数据")
                    console.log(this.selectedTableOrder)



                })
                .catch(error => {
                    this.selectedTableOrder = [];
                    ElMessage.error('无点餐用例');
                    console.log("问题在哪里");
                    console.log(error);
                    return;
                })
                .finally(() => {
                })
        },
        // 处理点单
        handleOrder(tableId) {
            this.selectedTable = this.table.find(table => table.tableId === tableId);
            this.getOderAndDishInfo(tableId);
        },
        // 桌位信息改变 重新获得桌位属性所有值
        tableChange() {
            this.val = this.getUniqueValue();
        },
        // 添加预定
        addBook() {
            this.addVisible = true;

            this.tempBook = {
                tableId: '',
                customerId: '',
                bookTime: new Date(),
                bookNumber: '',
                bookStatus: '预定成功',
                bookRequement: '',
                bookNote: '',
            };
        },
        // 保存预定
        async saveAdd() {
            // 检查输入
            for (let key in this.tempBook) {
                if (this.tempBook[key] === '' && key !== 'bookNote' && key != 'bookrequenment') {
                    ElMessage.warning('请输入完所有信息');
                    return;
                }
            }
            this.addVisible = false;
            // 向后端添加预定
            await post(`/api/Book/Add`, this.tempBook)
                .then(response => {
                    console.log(`add book!`);
                    if (!response) {
                        ElMessage.error('添加失败，请稍后重试');
                        return;
                    }

                    this.addVisible = false;

                    let today = new Date();
                    let todayYyyy = today.getFullYear();
                    let todayMm = today.getMonth();
                    let todayDd = today.getDate();

                    let yyyy = this.tempBook.bookTime.getFullYear();
                    let mm = this.tempBook.bookTime.getMonth();
                    let dd = this.tempBook.bookTime.getDate();
                    // 是今天就修改桌位状态
                    if (yyyy === todayYyyy && mm === todayMm && dd === todayDd) {
                        let table = this.table.find((item) => (item.tableId == this.tempBook.tableId));
                        table.tableStatus = '预定';
                        put('/api/MyTable/Update', {
                            tableId: table.tableId,
                            tableStatus: '预定',
                        })
                            .then(response => {
                                console.log(response);
                                if (!response) {
                                    ElMessage.error('修改桌位状态失败，请稍后重试');
                                    return;
                                }
                                ElMessage.success('修改桌位成功');
                                return;
                            })
                            .catch(error => {
                                console.log(error);
                                ElMessage.error('修改桌位状态失败，请稍后重试');
                                return;
                            })
                    }
                    // 重新获取预定信息
                    get(`/api/Book/GetAll`, {})
                        .then(response => {
                            console.log('get book true');
                            this.book = response.data;
                            if (!this.book) {
                                ElMessage.error('获取预定信息失败，请稍后重试');
                                return;
                            }
                        })
                        .catch(error => {
                            console.log(error);
                            this.book = [];
                            ElMessage.error('获取预定信息失败，请稍后重试');
                            return;
                        })

                    ElMessage.success('添加预定成功');
                })
                .catch(error => {
                    ElMessage.error('添加失败，请稍后重试');
                    console.log(error);
                    return;
                })
        },
        // 删除预定信息    ok
        handleDeleteBook(index, e) {
            ElMessageBox.confirm('确定要删除吗？', '提示', {
                type: 'warning',
                confirmButtonText: '确定',
                cancelButtonText: '取消',
            }).then(() => {
                console.log(this.book[index]);
                del(`/api/Book/Delete/${this.book[index]['tableId']}/${this.book[index]['customerId']}`, {})
                    .then((response) => {
                        console.log(response)
                        if (response.status === false) {
                            ElMessage.error('删除失败，请稍后重试');
                            return;
                        }
                        this.book.splice(index, 1);
                        ElMessage.success('删除成功');
                    })
                    .catch((error) => {
                        console.log(error);
                        return;
                    });
            }).catch(() => { });
            if (e.target.nodeName == "SPAN" || e.target.nodeName == "I") {
                e.target.parentNode.blur();
            } else {
                e.target.blur();
            }
        },
        // 添加点单
        handleAddOrder() {
            // 判断该桌位状态
            if (this.selectedTable.tableStatus === '空闲') {
                this.$confirm('该桌位未开台，是否开启？', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'info',
                }).then(() => {
                    this.selectedTable.tableStatus = '使用中';
                    this.addOrderVisible = true;
                }).catch(() => {
                    ElMessage.info('已取消');
                    return;
                })
            }
            else if (this.selectedTable.tableStatus === '使用中') {
                this.addOrderVisible = true;
            }
            else if (this.selectedTable.tableStatus === '预定') {
                this.$confirm('该桌位为预定桌，是否开启？', '提示', {
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                    type: 'info',
                }).then(() => {
                    this.selectedTable.tableStatus = '使用中';
                    this.addOrderVisible = true;
                }).catch(() => {
                    ElMessage.info('已取消');
                    return;
                })
            }
            else if (this.selectedTable.tableStatus === '故障') {
                ElMessage.warning('该桌故障~');
            }
        },
        // 点单选择菜品
        handleDishSelectionChange(selection) {
            this.selectedDish = selection;
        },
        // 保存添加点单
        saveOrderAdd() {
            if (this.selectedTable.tableId === '') {
                ElMessage.warning('请选择桌位');
                return;
            }
            // 同时添加多个点单
            const promises = this.selectedDish.map(dish => {
                return post(`/api/MyOrder/Add`, {
                    tableId: this.selectedTable.tableId,
                    dishId: dish.dishId,
                    consumptionRecordId:0,
                    orderTime: new Date(),
                    orderStatus: '未确认',
                });
            });

            Promise.all(promises)
                .then(responses => {
                    const isAllSuccess = responses.every(response => response);
                    if (isAllSuccess) {
                        this.addOrderVisible = false;
                        ElMessage.success(`为${this.selectedTable.tableId}桌添加菜品成功`);
                        this.getOderAndDishInfo(this.selectedTable.tableId);
                    } else {
                        ElMessage.error('添加失败，请稍后重试');
                    }
                })
                .catch(error => {
                    ElMessage.error('添加失败，请稍后重试');
                    console.log(error);
                });
        },
        // 选择需要确认的点单
        selectConfirmDish(selection) {
            this.confirmOrder = selection;
        },
        // 点单未确认 可选 点单成功不可选
        orderSelectionCheck(row) {
            return row.orderStatus === '未确认';
        },
        // 确认所选的点单
        handleConfirmDish() {
            if (this.confirmOrder.length == 0) {
                ElMessage.info('请选择至少一样未确定菜品');
                return;
            }
            // 更新多个点单信息
            const promises = this.confirmOrder.map(item => {
                const dishItem = this.dish.find(item1 => item1.dishName === item.dishName);
                return put(`/api/MyOrder/Update`, {
                    tableId: this.selectedTable.tableId,
                    dishId: dishItem.dishId,
                    orderTime: item.orderTime,
                    orderStatus: '点单成功',
                    consumptionRecordId:0
                })
            });
            Promise.all(promises)
                .then(responses => {
                    const isAllSuccess = responses.every(response => response);
                    if (isAllSuccess) {
                        ElMessage.success('确认成功');
                        this.confirmOrder.forEach(item => {
                            item.orderStatus = '点单成功';
                        })
                    } else {
                        ElMessage.error('确认失败，请稍后重试');
                    }
                })
                .catch(error => {
                    ElMessage.error('确认失败，请稍后重试');
                    console.log(error)
                })
        },
        // 处理预定信息改变
        handleBookChange(newBook) {
            console.log(newBook);
            put(`/api/Book/Update`, {
                tableId: newBook.tableId,
                customerId: newBook.customerId,
                bookTime: newBook.bookTime,
                bookStatus: newBook.bookStatus,
            })
                .then(response => {
                    console.log('update book');
                    if (!response) {
                        ElMessage.error('更新失败，请稍后重试');
                        return;
                    }
                    ElMessage.success('更新成功');
                    return;
                })
                .catch(error => {
                    ElMessage.error('更新失败，请稍后重试');
                    console.log(error);
                    return;
                })
        },
        // 挂账
        handlePayOnRoom() {
            if (this.selectedTable === '') {
                return;
            }
            ElMessageBox.prompt('请输入房间号', '提示', {
                confirmButtonText: '确定',
                cancelButtonText: '取消',
                inputPattern: /^\d+$/,
                inputErrorMessage: '请输入有效的房间号',
                callback: (action) => {
                    if (action.action === 'confirm') {
                        if (action.value) {
                            let roomNum = Number(action.value);
                            const dishPK = this.selectedTableOrder.map(item => {
                                const dishItem = this.dish.find(item1 => item1.dishName === item.dishName);
                                return {
                                    tableId: this.selectedTable.tableId,
                                    dishId: dishItem.dishId,
                                    orderTime: item.orderTime
                                }
                            })
                            const userInfo = getUserInfo();
                            console.log({
                                roomNumber: roomNum,
                                consumeAmount: this.sumCost,
                                tableId: this.selectedTable.tableId,
                                orderMessages: dishPK,
                            })
                            post(`/api/MyOrder/AddCateringRecord`, {
                                roomNumber: roomNum,
                                consumeAmount: this.sumCost,
                                    tableId: this.selectedTable.tableId,
                                    orderMessages: dishPK,
                            })
                                .then(response => {
                                    if (!response) {
                                        ElMessage.error('挂账失败，请稍后重试');

                                        return;
                                    }
                                    console.log('pay on room success');
                                    this.selectedTable.tableStatus = '空闲';
                                    this.selectedTableOrder = [];
                                    ElMessage.success('挂账成功');
                                    return;
                                })
                                .catch(error => {
                                    ElMessage.error('挂账失败，请稍后重试');
                                    console.log(error);
                                    return;
                                })
                        } else {
                            ElMessage.info('请输入');
                        }
                    } else {
                        ElMessage.info('已取消');
                    }
                },
            });
        },
        // 时间转换函数
        bookTime2DateStr(date) {
            if (typeof date === 'object' && date instanceof Date) {
                return date.toISOString().split('T')[0];
            }
            return date.split('T')[0];
        },
        bookTime2TimeStr(date) {
            if (typeof date === 'object' && date instanceof Date) {
                let str = date.toISOString().split('T')[1];
                if (str.indexOf('.') != -1)
                    return str.substring(0, str.indexOf('.'));
                return str;
            }
            let str = date.split('T')[1];
            if (str.indexOf('.') != -1)
                return str.substring(0, str.indexOf('.'));
            return str;
        },
        // 顾客id转换为名字
        customerId2Name(customerId) {
            let customerItem = this.customer.find(item => item.customerId == customerId);
            if (customerItem && customerItem.hasOwnProperty('name'))
                return customerItem.name;
            return '陈华机'
        },
        // 顾客id转换为顾客
        customerId2customer(customerId) {
            let ans = this.customer.find(item => item.customerId == customerId);
            return ans;
        },
        // 显示顾客信息
        handleShowCustomerInfo(customerId) {
            this.customerInfoVisible = true;
            this.selectedCustomer = this.customerId2customer(customerId)
        },
        // 添加顾客
        addCustomer() {
            this.newCustomer = {
                name: '',
                id: '',
                gender: '',
                phone: '',
                greditGrade: '',
                memberGrade: '',
            };

            this.checkInput();
            this.addCustomerVisible = true;
        },
        // 检查字符串是否为数字
        checkNumber: function (string) {
            for (let i = 0; i < string.length; i++) {
                if (string[i] < '0' || string[i] > '9')
                    return false;
            }

            return true;
        },
        // 检查添加顾客信息是否正确
        checkInput: function () {
            this.ifInputPassed = false;

            if (this.newCustomer.name == "") {
                this.errorMessage = "名字不能为空";
                return;
            }
            else if (this.newCustomer.id == "") {
                this.errorMessage = "身份证号不能为空";
                return;
            }
            else if (this.newCustomer.id.length != 18) {
                this.errorMessage = "身份证号长度异常";
                return;
            }
            else if (!this.checkNumber(this.newCustomer.id.slice(0, 17))
                || (!this.checkNumber(this.newCustomer.id.slice(17, 18))
                    && this.newCustomer.id.at(17) != "x"
                    && this.newCustomer.id.at(17) != "X")) {
                this.errorMessage = "身份证号格式异常";
                return;
            }
            else if (this.newCustomer.gender == "") {
                this.errorMessage = "性别不能为空";
                return;
            }
            else if (this.newCustomer.phone == "") {
                this.errorMessage = "电话号码不能为空";
                return;
            }
            else if (!this.checkNumber(this.newCustomer.phone)) {
                this.errorMessage = "电话号码必须为数字";
                return;
            }
            else if (this.newCustomer.phone.length > 11) {
                this.errorMessage = "电话号码长度异常";
                return;
            }

            this.ifInputPassed = true;
        },
        // 保存顾客添加
        saveCustomerAdd() {
            if (!this.ifInputPassed) {
                return;
            }

            post(`/api/Customer/Add`, this.newCustomer)
                .then(response => {
                    console.log(`add customer ${response}`);
                    if (!response) {
                        ElMessage.error('新增失败，请稍后重试');
                        return;
                    }
                    this.addCustomerVisible = false;
                    ElMessage.success(`添加顾客${this.newCustomer.name}成功`);
                    get(`/api/Customer/GetAll`, {})
                        .then(response => {
                            console.log(`get customer ${response}`);
                            this.customer = response.data;
                            if (!this.customer) {
                                this.customer = [];
                                ElMessage.error('获取顾客信息失败，请稍后重试');
                                return;
                            }
                        })
                        .catch(error => {
                            console.log(error);
                            this.customer = [];
                            ElMessage.error('获取顾客信息失败，请稍后重试');
                            return;
                        })
                })
                .catch(error => {
                    ElMessage.error('新增失败，请稍后重试');
                    console.log(error);
                    return;
                })
        },
        // 处理选择第一位顾客
        handleCustomerSelectionChange(selection) {
            if (selection.length != 1) {
                ElMessage.warning('请选择一位顾客');
                return;
            }
            this.tempBook.customerId = selection[0].customerId;
        }
    },
    computed: {
        // 转换为日期
        formattedDate() {
            if (this.wantedDate) {
                let formattedDate = this.wantedDate.toISOString().split('T')[0];
                return formattedDate;
            }
            else {
                let today = new Date();
                let formattedDate = today.toISOString().split('T')[0];
                return formattedDate;
            }
        },
        // 从firstDate开始的七天
        dates() {
            let today = this.firstDate;
            let ans = [];
            if (!today) {
                today = new Date();
            }
            for (let i = 0; i < 7; i++) {
                let tempDate = new Date(today.getTime() + i * 24 * 60 * 60 * 1000);
                ans.push(tempDate);
            }
            return ans;

        },
        // 过滤后的桌位信息
        tableShow() {
            let today = new Date();
            let currentYear = today.getFullYear();
            let currentMonth = today.getMonth();
            let currentDay = today.getDate();

            let wantedYear = this.wantedDate.getFullYear();
            let wantedMonth = this.wantedDate.getMonth();
            let wantedDay = this.wantedDate.getDate();
            // 今天就显示数据库中过滤的
            if (wantedYear === currentYear && wantedMonth === currentMonth && wantedDay === currentDay) {
                let ans = this.table.filter(item => {
                    let location = item.tableLocation.replace(/[^a-zA-Z]/g, "");
                    return this.wantedLocation.includes(location)
                        && this.wantedStatus.includes(item.tableStatus);
                });
                return ans;
            } else {
                // 非今天显示预订和故障的
                let tempBookTableId = this.book.filter(item => {
                    let bookDate = null;
                    if (typeof item.bookTime !== 'object' || item.bookTime instanceof Date) {
                        bookDate = new Date(item.bookTime);
                    } else {
                        bookDate = item.bookTime;
                    }
                    let bookYear = bookDate.getFullYear();
                    let bookMonth = bookDate.getMonth();
                    let bookDay = bookDate.getDate();

                    let bookMeal = '';
                    const hour = bookDate.getHours();
                    if (hour >= 17 || hour < 5) {
                        bookMeal = "dinner";
                    } else if (hour >= 5 && hour < 11) {
                        bookMeal = "breakfast";
                    } else if (hour >= 11 && hour < 16) {
                        bookMeal = "lunch";
                    }

                    return bookYear === wantedYear
                        && bookMonth === wantedMonth
                        && bookDay === wantedDay
                        && bookMeal === this.meal
                        && item.bookStatus === '预定成功';
                }).map(item => item.tableId);

                let ans = JSON.parse(JSON.stringify(this.table));
                ans.forEach(item => {
                    if (item.tableStatus !== '故障') {
                        if (tempBookTableId.includes(item.tableId))
                            item.tableStatus = '预定';
                        else
                            item.tableStatus = '空闲';
                    }

                });
                return ans
            }
        },
        // 查找空闲桌位
        tableFree() {
            let ans = this.table.filter((item) => {
                return item.tableStatus === '空闲';
            });
            return ans;
        },
        // 计算各个桌位状态个数
        numOfStatus() {
            let ans = this.tableStatus.reduce((acc, status) => {
                acc[status.value] = 0;
                return acc;
            }, {});
            for (let table of this.table) {
                ans[table.tableStatus]++;
            }
            return ans;
        },
        // 根据关键词显示菜品
        dishShow() {
            if (this.keyword === '')
                return this.dish;
            const ans = this.dish.filter((item) => item['dishName'].includes(this.keyword));
            return ans;
        },
        // 计算所选桌位的总金额
        sumCost() {
            let ans = 0;
            this.selectedTableOrder.forEach(item => {
                if (item.orderStatus === '点单成功') {
                    ans += item.dishPrice;
                }
            });
            return ans;
        },
        // 根据关键词显示顾客
        customerShow() {
            if (this.customerKeyword === '')
                return this.customer;
            let ans = this.customer.filter(item => {
                if (item.name)
                    return item.name.includes(this.customerKeyword);
                return true;
            })
            return ans;
        },
        // 显示是否所有预定
        bookShow() {
            if (this.isAllBook) {
                return this.book;
            } else {
                return this.book.filter(item => {
                    let inputDate = null;
                    if (typeof item.bookTime !== 'object' || item.bookTime instanceof Date) {
                        inputDate = new Date(item.bookTime);
                    } else {
                        inputDate = item.bookTime;
                    }
                    let currentDate = new Date();
                    let inputDateOnly = new Date(inputDate.getFullYear(), inputDate.getMonth(), inputDate.getDate());
                    let currentDateOnly = new Date(currentDate.getFullYear(), currentDate.getMonth(), currentDate.getDate());
                    return inputDateOnly >= currentDateOnly;
                })
            }
        }
    },
    // 监视选中桌位的改变
    watch: {
        'selectedTable.tableStatus': {
            deep: false,
            immediate: false,
            handler(newValue, oldValue) {
                put('/api/MyTable/Update', this.selectedTable)
                    .then((response) => {
                        console.log(`update table ${response}`);
                        if (!response) {
                            ElMessage.error('更新失败，请稍后重试');
                            return;
                        }
                    })
                    .catch((error) => {
                        ElMessage.error('更新失败，请稍后重试');
                        console.log(error);
                        return;
                    });
            }
        },
        'selectedTable.bookable': {
            deep: false,
            immediate: false,
            handler(newValue, oldValue) {
                put('/api/MyTable/Update', this.selectedTable)
                    .then((response) => {
                        console.log(`update table ${response}`);
                        if (!response) {
                            ElMessage.error('更新失败，请稍后重试');
                            return;
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                        ElMessage.error('更新失败，请稍后重试');
                        return;
                    });
            }
        },
        'selectedTable.available': {
            deep: false,
            immediate: false,
            handler(newValue, oldValue) {
                put('/api/MyTable/Update', this.selectedTable)
                    .then((response) => {
                        console.log(`update table ${response}`);
                        if (!response) {
                            ElMessage.error('更新失败，请稍后重试');
                            return;
                        }
                    })
                    .catch((error) => {
                        console.log(error);
                        ElMessage.error('更新失败，请稍后重试');
                        return;
                    });
            }
        }
    },
    mounted() {
        // 挂载就获取信息
        get(`/api/MyTable/GetAll`, {})
            .then(response => {
                console.log('get table true');
                this.table = response.data;
                console.log(6666666666666666)
                console.log(response.data)
                if (!this.table) {
                    this.table = [];
                    ElMessage.error('获取桌位信息失败，请稍后重试');
                    return;
                }
            })
            .catch(error => {
                console.log(error);
                this.table = [];
                ElMessage.error('获取桌位信息失败，请稍后重试');
                return;
            })
            .finally(() => {
                this.val = this.getUniqueValue();
                if (this.val.hasOwnProperty('tableLocation')) {
                    this.wantedLocation = this.val.tableLocation.map(item => item.value);
                }
            });
        get(`/api/Dish/GetAllDishes`, {})
            .then(response => {
                console.log('get dish true');
                this.dish = response.data;
                if (!this.dish) {
                    this.dish = [];
                    ElMessage.error('获取菜品信息失败，请稍后重试');
                    return;
                }
            })
            .catch(error => {
                console.log(error);
                this.dish = [];
                ElMessage.error('获取菜品信息失败，请稍后重试');
                return;
            })
        get(`/api/Book/GetAll`, {})
            .then(response => {
                console.log('get book true');
                this.book = response.data;
                if (!this.book) {
                    ElMessage.error('获取预定信息失败，请稍后重试');
                    return;
                }
            })
            .catch(error => {
                console.log(error);
                this.book = [];
                ElMessage.error('获取预定信息失败，请稍后重试');
                return;
            })
        get(`/api/Customer/GetAll`, {})
            .then(response => {
                console.log('get customer true');
                this.customer = response.data;
                if (!this.customer) {
                    ElMessage.error('获取预定顾客失败，请稍后重试');
                    return;
                }
            })
            .catch(error => {
                console.log(error);
                this.customer = [];
                ElMessage.error('获取预定顾客失败，请稍后重试');
                return;
            })
        this.wantedDate = new Date();
    },
    setup() {
        const currentDate = ref(new Date());
        const meal = ref('no meal');

        const updateDate = () => {
            currentDate.value = new Date();
        }

        // update every 60s
        onMounted(() => {
            updateDate();
            setInterval(() => {
                const now = new Date();
                if (now.getDate() !== currentDate.value.getDate()) {
                    updateDate;
                }
            }, 60000);

            // 判断餐种
            const hour = currentDate.value.getHours();
            if (hour >= 17 || hour < 5) {
                meal.value = "dinner";
            } else if (hour >= 5 && hour < 11) {
                meal.value = "breakfast";
            } else if (hour >= 11 && hour < 16) {
                meal.value = "lunch";
            }
        });

        onUnmounted(() => {
            clearInterval();
        });

        return {
            currentDate,
            meal,
        };
    },

}
</script>

<style scoped>
.order-dishes {
    padding: 20px;
    background-image: linear-gradient(-45deg, #97cba7, #9ab7e3);
    height: 94vh;
}

/* ------------------------------------------ header ------------------------------------------------- */
.order-header {
    display: flex;
    height: 120px;
    background-color: #196883a0;
    position: relative;
    border-radius: 15px;
    width: 100%;
}

.header-picker-container {
    margin-top: 40px;
    width: 21%;
}

.header-picker {
    width: 100%;
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

.date-back {
    margin-top: 42px;
}

.date-back-button {
    height: 40px;
    width: 100%;
    border: 2px solid white;
    border-radius: 30px;
    background-color: #196883;
    margin-bottom: 5px;
}

.date-back-button:hover {
    background-color: #2aba98;
}

.btn-row {
    display: flex;
    flex-direction: row;
    margin-top: 10px;
    margin-left: 4%;
    background-color: #ffffffa0;
    width: 50%;
    height: 100px;
    border-radius: 15px;
}

.one-btn {
    display: flex;
    flex-direction: column;
    width: 100px;
    height: 100px;
}

.one-btn:hover {
    background-color: #196883b0;
}

.one-btn:hover .date-btn {
    color: white;
}

.breakfast,
.lunch,
.dinner {
    color: white;
}

.breakfast:hover,
.lunch:hover,
.dinner:hover {
    color: black;
}

.date-btn {
    width: 94px;
    border: 0;
    font-size: 18px;
    background-color: #ffffff00;
}

.date-btn:hover {
    color: #196883;
}

.right-column-btn {
    display: flex;
    flex-direction: column;
    margin: 0px;
}

.breakfast,
.lunch,
.dinner {
    margin: 0;
    border-radius: 5px;
    font-size: 16px;
    font-weight: lighter;
    height: 20px;
    width: 80px;
    white-space: nowrap;
    border: 0;
    background-color: #ffffff00;
}

.week-buttons {
    display: flex;
    flex-direction: column;
    margin-left: 4%;
    margin-top: 20px;
    width: 8.5%;
}

.week-back-button {
    height: 40px;
    border: 2px solid white;
    border-radius: 30px;
    background-color: #196883;
    margin-bottom: 5px;
}

.week-back-button:hover {
    background-color: #2aba98;
}

.week-forward-button {
    margin: 0;
    height: 40px;
    border: 2px solid white;
    border-radius: 30px;
    background-color: #198359;
}

.week-forward-button:hover {
    background-color: #9fd44b;
}

::v-deep .el-button {
    color: white;
    font-size: large;
    font-weight: lighter;
}

.order-main {
    display: flex;
    flex-direction: row;
}

/* ---------------------------------------- header end ----------------------------------------------- */
/* ---------------------------------------- table card ----------------------------------------------- */
.table-card {
    height: 80vh;
    max-width: 33.33%;
    min-width: 33.33%;
    overflow: auto;
    background-color: rgb(255, 255, 255, 0.8);
}

.table-card-header {
    display: flex;
    flex-wrap: wrap;
}

.table-card-status-container {
    padding: 10px;
    margin-left: 5px;
    margin-bottom: 5px;
    border-radius: 10px;
    width: 40%;
}

.table-card-status-label {
    font-size: larger;
    font-weight: normal;
}

.table-card-select-container {
    width: 100%;
    margin-top: 10px;
}

::v-deep .table-card-select-container .el-form-item {
    margin-top: 10px;
    margin-bottom: 10px;
}

::v-deep .table-card-select-container .el-form-item__label {
    font-size: large;
    font-weight: lighter;
    color: #196883;
}

.table-card-select {
    width: 90%;
}

::v-deep .table-card-select-container .el-input__wrapper {
    background-color: rgb(255, 255, 255, 0.5);
    height: 30px;
    padding-left: 10px;
}

::v-deep .table-card-select-container .el-input__wrapper.is-focus {
    border: 2px solid #2aba98;
}

::v-deep .table-card-select-container .el-input__inner {
    font-size: medium;
}

::v-deep .table-card-select-container .el-select {
    --el-select-input-focus-border-color: #2aba98;
}

::v-deep .table-card-select-container .el-select .el-select__caret {
    font-size: 15px;
    color: #324157;
}

.table-card-meal-info {
    margin-top: 10px;
}

.table-card-meal-info label {
    margin-right: 20px;
    font-size: x-large;
    color: #196883;
}

.table-card-table-list {
    display: flex;
    flex-wrap: wrap;
    width: 100%;
}

.table-card-table {
    width: 22%;
    height: 100%;
    margin-top: 1%;
    margin-left: 3%;
    padding: 5px;
    border: 2px solid white;
    box-sizing: border-box;
    border-radius: 20px;
}

.table-card-table:hover {
    border-color: #196883;
    box-shadow: 0 0 0.8em 0 #196883a0;
}

/* -------------------------------------- table card end --------------------------------------------- */
/* ---------------------------------------- book card ------------------------------------------------ */
.book-card {
    height: 80vh;
    max-width: 33.33%;
    min-width: 33.33%;
    overflow: auto;
    background-color: rgb(255, 255, 255, 0.8);
}

.add-button {
    width: 50px;
    height: 50px;
    border: 2px solid white;
    border-radius: 50px;
    background-color: #198359;
    float: right;
}

.add-button:hover {
    background-color: #9fd44b;
    box-shadow: 0 1em 1em -1em #0e3d26;
}

.book-card-switch {
    float: right;
    margin-right: 50px;
    margin-top: 30px;
}

::v-deep .el-switch__label--left span {
    font-size: large;
}

::v-deep .el-switch__label--right span {
    font-size: large;
}

::v-deep .el-switch__label--left.is-active {
    color: #196883;
}

::v-deep .el-switch__label--right.is-active {
    color: #196883;
}

.book-card-table-container {
    margin-top: 30px;
    padding: 5px;
    background-color: #196883;
    border-radius: 15px;
}

.book-card-table {
    width: 100%;
    height: 570px;
    background: #196883;
    border-radius: 5px;
}

::v-deep .el-table td {
    background-color: #73bfc3;
    padding: 10px;
}

::v-deep .el-table td,
.building-top .el-table th.is-leaf {
    border-bottom: 5px solid #196883;
}

.book-card-row-table-id {
    color: #196883;
}

.book-card-row-customer-id {
    color: #196883;
}

.book-card-row-customer-id:hover {
    color: #a76200;
    cursor: pointer;
}

.book-card-row-book-time {
    color: #196883;
}

.book-card-row-num {
    color: #196883;
}

.book-card-row-book-status {
    width: 100%;
}

::v-deep .book-card-row-book-status .el-input__wrapper {
    background-color: rgb(255, 255, 255, 0.5);
    height: 30px;
    padding-left: 10px;
}

::v-deep .book-card-row-book-status .el-input__wrapper.is-focus {
    border: 2px solid #2aba98;
}

::v-deep .book-card-row-book-status .el-input__inner {
    font-size: medium;
}

::v-deep .book-card-row-book-status .el-select {
    --el-select-input-focus-border-color: #2aba98;
}

::v-deep .book-card-row-book-status .el-select .el-select__caret {
    font-size: 15px;
    color: #324157;
}

.book-card-row-special-demand {
    color: #196883;
}

.book-card-row-note {
    color: #196883;
}

.delete-button {
    width: 100%;
    height: 32px;
    border: 2px solid #c1cbd9;
    background-color: #b02a08;
}

.delete-button:hover {
    background-color: #ff7350;
}

/* -------------------------------------- book card end ---------------------------------------------- */
/* ---------------------------------------- order card ----------------------------------------------- */
.order-card {
    height: 80vh;
    max-width: 33.33%;
    min-width: 33.33%;
    overflow: auto;
    background-color: rgb(255, 255, 255, 0.8);
}

::v-deep .el-switch {
    --el-switch-on-color: #196883;
    --el-switch-off-color: #828992;
}

::v-deep .el-form-item {
    margin-bottom: 20px;
}

::v-deep .el-form-item__label {
    font-size: x-large;
    font-weight: lighter;
    color: #196883;
}

::v-deep .order-card-select-container .el-input__wrapper {
    background-color: rgb(255, 255, 255, 0.5);
    height: 30px;
    padding-left: 10px;
}

::v-deep .order-card-select-container .el-input__wrapper.is-focus {
    border: 2px solid #2aba98;
}

::v-deep .order-card-select-container .el-input__inner {
    font-size: medium;
}

::v-deep .order-card-select-container .el-select {
    --el-select-input-focus-border-color: #2aba98;
}

::v-deep .order-card-select-container .el-select .el-select__caret {
    font-size: 15px;
    color: #324157;
}

.order-card-button {
    background: #00929b;
    border: 0;
    transform: translateY(0);
}

.order-card-button:hover {
    background: #44adde;
    color: #fff;
    box-shadow: 0 0 0.8em 0 #44addecc;
}

.order-card-table-container {
    background-color: #196883;
    padding: 5px;
    border-radius: 15px;
}

.order-card-table {
    width: 100%;
    height: 470px;
    background: #196883;
    border-radius: 5px;
}

.order-card-row-dish-name {
    color: #196883;
}

.order-card-row-dish-price {
    color: #196883;
}

.order-card-row-order-time {
    color: #196883;
}

.order-card-row-order-status {
    color: #196883;
}

/* -------------------------------------- order card end --------------------------------------------- */
/* -------------------------------------- add-book-dialog -------------------------------------------- */
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

::v-deep .add-book-dialog-form .el-form-item {
    margin-top: 40px;
}

.add-book-dialog-input {
    width: 75%;
}

.add-book-dialog-select {
    width: 75%;
}

.add-book-dialog-add-button {
    margin-left: 20px;
    width: 40px;
    height: 40px;
    border-radius: 40px;
    border: 2px solid white;
    background-color: #198359;
}

.add-book-dialog-add-button:hover {
    background-color: #9fd44b;
    box-shadow: 0 1em 1em -1em #0e3d26;
}

.add-book-dialog-table-container {
    margin-top: 10px;
    width: 100%;
    background-color: #196883;
    padding: 5px;
    border-radius: 15px;
}

.add-book-dialog-table {
    width: 100%;
    height: 250px;
    background: #196883;
    border-radius: 5px;
}

::v-deep .el-date-editor {
    --el-date-editor-width: 75%;
}

.add-book-dialog-input-number {
    width: 75%;
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

::v-deep .el-textarea {
    --el-input-border-color: rgb(212, 212, 212);
    --el-input-hover-border-color: #324157;
    --el-input-focus-border-color: white;
    --el-input-placeholder-color: #757575;
}

::v-deep .el-textarea__inner {
    border: 2px solid #324157;
    border-radius: 15px;
    background-color: rgb(255, 255, 255, 0.5);
    height: 50px;
    padding-left: 20px;
    font-size: larger;
    color: #196883;
}

::v-deep .el-textarea__inner:focus {
    border: 2px solid white;
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

/* ------------------------------------ add-book-dialog end ------------------------------------------ */
/* ------------------------------------- add-order-dialog -------------------------------------------- */
.add-order-dialog-input {
    margin-left: 12.5%;
    width: 75%;
    margin-bottom: 30px;
}

.add-order-dialog-table-container {
    margin-top: 10px;
    width: 100%;
    background-color: #196883;
    padding: 5px;
    border-radius: 15px;
}

.add-order-dialog-table {
    width: 100%;
    height: 400px;
    background: #196883;
    border-radius: 5px;
}

/* --------------------------------------- add-order-dialog end -------------------------------------- */
/* --------------------------------------- add-customer-dialog --------------------------------------- */
.add-customer-form-input {
    width: 85%;
}

.add-customer-form-select {
    width: 85%;
}

.dialog-error-message {
    height: 10px;
    display: flex;
    justify-content: center;
    font-size: medium;
    color: rgb(159, 86, 86);
}

.dialog-confirm-button-true {
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

.dialog-confirm-button-true:hover {
    background-color: #ccffc3;
}

.dialog-confirm-button-false {
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

/* ------------------------------------- add-customer-dialog end ------------------------------------- */
/* -------------------------------------- view-customer-dialog --------------------------------------- */
.view-customer-dialog-info {
    font-size: large;
    color: #324157;
}

/* ------------------------------------ view-customer-dialog end ------------------------------------- */</style>