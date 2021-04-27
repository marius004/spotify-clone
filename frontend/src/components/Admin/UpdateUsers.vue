<template>
    <div class="table-container">
        <h2 class="title">Update Users</h2>
        <table class="table table-striped table-dark">
            <thead>
                <tr>
                    <th scope="col">#</th>
                    <th scope="col">Username</th>
                    <th scope="col">Email</th>
                    <th scope="col">Actions</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="(user, index) in users" :key="index">
                    <th scope="row">
                        {{index + 1}}
                    </th>
                    <td>
                        {{user.username}}
                    </td>
                    <td>
                        {{user.email}}
                    </td>
                    <td>
                        <button @click="deleteHandler(user)" type="button" class="btn btn-danger">Delete</button>
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import userService from '../../_services/user.service';

export default {
    name: "UpdateUsers", 

    data() {
        return {
            users: [],
        }
    },

    async created() {
        try {
            const res = await userService.getAllUsers();
            this.users = res.data;
        }catch(err) {
            console.error(err);
        }
    }, 

    methods: {
        async deleteHandler(user) {
            if(confirm(`Do you really wanna delete the user ${user.username}`)) {
                if(!user.isAdmin) {
                    this.users = this.users.filter(usr => usr.id !== user.id);
                    try {
                        userService.deleteUser(user.id);
                    }catch(err) {
                        console.log(err);
                    }
                } else {
                    alert("You cannot delete an administrator!");
                }
            }
        }
    }
}
</script>

<style scoped>
* {font-weight: bold;}
.table-container {
    width: 96%;
    margin: 0 auto;
}
</style>
