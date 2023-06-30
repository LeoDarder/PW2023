<template>
    <div class="sidebar">
        <div class="device-selection">
            <h5 class="device-label">Device</h5>
            <select class="form-select" v-model="deviceAlias">
                <option
                    v-for="data in userData"
                    :key="data.guidDevice"
                    :value="data.deviceName">
                    {{ data.deviceName }}
                </option>
            </select>
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

export default {
    name: "SideBar",
    props: [
        'userData'
    ],
    data() {
        return {
            loading: true,
            loadingImage: require("../../public/loading.gif"),
            deviceAlias: "",
            deviceSelected: {},
            values: [],
            avgHeartBeat: null,
            avgLaps: null,
        }
    },
    mounted() {
        this.getAvgs(this.deviceSelected.guidDevice)
            .then(() => {
                this.deviceAlias = this.userData[0].deviceName;
            });
    },
    methods: {
        async getAvgs(device) {
            const avgHB = await fetch(`${baseUrl}/getAvgHB?devGUID=${device}`);
            this.avgHeartBeat = await avgHB.json();

            const avgLaps = await fetch(`${baseUrl}/getAvgLaps?devGUID=${device}`);
            this.avgLaps = await avgLaps.json();

            this.loading = false;
        }
    },
    watch: {
        deviceAlias(newVal) {
            this.userData.forEach(data => {
                if (data.deviceName === newVal) {
                    this.deviceSelected = data;
                    this.getAvgs(this.deviceSelected.guidDevice);
                    this.$emit("deviceSelected", this.deviceSelected);
                }
            });
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

.device-selection {
    display: flex;
    flex-direction: column;
    align-items: flex-start;
    margin-bottom: 3vh;
}

.device-label {
    font-family: LemonMilk;
    font-size: initial;
    margin-bottom: 5px;
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

.card-title {
    font-weight: bold;
    margin-bottom: 0px;
}

.card-icon {
    font-size: 70px;
    color: var(--color-darkblue);
}

.card-value {
    font-weight: bold;
    font-size: xx-large;
    margin-bottom: 0px;
}
</style>
