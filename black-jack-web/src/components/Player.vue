<template>
  <v-card elevation="2" class="rounded-lg" outlined shaped tile>
    <v-card-text>
      <p class="display-1 text--primary">{{ player.name }}</p>
    </v-card-text>
    <v-container v-for="hand in player.hands" :key="hand.length">
      <hand :cards="hand.cards"></hand>
      <v-card-actions>
        <v-btn
          v-for="action in hand.actions"
          :key="action"
          v-on:click="completeAction(action)"
        >
          {{ action }}
        </v-btn>
      </v-card-actions>
    </v-container>
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
    completeAction(action: Actions): void {
      this.$emit("action", action);
    }
  }
});
</script>
