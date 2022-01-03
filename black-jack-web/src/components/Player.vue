<template>
  <v-card elevation="2" height="350" class="rounded-lg" outlined shaped tile>
    <v-card-text>
      <p class="display-1 text--primary">{{ player.name }}</p>
    </v-card-text>
    <div v-for="hand in player.hands" :key="hand.length">
      <hand :cards="hand.cards"></hand>
      <v-card-actions>
        <v-btn
          v-for="(action, index) in hand.actions"
          :key="index"
          v-on:click="completeAction(action, hand.identifier, player.id)"
        >
          {{ getAction(action) }}
        </v-btn>
      </v-card-actions>
    </div>
  </v-card>
</template>

<script lang="ts">
import { Actions } from "@/model/";
import Vue from "vue";
import Hand from "@/components/Hand.vue";

export default Vue.extend({
  name: "Player",
  components: {
    Hand: Hand
  },
  props: {
    player: {
      type: Object,
      required: true
    }
  },
  computed: {},
  data: () => ({}),
  methods: {
    completeAction(action: Actions, handId: string, playerId: string): void {
      this.$emit("action", action, handId, playerId);
    },
    getAction(value: number): string {
      return Actions[value];
    }
  }
});
</script>

<style scoped></style>
