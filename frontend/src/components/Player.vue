<template>
    <div class="player">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 control">
                    <p class="title">{{title}}</p>
                    <div class="d-flex justify-content-center" style="margin-bottom: 12px">
                        <i @click="$emit('prev')" class="prev fa fa-step-backward"></i>
                        <i @click="playSong" v-if="!isPlaying" class="play fa fa-play-circle"></i>
                        <i @click="stopSong" v-else class="pause fa fa-pause-circle"></i>
                        <i @click="$emit('next')" class="fa fa-step-forward"></i>
                    </div>
                    <div class="progress">
                        <div class="progress-bar" role="progressbar" 
                            :style="progressStyling" 
                            aria-valuenow="0" aria-valuemin="0" 
                            :aria-valuemax="duration">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="display: none;" class="loder">
            <audio ref="loader" controls>
                <source :src="'data:audio/wav;base64,' + audio" type="audio/base64">
                <source :src="'data:audio/wav;base64,' + audio" type="audio/mpeg">
                Your browser does not support the audio tag.
            </audio>
        </div>
    </div>
</template>

<script>
import timeFormatter from '../_util/timeFormatter.js';

export default {
    name: "Player",
    
    props: {
        audio: {
            type: String, 
            required: true,
        }, 
        title: {
            type: String, 
            required: true,
        }
    },
    
    watch: {
        audio: function() {
            this.isPlaying = false;
            this.removeEventListener();
            this.seconds = 0;
            this.playSong();
        }
    },

    data() {
        return {
            isPlaying: false,
            duration: 0,
            secondsEv: undefined, 
            seconds: 0,
        }
    }, 

    methods: {
        playSong() {
            this.isPlaying = true;
            this.$refs.loader.play();
            this.duration = this.$refs.loader.duration;
            this.addPlayerEventListeners();
        }, 
        stopSong() {
            this.isPlaying = false;
            this.$refs.loader.pause();
            this.seconds = this.$refs.loader.currentTime;
            this.removeEventListener();
        }, 
        addPlayerEventListeners() {
            this.secondsEv = setInterval(() => {
                 this.seconds = this.$refs.loader.currentTime;
            }, 100);
        }, 
        removeEventListener() {
            if(this.secondsEv)
                clearInterval(this.secondsEv);
        }
    },

    computed: {
        progressStyling() {
            if(this.seconds && this.duration)
                return { width: `${this.seconds / this.duration * 100}%` }
            return { width: 0 };
        }, 
        songDuration() {
            if(this.duration)
                return timeFormatter.formatSeconds(this.duration); 
            return 0;
        }
    }
}
</script>

<style scoped>
.player {
    text-align: center;
    color: white;
    position: absolute;
    width: 100%;
    bottom: 0;
    border-top: 2px solid #78909c;
    background-color: #263238;
    padding: 12px 16px;
    -webkit-box-shadow: 5px -4px 5px 0px rgba(0,0,0,0.39);
    -moz-box-shadow: 5px -4px 5px 0px rgba(0,0,0,0.39);
    box-shadow: 5px -4px 5px 0px rgba(0,0,0,0.39);
}
.player i {
    font-size: 22px;
    margin-right: 50px;
    color: #e0e0e0;
    transition: color 0.5s;
}
.player i:hover {
    color: white;
}
.control {
    margin: 0 auto;
}
.title {
    transform: translate(-20px, 0);
    color: white;
}
</style>
