<template>
    <div class="updateArtists">
        <h2>Update/Create Artists</h2>
        <div class="row">
            <div class="col col-lg-6 createArtist">
                <h2>Create Artist</h2>
                <CreateArtist />
            </div>
            <div class="col col-lg-6 updateArtist">
                <h2>Update Artist</h2>
                <UpdateArtist />
            </div>
        </div>
    </div>
</template>

<script>
import CreateArtist from '@/components/Admin/CreateArtist.vue';
import UpdateArtist from '@/components/Admin/UpdateArtist.vue';

export default {
    name: "UpdateArtistForm", 
    components: {
        CreateArtist,
        UpdateArtist,
    }
}
</script>

<style scoped>
* { font-weight: 900; }
.col {
    border: 1px solid black;
}
.updateArtists {
    margin-bottom: 20px;
}
.createArtist {
    background-color: #b9f6ca;
}
.updateArtist {
    background-color: #eceff1;
}
</style>