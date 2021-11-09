<template>
  <v-container>
    <v-row align-content="start" no-gutters >
      <v-col cols="6" :offset=imageOffset(card.order) v-for="card in cards" :key="card.order">
          <v-img contain :src="card.image" height="200px" />
        </v-col>
    </v-row>
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
    }
  }
});
</script>
