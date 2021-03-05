import { Actions, ICard } from "@/model/";

export interface IHand {
  cards: ICard[];
  actions: Actions[];
}
