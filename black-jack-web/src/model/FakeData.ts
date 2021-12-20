import { IPlayer, Actions } from "@/model/";

interface ICard {
  imageName: NodeRequire;
  value: number;
}

export class PlayersTestData {
  public cardsPushList: ICard[] = [
    {
      imageName: require("../assets/cards/two_of_spades.png"),
      value: 2
    },
    {
      imageName: require("../assets/cards/ace_of_spades.png"),
      value: 11
    },
    {
      imageName: require("../assets/cards/four_of_spades.png"),
      value: 4
    },
    {
      imageName: require("../assets/cards/five_of_spades.png"),
      value: 5
    }
  ];
  public twoPlayerAfterDeal: IPlayer[] = [
    {
      name: "Dealer",
      playerIdentifier: "123456789",
      hands: [
        {
          cards: [
            {
              imageName: require("../assets/cards/card_back_blue.jpg"),
              value: null
            },
            {
              imageName: require("../assets/cards/three_of_spades.png"),
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
      playerIdentifier: "987654321",
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
      playerIdentifier: "45678913",
      hands: [
        {
          cards: [
            {
              imageName: require("../assets/cards/three_of_spades.png"),
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
      playerIdentifier: "123654987",
      hands: [
        {
          cards: [
            {
              imageName: require("../assets/cards/two_of_spades.png"),
              value: 2
            },
            {
              imageName: require("../assets/cards/ace_of_spades.png"),
              value: 11
            },
            {
              imageName: require("../assets/cards/four_of_spades.png"),
              value: 4
            },
            {
              imageName: require("../assets/cards/five_of_spades.png"),
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
