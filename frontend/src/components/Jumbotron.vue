<template>
<div class="jumbotron" :style="jumbotronStyling"> 
  <div class="center">
    <h1 class="title display-4" style="margin-top: 40px;">{{title}}</h1>
    <button class="btn btn-success btn-lg" href="#" role="button">{{buttonTitle}}</button>
  </div>
</div>    
</template>

<script>
export default {
    name: "Jumbotron",
    
    props: {
        title: {
            type: String, 
            required: true,
        },
        buttonTitle: {
            type: String, 
            required: true,
        },
        background: {
            type: String, 
            required: false,
        },
        color: {
            type: String, 
            required: false,
        },
    },

    computed: {
        jumbotronStyling() {
            return {
                'background-color': this.$props.background ? this.$props.background : '#ffc400',
                'color': this.$props.color ? this.$props.color : 'white',
            }
        }
    },

}
</script>

<style scoped>
@import url('https://fonts.googleapis.com/css2?family=Antonio:wght@500&display=swap');

.jumbotron {
    height: 90vh;
    font-family: 'Antonio', sans-serif;
}

.title {
    font-size: 50px;
    margin-bottom: 30px;
}

.center {
    position: absolute;
    top: 40%;
    left: 50%;
    transform: translate(-50%, -40%);
}

.btn {
    padding-left: 16px;
    padding-right: 16px;
    border-radius: 20px;
}

</style>