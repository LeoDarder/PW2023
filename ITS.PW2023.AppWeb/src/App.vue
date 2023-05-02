<template>
  <component @changeComponent="changeComponent" v-bind:is="displayedComponent"></component>
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
      displayedComponent: "LoginPage"
    }
  },
  methods: {
    changeComponent(otherComponent) {
      this.displayedComponent = otherComponent
    }
  }
}
</script>

<style>
    #app {
        font-family: Rockwell;
        -webkit-font-smoothing: antialiased;
        -moz-osx-font-smoothing: grayscale;
        text-align: center;
        display: flex;
        justify-content: center;
        align-items: center;
        color: var(--color-darkblue);
    }
</style>
