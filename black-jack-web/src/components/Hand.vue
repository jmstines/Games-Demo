<template>
  <v-container class="wrapper">
    <v-img
      v-for="card in cards"
      :key="card.order"
      :src="card.image"
      :class="cardClass(card.order)"
      :style="zIndex(card.order)"
    />
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  name: "Hand",
  props: {
    cards: {
      type: Array,
      required: true,
    },
  },
  computed: {},
  data: () => ({}),
  methods: {
    imageOffset: (order: number): string => {
      return order != 0 ? "-4" : "0";
    },
    imageStyle: (order: number): string => {
      const pixels = (order - 1) * 30;
      let style = `left:${pixels}px;`;
      if (order !== 0) {
        style = `${style} position:absolute; margin-top:-200px;`;
      } else {
        style = `right:${100}px;`;
      }
      return style;
    },
    zIndex(order: number): string {
      return `z-index: ${order * 10};`;
    },
    cardClass(order: number) {
      return `card${order + 1}`;
    },
  }
});
</script>

<style scoped>
.wrapper {
  display: grid;
  grid-gap: 0px;
  grid-template-columns: repeat(6, [col] 55px);
}
.card1 {
  grid-column: col 1 / span 2;
  grid-row: row 1;
}
.card2 {
  grid-column: col 2 / span 2;
  grid-row: row 1;
}
.card3 {
  grid-column: col 3 / span 2;
  grid-row: row 1;
}
.card4 {
  grid-column: col 4 / span 2;
  grid-row: row 1;
}
.card5 {
  grid-column: col 5 / span 2;
  grid-row: row 1;
}
</style>
