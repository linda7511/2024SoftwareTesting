<template>
    <div class="salary-manage">
        <img src="../../assets/personnel.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 人事管理 </label>
            <label class="title-content"> 工资管理 </label>
        </div>

        <!--搜索栏-->
        <div class="search-container">
            <el-select v-model="searchBy" placeholder="请选择查询方式" class="search-select">
                <el-option label="员工号" value="employeeId"></el-option>
                <el-option label="员工姓名" value="name"></el-option>
            </el-select>
            <el-input
                v-if="searchBy == 'employeeId'"
                v-model="searchId"
                placeholder="输入员工号查询"
                clearable
                @clear="clearSearch"
                class="search-input"
            ></el-input>
            <el-input
                v-else-if="searchBy == 'name'"
                v-model="searchName"
                placeholder="输入员工姓名查询"
                clearable
                @clear="clearSearch"
                class="search-input"
            ></el-input>
        </div>

        <!--员工工资信息栏-->
        <div class="table-container">
            <el-table :data="filteredEmployees" :row-class-name="tableRowClassName" class="table" :header-cell-style="{
                'padding': '20px',
                'font-size': 'x-large',
                'font-weight': 'lighter',
                'color': '#ffffff',
                'background-color': '#196883'
            }">
                <el-table-column prop="employeeId" label="员工ID">
                    <template #default="scope">
                        <label class="left-row-employee-id"> {{ scope.row.employeeId }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="name" label="员工姓名">
                    <template #default="scope">
                        <label class="left-row-name"> {{ scope.row.name }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="departmentName" label="所属部门" column-key="department"
                    :filters="[
                        {text: '餐饮', value: '餐饮' },
                        {text: '财务', value: '财务' },
                        {text: '人事', value: '人事' },
                        {text: '后勤', value: '后勤' },
                        {text: '前厅部', value: '前厅部' },
                        {text: '总经理办公室', value: '总经理办公室' },
                    ]"
                    :filter-method="filterHandler">
                    <template #default="scope">
                        <label class="left-row-department-name"> {{ scope.row.departmentName }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="PostName" label="岗位名称">
                    <template #default="scope">
                        <label class="left-row-post-name"> {{ scope.row.postName }} </label>
                    </template>
                </el-table-column>
                <el-table-column label="操作" align="center">
                    <template #default="scope">
                        <el-button icon="view" @click="viewSalaries(scope.row.employeeId)" class="view-button"> 查看历史 </el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>

        <!-- 岗位工资栏，可查看、修改 -->
        <div class="table-container">
            <el-table :data="posts" class="table" :header-cell-style="{
                'padding': '20px',
                'font-size': 'x-large',
                'font-weight': 'lighter',
                'color': '#ffffff',
                'background-color': '#196883'
            }">
                <el-table-column prop="postName" label="岗位名称">
                    <template #default="scope">
                        <label class="right-row-post-name"> {{ scope.row.postName }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="DepartmentName" label="所属部门" column-key="department"
                    :filters="[
                            {text: '餐饮', value: '餐饮' },
                            {text: '财务', value: '财务' },
                            {text: '人事', value: '人事' },
                            {text: '后勤', value: '后勤' },
                            {text: '前厅部', value: '前厅部' },
                            {text: '总经理办公室', value: '总经理办公室' },
                        ]"
                    :filter-method="filterHandler">
                    <template #default="scope">
                        <label class="right-row-department-name"> {{ scope.row.departmentName }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="NumberOfPeople" label="部门人数">
                    <template #default="scope">
                        <label class="right-row-number-of-people"> {{ scope.row.numberOfPeople }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="PositionLevel" label="岗位级别">
                    <template #default="scope">
                        <label class="right-row-position-level"> {{ scope.row.positionLevel }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="positionSalary" label="月薪/￥" sortable>
                    <template #default="scope">
                        <div>
                            <template v-if="editingRow === scope.row">
                                <!--按下回车或输入框失去焦点时提交修改-->
                                <el-input
                                    v-model="editingRow.positionSalary"
                                    @keyup.enter="handleKeyup"
                                    @blur="checkAndConfirmEdit"
                                    ref="editInput"
                                    class="right-row-input"
                                ></el-input>
                            </template>
                            <template v-else>
                                <label class="right-row-position-salary"> {{ scope.row.positionSalary }} </label>
                                <el-button
                                    :icon="Edit"
                                    style="padding: 0px;border: 0px;"
                                    @click="startEditing(scope.row)"
                                    class="right-row-edit-button"
                                ></el-button>
                            </template>
                        </div>
                    </template>
                </el-table-column>
            </el-table>
        </div>

        <!--展示员工历史工资记录，可以新增、修改、删除记录-->
        <el-dialog v-model="salaryDialogVisible" title="历史工资" width="100%">
            <p> 员工ID: {{ currentEmployeeId }} </p>

            <div class="dialog-table-container">
                <el-table :data="salaryRecords" class="dialog-table" :header-cell-style="{
                    'padding': '20px',
                    'font-size': 'medium',
                    'font-weight': 'lighter',
                    'color': '#ffffff',
                    'background-color': '#196883'
                }">
                    <el-table-column label="工资ID" prop="salaryId">
                        <template #default="scope">
                            <label class="dialog-row-salary-id"> {{ scope.row.salaryId }} </label>
                        </template>
                    </el-table-column>
                 
                    <el-table-column label="年" prop="year" column-key="year"
                        :filters="availableYears"
                        :filter-method="filterHandler">
                        <template #default="scope">
                            <label class="dialog-row-year"> {{ scope.row.year }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="月" prop="month" column-key="month"
                        :filters="availableMonths"
                        :filter-method="filterHandler">
                        <template #default="scope">
                            <label class="dialog-row-month"> {{ scope.row.month }} </label>
                        </template>
                    </el-table-column>
                  
                    <el-table-column label="节假日福利" prop="holidayAllowance">
                        <template #default="scope">
                            <label class="dialog-row-holiday-allowance"> {{ scope.row.holidayAllowance }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="补贴金额" prop="bonus">
                        <template #default="scope">
                            <label class="dialog-row-bonus"> {{ scope.row.bonus }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="加班费" prop="overtimePay">
                        <template #default="scope">
                            <label class="dialog-row-overtime-pay"> {{ scope.row.overtimePay }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="提成" prop="commission">
                        <template #default="scope">
                            <label class="dialog-row-commission"> {{ scope.row.commission }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="年终奖" prop="yearEndBonus">
                        <template #default="scope">
                            <label class="dialog-row-year-end-bonus"> {{ scope.row.yearEndBonus }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="其他奖励" prop="otherAllowance">
                        <template #default="scope">
                            <label class="dialog-row-other-allowance"> {{ scope.row.otherAllowance }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="奖励类型" prop="rewardType">
                        <template #default="scope">
                            <label class="dialog-row-reward-type"> {{ scope.row.rewardType }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="奖励金额" prop="rewardAmount">
                        <template #default="scope">
                            <label class="dialog-row-reward-amount"> {{ scope.row.rewardAmount }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="迟到扣款" prop="lateDeduction">
                        <template #default="scope">
                            <label class="dialog-row-late-deduction"> {{ scope.row.lateDeduction }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="早退扣款" prop="earlyDepartureDeduction">
                        <template #default="scope">
                            <label class="dialog-row-early-departure-deduction"> {{ scope.row.earlyDepartureDeduction }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="个人所得税" prop="incomeTax">
                        <template #default="scope">
                            <label class="dialog-row-income-tax"> {{ scope.row.incomeTax }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="五险一金" prop="socialInsurance">
                        <template #default="scope">
                            <label class="dialog-row-social-insurance"> {{ scope.row.socialInsurance }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="应发工资" prop="grossSalary">
                        <template #default="scope">
                            <label class="dialog-row-gross-salary"> {{ scope.row.grossSalary }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="实发工资" prop="netSalary">
                        <template #default="scope">
                            <label class="dialog-row-net-salary"> {{ scope.row.netSalary }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="操作" width="180" fixed="right">
                        <template #default="scope">
                            <div style="display: flex;justify-content: center;margin:0px;">
                                <el-button icon="Edit" @click="openEditSalaryDialog(scope.row)" class="dialog-row-edit-button"> 修改 </el-button>
                                <el-button icon="Delete" @click="deleteSalary(scope.row)" class="dialog-row-delete-button"> 删除 </el-button>
                            </div>
                        </template>
                    </el-table-column>
                </el-table>
            </div>

            <div class="dialog-buttons">
                <el-button type="primary" icon="Plus" class="dialog-add-button" @click="showAddDialog"> 新增 </el-button>
            </div>
        </el-dialog>

        <!-- 弹出的对话框，用于新增工资信息 -->
        <el-dialog v-model="addDialogVisible" title="新增工资记录" width="65%" >
            <el-form inline="true" :model="newSalary" :rules="addRules" ref="newSalaryForm" label-width="150px">
                <!-- 表单项，包括工资信息字段 -->
                <el-form-item label="年" prop="year" required>
                    <el-input v-model.number="newSalary.year" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="月" prop="month" required>
                    <el-input v-model.number="newSalary.month" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="奖金" prop="bonus" required>
                    <el-input v-model.number="newSalary.bonus" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="节假日福利" prop="holidayAllowance" required>
                    <el-input v-model.number="newSalary.holidayAllowance" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="其他奖励" prop="otherAllowance" required>
                    <el-input v-model.number="newSalary.otherAllowance" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="提成" prop="commission" required>
                    <el-input v-model.number="newSalary.commission" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="年终奖" prop="yearEndBonus" required>
                    <el-input v-model.number="newSalary.yearEndBonus" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="加班费" prop="overtimePay" required>
                    <el-input v-model.number="newSalary.overtimePay" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="奖励类型" prop="rewardType" required>
                    <el-input v-model="newSalary.rewardType" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="奖励金额" prop="rewardAmount" required>
                    <el-input v-model.number="newSalary.rewardAmount" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="迟到扣款" prop="lateDeduction" required>
                    <el-input v-model.number="newSalary.lateDeduction" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="早退扣款" prop="earlyDepartureDeduction" required>
                    <el-input v-model.number="newSalary.earlyDepartureDeduction" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="个人所得税" prop="incomeTax" required>
                    <el-input v-model.number="newSalary.incomeTax" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="五险一金" prop="socialInsurance" required>
                    <el-input v-model.number="newSalary.socialInsurance" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="应发工资" prop="grossSalary" required>
                    <el-input v-model.number="newSalary.grossSalary" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="实发工资" prop="netSalary" required>
                    <el-input v-model.number="newSalary.netSalary" class="dialog-form-input"></el-input>
                </el-form-item>
            </el-form>

            <div class="dialog-buttons">
                <button class="dialog-save-button" @click="saveNewSalary"> 保存 </button>
                <button class="dialog-cancel-button" @click="addDialogVisible = false"> 取消 </button>
            </div>
        </el-dialog>

        <!-- 弹出的对话框，用于修改工资信息 -->
        <el-dialog v-model="editDialogVisible" title="修改工资记录" width="65%">
            <el-form inline="true" :model="editSalary" ref="newSalaryForm" label-width="150px">
                <!-- 表单项，包括工资信息字段 -->
                <el-form-item label="年" prop="year" required>
                    <el-input v-model="editSalary.year" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="月" prop="month" required>
                    <el-input v-model="editSalary.month" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="节假日福利" prop="holidayAllowance" required>
                    <el-input v-model="editSalary.holidayAllowance" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="补贴金额" prop="bonus" required>
                    <el-input v-model="editSalary.bonus" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="加班费" prop="overtimePay" required>
                    <el-input v-model="editSalary.overtimePay" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="提成" prop="commission" required>
                    <el-input v-model="editSalary.commission" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="年终奖" prop="yearEndBonus" required>
                    <el-input v-model="editSalary.yearEndBonus" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="其他奖励" prop="otherAllowance" required>
                    <el-input v-model="editSalary.otherAllowance" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="奖励类型" prop="rewardType" required>
                    <el-input v-model="editSalary.rewardType" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="奖励金额" prop="rewardAmount" required>
                    <el-input v-model="editSalary.rewardAmount" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="迟到扣款" prop="lateDeduction" required>
                    <el-input v-model="editSalary.lateDeduction" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="早退扣款" prop="earlyDepartureDeduction" required>
                    <el-input v-model="editSalary.earlyDepartureDeduction" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="个人所得说" prop="incomeTax" required>
                    <el-input v-model="editSalary.incomeTax" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="五险一金" prop="socialInsurance" required>
                    <el-input v-model="editSalary.socialInsurance" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="应发工资" prop="grossSalary" required>
                    <el-input v-model="editSalary.grossSalary" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="实发工资" prop="netSalary" required>
                    <el-input v-model="editSalary.netSalary" class="dialog-form-input"></el-input>
                </el-form-item>
            </el-form>

            <div class="dialog-buttons">
                <button class="dialog-save-button" @click="saveEditSalary"> 保存 </button>
                <button class="dialog-cancel-button" @click="editDialogVisible = false"> 取消 </button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import { post,put,del,get } from '../../axios/axiosConfig.js'
import { getSalaryInfo,delSalary } from '../../axios/salary.js';
import {Edit,Delete,Plus} from '@element-plus/icons-vue'
import { fetchEmployeeData,fetchDepartmentData } from '../../axios/job_scheduling.js'
import { ElMessage } from 'element-plus';
    export default {
        data() {
            return {
                searchBy:'employeeId',//查询方式
                searchId: '',
                searchName:'',
                /*员工工资信息*/
                employees: [],
                /*员工历史工资记录*/ 
                salaryRecords: [],
                /*可选择的年月列表*/
                availableYears: [{text: '2021', value: '2021' },{text: '2022', value: '2022' },
                    {text: '2023', value: '2023' }], 
                availableMonths: Array.from({ length: 12 }, (_, index) => ({
                    text: (index + 1).toString(),
                    value: (index + 1).toString(),
                    })),
                /*可根据员工状态添加行底色*/
                tableRowClassName:'',
                searchId: "",
                currentEmployeeId:'',
                salaryDialogVisible:false,//员工历史工资记录对话框是否显示
                addDialogVisible: false, // 新增工资记录对话框是否显示
                editDialogVisible : false,
                newSalary:{
                    "employeeId": this.currentEmployeeId,
                    "year": null,
                    "month": null,
                    "bonus": null,
                    "holidayAllowance": null,
                    "otherAllowance": null,
                    "commission": null,
                    "yearEndBonus": null,
                    "overtimePay": null,
                    "rewardType": "",
                    "rewardAmount": null,
                    "lateDeduction": null,
                    "earlyDepartureDeduction": null,
                    "incomeTax": null,
                    "socialInsurance": null,
                    "grossSalary": null,
                    "netSalary": null
                }, // 新增工资记录数据
                addRules:{
                    employeeID:[{ required: true, message: '请输入正确的员工id', trigger: 'blur',pattern:/^[0-9]*$/ }],
                    Year: [
                    { required: true, message: '请输入正确的年份', trigger: 'blur' },
                    {
                        validator: this.validateYear,
                    },
                    ],
                    Month: [
                    { required: true, message: '请输入正确的月份', trigger: 'blur' },
                    {
                        validator: this.validateMonth,
                    },
                    ], 
                    Bonus: [{ required: true, message: '请输入正确的奖金', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    HolidayAllowance: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    OtherAllowance: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    Commission: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    YearEndBonus: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    OvertimePay: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    RewardType:[{ required: true, message: '请输入奖励类型', trigger: 'blur' }],
                    RewardAmount: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    LateDeduction: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    EarlyDepartureDeduction: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    IncomeTax: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    SocialInsurance: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    GrossSalary: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                    NetSalary: [{ required: true, message: '请输入正确的数字', trigger: 'blur',pattern:/^[0-9]+([.]{1}[0-9]+){0,1}$/ }],
                },
                editSalary:{
                    
                },
                /*岗位薪资信息*/
                posts:[],
                Edit,//返回岗位薪资表要用到的button图标
                editingRow: null,
                originalSalary: null,
            };
        },
        computed: {
            filteredEmployees() {
                if(this.searchBy==='employeeId'){
                    if (this.searchId) {
                        return this.employees.filter(employee => employee.employeeId.toString().includes(this.searchId));
                    }
                }
                else if(this.searchBy==='name'){
                    if (this.searchName) {
                        return this.employees.filter(employee => employee.name.includes(this.searchName));
                    }
                }
                return this.employees;
            },
        },
        async mounted() {
            // 从后端获取employees,posts数据
            this.employees=await fetchEmployeeData();
            console.log(9999999999)
            console.log(this.employees)
            this.posts=await fetchDepartmentData();
        },
        methods: {
            //输入验证函数
            async validateYear(rule, value, callback) {
                const currentYear = new Date().getFullYear();
                if (value >= 2000 && value <= currentYear) {
                    callback();
                } else {
                    callback(new Error('请填写正确的年份'));
                }
            },
            async validateMonth(rule, value, callback) {
                if (value >= 1 && value <= 12) {
                    callback();
                } else {
                    callback(new Error('请填写正确的月份'));
                }
            },

            /*查看工资信息 */
            async viewSalaries(employeeId) {
                this.currentEmployeeId = employeeId;
                this.salaryRecords = await getSalaryInfo(employeeId);
                this.salaryDialogVisible = true;
                console.log(77777)
                console.log(this.salaryRecords)
            },
            /*清除搜索id*/
            clearSearch() {
                this.searchId = "";
                this.searchName ='';
            },
            /*筛选过滤处理函数*/
            filterHandler(value, row, column) {
                const property = column['property']
                return row[property] === value
            },
            /*启动输入框、编辑岗位月薪*/
            startEditing(row) {
                this.editingRow = row;
                this.originalSalary = row.positionSalary;
                this.$nextTick(() => {
                    this.$refs.editInput.focus();
                });
            },
            /*检查键盘事件是否是回车键 */
            handleKeyup(event) {
                if (event.keyCode === 13) {
                    // 13 表示回车键的键码
                    this.confirmEdit();
                }
            },
            /*检查数据是否改动*/
            checkAndConfirmEdit() {
                if (this.editingRow.positionSalary != this.originalSalary) {
                    this.confirmEdit();
                } else {
                    this.editingRow.positionSalary = this.originalSalary;
                    this.editingRow = null;
                    this.originalSalary = null;
                }
            },
            /*编辑完成，提示是否确认修改*/
            confirmEdit() {
                if(isNaN(parseFloat(this.editingRow.positionSalary)) || this.editingRow.positionSalary<=0){
                    ElMessage.error('薪资修改不合法！');
                    // 取消操作，恢复原始工资
                    this.editingRow.positionSalary = this.originalSalary;
                    this.editingRow = null;
                    this.originalSalary = null;
                    return ;
                }
                ElMessage.success(this.editingRow.positionSalary);
                this.$confirm(`确认修改${this.editingRow.departmentName}部门-${this.editingRow.postName}的月薪为${this.editingRow.positionSalary}元？`, "提示", {
                    confirmButtonText: "确认",
                    cancelButtonText: "取消",
                    type: "warning",
                })
                .then(() => {
                    // 发送postID、salary到后端
                    // 成功后更新数据
                    put('/api/Post/Update',{
                        "postId": this.editingRow.postId,
                        "positionSalary": this.editingRow.positionSalary,
                    })
                    .then(response => {
                        if(response)
                            ElMessage.success('修改成功！');
                        else{
                            ElMessage.error('修改失败，请稍后重试');
                        }
                    })
                    this.editingRow = null;
                    this.originalSalary = null;
                })
                .catch(() => {
                    // 取消操作，恢复原始工资
                    if (this.editingRow && this.originalSalary) {
                        this.editingRow.positionSalary = this.originalSalary;
                    this.editingRow = null;
                    this.originalSalary = null;
                    }
                       
                });
            },
            /*根据选择的年月筛选工资记录*/
            filterSalaryRecords(value, row, column) {
                // 在这里根据选择的年月进行筛选
            },
            openEditSalaryDialog(salary){
                this.editSalary=salary;
                console.log("修改修改修改")
                console.log(salary)
                this.editDialogVisible = true;
            },
            saveEditSalary(){
                this.$confirm("确认修改？", "提示", {
                    confirmButtonText: "确认",
                    cancelButtonText: "取消",
                    type: "warning",
                })
                .then(async () => {
                    console.log(7777777777777777)
                    console.log(this.editSalary)
                    await put('/api/Salary/Update',this.editSalary)
                    .then(response =>{
                        if(response.status){
                            console.log(response);
                        ElMessage.success('修改成功');
                        }
                        else{
                            ElMessage.error('修改失败');
                        }
                       
                        this.editDialogVisible = false;
                    })
                    .catch(error => {
                        console.error(error);
                    })
                })
                .catch(()=>{

                })
            },
            // 点击删除按钮，弹出确认提示框
            deleteSalary(targetSalary) {
                console.log(targetSalary);
                this.$confirm("确认删除该工资记录？", "提示", {
                    confirmButtonText: "确认",
                    cancelButtonText: "取消",
                    type: "warning",
                })
                .then(async () => {
                    // 在这里执行删除操作
                    // 删除后刷新工资列表
                    const response = await delSalary(targetSalary.salaryId);
                    if(response){
                        ElMessage.success('删除成功');
                    }
                    this.salaryRecords = await getSalaryInfo(targetSalary.employeeId);
                })
                .catch(() => {
                    // 用户取消删除
                });
            },
            /* 点击新增按钮，显示新增工资记录对话框*/
            showAddDialog() {
                this.addDialogVisible = true;
                console.log(this.newSalary.employeeID);
            },
            /* 点击对话框的保存按钮，保存新增工资记录*/
            async saveNewSalary() {
                // 将 newSalary 添加到 salaryRecords 数组中
                this.newSalary.employeeId = this.currentEmployeeId;
                console.log(this.newSalary);
                this.$refs.newSalaryForm.validate(async valid => {
                    if (valid) {
                        await post('/api/Salary/Add',this.newSalary)
                        .then(async response => {
                            console.log(response);    
                            if(response.status)
                                ElMessage.success('新增成功！');
                            this.salaryRecords=await getSalaryInfo(this.newSalary.employeeId);
                            this.newSalary = {};
                            this.addDialogVisible = false;
                            })
                        .catch(error => {
                            console.error(error);
                            ElMessage.error('新增失败！')
                        })
                    }
                    else{
                        // 表单验证不通过，显示错误提示
                        this.$message.error('请填写正确的表单数据');
                    }
                });
            },
            /*点击取消按钮，取消编辑工资信息*/
            cancelEdit() {
                this.salaryDialogVisible = false;
                this.currentSalary = {};
            },
        },
    }
</script>

<style scoped>
.salary-manage {
    padding: 20px;
    background-image: linear-gradient(-45deg, #97cba7, #9ab7e3);
    height: 115vh;
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
.flex-container {
    display: flex;
}
.search-input {
	width: 33%;
    margin-top: 30px;
    margin-bottom: 30px;
}
.search-select {
	width: 23%;
    margin-left: 20%;
    margin-right: 7%;
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
    font-size: large;
    color: #196883;
}
::v-deep .el-select {
    --el-select-border-color-hover: #324157;
    --el-select-input-focus-border-color: #white;
}
::v-deep .el-select .el-select__caret {
    font-size: 30px;
    color: #324157;
}
.table-container {
    background-color: #196883;
    padding: 20px;
    border-radius: 15px;
    margin-bottom: 20px;
}
.table {
    height: 350px;
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
.left-row-employee-id {
    font-size: large;
    color: #196883;
}
.left-row-name {
    font-size: large;
    color: #a76200;
}
.left-row-department-name {
    font-size: large;
    color: #196883;
}
.left-row-post-name {
    font-size: large;
    color: #a76200;
}
.view-button {
    margin-left: 10px;
    width: 100%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #196883;
}
.view-button:hover {
    background-color: #2aba98;
}
::v-deep .el-button {
    color: white;
    font-size: large;
    font-weight: lighter;
}
.right-row-post-name {
    font-size: large;
    color: #a76200;
}
.right-row-department-name {
    font-size: large;
    color: #196883;
}
.right-row-number-of-people {
    font-size: large;
    color: #a76200;
}
.right-row-position-level {
    font-size: large;
    color: #196883;
}
.right-row-input {
    width: 100%;
}
.right-row-position-salary {
    font-size: large;
    color: #196883;
}
.right-row-edit-button {
    width: 25px;
    height: 25px;
    border-radius: 25px;
    background-color: #196883;
}
.right-row-edit-button:hover {
    background-color: #97cba7;
}
::v-deep .el-dialog {
    width: 70%;
    background-image: linear-gradient(-45deg, #97cba7, #9ab7e3);
    border-radius: 30px;
}
::v-deep .el-dialog__title {
    display: flex;
    justify-content: center;
    font-size: x-large;
    margin-top: 20px;
    color: #196883;
}
::v-deep .el-dialog__headerbtn {
    font-size: 30px;
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
    font-size: large;
    font-weight: bold;
    color: #324157;
}
.dialog-table-container {
    background-color: #196883;
    padding: 10px;
    border-radius: 15px;
}
.dialog-table {
    width: 100%;
    max-height: 470px;
    background: #196883;
    border-radius: 5px;
}
.dialog-row-salary-id {
    font-size: medium;
    color: #196883;
}
.dialog-row-emp-id {
    font-size: medium;
    color: #196883;
}
.dialog-row-year {
    font-size: medium;
    color: #a76200;
}
.dialog-row-month {
    font-size: medium;
    color: #196883;
}
.dialog-row-holiday-allowance {
    font-size: medium;
    color: #196883;
}
.dialog-row-bonus {
    font-size: medium;
    color: #196883;
}
.dialog-row-overtime-pay {
    font-size: medium;
    color: #196883;
}
.dialog-row-commission {
    font-size: medium;
    color: #196883;
}
.dialog-row-year-end-bonus {
    font-size: medium;
    color: #196883;
}
.dialog-row-other-allowance {
    font-size: medium;
    color: #a76200;
}
.dialog-row-reward-type {
    font-size: medium;
    color: #196883;
}
.dialog-row-reward-amount {
    font-size: medium;
    color: #196883;
}
.dialog-row-late-deduction {
    font-size: medium;
    color: #196883;
}
.dialog-row-early-departure-deduction {
    font-size: medium;
    color: #196883;
}
.dialog-row-income-tax {
    font-size: medium;
    color: #196883;
}
.dialog-row-social-insurance {
    font-size: medium;
    color: #196883;
}
.dialog-row-gross-salary {
    font-size: medium;
    color: #a76200;
}
.dialog-row-net-salary {
    font-size: medium;
    color: #196883;
}
.dialog-row-edit-button {
    width: fit-content;
    font-size: small;
    /*height: 48px;*/
    border: 3px solid #c1cbd9;
    background-color: #196883;
}
.dialog-row-edit-button:hover {
    background-color: #2aba98;
}
.dialog-row-delete-button {
    margin-left: 10px;
    font-size: small;
    width: content;
    /*height: 48px;*/
    border: 3px solid #c1cbd9;
    background-color: #b02a08;
}
.dialog-row-delete-button:hover {
    background-color: #ff7350;
}
.dialog-buttons {
    display: flex;
    justify-content: center;
    margin-top: 30px;
}
.dialog-add-button {
    width: 30%;
    height: 50px;
    border-radius: 10px;
    border: 0;
    background-image: linear-gradient(to right, #30cfd0, #330867);
}
.dialog-add-button:hover {
    background-image: linear-gradient(to right, #90de83, #00929b);
}
::v-deep .el-form-item {
    margin-bottom: 10px;
}
::v-deep .el-form-item__label {
    font-size: medium;
    font-weight: lighter;
    color: #196883;
}
.dialog-form-input {
    width: 75%;
}
.dialog-save-button {
    margin-left: 10px;
    margin-right: 10px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 0px;
    padding-bottom: 0px;
    font-size: large;
    color: green;
    border: 2px solid green;
    border-radius: 10px;
    background-color: white;
    height: 40px;
}
.dialog-save-button:hover {
    background-color: #ccffc3;
}
.dialog-cancel-button {
    margin-left: 10px;
    margin-right: 10px;
    padding-left: 25px;
    padding-right: 25px;
    padding-top: 0px;
    padding-bottom: 0px;
    font-size: large;
    color: red;
    border: 2px solid red;
    border-radius: 10px;
    background-color: white;
    height: 40px;
}
.dialog-cancel-button:hover {
    background-color: #ffadad;
}
</style>
