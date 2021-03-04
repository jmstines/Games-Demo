<template>
  <div>
    <v-img
      v-for="card in cards"
      :style="imageStyle(card.order)"
      :key="card.order"
      :src="card.image"
      contain
      height="200"
    />
  </div>
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
    imageStyle: (order: number): string => {
      const pixels = (order - 1) * 30;
      let style = `left:${pixels}px;`
      if (order !== 0) {
        style = `${style} position:absolute; margin-top:-200px;`;
      }
      else {
        style = `right:${100}px;`
      }
      return style;
    },
  },
});
</script>