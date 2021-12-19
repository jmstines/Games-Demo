<template>
  <v-container class="wrapper">
    <v-img
      v-for="card in cards"
      :key="card.order"
      :src="card.image"
      :style="cardStyle(card.order)"
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
      required: true
    }
  },
  computed: {},
  data: () => ({}),
  methods: {
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
