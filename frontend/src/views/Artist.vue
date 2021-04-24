<template>

    <div v-if="!loaded">
        <Loading
            text="Fetching artist data..."
        />
    </div>

    <div v-else>

        <ArtistCard 
            :image="image" 
            :name="data && data.name ? data.name : ''"
            :rating="data && data.rating ? data.rating : -1"
            quote="That's one of the main thing with the lyrics - not giving any answers."
        />

        <Songs :audios="audioData"/>

        <!-- <div style="margin-top: 100px;" v-for="(audio, index) in audioData.data" :key="index">
            <audio controls>
                <source :src="'data:audio/wav;base64,' + audio.audio" type="audio/base64">
                <source :src="'data:audio/wav;base64,' + audio.audio" type="audio/mpeg">
                Your browser does not support the audio tag.
            </audio>
        </div> -->

        <Player 
            :audio="audioData[1].audio"
            :title="audioData[1].name"
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
            this.data = await artistService.get(this.artistId);
        }catch {
            this.data = { error: "Artist not found" };
            this.$router.push('/');
        }

        try {
            this.audioData = await songService.getBySinger(this.artistId);
            this.audioData = this.audioData.data;
            this.loaded = true;
        }catch {
            console.log("bad");
        }

    }, 

    methods: {
        nextSong() {
           this.index = 1;
        }, 
        prevSong() {
            this.index = 1;
        }
    },

    data() {
        return {
            artistId: "",
            data: {},
            loaded: false,
            audioData: "",
            index: 0,
        }
    },

    computed: {
        image() {
            if(this.data && this.data.image) 
                return "data:image/png;base64, " + this.data.image;
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
