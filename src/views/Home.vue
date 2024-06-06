<template>
  <n-card class="home-box" content-style="padding-bottom: 0;" footer-style="margin-bottom: 0;padding-bottom: 0;">
    <n-space justify="center">
      <n-icon class="logo" size="15em">
      <svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 512 512">
        <image
          xlink:href="../components/icons/Icon.png" 
          x="0"  
          y="0"  
          width="512" 
          height="512" 
        />
      </svg>
      </n-icon>
    </n-space>
    <n-space justify="center">
      <n-p class="project-title">Bug Hunter</n-p>
    </n-space>
    <n-divider class="divider" />
    <n-space justify="center">
      <n-p class="tech-stack" :depth="3">同济大学软件学院2024软件测试课程作业</n-p>
    </n-space>
    <n-space justify="center">
      <n-p class="intro">一个自动化软件测试工具</n-p>
    </n-space>
  </n-card>
</template>

<script setup lang="ts">
import { NIcon, NCard, NSpace, NP, NDivider } from 'naive-ui'

</script>

<style scoped>
.home-box {
  width: 100%;
  height: 88vh;
  overflow: auto;
}

.logo {
  margin: .4em 0 .1em 0;
}

.project-title {
  font-size: 2.4em;
  font-weight: 500;
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
}

.divider {
  width: 60%;
  margin: .8em auto;
}

.tech-stack {
  font-size: 1.2em;
  font-weight: 500;
  margin-bottom: .8em;
}

.vue-logo {
  padding-top: .05em;
}

.intro {
  margin-top: .8em;
  font-size: 1.2em;
  font-weight: 510;
}

.try {
  font-size: 1.2em;
  font-weight: 500;
  margin: .8em 0 3.6em 0;
}

.flex-wrapper {
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 100%;
}

::-webkit-scrollbar {
  height: 0;
  width: 0;
}

.flex-column {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
}

.icp {
  color: #777;
  margin: 0;
  padding: 0;
  overflow: auto;
}

.icp:link {
  text-decoration: none;
}

.icp:visited {
  color: #777;
  text-decoration: underline;
}

.icp:hover {
  color: deepskyblue;
  text-decoration: underline;
}

icp:active {
  color: springgreen;
  text-decoration: underline;
}
</style>