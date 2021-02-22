import { Actions, ICard } from "@/model/";

export interface IPlayer {
    name: string;
    visibleCards: ICard[];
    actions: Actions[];
  }