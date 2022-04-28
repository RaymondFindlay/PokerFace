using System;
using System.Collections.Generic;
using System.Text;

namespace PokerFace.Exceptions
{
    public sealed class NullFileException : Exception
    {
        public NullFileException()
            :base("Error: File does not exist")
        {

        }
    }
}
