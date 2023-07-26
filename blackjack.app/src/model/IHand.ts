import { Actions, ICard } from "./index";

export interface IHand {
  cards: Array<ICard | null>;
  actions: Actions[];
  cardCount: number;
  pointValue: number;
  status: number;
  identifier: string;
}
