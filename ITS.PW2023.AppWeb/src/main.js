import { createApp } from 'vue';
import App from './App.vue';
import {createRouter, createWebHistory} from 'vue-router';
import WorkoutStatistics from './components/WorkoutStatistics.vue';
import WorkoutDetails from './components/WorkoutDetails.vue';
import GeneralStatistics from './components/GeneralStatistics.vue';
import GeneralDetails from './components/GeneralDetails.vue';

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
    {
      path: '/workout/:id',
      name: 'WorkoutDetails',
      component: WorkoutDetails,
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
