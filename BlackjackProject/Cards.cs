using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackProject
{
    internal class Cards
    {
        public bool IsFaceDown { get; set; }
        public enum Suit
        {
            Hearts,
            Diamonds,
            Clubs,
            Spades
        }
        public enum Rank
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Jack = 10,
            Queen = 10,
            King = 10,
            Ace = 11
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
                return "Card hidden";
            else
                return CardRank + " of " + CardSuit;
        }
    }
}
