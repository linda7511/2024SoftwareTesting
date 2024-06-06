<template>
    <div class="login-view">
      <div class="form-wrapper">
        <div class="header">
          登录
        </div>
        <div class="input-wrapper">
          <div class="border-wrapper">
            <input type="text" name="username" placeholder="username" class="border-item" v-model="formData.username">
          </div>
          <div class="border-wrapper">
            <input type="password" name="password" placeholder="password" class="border-item" v-model="formData.password">
          </div>
          <div class="logIn">
            <div class="btn" @click="login">登录</div>
            <div class="btn" @click="doCancel">取消</div>
          </div>
        </div>
      </div>
    </div>
</template>

<script>
import { ElMessage } from "element-plus";
import { get, put, post, del } from "../axios/axiosConfig.js";
import { setToken, setUserName, setUserId, setUserInfo } from "../axios/axiosConfig.js";

export default {
    data() {
        return {
            formData: {
              username: "",
              password: "",
            }
        }
    },
    props: {
        isActive: {
            type: Boolean,
            required: true
        }
    },
    methods: {
        sleep: function(time) {
            if (time <= 0 || time >= 5000)
              return;

            var timeStamp = new Date().getTime();
            var endTime = timeStamp + time;
          
            while (true) {
                if (new Date().getTime() > endTime)
                  return;
            }
        },
        // 执行登录操作
        login: async function () {
          if (this.formData.username == "") {
            ElMessage.error("用户名不能为空！");
            return;
          }

          else if (this.formData.password == "") {
            ElMessage.error("密码不能为空！");
            return;
          }

         await post(`/api/Employee/Login/${this.formData.username}/${this.formData.password}`,{
          account:this.formData.username,
          password:this.formData.password
         })
            .then(async(res) => {
              console.log(res)
              
              if(res.data.statusCode == 0){ //登录成功
                ElMessage.success(res.data.message);
                if (res.data.token) {
                    localStorage.setItem('token', res.data.token);
                    localStorage.setItem('userInfo', JSON.stringify(res.data.employee));
                }      
                setUserName(res.data.employee.name);
                setUserId(res.data.employee.employeeId);
                //执行试图跳转
                await get(`/api/Employee/GetAll`, {})
                  .then(async(response) => {
                 
                    console.log(response)
                    console.log(123)
                    if (!response.status) {
                      ElMessage.error('获取岗位信息失败，请稍后重试');
                      return;
                    }
                    let emps = response.data;
                    console.log(emps)
                    let empItem = emps.find(item => item.postId === res.data.employee.postId);
                    console.log(empItem)
                    let dept2route = {
                      '总经理办公室': '/e_home/frontReception/resident_checkin',
                      '餐饮': '/e_restaurant/order_dishes',
                      '财务': '/e_materialOnly/purchasing_materials',
                      '人事': '/e_personnelOnly/check_attendance',
                      '前厅部': '/e_frontReceptionOnly/settlement_bill',
                      '后勤': '/e_parkingOnly/entry_exit',
                    };
                    if (dept2route.hasOwnProperty(res.data.departmentName)) {
                      this.$router.push(dept2route[res.data.departmentName])
                    }
                     else {
                      ElMessage.error('登录失败，请稍后重试');
                    }
                  })
                  .catch(err => {
                    console.log(err);
                    ElMessage.error('获取岗位信息失败，请稍后重试');
                    return;
                  })
              }else{  //登录失败
                ElMessage.error(res.Message);
              }
          }).catch((err) => { 
            console.log(err);
            ElMessage.error('登录失败');
          })
        },
        doCancel() {//清除用户的输入
          this.formData.username = ""
          this.formData.password = ""
        }
      },
}
</script>

<style scoped>
.login-view {
  background-image: url('../assets/loginBackground.jpg');
  height: 100vh;
  width: 100vw;
}
.active-button{
  height: 25px;
  font-size: 12px;
  border-radius: 4px;
  color: rgba(255, 255, 255, 0.8);
  border: 1px solid rgba(0, 0, 0, 0.9);
  background-color: rgba(11, 117, 127, 0.8);
  }
  .inactive-button{
    height: 25px;
  font-size: 12px;
  border-radius: 4px;
  color: rgba(5, 5, 5, 0.8);
  border: 1px solid rgba(0, 0, 0, 0.9);
  background-color: rgba(233, 241, 241, 0.8);
  }

  .form-wrapper {
    width: 300px;
    background-color: rgba(41, 45, 62, 0.587);
    color: white;
    border-radius: 20px;
    padding: 50px;
    position: fixed;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
  }

  .form-wrapper .header {
    text-align: center;
    font-size: 35px;
    text-transform: uppercase;
    line-height: 100px;
  }

  .form-wrapper .input-wrapper input {
    background-color: rgb(41, 45, 62);
    border: 0;
    width: 100%;
    text-align: center;
    font-size: 15px;
    color: #fff;
    outline: none;
  }

  .form-wrapper .input-wrapper input::placeholder {
    text-transform: uppercase;
  }

  .form-wrapper .input-wrapper .border-wrapper {
    background-image: linear-gradient(to right,#e8198b,#0eb4dd);
    width: 100%;
    height: 50px;
    margin-bottom: 20px;
    border-radius: 30px;
    display: flex;
    align-items: center;
    justify-content: center;
  }

  .form-wrapper .input-wrapper .border-wrapper .border-item {
    height: calc(100% - 4px);
    width: calc(100% - 4px);
    border-radius: 30px;
  }
  .form-wrapper .logIn {
    display: flex;
    justify-content: center;
  }
  .form-wrapper .logIn .btn {
    width: 60%;
    text-transform: uppercase;
    border: 2px solid #0eb4dd;
    text-align: center;
    line-height: 50px;
    border-radius: 30px;
    cursor: pointer;
    transition: .2s;
  }

  .form-wrapper .logIn .btn:hover{
    background-color: #0eb4dd;
  }

  .form-wrapper .icon-wrapper {
    text-align: center;
    width: 60%;
    margin: 0 auto;
    margin-top: 20px;
    border-top: 1px dashed rgb(146, 146, 146);
    padding: 20px;
  }

  .form-wrapper .icon-wrapper i {
    font-size: 20px;
    color: rgb(187, 187, 187);
    cursor: pointer;
    border: 1px solid #fff;
    padding: 5px;
    border-radius: 20px;
  }
</style>