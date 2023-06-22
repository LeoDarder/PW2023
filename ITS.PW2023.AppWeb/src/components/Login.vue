<template>
    <div class="login">
        <div class="title">
            <i class="bi bi-droplet-half"></i>
            <p>Earth4Sport</p>
        </div>
        <div class="loginForm">
            <div class="input-group flex-nowrap loginField">
                <span class="input-group-text icon" id="addon-wrapping"><i class="bi bi-person-fill"></i></span>
                <input type="text" class="form-control font" placeholder="Username" aria-label="Username"
                    aria-describedby="addon-wrapping">
            </div>
            <div class="input-group flex-nowrap loginField">
                <span class="input-group-text icon" id="addon-wrapping"><i class="bi bi-key-fill"></i></span>
                <input type="password" class="form-control font" placeholder="Password" aria-label="Password"
                    aria-describedby="addon-wrapping">
            </div>
            <div class="col-12">
                <button type="submit" class="btn loginButton" @click="validateCredentials" value="">
                    <span><i class="bi bi-person-fill-check" style="font-size: large;"></i></span>
                </button>
            </div>
            <div class="alert alert-danger align-items-center errorAlert" role="alert" ref="error" :class="{ 'classError': classError }">
                <i class="bi bi-person-fill-slash"></i>&nbsp;{{ error }}
            </div>
        </div>
    </div>
</template>

<script>
import { SHA256 } from 'crypto-js';

const baseUrl = "https://cper-pw2023-gruppo5-api.azurewebsites.net";
const username = "Carletto";
const pw = "cagasbura";

export default {
    name: "LoginPage",
    data() {
        return {
            error: "",
            classError: false
        }
    },
    methods: {
        async validateCredentials() {
            var criptedPassw = SHA256(pw).toString();
            var credentials = await fetch(`${baseUrl}/getUserData?username=${username}&password=${criptedPassw}`);
            var userData = await credentials.json();
            console.log("Request status", credentials.status);
            
            if (credentials.status === 200) {
                this.reditectToHomePage();
                this.$emit("successfulLogin", userData);
            }
            else if (credentials.status === 400) {
                this.changeErrorMessage("Incorrect username or password");
            }
            else {
                this.changeErrorMessage("Unknown error");
            }
        },
        reditectToHomePage() {
            this.$emit("changeComponent", "HomePage");
        },
        changeErrorMessage(error) {
            this.error = error;
            this.classError = true;
            setTimeout(() => {
                this.classError = false;
            }, 3000)
        }
    }
}
</script>

<style>
@font-face {
    font-family: LemonMilk;
    src: url(../../fonts/LEMONMILK-Medium.otf);
}

.login {
    top: 50%;
    position: absolute;
    transform: translate(0, -50%);
}

.title {
    font-family: LemonMilk;
    font-size: 90px;
    font-style: italic;
    text-align: center;
    margin-bottom: 15px;
}

.loginForm {
    margin: auto;
}

.loginField {
    width: 50%;
    height: 40px;
    margin: 0px auto 15px;
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
    font-family: LemonMilk;
    height: 45px;
    border: 2px solid var(--color-darkblue);
    color: var(--color-darkblue);
    background-color: transparent;
    margin: 0px auto 15px;
}

.loginButton:hover {
    color: var(--color-lightblue);
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
    display: none;
    opacity: 0;
    top: 50%;
    left: 25px;
    transform: translate(0, -50%);
    transition: 0.5s;
    color: var(--color-lightblue);
}

.loginButton:hover span {
    padding-right: 65px;
}

.loginButton:hover span:after {
    display: block;
    opacity: 1;
    right: 0;
}

.errorAlert {
    visibility: hidden;
    font-size: initial;
    width: fit-content;
    margin: auto;
}

.classError {
    animation-name: displayAlert;
    animation-duration: 3s; 
}

@keyframes displayAlert {
    0% {
        visibility: hidden;
        opacity: 0;
    }
    25% { 
        visibility: visible;
        opacity: 1;
    }
    75% {
        visibility: visible;
        opacity: 1;
    }
    100% { 
        visibility: hidden;
        opacity: 0;
    }
}
</style>