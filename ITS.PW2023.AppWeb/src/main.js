import { createApp } from 'vue';
import App from './App.vue';
import {createRouter, createWebHistory} from 'vue-router';
import WorkoutStatistics from './components/WorkoutStatistics.vue';
import GeneralStatistics from './components/GeneralStatistics.vue';

const routes = [
    {
      path: '/',
      name: 'Workouts',
      component: WorkoutStatistics
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
