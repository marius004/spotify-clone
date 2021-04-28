<template>
 <h2 class="title">Update/Create Songs</h2>
    <div class="row updateSongs">
        <div class="col col-lg-6 create-col" style="background-color: #bbdefb">
            <h2>Create Song</h2>
            <CreateSong />
        </div>
        <div class="col col-lg-6">
            <div class="form-container">
                <h2>Update Song</h2>
                <UpdateSong />
            </div>
        </div>
    </div>
</template>

<script>
import CreateSong from "./CreateSong";
import UpdateSong from "./UpdateSong";

export default {
    name: "UpdateSongs", 
    
    components: {
        CreateSong,
        UpdateSong,
    }
}
</script>

<style scoped>
* { font-weight: 900; }
.updateSongs {
    margin-bottom: 20px;
}
.form-container {
    width: 96%;
    margin: 0 auto;
    padding: 12px 16px;
}
.col {
    border: 1px solid black;
}
</style>