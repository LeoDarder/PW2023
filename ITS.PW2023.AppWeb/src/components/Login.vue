<template>
    <div class="login">
        <div class="title">
            <i class="bi bi-droplet-half" style="font-size: 100px;"></i>
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
        color: var(--color-darkblue);
    }
    .font {
        font-family: Helvetica;
        color: var(--color-darkblue);
    }
    .loginButton {
        height: 45px;
        border: 2px solid var(--color-darkblue);
        color: var(--color-darkblue);
        background-color: transparent;
    }
    .loginButton:hover {
        border: 2px solid var(--color-darkblue);
        color: var(--color-beige);
        background-color: var(--color-darkblue);
    }
    .loginButton span {
        cursor: pointer;
        display: inline-block;
        position: relative;
        transition: 0.5s;
    }
    .loginButton span:after {
        content: " LOGIN";
        position: absolute;
        opacity: 0;
        top: 50%;
        left: 30px;
        transform: translate(0, -50%);
        transition: 0.5s;
        color: var(--color-beige);
    }
    .loginButton:hover span {
        padding-right: 65px;
    }
    .loginButton:hover span:after {
        opacity: 1;
        right: 0;
    }
</style>