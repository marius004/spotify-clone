<template>
    <div>
        <Navbar />
        <Jumbotron 
            title="Welcome to the admin page"
            buttonTitle="Go home"
        />
        <UpdateUsers />        
    </div>
</template>

<script>
import Navbar from '@/components/Navbar.vue';
import Jumbotron from '@/components/Jumbotron.vue';
import UpdateUsers from '@/components/Admin/UpdateUsers.vue';

export default {
    name: "AdminPanel", 
    components: {
        Navbar,
        Jumbotron,
        UpdateUsers,
    }
}
</script>

<style scoped>
</style>
