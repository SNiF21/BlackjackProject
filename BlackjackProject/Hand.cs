using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackProject
{
    internal class Hand
    {
        public List<Cards> cardsInHand { get; private set; }

        public int CardCount
        {
            get { return cardsInHand.Count; }
        }

        public Hand()
        {
            cardsInHand = new List<Cards>();
        }

        public Hand(Deck deck)
        {
            cardsInHand = new List<Cards>();
            DealStartingHand(deck);
        }

        public void AddCard(Cards card)
        {
            cardsInHand.Add(card);
        }

        public void AddFromDeck(Deck deck)
        {
            Cards card = deck.DealCard();
            cardsInHand.Add(card);
        }

        public virtual void DealStartingHand(Deck deck)
        {
            AddFromDeck(deck);
            AddFromDeck(deck);
        }

        public int CalculateHandValue()
        {
            int totalValue = 0;
            int aceCount = 0;
            for (int i = 0; i < CardCount; i++)
            {
                totalValue += (int)cardsInHand[i].CardRank;
                if (cardsInHand[i].CardRank == Cards.Rank.Ace)
                {
                    aceCount++;
                }
            }
            while (totalValue > 21 && aceCount > 0)
            {
                totalValue -= 10;
                aceCount--;
            }
            return totalValue;
        }

        public bool IsBust()
        {
            return CalculateHandValue() > 21;
        }

        public bool IsBlackjack()
        {
            return CardCount == 2 && CalculateHandValue() == 21;
        }

        public bool CanSplit()
        {
            return CardCount == 2 && cardsInHand[0].CardRank == cardsInHand[1].CardRank;
        }

        public virtual void ClearHand()
        {
            cardsInHand.Clear();
        }

        public override string ToString()
        {
            string str = "";
            for (int i = 0; i < CardCount; i++)
            {
                str += cardsInHand[i].ToString();
                if (i < CardCount - 1)
                    str += ", ";
            }
            return str;
        }

    }
}
