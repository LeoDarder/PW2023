<template>
    <div class="card">
        <h1 class="activityId">#{{ shortId }}</h1>
        <div class="card-body">
            <p class="card-text"><b style="font-family: LemonMilk;">Date</b><br>{{ formatDate }}</p>
            <p class="card-text"><b style="font-family: LemonMilk;">Duration</b><br>{{ duration }} minutes</p>
            <p class="card-text"><b style="font-family: LemonMilk;">Average heart beat</b><br>{{ avgHB }} bpm</p>
            <p class="card-text"><b style="font-family: LemonMilk;">Position</b><br>LON {{ position.longitude }}<br>LAT {{ position.latitude }}</p>
            <p class="card-text"><b style="font-family: LemonMilk;">Laps</b><br>{{ laps }}</p>
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
        },
        formatDate() {
            var date = new Date(this.date);
            return `${date.toLocaleDateString()}`
        }
    }
}
</script>

<style>
@font-face {
    font-family: LemonMilk;
    src: url(../../fonts/LEMONMILK-Medium.otf);
}

.activityId {
    font-family: LemonMilk;
    font-style: italic;
    font-size: 50px;
    color: var(--color-blue);
    text-shadow: 2px 2px var(--color-darkblue);
}
</style>