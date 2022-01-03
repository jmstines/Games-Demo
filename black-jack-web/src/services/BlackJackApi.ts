import { IPlayer } from "@/model/";
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
  ): Promise<IPlayer[]> {
    return await this.axiosInstance
      .get("BeginGame", {
        params: {
          playerId: playerId,
          numberOfPlayers: numberOfPlayers,
          numberOfHands: handCount
        }
      })
      .then(response => {
        return (response.data.players as unknown) as IPlayer[];
      });
  }

  public async Hit(playerId: string, handId: string): Promise<IPlayer[]> {
    return await this.axiosInstance
      .get("Hit", {
        params: {
          playerId: playerId,
          handId: handId
        }
      })
      .then(response => {
        return (response.data.players as unknown) as IPlayer[];
      });
  }
}
