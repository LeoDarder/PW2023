<template>
  <component
    :is="displayedComponent"
    :userData="userData"
    @changeComponent="changeComponent"
    @successfulLogin="successfulLogin">
  </component>
</template>

<script>
import LoginPage from './components/Login.vue'
import HomePage from './components/HomePage.vue';

export default {
  name: 'App',
  components: {
    LoginPage,
    HomePage
  },
  data() {
    return {
      displayedComponent: "LoginPage",
      userData: []
    }
  },
  methods: {
    changeComponent(otherComponent) {
      this.displayedComponent = otherComponent;
    },
    successfulLogin(data) {
      this.userData = data;
    }
  }
}
</script>

<style>
    #app {
        font-size: large;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        display: flex;
        justify-content: center;
        align-items: center;
        color: var(--color-darkblue);
    }
</style>
