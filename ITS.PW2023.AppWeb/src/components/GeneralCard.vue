<template>
    <div class="card">
        <i :class="[setIcon, 'activityIcon']"></i>
        <div class="card-body">
            <h5 class="card-title">{{ setTitle }}</h5>
            <p class="card-text">{{ setText }}</p>
        </div>
        <a href="#" class="btn details" @click="seeDetails">DETAILS</a>
    </div>
</template>

<script>
export default {
    name: "GeneralCard",
    props: [
        'icon',
        'title',
        'text'
    ],
    computed: {
        setIcon() {
            return this.icon;
        },
        setTitle() {
            return this.title;
        },
        setText() {
            return this.text;
        }
    },
    methods: {
        navigateTo() {
            this.$emit("navigateTo", "WorkoutStatistics");
        },
        seeDetails() {
            // ...
        }
    }
}
</script>

<style>
    .card {
        width: 20rem;
        margin: 30px 0px 0px 30px;
    }
    .activityIcon {
        font-size: 80px;
        color: var(--color-darkblue);
    }
</style>