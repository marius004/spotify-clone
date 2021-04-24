<template>
    <div class="player">
        <div class="container">
            <div class="row">
                <div class="col-lg-12 control">
                    <p class="title">{{title}}</p>
                    <div class="d-flex justify-content-center" style="margin-bottom: 12px">
                        <i @click="prevSong" class="prev fa fa-step-backward"></i>
                        <i @click="playSong" v-if="!isPlaying" class="play fa fa-play-circle"></i>
                        <i @click="stopSong" v-else class="pause fa fa-pause-circle"></i>
                        <i @click="nextSong" class="fa fa-step-forward"></i>
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

        <audio ref="loader">
            <source ref="audioSrc" :src="'data:audio/wav;base64,' + currentSong" type="audio/mpeg">
            Your browser does not support the audio tag.
        </audio>
    </div>
</template>

<script>
import timeFormatter from '../_util/timeFormatter.js';

export default {
    name: "Player",
    
    props: {
        currentSong: {
            type: String, 
            required: true,
        }, 
        title: {
            type: String, 
            required: true,
        }
    },
    
    watch: {
        currentSong: function() {
            this.$refs.loader.load();
            this.isPlaying = false;
            this.removeEventListener();
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
            this.seconds = 0;
            this.duration = this.$refs.loader.duration;
            this.addPlayerEventListeners();
            this.isPlaying = true;
            this.$refs.loader.play();
        },  
        stopSong() {
            this.$refs.loader.pause();
            this.seconds = this.$refs.loader.currentTime;
            this.removeEventListener();
            this.isPlaying = false;
        }, 
        addPlayerEventListeners() {
            this.secondsEv = setInterval(() => {
                 this.seconds = this.$refs.loader.currentTime;
                 this.duration = this.$refs.loader.duration;
            }, 100);
        }, 
        removeEventListener() {
            if(this.secondsEv)
                clearInterval(this.secondsEv);
        }, 
        nextSong() {
            this.seconds = 0;
            this.duration = 0;
            this.$emit('next');
        },
        prevSong() {
            this.seconds = 0;
            this.duration = 0;
            this.$emit('prev'); 
        }
    },

    computed: {
        progressStyling() {
            if(this.seconds && this.duration) {
                if(this.duration !== 0)
                    return { width: `${this.seconds / this.duration * 100}%` }
                return { width: 0 };
            }

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
