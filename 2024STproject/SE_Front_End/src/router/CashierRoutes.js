// 定义收银管理子路由的名称数组
const cashierRoutes = [
    "settlement_bill",
  ];
  
  // 创建收银管理子路由对象的函数
  const createCashierRoute = (routeName) => ({
    path: routeName,
    component: () => import(/* webpackChunkName: 'cashier' */ `../employee/Cashing/${routeName}.vue`),
  });
  
  // 通过循环创建收银管理子路由对象数组
  const cashierRouteObjects = cashierRoutes.map((routeName) => createCashierRoute(routeName));
  
  // 创建收银管理父路由对象
  const cashierParentRoute = {
    path: "cashier",
    component: () => import(/* webpackChunkName: 'cashier' */ "../employee/E_Cashier.vue"),
    children: cashierRouteObjects,
  };
  
  // 导出收银管理父路由对象和其它可能需要的内容
  export { cashierParentRoute };
  