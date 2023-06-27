import { GameStatus, IPlayer } from "@/model/";

export interface IGame {
  players: IPlayer[];
  currentPlayerId: string;
  status: GameStatus;
  id: string;
}
