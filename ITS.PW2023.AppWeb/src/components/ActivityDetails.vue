<template>
    <div class="detailCard">
        <div class="info">
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
                    <div class="lapsGraph" ref="laps"></div>
                    <div class="description">
                        <span><b class="costumFont">LAPS COMPLETED</b> {{ values.laps }}</span><br>
                        <span><b class="costumFont">Goal</b> {{ goal }}</span><br>
                        <span><b class="costumFont">Distance</b> 1.5km</span>
                    </div>
                </div>
            </div>
            <div class="map" id="map"></div>
        </div>
    </div>
</template>

<script>
import * as L from 'leaflet';
import Chart from 'chart.js/auto';
import ProgressBar from 'progressbar.js'

const baseUrl = "https://cper-pw2023-gruppo5-api.azurewebsites.net";
const devGuid = "aa76d690-29c4-4696-999d-997a18e46e75";

export default {
    name: "ActivityDetails",
    data() {
        return {
            laps: null,
            goal: null,
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
        this.actGuid = this.$route.params.id;
        this.getValues()
            .then((data) => {
                console.log(data);
                this.initHBGraph(data);
                this.initLapsGraph(data);
                this.initMap(data);
            })

    },
    methods: {
        async getValues() {
            const response = await fetch(`${baseUrl}/getRows?devGUID=${devGuid}&actGUID=${this.actGuid}`);
            this.values = await response.json();
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
                            label: "Average Heart Beat",
                            data: values.map(row => row.heartBeat),
                            fill: true,
                            borderColor: '#ED6363', // red
                            backgroundColor: 'rgba(237, 99, 99, 0.3)', // red opacity
                            tension: 0.3
                        }
                    ]
                }
            })
        },
        initLapsGraph(data) {
            this.goal = 10; // da prendere dal login
            var laps = data.laps;
            var perc = (laps / this.goal);

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
                        circle.setText(`<i class="bi bi-water"></i><br><p>${laps}</p>`);
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
@font-face {
    font-family: LemonMilk;
    src: url(../../fonts/LEMONMILK-Medium.otf);
}

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
    padding: 5px;
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    background-color: var(--color-white);
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
    padding: 5px;
    margin-bottom: 10px;
    border-radius: 6px;
    background-color: var(--color-white);
}

.laps {
    width: 100%;
    padding: 10px;
    border-radius: 6px;
    background-color: var(--color-white);
    display: grid;
    grid-template-columns: 50% 50%;
}

.lapsGraph {
    width: 12vw;
    margin: auto;
    position: relative;
}

.description {
    text-align: left;
    display: flex;
    flex-direction: column;
    justify-content: center;
}

.map {
    border-radius: 6px;
    width: 50%;
}
</style>