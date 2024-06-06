// 定义前台接待子路由的名称数组
const receptionRoutes = [
    "resident_checkin",
    "idle_query",
    "guest_info",
    "process_order",
  ];
  
  // 创建前台接待子路由对象的函数
  const createReceptionRoute = (routeName) => ({
    path: routeName,
    component: () => import(/* webpackChunkName: 'reception' */ `../employee/Reception/${routeName}.vue`),
  });
  
  // 通过循环创建前台接待子路由对象数组
  const receptionRouteObjects = receptionRoutes.map((routeName) => createReceptionRoute(routeName));
  
  // 创建前台接待父路由对象
  const receptionParentRoute = {
    path: "reception",
    component: () => import(/* webpackChunkName: 'reception' */ "../employee/E_Reception.vue"),
    children: receptionRouteObjects,
  };
  
  // 导出前台接待父路由对象和其它可能需要的内容
  export { receptionParentRoute };
  