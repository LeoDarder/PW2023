<template>
    <div class="card">
        <h1 class="activityId">#{{ shortId }}</h1>
        <div class="card-body">
            <p class="card-text"><b>Date</b><br>{{ date }}</p>
            <p class="card-text"><b>Duration</b><br>{{ duration }} minutes</p>
            <p class="card-text"><b>Average heart beat</b><br>{{ avgHB }} bpm</p>
            <p class="card-text"><b>Position</b><br>LON {{ position.longitude }}<br>LAT {{ position.latitude }}</p>
            <p class="card-text"><b>Laps</b><br>{{ laps }}</p>
        </div>
        <router-link :to="{ name: 'ActivityDetails', params: { id: id } }" class="btn details">DETAILS</router-link>
    </div>
</template>

<script>

export default {
    name: "ActivityCard",
    props: [
        'id',
        'date',
        'duration',
        'avgHB',
        'position',
        'laps'
    ],
    data() {
        return {
            activities: null
        }
    },
    computed: {
        shortId() {
            return this.id.toString().slice(0, 4);
        }
    }
}
</script>

<style>
.activityId {
    font-size: 50px;
    color: var(--color-darkblue);
}
</style>