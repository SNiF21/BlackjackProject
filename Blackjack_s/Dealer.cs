using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_s
{
    internal class Dealer : Hand
    {
        public Dealer() : base() { }
        public Dealer(Deck deck) : base(deck) { }
        public override void DealStartingHand(Deck deck)
        {
            AddFromDeck(deck);
            Cards hiddenCard = deck.DealHiddenCard();
            AddCard(hiddenCard);
        }

        public void RevealCard()
        {
            cardsInHand[1].IsFaceDown = false;
        }

        public void PlayUntillEnd(Deck deck)
        {
            RevealCard();
            while (CalculateHandValue()<17)
            {
                AddFromDeck(deck);
            }
        }
    }
}
