<template>
    <div class="app">
        <div class="title">
            <h1>NOME APP</h1>
        </div>
        <div class="loginForm">
            <div class="input-group flex-nowrap loginField">
                <span class="input-group-text icon" id="addon-wrapping"><i class="bi bi-person-fill"></i></span>
                <input type="text" class="form-control font" placeholder="Username" aria-label="Username" aria-describedby="addon-wrapping">
            </div>
            <div class="input-group flex-nowrap loginField">
                <span class="input-group-text icon" id="addon-wrapping"><i class="bi bi-key-fill"></i></span>
                <input type="password" class="form-control font" placeholder="Password" aria-label="Password" aria-describedby="addon-wrapping">
            </div>
            <div class="col-12">
                <button type="submit" class="btn loginButton" @click="validateCredentials">LOGIN</button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
        name: "LoginPage",
        methods: {
            validateCredentials() {
                /*this.reditectToHomePage();*/
            },
            redirectToHomePage() {
                // redirect
            }
        }
}
</script>

<style>
    .app {
        top: 50%;
        position: absolute;
        transform: translate(0, -50%);
    }
    .title {
        text-align: center;
        margin-bottom: 15px;
    }
    .loginForm {
        margin: auto;
    }
    .loginField {
        margin-bottom: 15px;
        height: 40px;
    }
    .icon {
        font-size: large;
        color: var(--color-p3-text);
    }
    .font {
        font-family: Helvetica;
        color: var(--color-p3-text);
    }
    .loginButton {
        border: 2px solid var(--color-p3-text);
        color: var(--color-p3-text);
        background-color: transparent;
    }
    .loginButton:hover {
        border: 2px solid var(--color-p3-text);
        color: var(--color-p3-bg);
        background-color: var(--color-p3-text);
    }
</style>