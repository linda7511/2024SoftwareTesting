<template>
	<div class="consumption-records">
        <img src="../../assets/material.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 物资管理 </label>
            <label class="title-content"> 消耗记录 </label>
        </div>

		<div class="search-container">
			<el-select v-model="query.searchType" class="search-select">
                <el-option label="通过名称搜索" value="GoodsName"></el-option>
            </el-select>

            <el-input v-model="query.item" placeholder="关键词" class="search-input"></el-input>
			<el-button type="primary" :icon="Search" @click="handleSearch" class="search-button"> 搜索 </el-button>
      <el-button type="primary"  @click="handleReset" class="search-button"> 重置 </el-button>
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
				<el-table-column prop="GoodsId" label="ID">
                    <template #default="scope">
                        <label class="row-goods-id"> {{ scope.row.consumeId }} </label>
                    </template>
                </el-table-column>
				<el-table-column prop="GoodsName" label="物品">
                    <template #default="scope">
                        <label class="row-goods-name"> {{ scope.row.goodsName }} </label>
                    </template>
                </el-table-column>
				<el-table-column prop="ConsumeQuantity" label="消耗数量">
                    <template #default="scope">
                        <label class="row-consume-quantity"> {{ scope.row.consumeAmount }} </label>
                    </template>
                </el-table-column>
				<el-table-column label="操作" align="center">
					<template #default="scope">                     
						<el-button :icon="Delete" @click="handleDelete(scope.$index)" v-permiss="16" class="delete-button">
							删除
						</el-button>
					</template>
				</el-table-column>
			</el-table>
        </div>

		<!-- 编辑弹出框 -->
		<el-dialog title="编辑" v-model="editVisible" width="30%">
			<el-form label-width="140px">
                <el-form-item label="消耗数量">
                    <el-input v-model="form.consumeAmount" class="dialog-form-input"></el-input>
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
                <el-form-item label="物品">
                    <el-input v-model="newItem.goodsName" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="消耗数量">
                    <el-input v-model="newItem.consumeAmount" class="dialog-form-input"></el-input>
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
import { get, post,put,del } from '../../axios/axiosConfig.js';

interface TableItem {
  consumeId:number,
	departmentId: number;
	goodsId: number;
	goodsName: string;
	consumeAmount: number;
}

const query = reactive({
  searchType: 'GoodsName', // 默认选择通过GoodsName搜索
	item: '',
	pageIndex: 1,
	pageSize: 10
});
const tableData = ref<TableItem[]>([]);
const pageTotal = ref(0);
// 添加获取 GoodsId 的辅助函数
const getGoodsId = async (goodsName: string) => {
  try {
    const response = await get(`/api/Good/GetByName/${goodsName}`, {
    });
    
    if (response.status === 204) {
        // 如果响应状态码为 204 No Content，显示"无结果"提示
        ElMessage.error('无结果');
        return;
    }

    const data = await response.data;
    return data.goodsId;
  } catch (error) {
    console.error('获取 GoodsId 时出错:', error);
    return null;
  }
};

const getData = async () => {
  try {
    let url = '/api/Consume/GetAll'; // 默认使用获取全部数据的接口

    if (query.searchType === 'GoodsName' && query.item) {
      const goodsId = await getGoodsId(query.item); // 获取 GoodsId
      if (goodsId !== null) {
        // 如果获取到了 GoodsId，则使用 GetByGoodsId 接口进行搜索
        url = `/Consume/GetByGoodsId/${goodsId}`;
      }
    }

    const response = await get(url, {
    });

    if (response.status === 204) {
        // 如果响应状态码为 204 No Content，显示"无结果"提示
        ElMessage.error('无结果');
        return;
    }

    const data = await response.data;

    // 在循环中，获取 GoodsName 并添加到每个数据项
    for (const item of data) {
      try {
        const goodsId = item.goodsId;
        const goodsResponse = await get(`/api/Good/Get/${goodsId}`, {
        });

        const goodsData = await goodsResponse.data;
        item.goodsName = goodsData.goodsName; // 假设返回的属性名为 "Name"

      } catch (goodsError) {
        console.error('获取商品数据时出错:', goodsError);
      }
    }

    // 将获取到的数据赋值给 tableData
    tableData.value = data;
  } catch (error) {
    console.error('获取数据时出错:', error);
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
        const deleteResponse = await del(`/api/Consume/Delete/${tableData.value[index].consumeId}`,
         {
          consumeId:tableData.value[index].consumeId
        });

        if (deleteResponse.status) {
          ElMessage.success('删除成功');
          await getData();
        } else {
          ElMessage.error('删除失败');
        }
      })
      .catch(() => {});
  } catch (error) {
    console.error('Error during deleting record:', error);
  }
};

// 表格编辑时弹窗和保存
const editVisible = ref(false);
let form = reactive({
    departmentId: 0,
    goodsId: 0,
    goodsName: '',
    consumeAmount: 0,
    consumeId:0,
});
let idx: number = -1;
const handleEdit = (index: number, row: any) => {
	idx = index;
  form.consumeId=row.consumeId;
	form.departmentId = row.departmentId;
	form.goodsId = row.goodsId;
	form.goodsName = row.goodsName;
	form.consumeAmount = row.consumeAmount;
	editVisible.value = true;
};

const saveEdit = async () => {
  try {
    let form01 = {
    departmentId: form.departmentId,
    goodsId: form.goodsId,
    consumeId:form.consumeId,
    consumeAmount: form.consumeAmount,
};
    // 发送带有存储的 Token 的编辑请求
  const editResponse = await put(`/api/Consume/Update`, form01
    );

    if (editResponse.status) {
      editVisible.value = false;
      ElMessage.success(`修改第 ${idx + 1} 行成功`);
      tableData.value[idx].departmentId = form.departmentId;
      tableData.value[idx].goodsId = form.goodsId;
      tableData.value[idx].goodsName = form.goodsName;
	    tableData.value[idx].consumeAmount = form.consumeAmount;
    } else {
      ElMessage.error('修改失败');
    }
  } catch (error) {
    console.error('Error during editing record:', error);
  }
};

const addVisible = ref(false);
const newItem = reactive<TableItem>({
  consumeId:0,
  goodsId:0,
  goodsName: '',
	consumeAmount: 0,
	departmentId: 1,
});

// 点击“新增”按钮时弹出弹窗
const handleAdd = () => {
  addVisible.value = true;

    // 清空表单数据
  Object.assign(newItem, {
    goodsId:0,
    goodsName: '',
    consumeAmount: '',
    departmentId: 1,
  });
};

// 取消新增
const cancelAdd = () => {
  addVisible.value = false;
  // 清空表单数据
  Object.assign(newItem, {
    goodsId:0,
    goodsName: '',
    consumeQuantity: '',
    departmentId: 1,
  });
};

// 保存新增数据
const saveAdd = async () => {
  try {

    // 在这里发送获取 GoodsId 的请求
    const goodsResponse = await get(`/api/Good/GetByName/${newItem.goodsName}`, {
    });
    console.log(goodsResponse.status)
    if (goodsResponse.status === false) {
      // 如果响应状态码为 204 No Content，显示"无结果"提示
      ElMessage.error('无此物品');
      return;
    }
    console.log(1111111)
    const goodsData =  goodsResponse.data;

    const newRecord = {
      goodsId: goodsData.goodsId,
      consumeAmount: newItem.consumeAmount,
      departmentId: newItem.departmentId,
    };

    const newRecord1 = {
      goodsId: goodsData.goodsId,
      goodsName: goodsData.goodsName,
      consumeAmount: newItem.consumeAmount,
      departmentId: newItem.departmentId,
    };
    const addResponse = await post('/api/Consume/Add', 
      newRecord
    );
    const isSuccess = addResponse.data;
    if (addResponse.status) {
      tableData.value.push(newRecord1);
        ElMessage.success('新增成功');
        window.location.reload();

      addVisible.value = false;

      // 清空表单数据
      Object.assign(newItem, {
        goodsId: 0,
        goodsName: '',
        inventory: 0,
      });
    } else {
      ElMessage.error('新增失败');
    }

    addVisible.value = false;

    // 清空表单数据
    Object.assign(newItem, {
      goodsId: 0,
      goodsName: '',
      inventory: 0,
    });
  } catch (error) {
    console.error('Error during adding new record:', error);
  }
};

</script>

<style scoped>
.consumption-records {
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
    color: #a76200;
}
.row-consume-quantity {
    font-size: large;
    color: #196883;
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
