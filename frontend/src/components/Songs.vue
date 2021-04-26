<template>
    <div class="form-container">
        <table class="table table-striped table-dark">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Title</th>
                    <th scope="col">Liked by</th>
                    <th scope="col">Play</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(audio, index) in audios" 
                    :key="index" :class="{active: index == activeSong}">
                    <th scope="row">{{index + 1}}</th>
                    <td @click="$router.push(`/song/${audio.id}`)">
                        {{audio.name}}
                    </td>
                    <td>{{ likes[index] ? likes[index] : 0 }}</td>
                    <td><i @click="$emit('play', index)" class="fa fa-play"></i></td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import songService from '../_services/song.service';
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
    }, 
    methods: {
        async getLikes(id) {
            const likes = await songService.getSongLikes(id);
            console.log(likes.data);
            return likes.data;
        }
    },
    data() {
        return {
            likes: []
        }
    },
    async created() {
        if(this.audios) {
            for(let i = 0;i < this.audios.length;++i) {
                const res = await songService.getSongLikes(this.audios[i].id);
                this.likes.push(res.data);
            }
        }
    }
}
</script>

<style scoped>
* { font-weight: 900; }
.form-container {
    width: 94%;
    margin: 0 auto;
    max-height: 30vh;
    overflow: auto;
}
.fa-play {
    color: green;
}
tbody tr.active {
    color: #ef6c00;
}
@media only screen and (max-width: 800px) {
    .form-container {
        max-height: 20vh;
    }
}
</style>
