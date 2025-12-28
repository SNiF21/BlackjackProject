using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack_c
{
    internal class Player : Hand
    {
        public int Balance { get; private set; }
        public int CurrentBet { get; private set; }
        public Hand SplitHand { get; private set; } = null;

        public Player(string name)
        {
            Balance = 1000;
        }

        public void PlaceBet(int amount)
        {
            CurrentBet = amount;
            Balance -= amount;
        }

        public void WonGame()
        {
            Balance += 2 * CurrentBet;
            CurrentBet = 0;
        }

        public void LostGame()
        {
            CurrentBet = 0;
        }

        public void Split(Deck deck)
        {
            SplitHand = new Hand();
            SplitHand.AddCard(cardsInHand[1]);
            cardsInHand.RemoveAt(1);
            AddFromDeck(deck);
            SplitHand.AddFromDeck(deck);
            Balance -= CurrentBet;
        }

        public void ResetBet()
        {
            CurrentBet = 0;
        }

        public void RefundBet()
        {
            Balance += CurrentBet;
            CurrentBet = 0;
        }

        public void WinSplit()
        {
            Balance += 2 * CurrentBet;
        }

        public void TieSplit()
        {
            Balance += CurrentBet;
        }
    }
}
