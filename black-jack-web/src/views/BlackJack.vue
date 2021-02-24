<template>
  <div class="blackjack">
    <v-container fluid>
      <v-row dense justify="center">
        <v-col cols="6">
          <h1 class="d-flex justify-center">
            This is the Black Jack Game page.
          </h1>
          <h4 class="d-flex justify-center">Turn {{ turn }}</h4>
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
import { IPlayer, Actions, GameStatus, ICard } from "@/model/";

interface ICardImage {
  image: NodeRequire;
}

export class PlayersTestData {
  public cardsPushList: ICardImage[] = [
    { image: require("../assets/cards/2_of_spades.png") },
    { image: require("../assets/cards/ace_of_spades.png") },
    { image: require("../assets/cards/4_of_spades.png") },
    { image: require("../assets/cards/5_of_spades.png") },
  ];
  public twoPlayerAfterDeal: IPlayer[] = [
    {
      visibleCards: [
        { order: 0, image: require("../assets/cards/3_of_spades.png") },
      ],
      name: "Dealer",
      actions: [],
      cardCount: 2
    },
    {
      visibleCards: [
        { order: 0, image: require("../assets/cards/2_of_spades.png") },
      ],
      name: "Player One",
      actions: [Actions.Hit, Actions.Hold],
      cardCount: 2
    },
  ];

  public twoPlayerMaxCards: IPlayer[] = [
    {
      visibleCards: [
        { order: 0, image: require("../assets/cards/3_of_spades.png") },
      ],
      name: "Dealer",
      actions: [],
      cardCount: 2
    },
    {
      visibleCards: [
        { order: 0, image: require("../assets/cards/2_of_spades.png") },
        { order: 1, image: require("../assets/cards/ace_of_spades.png") },
        { order: 2, image: require("../assets/cards/4_of_spades.png") },
        { order: 3, image: require("../assets/cards/5_of_spades.png") },
      ],
      name: "Player One",
      actions: [Actions.Hit, Actions.Hold],
      cardCount: 5
    },
  ];
}

interface IData {
  turn: number;
  players: IPlayer[];
  gameStatus: GameStatus;
  cardBack: NodeRequire;
}

export default Vue.extend({
  name: "BlackJack",
  components: {
    Player,
  },
  data: (): IData => ({
    turn: 1,
    players: [],
    gameStatus: GameStatus.Waiting,
    cardBack: require('../assets/cards/card_back_blue.jpg')
  }),
  computed: {
    gameWaiting(): boolean {
      return this.gameStatus === GameStatus.Waiting;
    },
  },
  methods: {
    addHiddenCard(cards: ICard[]) {
      const cardBack: ICard = { 
        order: 0,
        image: require('../assets/cards/card_back_blue.jpg')
      }
      cards.forEach(card => card.order++);
      cards.unshift(cardBack);
    },
    beginGame() {
      this.players = new PlayersTestData().twoPlayerAfterDeal;
      this.players.forEach(player => {{
        this.addHiddenCard(player.visibleCards);
      }})
      this.gameStatus = GameStatus.InProgress;
    },
    playerAction(action: Actions): void {
      const image = new PlayersTestData().cardsPushList.slice()[0].image;
      const cardCount = this.players[1].visibleCards.length;

      switch (action) {
        case Actions.Hit:
          this.players[1].cardCount++;
          this.players[1].visibleCards.push({ order: cardCount, image: image });
          break;
        case Actions.Hold:
          this.players[1].actions = this.players[1].actions.filter(action => {
            return action !== Actions.Hit &&
            action !== Actions.Hold
          })
          break;
        default:
          throw new Error("Action Type Not found.");
      }
    },
  },
});
</script>
