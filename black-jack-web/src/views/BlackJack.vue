<template>
  <div class="blackjack">
    <v-container fluid>
      <v-row dense justify="center">
        <v-col cols="6">
          <h1>This is the Black Jack Game page.</h1>
          <h3>Turn {{ turn }}</h3>
        </v-col>
      </v-row>
      <v-row dense justify="center">
        <v-col cols="4" v-for="player in players" :key="player.name">
          <Player :player="player" :turn="turn"> </Player>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Player from "@/components/Player.vue";
import { IPlayer, Actions } from "@/model/";

interface IData {
  turn: number;
  players: IPlayer[];
}

export default Vue.extend({
  name: "BlackJack",
  components: {
    Player
  },
  data: (): IData => ({
    turn: 1,
    players: [
      {
        visibleCards: [{order: 0, image: require("../assets/cards/3_of_spades.png")}],
        name: "Dealer",
        actions: [],
      },
      {
        visibleCards: [
          {order: 0, image: require("../assets/cards/2_of_spades.png")},
          {order: 1, image: require("../assets/cards/ace_of_spades.png")},
          {order: 2, image: require("../assets/cards/4_of_spades.png")},
          {order: 3, image: require("../assets/cards/5_of_spades.png")},
          {order: 4, image: require("../assets/cards/6_of_spades.png")}
          ],
        name: "Player One",
        actions: [Actions.Hit, Actions.Hold],
      }
    ],
  }),
});
</script>