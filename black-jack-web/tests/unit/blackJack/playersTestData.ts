import { Actions, IPlayer } from "@/model";

export class PlayersTestData {
  public twoPlayerAfterDeal: IPlayer[] = [
    {
      name: "Dealer",
      playerIdentifier: "654987321",
      hands: [
        {
          cards: [
            {
              imageName: require("../assets/cards/card_back_blue.jpg"),
              value: null
            },
            {
              imageName: require("../assets/cards/3_of_spades.png"),
              value: 3
            }
          ],
          actions: [],
          cardCount: 2,
          pointValue: 3,
          status: 3
        }
      ]
    },
    {
      name: "Player One",
      playerIdentifier: "978756321",
      hands: [
        {
          cards: [
            {
              imageName: require("../assets/cards/card_back_blue.jpg"),
              value: null
            },
            {
              imageName: require("../assets/cards/ace_of_spades.png"),
              value: 11
            }
          ],
          actions: [Actions.Hit, Actions.Hold, Actions.Split],
          cardCount: 2,
          pointValue: 3,
          status: 3
        }
      ]
    }
  ];

  public twoPlayerMaxCards: IPlayer[] = [
    {
      name: "Dealer",
      playerIdentifier: "1326556798",
      hands: [
        {
          cards: [
            {
              imageName: require("../assets/cards/3_of_spades.png"),
              value: 3
            }
          ],
          actions: [],
          cardCount: 2,
          pointValue: 3,
          status: 3
        }
      ]
    },
    {
      name: "Player One",
      playerIdentifier: "951623487",
      hands: [
        {
          cards: [
            {
              imageName: require("../assets/cards/2_of_spades.png"),
              value: 2
            },
            {
              imageName: require("../assets/cards/ace_of_spades.png"),
              value: 11
            },
            {
              imageName: require("../assets/cards/4_of_spades.png"),
              value: 4
            },
            {
              imageName: require("../assets/cards/5_of_spades.png"),
              value: 5
            }
          ],
          actions: [Actions.Hit, Actions.Hold],
          cardCount: 2,
          pointValue: 3,
          status: 3
        }
      ]
    }
  ];
}
