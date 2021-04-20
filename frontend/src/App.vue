<template>
  <router-view/>
  <Footer />
</template>

<script>
import Footer from '@/components/Footer.vue';

export default {
  name: "App",

  components: {
    Footer,
  },
}
</script>

<style>
@import url('https://fonts.googleapis.com/css2?family=Antonio:wght@500&family=Roboto:ital,wght@1,100&display=swap');

* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

#app {
  font-family: 'Roboto', sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>
