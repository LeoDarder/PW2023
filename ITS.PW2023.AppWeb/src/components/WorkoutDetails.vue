<template>
    <div class="detailCard">
        <div class="general info">
            <span><b>Activity</b> {{ activity }}</span>
            <span><b>Date</b> {{ date }}</span>
            <span><b>Duration</b> {{ duration }}</span>
        </div>
        <div class="general graphs">
            <div class="graph">
                <canvas id="graph" ref="graph"></canvas>
            </div>
            <div class="tubs">
                <canvas id="tubs" ref="tubs"></canvas><br>
                <b>Obiettivo</b> {{ obiettivo }}<br>
                <b>Tubs completed</b> {{ nTubs }}
            </div>
        </div>
        <div class="general position" id="map"></div>
    </div>
</template>

<script>
import * as L from 'leaflet';
import Chart from 'chart.js/auto';

export default {
    name: "WorkoutDetails",
    data() {
        return {
            nTubs: null,
            obiettivo: null
        }
    },
    computed: {
        activity() {
            return "(guid)";
        },
        date() {
            return Date.now();
        },
        duration() {
            return Math.floor(Math.random() * 90);
        }
    },
    mounted() {
        this.initMap();
        this.initHRGraph();
        this.initTubsGraph();
    },
    methods: {
        initMap() {
            var map = L.map('map').setView([0.0, 0.0], 8);

            L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
                maxZoom: 19,
                attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
            }).addTo(map);

            var marker = L.marker([0.0, 0.0]).addTo(map);
            marker.bindPopup("<b>LAT</b> 0.0<br></br><b>LON</b> 0.0").openPopup();

            L.circle([0.0, 0.0], {
                color: '#1088a0', // dark blue
                fillColor: '#78b2c4', // blue
                fillOpacity: 0.5,
                radius: 20
            }).addTo(map);
        },
        initHRGraph() {
            var data = []
            // this.getValues(); // riempire i dati con le API
            for (var i = 0; i < 10; i++) {
                data.push({
                    time: Date.now(),
                    hb: Math.floor(Math.random() * 100)
                })
            }

            new Chart(document.getElementById('graph').getContext("2d"), {
                type: 'line',
                data: {
                    labels: data.map(row => row.time),
                    datasets: [
                        {
                            label: "Average heart rate",
                            data: data.map(row => row.hb),
                            fill: true,
                            borderColor: '#1088a0', // dark blue
                            tension: 0.1
                        }
                    ]
                }
            })

        },
        initTubsGraph() {
            // this.getValues(); // riempire i dati con le API
            this.nTubs = Math.floor(Math.random() * 20)
            this.obiettivo = 20

            var perc = (this.nTubs / this.obiettivo) * 100;

            new Chart(document.getElementById('tubs').getContext("2d"), {
                type: 'doughnut',
                data: {
                    labels: ['nTubs', 'obiettivo'],
                    datasets: [
                        {
                            // cambiare le label con i valori delle tubs completate e mancanti
                            data: [perc, (100 - perc)],
                            backgroundColor: [
                                '#1088a0', // dark blue
                                '#78b2c4' // blue
                            ],
                        }
                    ]
                }
            })

        },
        getValues() {

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
    width: 100%;
}

.position {
    height: 300px;
}
</style>