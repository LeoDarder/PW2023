<template>
    <div class="statisticPage">
        <general-card
            v-for="stat in statistics"
            :key="stat.id"
            :icon="stat.icon"
            :title="stat.title"
            :text="stat.text"
        ></general-card>
    </div>
</template>

<script>
import GeneralCard from './GeneralCard.vue';

export default {
    name: "GeneralStatistics",
    components: {
        GeneralCard
    },
    data() {
        return {
            statistics: []
        }
    },
    mounted () {
        var heartRate = {
            id: 0,
            icon: "bi bi-heart-pulse-fill",
            title: "Heart rate",
            text: "Heart rate tracking during workouts and while wearing the smartwatch"
        };
        var position = {
            id: 1,
            icon: "bi bi-geo-alt-fill",
            title: "Position",
            text: "Position tracking during workouts and while wearing the smartwatch"
        };
        var tubs = {
            id: 2,
            icon: "bi bi-life-preserver",
            title: "No. tubs",
            text: "No. tubs tracking during workouts"
        };
        this.statistics.push(heartRate, position, tubs);
    }
}
</script>