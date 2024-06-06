// 定义人事管理子路由的名称数组
const personnelRoutes = [
    "check_attendance",
    "employee_application",
    "job_scheduling",
    "salary_manage",
  ];
  
  // 创建人事管理子路由对象的函数
  const createPersonnelRoute = (routeName) => ({
    path: routeName,
    component: () => import(/* webpackChunkName: 'personnel' */ `../employee/Personnel/${routeName}.vue`),
  });
  
  // 通过循环创建人事管理子路由对象数组
  const personnelRouteObjects = personnelRoutes.map((routeName) => createPersonnelRoute(routeName));
  
  // 创建人事管理父路由对象
  const personnelParentRoute = {
    path: "personnel",
    component: () => import(/* webpackChunkName: 'personnel' */ "../employee/E_Personnel.vue"),
    children: personnelRouteObjects,
  };
  
  // 导出人事管理父路由对象和其它可能需要的内容
  export { personnelParentRoute };
  