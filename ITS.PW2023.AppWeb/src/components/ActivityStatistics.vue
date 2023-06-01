<template>
    <div v-if="!loading" class="statisticPage">
        <activity-card
            v-for="activity in activities"
            :key="activity.guid"
            :id="activity.guid"
            :date="activity.date"
            :duration="activity.duration"
            :avgHB="activity.avgHB"
            :position="activity.position"
            :laps="activity.laps"
        ></activity-card>
    </div>
    <div v-else class="loading">
        <img :src="loadingImage" />
        <h4>LOADING ...</h4>
    </div>
</template>

<script>
import ActivityCard from './ActivityCard.vue';

const baseUrl = "https://cper-pw2023-gruppo5-api.azurewebsites.net";
const devGuid = "36cd50f0-fc01-4ddb-930d-011a7afcb417";

export default {
    name: "ActivityStatistics",
    components: {
        ActivityCard
    },
    data() {
        return {
            loading: true,
            loadingImage: require("../../public/swimming-loader-1.gif"),
            activities: [],
            avgHeartBeat: null,
            avgLaps: null
        }
    },
    mounted() {
        this.getValues();
    },
    methods: {
        async getValues() {
            const activities = await fetch(`${baseUrl}/getActivities?devGUID=${devGuid}`);
            this.activities = await activities.json();
            this.loading = false;

            this.activities.sort(function(a,b){
                return new Date(b.date) - new Date(a.date);
            });

            /* STRUTTURA OGGETTO
            {
                avgHB: 136.5
                date: "2023-05-04T10:00:04.066517Z"
                duration: 0
                guid: "bc3da806-f738-49a6-912b-83a9e29fb70d"
                laps: 0
                position: {
                    latitude: 10.289943348874695
                    longitude: -119.23106836697349
                }
            }
            */

            const avgHB = await fetch(`${baseUrl}/getAvgHB?devGUID=${devGuid}`);
            this.avgHeartBeat = await avgHB.json();

            const avgLaps = await fetch(`${baseUrl}/getAvgLaps?devGUID=${devGuid}`);
            this.avgLaps = await avgLaps.json();
        }
    }
}
</script>

<style>
.general-data {
    display: flex;
    flex-direction: column;
}

.general {
    font-family: LemonMilk;
    height: 28vh;
    padding: 10px;
    margin-bottom: 5vh;
    display: flex;
    justify-content: space-evenly;
    background-color: var(--color-lightblue);
}
</style>