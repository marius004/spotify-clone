<template>
    <div class="container cards">
        <div class="row">
            <div class="col lg-4 sm-12">
                <SongCard
                    title="Mark Tremonti"
                    :image="require('../assets/img/mark-tremonti.jpg')"
                />
            </div>
            <div class="col lg-4 sm-12">
                <SongCard 
                    title="Snoop Dogg"
                    :image="require('../assets/img/snoop-dogg.jpg')"
                />
            </div>
            <div class="col lg-4 sm-12">
                <SongCard 
                    title="Chad Kroeger"
                    :image="require('../assets/img/chad-kroeger.jpg')"
                />
            </div>
        </div>
         <div class="row">
            <div class="col lg-4">
                <SongCard
                    title="Creed"
                    :image="require('../assets/img/creed.jpg')"
                />
            </div>
            <div class="col lg-4">
                <SongCard 
                    title="Eminem"
                    :image="require('../assets/img/eminem.jpg')"
                />
            </div>
            <div class="col lg-4 sm-12">
                <SongCard 
                    title="Alter Bridge"
                    :image="require('../assets/img/alter-bridge.jpg')"
                />
            </div>
        </div>
    </div>
</template>

<script>
import SongCard from '@/components/SongCard.vue';

export default {
    name: "SongCards",
    components: {
        SongCard,
    }
}
</script>

<style scoped>
.cards {
    margin-bottom: 30px;
}
.card {
    margin-bottom: 20px;
}
</style>