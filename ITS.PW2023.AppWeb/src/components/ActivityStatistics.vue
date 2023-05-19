<template>
    <div class="statisticPage">
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
</template>

<script>
import ActivityCard from './ActivityCard.vue';

const baseUrl = "https://cper-pw2023-gruppo5-api.azurewebsites.net";
const devGuid = "aa76d690-29c4-4696-999d-997a18e46e75";

export default {
    name: "ActivityStatistics",
    components: {
        ActivityCard
    },
    data() {
        return {
            activities: []
        }
    },
    mounted() {
        this.getValues();
    },
    methods: {
        async getValues() {
            const response = await fetch(`${baseUrl}/getActivities?devGUID=${devGuid}`);
            this.activities = await response.json();

            /*
            STRUTTURA OGGETTO
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
        }
    }
}
</script>