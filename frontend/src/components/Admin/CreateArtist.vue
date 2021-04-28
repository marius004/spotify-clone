<template>
     <div class="form-container">
        <form @submit.prevent="handleFormSubmit">
            <div class="form-group">
                <input v-model="name" type="text" class="form-control" id="name" required placeholder="Enter Name">
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
            <div class="form-group">
                <input v-model="quote" type="text" class="form-control" id="quote" required placeholder="Enter Quote">
            </div> 
            <div class="form-group custom-file choose-image">
                <input required type="file" @change="onFileChange" accept="image/*" class="custom-file-input" id="customFile">
                <label class="custom-file-label" for="customFile">Choose image file</label>
            </div> 
            <div class="form-group">
                <button class="create-btn btn btn-primary">Submit</button>
            </div>               
        </form> 
    </div>                  
</template>

<script>
import {getCategories} from '../../_services/category.service.js';
import {imageToBase64} from '../../_helpers/conversions.js';
import artistService from '../../_services/artist.service.js';

export default {
    name: "CreateArtist", 

    data() {
        return {
            name: "",
            categories: [],
            categoriesSelected: [],
            quote: "",
            b64Image: "",
        }
    }, 

    async created() {
        const categoriesRes = await getCategories();
        this.categories = categoriesRes.data;
    },

    methods: {
        async onFileChange(event) {
            const file = event.target.files[0];
            imageToBase64(file, result => { // TODO later refactor this using async/await
                this.b64Image = result.substring(23);
            });
        },
        async handleFormSubmit() {
            try {
                await artistService.createNewArtist(this.name, this.categoriesSelected, this.quote, this.b64Image);
            } catch(err) {
                console.error(err);
            }
        },
    }
}
</script>

<style scoped>
* { font-weight: 900; }
.form-container {
    width: 96%;
    margin: 0 auto;
    padding: 12px 16px;
}
.select-artist, .select-categories {
    text-align: left;
}
.select-categories > label {
    font-size: 20px;
    margin-right: 4px;
}
.create-btn {
    width: 100%;
}
.choose-image {
    margin-bottom: 12px;
}
</style>