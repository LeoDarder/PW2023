<template>
    <div class="detailCard">
        <div class="general info">
            <span><b>Activity</b> {{ actGuid }}</span>
            <span><b>Date</b> {{ formatDate }}</span>
            <span><b>Duration</b> {{ values.duration }} minutes</span>
        </div>
        <div class="general graphs">
            <div class="graph">
                <canvas ref="graph"></canvas>
            </div>
            <div class="laps">
                <div class="lapsGraph" ref="laps"></div><br>
                <p><b>Goal</b> {{ goal }}</p>
            </div>
        </div>
        <div class="general position" id="map"></div>
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
        }
    },
    mounted() {
        this.actGuid = this.$route.params.id;
        this.getValues()
            .then((data) => {
                console.log(data);
                this.initHRGraph(data);
                this.initTubsGraph(data);
                this.initMap(data);
            })

    },
    methods: {
        async getValues() {
            const response = await fetch(`${baseUrl}/getRows?devGUID=${devGuid}&actGUID=${this.actGuid}`);
            this.values = await response.json();
            return this.values;
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
                color: '#1088a0', // dark blue
                fillColor: '#78b2c4', // blue
                fillOpacity: 0.5,
                radius: 20
            }).addTo(map);
        },
        initHRGraph(data) {
            var values = JSON.parse(JSON.stringify(data.hbInstances));

            // TODO cambiare stile del grafico
            new Chart(this.$refs.graph.getContext("2d"), {
                type: 'line',
                data: {
                    labels: values.map(row => {
                        var datetime = new Date(row.time);
                        return `${datetime.toLocaleTimeString()}`
                    }),
                    datasets: [
                        {
                            label: "Average heart rate",
                            data: values.map(row => row.heartBeat),
                            fill: false,
                            borderColor: '#1088a0', // dark blue
                            tension: 0.1
                        }
                    ]
                }
            })
        },
        initTubsGraph(data) {
            this.goal = 10; // da prendere dal login
            var laps = data.laps;
            var perc = (laps / this.goal);

            var lapsCompleted = new ProgressBar.Circle(this.$refs.laps, {
                strokeWidth: 4,
                duration: 1000,
                easing: 'easeInOut',
                color: '#1088a0', // dark blue
                trailColor: '#78b2c4', // blue
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
            lapsCompleted.text.style.fontSize = '50px';
            lapsCompleted.animate(perc >= 1 ? 1 : perc); // da 0.0 a 1.0
        }
    }
}
</script>

<style>
.detailCard {
    width: 100%;
}

.general {
    width: 95%;
    margin: 15px auto;
    border: solid 1px grey;
}

.info {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
}

.graphs {
    display: grid;
    grid-template-columns: 70% 30%;
}

.graph {
    width: 90%;
    margin: auto;
}

.laps {
    width: 60%;
    margin: auto;
}

.position {
    height: 300px;
}
</style>