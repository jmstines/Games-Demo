﻿using Entities.Enums;
using System.Collections.Generic;

namespace Entities.ResponceDto
{
	public class BlackJackPlayerDto
	{
		public string Name { get; set; }
		public string PlayerIdentifier { get; set; }
		public List<HandDto> Hands { get; set; }
		public PlayerStatusTypes Status { get; set; }
	}
}
