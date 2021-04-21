<template>
    <div class="container text-center header">
        <router-link to='/'>
            <i class="fa fa-spotify"></i>
            <h1 class="header-title" style="display: inline;">Spotify</h1>
        </router-link>
    </div>
</template>

<script>
export default {
    name: 'Header',
}
</script>

<style scoped>
.header {
    padding-top: 12px;
    font-weight: 900;
}
.header-title {
    text-decoration: none;
    color: black;
}
i {
    font-size: 40px;
    margin-right: 2px;
    color: black;
}
a {
    text-decoration: none;
    color: black;
}
</style>