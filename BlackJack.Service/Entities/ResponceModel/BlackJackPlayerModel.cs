using Entities.Enums;
using System;
using System.Collections.Generic;

namespace Entities.ResponceDto
{
	public class BlackJackPlayerModel
	{
		public string Name { get; set; }
		public string Id { get; set; }
		public List<HandModel> Hands { get; set; }
		public PlayerStatusTypes Status { get; set; }
	}
}
