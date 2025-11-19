using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackProject
{
    internal class Dealer : Hand
    {
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
