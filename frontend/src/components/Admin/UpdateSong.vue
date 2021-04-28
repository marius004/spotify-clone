<template>
    <p class="text-danger text-left">{{searchErr}}</p>
    <form class="form-inline" @submit.prevent="searchSongHandler">
        <div class="form-group mb-2">
            <br/>
            <label for="songId" class="sr-only">Song Id</label>
            <input v-model="songId" required type="text" class="search-input form-control" id="songId" placeholder="Enter Id">
        </div>
        <button style="margin-left: 12px;" type="submit" class="btn btn-primary mb-2">Search</button>
    </form>
    <form v-if="showUpdateForm" @submit.prevent="handleFormSubmit">
        <div class="form-group">
            <input v-model="name" type="text" class="form-control" id="name" required placeholder="Enter Name">
        </div>
        <div class="form-group select-artist">
            <label for="artists">Choose an artist: </label>
            <select required v-model="artistSelected" name="artists" id="artists">
                <option v-for="artist in artists" 
                        :key="artist.id"
                        :value="artist.id">
                        {{artist.name}}
                </option>
            </select>
        </div>
        <div class="form-group custom-file choose-audio">
            <input type="file" @change="onFileChange" accept=".mp3,audio/*" class="custom-file-input" id="customFile">
            <label class="custom-file-label" for="customFile">Choose mp3 file</label>
        </div>
        <div class="form-group">
            <button class="update-btn btn btn-success">Submit</button>
        </div>
    </form>
</template>

<script>
import songService from '../../_services/song.service';
import artistService from '../../_services/artist.service';
import {audioToBase64} from '../../_helpers/conversions.js';

export default {
    name: "UpdateSong",
    
    data() {
        return {
            songId: "",
            name: "",
            artistId: "",
            audio: "",
            artistSelected: "", 
            showUpdateForm: false,
            searchErr: "",
            artists: [],
            b64Song: "",
        }
    },

    async created() {
        const artistsRes = await artistService.getPlainArtists();
        this.artists = artistsRes.data;
    },

    methods: {
        async searchSongHandler() {
            try {
                const res = await songService.getById(this.songId);
                const data = res.data;
                this.artistSelected = data.artistId;
                this.artistId = data.artistId;
                this.name = data.name;
                this.showUpdateForm = true;
                this.searchErr = "";
                this.b64Song = data.audio;
            }catch(err) {
                console.error(err);
                this.searchErr = "Song not found";
                this.showUpdateForm = false;
            }
        }, 
        async onFileChange(event) {
            const file = event.target.files[0];
            const res = await audioToBase64(file)
            this.b64Song = res.substring(23) // 23 because we wanna skip "data:audio/mpeg;base64,"
        }, 
        async handleFormSubmit() {
           try {
                await songService.updateSong(this.songId, this.name, this.artistId, this.b64Song);
                this.showUpdateForm = false;
                this.songId = "";
           }catch(err) {
               console.eror(err);
           }
        }
    }
}
</script>

<style scoped>
.select-artist, .select-categories {
    text-align: left;
}
.update-btn {
    margin-top: 12px;
    width: 100%;
}
.search-input {
    width: 100%;
}
</style>