import { IHand } from "@/model/";

export interface IPlayer {
  name: string;
  id: string;
  hands: IHand[];
}
