<template>
    <Header />
    <hr>
    <div class="form-container">
        <form @submit.prevent="handleFormSubmit">
            <p class="warning">{{submitErr}}</p>
            <div class="form-group">
                <label for="username">Username</label>
                <input v-model="username" required type="text" placeholder="Enter username" class="form-control" id="username" aria-describedby="usernameHelp">
                <p class="warning">{{usernameErr}}</p>
            </div>
            <div class="form-group">
                <label class="text-left" for="exampleInputPassword1">Password</label>
                <input v-model="password" required placeholder="Enter password" type="password" class="form-control" id="exampleInputPassword1">
                <p class="warning">{{passwordErr}}</p>
            </div>
            <button 
                type="submit" 
                class="btn btn-primary login"
            >
                LOG IN
            </button>
        </form>
        <hr>
        <p class="create-account-title">Don't have an account?</p>
        <router-link to="/signup">
            <button class="btn btn-success signup">SIGN UP FOR SPOTIFY</button>
        </router-link>
    </div>
</template>

<script>
import Header from '@/components/Header.vue';
import userService from '@/_services/user.service.js';

export default {
    name: "Login",
    
    components: {
        Header,
    },

    data() {
        return {
            username: '', 
            password: '',
            usernameErr: '', 
            passwordErr: '',
            submitErr: ''
        }
    }, 

    methods: {

        clearErrors() {
            this.usernameErr = '';
            this.passwordErr = '';
            this.submitErr = '';
        },

        async handleFormSubmit() {
            if(this.validateInput()) {
                try {
                    await userService.login(this.username, this.password);
                    this.$router.push('/');
                } catch {
                    this.submitErr = 'Username or Password invalid';    
                }
            }
            
            setTimeout(this.clearErrors, 5000);
        }, 

        validateInput() {
            let ok = true;

            if(this.username.trim() === '') {
                this.usernameErr = 'Username cannot be empty string';
                ok = false;
            }

            if(this.password.trim() === '') {
                this.passwordErr = 'Passsword cannot be empty string';
                ok = false;
            }

            return ok;
        }

    }
}
</script>

<style scoped>
@import url('../assets/css/userAuth.css');
* {font-weight: 900;}
</style>
