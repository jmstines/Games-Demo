import { Actions, IPlayer } from "@/model";

export class PlayersTestData {
  public twoPlayerAfterDeal: IPlayer[] = [
    {
      name: "Dealer",
      id: "654987321",
      hands: [
        {
          identifier: "",
          cards: [
            {
              suit: null,
              rank: null,
              value: null
            },
            {
              suit: "spades",
              rank: "three",
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
      id: "978756321",
      hands: [
        {
          identifier: "",
          cards: [
            {
              suit: null,
              rank: null,
              value: null
            },
            {
              suit: "spades",
              rank: "ace",
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
      id: "1326556798",
      hands: [
        {
          identifier: "",
          cards: [
            {
              suit: "spades",
              rank: "three",
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
      id: "951623487",
      hands: [
        {
          identifier: "",
          cards: [
            {
              suit: "spades",
              rank: "two",
              value: 2
            },
            {
              suit: "spades",
              rank: "ace",
              value: 11
            },
            {
              suit: "spades",
              rank: "four",
              value: 4
            },
            {
              suit: "spades",
              rank: "five",
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
