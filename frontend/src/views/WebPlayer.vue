<template>
    <div v-if="loaded">
        <Navbar />
        <div class="web-player-container">
            <SongShowCase 
                :cards="cards" 
                :hideText="true"
            />
            <hr>
            <div class="selector">
                <div class="container">
                    <div class="row" style="margin-bottom: 20px;">
                        <div class="col-lg-7">
                            <h3>Select Artist</h3>
                            <select multiple ref="artistSelect" required 
                                v-model="artistsSelected" 
                                name="artists" id="artists"
                                size="3">
                                <option v-for="artist in artists" 
                                    :key="artist.id"
                                    :value="artist.id">
                                    {{artist.name}}
                                </option>
                            </select>
                        </div>
                        <div class="col-lg-5" style="padding-top: 20px">
                            <button 
                                @click="filterSongs"
                                class="btn btn-success">
                                Search
                            </button>
                        </div>
                    </div>
                </div>

                <Songs 
                    :audios="songsToDisplay"
                    :activeSong="-1"
                />

            </div>
        </div>
    </div>
    <div v-else>
        <Loading 
            text="Loading Data..."
        />
    </div>
</template>

<script>
import SongShowCase from '@/components/SongShowCase.vue';
import Navbar from '@/components/Navbar.vue';
import songService from '../_services/song.service.js';
import Loading from '@/components/Loading.vue';
import artistService from '../_services/artist.service.js';
import Songs from '@/components/Songs.vue';

export default {
    name: "WebPlayer",
    
    components: {
        SongShowCase,
        Navbar, 
        Loading,
        Songs,
    },

    methods: {
        filterSongs() {
            if(this.artistsSelected.length == 0) {
                this.songsToDisplay = this.songs;
            } else if(this.artistsSelected.includes("any")) {
                this.songsToDisplay = this.songs;
            } else {
                this.songsToDisplay = this.songs.filter(sng => {
                    for(let i = 0;i < this.artistsSelected.length;++i) {
                        if(sng.artistId == this.artistsSelected[i])
                            return true;
                    }
                    return false;
                });
            }
        }
    },

    async created() {
        try {
            /// async tasks running at the same time
            const songRes = songService.getAll();
            const artistsRes = artistService.getPlainArtists();

            this.songs = (await songRes).data;
            this.songsToDisplay = this.songs;
            this.artists = (await artistsRes).data;

            this.artists.unshift({
                id: "any", 
                name: "any",
            });

            this.loaded = true;

        } catch(err) {
            console.log(err);
            this.$router.push('/');
        }
    },

    data() {
        return {
            loaded: false,
            songs: [],
            artists: [],
            artistsSelected: [],
            songsToDisplay: [],
            cards: [
                [{
                    title: 'Mark Tremonti',
                    image: 'mark-tremonti.jpg',
                    artistId: '607eb9745c0f7f6194dd7922'
                },
                {
                    title: 'Snoop Dogg',
                    image: 'snoop-dogg.jpg', 
                    artistId: '6089934f857cf0b778978b9a'
                },
                {
                    title: 'Nickleback',
                    image: 'chad-kroeger.jpg', 
                    artistId: '608994b7857cf0b778978b9b'
                }
            ],
            [{
                    title: 'Creed',
                    image: 'creed.jpg', 
                    artistId: '60899572857cf0b778978b9c'
                },
                {
                    title: 'Eminem',
                    image: 'eminem.jpg', 
                    artistId: '6081c7c0eb1113c1a5483a96'
                },
                {
                    title: 'Alter Bridge',
                    image: 'alter-bridge.jpg', 
                    artistId: '60886abfd12ddec30ea90ae9'
                }
            ]
        ]
    }
  }
}
</script>

<style scoped>
* { font-weight: 900; }
.selector {
    padding: 20px;
    margin-bottom: 20px;
}
</style>
