<template>
    <div class="statistics">
        <side-bar
            ref="sideBar"
            :userData="userData"
            @reloadActivities="getValues">
        </side-bar>
        <div v-if="!loading" class="statistics-page">
            <div class="reload-section">
                <button class="btn reload" type="button" @click="reloadData">
                    <span><b><i class="bi bi-arrow-clockwise"></i></b></span>
                </button>
            </div>
            <div class="activities">
                <activity-card
                    v-for="activity in activities"
                    :key="activity.idDevice"
                    :id="activity.idDevice"
                    :date="activity.time"
                    :duration="activity.duration"
                    :avgHB="activity.avgHB"
                    :position="activity.position"
                    :laps="activity.laps"
                    :userData="userData"
                    @openedDetails="openedDetails"
                ></activity-card>
            </div>
        </div>
        <div v-else class="loading">
            <img :src="loadingImage" />
            <h4>LOADING ...</h4>
        </div>
    </div>
</template>

<script>
import ActivityCard from './ActivityCard.vue';
import SideBar from './SideBar.vue';

const baseUrl = "https://cper-pw2023-gruppo5-api.azurewebsites.net";
const devGuid = "36cd50f0-fc01-4ddb-930d-011a7afcb417";

export default {
    name: "ActivityStatistics",
    components: {
        ActivityCard,
        SideBar
    },
    props: [
        'userData'
    ],
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
        this.$emit("openedActivities");

        this.getValues();
    },
    methods: {
        reloadData() {
            this.getValues();
            this.$refs.sideBar.getAvgs();
        },
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
                time: "2023-05-04T10:00:04.066517Z"
                duration: 0
                idDevice: "bc3da806-f738-49a6-912b-83a9e29fb70d"
                laps: 0
                position: {
                    latitude: 10.289943348874695
                    longitude: -119.23106836697349
                }
            }
            */
        },
        openedDetails() {
            this.$emit("openedDetails")
        }
    }
}
</script>

<style>
.statistics {
    display: flex;
    flex-direction: row;
}

.reload-section {
    display: flex;
    margin: 5vh 0vh -2vh 5vh;
}

.reload {
    font-family: LemonMilk;
    font-size: large;
    height: 45px;
    border: 1px solid var(--color-darkblue);
    color: var(--color-darkblue);
    background-color: transparent;
}

.reload:hover {
    color: var(--color-lightblue);
    background-color: var(--color-darkblue);
}

.activities {
    display: flex;
    justify-content: start;
    flex-wrap: wrap;
    margin: 5vh 0vh 0vh 5vh;
}
</style>