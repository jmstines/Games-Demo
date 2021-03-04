import { Actions, ICard, IHand } from "@/model/";

export interface IPlayer {
    name: string;
    hands: IHand[];
  }