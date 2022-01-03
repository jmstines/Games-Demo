using Entities.Enums;
using Entities.ResponceDto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
	// TODO - Add Tests for the Mapper Class
	public static class MapperBlackJackGameDto
	{
		private const string CardBackName = "card_back_blue";
		public static BlackJackGameDto ToDto(this BlackJackGame game, string playerId)
		{
			_ = game ?? throw new ArgumentNullException(nameof(game));
			var dto = new BlackJackGameDto
			{
				Status = game.Status,
				CurrentPlayerId = game.CurrentPlayer.Identifier,
				Players = new List<BlackJackPlayerDto>()
			};

			foreach (var player in game.Players)
			{
				BlackJackPlayerDto playerDto;
				if (game.Status != Enums.GameStatus.Complete)
				{
					var isCurrentPlayer = player.Identifier.Equals(playerId);
					playerDto = MapPlayer(player, isCurrentPlayer);
				}
				else
				{
					playerDto = MapPlayer(player, true);
				}
				dto.Players.Add(playerDto);
			}
			return dto;
		}

		private static BlackJackPlayerDto MapPlayer(BlackJackPlayer player, bool showAll)
		{
			return new BlackJackPlayerDto
			{
				Name = player.Name,
				Id = player.Identifier,
				Hands = MapHand(player.Hands, showAll),
				Status = player.Status
			};
		}

		private static List<HandDto> MapHand(IEnumerable<Hand> hands, bool showAll)
		{
			List<HandDto> handDtos = new List<HandDto>();
			foreach (var hand in hands)
			{
				var cards = MapCards(hand.Cards, showAll);
				var dto = new HandDto
				{
					
					Cards = cards,
					Identifier = hand.Identifier,
					Actions = GetHandActions(hand.Actions, showAll), 
					CardCount = hand.Cards.Count(),
					PointValue = cards.Sum(x => x.Value),
					Status = hand.Status
				};
				handDtos.Add(dto);
			}
			return handDtos;
		}

		private static IEnumerable<HandActionTypes> GetHandActions(
			IEnumerable<HandActionTypes> actions, bool showAll) =>
			showAll ? actions : Enumerable.Empty<HandActionTypes>();

		private static List<BlackJackCardDto> MapCards(IEnumerable<BlackJackCard> cards, bool showAll)
		{
			var cardDtos = new List<BlackJackCardDto>();

			foreach(var card in cards)
			{
				var cardDto = MapCard(card, showAll);
				cardDtos.Add(cardDto);
			}
			return cardDtos;
		}

		private static BlackJackCardDto MapCard(BlackJackCard card, bool showAll)
		{
			if (ShouldShowCardBack(card.FaceDown, showAll))
			{
				return new BlackJackCardDto()
				{
					ImageName = CardBackName
				};
			}

			return new BlackJackCardDto()
			{
				ImageName = $"{card.Rank.ToString().ToLower()}_of_{card.Suit.ToString().ToLower()}",
				Value = card.Value
			};
		}

		private static bool ShouldShowCardBack(bool faceDown, bool showAll) => 
			faceDown && showAll == false;
	}
}
