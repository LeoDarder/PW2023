<template>
    <div class="card">
        <h1>#{{ id+1 }}</h1>
        <div class="card-body">
            <p class="card-text"><b>Date</b> {{ time }}</p>
            <p class="card-text"><b>Duration</b> {{ duration }} minutes</p>
            <p class="card-text"><b>Avg. heart rate</b> {{ averageHeartRate }} bpm</p>
            <p class="card-text"><b>Last position</b> {{ lastPosition }}</p>
            <p class="card-text"><b>No. tubs</b> {{ numberOfTubs }}</p>
        </div>
        <a href="#" class="btn details" @click="seeDetails">DETAILS</a>
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
    },
    methods: {
        seeDetails() {
            // ...
        }
    }
}
</script>

<style>
    .card {
        width: 30vh;
        margin: 30px 10px;
    }
    .activityIcon {
        font-size: 80px;
        color: var(--color-darkblue);
    }
    .card-title {
        font-weight: bold;
    }
    .details {
        margin: 10px auto;
        border: 2px solid var(--color-darkblue);
        color: var(--color-darkblue);
        background-color: transparent;
    }
    .details:hover {
        border: 2px solid var(--color-darkblue);
        color: var(--color-beige);
        background-color: var(--color-darkblue);
    }
</style>