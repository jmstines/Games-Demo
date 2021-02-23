import { Actions, IPlayer } from "@/model"

export class playersTestData {
    public twoPlayerAfterDeal: IPlayer[] = [
        {

            visibleCards: [
                { order: 0, image: require("../assets/cards/3_of_spades.png") },
            ],
            name: "Dealer",
            actions: [],
        },
        {
            visibleCards: [
                { order: 0, image: require("../assets/cards/2_of_spades.png") },
                { order: 1, image: require("../assets/cards/ace_of_spades.png") },
                { order: 2, image: require("../assets/cards/4_of_spades.png") },
                { order: 3, image: require("../assets/cards/5_of_spades.png") }
            ],
            name: "Player One",
            actions: [Actions.Hit, Actions.Hold],
        }
    ]

    public twoPlayerMaxCards: IPlayer[] = [
        {

            visibleCards: [
                { order: 0, image: require("../assets/cards/3_of_spades.png") },
            ],
            name: "Dealer",
            actions: [],
        },
        {
            visibleCards: [
                { order: 0, image: require("../assets/cards/2_of_spades.png") },
                { order: 1, image: require("../assets/cards/ace_of_spades.png") },
                { order: 2, image: require("../assets/cards/4_of_spades.png") },
                { order: 3, image: require("../assets/cards/5_of_spades.png") }
            ],
            name: "Player One",
            actions: [Actions.Hit, Actions.Hold],
        }
    ]
}