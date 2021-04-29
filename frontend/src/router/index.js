import { createRouter, createWebHistory } from 'vue-router';
import Home from '../views/Home.vue';
import userService from '../_services/user.service.js';

const routes = [{
        path: '/',
        name: 'Home',
        component: Home
    },
    {
        path: '/login',
        name: 'Login',
        component: () =>
            import ('../views/Login.vue')
    },
    {
        path: '/signup',
        name: 'Signup',
        component: () =>
            import ('../views/Signup.vue')
    },
    {
        path: '/account/:id',
        name: 'Account',
        component: () =>
            import ('../views/Account.vue')
    },
    {
        path: '/artist/:id',
        name: 'Artist',
        component: () =>
            import ('../views/Artist.vue')
    },
    {
        path: '/song/:id',
        name: 'Song',
        component: () =>
            import ('../views/Song.vue'),
    },
    {
        path: '/admin',
        name: 'Admin',
        component: () =>
            import ('../views/AdminPanel.vue'),
    },
    {
        path: '/forbidden',
        name: 'Forbidden',
        component: () =>
            import ('../views/Forbidden.vue'),
    },
    {
        path: '/webplayer',
        name: 'WebPlayer',
        component: () =>
            import ('../views/WebPlayer.vue')
    }
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

router.beforeEach(async(to, from, next) => {

    // redirect to login page if not logged in and trying to access a restricted page
    const publicPages = ['/login', '/', '/signup', '/forbidden'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = userService.isUserLoggedIn();

    if (authRequired && !loggedIn)
        return next('/login');

    if (to.path == '/admin') {
        const isAdmin = await userService.isCurrentUserAdmin();
        if (!isAdmin)
            return next('/forbidden');
    }

    next();
});

export default router;