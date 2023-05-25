<template>
    <div class="page">
        <div class="header">
            <nav class="navbar">
                <div class="nav-menu">
                    <i class="bi bi-droplet-half" style="font-size: xx-large;"></i>
                    <router-link to="/" class="link">Activities</router-link>
                </div>
                <div class="nav-logout">
                    <button class="btn logoutButton" type="button" @click="logout">
                        <span><i class="bi bi-person-fill-x" style="font-size: large;"></i></span>
                    </button>
                </div>
            </nav>
        </div>
        <div class="body">
            <router-view />
        </div>
    </div>
</template>

<script>
export default {
    name: "HomePage",
    methods: {
        logout() {
            this.$emit("changeComponent", "LoginPage");
        }
    }
}
</script>

<style>
@font-face {
    font-family: LemonMilk;
    src: url(../../fonts/LEMONMILK-Medium.otf);
}

.page {
    width: 100%;
}

.header {
    font-family: LemonMilk;
    width: 100%;
    color: var(--color-lightblue);
    background-color: var(--color-darkblue);
    padding: 0 1%;
}

.nav-menu {
    display: flex;
    flex-wrap: inherit;
    align-items: center;
    gap: 10px;
}

.link {
    color: var(--color-lightblue);
    background-color: transparent;
    text-decoration: none;
    border-bottom: solid 1px transparent;
    transition: 0.5s;
}

.link::after {
    content: '';
    width: 0px;
    height: 1px;
    display: block;
    background: var(--color-lightblue);
    transition: 300ms;
}

.link:hover::after {
    width: 100%;
}

.logoutButton {
    font-family: LemonMilk;
    height: 45px;
    border: 2px solid var(--color-lightblue);
    color: var(--color-lightblue);
    background-color: transparent;
}

.logoutButton:hover {
    color: var(--color-darkblue);
    background-color: var(--color-lightblue);
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
    left: 25px;
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
}</style>