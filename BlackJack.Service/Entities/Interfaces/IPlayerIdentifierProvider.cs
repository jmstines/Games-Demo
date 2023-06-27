using System;

namespace Entities.Interfaces;

public interface IPlayerIdentifierProvider
{
    string GeneratePlayerId();
}
