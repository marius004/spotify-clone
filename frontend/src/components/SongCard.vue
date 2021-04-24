<template>
  <div class="card">
    <img class="card-img-top" :src="image" alt="Artist Image">
    <div class="card-body">
      <h5 class="card-title">{{title}}</h5>
      <button 
        @click="$router.push(`/artist/${artistId}`)"
        class="btn btn-primary">
        See songs
      </button>
    </div>
  </div>
</template>

<script>
export default {
    name: "SongCard",
    props: {
        title: {
            type: String, 
            required: true,
        },
        image: {
            type: String, 
            required: true,
        },
        artistId: {
          type: String, 
          required: true,
        }
    }
}
</script>

<style scoped>
.btn {
    color: white;
}
img {
    max-height: 200px;
}
* {font-weight: 900;}
</style>
