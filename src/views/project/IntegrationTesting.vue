<!-- <script setup lang="ts">
import { NTabs, NTabPane } from 'naive-ui'
import IntegrationHomeVue from '../../components/integrationTesting/IntegrationHome.vue'
import sdk from '@stackblitz/sdk'
import { onMounted } from 'vue'

const tabs = [
  {
    name: 'home',
    tab: '集成测试'
  }
] -->

<template>
  <div>
    <button @click="runTests">Run Tests</button>
    <div v-if="testResults">...Display test results here...</div>
    <div>
      <iframe v-if="iframeShow" :src="'2024STproject/SE_Back_End/target/allure-report/index.html'" width="100%" height="500px"></iframe>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      testResults: null,
      iframeShow: false,
    };
  },
  methods: {
    async runTests() {
      console.log("run start");
      try {
        const response = await axios.post('http://127.0.0.1:12345/run-integrationTests');
        console.log("response:",response);

        const reportHtml = response.data.reportHtml;
        console.log('reportHtml:',reportHtml);
        //this.embedReportInFrontend(reportHtml);
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
