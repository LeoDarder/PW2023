<template>
    <div class="statisticPage">
        <workout-card
            v-for="workout in workouts"
            :key="workout.id"
            :id="workout.id"
            :time="workout.time"
            :duration="workout.duration"
            :averageHeartRate="workout.averageHeartRate"
            :lastPosition="workout.lastPosition"
            :numberOfTubs="workout.numberOfTubs"
        ></workout-card>
    </div>
</template>

<script>
import WorkoutCard from './WorkoutCard.vue';

export default {
    name: "WorkoutsStatistics",
    components: {
        WorkoutCard
    },
    props: [
        'workouts'
    ],
    computed: {
        setWorkouts() {
            return this.workouts;
        }
    },
    methods: {
    }
}
</script>