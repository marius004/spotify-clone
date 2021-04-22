<template>
  <div class="home">
    <Navbar />
    <Jumbotron 
      title="Listen to music. Everywhere."
      buttonTitle="Listen to music"
    />
    <SongShowCase 
      :cards="cards" 
    />
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
  }, 
  data() {
    return {
      cards: [
          [{
                  title: 'Mark Tremonti',
                  image: 'mark-tremonti.jpg'
              },
              {
                  title: 'Snoop Dogg',
                  image: 'snoop-dogg.jpg'
              },
              {
                  title: 'Chad Kroeger',
                  image: 'chad-kroeger.jpg'
              }
          ],
          [{
                  title: 'Creed',
                  image: 'creed.jpg'
              },
              {
                  title: 'Eminem',
                  image: 'eminem.jpg'
              },
              {
                  title: 'Alter Bridge',
                  image: 'alter-bridge.jpg'
              }
          ]
      ]
    }
  }
}
</script>
