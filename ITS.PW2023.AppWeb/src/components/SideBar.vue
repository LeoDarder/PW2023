<template>
    <div class="sidebar">
        <div class="device-reload">
            <select class="form-select selectDevice">
                da cambiare con gli alias presi da db
                <option selected disabled>Select a device</option>
                <option value="0">All</option>
                <option value="1">Device 1</option>
                <option value="2">Device 2</option>
                <option value="3">Device 2</option>
            </select>
            <button class="btn reload" type="button" @click="$emit('reloadActivities')">
                <span><i class="bi bi-cloud-arrow-down-fill"></i></span>
            </button>
        </div>
        <div v-if="!loading" class="general-data">
            <div class="general">
                <h5 class="card-title">Avg. heart beat</h5>
                <i class="bi bi-heart-pulse-fill card-icon"></i>
                <h5 class="card-value">{{ this.avgHeartBeat }}</h5>
            </div>
            <div class="general">
                <h5 class="card-title">Avg. laps</h5>
                <i class="bi bi-water card-icon"></i>
                <h5 class="card-value">{{ this.avgLaps }}</h5>
            </div>
        </div>
        <div v-else class="loading side">
            <img :src="loadingImage" style="width: 40%; align-self: center; margin-top: calc(50% + 64px);"/><br />
            <h4>LOADING ...</h4>
        </div>
    </div>
</template>

<script>
const baseUrl = "https://cper-pw2023-gruppo5-api.azurewebsites.net";
const devGuid = "36cd50f0-fc01-4ddb-930d-011a7afcb417";

export default {
    name: "SideBar",
    data() {
        return {
            loading: true,
            loadingImage: require("../../public/loading.gif"),
            values: [],
            avgHeartBeat: null,
            avgLaps: null
        }
    },
    mounted() {
        this.getAvgs();
    },
    methods: {
        async reloadData() {
            // TODO da gestire
        },
        async getAvgs() {
            const avgHB = await fetch(`${baseUrl}/getAvgHB?devGUID=${devGuid}`);
            this.avgHeartBeat = await avgHB.json();

            const avgLaps = await fetch(`${baseUrl}/getAvgLaps?devGUID=${devGuid}`);
            this.avgLaps = await avgLaps.json();

            this.loading = false;
        }
    }
}
</script>

<style>
.sidebar {
    /* width: 30%; */
    width: 308px;
    min-width: 308px;
    min-height: 100vh;
    padding: 5vh;
    background-color: rgba(175, 211, 226, 0.3); /* --color-lightblue */
    box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.3);
}

.device-reload {
    display: flex;
    margin-bottom: 3vh;
}

.selectDevice {
    margin-right: 10px;
}

.reload {
    font-family: LemonMilk;
    height: 45px;
    border: 2px solid var(--color-darkblue);
    color: var(--color-darkblue);
    background-color: transparent;
}

.reload:hover {
    color: var(--color-lightblue);
    background-color: var(--color-darkblue);
}

.general-data {
    display: flex;
    flex-direction: column;
}

.general {
    font-family: LemonMilk;
    display: flex;
    flex-direction: column;
    justify-content: space-evenly;
    width: 100%;
    padding: 3vh;
    border-radius: 6px;
    margin-bottom: 3vh;
    color: var(--color-black);
    background-color: var(--color-lightblue);
}
</style>
