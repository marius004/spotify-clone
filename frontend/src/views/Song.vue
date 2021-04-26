<template>
<div>
    <div v-if="!loaded">
        <Loading 
            text="Fetching song..."
        />
    </div>

    <div v-else>
        
        <h1>{{songTitle}}</h1>
        <h2>Artist: </h2>

        <ArtistCard 
            :image="artistImage"
            :rating="artist.rating"
            :artistId="artist.id"
            :quote="artist.quote"
            :name="artist.name"
            :artistLink="`/artist/${artist.id}`"
        />

        <div class="container">
            <h3>Liked by {{likes}}</h3>
            <i @click="likeClickHandler" class="like fa fa-thumbs-up"></i>
        </div>
        
        <Player 
            :currentSong="audio"
        />

    </div>

</div>
</template>

<script>
import Player from '@/components/Player.vue';
import songService from '../_services/song.service';
import Loading from '@/components/Loading.vue';
import artistService from '../_services/artist.service';
import ArtistCard from '@/components/ArtistCard.vue';
import userService from '../_services/user.service';

export default {
    name: "Song", 
    
    data() {
        return {
            artist: "",
            data: undefined,
            loaded: false,
            likes: 0,
            likesSong: undefined, 
        }
    },

    computed: {
        audio() {
            if(this.data === undefined)
                return "";
            return this.data.audio;
        }, 
        songTitle() {
            if(this.data === undefined)
                return "";
            return this.data.name;
        }, 
        artistImage() {
            if(this.artist && this.artist.image) 
                return "data:image/png;base64, " + this.artist.image;
            return "";
        }
    },

    async created() {
        try {
            const res = await songService.getById(this.$route.params.id);
            const Data = res.data;
            this.data = Data;

            const res2 = await artistService.getById(this.data.artistId);
            this.artist = res2.data;

            const likesReq = await songService.getSongLikes(this.$route.params.id);
            this.likes = likesReq.data;

            this.loaded = true;
        }catch(err) {
            console.error(err);
            this.$router.push('/');
        }
    },

    components: {
        Player,
        Loading,
        ArtistCard
    }, 

    methods: {
        async likeClickHandler() {
            const likedSongs = userService.getSongsLiked();
            if(likedSongs.includes(this.$route.params.id)) {
                userService.removeSongLiked(this.$route.params.id);
                
                await userService.updateSongsLikedOnBackend(this.$route.params.id);

                this.likes--;
            }else { 
                userService.addSongLiked(this.$route.params.id); 

                await userService.updateSongsLikedOnBackend(this.$route.params.id);

                this.likes++;
            }
        }, 
    }, 

}
</script>

<style scoped>
i {
    font-size: 30px;
}
.like {
    color: #42a5f5;
    margin-right: 20px;
    transition: color .6s;
}
.like:hover {
    color: #1976d2;
}
.dislike {
    color: #ff4081;
}
.dislike:hover{
    color: #d50000;
}
</style>
