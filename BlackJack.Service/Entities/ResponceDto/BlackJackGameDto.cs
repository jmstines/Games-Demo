using Entities.Enums;
using System;
using System.Collections.Generic;

namespace Entities.ResponceDto
{
	public class BlackJackGameDto
	{
		public List<BlackJackPlayerDto> Players { get; set; }
		public string CurrentPlayerId { get; set; }
		public GameStatus Status { get; set; }
	}
}
