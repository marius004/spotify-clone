<template>
    <ArtistCard 
        :image="image" 
        :name="data && data.name ? data.name : ''"
        :rating="data && data.rating ? data.rating : -1"
        :loaded="loaded"
        quote="That's one of the main thing with the lyrics - not giving any answers."
    />
</template>
 
<script>
import artistService from '../_services/artist.service.js';
import ArtistCard from '../components/ArtistCard.vue';

export default {
    name: "Artist",

    async created() {
        this.artistId = this.$route.params.id;
        try {
            this.data = await artistService.get(this.artistId);
            this.loaded = true;
            this.data.rating = 3;
        }catch {
            this.data = { error: "Artist not found" };
            this.$router.push('/');
        }
    }, 

    data() {
        return {
            artistId: "",
            data: {},
            loaded: false,
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
