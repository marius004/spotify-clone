<template>
 <h2 class="title">Update Songs</h2>
    <div class="row updateSongs">
        <div class="col col-lg-6 create-col">
            <h2>Create Song</h2>
            <div class="form-container">
                <form @submit.prevent="handleFormSubmit">
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
                    <div class="form-group select-categories">
                       <label for="categories">Choose categories: </label>
                       <br />
                       <select required v-model="categoriesSelected" name="categories" id="categories" multiple size="3">
                            <option v-for="category in categories"
                                :key="category.id"
                                :value="category.id">
                                {{category.category}}
                            </option>
                        </select> 
                    </div>
                    <div class="form-group custom-file choose-audio">
                        <input required type="file" @change="onFileChange" accept=".mp3,audio/*" class="custom-file-input" id="customFile">
                        <label class="custom-file-label" for="customFile">Choose mp3 file</label>
                    </div>
                    <div class="form-group">
                        <button class="create-btn btn btn-success">Submit</button>
                    </div>
                </form>                
            </div>
        </div>
        <div class="col col-lg-6">

        </div>
    </div>
</template>

<script>
import {audioToBase64} from '../../_helpers/conversions.js';
import {getCategories} from '../../_services/category.service.js';
import artistService from '../../_services/artist.service.js';
import songService from '../../_services/song.service.js';

export default {
    name: "UpdateSongs", 
    
    data() {
        return {
            name: "",
            artistSelected: "607eb9745c0f7f6194dd7922",
            categoriesSelected: [], 
            b64Song: "",
            categories: [],
            artists: [],
        }
    },

    async created() {
        const categoriesRes = await getCategories();
        this.categories = categoriesRes.data;

        const artistsRes = await artistService.getPlainArtists();
        this.artists = artistsRes.data;
    },

    methods: {
        async handleFormSubmit() {
           try {
                await songService.createNewSong(this.name, this.artistSelected, this.categoriesSelected, this.b64Song);
            }catch(err) {
                console.error(err);
            }
        },
        async onFileChange(event) {
            const file = event.target.files[0];
            const res = await audioToBase64(file)
            this.b64Song = res.substring(23) // 23 because we wanna skip "data:audio/mpeg;base64,"
        }
    }
}
</script>

<style scoped>
* { font-weight: 900; }
.updateSongs {
    margin-bottom: 20px;
}
.form-container {
    width: 96%;
    margin: 0 auto;
    padding: 12px 16px;
}
.col {
    border: 1px solid black;
}
.select-artist, .select-categories {
    text-align: left;
}
.select-artist > label, .select-categories > label {
    font-size: 20px;
    margin-right: 4px;
}
.choose-audio {
    margin-bottom: 12px;
}
.create-btn {
    width: 100%;
}
.create-col {
    background-color: #bbdefb;
}
</style>