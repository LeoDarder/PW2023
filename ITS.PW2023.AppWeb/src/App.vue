<template>
  <LoginPage></LoginPage>
</template>

<script>
import LoginPage from './components/Login.vue'

export default {
  name: 'App',
  components: {
    LoginPage
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
        color: var(--color-p3-text);
    }
</style>
