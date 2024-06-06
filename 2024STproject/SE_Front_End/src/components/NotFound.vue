<template>
    <div class="main-container">
        <div class="img-container container-item">
            <img src="../assets/logo.png">
        </div>
        <div class="text-container container-item">
            <div class="code">404</div>
            <div class="text">您查看的页面貌似丢失了</div>
            <div class="return-link" @click="returnHome">返回首页，查看更多内容</div>
        </div>
    </div>
</template>

<script>
export default {
    methods: {
        returnHome() {
            this.$router.push("/e_home/introduction")
        }
    }
}
</script>

<style scoped>
.main-container {
  width: 600px;
  background-color: white;
  font-size: 0;
  border-radius: 20px;
  box-shadow: 0 0 50px 0 rgba(176, 176, 176, 0.63);
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}

.main-container .container-item {
  display: inline-block;
  vertical-align: middle;
  /* 平分主容器 */
  width: 50%;
}

.main-container .img-container {
  border-top-left-radius: 20px;
  border-bottom-left-radius: 20px;
}

.main-container .text-container .code {
  font-size: clamp(150px,20vw,200px);
  font-family: 'Arial Narrow';
  color: rgb(53, 84, 111);
  font-weight: bolder;
  text-align: center;
}

.main-container .text-container .text {
  font-size: 18px;
  text-align: center;
  font-weight: 200;
  margin-bottom: 20px;
}

.main-container .text-container .return-link {
  font-size: 15px;
  font-weight: 200;
  text-align: center;
  text-decoration-line: underline;
  cursor: pointer;
}
</style>
