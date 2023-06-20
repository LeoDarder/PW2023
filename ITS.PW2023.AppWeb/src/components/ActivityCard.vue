<template>
    <div class="card">
        <h1 class="cardTitle">{{ formatDate }}</h1>
        <h1 class="cardSubtitle">{{ formatTime }}</h1>
        <div class="card-body">
            <p class="card-text"><b style="font-family: LemonMilk;">Duration</b><br>{{ duration }} minutes</p>
            <p class="card-text"><b style="font-family: LemonMilk;">Average heart beat</b><br>{{ avgHB }} bpm</p>
            <p class="card-text"><b style="font-family: LemonMilk;">Position</b><br>LON {{ position.longitude }}<br>LAT {{ position.latitude }}</p>
            <p class="card-text"><b style="font-family: LemonMilk;">Laps</b><br>{{ laps }}</p>
        </div>
        <router-link :to="{ name: 'ActivityDetails', params: { id: id } }" class="btn details" @openedDetails="openedDetails">DETAILS</router-link>
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
        },
        formatTime() {
            var date = new Date(this.date);
            return `${date.toLocaleTimeString([], {hour: '2-digit', minute:'2-digit'})}`
        }
    },
    methods: {
        openedDetails() {
            this.$emit("openedDetails");
        }
    }
}
</script>

<style>
@font-face {
    font-family: LemonMilk;
    src: url(../../fonts/LEMONMILK-Medium.otf);
}

.cardTitle {
    font-family: LemonMilk;
    font-size: xx-large;
    color: var(--color-blue);
    text-shadow: 1.5px 1.5px var(--color-darkblue);
    margin-top: 10px;
}

.cardSubtitle {
    font-family: LemonMilk;
    font-style: italic;
    font-size: x-large;
    color: var(--color-blue);
    margin-bottom: 10px;
}

.card-body {
    font-size: initial;
    padding: 0px 16px;
    display: flex;
    flex-direction: column;
    justify-content: center;
}
</style>