<template>
    <div class="login">
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
                <button type="submit" class="btn loginButton" @click="validateCredentials" value="">
                    <span><i class="bi bi-person-fill-check" style="font-size: large;"></i></span>
                </button>
            </div>
        </div>
    </div>
</template>

<script>
export default {
        name: "LoginPage",
        methods: {
            validateCredentials() {
                this.reditectToHomePage();
            },
            reditectToHomePage() {
                this.$emit("changeComponent", "HomePage")
            }
        }
}
</script>

<style>
    .login {
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
        height: 45px;
        border: 2px solid var(--color-p3-text);
        color: var(--color-p3-text);
        background-color: transparent;
        transition: linear 1s;
    }
    .loginButton:hover {
        border: 2px solid var(--color-p3-text);
        color: var(--color-p3-bg);
        background-color: var(--color-p3-text);
    }

    .loginButton span {
        cursor: pointer;
        display: inline-block;
        position: relative;
        transition: 1s;
    }
    .loginButton span:after {
        content: " LOGIN";
        position: absolute;
        opacity: 0;
        top: 0;
        left: 30px;
        transition: 1s;
        color: var(--color-p3-bg);
    }
    .loginButton:hover span {
        padding-right: 60px;
    }

    .loginButton:hover span:after {
        opacity: 1;
        right: 0;
    }
</style>