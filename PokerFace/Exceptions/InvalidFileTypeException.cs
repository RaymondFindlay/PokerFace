using System;
using System.Collections.Generic;
using System.Text;

namespace PokerFace.Exceptions
{
    public sealed class InvalidFileTypeException : Exception
    {
        public InvalidFileTypeException()
            :base("Error: Invalid file type")
        {

        }
    }
}
