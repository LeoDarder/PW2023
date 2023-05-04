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
    data() {
        return {
            workouts: []
        }
    },
    mounted() {
        for (var i = 0; i < 10; i++) {
            var workout = {
                id: i,
                time: Date.now(),
                duration: Math.floor(Math.random() * 90),
                averageHeartRate: Math.floor(Math.random() * 100),
                lastPosition: "LAT 0.0.0",
                numberOfTubs: Math.floor(Math.random() * 10)
            };
            this.workouts.push(workout);
        }
    }
}
</script>