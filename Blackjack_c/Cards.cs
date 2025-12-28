using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_c
{
    internal class Cards
    {
        public bool IsFaceDown { get; set; }
        public enum Suit
        {
            hearts,
            diamonds,
            clubs,
            spades
        }
        public enum Rank
        {
            two = 2,
            three = 3,
            four = 4,
            five = 5,
            six = 6,
            seven = 7,
            eight = 8,
            nine = 9,
            ten = 10,
            jack = 10,
            queen = 10,
            king = 10,
            ace = 11
        }
        public Suit CardSuit { get; private set; }
        public Rank CardRank { get; private set; }
        public Cards(Suit suit, Rank rank, bool isFaceDown = false)
        {
            CardSuit = suit;
            CardRank = rank;
            IsFaceDown = isFaceDown;
        }
        public override string ToString()
        {
            if(IsFaceDown)
                return "card hidden";
            else
                return CardRank + " of " + CardSuit;
        }
    }
}
