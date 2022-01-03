import { Actions, ICard } from "@/model/";

export interface IHand {
  cards: ICard[];
  actions: Actions[];
  cardCount: number;
  pointValue: number;
  status: number;
  identifier: string;
}
