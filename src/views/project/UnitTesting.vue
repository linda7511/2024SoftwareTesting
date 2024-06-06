<template>
  <div>
    <button @click="runTests">Run Tests</button>
    <div v-if="testResults">...Display test results here...</div>
    <div>
      <iframe v-if="iframeSrc" :src="iframeSrc" width="100%" height="500px"></iframe>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      testResults: null,
      iframeSrc: null,
    };
  },
  methods: {
    async runTests() {
      try {
        const response = await axios.post('http://localhost:3000/run-tests');

        const reportHtml = response.data.reportHtml;
        console.log('reportHtml:',reportHtml);
        this.embedReportInFrontend(reportHtml);

        console.log('Allure report embedded in frontend.');
      } catch (error) {
        console.error('Error running tests or generating report', error);
      }
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
