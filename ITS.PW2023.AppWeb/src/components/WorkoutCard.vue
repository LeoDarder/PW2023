<template>
    <div class="card">
        <h1 class="workoutId">#{{ id+1 }}</h1>
        <div class="card-body">
            <p class="card-text"><b>Date</b> {{ time }}</p>
            <p class="card-text"><b>Duration</b> {{ duration }} minutes</p>
            <p class="card-text"><b>Avg. heart rate</b> {{ averageHeartRate }} bpm</p>
            <p class="card-text"><b>Last position</b> {{ lastPosition }}</p>
            <p class="card-text"><b>No. tubs</b> {{ numberOfTubs }}</p>
        </div>
        <router-link :to="{ name: 'WorkoutDetails', params: { id: id }}" class="btn details">DETAILS</router-link>
    </div>
</template>

<script>
export default {
    name: "WorkoutCard",
    props: [
        'id',
        'time',
        'duration',
        'averageHeartRate',
        'lastPosition',
        'numberOfTubs'
    ],
    computed: {
        setId() {
            return this.id;
        },
        setTime() {
            return this.time;
        },
        setDuration() {
            return this.duration;
        },
        setAverageHeartRate() {
            return this.averageHeartRate;
        },
        setLastPosition() {
            return this.lastPosition;
        },
        setNumberOfTubs() {
            return this.numberOfTubs;
        }
    }
}
</script>

<style>
    .workoutId {
        font-size: 50px;
        color: var(--color-darkblue);
        /* text-shadow: 2px 2px 4px gray; */
    }
</style>