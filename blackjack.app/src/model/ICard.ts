import { CardRank } from "./CardRank";
import { CardSuit } from "./CardSuit";
import { CardValue } from "./CardValue";

export interface ICard {
  suit: CardSuit | null;
  rank: CardRank | null;
  value: CardValue | null;
}
