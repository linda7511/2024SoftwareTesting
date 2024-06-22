<template>
  <div>
    <label for="testSelect">选择测试用例：</label>
    <select id="testSelect" v-model="selectedTest">
      <option v-for="test in testList" :key="test" :value="test">
        {{ test }}
      </option>
    </select>
    <button @click="runSelectedTest">执行测试</button>
    <div v-if="testResults">
      <h3>测试结果：</h3>
      <pre>{{ testResults }}</pre>
    </div>
  </div>
</template>


<!-- <script setup lang="ts">
import axios from 'axios';
import { ref } from 'vue';
import { NTabs, NTabPane } from 'naive-ui'
import SystemHomeVue from '../../components/systemTesting/SystemHome.vue'
import sdk from '@stackblitz/sdk'
import { onMounted } from 'vue' -->

<!-- // const tabs = [
//   {
//     name: 'home',
//     tab: '系统测试'
//   }
// ] -->

<script>
import axios from 'axios';
export default {
  data() {
    return {
      selectedTest: '',
      testList: ['宾客入住', '管理宾客信息', '空房查询','桌位管理'], // 测试用例列表
      testResults: null,
    };
  },
  methods: {
    async runSelectedTest() {
      try {
        const response = await axios.post('http://127.0.0.1:12345/run-systemTest', { test: this.selectedTest });
        this.testResults = response.data;
      } catch (error) {
        console.error('Error running system test:', error);
      }
    },
  },
};
</script>

<style scoped>
.root-wrapper {
  display: flex;
  gap: .5em;
  font-size: 1rem;
}

.left-part {
  width: 36em;
}

.right-part {
  box-sizing: border-box;
  width: 45.6em;
}
</style>