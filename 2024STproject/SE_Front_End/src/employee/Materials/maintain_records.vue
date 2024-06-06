<template>
  <div class="maintain-records">
    <img src="../../assets/material.svg" class="function-icon">

    <div class="title">
      <label class="title-function"> 物资管理 </label>
      <label class="title-content"> 维护记录 </label>
    </div>

    <div class="search-container">
      <el-select v-model="query.searchType" class="search-select">
        <el-option label="通过名称搜索" value="Object"></el-option>
        <el-option label="通过房间号搜索" value="RoomId"></el-option>
      </el-select>

      <el-input v-model="query.item" placeholder="关键词" class="search-input"></el-input>
      <el-button type="primary" :icon="Search" @click="handleSearch" class="search-button"> 搜索 </el-button>
      <el-button type="primary" @click="handleReset" class="search-button"> 重置 </el-button>
      <el-button type="primary" :icon="Plus" @click="handleAdd" class="add-button"> 新增 </el-button>
    </div>

    <div class="table-container">
      <el-table :data="tableData" class="table" ref="multipleTable" :header-cell-style="{
        'padding': '20px',
        'font-size': 'x-large',
        'font-weight': 'lighter',
        'color': '#ffffff',
        'background-color': '#196883'
      }">
        <el-table-column prop="Object" label="维修物品">
          <template #default="scope">
            <label class="row-object"> {{ scope.row.maintainingItem }} </label>
          </template>
        </el-table-column>
        <el-table-column prop="RoomNum" label="房间ID" width="180">
          <template #default="scope">
            <label class="row-room-id"> {{ scope.row.roomId }} </label>
          </template>
        </el-table-column>
        <el-table-column prop="Result" label="状态" width="180">
          <template #default="scope">
            <button :class="'row-result-' + translateResult(scope.row.maintainingResult)"> {{ scope.row.maintainingResult
            }} </button>
          </template>
        </el-table-column>
        <el-table-column prop="MaintainTime" label="维修日期">
          <template #default="scope">
            <label class="row-maintain-time"> {{ scope.row.FormattedMaintainTime }} </label>
          </template>
        </el-table-column>

        <el-table-column label="操作" align="center">
          <template #default="scope">
            <el-button :icon="Edit" class="edit-button" @click="handleEdit(scope.$index, scope.row)" v-permiss="15">
              编辑
            </el-button>
            <el-button :icon="Delete" class="delete-button" @click="handleDelete(scope.$index)" v-permiss="16">
              删除
            </el-button>
          </template>
        </el-table-column>
      </el-table>
    </div>

    <!-- 编辑弹出框 -->
    <el-dialog title="编辑" v-model="editVisible" width="30%">
      <el-form label-width="140px">
        <el-form-item label="维修物品">
          <el-input v-model="form.maintainingItem" class="dialog-form-input"></el-input>
        </el-form-item>

        <el-form-item label="状态" prop="region">
          <el-select v-model="form.maintainingResult" :placeholder="form.maintainingResult" class="dialog-form-select">
            <el-option key="已维修" label="已维修" value="已维修"></el-option>
            <el-option key="已更换" label="已更换" value="已更换"></el-option>
            <el-option key="待维修" label="待维修" value="待维修"></el-option>
          </el-select>
        </el-form-item>

      </el-form>

      <div class="dialog-buttons">
        <button class="dialog-confirm-button" @click="saveEdit"> 确定 </button>
        <button class="dialog-cancel-button" @click="editVisible = false"> 取消 </button>
      </div>
    </el-dialog>

    <!-- 新增弹窗 -->
    <el-dialog title="新增数据" v-model="addVisible">
      <el-form label-width="140px">
        <el-form-item label="维修物品">
          <el-input v-model="newItem.maintainingItem" class="dialog-form-input"></el-input>
        </el-form-item>
        <el-form-item label="房间ID">
          <el-input v-model="newItem.roomId" class="dialog-form-input"></el-input>
        </el-form-item>
        <el-form-item label="状态" prop="region">
          <el-select v-model="newItem.maintainingResult" placeholder="请选择" class="dialog-form-select">
            <el-option key="已维修" label="已维修" value="已维修"></el-option>
            <el-option key="已更换" label="已更换" value="已更换"></el-option>
            <el-option key="待维修" label="待维修" value="待维修"></el-option>
          </el-select>
        </el-form-item>
        <el-form-item label="时间">
          <el-date-picker v-model="newItem.maintainingTime" type="date" format="YYYY/MM/DD" value-format="YYYY-MM-DD"
            class="dialog-form-picker"></el-date-picker>
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
import { get, post, put, del } from '../../axios/axiosConfig.js';

interface TableItem {
  maintainingItem: string;
  roomId: number;
  //RoomNum: number;
  maintainingResult: string;
  maintainingTime: string;
  employeeId: number;
  FormattedMaintainTime?: string;
}


const query = reactive({
  searchType: 'Object', // 默认选择通过名称搜索
  item: '',
  pageIndex: 1,
  pageSize: 10
});
const tableData = ref<TableItem[]>([]);
const pageTotal = ref(0);
// 获取表格数据
const getData = async () => {
  try {
    let url = '/api/Maintain/GetAll'; // 默认使用获取全部数据的接口
   
    if (query.searchType === 'Object' && query.item) {
      // 如果搜索类型为 'Object' 并且关键词不为空，则使用 GetByObject 接口进行搜索
      url = `/api/Maintain/GetByObject/${query.item}`;
    } else if (query.searchType === 'RoomId' && query.item) {
      // 如果搜索类型为 'RoomId' 并且关键词不为空，则使用 GetByRoom 接口进行搜索

      url = `/api/Maintain/GetByRoom/${query.item}`;
    }
    tableData.value=[];
    const response = await get(url, {
    });

    if (!response.status) {
      // 如果响应状态码为 204 No Content，显示"无结果"提示
      ElMessage.error('无结果');
      return;
    }

    const data =  response.data;

    // 格式化日期函数
    const formatDateToCustomFormat = (dateString: string) => {
      const date = new Date(dateString);
      const year = date.getFullYear();
      const month = String(date.getMonth() + 1).padStart(2, '0'); // 月份从 0 开始，需要加 1
      const day = String(date.getDate()).padStart(2, '0');
      return `${year}-${month}-${day}`;
    };

    // 将获取到的数据赋值给 tableData
    tableData.value = data;
  } catch (error) {
    console.error('Error fetching data:', error);
  }
};

getData();

const handleSearch = () => {
  getData(); // 在点击搜索按钮时调用搜索功能
};

const handleReset = () => {
  query.item = ''; // 清空搜索关键词
  getData(); // 重置后获取所有数据
};

// 分页导航
const handlePageChange = (val: number) => {
  query.pageIndex = val;
  getData();
};


// 删除操作
const handleDelete = async (index: number) => {
  try {
    // 二次确认删除
    ElMessageBox.confirm('确定要删除吗？', '提示', {
      type: 'warning'
    })
      .then(async () => {
        // 发送带有存储的 Token 的删除请求
        const deleteResponse = await del(`/api/Maintain/Delete/${tableData.value[index].roomId}/${tableData.value[index].employeeId}/${tableData.value[index].maintainingTime}`, {
        });

        if (deleteResponse.status) {
          //ElMessage.success('删除成功');
          tableData.value.splice(index, 1);
          location.reload();

          ElMessage.success('删除成功');
        } else {
          ElMessage.error('删除失败');
        }
      })
      .catch(() => { });
  } catch (error) {
    console.error('Error during deleting record:', error);
  }
};

// 表格编辑时弹窗和保存
const editVisible = ref(false);
let form = reactive({
  maintainingItem: '',
  roomId: 0,
  maintainingResult: '',
  maintainingTime: '',
  employeeId: 0,
});
let idx: number = -1;
const handleEdit = (index: number, row: any) => {
  idx = index;
  form.maintainingItem = row.maintainingItem;
  form.roomId = row.roomId;
  form.maintainingResult = row.maintainingResult;
  form.maintainingTime = row.maintainingTime;
  form.employeeId = row.employeeId;
  editVisible.value = true;
};
const saveEdit = async () => {
  try {

    // 发送带有存储的 Token 的编辑请求
    const editResponse = await put(`/api/Maintain/Update`, {
      maintainingItem : form.maintainingItem,
      roomId : form.roomId,
      maintainingResult : form.maintainingResult,
      maintainingTime : form.maintainingTime,
      employeeId : form.employeeId
      
    });

    if (editResponse.status) {
      editVisible.value = false;
      ElMessage.success(`修改第 ${idx + 1} 行成功`);
      tableData.value[idx].maintainingItem = form.maintainingItem;
      tableData.value[idx].roomId = form.roomId;
      tableData.value[idx].maintainingResult = form.maintainingResult;
      tableData.value[idx].maintainingTime = form.maintainingTime;
    } else {
      ElMessage.error('修改失败');
    }
  } catch (error) {
    console.error('Error during editing record:', error);
  }
};

const addVisible = ref(false);
const newItem = reactive({
  maintainingItem: '',
  roomId: 0,
  maintainingResult: '',
  maintainingTime: '',
  employeeId: 1,
});

// 点击“新增”按钮时弹出弹窗
const handleAdd = () => {
  addVisible.value = true;

  // 清空表单数据
  Object.assign(newItem, {
    maintainingItem: '',
    roomId: '',
    maintainingResult: '',
    maintainingTime: '',
    employeeId: 1,
  });
};

// 取消新增
const cancelAdd = () => {
  addVisible.value = false;
  // 清空表单数据
  Object.assign(newItem, {
    maintainingItem: '',
    roomId: '',
    maintainingResult: '',
    maintainingTime: '',
    employeeId: 1,
  });
};

// 保存新增数据
const saveAdd = async () => {
  try {

    const newRecord = {
      maintainingItem: newItem.maintainingItem,
      maintainingResult: newItem.maintainingResult,
      maintainingTime: newItem.maintainingTime,
      employeeId: newItem.employeeId,
    };
    let timenow = newItem.maintainingTime + "T12:00:00";
    console.log(111111)
    console.log(timenow)
    const newRecord1 = {
      maintainingItem: newItem.maintainingItem,
      roomId: newItem.roomId,
      maintainingResult: newItem.maintainingResult,
      maintainingTime: newItem.maintainingTime,
      employeeId: newItem.employeeId,
    };

    const addResponse = await post('/api/Maintain/Add', {

      maintainingItem: newItem.maintainingItem,
      roomId: newItem.roomId,
      maintainingResult: newItem.maintainingResult,
      maintainingTime: timenow,
      employeeId: newItem.employeeId,
    });
    console.log(111111111111111)
    console.log(addResponse)
    if (addResponse.status) {
      tableData.value.push(newRecord1);
      location.reload();

      ElMessage.success('新增成功');
    } else {
      ElMessage.error('新增失败');
    }

    addVisible.value = false;

    // 清空表单数据
    Object.assign(newItem, {
      maintainingItem: '',
      roomId: '',
      maintainingResult: '',
      maintainingTime: '',
      employeeId: 1,
    });
  } catch (error) {
    console.error('Error during adding new record:', error);
  }
};

//将状态转为对应英文（调整样式用）
const translateResult = (result: string) => {
  switch (result) {
    case "已维修":
      return "repaired";
      break;
    case "已更换":
      return "replaced";
      break;
    case "待维修":
      return "to-be-repaired";
      break;
    default:
      break;
  }
}

</script>

<style scoped>
.maintain-records {
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

.row-object {
  font-size: large;
  color: #196883;
}

.row-room-id {
  font-size: large;
  color: #196883;
}

.row-result-repaired {
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

.row-result-replaced {
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

.row-result-to-be-repaired {
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

.row-maintain-time {
  font-size: large;
  color: #a76200;
}

.edit-button {
  margin-left: 10px;
  width: 37%;
  height: 48px;
  border: 2px solid #c1cbd9;
  background-color: #196883;
}

.edit-button:hover {
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
