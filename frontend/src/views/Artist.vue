<template>
    <ArtistCard 
        :image="image" 
        :name="data && data.name ? data.name : ''"
        :rating="data && data.rating ? data.rating : -1"
        :loaded="loaded"
        quote="That's one of the main thing with the lyrics - not giving any answers."
    />

    <div style="margin-top: 100px;" v-for="(audio, index) in audioData.data" :key="index">
        <audio controls>
            <source :src="'data:audio/wav;base64,' + audio.audio" type="audio/base64">
            <source :src="'data:audio/wav;base64,' + audio.audio" type="audio/mpeg">
            Your browser does not support the audio tag.
        </audio>
    </div>
    
</template>
 
<script>
import artistService from '../_services/artist.service.js';
import ArtistCard from '@/components/ArtistCard.vue';
import songService from '../_services/song.service.js';

export default {
    name: "Artist",

    async created() {
        this.artistId = this.$route.params.id;
        try {
            this.data = await artistService.get(this.artistId);
            this.data.rating = 3;
        }catch {
            this.data = { error: "Artist not found" };
            this.$router.push('/');
        }

        try {
            this.audioData = await songService.getBySinger(this.artistId);
            this.loaded = true;
        }catch {
            console.log("bad");
        }

    }, 

    data() {
        return {
            artistId: "",
            data: {},
            loaded: false,
            audioData: "",
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
    }
}
</script>

<style scoped>
</style>
