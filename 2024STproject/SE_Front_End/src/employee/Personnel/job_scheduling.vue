<template>
    <div class="job-scheduling">
        <img src="../../assets/personnel.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 人事管理 </label>
            <label class="title-content"> 岗位调度 </label>
        </div>

        <div class="search-container">
            <el-select v-model="searchBy" placeholder="请选择查询方式" class="search-select">
                <el-option label="员工号" value="EmployeeId"></el-option>
                <el-option label="员工姓名" value="Name"></el-option>
            </el-select>

            <el-input
                v-if="searchBy == 'EmployeeId'"
                v-model="searchEmployeeID"
                placeholder="输入员工号查询"
                clearable
                @clear="clearSearchE"
                class="search-input"
            ></el-input>
            <el-input
                v-else-if="searchBy == 'Name'"
                v-model="searchName"
                placeholder="输入员工姓名查询"
                clearable
                @clear="clearSearchE"
                class="search-input"
            ></el-input>
            <el-button type="primary" @click="addDialogVisible=true" class="add-button">
                <el-icon><Plus /></el-icon>添加员工
            </el-button>
        </div>

        <div class="table-container">
            <el-table :data="filteredEmployees" class="table" :header-cell-style="{
                'padding': '20px',
                'font-size': 'x-large',
                'font-weight': 'lighter',
                'color': '#ffffff',
                'background-color': '#196883'
            }">
                <el-table-column label="员工ID" prop="EmployeeId" sortable>
                    <template #default="scope">
                        <label class="row-employee-id"> {{ scope.row.employeeId }} </label>
                    </template>
                </el-table-column>
                <el-table-column label="员工姓名" prop="Name">
                    <template #default="scope">
                        <label class="row-name"> {{ scope.row.name }} </label>
                    </template>
                </el-table-column>
                <el-table-column label="所属部门" prop="DepartmentName" column-key="department"
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
                        <label class="row-department-name"> {{ scope.row.departmentName }} </label>
                    </template>
                </el-table-column>
                <el-table-column label="岗位名称" prop="PostName">
                    <template #default="scope">
                        <label class="row-post-name"> {{ scope.row.postName }} </label>
                    </template>
                </el-table-column>
                <el-table-column label="操作" width="350" align="center">
                    <template #default="scope" >
                        <el-button icon="refresh-right" @click="openDialog(scope.row)" class="schedule-button"> 岗位调度 </el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>

        <!--员工岗位信息总览，提供调度入口-->
        <!--选择某员工进行岗位调度时弹出对话框-->
        <el-dialog v-model="dialogVisible" title="岗位调度" width="70%">
            <el-input
                v-model="searchDepartmentID"
                placeholder="搜索部门ID"
                clearable
                @clear="clearSearch"
                class="schedule-dialog-input"
            ></el-input>

            <div class="schedule-dialog-table-container">
                <el-table :data="filteredPosts" :header-cell-style="{
                    'padding': '20px',
                    'font-size': 'x-large',
                    'font-weight': 'lighter',
                    'color': '#ffffff',
                    'background-color': '#196883'
                }">
                    <el-table-column label="部门ID" prop="DepartmentId" width="140">
                        <template #default="scope">
                            <label class="schedule-dialog-row-department-id"> {{ scope.row.departmentId }} </label>
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
                            <label class="schedule-dialog-row-department-name"> {{ scope.row.departmentName }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="部门人数" prop="NumberOfPeople">
                        <template #default="scope">
                            <label class="schedule-dialog-row-number-of-people"> {{ scope.row.numberOfPeople }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="岗位名称" prop="PostName">
                        <template #default="scope">
                            <label class="schedule-dialog-row-post-name"> {{ scope.row.postName }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="岗位级别" prop="PositionLevel">
                        <template #default="scope">
                            <label class="schedule-dialog-position-level"> {{ scope.row.positionLevel }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="操作">
                        <template #default="scope">
                            <el-button icon="right" @click="confirmDispatch(scope.row)" class="schedule-dialog-schedule-button"> 调度 </el-button>
                        </template>
                    </el-table-column>
                </el-table>
            </div>
        </el-dialog>

        <el-dialog v-model="addDialogVisible" title="添加员工" width="65%">
            <el-form ref="formRef" inline="true" :model="form" label-width="140px" :rules="rules">
                <el-form-item label="员工姓名" prop="name" required>
                    <el-input v-model="form.name" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="性别" prop="sex" required>
                    <el-radio-group v-model="form.sex">
                        <el-radio label="男" value="男"/>
                        <el-radio label="女" value="女"/>
                    </el-radio-group>
                </el-form-item>
                <el-form-item label="年龄" prop="age" required>
                    <el-input-number v-model.number="form.age" :min="18" :max="100" class="add-dialog-form-input-number"></el-input-number>
                </el-form-item>
                <el-form-item label="岗位" prop="postId" required>
                    <el-select v-model="form.postId" placeholder="请选择岗位" class="post-search-select">
                        <el-option label="总经理" value=1></el-option>
                        <el-option label="厨师长" value=2></el-option>
                        <el-option label="财务总监" value=3></el-option>
                        <el-option label="人事部总监" value=4></el-option>
                        <el-option label="HR" value=5></el-option>
                        <el-option label="前台" value=6></el-option>
                        <el-option label="保安" value=7></el-option>
                        <el-option label="保洁" value=8></el-option>
                        <el-option label="维修工人" value=9></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="电话号码" prop="phoneNumber" required>
                    <el-input v-model="form.phoneNumber" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="底薪" prop="basePay" required>
                    <el-input v-model.number="form.basePay" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="员工账号" prop="account" required>
                    <el-input v-model="form.account" class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="密码" prop="password" required>
                    <el-input v-model="form.password" type="password" show-password class="add-dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="银行名称" prop="bankName" required>
                    <el-autocomplete
                        v-model="form.bankName"
                        :fetch-suggestions="fetchSuggestions"
                        placeholder="请输入银行名称"
                    ></el-autocomplete>
                </el-form-item>
                <el-form-item label="银行卡号" prop="creditCardNumber" required >
                    <el-input v-model="form.creditCardNumber" class="add-dialog-form-input"></el-input>
                </el-form-item>
            </el-form>

            <div class="add-dialog-buttons">
                <button class="add-dialog-submit-button" @click="addEmployee"> 提交 </button>
                <button class="add-dialog-cancel-button" @click="addDialogVisible = false"> 取消 </button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import { ElMessage } from 'element-plus';
import { put,get,post, del } from '../../axios/axiosConfig.js'
import { fetchEmployeeData,fetchDepartmentData } from '../../axios/job_scheduling.js'
import { Plus,Search} from '@element-plus/icons-vue';
import {ref} from 'vue'

    export default {
        data() {
            return {
                employees: [{}],
                dialogVisible: false,
                searchBy:'EmployeeId',
                searchEmployeeID: "",
                searchName:'',
                searchDepartmentID: "",
                addDialogVisible:false,
                departments: [{}],
                form:{
                    name: "",
                    account: "",
                    sex: "",
                    age: ref(1),
                    postId: "",
                    entryTime: new Date().toISOString(),
                    phoneNumber: "",
                    basePay: ref(0),
                    password: "",
                    bankName: '',
                    creditCardNumber: ""
                },
                rules: {
                    name: [{ required: true, message: '请输入正确的员工姓名', trigger: 'blur' },
                            {validator: this.validateName,}],
                    account: [{ required: true, message: '请输入账号', trigger: 'blur' }],
                    sex: [{ required: true, message: '请选择员工性别', trigger: 'blur' }],
                    postId: [{ required: true, message: '请输入正确的岗位id', trigger: 'blur'}],
                    phoneNumber: [{ required: true, message: '请输入正确的电话号码', trigger: 'blur',pattern: /^1[3456789]\d{9}$/ }],
                    basePay: [{ required: true, message: '请输入正确的底薪', trigger: 'blur',pattern:/^[0-9]*$/}],
                    password: [{ required: true, message: '请输入密码', trigger: 'blur' }],
                    bankName: [{ required: true, message: '请选择银行', trigger: 'blur' }],
                    creditCardNumber: [{ required: true, message: '请输入银行卡密码', trigger: 'blur' }],
                },
                restaurants: [
                    { value: '中国农业银行', link: '中国农业银行' },
                    { value: '中国工业银行', link: '中国工业银行' },
                    { value: '中国银行', link: '中国银行' },
                    { value: '招商银行', link: '招商银行' },], 
    
            }
        },
        async mounted() {
            // 从后端获取employees,departments数据
            this.employees=await fetchEmployeeData();
            this.departments=await fetchDepartmentData();
        },
        methods: {
            validateName(rule,value,callback){
                // 定义一个正则表达式来匹配汉字字符
                const chineseCharacterRegex = /^[\u4e00-\u9fa5]+$/;
                if (chineseCharacterRegex.test(value) && value.length
                 === value.match(/[\u4e00-\u9fa5]/g).length) {
                    callback();
                } else {
                    callback(new Error('请输入汉字'));
                }
            },


            openDialog(employee) {
                console.log(1111111111119999999999999)
                console.log(employee)
                this.dialogVisible = true;
                this.filteredPositions = this.departments.filter(position => position.departmentId === employee.departmentId);
                this.selectedEmployee = employee;
            },
            /*是否确认调度提示*/
            async confirmDispatch(post) {
                const { employeeId, name } = this.selectedEmployee;
                const message = `确认将员工${name}（员工ID: ${employeeId}）调度到${post.postName}岗位？`;
 
                this.$confirm(message, "提示", {
                    confirmButtonText: "确认",
                    cancelButtonText: "取消",
                    type: "warning",
                })
                .then(async () => {
                    try {
                        console.log(this.selectedEmployee)
                        const targetEmployee = await get('/api/Employee/GetByEmpId/' + this.selectedEmployee.employeeId, {});
                        // 在相同的作用域内访问 targetEmployee
                        targetEmployee.postId = post.postId;
                        console.log(targetEmployee);
                       
                        await put('/api/Employee/Update', targetEmployee)
                            .then(response => {
                            console.log(response);
                            console.log(typeof response);
                            if (response.token) {
                                localStorage.setItem('token', response.token);
                            }  
                            })
                            .catch(error => {
                            console.error(error);
                            });

                        ElMessage.success('岗位调度成功');
                        // 更新前端员工的部门和岗位信息
                        this.dialogVisible = false;
                        const UpdateDepartment = await get('/api/Department/Get/' + post.departmentId, {});
                        this.selectedEmployee.departmentName = UpdateDepartment.data.departmentName;
                        this.selectedEmployee.departmentId = post.departmentId;
                        this.selectedEmployee.postName = post.postName;
                        this.selectedEmployee.postId = post.postId;
                        this.selectedEmployee = null;
                        } catch (error) {
                        console.error(error);
                        }
                })
                .catch(() => {
                    // 用户取消或关闭弹窗，仍返回对话框
                });
            },
            /*清除搜索id*/
            clearSearchE() {
                this.searchEmployeeID = "";
                this.searchName = "";
            },
            clearSearchD() {
                this.searchDepartmentID = "";
            },
            /*自动补全输入框选项*/
            fetchSuggestions(queryString, cb) {
                const results = queryString
                    ? this.restaurants.filter(this.createFilter(queryString))
                    : this.restaurants
                // call callback function to return suggestions
                cb(results)
            },
            createFilter(queryString){
                return (restaurant) => {
                    return (
                    restaurant.value.indexOf(queryString) != -1
                    )
                }
            },
            /*添加员工 */
            async addEmployee(){
                console.log(this.form.age);
                console.log(this.form.bankName);
                this.$refs.formRef.validate(async valid => {
                    if (valid) {
                        await post('/api/Employee/Add',this.form)
                            .then(response => {
                                console.log(response);
                                if(response.status===true)
                                    ElMessage.success('添加成功');
                                else
                                    ElMessage.error('添加失败');
                            })
                            .catch(error => {
                                console.error(error);
                                ElMessage.error('添加失败，请稍后重试');
                            })
                        this.form = {};
                        this.addDialogVisible = false;
                        this.employees=await fetchEmployeeData();
                    }
                    else{
                        // 表单验证不通过，显示错误提示
                        this.$message.error('请填写正确的表单数据');
                    }
                });
            },
            async deleteEmployee(employee){
                console.log(employee);
                const message = `确认删除员工${employee.name}（员工ID: ${employee.employeeId}）？`;
                this.$confirm(message, "提示", {
                    confirmButtonText: "确认",
                    cancelButtonText: "取消",
                    type: "warning",
                })
                .then(async () => {
                    await del('/api/Employee/Delete/'+employee.employeeId,{})
                    .then(async response => {
                        console.log(response);
                        ElMessage.success('删除成功！');
                        this.employees=await fetchEmployeeData();
                    })
                    .catch(error => {
                        console.error(error);
                    })
                })
                .catch(()=>{

                })
            },
            /*筛选过滤处理函数*/
            filterHandler(value, row, column) {
                const property = column['property']
                return row[property] === value
            },
        },
        computed: {
            /*搜索部门ID处理*/
            filteredPosts() {
                if(this.searchDepartmentID)
                    return this.departments.filter(department => department.departmentId.toString().includes(this.searchDepartmentID));
                return this.departments;
            },
            /*搜索员工ID处理*/
            filteredEmployees() {
                if(this.searchBy==='EmployeeId'){
                    if (this.searchEmployeeID) {
                        return this.employees.filter(employee => employee.employeeId.toString().includes(this.searchEmployeeID));
                    }
                }
                else if(this.searchBy==='Name'){
                    if (this.searchName) {
                        return this.employees.filter(employee => employee.name.includes(this.searchName));
                    }
                }
                return this.employees;
            },
        },
    }
</script>

<style scoped>
.job-scheduling {
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
    margin-left: 20%;
}
.search-select {
	width: 21%;
    margin-right: 40px;
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
    --el-select-input-focus-border-color: #white;
}
::v-deep .el-select .el-select__caret {
    font-size: 30px;
    color: #324157;
}
.add-button {
    margin-left: 40px;
    width: 90px;
    height: 90px;
    border: 2px solid white;
    border-radius: 90px;
    background-color: #198359;
}
.add-button:hover {
    background-color: #9fd44b;
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
.table {
    width: 100%;
    height: 550px;
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
.schedule-button {
    margin-left: 10px;
    width: 37%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #196883;
}
.schedule-button:hover {
    background-color: #2aba98;
}
.delete-button {
    margin-left: 10px;
    width: 37%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #b02a08;
}
.delete-button:hover {
    background-color: #ff7350;
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
.schedule-dialog-input {
    margin-left: 25%;
    margin-bottom: 30px;
    width: 50%;
}
.schedule-dialog-table-container {
    background-color: #196883;
    padding: 20px;
    border-radius: 15px;
}
.schedule-dialog-table {
    width: 100%;
    height: 470px;
    background: #196883;
    border-radius: 5px;
}
.schedule-dialog-row-department-id {
    font-size: large;
    color: #196883;
}
.schedule-dialog-row-department-name {
    font-size: large;
    color: #196883;
}
.schedule-dialog-row-number-of-people {
    font-size: large;
    color: #196883;
}
.schedule-dialog-row-post-name {
    font-size: large;
    color: #a76200;
}
.schedule-dialog-position-level {
    font-size: large;
    color: #a76200;
}
.schedule-dialog-schedule-button {
    margin-left: 10px;
    width: 65%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #196883;
}
.schedule-dialog-schedule-button:hover {
    background-color: #2aba98;
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
    width: 100%;
}
.add-dialog-form-input-number {
    width: 100%;
}
.post-search-select {
	width: 50%;
    margin-right: 40px;
}
::v-deep .el-radio__label {
    font-size: x-large;
    color: #324157;
}
::v-deep .el-radio__input.is-checked .el-radio__inner {
    background-color: #00929b;
    border: 0;
}
::v-deep .el-radio__input.is-checked + .el-radio__label {
    color: #00929b;
}
::v-deep .el-input-number__decrease {
    color: #196883;
    background-color: rgb(255, 255, 255, 0);
}
::v-deep .el-input-number__decrease:hover {
    color: white;
}
::v-deep .el-input-number__decrease.is-disabled {
    color: red;
}
::v-deep .el-input-number__increase {
    color: #196883;
    background-color: rgb(255, 255, 255, 0);
}
::v-deep .el-input-number__increase.is-disabled {
    color: red;
}
::v-deep .el-input-number__increase:hover {
    color: white;
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
</style>