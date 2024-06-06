<template>
    <div class="check-attendance">
        <img src="../../assets/personnel.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 人事管理 </label>
            <label class="title-content"> 考勤管理 </label>
        </div>
        <!-- 搜索栏 -->
        <div class="search-container">
            <el-select v-model="searchBy" placeholder="请选择查询方式" class="search-select">
                <el-option label="员工号" value="employeeId"></el-option>
                <el-option label="员工姓名" value="name"></el-option>
            </el-select>
            <el-input v-if="searchBy == 'employeeId'" v-model="searchId" placeholder="输入员工号查询" clearable
                @clear="clearSearch" class="search-input"></el-input>
            <el-input v-else-if="searchBy == 'name'" v-model="searchName" placeholder="输入员工姓名查询" clearable
                @clear="clearSearch" class="search-input"></el-input>
        </div>
        <!-- 信息栏 -->
        <div class="table-container">
            <el-table :data="filteredEmployees" :row-class-name="tableRowClassName" class="table" :header-cell-style="{
                'padding': '20px',
                'font-size': 'x-large',
                'font-weight': 'lighter',
                'color': '#ffffff',
                'background-color': '#196883'
            }">
                <el-table-column prop="employeeId" label="员工ID" sortable>
                    <template #default="scope">
                        <label class="row-employee-id"> {{ scope.row.employeeId }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="name" label="员工姓名">
                    <template #default="scope">
                        <label class="row-name"> {{ scope.row.name }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="departmentName" label="所属部门" column-key="department" :filters="[
                    { text: '餐饮', value: '餐饮' },
                    { text: '财务', value: '财务' },
                    { text: '人事', value: '人事' },
                    { text: '后勤', value: '后勤' },
                    { text: '前厅部', value: '前厅部' },
                    { text: '总经理办公室', value: '总经理办公室' },
                ]" :filter-method="filterHandler">
                    <template #default="scope">
                        <label class="row-department-name"> {{ scope.row.departmentName }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="postName" label="岗位名称">
                    <template #default="scope">
                        <label class="row-post-name"> {{ scope.row.postName }} </label>
                    </template>
                </el-table-column>
                <el-table-column label="操作" align="center">
                    <template #default="scope">
                        <el-button @click="viewAttendance(scope.row)" class="view-button">查看考勤信息</el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>

        <!-- 在这里显示员工考勤信息 -->
        <el-dialog v-model="attendanceDialogVisible" title="考勤信息" width="85%">
            <label class="employee-info"> 员工ID：{{ currentEmployeeId }} </label>
            <label class="employee-info"> 员工姓名：{{ currentEmployeeName }} </label>
            <div style="height: 40px;"></div>

            <div class="view-dialog-table-container">
                <el-table :data="AttendanceInfo" v-if="AttendanceInfo" class="view-dialog-table" :header-cell-style="{
                    'padding': '20px',
                    'font-size': 'x-large',
                    'font-weight': 'lighter',
                    'color': '#ffffff',
                    'background-color': '#196883'
                }">
                    <el-table-column label="年" prop="Year">
                        <template #default="scope">
                            <span class="view-dialog-row-year"> {{ scope.row.year }} </span>
                        </template>
                    </el-table-column>
                    <el-table-column label="月" prop="Month">
                        <template #default="scope">
                            <span class="view-dialog-row-month"> {{ scope.row.month }} </span>
                        </template>
                    </el-table-column>
                    <el-table-column label="应到天数" prop="ExpectAttendanceDays" editable>
                        <template #default="scope">
                            <span v-if="scope.row === editingRow && scope.column.property === editingColumn">
                                <el-input-number v-model="scope.row.expectAttendanceDays" size="small" style="width: 80px;"
                                    :min="1" :max="31" @keyup.enter="handleKeyup" @blur="checkAndConfirmEdit"
                                    class="view-dialog-input-number">
                                </el-input-number>
                            </span>
                            <span v-else @click="startEditing(scope.row, scope.column.property)"
                                class="view-dialog-row-expect-attendance-days">
                                {{ scope.row.expectAttendanceDays }}
                                <el-icon>
                                    <Edit />
                                </el-icon>
                            </span>
                        </template>
                    </el-table-column>
                    <el-table-column label="实到天数" prop="ActualAttendanceDays" editable>
                        <template #default="scope">
                            <span v-if="scope.row === editingRow && scope.column.property === editingColumn">
                                <el-input-number v-model="scope.row.actualAttendanceDays" size="small" style="width: 80px;"
                                    :min="1" :max="31" @keyup.enter="handleKeyup" @blur="checkAndConfirmEdit"
                                    class="view-dialog-input-number">
                                </el-input-number>
                            </span>
                            <span v-else @click="startEditing(scope.row, scope.column.property)"
                                class="view-dialog-row-actual-attendance-days">
                                {{ scope.row.actualAttendanceDays }}
                                <el-icon>
                                    <Edit />
                                </el-icon>
                            </span>
                        </template>
                    </el-table-column>
                    <el-table-column label="事假天数" prop="PersonalLeaveDays" editable>
                        <template #default="scope">
                            <span v-if="scope.row === editingRow && scope.column.property === editingColumn">
                                <el-input-number v-model="scope.row.personalLeaveDays" size="small" style="width: 80px;"
                                    :min="0" :max="31" @keyup.enter="handleKeyup" @blur="checkAndConfirmEdit"
                                    class="view-dialog-input-number">
                                </el-input-number>
                            </span>
                            <span v-else @click="startEditing(scope.row, scope.column.property)"
                                class="view-dialog-row-personal-leave-days">
                                {{ scope.row.personalLeaveDays }}
                                <el-icon>
                                    <Edit />
                                </el-icon>
                            </span>
                        </template>
                    </el-table-column>
                    <el-table-column label="病假天数" prop="SickLeaveDays" editable>
                        <template #default="scope">
                            <span v-if="scope.row === editingRow && scope.column.property === editingColumn">
                                <el-input-number v-model="scope.row.sickLeaveDays" size="small" style="width: 80px;"
                                    :min="0" :max="31" @keyup.enter="handleKeyup" @blur="checkAndConfirmEdit"
                                    class="view-dialog-input-number">
                                </el-input-number>
                            </span>
                            <span v-else @click="startEditing(scope.row, scope.column.property)"
                                class="view-dialog-row-sick-leave-days">
                                {{ scope.row.sickLeaveDays }}
                                <el-icon>
                                    <Edit />
                                </el-icon>
                            </span>
                        </template>
                    </el-table-column>
                    <el-table-column label="迟到天数" prop="LateDays" editable>
                        <template #default="scope">
                            <span v-if="scope.row === editingRow && scope.column.property === editingColumn">
                                <el-input-number v-model="scope.row.lateDays" size="small" style="width: 80px;" :min="0"
                                    :max="31" @keyup.enter="handleKeyup" @blur="checkAndConfirmEdit"
                                    class="view-dialog-input-number">
                                </el-input-number>
                            </span>
                            <span v-else @click="startEditing(scope.row, scope.column.property)"
                                class="view-dialog-row-late-days">
                                {{ scope.row.lateDays }}
                                <el-icon>
                                    <Edit />
                                </el-icon>
                            </span>
                        </template>
                    </el-table-column>
                    <el-table-column label="早退天数" prop="EarlyDepartureDays" editable>
                        <template #default="scope">
                            <span v-if="scope.row === editingRow && scope.column.property === editingColumn">
                                <el-input-number v-model="scope.row.earlyDepartureDays" size="small" style="width: 80px;"
                                    :min="0" :max="31" @keyup.enter="handleKeyup" @blur="checkAndConfirmEdit"
                                    class="view-dialog-input-number">
                                </el-input-number>
                            </span>
                            <span v-else @click="startEditing(scope.row, scope.column.property)"
                                class="view-dialog-row-early-departure-days">
                                {{ scope.row.earlyDepartureDays }}
                                <el-icon>
                                    <Edit />
                                </el-icon>
                            </span>
                        </template>
                    </el-table-column>
                    <el-table-column label="旷工天数" prop="AbsentDays" editable>
                        <template #default="scope">
                            <span v-if="scope.row === editingRow && scope.column.property === editingColumn">
                                <el-input-number v-model="scope.row.absentDays" size="small" style="width: 80px;" :min="0"
                                    :max="31" @keyup.enter="handleKeyup" @blur="checkAndConfirmEdit"
                                    class="view-dialog-input-number">
                                </el-input-number>
                            </span>
                            <span v-else @click="startEditing(scope.row, scope.column.property)"
                                class="view-dialog-row-absent-days">
                                {{ scope.row.absentDays }}<el-icon>
                                    <Edit />
                                </el-icon>
                            </span>
                        </template>
                    </el-table-column>
                    <el-table-column label="操作">
                        <template #default="scope">
                            <el-button :icon="Delete" @click="handleDelete(scope.row)" class="delete-button"> 删除
                            </el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>

            <div class="view-dialog-buttons">
                <button class="view-dialog-add-button" @click="addDialogVisible = true"> 新增 </button>
                <button class="view-dialog-close-button" @click="attendanceDialogVisible = false"> 关闭 </button>
            </div>
        </el-dialog>

        <!-- 新增考勤信息对话框 -->
        <el-dialog v-model="addDialogVisible" title="新增考勤信息" width="70%">
            <el-form inline="true" ref="formRef" :model="form" :rules="rules" label-width="140px">
                <el-form-item label="员工ID" prop='employeeId' required>
                    <el-input v-model.number="form.employeeId" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="年" prop='year' required>
                    <el-input v-model.number="form.year" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="月" prop='month' required>
                    <el-input v-model.number="form.month" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="应到天数" prop='expectAttendanceDays' required>
                    <el-input v-model.number="form.expectAttendanceDays" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="实到天数" prop='actualAttendanceDays' required>
                    <el-input v-model.number="form.actualAttendanceDays" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="事假天数" prop='personalLeaveDays' required>
                    <el-input v-model.number="form.personalLeaveDays" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="病假天数" prop='sickLeaveDays' required>
                    <el-input v-model.number="form.sickLeaveDays" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="迟到天数" prop='lateDays' required>
                    <el-input v-model.number="form.lateDays" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="早退天数" prop='earlyDepartureDays' required>
                    <el-input v-model.number="form.earlyDepartureDays" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="旷工天数" prop='absentDays' required>
                    <el-input v-model.number="form.absentDays" class="add-dialog-form-input"></el-input>
                </el-form-item>
            </el-form>

            <div class="add-dialog-buttons">
                <button class="add-dialog-submit-button" @click="addAttendanceInfo"> 提交 </button>
                <button class="add-dialog-cancel-button" @click="addDialogVisible = false"> 取消 </button>
            </div>
        </el-dialog>


        <!-- 今日打卡组件 -->
        <!-- 地图组件 -->
        <div id="map">
        </div>
        <!-- 时间组件 -->
        <div style="display:block;text-align:center;justify-content: center;margin: 20px;">
            <span class="time"> {{ gettime }} </span>
            <br>
            <el-button color="#324157" @click="submitAttendance()" :disabled="isButtonDisabled" class="check-button">
                今日打卡
            </el-button>
        </div>
    </div>
</template>

<script>
import { post, put, del, get, userId, getUserInfo } from '../../axios/axiosConfig.js'
import { getAttendanceInfo } from '../../axios/check_attendance.js';
import { fetchEmployeeData } from '../../axios/job_scheduling.js';
import { ElMessage, ElMessageBox } from 'element-plus';
import { Plus, Delete } from '@element-plus/icons-vue';
console.log(userId)



import AMapLoader from '@amap/amap-jsapi-loader'
import { onMounted, shallowRef, ref } from 'vue'

export default {
    setup() {
        const map = shallowRef(null);
        let currentPosition = shallowRef(null);
        const targetPosition = [121.215252, 31.286054]; // 目标位置坐标为同济大学嘉定校区
        const circle = shallowRef(null); // 圆形覆盖物对象

        window._AMapSecurityConfig = {
            securityJsCode: "c61d328ad9a906eaf64ea4f23914cd94",
        };

        // 初始化地图
        function initMap() {
            AMapLoader.load({
                key: 'a34a5db499f2b8f2c70d912fca749747',
                version: "2.0",
                plugins: [
                    "AMap.Scale",
                    "AMap.ToolBar",
                    "AMap.Geolocation",
                    "AMap.HawkEye",
                ],
            }).then((AMap) => {
                map.value = new AMap.Map("map", {
                    zoom: 15,//初始地图层级
                    center: targetPosition,//初始地图中心点
                    dragEnable: true,
                    scrollWheel: true,
                    doubleClickZoom: true,
                    keyboardEnable: true,
                    willReadFrequently: true,
                });

                // 添加比例尺
                map.value.addControl(new AMap.Scale());

                // 添加工具条
                map.value.addControl(new AMap.ToolBar());

                // 添加定位按钮
                map.value.addControl(new AMap.Geolocation({
                    enableHighAccuracy: true, // 启用高精度定位
                    accuracy: 'high',
                    useNative: true,
                    enableCache: true,
                    interval: 2000,
                    timeout: 10000,
                }));

                // 添加鹰眼
                let HawkEye = new AMap.HawkEye({
                    position: "LT",
                });
                map.value.addControl(HawkEye);

                // 添加标记
                let markerPoint = new AMap.Marker({
                    position: [121.215252, 31.286054],
                    offset: new AMap.Pixel(-12, -32),
                    draggable: false,
                    defaultCursor: 'pointer'
                });
                map.value.add(markerPoint);

                // 获取用户当前位置
                const geolocation = new AMap.Geolocation();
                console.log(geolocation);
                geolocation.getCurrentPosition(function (status, result) {
                    if (status == 'complete') {
                        console.log(result);
                        currentPosition = [result.position.lng, result.position.lat];
                        console.log(currentPosition);
                    } else {
                        ElMessage.error('获取位置失败，请检查浏览器设置');
                    }
                });

                // 创建圆形覆盖物表示有效范围
                circle.value = new AMap.Circle({
                    center: targetPosition,
                    radius: 1000, // 有效距离
                    strokeColor: "#3366FF", // 边线颜色
                    strokeWeight: 2, // 边线宽度
                    strokeOpacity: 0.5, // 边线透明度
                    fillOpacity: 0.2, // 填充透明度
                    fillColor: "#3366FF", // 填充颜色
                });
                map.value.add(circle.value);

            }
            ).catch((e) => {
                console.log(e);
            });
        }


        // 打卡功能
        function checkIn() {
            const response = getUserInfo();
            this.LoggedEmployeeId = response.employeeId;

            console.log(123)
            console.log(currentPosition);
            console.log(targetPosition);
            if (currentPosition) {
                const distance = AMap.GeometryUtil.distance(currentPosition, targetPosition);

                if (distance <= 50000) {
                    return true;
                }
                else {
                    ElMessage.warning('距离目标位置超过1km，无法打卡！');
                    return false;
                }
            }
            else {
                ElMessage.error('无法获取当前位置，请确认定位功能已开启并允许获取位置信息。');
                return false;
            }
        }

        // 调用地图初始化函数
        onMounted(() => {
            initMap();
        });

        return { map, initMap, checkIn, currentPosition };
    },

    data() {
        return {
            LoggedEmployeeId: userId,
            searchBy: 'employeeId',//查询方式
            gettime: "", //当前时间

            employees: [],
            /*可根据员工状态添加行底色*/
            tableRowClassName: '',
            searchId: '',
            searchName: '',
            attendanceDialogVisible: false,
            currentEmployeeId: null,
            currentEmployeeName: '',
            AttendanceInfo: [],
            isButtonDisabled: false, // 初始状态为未禁用
            addDialogVisible: false,

            form: {
                employeeId: this.currentEmployeeId,
                year: null,
                month: null,
                expectAttendanceDays: null,
                actualAttendanceDays: null,
                personalLeaveDays: null,
                sickLeaveDays: null,
                lateDays: null,
                earlyDepartureDays: null,
                absentDays: null
            },
            rules: {
                EmpId: [{ required: true, message: '请输入正确的员工id', trigger: 'blur', pattern: /^[0-9]*$/ }],
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
                ExpectAttendanceDays: [
                    { required: true, message: '请输入正确的应到天数', trigger: 'blur' },
                    {
                        validator: this.validateDays,
                    },],
                ActualAttendanceDays: [
                    { required: true, message: '请输入正确的实到天数', trigger: 'blur' },
                    {
                        validator: this.validateDays,
                    },],
                PersonalLeaveDays: [
                    { required: true, message: '请输入正确的事假天数', trigger: 'blur' },
                    {
                        validator: this.validateDays,
                    },],
                SickLeaveDays: [
                    { required: true, message: '请输入正确的病假天数', trigger: 'blur' },
                    {
                        validator: this.validateDays,
                    },],
                LateDays: [
                    { required: true, message: '请输入正确的迟到天数', trigger: 'blur' },
                    {
                        validator: this.validateDays,
                    },],
                EarlyDepartureDays: [
                    { required: true, message: '请输入正确的早退天数', trigger: 'blur' },
                    {
                        validator: this.validateDays,
                    },],
                AbsentDays: [
                    { required: true, message: '请输入正确的旷工天数', trigger: 'blur' },
                    {
                        validator: this.validateDays,
                    },],
            },
            editingRow: null, // 用于追踪当前正在编辑的行
            editingColumn: null, // 用于追踪当前正在编辑的列
            originalData: null,//用于保存启动单元格输入框之前的数据
            /*保存各列的属性名*/
            columnLabels: {
                ExpectAttendanceDays: "应到天数",
                ActualAttendanceDays: "实到天数",
                PersonalLeaveDays: "事假天数",
                SickLeaveDays: "病假天数",
                LateDays: "迟到天数",
                EarlyDepartureDays: "早退天数",
                AbsentDays: "旷工天数"
            },
        };
    },
    /*更新组件*/
    async mounted() {
        this.employees = await fetchEmployeeData();
    },

    computed: {
        filteredEmployees() {
            console.log(this.searchBy);
            if (this.searchBy === 'employeeId') {
                if (this.searchId) {
                    return this.employees.filter(employee => employee.employeeId.toString().includes(this.searchId));
                }
            }
            else if (this.searchBy === 'name') {
                if (this.searchName) {
                    return this.employees.filter(employee => employee.name.includes(this.searchName));
                }
            }
            return this.employees;
        },
    },

    methods: {
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
        async validateDays(rule, value, callback) {
            if (value >= 0 && value <= 31) {
                callback();
            } else {
                callback(new Error('天数应在0到31之间'));
            }
        },



        getTime() {
            var _this = this;
            let yy = new Date().getFullYear();
            var mm =
                new Date().getMonth() > 9
                    ? new Date().getMonth() + 1
                    : new Date().getMonth() == 9
                        ? new Date().getMonth() + 1
                        : '0' + (new Date().getMonth() + 1);
            var dd = new Date().getDate() < 10 ? '0' + new Date().getDate() : new Date().getDate();
            let hh = new Date().getHours();
            let mf =
                new Date().getMinutes() < 10 ? '0' + new Date().getMinutes() : new Date().getMinutes();
            let ss =
                new Date().getSeconds() < 10 ? '0' + new Date().getSeconds() : new Date().getSeconds();
            _this.gettime = yy + '-' + mm + '-' + dd + ' ' + hh + ':' + mf + ':' + ss;
        },
        /*每1000ms更新一次时间 */
        currentTime() {
            setInterval(this.getTime, 1000);
        },
        /*提交打卡信息 */
        async submitAttendance() {
            // 如果按钮未禁用，则执行提交考勤操作
            if (!this.isButtonDisabled) {
                // 提交考勤逻辑
                /*向后端发送id和打卡时间*/
                const hour = new Date().getHours();
                const Year = new Date().getFullYear();
                const Month = new Date().getMonth() + 1;
                const res = getUserInfo();
                console.log(res)
                this.LoggedEmployeeId = res.employeeId;
                /*获取当前AttendanceId */
                const res1 = await get(`/api/AttendanceInformation/GetByEmpId/${this.LoggedEmployeeId}`, { empId: this.LoggedEmployeeId });
                const response = res1.data;
                var id;
                console.log(456)
                console.log(response)
                //response就表示原来有考勤
                if (response && response[0].month == Month && response[0].year == Year) {
                    console.log(789)
                    //当月已存在考勤数据，则在该数据的基础上修改更新
                    response[0].actualAttendanceDays = response[0].actualAttendanceDays + 1;
                    id = response[0].attendanceId;
                    const oldActualAttendanceDays = response[0].actualAttendanceDays;
                    const absentDays = response[0].absentDays;
                    const earlyDepartureDays = response[0].earlyDepartureDays;
                    const lateDays = response[0].lateDays + 1;
                    const month = response[0].month;
                    const personalLeaveDays = response[0].personalLeaveDays;
                    const sickLeaveDays = response[0].sickLeaveDays;
                    const year = response[0].year;
                    const employeeId = response[0].employeeId;
                    const expectAttendanceDays = response[0].expectAttendanceDays

                    if (hour <= 9) {
                        await put('/api/AttendanceInformation/Update',
                            {
                                "attendanceId": id,
                                "actualAttendanceDays": oldActualAttendanceDays + 1,
                                "absentDays": response[0].absentDays,
                                "earlyDepartureDays": response[0].earlyDepartureDays,
                                "employeeId": response[0].employeeId,
                                "expectAttendanceDays": response[0].expectAttendanceDays,
                                "lateDays": response[0].lateDays,
                                "month": response[0].month,
                                "personalLeaveDays": response[0].personalLeaveDays,
                                "sickLeaveDays": response[0].sickLeaveDays,
                                "year": response[0].year
                            })
                            .then(response => {
                                console.log(response);
                                if (response.status) {
                                    ElMessage.success('打卡成功!');
                                }
                                else {
                                    ElMessage.error('打卡失败');
                                }
                            })
                            .catch(error => {
                                console.error(error);
                            })
                    }
                    else {
                        console.log("dddd")
                        await put('/api/AttendanceInformation/Update',
                            {
                                "attendanceId": id,
                                "actualAttendanceDays": oldActualAttendanceDays + 1,
                                "absentDays": absentDays,
                                "earlyDepartureDays": earlyDepartureDays,
                                "employeeId": employeeId,
                                "expectAttendanceDays": expectAttendanceDays,
                                "lateDays": lateDays + 1,
                                "month": month,
                                "personalLeaveDays": personalLeaveDays,
                                "sickLeaveDays": sickLeaveDays,
                                "year": year

                            })
                            .then(response => {
                                console.log(response);
                                if (response.status) {
                                    ElMessage.success('打卡成功!您已迟到');
                                }
                                else {
                                    ElMessage.error('打卡失败');
                                }

                            })
                            .catch(error => {
                                console.error(error);
                            })
                    }
                }
                else {//没有当月考勤数据，为该月新建考勤数据
                    console.log(888)
                    if (hour <= 9) {
                        await post('/api/AttendanceInformation/Add',
                            {
                                "employeeId": this.LoggedEmployeeId,
                                "year": Year,
                                "month": Month,
                                "expectAttendanceDays": 31,
                                "actualAttendanceDays": 1,
                                "personalLeaveDays": 0,
                                "sickLeaveDays": 0,
                                "lateDays": 0,
                                "earlyDepartureDays": 0,
                                "absentDays": 0
                            })
                            .then(response => {
                                console.log(Month)
                                console.log(response);
                                if (response.status) {
                                    ElMessage.success('打卡成功!');
                                }
                                else {
                                    ElMessage.error('打卡失败');
                                }
                            })
                            .catch(error => {
                                console.error(error);
                            })
                    }
                    else {
                        await post('/api/AttendanceInformation/Add',
                            {
                                "employeeId": this.LoggedEmployeeId,
                                "year": Year,
                                "month": Month,
                                "expectAttendanceDays": 31,
                                "actualAttendanceDays": 1,
                                "personalLeaveDays": 0,
                                "sickLeaveDays": 0,
                                "lateDays": 1,
                                "earlyDepartureDays": 0,
                                "absentDays": 0
                            })
                            .then(response => {
                                console.log(response);
                                if (response.status) {
                                    ElMessage.success('打卡成功!您已迟到');
                                }
                                else {
                                    ElMessage.error('打卡失败');
                                }
                            })
                            .catch(error => {
                                console.error(error);
                            })
                    }
                }

                // 禁用按钮
                this.isButtonDisabled = true;
            }

        },

        /*每天0点解除按钮禁用*/
        checkResetButtonStatus() {
            const currentTime = new Date();
            const currentHour = currentTime.getHours();
            const currentMinute = currentTime.getMinutes();

            // 每天的0点时解锁按钮
            if (currentHour === 0 && currentMinute === 0) {
                this.isButtonDisabled = false;
            }
        },

        /*查看考勤信息 */
        async viewAttendance(selectedEmployee) {
            this.attendanceDialogVisible = true;
            this.currentEmployeeId = selectedEmployee.employeeId;
            this.currentEmployeeName = selectedEmployee.name;
            console.log('selectedEmployeeId:' + selectedEmployee.employeeId);
            this.AttendanceInfo = await getAttendanceInfo(selectedEmployee.employeeId);
            console.log(this.AttendanceInfo);
        },
        /*清除搜索id*/
        clearSearch() {
            this.searchId = "";
            this.searchName = "";
        },
        /*新增打卡信息*/
        async addAttendanceInfo() {
            console.log(this.currentEmployeeId);
            console.log(this.form);
            this.$refs.formRef.validate(async valid => {
                if (valid) {
                    await post('/api/AttendanceInformation/Add', this.form)
                        .then(async response => {
                            console.log(response);
                            if (response.status)
                                ElMessage.success('新增成功！');
                            else
                                ElMessage.error('新增失败！');
                            console.log(this.currentEmployeeId);
                            this.AttendanceInfo = await getAttendanceInfo(this.currentEmployeeId);
                            this.form = {};
                            this.addDialogVisible = false;
                        })
                        .catch(error => {
                            console.error(error);
                            ElMessage.error('新增失败！')
                        })
                }
                else {
                    // 表单验证不通过，显示错误提示
                    this.$message.error('请填写正确的表单数据');
                }
            });
        },

        /*筛选过滤处理函数*/
        filterHandler(value, row, column) {
            const property = column['property']
            return row[property] === value
        },

        handleDelete(SelectedAttendanceInfo) {
            console.log(SelectedAttendanceInfo);
            const message = `确认删除员工（Id: ${this.currentEmployeeId}）${SelectedAttendanceInfo.year}年${SelectedAttendanceInfo.month}月的考勤记录？`;
            this.$confirm(message, "提示", {
                confirmButtonText: "确认",
                cancelButtonText: "取消",
                type: "warning",
            })
                .then(async () => {
                    try {
                        const response = await del(`/api/AttendanceInformation/Delete/${SelectedAttendanceInfo.attendanceId}`, {})

                        console.log(response);
                        ElMessage.success('删除成功！');
                        this.AttendanceInfo = await getAttendanceInfo(this.currentEmployeeId);
                    }
                    catch (error) {
                        console.error(error);
                    }
                })
                .catch(() => {

                })
        },

        /*启动考勤信息表单元格中的输入框*/
        startEditing(row, column) {
            console.log('row-column', row, '-', column)
            if (this.editingRow !== null && this.editingColumn !== null) {
                this.checkAndConfirmEdit(() => {
                    console.log('change1');
                    this.editingRow = row;
                    this.editingColumn = column;
                    this.originalData = row[column];
                });
            } else {
                console.log('change2');
                this.editingRow = row;
                this.editingColumn = column;
                this.originalData = row[column];
            }
        },
        stopEditing() {
            this.editingRow = null;
            this.editingColumn = null;
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
            console.log('checkAndConfirm')
            if (this.editingRow[this.editingColumn] != this.originalData) {
                this.confirmEdit();
            } else {
                this.editingRow[this.editingColumn] = this.originalData;
                this.editingRow = null;
                this.editingColumn = null;
                this.originalData = null;
            }
        },
        /*编辑完成，提示是否确认修改*/
        confirmEdit() {
            console.log(this.editingRow);
            if (isNaN(parseFloat(this.editingRow[this.editingColumn])) || this.editingRow[this.editingColumn] < 0 || this.editingRow[this.editingColumn] > 31) {
                ElMessage.error('考勤信息不合法！');
                // 取消操作，恢复原始工资
                this.editingRow[this.editingColumn] = this.originalData;
                this.editingRow = null;
                this.originalData = null;
                return;
            }
            /*要是post数据中有departmentName就好了 */
            this.$confirm(`确认修改${this.currentEmployeeId}员工-${this.editingRow.Year}年${this.editingRow.Month}月的${this.columnLabels[this.editingColumn]}为${this.editingRow[this.editingColumn]}？`, "提示", {
                confirmButtonText: "确认",
                cancelButtonText: "取消",
                type: "warning",
            })
                .then(async () => {
                    console.log('newdata' + this.editingRow[this.editingColumn])
                    // 成功后更新数据
                    try {
                        const response = await put('/api/AttendanceInformation/Update', {
                            attendanceId: this.editingRow.AttendanceId,
                            [this.editingColumn]: this.editingRow[this.editingColumn],
                        });
                        console.log(response);
                    } catch (error) {
                        console.error('Error in then:', error);
                    } finally {
                        this.editingRow = null;
                        this.editingColumn = null;
                        this.originalData = null;
                    }
                })
                .catch(() => {
                    // 取消操作，恢复原始工资
                    if (this.editingRow && this.editingColumn && this.originalData) {
                        console.log('editingColumn:' + this.editingColumn);
                        console.log('originalData:' + this.originalData);
                        this.editingRow[this.editingColumn] = this.originalData;
                    }
                    this.editingRow = null;
                    this.editingColumn = null;
                    this.originalData = null;
                });
        },
    },

    created() {
        // 每分钟检查一次是否需要解锁按钮
        this.checkResetButtonStatus();
        setInterval(this.checkResetButtonStatus, 60000); // 每分钟的毫秒数
    }
}
</script>

<style scoped>
.check-attendance {
    padding: 20px;
    background-image: linear-gradient(-45deg, #97cba7, #9ab7e3);
    height: 155vh;
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

.search-select {
    margin-left: 20%;
    margin-right: 40px;
    width: 16%;
}

.search-input {
    width: 25%;
}

::v-deep .el-select {
    --el-select-border-color-hover: #324157;
    --el-select-input-focus-border-color: white;
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

::v-deep .el-select .el-select__caret {
    font-size: 30px;
    color: #324157;
}

.table-container {
    background-color: #196883;
    padding: 20px;
    border-radius: 15px;
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

.row-employee-id {
    font-size: large;
    color: #196883;
}

.row-name {
    font-size: large;
    color: #196883;
}

.row-department-name {
    font-size: large;
    color: #a76200;
}

.row-post-name {
    font-size: large;
    color: #a76200;
}

.view-button {
    font-size: 5px;
    margin-left: 10px;
    width: 75%;
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

.employee-info {
    display: flex;
    justify-content: center;
    font-size: x-large;
    font-weight: bold;
    color: #324157;
}

.view-dialog-table-container {
    background-color: #196883;
    padding: 20px;
    border-radius: 15px;
}

.view-dialog-table {
    width: 100%;
    height: 470px;
    background: #196883;
    border-radius: 5px;
}

.view-dialog-row-year {
    font-size: large;
    color: #a76200;
}

.view-dialog-row-month {
    font-size: large;
    color: #a76200;
}

.view-dialog-row-expect-attendance-days {
    font-size: large;
    color: #196883;
}

.view-dialog-row-actual-attendance-days {
    font-size: large;
    color: #196883;
}

.view-dialog-row-personal-leave-days {
    font-size: large;
    color: #196883;
}

.view-dialog-row-sick-leave-days {
    font-size: large;
    color: #196883;
}

.view-dialog-row-late-days {
    font-size: large;
    color: #196883;
}

.view-dialog-row-early-departure-days {
    font-size: large;
    color: #196883;
}

.view-dialog-row-absent-days {
    font-size: large;
    color: #196883;
}

::v-deep .el-input-number__decrease {
    color: #196883;
    background-color: rgb(255, 255, 255, 0);
}

::v-deep .el-input-number__decrease.is-disabled {
    color: #686868;
}

::v-deep .el-input-number__increase {
    color: #196883;
    background-color: rgb(255, 255, 255, 0);
}

::v-deep .el-input-number__increase.is-disabled {
    color: #686868;
}

.delete-button {
    margin-left: 10px;
    width: 100%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #b02a08;
}

.delete-button:hover {
    background-color: #ff7350;
}

.view-dialog-buttons {
    display: flex;
    justify-content: center;
    margin-top: 30px;
}

.view-dialog-add-button {
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

.view-dialog-add-button:hover {
    background-color: #ccffc3;
}

.view-dialog-close-button {
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

.view-dialog-close-button:hover {
    background-color: #ffadad;
}

::v-deep .el-form-item {
    margin-bottom: 40px;
}

::v-deep .el-form-item__label {
    font-size: x-large;
    font-weight: lighter;
    color: #196883;
}

.add-dialog-form-input {
    width: 75%;
}

.add-dialog-buttons {
    display: flex;
    justify-content: center;
    margin-top: 30px;
}

.add-dialog-submit-button {
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

.add-dialog-submit-button:hover {
    background-color: #ccffc3;
}

.add-dialog-cancel-button {
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

.add-dialog-cancel-button:hover {
    background-color: #ffadad;
}

#map {
    margin: 30px auto;
    width: 98.5%;
    height: 500px;
    border: 10px solid #196883;
    border-radius: 15px;
}

.time {
    font-size: xx-large;
    font-weight: lighter;
    color: #196883;
}

.check-button {
    margin-top: 10px;
    width: 30%;
    height: 50px;
    border-radius: 10px;
    border: 0;
    background-image: linear-gradient(to right, #30cfd0, #330867);
}

.check-button:hover {
    background-image: linear-gradient(to right, #90de83, #00929b);
}

.check-button.is-disabled {
    background-color: #ccc;
    /* 设置禁用状态下的背景颜色为灰色 */
    cursor: not-allowed;
    /* 修改鼠标样式为不可点击 */
}
</style>