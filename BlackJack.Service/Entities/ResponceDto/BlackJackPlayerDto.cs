using Entities.Enums;
using System;
using System.Collections.Generic;

namespace Entities.ResponceDto
{
	public class BlackJackPlayerDto
	{
		public string Name { get; set; }
		public string Id { get; set; }
		public List<HandDto> Hands { get; set; }
		public PlayerStatusTypes Status { get; set; }
	}
}
