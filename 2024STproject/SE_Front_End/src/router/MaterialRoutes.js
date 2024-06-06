// 定义材料管理子路由的名称数组
const materialRoutes = [
    "purchasing_materials",
    "view_inventory",
    "maintain_records",
    "material_info",
    "consumption_records",
  ];
  
  // 创建材料管理子路由对象的函数
  const createMaterialRoute = (routeName) => ({
    path: routeName,
    component: () => import(/* webpackChunkName: 'material' */ `../employee/Materials/${routeName}.vue`),
  });
  
  // 通过循环创建材料管理子路由对象数组
  const materialRouteObjects = materialRoutes.map((routeName) => createMaterialRoute(routeName));
  
  // 创建材料管理父路由对象
  const materialParentRoute = {
    path: "material",
    component: () => import(/* webpackChunkName: 'material' */ "../employee/E_Material.vue"),
    children: materialRouteObjects,
  };
  
  // 导出材料管理父路由对象和其它可能需要的内容
  export { materialParentRoute };
  