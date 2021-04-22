<template>
    <div class="loading-container">
        <div>
            <i class="fa fa-cog fa-spin"></i>
        </div>
        <h1 v-if="text">{{text}}</h1>
    </div>
</template>

<script>
export default {
    name: "Loading", 
    props: ['text']
}
</script>

<style scoped>
.loading-container {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
}
.loading-container  i {
    font-size: 100px;
    color: #009688;
}
</style>
