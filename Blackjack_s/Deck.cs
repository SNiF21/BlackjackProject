using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Blackjack_s.Cards;

namespace Blackjack_s
{
    internal class Deck
    {
        private List<Cards> cardsInDeck;
        private Random rng = new Random();
        public int Count
        {
            get { return cardsInDeck.Count; }
        }

        private void Initialize()
        {
            cardsInDeck.Clear();

            Array suits = Enum.GetValues(typeof(Suit));
            Array ranks = Enum.GetValues(typeof(Rank));

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; j++)
                {
                    cardsInDeck.Add(new Cards((Suit)suits.GetValue(i), (Rank)ranks.GetValue(j)));
                }
            }
        }

        public Deck()
        {
            cardsInDeck = new List<Cards>();
            Initialize();
        }

        public void Shuffle()
        {
            int n = cardsInDeck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Cards temp = cardsInDeck[k];
                cardsInDeck[k] = cardsInDeck[n];
                cardsInDeck[n] = temp;
            }
        }

        public Cards DealCard()
        {
            if (cardsInDeck.Count == 0)
                throw new Exception("No cards left in the deck.");
            Cards dealt = cardsInDeck[0];
            cardsInDeck.RemoveAt(0);
            return dealt;
        }

        public Cards DealHiddenCard()
        {
            Cards dealtHidden = DealCard();
            dealtHidden.IsFaceDown = true;
            return dealtHidden;
        }

        public void Reset()
        {
            Initialize();
            Shuffle();
        }
    }
}
