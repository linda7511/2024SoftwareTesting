import {createRouter, createWebHistory} from 'vue-router'
// import { cashierParentRoute } from "../router/CashierRoutes.js";
// import { guestroomParentRoute } from "../router/GuestroomRoutes.js";
import { materialParentRoute } from "../router/MaterialRoutes.js";
//import { parkingParentRoute } from "../router/ParkingRoutes.js";
import { personnelParentRoute } from "../router/PersonnelRoutes.js";
// import { receptionParentRoute } from "../router/ReceptionRoutes.js";
// import { reservationParentRoute } from "../router/ReservationRoutes.js";
import { restaurantParentRoute } from "../router/RestaurantRoutes.js";
import { frontReceptionParentRoute } from "../router/FrontReception.js";


const routes = [
  {
    path :"",
    redirect: "/login"
  },
  {
    path:"/login",
    name:"login",
    component: () => import( /* webpackChunkName: 'Login' */ "../components/LogInView.vue"),
  },
  {
    //任意匹配
    path:"/:pathMatch(.*)",
    name:"notfound",
    component: () => import( /* webpackChunkName: 'NotFound' */ "../components/NotFound.vue"),
  },
  //以下为员工部分
  {
    path:"/e_home",
    name:"e_home",
    component: () => import( /* webpackChunkName: 'E_Home' */ "../employee/E_HomePage.vue"),
    children:[
      //首页介绍
      {
        path:"introduction",
        component: () => import( /* webpackChunkName: 'introduction' */ "../employee/E_HomeIntroduction.vue"),
      },
      // 前台接待
      frontReceptionParentRoute,
      //物资管理
      materialParentRoute,
      //人事管理
      personnelParentRoute,
      //餐厅接待
      restaurantParentRoute
    ]
  },
  {
    path:'/e_frontReceptionOnly',
    name:'e_frontReceptionOnly',
    component: () => import('../employee/E_FrontReceptionOnly.vue'),
    children: [
      {
        path:"settlement_bill",
        component: () => import("../employee/FrontReception/settlement_bill.vue"),
      },
      {
        path:"resident_checkin",
        component: () => import("../employee/FrontReception/resident_checkin.vue"),
      },
      {
        path:"idle_query",
        component: () => import("../employee/FrontReception/idle_query.vue"),
      },
      {
        path:"guest_info",
        component: () => import("../employee/FrontReception/guest_info.vue"),
      },
      {
        path:"process_order",
        component: () => import("../employee/FrontReception/process_order.vue"),
      },
    ]
  },
  {
    path:'/e_materialOnly',
    name:'e_materialOnly',
    component: () => import('../employee/E_MaterialOnly.vue'),
    children: [
      {
        path:"purchasing_materials",
        component: () => import("../employee/Materials/purchasing_materials.vue"),
      },
      {
        path:"view_inventory",
        component: () => import("../employee/Materials/view_inventory.vue"),
      },
      {
        path:"maintain_records",
        component: () => import("../employee/Materials/maintain_records.vue"),
      },
      {
        path:"consumption_records",
        component: () => import("../employee/Materials/consumption_records.vue"),
      },
    ]
  },
  {
    path:'/e_personnelOnly',
    name:'e_personnelOnly',
    component: () => import('../employee/E_PersonnelOnly.vue'),
    children: [
      {
        path:"check_attendance",
        component: () => import('../employee/Personnel/check_attendance.vue')
      },
      {
        path:"employee_application",
        component: () => import('../employee/Personnel/employee_application.vue')
      },
      {
        path:"job_scheduling",
        component: () => import('../employee/Personnel/job_scheduling.vue')
      },
      {
        path:"salary_manage",
        component: () => import('../employee/Personnel/salary_manage.vue')
      },
    ]
  },
  {
    path:'/e_restaurant',
    name:'e_restaurant',
    component: () => import('../employee/E_RestauranteOnly.vue'),
    children:[
      {
        path:"dish_manage",
        component: () => import('../employee/Restaurant/dish_manage.vue')
      },
      {
        path:"order_dishes",
        component: () => import('../employee/Restaurant/order_dishes.vue')
      },
      {
        path:"table_manage",
        component: () => import('../employee/Restaurant/table_manage.vue')
      },
    ]
  }
]

//创建路由对象
const router = createRouter({ 
    routes,
    history : createWebHistory()
})  
  








export default router
