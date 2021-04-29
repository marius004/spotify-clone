<template>
  <div class="home">
    <Navbar />
    <Jumbotron 
      title="Listen to music. Everywhere."
      buttonTitle="Listen to music"
      @btnClick="jumbotronClickHandler"
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

  methods: {
    jumbotronClickHandler() {
      this.$router.push('/webplayer');
    }
  },

  data() {
    return {
      cards: [
          [{
                  title: 'Mark Tremonti',
                  image: 'mark-tremonti.jpg',
                  artistId: '607eb9745c0f7f6194dd7922'
              },
              {
                  title: 'Snoop Dogg',
                  image: 'snoop-dogg.jpg', 
                  artistId: '6089934f857cf0b778978b9a'
              },
              {
                  title: 'Nickleback',
                  image: 'chad-kroeger.jpg', 
                  artistId: '608994b7857cf0b778978b9b'
              }
          ],
          [{
                  title: 'Creed',
                  image: 'creed.jpg', 
                  artistId: '60899572857cf0b778978b9c'
              },
              {
                  title: 'Eminem',
                  image: 'eminem.jpg', 
                  artistId: '6081c7c0eb1113c1a5483a96'
              },
              {
                  title: 'Alter Bridge',
                  image: 'alter-bridge.jpg', 
                  artistId: '60886abfd12ddec30ea90ae9'
              }
          ]
      ]
    }
  }
}
</script>
