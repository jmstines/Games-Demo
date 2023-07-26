import { GameStatus, IPlayer } from "./index";

export interface IGame {
  players: IPlayer[];
  currentPlayerId: string;
  status: GameStatus;
  id: string;
}
