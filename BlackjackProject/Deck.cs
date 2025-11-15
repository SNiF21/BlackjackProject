using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static BlackjackProject.Cards;

namespace BlackjackProject
{
    internal class Deck
    {
        private List<Cards> cards;
        private static Random rng = new Random();
        public int Count => cards.Count;
        private void Initialize()
        {
            cards.Clear();
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Cards(suit, rank));
                }
            }
        }

        public Deck()
        {
            cards = new List<Cards>();
            Initialize();
        }

        public void Shuffle()
        {
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Cards temp = cards[k];
                cards[k] = cards[n];
                cards[n] = temp;
            }
        }

        public Cards DealCard()
        {
            if (cards.Count == 0)
                throw new InvalidOperationException("No cards left in the deck.");
            Cards dealt = cards[0];
            cards.RemoveAt(0);
            return dealt;
        }

        public void Reset()
        {
            Initialize();
            Shuffle();
        }
    }
}
