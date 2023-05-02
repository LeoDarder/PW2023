<template>
    <div class="activities">
        <div class="card" style="margin-left: 20px">
            <i class="bi bi-heart-pulse-fill activityIcon"></i>
            <div class="card-body">
                <h5 class="card-title">Heart rate</h5>
                <p class="card-text">Heart rate tracking during workouts and while wearing the smartwatch</p>
                <a href="#" class="btn details">DETAILS</a>
            </div>
        </div>
        <div class="card">
            <i class="bi bi-geo-alt-fill activityIcon"></i>
            <div class="card-body">
                <h5 class="card-title">Position</h5>
                <p class="card-text">Position rate tracking during workouts and while wearing the smartwatch</p>
                <a href="#" class="btn details">DETAILS</a>
            </div>
        </div>
        <div class="card">
            <i class="bi bi-life-preserver activityIcon"></i>
            <div class="card-body">
                <h5 class="card-title">No. tubs</h5>
                <p class="card-text">No. tubs rate tracking during workouts and while wearing the smartwatch</p>
                <a href="#" class="btn details">DETAILS</a>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    name: "ActivityCard"
}
</script>

<style>
    .activities {
        display: flex;
        gap: 20px;
    }
    .card {
        width: 16rem;
        margin-top: 20px;
    }
    .activityIcon {
        font-size: 80px;
        color: var(--color-darkblue);
    }
    .card-title {
        font-weight: bold;
    }
    .details {
        border: 2px solid var(--color-darkblue);
        color: var(--color-darkblue);
        background-color: transparent;
    }
    .details:hover {
        border: 2px solid var(--color-darkblue);
        color: var(--color-beige);
        background-color: var(--color-darkblue);
    }
</style>