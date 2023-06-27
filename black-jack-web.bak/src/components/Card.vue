<template>
  <v-container class="wrapper">
    <v-card id="card-wrapper" />
  </v-container>
</template>

<script lang="ts">
import { ICard } from "@/model";

export default {
  name: "Card",
  props: ["card"],
  computed: {},
  methods: {
    addCardElements(card: ICard) {
      const cardElement = document.querySelectorAll(".card")[0];
      const value = card.value;
      const rank = card.rank ?? "";
      const suit = card.suit;

      const valueAsNumber = value == null ? 0 : value;
      if (isNaN(valueAsNumber)) {
        cardElement.append(this.createPip());
      } else {
        for (let i = 0; i < valueAsNumber; i++) {
          cardElement.append(this.createPip());
        }
      }

      cardElement.append(this.createCornerNumber("top", rank));
      cardElement.append(this.createCornerNumber("bottom", rank));
    },
    createCornerNumber(position: string, value: string) {
      const corner = document.createElement("div");
      corner.textContent = value;
      corner.classList.add("corner-number");
      corner.classList.add(position);
      return corner;
    },
    createPip(): HTMLDivElement {
      const pip = document.createElement("div");
      pip.classList.add("pip");
      return pip;
    }
  }
};
</script>

<style scoped>
*,
*::before,
*::after {
  box-sizing: border-box;
}

body {
  background-color: #ddd;
  display: flex;
  flex-wrap: wrap;
  gap: 0.5em;
}

.card {
  --width: 5em;
  --height: calc(var(--width) * 1.4);
  width: var(--width);
  height: var(--height);
  background-color: white;
  border: 1px solid black;
  border-radius: 0.25em;
  padding: 1em;
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  grid-template-rows: repeat(8, 1fr);
  align-items: center;
  position: relative;
}

[data-suit="heart"].card,
[data-suit="diamond"].card {
  color: red;
}

[data-suit="spade"].card,
[data-suit="club"].card {
  color: black;
}

.pip {
  grid-row-end: span 2;
  width: 100%;
  aspect-ratio: 1 / 1;
  background-repeat: no-repeat;
  background-position: center;
  background-size: cover;
}

[data-suit="heart"] .pip {
  background-image: url("@/assets/cardParts/heart.svg");
}

[data-suit="diamond"] .pip {
  background-image: url("@/assets/cardParts/diamond.svg");
}

[data-suit="spade"] .pip {
  background-image: url("@/assets/cardParts/spade.svg");
}

[data-suit="club"] .pip {
  background-image: url("@/assets/cardParts/club.svg");
}

.corner-number {
  position: absolute;
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  letter-spacing: -0.1em;
  font-size: 0.8em;
}

.corner-number.top {
  top: 0.25em;
  left: 0.25em;
}

.corner-number.bottom {
  bottom: 0.25em;
  right: 0.25em;
  transform: rotate(180deg);
}

.corner-number::after {
  line-height: 0;
  display: block;
  width: 0.5em;
}

[data-suit="heart"] .corner-number::after {
  content: url("@/assets/cardParts/heart.svg");
}

[data-suit="spade"] .corner-number::after {
  content: url("@/assets/cardParts/spade.svg");
}

[data-suit="club"] .corner-number::after {
  content: url("@/assets/cardParts/club.svg");
}

[data-suit="diamond"] .corner-number::after {
  content: url("@/assets/cardParts/diamond.svg");
}

[data-value="J"][data-suit="diamond"] .pip,
[data-value="J"][data-suit="heart"] .pip {
  background-image: url("@/assets/cardParts/jack-red.svg");
}
[data-value="J"][data-suit="spade"] .pip,
[data-value="J"][data-suit="club"] .pip {
  background-image: url("@/assets/cardParts/jack-black.svg");
}

[data-value="Q"][data-suit="diamond"] .pip,
[data-value="Q"][data-suit="heart"] .pip {
  background-image: url("@/assets/cardParts/queen-red.svg");
}
[data-value="Q"][data-suit="spade"] .pip,
[data-value="Q"][data-suit="club"] .pip {
  background-image: url("@/assets/cardParts/queen-black.svg");
}

[data-value="K"][data-suit="diamond"] .pip,
[data-value="K"][data-suit="heart"] .pip {
  background-image: url("@/assets/cardParts/king-red.svg");
}
[data-value="K"][data-suit="spade"] .pip,
[data-value="K"][data-suit="club"] .pip {
  background-image: url("@/assets/cardParts/king-black.svg");
}

[data-value="A"] .pip,
[data-value="J"] .pip,
[data-value="Q"] .pip,
[data-value="K"] .pip {
  grid-row-start: 2;
  grid-column-start: 1;
  grid-row-end: span 6;
  grid-column-end: span 3;
}

[data-value="2"] .pip:first-child {
  grid-row-start: 1;
  grid-column-start: 2;
}

[data-value="2"] .pip:nth-child(2) {
  grid-row-start: 7;
  grid-column-start: 2;
  transform: rotate(180deg);
}

[data-value="3"] .pip:first-child {
  grid-row-start: 1;
  grid-column-start: 2;
}

[data-value="3"] .pip:nth-child(2) {
  grid-row-start: 4;
  grid-column-start: 2;
}

[data-value="3"] .pip:nth-child(3) {
  grid-row-start: 7;
  grid-column-start: 2;
  transform: rotate(180deg);
}

[data-value="4"] .pip:first-child {
  grid-row-start: 1;
  grid-column-start: 1;
}

[data-value="4"] .pip:nth-child(2) {
  grid-row-start: 1;
  grid-column-start: 3;
}

[data-value="4"] .pip:nth-child(3) {
  grid-row-start: 7;
  grid-column-start: 1;
  transform: rotate(180deg);
}

[data-value="4"] .pip:nth-child(4) {
  grid-row-start: 7;
  grid-column-start: 3;
  transform: rotate(180deg);
}

[data-value="5"] .pip:first-child {
  grid-row-start: 1;
  grid-column-start: 1;
}

[data-value="5"] .pip:nth-child(2) {
  grid-row-start: 1;
  grid-column-start: 3;
}

[data-value="5"] .pip:nth-child(3) {
  grid-row-start: 7;
  grid-column-start: 1;
  transform: rotate(180deg);
}

[data-value="5"] .pip:nth-child(4) {
  grid-row-start: 7;
  grid-column-start: 3;
  transform: rotate(180deg);
}

[data-value="5"] .pip:nth-child(5) {
  grid-row-start: 4;
  grid-column-start: 2;
}

[data-value="6"] .pip:first-child {
  grid-row-start: 1;
  grid-column-start: 1;
}

[data-value="6"] .pip:nth-child(2) {
  grid-row-start: 1;
  grid-column-start: 3;
}

[data-value="6"] .pip:nth-child(3) {
  grid-row-start: 7;
  grid-column-start: 1;
  transform: rotate(180deg);
}

[data-value="6"] .pip:nth-child(4) {
  grid-row-start: 7;
  grid-column-start: 3;
  transform: rotate(180deg);
}

[data-value="6"] .pip:nth-child(5) {
  grid-row-start: 4;
  grid-column-start: 1;
}

[data-value="6"] .pip:nth-child(6) {
  grid-row-start: 4;
  grid-column-start: 3;
}

[data-value="7"] .pip:first-child {
  grid-row-start: 1;
  grid-column-start: 1;
}

[data-value="7"] .pip:nth-child(2) {
  grid-row-start: 1;
  grid-column-start: 3;
}

[data-value="7"] .pip:nth-child(3) {
  grid-row-start: 7;
  grid-column-start: 1;
  transform: rotate(180deg);
}

[data-value="7"] .pip:nth-child(4) {
  grid-row-start: 7;
  grid-column-start: 3;
  transform: rotate(180deg);
}

[data-value="7"] .pip:nth-child(5) {
  grid-row-start: 4;
  grid-column-start: 1;
}

[data-value="7"] .pip:nth-child(6) {
  grid-row-start: 4;
  grid-column-start: 3;
}

[data-value="7"] .pip:nth-child(7) {
  grid-row-start: 2;
  grid-column-start: 2;
}

[data-value="8"] .pip:first-child {
  grid-row-start: 1;
  grid-column-start: 1;
}

[data-value="8"] .pip:nth-child(2) {
  grid-row-start: 1;
  grid-column-start: 3;
}

[data-value="8"] .pip:nth-child(3) {
  grid-row-start: 7;
  grid-column-start: 1;
  transform: rotate(180deg);
}

[data-value="8"] .pip:nth-child(4) {
  grid-row-start: 7;
  grid-column-start: 3;
  transform: rotate(180deg);
}

[data-value="8"] .pip:nth-child(5) {
  grid-row-start: 4;
  grid-column-start: 1;
}

[data-value="8"] .pip:nth-child(6) {
  grid-row-start: 4;
  grid-column-start: 3;
}

[data-value="8"] .pip:nth-child(7) {
  grid-row-start: 2;
  grid-column-start: 2;
}

[data-value="8"] .pip:nth-child(8) {
  grid-row-start: 6;
  grid-column-start: 2;
  transform: rotate(180deg);
}

[data-value="9"] .pip:first-child {
  grid-row-start: 1;
  grid-column-start: 1;
}

[data-value="9"] .pip:nth-child(2) {
  grid-row-start: 3;
  grid-column-start: 1;
}

[data-value="9"] .pip:nth-child(3) {
  grid-row-start: 5;
  grid-column-start: 1;
  transform: rotate(180deg);
}

[data-value="9"] .pip:nth-child(4) {
  grid-row-start: 7;
  grid-column-start: 1;
  transform: rotate(180deg);
}

[data-value="9"] .pip:nth-child(5) {
  grid-row-start: 1;
  grid-column-start: 3;
}

[data-value="9"] .pip:nth-child(6) {
  grid-row-start: 3;
  grid-column-start: 3;
}

[data-value="9"] .pip:nth-child(7) {
  grid-row-start: 5;
  grid-column-start: 3;
  transform: rotate(180deg);
}

[data-value="9"] .pip:nth-child(8) {
  grid-row-start: 7;
  grid-column-start: 3;
  transform: rotate(180deg);
}

[data-value="9"] .pip:nth-child(9) {
  grid-row-start: 4;
  grid-column-start: 2;
}

[data-value="10"] .pip:first-child {
  grid-row-start: 1;
  grid-column-start: 1;
}

[data-value="10"] .pip:nth-child(2) {
  grid-row-start: 3;
  grid-column-start: 1;
}

[data-value="10"] .pip:nth-child(3) {
  grid-row-start: 5;
  grid-column-start: 1;
  transform: rotate(180deg);
}

[data-value="10"] .pip:nth-child(4) {
  grid-row-start: 7;
  grid-column-start: 1;
  transform: rotate(180deg);
}

[data-value="10"] .pip:nth-child(5) {
  grid-row-start: 1;
  grid-column-start: 3;
}

[data-value="10"] .pip:nth-child(6) {
  grid-row-start: 3;
  grid-column-start: 3;
}

[data-value="10"] .pip:nth-child(7) {
  grid-row-start: 5;
  grid-column-start: 3;
  transform: rotate(180deg);
}

[data-value="10"] .pip:nth-child(8) {
  grid-row-start: 7;
  grid-column-start: 3;
  transform: rotate(180deg);
}

[data-value="10"] .pip:nth-child(9) {
  grid-row-start: 2;
  grid-column-start: 2;
}

[data-value="10"] .pip:nth-child(10) {
  grid-row-start: 6;
  grid-column-start: 2;
  transform: rotate(180deg);
}
</style>
