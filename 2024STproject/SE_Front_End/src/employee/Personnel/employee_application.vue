<template>
    <div class="employee-application">
        <img src="../../assets/personnel.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 人事管理 </label>
            <label class="title-content"> 员工申请 </label>
        </div>

        <div class="search-container">
            <el-select v-model="query.searchType" class="search-select">
                <el-option label="员工号" value="employeeId"></el-option>
                <el-option label="申请类型" value="applicationType"></el-option>
                <el-option label="申请时间" value="applicationTime"></el-option>
            </el-select>

            <el-input v-model="query.keyword" placeholder="关键词" class="search-input"
                v-if="query.searchType === 'employeeId'"></el-input>
            <el-date-picker v-model="query.keyword" type="date" format="YYYY/MM/DD" value-format="YYYY-MM-DD" placeholder=""
                class="search-picker" v-if="query.searchType === 'applicationTime'"></el-date-picker>
            <el-select v-model="query.keyword" placeholder="" class="search-select"
                v-if="query.searchType === 'applicationType'">
                <el-option key="请假一天" label="请假一天" value="请假一天"></el-option>
                <el-option key="请假半天" label="请假半天" value="请假半天"></el-option>
                <el-option key="调休" label="调休" value="调休"></el-option>
                <el-option key="病假" label="病假" value="病假"></el-option>
                <el-option key="产假" label="产假" value="产假"></el-option>
                <el-option key="事假" label="事假" value="事假"></el-option>
                <el-option key="婚假" label="婚假" value="婚假"></el-option>
                <el-option key="丧假" label="丧假" value="丧假"></el-option>
            </el-select>

            <el-button type="primary" :icon="Search" @click="handleSearch" class="search-button"> 搜索 </el-button>
            <el-button type="primary" :icon="Plus" @click="handleAdd" class="add-button"> 新增 </el-button>
            <el-button type="primary" icon="circle-close" @click="clearSearch" class="clear-button"> 清除 </el-button>
        </div>

        <div class="table-container">
            <el-table :data="tableData" class="table" ref="multipleTable" :header-cell-style="{
                'padding': '20px',
                'font-size': 'x-large',
                'font-weight': 'lighter',
                'color': '#ffffff',
                'background-color': '#196883'
            }">
                <el-table-column prop="employeeId" label="员工号" width="140">
                    <template #default="scope">
                        <label class="row-employee-id"> {{ scope.row.employeeId }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="applicationType" label="申请类型">
                    <template #default="scope">
                        <label class="row-application-type"> {{ scope.row.applicationType }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="applicationContent" label="申请内容">
                    <template #default="scope">
                        <label class="row-application-content"> {{ scope.row.applicationContent }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="ifApprove" label="是否批准" width="160">
                    <template #default="scope">
                        <label class="row-if-approve"> {{ scope.row.ifApprove }} </label>
                    </template>
                </el-table-column>
                <el-table-column prop="applicationTime" label="申请时间">
                    <template #default="scope">
                        <label class="row-application-time"> {{ scope.row.applicationTime }} </label>
                    </template>
                </el-table-column>
                <el-table-column label="操作" align="center">
                    <template #default="scope">
                        <el-button :icon="Edit" @click="handleEdit(scope.$index, scope.row)" v-permiss="15"
                            class="edit-button">
                            编辑
                        </el-button>
                        <el-button :icon="Delete" @click="handleDelete(scope.$index)" v-permiss="16" class="delete-button">
                            删除
                        </el-button>
                    </template>
                </el-table-column>
            </el-table>
        </div>

        <!-- 编辑弹出框 -->
        <el-dialog title="编辑申请" v-model="editVisible">
            <el-form label-width="140px">
                <el-form-item label="员工号">
                    <el-input v-model="form.employeeId" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="申请类型">
                    <el-select v-model="form.applicationType" class="dialog-form-select">
                        <el-option key="请假一天" label="请假一天" value="请假一天"></el-option>
                        <el-option key="请假半天" label="请假半天" value="请假半天"></el-option>
                        <el-option key="调休" label="调休" value="调休"></el-option>
                        <el-option key="病假" label="病假" value="病假"></el-option>
                        <el-option key="产假" label="产假" value="产假"></el-option>
                        <el-option key="事假" label="事假" value="事假"></el-option>
                        <el-option key="婚假" label="婚假" value="婚假"></el-option>
                        <el-option key="丧假" label="丧假" value="丧假"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="申请内容">
                    <el-input v-model="form.applicationContent" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="是否批准">
                    <el-select v-model="form.ifApprove" placeholder=form.ifApprove class="dialog-form-select">
                        <el-option key="是" label="是" value="是"></el-option>
                        <el-option key="否" label="否" value="否"></el-option>
                        <el-option key="待批准" label="待批准" value="待批准"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="申请时间">
                    <el-date-picker v-model="form.applicationTime" type="date" format="YYYY/MM/DD" value-format="YYYY-MM-DD"
                        class="dialog-form-picker"></el-date-picker>
                </el-form-item>
            </el-form>

            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="saveEdit"> 确定 </button>
                <button class="dialog-cancel-button" @click="editVisible = false"> 取消 </button>
            </div>
        </el-dialog>

        <!-- 新增弹窗 -->
        <el-dialog title="新增申请" v-model="addVisible">
            <el-form label-width="140px">
                <el-form-item label="员工号">
                    <el-input v-model="newItem.employeeId" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="申请类型">
                    <el-select v-model="newItem.applicationType" class="dialog-form-select">
                        <el-option key="请假一天" label="请假一天" value="请假一天"></el-option>
                        <el-option key="请假半天" label="请假半天" value="请假半天"></el-option>
                        <el-option key="调休" label="调休" value="调休"></el-option>
                        <el-option key="病假" label="病假" value="病假"></el-option>
                        <el-option key="产假" label="产假" value="产假"></el-option>
                        <el-option key="事假" label="事假" value="事假"></el-option>
                        <el-option key="婚假" label="婚假" value="婚假"></el-option>
                        <el-option key="丧假" label="丧假" value="丧假"></el-option>
                    </el-select>
                </el-form-item>
                <el-form-item label="申请内容">
                    <el-input v-model="newItem.applicationContent" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="申请时间">
                    <el-date-picker v-model="newItem.applicationTime" type="date" format="YYYY/MM/DD"
                        value-format="YYYY-MM-DD" class="dialog-form-picker"></el-date-picker>
                </el-form-item>
            </el-form>

            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="saveAdd"> 确定 </button>
                <button class="dialog-cancel-button" @click="cancelAdd"> 取消 </button>
            </div>
        </el-dialog>
    </div>
</template>

<script setup lang="ts" name="basetable">
import { ref, reactive } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import { Delete, Edit, Search, Plus } from '@element-plus/icons-vue';
import { get, del, put, post } from '../../axios/axiosConfig.js';

interface TableItem {
    departmentId: number;
    applicationId: number;
    employeeId: number;
    applicationType: string;
    applicationContent: string;
    ifApprove: string;
    applicationTime: string;
}

const query = reactive({
    searchType: 'applicationType',
    keyword: '',
});

const tableData = ref<TableItem[]>([]);
const pageTotal = ref(0);

// 获取原始表格数据
const primaryData = async () => {
    try {
        const response = await get(`/api/Application/GetAll`, {});
        console.log("获取的数据", response);

        tableData.value = []; // 清空数组，使其变为空数组

        // 遍历每一组数据
        for (const responseData of response.data) {
            const primaryTableItem = {
                applicationId: responseData.applicationId,
                employeeId: responseData.employeeId,
                applicationType: responseData.applicationType,
                applicationContent: responseData.applicationContent,
                ifApprove: responseData.ifApprove,
                applicationTime: formatDate(responseData.applicationTime),
            };
            console.log("每个新增数据", primaryTableItem);
            tableData.value.push(primaryTableItem);
        }
    } catch (error) {
        console.error('Error fetching data:', error);
    }
};

const getData = () => {
    primaryData();
};

getData();

// 获取查询表格数据
const fetchData = async (queryParams) => {
    try {
        let response;

        if (queryParams.searchType === 'employeeId') {
            const empId = queryParams.keyword;
            response = await get('/api/Application/GetByEmpId/' + empId, {});
        } else if (queryParams.searchType === 'applicationType') {
            const appType = queryParams.keyword;
            response = await get(`/api/Application/GetByType/` + appType, {});
        }
        else if (queryParams.searchType === 'applicationTime') {
            const appTime = new Date(queryParams.keyword).toISOString();
            console.log("申请时间", appTime);
            response = await get(`/api/Application/GetByApplicationTimeOfDay/` + appTime, {});
        }

        tableData.value = []; // 清空数组，使其变为空数组

        if (!response) {
            ElMessage.warning('搜索结果为空'); // 提示搜索结果为空
            return; // 返回空结果，不进行后续操作
        }
        console.log(7777)
        console.log(response)
        // 遍历每一组数据
        for (const responseData of response.data) {
            const searchTableItem = {
                applicationId: responseData.applicationId,
                employeeId: responseData.employeeId,
                applicationType: responseData.applicationType,
                applicationContent: responseData.applicationContent,
                ifApprove: responseData.ifApprove,
                applicationTime: formatDate(responseData.applicationTime),
            };
            tableData.value.push(searchTableItem);
        }
    } catch (error) {
        console.error('Error fetching data:', error);
    }
};

// 查询操作
const handleSearch = () => {
    // 检查搜索关键词是否为空
    if (!query.keyword) {
        ElMessage.error('关键词不能为空');
        return;
    }

    query.pageIndex = 1;
    const queryParams = { ...query }; // 复制查询参数对象

    // 检查搜索类型是否是员工号，并且关键词是否为数字
    if (queryParams.searchType === 'employeeId') {
        const empId = Number(queryParams.keyword);
        if (isNaN(empId)) {
            ElMessage.error('搜索员工号必须是数字');
            return;
        }
    }

    fetchData(queryParams);
};

// 删除操作
const handleDelete = async (index: number) => {
    try {
        const applicationId = tableData.value[index].applicationId;
        const response = await del(`/api/Application/Delete/` + applicationId, {});
        if (response.status) {
            ElMessage.success('删除成功');
            tableData.value.splice(index, 1);
        } else {
            ElMessage.error('删除失败');
        }
    } catch (error) {
        ElMessage.error('删除时出现错误');
        console.error('删除时出现错误：', error);
    }
};

// 表格编辑时弹窗和保存
const editVisible = ref(false);
let form = reactive({
    employeeId: 1,
    applicationType: '',
    applicationContent: '',
    ifApprove: '',
    applicationTime: ''
});
let originalData = {}; // 存储原始数据
let changedFields = {}; // 存储发生变化的属性和对应的新值
let idx: number = -1;

const handleEdit = (index: number, row: any) => {
    idx = index;
    // 存储原始数据
    originalData = {
        employeeId: row.employeeId,
        applicationType: row.applicationType,
        applicationContent: row.applicationContent,
        ifApprove: row.ifApprove,
        applicationTime: row.applicationTime,
    };

    form.employeeId = row.employeeId;
    form.applicationType = row.applicationType;
    form.applicationContent = row.applicationContent;
    form.ifApprove = row.ifApprove;
    form.applicationTime = row.applicationTime;
    editVisible.value = true;
};

const saveEdit = async () => {
    try {
        editVisible.value = false;

        // 检查必填字段是否为空
        if (!form.employeeId || !form.applicationType || !form.applicationContent || !form.applicationTime) {
            ElMessage.error('所有必填字段不能为空');
            return;
        }

        // 检查员工ID是否为数字
        if (isNaN(form.employeeId)) {
            ElMessage.error('员工号必须是数字');
            return;
        }

        // 检查编辑的数据是否符合筛选条件
        if (query.searchType === 'applicationType' && form.applicationType !== originalData.applicationType) {
            ElMessage.warning('不允许编辑该内容');
            return;
        }

        if (query.searchType === 'applicationTime' && form.applicationTime !== originalData.applicationTime) {
            ElMessage.warning('不允许编辑该内容');
            return;
        }

        if (query.searchType === 'employeeId' && form.employeeId !== originalData.employeeId) {
            ElMessage.warning('不允许编辑该内容');
            return;
        }

        const applicationId = tableData.value[idx].applicationId;

        // 比较变化的字段
        changedFields.applicationType = form.applicationType;
        changedFields.applicationTime = form.applicationTime + "T12:00:00";
        changedFields.ifApprove = form.ifApprove;
        changedFields.employeeId = form.employeeId;
        changedFields.applicationContent = form.applicationContent;
        changedFields.applicationId = applicationId;

        // 发送更新请求
        const response = await put(`/api/Application/Update1`, changedFields);

        if (response) {
            // 将更新后的数据应用到 tableData 中的相应位置
            getData();
            ElMessage.success(`修改第 ${idx + 1} 行成功`);
        } else {
            ElMessage.error('修改失败');
        }
    } catch (error) {
        ElMessage.error('修改时出现错误');
        console.error('修改时出现错误：', error);
    }
};


const addVisible = ref(false);
const newItem = reactive<TableItem>({
    departmentId: 1,
    employeeId: 1,
    applicationType: '',
    applicationContent: '',
    applicationTime: '',
});

// 点击“新增”按钮时弹出弹窗
const handleAdd = () => {
    // 检查搜索类型和搜索关键词是否为空
    if (query.searchType === '' && query.keyword === '') // 两者都为空
    {
        addVisible.value = true; // 符合条件，允许新增数据
    } else {
        ElMessage.warning('请先执行清除搜索的操作'); // 不符合条件，提醒用户
        return;
    }

    // 清空表单数据
    Object.assign(newItem, {
        employeeId: 1,
        applicationType: '',
        applicationContent: '',
        applicationTime: '',
    });
};

// 取消新增
const cancelAdd = () => {
    addVisible.value = false;
    // 清空表单数据
    Object.assign(newItem, {
        employeeId: 1,
        applicationType: '',
        applicationContent: '',
        applicationTime: '',
    });
};

// 保存新增数据
const saveAdd = async () => {
    addVisible.value = false;

    // 检查必填字段是否为空
    if (!newItem.employeeId || !newItem.applicationType || !newItem.applicationContent || !newItem.applicationTime) {
        ElMessage.error('所有必填字段不能为空');
        return;
    }

    // 检查员工ID是否为数字
    if (isNaN(newItem.employeeId)) {
        ElMessage.error('员工号必须是数字');
        return;
    }

    const newapplicationtime = new Date(newItem.applicationTime).toISOString();
    const newRecord: TableItem = {
        employeeId: newItem.employeeId,
        applicationType: newItem.applicationType,
        applicationContent: newItem.applicationContent,
        departmentId: 1,
        ifApprove: "待批准",
        applicationTime: newapplicationtime,
    };

    try {
        const response = await post('/api/Application/Add', newRecord);

        console.log(777777777)
        console.log('结果:', response);

        ElMessage.success('新增成功');

        const newTableItem = {
            departmentId: response.data.departmentId,
            applicationId: response.data.applicationId,
            employeeId: response.data.employeeId,
            applicationType: response.data.applicationType,
            applicationContent: response.data.applicationContent,
            ifApprove: response.data.ifApprove,
            applicationTime: formatDate(response.data.ApplicationTime),
        };
        getData();
        Object.assign(newItem, {

            employeeId: 1,
            applicationType: '',
            applicationContent: '',
            applicationTime: '',
        });
    } catch (error) {
        ElMessage.error('新增时出现错误');
        console.error('Error adding:', error);
    }
};

const formatDate = (isoTime) => {
    const dateObj = new Date(isoTime);
    const year = dateObj.getFullYear();
    const month = String(dateObj.getMonth() + 1).padStart(2, '0');
    const day = String(dateObj.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
};

//清除搜索内容，一切回到初始状态
const clearSearch = () => {
    query.searchType = '';
    query.keyword = '';
    getData(); // 重新加载原始数据
};

</script>

<style scoped>
.employee-application {
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
    margin-left: 16%;
}

.search-select {
    width: 28%;
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

.search-button {
    margin-left: 20px;
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

.add-button {
    margin-left: 20px;
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

.clear-button {
    margin-left: 20px;
    width: 90px;
    height: 90px;
    border: 2px solid white;
    border-radius: 90px;
    background-color: #b02a08;
}

.clear-button:hover {
    background-color: #ff7350;
    /* 鼠标悬停时的颜色 */
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

.row-application-type {
    font-size: large;
    color: #a76200;
}

.row-application-content {
    font-size: large;
    color: #a76200;
}

.row-if-approve {
    font-size: large;
    color: #196883;
}

.row-application-time {
    font-size: large;
    color: #196883;
}

.edit-button {
    margin-left: 10px;
    width: 45%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #196883;
}

.edit-button:hover {
    background-color: #2aba98;
}

.delete-button {
    margin-left: 10px;
    width: 45%;
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
    width: 35%;
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
    margin-bottom: 40px;
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

.dialog-form-picker {
    width: 75%;
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
