<template>
  <div class="home">
    <Navbar />
    <Jumbotron 
      title="Listen to music. Everywhere."
      buttonTitle="Go to player"
    />
    <SongShowCase />
    <Footer />
  </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue';
import Jumbotron from '@/components/Jumbotron.vue';
import SongShowCase from '@/components/SongShowCase.vue';
import Footer from '@/components/Footer.vue';

export default {
  name: 'Home',
  components: {
    Navbar,
    Jumbotron,
    SongShowCase,
    Footer,
  }
}
</script>
