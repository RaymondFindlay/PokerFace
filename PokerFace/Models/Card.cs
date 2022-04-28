using System.Collections.Generic;

namespace PokerFace.Models
{
    public sealed class Card
    {
        public int Value { get; set; }
        public string Suit { get; set; }

        public Card(string value, string suit)
        {
            var values = new Dictionary<string, int>()
            {
                { "A", 0 },
                { "2", 1 },
                { "3", 2 },
                { "4", 3 },
                { "5", 4 },
                { "6", 5 },
                { "7", 6 },
                { "8", 7 },
                { "9", 8 },
                { "T", 9 },
                { "J", 10 },
                { "Q", 11 },
                { "K", 12 },
            };

            values.TryGetValue(value, out int rank);

            Value = rank;
            Suit = suit;
        }
    }
}
