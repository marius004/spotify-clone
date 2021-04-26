<template>
<div class="artist-container">
    <div class="row">

    <div class="col-lg-12">

            <div style="border: none;" class="card flex-md-row mb-4 box-shadow h-md-250">
                <div class="col-lg-6">
                     <img style="border: none;" class="card-img-right flex-auto d-md-block" :src="image" >
                </div>
                
                <div class="col-lg-6 description">
                    <router-link v-if="artistLink" :to="artistLink">
                        <h1>{{name}}</h1>
                    </router-link>
                    <h1 v-else>
                        {{name}}
                    </h1>
                    <div class="stars">
                        <i class="fa fa-star fa-lg" 
                        v-for="star in 5" 
                        :key="star" 
                        aria-hidden="true"
                        :style="{'color': (star <= rating) ? '#ffd600' : '#bdbdbd'}">
                        </i>
                    </div>
                    <h4>
                        <i class="fa fa-quote-left"></i>
                        {{quote}}
                        <i class="fa fa-quote-right"></i>
                    </h4>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
export default {
    name: "ArtistCard", 
    
    props: {
        image: {
            type: String, 
            required: true,
        },
        name: {
            type: String, 
            required: true,
        }, 
        rating: {
            type: Number, 
            required: true,
        }, 
        quote: {
            type: String, 
            required: true,
        }, 
        artistLink: {
            type: String, 
            required: false,
        }
    },
}
</script>

<style scoped>
* { font-weight: bold; }
img {
    margin-top: 20px;
    width: 100%;
    min-height: 30vh;
    max-height: 45vh;
}
.artist-container {
    width: 94%;
    margin: 20px auto 20px auto;
    -webkit-box-shadow: 15px 44px 124px 0px rgba(133,124,133,1);
    -moz-box-shadow: 15px 44px 124px 0px rgba(133,124,133,1);
    box-shadow: 15px 44px 124px 0px rgba(133,124,133,1);
}
@media only screen and (max-width: 1050px) {
    img {
        max-height: 15vh;
    }
    .fa-star {
        font-size: 24px;
    }
    .stars {
        margin-bottom: 12px;
    } 
    h4 {
        font-size: 18px;
    }
}
.description {
    position: relative;
    padding-top: 20px;
}
.fa-star {
    font-size: 36px;
}
.stars {
    margin-bottom: 20px;
}
</style>