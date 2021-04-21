<template>
    <div class="container cards">
        <div class="row" v-for="(cardRow, rIndex) in cards" :key="rIndex">
            <div class="col lg-4 sm-12" v-for="(card, cIndex) in cardRow" :key="cIndex">
                 <SongCard
                    :title="card.title"
                    :image="require(`@/assets/img/${card.image}`)"
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
    },

    props: {
        cards: {
            type: Array, 
            required: true,
        }
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