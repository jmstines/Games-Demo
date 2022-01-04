import { IGame } from "@/model/";
import axios from "axios";

export class BlackJackApi {
  private blackJackApi = process.env.VUE_APP_BLACKJACK_API;
  private axiosInstance = axios.create({
    baseURL: this.blackJackApi,
    withCredentials: false
  });

  public async BeginGame(
    playerId: string,
    numberOfPlayers = 1,
    handCount = 1
  ): Promise<IGame> {
    return await this.axiosInstance
      .get("BeginGame", {
        params: {
          playerId: playerId,
          numberOfPlayers: numberOfPlayers,
          numberOfHands: handCount
        }
      })
      .then(response => {
        return (response.data as unknown) as IGame;
      });
  }

  public async Hit(
    gameid: string,
    playerId: string,
    handId: string
  ): Promise<IGame> {
    return await this.axiosInstance
      .get("Hit", {
        params: {
          gameId: gameid,
          playerId: playerId,
          handId: handId
        }
      })
      .then(response => {
        return (response.data as unknown) as IGame;
      });
  }
}
