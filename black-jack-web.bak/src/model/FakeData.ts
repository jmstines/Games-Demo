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
      id: "123456789",
      hands: [
        {
          identifier: "",
          cards: [
            null,
            {
              rank: "three",
              suit: "spades",
              value: null
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
      id: "987654321",
      hands: [
        {
          identifier: "",
          cards: [
            null,
            {
              rank: "ace",
              suit: "spades",
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
      id: "45678913",
      hands: [
        {
          identifier: "",
          cards: [
            {
              rank: "three",
              suit: "spades",
              value: null
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
      id: "123654987",
      hands: [
        {
          identifier: "",
          cards: [
            {
              rank: "two",
              suit: "spades",
              value: null
            },
            {
              rank: "ace",
              suit: "spades",
              value: 1
            },
            {
              rank: "four",
              suit: "spades",
              value: 4
            },
            {
              rank: "five",
              suit: "spades",
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
