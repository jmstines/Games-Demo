import { IHand } from "@/model/";

export interface IPlayer {
  name: string;
  playerIdentifier: string;
  hands: IHand[];
}
