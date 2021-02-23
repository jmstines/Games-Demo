<template>
  <v-card elevation="2" class="rounded-lg" outlined shaped tile max-width="450">
    <v-card-text>
      <p class="display-1 text--primary">{{ player.name }}</p>
    </v-card-text>
    <v-container fluid>
      <v-row dense>
        <v-col cols="6">
          <v-card style="position: relative">
            <v-img
              v-for="visibleCards in player.visibleCards"
              :style="`left:${cardMarginTop(visibleCards.order)}px`"
              style="position: absolute"
              :key="visibleCards.length"
              :src="visibleCards.image"
              contain
              height="200"
            />
          </v-card>
        </v-col>
        <v-col cols="6">
          <v-img
            :src="require('../assets/cards/card_back_blue.jpg')"
            contain
            height="200"
          />
        </v-col>
      </v-row>
    </v-container>
    <v-card-actions>
      <v-btn v-for="action in player.actions" :key="action" v-on:click="completeAction(action)">
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
  computed: { },
  data: () => ({ }),
  methods: {
    cardMarginTop: (order: number): number => {
      return order * 25;
    },
    completeAction(action: Actions): void {
      this.$emit("action", action);
    }
  },
});
</script>