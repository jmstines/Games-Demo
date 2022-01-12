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
          <v-btn elevation="2" x-large v-if="gameWaiting" v-on:click="joinGame"
            >Find Game</v-btn
          >
          <v-btn elevation="2" x-large v-if="gameReady" v-on:click="beginGame"
            >Start Game</v-btn
          >
        </v-col>
      </v-row>
      <v-row dense justify="center">
        <v-col cols="4" v-for="player in game.players" :key="player.name">
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
import { Actions, GameStatus, IGame } from "@/model/";
import { BlackJackApi } from "@/services/BlackJackApi";

// TODO: Implement SSE for push event back to client
interface IData {
  turn: number;
  game: IGame;
}

const api = new BlackJackApi();

export default Vue.extend({
  name: "BlackJack",
  components: {
    Player
  },
  data: (): IData => ({
    turn: 1,
    game: {
      players: [],
      currentPlayerId: "",
      status: GameStatus.Waiting,
      id: ""
    }
  }),
  computed: {
    gameWaiting(): boolean {
      return this.game.status === GameStatus.Waiting;
    },
    gameReady(): boolean {
      return this.game.status === GameStatus.Ready;
    },
    applicationTitle(): string {
      const env = process.env.VUE_APP_ENV;
      return `BlackJack Game ${env}`;
    }
  },
  methods: {
    async joinGame(): Promise<void> {
      this.game = await api.JoinGame(
        "416f159f-0d56-49ea-a3ef-64e98cc13a06",
        1,
        1
      );
    },    
    async beginGame(): Promise<void> {
      this.game = await api.BeginGame(
        this.game.id,
        this.game.currentPlayerId
      );
    },
    async playerAction(
      action: Actions,
      handId: string,
      playerId: string
    ): Promise<void> {
      switch (action) {
        case Actions.Hit:
          this.game = await api.Hit(this.game.id, playerId, handId);
          break;
        case Actions.Hold:
          console.error("Action Type Not Implemented.");
          this.game = await api.Hold(this.game.id, playerId, handId);
          break;
        case Actions.Split:
          this.game = await api.Split(this.game.id, playerId, handId);
          break;
        default:
          throw new Error("Action Type Not found.");
      }
    }
  }
});
</script>
