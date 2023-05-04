import { createApp } from 'vue';
import App from './App.vue';
import {createRouter, createWebHistory} from 'vue-router';
import WorkoutsStatistics from './components/WorkoutsStatistics.vue';
import GeneralStatistics from './components/GeneralStatistics.vue';

const routes = [
    {
      path: '/',
      name: 'Workouts',
      component: WorkoutsStatistics
    },
    {
      path: '/general',
      name: 'General',
      component: GeneralStatistics
    },
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

createApp(App).use(router).mount('#app');
