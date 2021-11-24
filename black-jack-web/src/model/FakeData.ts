import { IPlayer, Actions } from "@/model/";

interface ICardImage {
  image: NodeRequire;
}

export class PlayersTestData {
  public cardsPushList: ICardImage[] = [
    { image: require("../assets/cards/2_of_spades.png") },
    { image: require("../assets/cards/ace_of_spades.png") },
    { image: require("../assets/cards/4_of_spades.png") },
    { image: require("../assets/cards/5_of_spades.png") }
  ];
  public twoPlayerAfterDeal: IPlayer[] = [
    {
      name: "Dealer",
      hands: [
        {
          cards: [
            { 
              order: 0,
              image: require("../assets/cards/card_back_blue.jpg")
            },
            { 
              order: 1,
              image: require("../assets/cards/3_of_spades.png")
            }
          ],
          actions: []
        }
      ]
    },
    {
      name: "Player One",
      hands: [
        {
          cards: [
            { order: 0, image: require("../assets/cards/card_back_blue.jpg") },
            { order: 1, image: require("../assets/cards/ace_of_spades.png") }
          ],
          actions: [Actions.Hit, Actions.Hold, Actions.Split]
        }
      ]
    }
  ];

  public twoPlayerMaxCards: IPlayer[] = [
    {
      name: "Dealer",
      hands: [
        {
          cards: [
            { order: 0, image: require("../assets/cards/3_of_spades.png") }
          ],
          actions: []
        }
      ]
    },
    {
      name: "Player One",
      hands: [
        {
          cards: [
            { order: 0, image: require("../assets/cards/2_of_spades.png") },
            { order: 1, image: require("../assets/cards/ace_of_spades.png") },
            { order: 2, image: require("../assets/cards/4_of_spades.png") },
            { order: 3, image: require("../assets/cards/5_of_spades.png") }
          ],
          actions: [Actions.Hit, Actions.Hold]
        }
      ]
    }
  ];
}
