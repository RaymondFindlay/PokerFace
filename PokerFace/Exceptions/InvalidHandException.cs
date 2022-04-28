using System;
using System.Collections.Generic;
using System.Text;

namespace PokerFace.Exceptions
{
    public sealed class InvalidHandException : Exception
    {
        public InvalidHandException(int cardCount)
            :base($"Hands should have 5 cards only, but found {cardCount} cards")
        {

        }
    }
}
