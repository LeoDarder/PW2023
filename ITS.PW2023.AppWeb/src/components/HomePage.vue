<template>
    <div class="header">
        <nav class="navbar">
            <div class="nav-menu">
                <a class="nav-link active" aria-current="page" href="#">
                    <i class="bi bi-balloon-heart" style="font-size: xx-large;"></i> <!-- tipo logo-->
                </a>
                <a class="nav-link" href="#">Qualcosa</a>
                <a class="nav-link" href="#">Qualcos'altro</a>
            </div>
            <div class="nav-logout">
                <button class="btn logoutButton" type="button" @click="logout">
                    <i class="bi bi-person-fill-x" style="font-size: large;"></i>
                </button>
            </div>
        </nav>
    </div>
</template>

<script>
export default {
    name: "HomePage",
    methods: {
        logout() {
            this.$emit("changeComponent", "LoginPage")
        }
    }
}
</script>

<style>
    .header {
        width: 100%;
        color: var(--color-p3-bg);
        background-color: var(--color-p3-text);
        padding: 0 1%;
    }
    .nav-menu {
        display: flex;
        flex-wrap: inherit;
        align-items: center;
        gap: 10px;
    }
    .logoutButton {
        border: 2px solid var(--color-p3-bg);
        color: var(--color-p3-bg);
        background-color: transparent;
    }
    .logoutButton:hover {
        border: 2px solid var(--color-p3-bg);
        color: var(--color-p3-text);
        background-color: var(--color-p3-bg);
    }
    .logoutButton:hover::after {
        content: " LOGOUT";
    }
</style>