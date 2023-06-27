using Entities.Enums;
using Entities.Interfaces;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

namespace Entities;

public class Hand
{
	public List<HandActionTypes> Actions = new();
	public List<BlackJackCard> Cards = new();
	public int PointValue { get; set; } = 0;
	public HandStatusTypes Status { get; set; } = HandStatusTypes.InProgress;
}
