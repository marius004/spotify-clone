<template>
    <div>
        <Navbar />
        <Jumbotron 
            title="Welcome to the admin page"
            buttonTitle="Go home"
            @btnClick="jumbotronClickHandler"
        />
        <div class="updateContainer">
            <UpdateUsers /> 
            <DeleteSongs />
            <UpdateSongForm />     
            <UpdateArtistForm />
        </div>
    </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue';
import Jumbotron from '@/components/Jumbotron.vue';
import UpdateUsers from '@/components/Admin/UpdateUsers.vue';
import UpdateSongForm from '@/components/Admin/UpdateSongForm.vue';
import UpdateArtistForm from '@/components/Admin/UpdateArtistForm.vue';
import DeleteSongs from '@/components/Admin/DeleteSongs.vue';

export default {
    name: "AdminPanel", 
    components: {
        Navbar,
        Jumbotron,
        UpdateUsers,
        UpdateSongForm,
        UpdateArtistForm,
        DeleteSongs,
    }, 
    methods: {
        jumbotronClickHandler() {
            this.$router.push('/');
        }
    }
}
</script>

<style scoped>
.updateContainer {
   width: 96%;
   margin: 0 auto; 
}
</style>
