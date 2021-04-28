<template>
    <h2 class="title">Delete Songs</h2>
    <div style="max-height: 40vh; overflow: auto;margin-bottom: 20px;">
        <table
            class="delete-songs table table-striped table-dark">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Name</th>
                    <th scope="col">Artist</th>
                    <th scope="col">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(song, index) in songs" :key="index">
                    <th scope="row">
                        {{index + 1}}
                    </th>
                    <td>
                        {{song.name}}
                    </td>
                    <td>
                        {{song.artistName}}
                    </td>
                    <td>
                        <button @click="deleteHandler(song)" type="button" class="btn btn-danger">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import songService from '../../_services/song.service';

export default {
    name: "DeleteSongs", 
    
    data() {
        return {
            songs: []
        }
    },

    methods: {
        async deleteHandler(song) {
            try {
                await songService.deleteById(song.id);
                this.songs = this.songs.filter(sng => sng.id !== song.id);
            }catch(err) {
                console.log(err);
            }
        }
    },

    async created() {
        try {
            const res = await songService.getAll();
            this.songs = res.data;
        }catch(err) {
            console.log(err);
        }
    },
}
</script>

<style scoped>
* { font-weight: 900; }
</style>