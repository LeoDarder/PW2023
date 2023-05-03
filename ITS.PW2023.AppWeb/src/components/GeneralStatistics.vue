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
    props: [
        'statistics'
    ],
    computed: {
        setStatistics() {
            console.log("statistics");
            return this.statistics;
        }
    },
    methods: {
    }
}
</script>