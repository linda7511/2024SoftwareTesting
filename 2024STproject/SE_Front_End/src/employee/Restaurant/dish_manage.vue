<template>
    <div class="dish-manage">
        <img src="../../assets/restaurant.svg" class="function-icon">
        
        <!-- 餐厅标题 -->
        <div class="title">
            <label class="title-function"> 餐厅接待 </label>
            <label class="title-content"> 菜品管理 </label>
        </div>

        <!-- 得到菜品信息后显示 -->
        <template v-if="isGetDishData">
            <!-- 菜品搜索 添加 -->
            <div class="search-container">
                <label> 搜索： </label>
                <el-select v-model="searchType" class="search-select">
                    <el-option label="通过菜品名称搜索" value="dishName"></el-option>
                    <el-option label="通过菜品口味搜索" value="dishTaste"></el-option>
                </el-select>

                <el-input
                    v-model="keyword"
                    placeholder="请输入关键词"
                    clearable
                    class="search-input"
                ></el-input>
                <el-button icon="plus" @click="handleAdd" class="add-button"> 新增 </el-button>
            </div>

            <!-- 菜品展示表格 -->
            <div class="table-container">
                <el-table :data="dishShow" class="table" header-cell-class-name="table-header" :header-cell-style="{
                    'padding': '20px',
                    'font-size': 'x-large',
                    'font-weight': 'lighter',
                    'color': '#ffffff',
                    'background-color': '#196883'
                }">
                    <el-table-column prop="dishName" label="菜品名称">
                        <template #default="scope">
                            <label class="row-dish-name"> {{ scope.row.dishName }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="dishPrice" label="菜品价格">
                        <template #default="scope">
                            <label class="row-dish-price"> {{ scope.row.dishPrice }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="dishTaste" label="菜品口味"
                        :filters="val['dishTaste']"
                        :filter-method="filterHandler">
                        <template #default="scope">
                            <label class="row-dish-taste"> {{ scope.row.dishTaste }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column label="操作" align="center">
                        <template #default="scope">                     
                            <el-button icon="edit" @click="handleEdit(scope.$index, scope.row, $event)" v-permiss="15" class="edit-button">
                                编辑
                            </el-button>
                            <el-button icon="delete" @click="handleDelete(scope.$index, $event)" v-permiss="16" class="delete-button">
                                删除
                            </el-button>
                        </template>
                    </el-table-column>      
                </el-table>  
            </div>
        </template>

        <!-- 没得到菜品信息显示 -->
        <template v-else>
            <div class="error-tip">
                <span class="error-text"> 加载中... </span>
                <el-icon class="error-icon"><Loading /></el-icon>
            </div>
        </template>

        <!-- 编辑菜品对话框 -->
        <el-dialog title="编辑" v-model="editVisible">
            <el-form label-width="140px">
                <el-form-item label="菜品名称">
                    <el-input v-model="tempDish.dishName" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="菜品价格">
                    <el-input-number v-model.number="tempDish.dishPrice" :min="0" class="dialog-form-input-number"></el-input-number>
                </el-form-item>
                <el-form-item label="菜品口味">
                    <el-input v-model="tempDish.dishTaste" class="dialog-form-input"></el-input>
                </el-form-item>
            </el-form>

            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="saveEdit"> 确定 </button>
                <button class="dialog-cancel-button" @click="editVisible = false"> 取消 </button>
            </div>
        </el-dialog>

        <!-- 增加菜品对话框 -->
        <el-dialog title="新增" v-model="addVisible">
            <el-form label-width="140px">
                <el-form-item label="菜品名称">
                    <el-input v-model="tempDish.dishName" placeholder="请输入" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="菜品价格">
                    <el-input-number v-model.number="tempDish.dishPrice" :min="0" placeholder="请输入" class="dialog-form-input-number"></el-input-number>
                </el-form-item>
                <el-form-item label="菜品口味">
                    <el-input v-model="tempDish.dishTaste" placeholder="请输入" class="dialog-form-input"></el-input>
                </el-form-item>
            </el-form>

            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="saveAdd"> 确定 </button>
                <button class="dialog-cancel-button" @click="cancelAdd"> 取消 </button>
            </div>
        </el-dialog>
    </div>
</template>

<script>
import { ElMessage, ElMessageBox } from 'element-plus';
import { get, del, put, post } from '../../axios/axiosConfig.js'
    export default {
        data() {
            return {
                searchType: 'dishName',     // 搜索类型 DishName DishType
                keyword: '',                // 搜索关键词
                dish: [],                   // 菜品数据信息
                val: {'DishTaste': []},     // 用于菜品分类显示
                tempDish: {},               // 用于添加菜品的临时变量
                editVisible: false,         // 控制编辑对话框是否可见
                addVisible: false,          // 控制添加对话框是否可见
                isGetDishData: false,       // 反馈是否得到菜品信息
                editIndex: 0,               // 记录编辑菜品信息的索引
            }
        },
        methods: {
            // 限制显示字数 将过长字符串转换为"16个字符 ..."
            limitWord(val) {
                if (!val) 
                    return "";
                const maxLen = 16;
                if (val.length > maxLen) {
                    return val.slice(0, maxLen) + "...";
                }
                return val;
            },
            // 点击删除按钮
            handleDelete(index, e) {
                // 二次确认是否删除
                ElMessageBox.confirm('确定要删除吗？', '提示', {
                    type: 'warning',
                    confirmButtonText: '确定',
                    cancelButtonText: '取消',
                }).then(() => {
                    del(`/api/Dish/Delete/${this.dish[index]['dishId']}`, {})
                        .then((response) => {
                            console.log(`delete dish ${response}`);
                            // 删除不成功消息提示
                            if (!response) {
                                ElMessage.error('删除失败，请稍后重试');
                                return;
                            }
                            // 删除成功前端列表删除
                            ElMessage.success('删除成功');
                            this.dish.splice(index, 1);
                        })
                        .catch((error) => {
                            // 删除失败消息提示
                            ElMessage.error('删除失败，请稍后重试');
                            console.log(error);
                            return;
                        });
                }).catch(() => {});
                // 消除点击后的焦点
                if (e.target.nodeName == "SPAN" || e.target.nodeName == "I") {
                    e.target.parentNode.blur();
                } else {
                    e.target.blur();
                }
            },
            // 点击编辑按钮 传入索引 该行信息 事件
            handleEdit(index, row, e) {
                // 浅拷贝到临时变量
                this.tempDish = {...row}
                // 消息提示
                ElMessage.info(`修改第${index}行`)
                // 记录修改索引
                this.editIndex = index;
                // 显示编辑对话框
                this.editVisible = true;
                // 消除点击后的焦点
                if (e.target.nodeName == "SPAN" || e.target.nodeName == "I") {
                    e.target.parentNode.blur();
                } else {
                    e.target.blur();
                }
            },
            // 保存编辑
            saveEdit() {
                // 向后端传输更改信息
                put('/api/Dish/Update', this.tempDish)
                    .then((response) => {
                        console.log(`update dish ${response}`);
                        if (!response) {
                            ElMessage.error('更新失败，请稍后重试');
                            return;
                        }
                        // 更改成功修改前端信息
                        this.editVisible = false;
                        this.dish[this.editIndex] = {...this.tempDish}
                        ElMessage.success(`修改第${this.editIndex}行成功`)
                        this.val = this.getUniqueValue();
                    })
                    .catch((error) => {
                        ElMessage.error('更新失败，请稍后重试');
                        console.log(error)
                        return;
                    });
            },
            // 点击添加按钮
            handleAdd() {
                this.addVisible = true;

                this.tempDish = {
                    dishName: '',
                    dishPrice: 0,
                    dishTaste: '',
                };
                console.log('After click',this.tempDish);
            },
            // 取消添加
            cancelAdd() {
                this.addVisible = false;           
            },
            // 保存添加
            saveAdd() {
                // 检查输入
                for (let key in this.tempDish) {
                    if (this.tempDish.hasOwnProperty(key) && typeof this.tempDish[key] === 'string' && this.tempDish[key] === '') {
                        ElMessage.warning('请输入完所有信息');
                        return;
                    }
                }
                // 向后端添加
                console.log('When Add', this.tempDish);
                post('/api/Dish/Add', this.tempDish)
                    .then((response) => {
                        console.log(`add dish ${response}`);
                        // 添加失败消息提示
                        if (!response) {
                            ElMessage.error('添加失败，请稍后重试');
                            return;
                        }
                        // 添加完成后重新获取所有菜品信息
                        get(`/api/Dish/GetAllDishes`, {})
                            .then(response => {
                                console.log(true);
                                this.dish = response.data;
                                console.log(123456)
                                console.log(this.dish)
                                if (!this.dish) {
                                    ElMessage.error('获取数据失败，请稍后重试');
                                    this.dish = [];
                                    return;
                                }
                                this.addVisible = false;
                                this.val = this.getUniqueValue();
                                ElMessage.success('新增成功');
                            })
                            .catch(error => {
                                console.log(error);
                                ElMessage.error('获取数据失败，请稍侯重试');
                                this.dish = [];
                                return;
                            })
                    })
                    .catch((error) => {
                        ElMessage.error('添加失败，请稍后重试');
                        console.log(error)
                        return;
                    });
            },
            // 获得菜品信息每个属性的所有不重复值
            getUniqueValue() {
                if (this.dish == [])
                    return;
                let ans = {};
                this.dish.forEach((item) => {
                    let key = 'DishTaste';
                    if (ans.hasOwnProperty(key)) {
                        const allVal = ans[key].map(obj => obj.text);
                        if (!allVal.includes(item[key])) {
                            ans[key].push({
                                'text': item[key],
                                'value': item[key],
                            });
                        }
                    } else {
                        ans[key] = [{
                            'text': item[key],
                            'value': item[key],
                        }];
                    }
                });
                return ans;
            },
            // 表格显示过滤函数
            filterHandler(value, row, column) {
                const property = column['property']
                return row[property] === value
            }
        },
        computed: {
            dishShow() {
                // 根据搜索关键词显示结果
                if (this.keyword === '')
                    return this.dish;
                const ans = this.dish.filter((item) => item[this.searchType].includes(this.keyword));
                return ans;
            },
        },
        mounted() {
            // 挂载就尝试获取菜品数据
            get(`/api/Dish/GetAllDishes`, {})
                .then(response => {
                    console.log('get dish!');
                    this.dish = response.data;
                    if (this.dish)
                        this.isGetDishData = true;
                    else {
                        this.dish = [];
                    }
                })
                .catch(error => {
                    console.log(error);
                    this.dish = [];
                })
                .finally(() => {
                    this.val = this.getUniqueValue();
                });
        },
    }
</script>

<style scoped>
.dish-manage {
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
    margin-left: 17%;
}
.search-container label {
    margin-left: 50px;
    margin-right: 5px;
    font-size: x-large;
    color: #196883;
}
.search-select {
	width: 25%;
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
.row-dish-name {
    font-size: large;
    color: #a76200;
}
.row-dish-price {
    font-size: large;
    color: #a76200;
}
.row-dish-taste {
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
.dialog-form-input-number {
    width: 40%;
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
    cursor: pointer;
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
    cursor: pointer;
}
.error-tip {
    box-shadow: 0 0 50px 0 rgba(176, 176, 176, 0.63);
    position: fixed;
    top: 30%;
    left: 35%;
    border-radius: 20px;
    background-color: #ffffff80;
    width: 40%;
    height: 400px;
    font-size: clamp(100px,20vw,100px);
    display: flex;
}
.error-text {
    position: absolute;
    left: 25%;
    top: 25%;
    color: #324157;
}
.error-icon {
    position: absolute;
    left: 42%;
    top: 60%;
    color: #818791;
}
</style>