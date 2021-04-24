<template>
<div class="artist-container">
    <div class="row">

    <div class="col-lg-12">

            <div style="margin-bottom: 0;" class="card flex-md-row mb-4 box-shadow h-md-250">
                <div class="col-lg-6">
                     <img class="card-img-right flex-auto d-none d-md-block" :src="image" >
                </div>
                
                <div class="col-lg-6 description">
                    <h1>{{name}}</h1>
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
                    <div class="buttons">
                        <button class="play-btn btn btn-success">Play</button>
                        <i class="like fa fa-thumbs-up"></i>
                        <i class="dislike fa fa-thumbs-down"></i>
                    </div>
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
    },
}
</script>

<style scoped>
* { font-weight: bold; }
img {
    width: 100%;
    max-height: 50vh;
}
.artist-container {
    width: 94%;
    padding: 12px;
    margin: 20px auto 20px auto;
    -webkit-box-shadow: 15px 44px 124px 0px rgba(133,124,133,1);
    -moz-box-shadow: 15px 44px 124px 0px rgba(133,124,133,1);
    box-shadow: 15px 44px 124px 0px rgba(133,124,133,1);
}
.description {
    position: relative;
}
.fa-star {
    font-size: 36px;
}
.buttons {
    position: absolute;
    bottom: 10px;
    width: 100%;
    margin: 0 auto;
}
.stars {
    margin-bottom: 20px;
}
.play-btn {
    font-size: 24px;
    text-transform: uppercase;
    padding-left: 16px;
    padding-right: 16px;
    border-radius: 12px;
    margin-left: 12px;
}
.like, .dislike {
    font-size: 24px;
    margin-left: 12px;
}
.like {
    color: #2196f3;
}
.dislike {
    color: #ff8a80;
}
</style>
