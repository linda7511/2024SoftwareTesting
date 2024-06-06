// 定义餐厅管理子路由的名称数组
const restaurantRoutes = [
    "dish_manage",
    "order_dishes",
    "table_manage",
  ];
  
  // 创建餐厅管理子路由对象的函数
  const createRestaurantRoute = (routeName) => ({
    path: routeName,
    component: () => import(/* webpackChunkName: 'restaurant' */ `../employee/Restaurant/${routeName}.vue`),
  });
  
  // 通过循环创建餐厅管理子路由对象数组
  const restaurantRouteObjects = restaurantRoutes.map((routeName) => createRestaurantRoute(routeName));
  
  // 创建餐厅管理父路由对象
  const restaurantParentRoute = {
    path: "restaurant",
    component: () => import(/* webpackChunkName: 'restaurant' */ "../employee/E_Restaurant.vue"),
    children: restaurantRouteObjects,
  };
  
  // 导出餐厅管理父路由对象和其它可能需要的内容
  export { restaurantParentRoute };
  