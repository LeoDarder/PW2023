<template>
    <div class="page">
        <div class="header">
            <nav class="navbar">
                <div class="nav-menu">
                    <a class="nav-link active" aria-current="page" href="#">
                        <i class="bi bi-droplet-half" style="font-size: xx-large;"></i>
                    </a>
                    <a class="nav-link" href="#">Qualcosa</a>
                    <a class="nav-link" href="#">Qualcos'altro</a>
                </div>
                <div class="nav-logout">
                    <button class="btn logoutButton" type="button" @click="logout">
                        <span><i class="bi bi-person-fill-x" style="font-size: large;"></i></span>
                    </button>
                </div>
            </nav>
        </div>
        <div class="body">
            <activity></activity>
        </div>
    </div>
</template>

<script>
import Activity from './Activity.vue'

export default {
    name: "HomePage",
    components: {
        Activity
    },
    methods: {
        logout() {
            this.$emit("changeComponent", "LoginPage")
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
    opacity: 1;
    right: 0;
}
</style>