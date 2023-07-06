<template>
    <div v-if="!loading" class="detailCard">
        <div class="info">
            <router-link to="/" class="btn back"><i class="bi bi-caret-left-fill"></i></router-link>
            <span><b class="costumFont">Date</b> {{ formatDate }}</span>
            <span><b class="costumFont">Time</b> {{ formatTime }}</span>
            <span><b class="costumFont">Duration</b> {{ values.duration }} minutes</span>
        </div>
        <div class="graphs">
            <div class="small-graphs">
                <div class="graph">
                    <span><b class="costumFont">AVERAGE HEART BEAT</b></span><br>
                    <canvas ref="graph"></canvas>
                </div>
                <div class="laps">
                    <span><b class="costumFont">LAPS COMPLETED</b></span><br>
                    <div class="lapsGraphs">
                        <div class="lapsGraph" ref="laps"></div>
                        <div class="description">
                            <span><b class="costumFont desc">Goal</b> {{ goal }}</span><br>
                            <span><b class="costumFont desc">Distance</b> {{ distance }} m</span>
                        </div>
                    </div>
                </div>
            </div>
                <div class="map" id="map"></div>
            </div>
        </div>
    <div v-else class="loading">
        <img :src="loadingImage" style="margin-top: calc(50% + 64px);"/>
        <h4>LOADING ...</h4>
    </div>
</template>

<script>
import * as L from 'leaflet';
import Chart from 'chart.js/auto';
import ProgressBar from 'progressbar.js'

const baseUrl = "https://cper-pw2023-gruppo5-api.azurewebsites.net";

export default {
    name: "ActivityDetails",
    props: [
        'userData'
    ],
    data() {
        return {
            loading: true,
            loadingImage: require("../../public/swimming-loader-2.gif"),
            name: null,
            deviceSelected: {},
            laps: null,
            goal: null,
            distance: null,
            actGuid: null,
            values: {}
        }
    },
    computed: {
        formatDate() {
            var date = new Date(this.values.date);
            return `${date.toLocaleDateString()}`
        },
        formatTime() {
            var date = new Date(this.values.date);
            return `${date.toLocaleTimeString([], {hour: '2-digit', minute:'2-digit'})}`
        }
    },
    mounted() {
        this.$emit("openedDetails");
        
        this.actGuid = this.$route.params.id;
        this.name = this.$route.params.name;

        this.userData.forEach(data => {
            if (data.deviceName === this.name) {
                this.deviceSelected = data;
            }
        })

        this.getValues(this.deviceSelected.guidDevice)
            .then((data) => {
                console.log(data);
                this.initHBGraph(data);
                this.initLapsGraph(data);
                this.initMap(data);
            });
    },
    methods: {
        async getValues(device) {
            const response = await fetch(`${baseUrl}/getRows?devGUID=${device}&actGUID=${this.actGuid}`);
            this.values = await response.json();
            this.loading = false;
            return this.values;
        },
        initHBGraph(data) {
            var values = JSON.parse(JSON.stringify(data.hbInstances));

            new Chart(this.$refs.graph.getContext("2d"), {
                type: 'line',
                data: {
                    labels: values.map(row => {
                        var datetime = new Date(row.time);
                        return `${datetime.toLocaleTimeString()}`
                    }),
                    datasets: [
                        {
                            label: "Avg. heart beat",
                            data: values.map(row => row.heartBeat),
                            fill: true,
                            borderColor: '#ED6363', // red
                            backgroundColor: 'rgba(237, 99, 99, 0.3)', // red opacity
                            tension: 0.3
                        }
                    ]
                },
                options: {
                    scales: {
                        y: {
                            min: 30,
                            max: 200,
                        }
                    }
                }
            })
        },
        initLapsGraph(data) {
            this.userData.forEach(data => {
                if (data.guidDevice === this.deviceSelected.guidDevice) {
                    this.goal = data.desiredLaps;
                }
            })
            var laps = data.laps;
            var perc = (laps / this.goal);

            
            this.distance = (data.laps * 50);
            console.log("data.laps", data.laps);
            console.log("distance", this.distance);

            var lapsCompleted = new ProgressBar.Circle(this.$refs.laps, {
                strokeWidth: 4,
                duration: 1000,
                easing: 'easeInOut',
                color: '#146C94', // --color-darkblue
                trailColor: '#AFD3E2', // --color-lightblue
                trailWidth: 1.5,
                step: function (state, circle) {
                    if (laps === 0) {
                        circle.setText('');
                    }
                    else {
                        circle.setText(`<i class="bi bi-water"></i><br><p style="margin:0px;">${laps}</p>`);
                    }

                }
            });
            lapsCompleted.text.style.fontSize = '40px';
            lapsCompleted.text.style.fontFamily = 'LemonMilk';
            lapsCompleted.animate(perc >= 1 ? 1 : perc); // da 0.0 a 1.0
        },
        initMap(data) {
            var lon = data.position.longitude;
            var lat = data.position.latitude;

            var map = L.map('map').setView([lat, lon], 8);

            L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
                maxZoom: 19,
                attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
            }).addTo(map);

            var marker = L.marker([lat, lon]).addTo(map);
            marker.bindPopup(`<b>LAT</b> ${lat}<br></br><b>LON</b> ${lon}`).openPopup();

            L.circle([lat, lon], {
                color: '#ED6363', // red
                fillColor: 'rgba(237, 99, 99, 0.3)', // red opacity
                fillOpacity: 0.5,
                radius: 20
            }).addTo(map);
        },
    }
}
</script>

<style>
.costumFont {
    font-family: LemonMilk;
}

.detailCard {
    width: 100%;
    color: var(--color-darkblue);
}

.info {
    margin: 10px 10px 0px 10px;
    border-radius: 6px;
    display: grid;
    align-items: center;
    grid-template-columns: 4% 32% 32% 32%;
    background-color: var(--color-white);
}

.back {
    font-family: LemonMilk;
    font-size: large;
    margin: 10px auto;
    border: 2px solid var(--color-darkblue);
    color: var(--color-darkblue);
    background-color: transparent;
}

.back:hover {
    color: var(--color-lightblue);
    background-color: var(--color-darkblue);
}

.graphs {
    display: flex;
    flex-direction: row;
    height: max-content;
    margin: 10px 10px 0px 10px;
}

.small-graphs {
    width: 50%;
    margin-right: 10px;
    border-radius: 6px;
    display: flex;
    flex-direction: column;
}

.graph {
    width: 100%;
    padding: 10px;
    margin-bottom: 10px;
    border-radius: 6px;
    background-color: var(--color-white);
}

.laps {
    width: 100%;
    padding: 10px;
    border-radius: 6px;
    margin-bottom: 10px;
    background-color: var(--color-white);
    display: flex;
    flex-direction: column;
}

.lapsGraphs {
    display: flex;
    flex-direction: row;
}

.lapsGraph {
    width: 10vw;
    margin-left: auto;
    margin-right: 10px;
    position: relative;
}

.description {
    text-align: left;
    margin-left: 10px;
    margin-right: auto;
    display: flex;
    flex-direction: column;
    justify-content: center;
}

.map {
    border-radius: 6px;
    width: 50%;
    margin-bottom: 10px;
}
</style>