<template>
	<div class="view-inventory">
        <img src="../../assets/material.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 物资管理 </label>
            <label class="title-content"> 查看库存 </label>
        </div>

		<div class="search-container">
			<el-select v-model="query.searchType" class="search-select">
                <el-option label="通过名称搜索" value="GoodsName"></el-option>
            </el-select>

            <el-input v-model="query.item" placeholder="关键词" class="search-input"></el-input>
			<el-button type="primary" :icon="Search" @click="handleSearch" class="search-button"> 搜索 </el-button>
		</div>

        <div class="table-container">
            <el-table :data="tableData" class="table" ref="multipleTable" :header-cell-style="{
                'padding': '20px',
                'font-size': 'x-large',
                'font-weight': 'lighter',
                'color': '#ffffff',
                'background-color': '#196883'
            }">
				<el-table-column prop="GoodsId" label="ID">
                    <template #default="scope">
                        <label class="row-goods-id"> {{ scope.row.goodsId }} </label>
                    </template>
                </el-table-column>
				<el-table-column prop="GoodsName" label="物品">
                    <template #default="scope">
                        <label class="row-goods-name"> {{ scope.row.goodsName }} </label>
                    </template>
                </el-table-column>
				<el-table-column prop="Inventory" label="库存数量">
                    <template #default="scope">
                        <label class="row-inventory"> {{ scope.row.inventory }} </label>
                    </template>
                </el-table-column>
			</el-table>
        </div>
	</div>
</template>

<script setup lang="ts" name="basetable">
import { ref, reactive } from 'vue';
import { ElMessage, ElMessageBox } from 'element-plus';
import { Delete, Edit, Search, Plus } from '@element-plus/icons-vue';
import { get, post,put,del } from '../../axios/axiosConfig.js';

interface TableItem {
	goodsId: number;
	goodsName: string;
	inventory: number;
}

const query = reactive({
  searchType: 'GoodsName', // 默认选择通过GoodsName搜索
  item: '',
  pageIndex: 1,
  pageSize: 10
});
const tableData = ref<TableItem[]>([]);
const pageTotal = ref(0);
// 获取表格数据
const getData = async () => {
  try {
    let url = '/api/Good/GetAllInfo';

    if (query.searchType === 'GoodsName' && query.item) {
      // 如果搜索类型为 'GoodsName' 并且关键词不为空，则使用 GetByName 接口进行搜索
      url = `/api/Good/GetByName/${query.item}`;
    }

    const response = await get(url, {
    });

    if (response.status === 204) {
      // 如果响应状态码为 204 No Content，显示"无结果"提示
      ElMessage.error('无结果');
      return;
    }

    const data = await response.data;

    tableData.value = Array.isArray(data) ? data : [data]; // 将单个对象包装到数组中，确保 tableData 是一个数组
    
  } catch (error) {
    console.error('获取数据时发生错误:', error);
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

</script>

<style scoped>
.view-inventory {
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
::v-deep .el-table td,.building-top .el-table th.is-leaf {
    border-bottom: 10px solid #196883;
}
.row-goods-id {
    font-size: large;
    color: #196883;
}
.row-goods-name {
    font-size: large;
    color: #196883;
}
.row-inventory {
    font-size: large;
    color: #a76200;
}
.consume-button {
    margin-left: 10px;
    width: 30%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #777e86;
}
.consume-button:hover {
    background-color: #c1cbd9;
}
.edit-button {
    margin-left: 10px;
    width: 30%;
    height: 48px;
    border: 2px solid #c1cbd9;
    background-color: #196883;
}
.edit-button:hover {
    background-color: #2aba98;
}
.delete-button {
    margin-left: 10px;
    width: 30%;
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
.dialog-form-input-number {
    width: 75%;
}
::v-deep .el-input-number__decrease {
    width: 20%;
    font-size: 35px;
    color: #196883;
    background: rgb(255, 255, 255, 0);
}
::v-deep .el-input-number__decrease:hover {
    color: white;
}
::v-deep .el-input-number__increase {
    width: 20%;
    font-size: 35px;
    color: #196883;
    background: rgb(255, 255, 255, 0);
}
::v-deep .el-input-number__increase:hover {
    color: white;
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
