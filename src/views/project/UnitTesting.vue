<template>
  <div class="flex-container">
    <!-- 子系统选择下拉框 -->
    <label for="subsystemSelect">选择子系统：</label>
    <select name="subsystemSelect" id="subsystemSelect" v-model="selectedSubsystem" @change="updateClasses">
      <option value="" disabled>请选择子系统</option>
      <option v-for="subsystem in subsystemList" :key="subsystem" :value="subsystem">
        {{ subsystem }}
      </option>
    </select>

    <!-- 类选择下拉框，根据子系统动态更新 -->
    <label for="classSelect">选择类：</label>
    <select id="classSelect" v-model="selectedClass" @change="updateMethods">
      <option value="" disabled>请选择类</option>
      <option v-for="classItem in filteredClassList" :key="classItem" :value="classItem">
        {{ classItem }}
      </option>
    </select>

    <!-- 方法选择下拉多选框，根据类动态更新 -->
    <label for="methodSelect">选择方法：</label>
    <select id="methodSelect" v-model="selectedMethods">
      <option value="" disabled>请选择方法</option>
      <option v-for="method in filteredMethodsList" :key="method" :value="method">
        {{ method }}
      </option>
    </select>
    <div>
      <button @click="runTests">Run Tests</button>
    </div>
  </div>
  <div>
    <!-- 加载动画，这里用v-show替代v-if以实现过渡效果 -->
    <div v-show="loading">...Loading...
      <el-icon class="error-icon"><Loading /></el-icon>
    </div>
    <div>
      <iframe v-if="iframeShow" :src="'2024STproject/SE_Back_End/canteen-service/target/allure-report/index.html'" width="100%" height="600px"></iframe>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
// import { Loading } from '@element-plus/icons-vue';

export default {
  // components:{
  //   Loading
  // },
  data() {
    return {
      // 初始数据
      selectedSubsystem: null,
      selectedClass: null,
      selectedMethods: [],
      subsystemList:['餐厅子系统','人事子系统','绩效子系统','前台接待子系统'],
      classList: {
        '餐厅子系统': ['All','Dish', 'Book','MyOrder','MyTable'],
        '人事子系统': ['Class3', 'Class4'],
        '绩效子系统': ['Class3', 'Class4'],
        '前台接待子系统': ['Class3', 'Class4'],
      },
      methodList: {
        'Dish': ['newDish', 'removeDish','updateDish','all'],
        'Book': ['newBookInfo', 'modifyBookInfo', 'cancleBookInfo','all'],
        'MyOrder': ['order', 'modifyMyOrder','all'],
        'MyTable': ['createTable', 'amendTable', 'removeBookInfo','all'],
      },
      testResults: null,
      iframeShow: false,
      loading: false,
      iframeSrc: '' // 初始为空，根据需要更新
    };
  },
  computed: {
    filteredClassList() {
      return this.selectedSubsystem && this.classList[this.selectedSubsystem] || [];    },
    filteredMethodsList() {
      return this.selectedClass && this.methodList[this.selectedClass] || [];    }
  },
  methods: {
    updateClasses() {
      console.log('updateClasses called');
      if (this.selectedSubsystem) {
        this.selectedClass = this.classList[this.selectedSubsystem][0]; // 默认选择第一个类
      } else {
        this.selectedClass = null;
      }
      this.selectedMethods = [];
    },
    updateMethods() {
      console.log('updateMethods called');
      if (this.selectedClass) {
        this.selectedMethods = this.methodList[this.selectedClass] ? [this.methodList[this.selectedClass][0]] : []; // 默认选择第一个方法
      }
    },
    async runTests() {
      this.loading = true;
      console.log("run start");
      try {
        const response = await axios.post('http://127.0.0.1:12345/run-unitTests');
        console.log("response:",response);

        const reportHtml = response.data.reportHtml;
        console.log('reportHtml:',reportHtml);
        //this.embedReportInFrontend(reportHtml);
        this.loading = false;
        this.iframeShow = true;

        console.log('Allure report embedded in frontend.');
      } catch (error) {
        console.error('Error running tests or generating report', error);
      }
      console.log("run finished");
    },

    embedReportInFrontend(reportHtml) {
      const blob = new Blob([reportHtml], { type: 'text/html' });
      const reportUrl = URL.createObjectURL(blob);
      console.log('reportUrl:',reportUrl);
      this.iframeSrc = reportUrl;
    },
  }
};
</script>

<style scoped>
.flex-container {
  display: flex;          /* 启用Flexbox布局 */
  justify-content: space-around; /* 项目均匀分布在容器内 */
  align-items: center;    /* 垂直居中对齐项目 */
  flex-wrap: wrap;        /* 允许换行 */
}

.flex-container select {
  margin: 5px;            /* 添加一些外边距 */
  flex: 1;                /* 让每个下拉框占据可用空间 */
  min-width: 100px;       /* 设置最小宽度，防止过于拥挤 */
  max-width: 200px;       /* 设置最大宽度，控制下拉框大小 */
}
.error-icon {
    position: absolute;
    left: 42%;
    top: 60%;
    color: #818791;
}
</style>
