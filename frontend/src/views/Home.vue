<template>
  <div class="home">
    <Navbar />
    <Jumbotron 
      title="Listen to music. Everywhere."
      buttonTitle="Go to player"
    />
    <SongShowCase />
  </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue';
import Jumbotron from '@/components/Jumbotron.vue';
import SongShowCase from '@/components/SongShowCase.vue';

export default {
  name: 'Home',
  components: {
    Navbar,
    Jumbotron,
    SongShowCase,
  }
}
</script>
