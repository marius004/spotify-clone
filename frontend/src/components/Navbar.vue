<template>
<nav id="nav" class="navbar fixed-top navbar-expand-lg navbar-dark" style="background-color: black;color: white;">
  <div class="container">
    
    <router-link to="/">
        <div class="navbar-brand">
            <i class="fa fa-spotify"></i>
            Spotify
        </div>
    </router-link>

    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
    </button>

    <div class="collapse navbar-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav mr-auto"></ul>

        <div class="form-inline my-2 my-lg-0">
            <ul class="navbar-nav mr-auto">
                <li class="nav-item">
                   <a class="nav-link" href="">Support</a>
                </li>
                <li class="nav-item dropdown" style="opacity: 1;">
                    <a class="nav-link dropdown-toggle" href="#" id="navbarDropdown" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                        <i class="fa fa-user-circle"></i> Profile
                    </a>
                    <div class="dropdown-menu" aria-labelledby="navbarDropdown">
                        <router-link to="/login">
                            <a class="dropdown-item" href="#">Log in</a>
                        </router-link>
                        <router-link to="/signup">
                             <a class="dropdown-item" href="#">Sign up</a>
                        </router-link>
                    </div>
                </li>
            </ul>
        </div>
        
    </div>
 </div>
</nav>
</template>

<script>
export default {
    name: "Navbar",
}
</script>

<style scoped>
#nav {
    opacity: 0.85;
    z-index: 1;
    border-bottom: 1px solid black;
}
#nav .nav-link {
    opacity: 1;
    color: white;
    font-weight: 900;
}

#nav .dropdown-item {
    color: black;
    font-weight: 900;
}

.navbar-brand {
    font-size: 24px;
}

.navbar-brand i {
    font-size: 30px;
    margin-right: 2px;
    font-weight: 100;
}
a {
    font-size: 24px;
    text-decoration: none;
}
* {font-weight: 900;}
li {
    margin-right: 30px;
}
li:last-child {
    margin-right: 0;
}
</style>