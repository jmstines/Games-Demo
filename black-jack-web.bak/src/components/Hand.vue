<template>
  <v-container class="wrapper">
    <card
      v-for="(card, index) in cards"
      :card="card"
      :key="index"
      :style="cardStyle(index)"
    />
  </v-container>
</template>

<script lang="ts">
import Card from "@/components/Card.vue";

export default {
  name: "Hand",
  props: ["cards"],
  computed: {},
  methods: {
    cardStyle(order: number): string {
      return `
        z-index: ${order * 10};
        grid-column: ${order + 1} / span 2;
        grid-row: row 1
        `;
    }
  },
  components: { Card }
};
</script>

<style scoped>
.wrapper {
  display: grid;
  grid-gap: 0px;
  grid-template-columns: repeat(6, [col] 55px);
}
</style>
