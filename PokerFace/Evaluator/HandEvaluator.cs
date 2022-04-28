using PokerFace.Exceptions;
using PokerFace.Interfaces;
using PokerFace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PokerFace.Evaluator
{
    public class HandEvaluator : IEvaluator
    {
        private string bestHand = WinningHand.HighCard;

        public string Evaluate(string input)
        {
            // construct Hand object from string input
            var hand = new Hand(input);
            
            // Hand should only ever have 5 cards
            if (hand.Cards.Count != 5)
            {
                throw new InvalidHandException(hand.Cards.Count);
            }

            GetSets(hand); // determine pair, 3oak, 4oak, fullhouse
            GetStraightsAndFlushes(hand); // determine straights and flushes

            return bestHand;
        }

        //use this - evaluate sets i.e pairs, 3oak, 4oak, fullhouse
        private void GetSets(Hand hand)
        {
            var matches = hand.Cards
                .GroupBy(c => c.Value)
                .Where(g => g.Count() > 1)
                .ToList();

            //sets
            if (matches.Count == 1)
            {
                var set = matches[0].Count() switch
                {
                    2 => WinningHand.OnePair,
                    3 => WinningHand.ThreeOfAKind,
                    4 => WinningHand.FourOfAKind
                };

                bestHand = set;
            }

            // two pair, Full House
            if (matches.Count == 2)
            {
                // 2 sets of matches where one match is 3oak indicates a Full House
                if (matches[0].Count() == 3 || matches[1].Count() == 3)
                {
                    bestHand = WinningHand.FullHouse;
                }
                else
                {
                    bestHand = WinningHand.TwoPair;
                }
            }
        }

        private void GetStraightsAndFlushes(Hand hand)
        {
            // check suits
            var flush = isFlush(hand);
            // check sequence
            var straight = isStraight(hand);

            if (flush && straight)
            {
                var orderedCards = hand.Cards.OrderBy(c => c.Value).ToList();
                // if top card is ace and all same suit, its a royal flush
                if (orderedCards[4].Value == 13)
                {
                    bestHand = WinningHand.RoyalFlush;
                }
                else
                {
                    bestHand = WinningHand.StraightFlush;
                }
            }

            if (flush && !straight)
            {
                bestHand = WinningHand.Flush;
            }

            if (straight && !flush)
            {
                bestHand = WinningHand.Straight;
            }
        }

        private bool isFlush(Hand hand)
        {
            var suits = hand.Cards.OrderBy(c => c.Suit).ToList();
            if (suits[0].Suit == suits[4].Suit)
            {
                return true;
            }

            return false;
        }

        private bool isStraight(Hand hand)
        {
            // mutable variable in case it is required to reorder (see Ace High)
            var cards = hand.Cards;

            // is straight when there are no pairs, and difference between first and last value is 4
            var matches = hand.Cards
                .GroupBy(c => c.Value)
                .Where(g => g.Count() > 1)
                .ToList().Count;


            // Handle Ace High:
            // Check Ace is present
            var aces = hand.Cards.Select(c => c.Value).Where(c => c == 0).ToList();

            // Only apply Ace high if current High card is a King and an Ace is present
            if (hand.Cards[4].Value == 12 && aces.Count == 1)
            {
                // set Ace High value
                hand.Cards[0].Value = 13;
                // reorder cards in the hand
                cards = hand.Cards.OrderBy(c => c.Value).ToList();
            }

            // Get numerical difference between the high and low card in the hand
            var difference = cards[4].Value - cards[0].Value;

            // For a Straight, the difference should always be 4 and there should be no duplicates (pairs)
            if (difference == 4 && matches == 0)
            {
                return true;
            }

            return false;
        }
    }
}
