const frontReceptionRoutes = [
    "settlement_bill",
    "resident_checkin",
    "idle_query",
    "guest_info",
    "online_orders",
    "process_order",
];

const createFrontReceptionRoute = (routeName) => ({
    path: routeName,
    component: () => import(`../employee/FrontReception/${routeName}.vue`),
});

const frontReceptionRouteObjects = frontReceptionRoutes.map(routeName => createFrontReceptionRoute(routeName));

const frontReceptionParentRoute = {
    path: 'frontReception',
    component: () => import("../employee/E_FrontReception.vue"),
    children: frontReceptionRouteObjects,
};

export { frontReceptionParentRoute }