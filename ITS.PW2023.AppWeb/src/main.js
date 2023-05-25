import { createApp } from 'vue';
import App from './App.vue';
import {createRouter, createWebHistory} from 'vue-router';
import ActivityStatistics from './components/ActivityStatistics.vue';
import ActivityDetails from './components/ActivityDetails.vue';

const routes = [
    {
      path: '/',
      name: 'Activities',
      component: ActivityStatistics
    },
    {
      path: '/Activity/:id',
      name: 'ActivityDetails',
      component: ActivityDetails,
      params: true
    }
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

createApp(App).use(router).mount('#app');
