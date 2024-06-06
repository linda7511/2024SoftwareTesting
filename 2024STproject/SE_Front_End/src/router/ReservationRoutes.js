// 定义预订管理子路由的名称数组
const reservationRoutes = [
    "online_orders",
  ];
  
  // 创建预订管理子路由对象的函数
  const createReservationRoute = (routeName) => ({
    path: routeName,
    component: () => import(/* webpackChunkName: 'reservation' */ `../employee/Reservation/${routeName}.vue`),
  });
  
  // 通过循环创建预订管理子路由对象数组
  const reservationRouteObjects = reservationRoutes.map((routeName) => createReservationRoute(routeName));
  
  // 创建预订管理父路由对象
  const reservationParentRoute = {
    path: "reservation",
    component: () => import(/* webpackChunkName: 'reservation' */ "../employee/E_Reservation.vue"),
    children: reservationRouteObjects,
  };
  
  // 导出预订管理父路由对象和其它可能需要的内容
  export { reservationParentRoute };
  