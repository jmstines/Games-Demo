<template>
  <div class="blackjack">
    <v-container fluid>
      <v-row dense justify="center">
        <v-col cols="6">
          <h1 class="d-flex justify-center">
            {{ applicationTitle }}
          </h1>
          <h3 class="d-flex justify-center">Turn {{ turn }}</h3>
        </v-col>
      </v-row>
      <v-row>
        <v-col class="d-flex justify-center">
          <v-btn elevation="2" x-large v-if="gameWaiting" v-on:click="beginGame"
            >Start Game</v-btn
          >
        </v-col>
      </v-row>
      <v-row dense justify="center">
        <v-col cols="4" v-for="player in players" :key="player.name">
          <Player :player="player" :turn="turn" v-on:action="playerAction">
          </Player>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Player from "@/components/Player.vue";
import { IPlayer, Actions, GameStatus } from "@/model/";
import { BlackJackApi } from "@/services/BlackJackApi";

// TODO: Remove after function wire up
import { PlayersTestData } from "@/model/FakeData";

// TODO: Implement SSE for push event back to client
interface IData {
  turn: number;
  players: IPlayer[];
  gameStatus: GameStatus;
}

const api = new BlackJackApi();

export default Vue.extend({
  name: "BlackJack",
  components: {
    Player
  },
  data: (): IData => ({
    turn: 1,
    players: [],
    gameStatus: GameStatus.Waiting
  }),
  computed: {
    gameWaiting(): boolean {
      return this.gameStatus === GameStatus.Waiting;
    },
    applicationTitle(): string {
      const env = process.env.VUE_APP_ENV;
      return `BlackJack Game ${env}`;
    }
  },
  methods: {
    async beginGame(): Promise<void> {
      this.players = await api.BeginGame(
        "416f159f-0d56-49ea-a3ef-64e98cc13a06",
        1,
        1
      );
    },
    async playerAction(
      action: Actions,
      handId: string,
      playerId: string
    ): Promise<void> {
      
      switch (action) {
        case Actions.Hit:
          this.players = await api.Hit(playerId, handId);
          break;
        case Actions.Hold:
          this.players[1].hands[0].actions = this.players[1].hands[0].actions.filter(
            action => {
              return (
                action !== Actions.Hit &&
                action !== Actions.Hold &&
                action !== Actions.Split
              );
            }
          );
          break;
        case Actions.Split:
          this.players[1].hands[0].cards = [this.players[1].hands[0].cards[0]];
          this.players[1].hands.push(this.players[1].hands[0]);
          break;
        default:
          throw new Error("Action Type Not found.");
      }
    }
  }
});
</script>
