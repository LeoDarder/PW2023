import { createApp } from 'vue';
import App from './App.vue';
import {createRouter, createWebHistory} from 'vue-router';
import ActivityStatistics from './components/ActivityStatistics.vue';
import ActivityDetails from './components/ActivityDetails.vue';
import GeneralStatistics from './components/GeneralStatistics.vue';
import GeneralDetails from './components/GeneralDetails.vue';

const routes = [
    {
      path: '/',
      name: 'Activities',
      component: ActivityStatistics
    },
    {
      path: '/general',
      name: 'General',
      component: GeneralStatistics
    },
    {
      path: '/Activity/:id',
      name: 'ActivityDetails',
      component: ActivityDetails,
      params: true
    },
    {
      path: '/general/:title',
      name: 'GeneralDetails',
      component: GeneralDetails
    },
];

const router = createRouter({
    history: createWebHistory(process.env.BASE_URL),
    routes
});

createApp(App).use(router).mount('#app');
