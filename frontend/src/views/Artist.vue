<template>

    <div v-if="!loaded">
        <Loading
            text="Fetching artist data..."
        />
    </div>

    <div v-else>

        <ArtistCard 
            :image="image"
            :artistId="artistId" 
            :name="artistData && artistData.name ? artistData.name : ''"
            :rating="artistData && artistData.rating ? artistData.rating : -1"
            :quote="artistData.quote"
        />

        <Songs v-if="audioData.length > 0" 
            :audios="audioData"
            :activeSong="index"
            @play="handleSongsBtnClick"
        />

        <Player v-if="audioData.length > 0"
            :currentSong="audioData[index].audio"
            @next="nextSong"
            @prev="prevSong"
        />
    
    </div>

</template>
 
<script>
import artistService from '../_services/artist.service.js';
import ArtistCard from '@/components/ArtistCard.vue';
import songService from '../_services/song.service.js';
import Songs from '@/components/Songs.vue';
import Loading from '@/components/Loading.vue';
import Player from '@/components/Player.vue';

export default {
    name: "Artist",

    async created() {
        this.artistId = this.$route.params.id;

        try {
            let artistReq = artistService.getById(this.artistId);
            let songReq   = songService.getBySinger(this.artistId);
            
            const artistRes = await artistReq;
            const songRes   = await songReq;

            this.artistData = artistRes.data;
            this.audioData   = songRes.data;

            this.loaded = true;
        }catch(err) {
            console.error(err);
            this.$router.push('/');
        }
    }, 

    methods: {
        nextSong() {
           if(this.index === this.audioData.length - 1)
                this.index = 0;
           else
                this.index = Math.min(this.index + 1, this.audioData.length - 1);
        },
        prevSong() {
            if(this.index === 0)
                this.index = this.audioData.length - 1;
            else 
                this.index = Math.max(this.index - 1, 0);
        },
        handleSongsBtnClick(index) {
            console.log(index);
            this.index = index;
        },
    },

    beforeUnmount() {
        this.audioData = "";
    },

    data() {
        return {
            artistId: "",
            artistData: {},
            loaded: false,
            audioData: {},
            index: 0,
        }
    },

    computed: {
        image() {
            if(this.artistData && this.artistData.image) 
                return "data:image/png;base64, " + this.artistData.image;
            return "";
        }
    }, 

    components: {
        ArtistCard,
        Songs,
        Loading,
        Player,
    }
}
</script>

<style scoped>
</style>
