import { IHand } from "./index";

export interface IPlayer {
  name: string;
  id: string;
  hands: IHand[];
}
