<template>
    <div class="looking-for-music">
        <h1>Looking for music?</h1>
        <h4>Start listening to the best new releases.</h4>
        <button class="launch-web-player-btn btn text-center">Launch Web Player</button>
    </div>
    <SongCards :cards="cards" />
</template>

<script>
import SongCards from '@/components/SongCards.vue';

export default {
    
    name: "SongShowCase",

    components: {
        SongCards,
    }, 

    props: {
        cards: {
            type: Array, 
            required: true,
        }
    }
}
</script>

<style scoped>
.looking-for-music {
    width: 50%;
    margin: 0 auto;
    text-align: center;
    margin-bottom: 50px;
}
.looking-for-music h1 {
    font-weight: 900;
}
.looking-for-music h4 {
    color: #757575;
}
.launch-web-player-btn {
    color: black;
    background-color: white;
    border: 1px solid black;
    border-radius: 16px;
    font-weight: bold;
    font-size: 18px;
    transition: background-color .6s;
    font-weight: 900;
}

.launch-web-player-btn:hover {
    background-color: #e0e0e0;
}
</style>
