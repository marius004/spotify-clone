<template>
    <Header />
    <hr>
    <div class="form-container">
        <form @submit.prevent="handleFormSubmit">
            <p class="warning">{{submitErr}}</p>
            <div class="form-group">
                <label for="email">Email</label>
                <input v-model="email" type="email" required placeholder="Enter email" class="form-control" id="email" aria-describedby="emailHelp">
                <p class="warning">{{emailErr}}</p>
            </div>
            <div class="form-group">
                <label for="username">Create Username</label>
                <input v-model="username" required type="text" placeholder="Enter username" class="form-control" id="username" aria-describedby="usernameHelp">
                <p class="warning">{{usernameErr}}</p>
            </div>
            <div class="form-group">
                <label class="text-left" for="password">Create Password</label>
                <input v-model="password" required placeholder="Enter password" type="password" class="form-control" id="password">
                <p class="warning">{{passwordErr}}</p>
            </div>
            <div class="form-group">
                <label class="text-left" for="confirm-password">Confrm Password</label>
                <input v-model="confirmPassword" required placeholder="Confirm password" type="password" class="form-control" id="confirm-password">
                <p class="warning">{{confirmPasswordErr}}</p>
            </div>
            <button type="submit" class="signup btn btn-primary login">SIGN UP</button>
        </form>
        <hr>
        <p class="create-account-title">Have an account?</p>
        <router-link to="/login">
        <button class="login btn btn-success signup">LOG IN</button>
        </router-link>
    </div>
</template>

<script>
import Header from '@/components/Header.vue';
import userService from '@/_services/user.service.js';

export default {
    name: 'Signup',
    
    components: {
        Header,
    },

    data() {
        return {
            email: '',
            username: '', 
            password: '',
            confirmPassword: '',
            emailErr: '',
            usernameErr: '', 
            passwordErr: '',
            confirmPasswordErr: '',
            submitErr: ''
        }
    }, 

    methods: {

        async handleFormSubmit() {
            if(this.validateInput()) {

                const data = await userService.signup(this.email, this.username, this.password);
 
                if(data.error) {
                    this.submitErr = 'Username or Email already taken';
                } else {
                    this.$router.push('/');
                }
            }  

            setTimeout(this.clearErrors, 5000);
        },
        
        clearErrors() {
            this.emailErr = '';
            this.usernameErr = '';
            this.passwordErr = '';
            this.confirmPasswordErr = '';
            this.submitErr = '';
        },

        validateInput() {

            let ok = true;

            if(this.password.length < 8) {
                this.passwordErr = 'The password must contain at least 8 characters';
                ok = false;
            }if(this.password !== this.confirmPassword) {
                this.confirmPasswordErr = 'The password given does not match the password you entered aboove';
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
