<template>
    <div class="form-container">
        <table class="table table-striped table-dark">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Title</th>
                    <th scope="col">Liked by</th>
                    <th scope="col">Disliked by</th>
                    <th scope="col">Play</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(audio, index) in audios" 
                    :key="index" :class="{active: index == activeSong}">
                    <th scope="row">{{index + 1}}</th>
                    <td>{{audio.name}}</td>
                    <td>{{audio.likedBy ? audio.likedBy : 0 }}</td>
                    <td>{{audio.dislikedBy ? audio.dislikedBy : 0}}</td>
                    <td><i @click="$emit('play', index)" class="fa fa-play"></i></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
export default {
    name: "Songs", 
    props: {
        audios: {
            type: Array, 
            required: true,
        }, 
        activeSong: {
            type: Number, 
            required: true,
        }
    }
}
</script>

<style>
* { font-weight: 900; }
.form-container {
    width: 94%;
    margin: 0 auto;
    max-height: 35vh;
    overflow: auto;
}
.fa-play {
    color: green;
}
tbody tr.active {
    color: #ef6c00;
}
</style>
