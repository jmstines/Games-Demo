using Entities.Enums;
using System;
using System.Collections.Generic;

namespace Entities.ResponceDto
{
	public class HandModel
	{
		public string Identifier { get; set; }
		public IEnumerable<HandActionTypes> Actions { get; set; }
		public IEnumerable<BlackJackCardModel> Cards { get; set; }
		public int CardCount { get; set; }
		public int? PointValue { get; set; }
		public HandStatusTypes Status { get; set; }
	}
}
