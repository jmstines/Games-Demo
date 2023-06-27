import { Actions, ICard } from "@/model/";

export interface IHand {
  cards: Array<ICard | null>;
  actions: Actions[];
  cardCount: number;
  pointValue: number;
  status: number;
  identifier: string;
}
