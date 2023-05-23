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
    width: 60%;
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
}</style>