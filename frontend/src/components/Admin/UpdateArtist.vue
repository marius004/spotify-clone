<template>
    <p class="text-danger text-left">{{searchErr}}</p>
    <form class="form-inline" @submit.prevent="searchArtistHandler">
        <div class="form-group mb-2">
            <br/>
            <label for="songId" class="sr-only">Song Id</label>
            <input v-model="artistId" required type="text" class="search-input form-control" id="songId" placeholder="Enter Id">
        </div>
        <button style="margin-left: 12px;" type="submit" class="btn btn-primary mb-2">Search</button>
    </form>
    <form v-if="showForm" @submit.prevent="updateArtist">
        <div class="form-group">
            <input v-model="name" type="text" class="form-control" id="name" required placeholder="Enter Name">
        </div>
        <div class="form-group select-categories">
            <label for="categories">Choose categories: </label>
            <br />
            <div class="form-group">
                <select required v-model="categoriesSelected" name="categories" id="categories" multiple size="3">
                    <option v-for="category in categories"
                        :key="category.id"
                        :value="category.id">
                        {{category.category}}
                    </option>
                </select> 
            </div>
            <div class="form-group">
                <input v-model="quote" type="text" class="form-control" id="quote" required placeholder="Enter Quote">
            </div>
            <div class="form-group custom-file choose-image">
                <input type="file" @change="onFileChange" accept="image/*" class="custom-file-input" id="customFile">
                <label class="custom-file-label" for="customFile">Choose image file</label>
            </div>
            <div class="form-group">
                <button class="update-btn btn btn-primary">Submit</button>
            </div>
        </div>  
    </form>
</template>

<script>
import artistService from '../../_services/artist.service.js';
import {getCategories} from '../../_services/category.service.js';
import {imageToBase64} from '../../_helpers/conversions.js'

export default {
    name: "UpdateArtist",
    data() {
        return {
            name: "", 
            artistId: "",
            categoriesId: [], 
            rating: 0,
            quote: "",
            image: "",
            searchErr: "",
            showForm: false,
            b64Image: "",
            categories: [],
            categoriesSelected: [],
        }
    },

    async created() {
        const categoriesRes = await getCategories();
        this.categories = categoriesRes.data
    },

    methods: {
        async searchArtistHandler() {
            try {
                const res = await artistService.getById(this.artistId);
                const data = res.data;
                this.name = data.name;
                this.categoriesId = data.categoriesId;
                this.quote = data.quote;
                this.rating = data.rating;
                this.b64Image = data.image;
                this.showForm = true;
                this.searchErr = "";
                this.categoriesSelected = data.categoriesId;
            }catch(err) {
                this.showForm = false,
                console.error(err);
                this.searchErr = "Artist not found";
            }
        }, 
        async updateArtist() {
            //console.log(this.artistId, this.name, this.categoriesId, this.rating, this.quote, this.b64Image);
            try {
                await artistService.updateArtist(this.artistId, this.name, this.categoriesId, this.rating, this.quote, this.b64Image);
                this.showForm = false;
                this.artistId = "";
            } catch(err) {
                console.log(err);
                this.showForm = false;
            }
        },
        async onFileChange(event) {
            const file = event.target.files[0];
            imageToBase64(file, result => { // TODO later refactor this using async/await
                this.b64Image = result.substring(23);
            });
        },
    }
}
</script>

<style scoped>
* { font-weight: 900; }
.select-artist, .select-categories {
    text-align: left;
}
.select-categories > label {
    font-size: 20px;
    margin-right: 4px;
}
.update-btn {
    width: 100%;
}
.choose-image {
    margin-bottom: 12px;
}
</style>