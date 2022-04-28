using PokerFace.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerFace.Models
{
    public sealed class Hand
    {
        public List<Card> Cards { get; set; }

        public Hand(string input)
        {
            if (input.Length > 0)
            {
                string[] rawCards = input.Split(' ');
                var cards = new List<Card>();

                foreach (var card in rawCards)
                {
                    cards.Add(
                        new Card(card[0].ToString(), card[1].ToString()));
                }

                Cards = cards.OrderBy(c => c.Value).ToList();
            }
            else
            {
                throw new InvalidHandException(0);
            }

        }
    }
}
