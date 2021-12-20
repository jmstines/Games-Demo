<template>
  <v-container class="wrapper">
    <v-img
      v-for="(card, index) in cards"
      :key="index"
      :src="getCardImage(card.imageName)"
      :style="cardStyle(index)"
    />
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import { CardImages } from "@/model/CardImages";

interface IData {
  cardImages: Record<string, NodeRequire>;
}

export default Vue.extend({
  name: "Hand",
  props: {
    cards: {
      type: Array,
      required: true
    }
  },
  computed: {},
  data: (): IData => ({
    cardImages: new CardImages().cardNames
  }),
  methods: {
    getCardImage(name: string): NodeRequire {
      return this.cardImages[name];
    },
    cardStyle(order: number): string {
      return `
        z-index: ${order * 10};
        grid-column: ${order + 1} / span 2;
        grid-row: row 1
        `;
    }
  }
});
</script>

<style scoped>
.wrapper {
  display: grid;
  grid-gap: 0px;
  grid-template-columns: repeat(6, [col] 55px);
}
</style>
