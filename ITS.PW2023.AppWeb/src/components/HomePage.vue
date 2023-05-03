<template>
    <div class="page">
        <div class="header">
            <nav class="navbar">
                <div class="nav-menu">
                    <i class="bi bi-droplet-half" style="font-size: xx-large;"></i>
                    <a class="nav-link active" aria-current="page" href="#WorkoutStatistics.vue">Workouts</a>
                    <a class="nav-link" href="#GeneralStatistics.vue">General</a>
                </div>
                <div class="nav-logout">
                    <button class="btn logoutButton" type="button" @click="logout">
                        <span><i class="bi bi-person-fill-x" style="font-size: large;"></i></span>
                    </button>
                </div>
            </nav>
        </div>
        <div class="body">
            <component 
                @navigateTo="navigateTo"
                v-bind:is="currentComponent"
                :statistics="statistics"
                :workouts="workouts">
            </component>
        </div>
    </div>
</template>

<script>
import GeneralStatistics from './GeneralStatistics.vue';
import WorkoutsStatistics from './WorkoutsStatistics.vue';

export default {
    name: "HomePage",
    components: {
        GeneralStatistics,
        WorkoutsStatistics
    },
    data() {
        return {
            statistics: [],
            workouts: [],
            currentComponent: "WorkoutsStatistics"
        }
    },
    mounted() {
        this.defineStatistics();
        this.defineWorkouts();
    },
    methods: {
        logout() {
            this.$emit("changeComponent", "LoginPage")
        },
        defineStatistics() {
            var heartRate = {
                id: 0,
                icon: "bi bi-heart-pulse-fill",
                title: "Heart rate",
                text: "Heart rate tracking during workouts and while wearing the smartwatch"
            }
            var position = {
                id: 1,
                icon: "bi bi-geo-alt-fill",
                title: "Position",
                text: "Position tracking during workouts and while wearing the smartwatch"
            }
            var tubs = {
                id: 2,
                icon: "bi bi-life-preserver",
                title: "No. tubs",
                text: "No. tubs tracking during workouts"
            }
            this.statistics.push(heartRate, position, tubs);
        },
        defineWorkouts() {
            for (var i = 0; i < 10; i++) {
                var workout = {
                    id: i,
                    time: Date.now(),
                    duration: Math.floor(Math.random() * 90),
                    averageHeartRate: Math.floor(Math.random() * 100),
                    lastPosition: "LAT 0.0.0",
                    numberOfTubs: Math.floor(Math.random() * 10)
                }
                this.workouts.push(workout);
            }
        },
        navigateTo(otherComponent) {
            this.currentComponent = otherComponent;
        }
    }
}
</script>

<style>
.page {
    width: 100%;
}
.header {
    width: 100%;
    color: var(--color-beige);
    background-color: var(--color-darkblue);
    padding: 0 1%;
}
.nav-menu {
    display: flex;
    flex-wrap: inherit;
    align-items: center;
    gap: 10px;
}
.logoutButton {
    height: 45px;
    border: 2px solid var(--color-beige);
    color: var(--color-beige);
    background-color: transparent;
}
.logoutButton:hover {
    border: 2px solid var(--color-beige);
    color: var(--color-darkblue);
    background-color: var(--color-beige);
}
.logoutButton span {
    cursor: pointer;
    display: inline-block;
    position: relative;
    transition: 0.5s;
}
.logoutButton span:after {
    content: " LOGOUT";
    position: absolute;
    display: none;
    opacity: 0;
    top: 50%;
    left: 30px;
    transform: translate(0, -50%);
    transition: 0.5s;
    color: var(--color-darkblue);
}
.logoutButton:hover span {
    padding-right: 80px;
}
.logoutButton:hover span:after {
    display: block;
    opacity: 1;
    right: 0;
}
.body {
    display: flex;
}
</style>