<template>
    <div class="table-manage">
        <img src="../../assets/restaurant.svg" class="function-icon">

        <div class="title">
            <label class="title-function"> 餐厅接待 </label>
            <label class="title-content"> 桌位管理 </label>
        </div>

        <template v-if="isGetTableData">
            <div class="flex-container">
                <el-button icon="plus" @click="handleAdd" class="add-button"> 添加 </el-button>
            </div>

            <div class="table-container">
                <el-table 
                    :data="table" 
                    class="table"
                    :header-cell-style="{
                        'padding': '20px',
                        'font-size': 'x-large',
                        'font-weight': 'lighter',
                        'color': '#ffffff',
                        'background-color': '#196883'
                    }">
                    <el-table-column prop="capacityNum" label="容纳人数"
                        :filters="val['capacityNum']"
                        :filter-method="filterHandler"
                        width="130">
                        <template #default="scope">
                            <label class="row-capacity-num"> {{ scope.row.capacity }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="tableType" label="类型"
                        :filters="val['tableType']"
                        :filter-method="filterHandler"
                        width="130">
                        <template #default="scope">
                            <label class="row-table-type"> {{ scope.row.tableType }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="tableLocation" label="位置"
                        :filters="val['tableLocation']"
                        :filter-method="filterHandler"
                        width="130">
                        <template #default="scope">
                            <label class="row-table-location"> {{ scope.row.tableLocation }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="tableStatus" label="状态"
                        :filters="val['tableStatus']"
                        :filter-method="filterHandler"
                        width="130">
                        <template #default="scope">
                            <label class="row-table-status"> {{ scope.row.tableStatus }} </label>
                        </template>
                    </el-table-column>
                    <el-table-column prop="note" label="备注" width="200">
                        <template #default="scope">
                            <el-tooltip effect="dark" :content="scope.row.note" placement="left" class="row-note">
                                {{ limitWord(scope.row.note) }}
                            </el-tooltip>
                        </template>  
                    </el-table-column>
                    <el-table-column prop="bookable" label="是否可预定" width="140">
                        <template #default="scope">
                            <el-switch 
                                v-model="scope.row.bookable"
                                :active-value="1" 
                                :inactive-value="0"
                                @change="handleChange(scope.row)">
                            </el-switch>
                        </template>               
                    </el-table-column>
                    <el-table-column prop="available" label="是否可用" width="130">
                        <template #default="scope">
                            <el-switch 
                                v-model="scope.row.available"
                                :active-value="1" 
                                :inactive-value="0"
                                @change="handleChange(scope.row)">
                            </el-switch>
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

        <template v-else>
            <div class="error-tip">
                <span class="error-text"> 加载中... </span>
                <el-icon class="error-icon"><Loading /></el-icon>
            </div>
        </template>

        <el-dialog title="编辑" v-model="editVisible">
            <el-form label-width="140px">
                <el-form-item label="容纳人数">
                    <el-input-number v-model="editTable.capacity" :min="1" class="dialog-form-input-number"></el-input-number>
                </el-form-item>
                <el-form-item label="类型">
                    <el-input v-model="editTable.tableType" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="位置">
                    <el-input v-model="editTable.tableLocation" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="状态">
                    <el-input v-model="editTable.tableStatus" disabled="true" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="备注">
                    <el-input type="textarea" v-model="editTable.note" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="是否可预订">
                    <el-switch 
                        v-model="editTable.bookable" 
                        :active-value="1" 
                        :inactive-value="0">
                    </el-switch>
                </el-form-item>
                <el-form-item label="是否可用">
                    <el-switch 
                        v-model="editTable.available"
                        :active-value="1" 
                        :inactive-value="0">
                    </el-switch>
                </el-form-item>
            </el-form>

            <div class="dialog-buttons">
                <button class="dialog-confirm-button" @click="saveEdit"> 确定 </button>
                <button class="dialog-cancel-button" @click="editVisible = false"> 取消 </button>
            </div>
        </el-dialog>

        <el-dialog title="新增" v-model="addVisible">
            <el-form label-width="140px">
                <el-form-item label="容纳人数">
                    <el-input-number v-model="newTable.capacity" :min="1" class="dialog-form-input-number"></el-input-number>
                </el-form-item>
                <el-form-item label="类型">
                    <el-input v-model="newTable.tableType" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="位置">
                    <el-input v-model="newTable.tableLocation" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="状态">
                    <el-input v-model="newTable.tableStatus" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="备注">
                    <el-input type="textarea" v-model="newTable.note" class="dialog-form-input"></el-input>
                </el-form-item>
                <el-form-item label="是否可预订">
                    <el-switch 
                        v-model="newTable.bookable"
                        :active-value="1" 
                        :inactive-value="0">
                    </el-switch>
                </el-form-item>
                <el-form-item label="是否可用">
                    <el-switch 
                        v-model="newTable.available"
                        :active-value="1" 
                        :inactive-value="0">
                    </el-switch>
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
import { fetchTableData } from '../../axios/check_attendance.js';
    export default {
        data() {
            return {
                table: [],              // 桌位信息
                newTable: {},           // 添加桌位临时变量
                editTable: {},          // 编辑桌位临时变量
                editVisible: false,     // 控制编辑对话框可见
                addVisible: false,      // 控制添加对话框可见
                val: {},                // 存储桌位信息每个属性的值
                editIndex: 0,           // 记录编辑桌位的索引
                isGetTableData: false,  // 记录是否得到桌位信息
            }
        },
        methods: {
            // 限制显示字数 将过长字符串转换为"16个字符 ..."
            limitWord(val) {
                if (!val) 
                    return "";
                const maxLen = 8;
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
                    del(`/api/MyTable/Delete/${this.table[index]['tableId']}`, {})
                        .then((response) => {
                            console.log(`delete table ${response}`);
                            // 删除不成功消息提示
                            if (!response) {
                                ElMessage.error('删除失败，请稍后重试');
                                return;
                            }
                            // 删除成功前端列表删除
                            ElMessage.success('删除成功');
                            this.table.splice(index, 1);
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
                this.editTable = {...row};
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
                put('/api/MyTable/Update', this.editTable)
                    .then((response) => {
                        console.log(`update table ${response}`);
                        if (!response) {
                            ElMessage.error('更新失败，请稍后重试');
                            return;
                        }
                        // 更改成功修改前端信息
                        this.editVisible = false;
                        this.table[this.editIndex] = {...this.editTable};
                        ElMessage.success(`修改第${this.editIndex}行成功`);
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

                this.newTable = {
                    capacity: '',
                    tableType: '',
                    tableLocation: '',
                    tableStatus: '空闲',
                    note: '',
                    bookable: 1,
                    available: 1,
                };
            },
            // 取消添加
            cancelAdd() {
                this.addVisible = false;             
            },
            // 保存添加
            saveAdd() {
                // 检查输入
                for (let key in this.newTable) {
                    if (this.newTable.hasOwnProperty(key) && typeof this.newTable[key] === 'string' && this.newTable[key] === '') {
                        ElMessage.warning('请输入完所有信息');
                        return;
                    }
                }
                // 向后端添加
                post(`/api/MyTable/Add`, this.newTable)
                    .then((response) => {
                        console.log(`add table ${response}`);
                        // 添加失败消息提示
                        if (!response) {
                            ElMessage.error('添加失败，请稍后重试');
                            return;
                        }
                        // 添加完成后重新获取所有菜品信息
                        get(`/api/MyTable/GetAll`, {})
                            .then(response => {
                                console.log(true);
                                this.table = response.data;
                                if (!this.table) {
                                    ElMessage.error('获取数据失败，请稍侯重试');
                                    this.table = [];
                                    return;
                                }
                                this.addVisible = false;
                                ElMessage.success('新增成功');
                            })
                            .catch(error => {
                                console.log(error);
                                ElMessage.error('获取数据失败，请稍侯重试');
                                this.table = [];
                                return;
                            });
                    })
                    .catch((error) => {
                        console.log(error)
                        ElMessage.error('添加失败，请稍后重试');
                        return;
                    });
            },
            // 获得菜品信息每个属性的所有不重复值
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
            // 表格显示过滤函数
            filterHandler(value, row, column) {
                const property = column['property']
                if (property == 'tableLocation')
                    return row[property].includes(value);
                return row[property] === value
            },
            // 处理桌位信息更新
            handleChange(data) {
                put('/api/MyTable/Update', data)
                    .then((response) => {
                        console.log(`update table ${response}`);
                        if (!response) {
                            ElMessage.error('更新失败，请稍稍后重试');
                            return;
                        }
                    })
                    .catch((error) => {
                        ElMessage.error('更新失败，请稍稍后重试');
                        console.log(error)
                        return;
                    });
            },
        },
        mounted() {
            // 挂载就尝试获取桌位数据
            get(`/api/MyTable/GetAll`, {})
                .then(response => {
                    console.log(123456);
                    this.table = response.data;
                    console.log(response.data)
                    if (this.table)
                        this.isGetTableData = true; 
                    else {
                        this.table = [];
                    }
                })
                .catch(error => {
                    console.log(error);
                    this.table = [];
                })
                .finally(() => {
                    this.val = this.getUniqueValue();
                });
        },
    }
</script>

<style scoped>
.table-manage {
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
.flex-container {
    display: flex;
    justify-content: center;
	margin-bottom: 20px;
}
.add-button {
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
.row-capacity-num {
    font-size: large;
    color: #a76200;
}
.row-table-type {
    font-size: large;
    color: #a76200;
}
.row-table-location {
    font-size: large;
    color: #196883;
}
.row-table-status {
    font-size: large;
    color: #a76200;
}
::v-deep .el-only-child__content.el-tooltip__trigger {
    font-size: large;
    color: #a76200;
}
::v-deep .el-switch {
    --el-switch-on-color: #196883;
    --el-switch-off-color: #828992;
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
    height: 40px;
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