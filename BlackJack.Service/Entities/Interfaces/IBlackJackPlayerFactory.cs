using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Interfaces;

public interface IBlackJackPlayerFactory
{
    public KeyValuePair<string, IBlackJackPlayer> Create(PlayerTypes type, int? handCount = 1);
}
