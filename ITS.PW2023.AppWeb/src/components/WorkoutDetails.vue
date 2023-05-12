<template>
    <div class="detailCard">
        <div class="general info">
            <span><b>Activity</b> {{ activity }}</span>
            <span><b>Date</b> {{ date }}</span>
            <span><b>Duration</b> {{ duration }}</span>
        </div>
        <div class="general graphs">
            <h1>HEART RATE</h1>
            <h1>TUBS</h1>
        </div>
        <div class="general position" id="map"></div>
    </div>
</template>

<script>
import * as L from 'leaflet';

export default {
    name: "WorkoutDetails",
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
    .position {
        height: 300px;
    }
</style>