<template>
  <v-card elevation="2" class="rounded-lg" outlined shaped tile max-width="450">
    <v-card-text>
      <p class="display-1 text--primary">{{ player.name }}</p>
    </v-card-text>
      <v-img
        v-for="visibleCard in player.visibleCards"
        :style="cardStyle(visibleCard.order)"
        :key="visibleCard.order"
        :src="visibleCard.image"
        contain
        height="200"
      />
    <v-card-actions>
      <v-btn
        v-for="action in player.actions"
        :key="action"
        v-on:click="completeAction(action)"
      >
        {{ action }}
      </v-btn>
    </v-card-actions>
  </v-card>
</template> 

<script lang="ts">
import { Actions } from "@/model";
import Vue from "vue";

export default Vue.extend({
  name: "Player",
  props: {
    player: {
      type: Object,
      required: true,
    },
  },
  computed: {},
  data: () => ({}),
  methods: {
    cardStyle: (order: number): string => {
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
    completeAction(action: Actions): void {
      this.$emit("action", action);
    },
  },
});
</script>