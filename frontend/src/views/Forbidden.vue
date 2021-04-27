<template>
    <Navbar />
    <div class="container">
        <h1 class="text-danger">Access forbidden</h1>
    </div>
</template>

<script>
import Navbar from "@/components/Navbar.vue";

export default {
    name: "Forbidden", 
    components: {
        Navbar,
    }
}
</script>

<style scoped>
.container {
    width: 50%;
    margin: 200px auto 0 auto;
}
</style>