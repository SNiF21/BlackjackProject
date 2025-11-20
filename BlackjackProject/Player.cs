using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackProject
{
    internal class Player : Hand
    {
        public string Name { get; set; }
        public int Balance { get; set; }
        public int CurrentBet {  get; set; }
        public Hand SplitHand { get; set; } = null;
        public Player(string name) : base()
        {
            Name = name;
            Balance =5000;
        }

        public Player(string name, Deck deck) : base(deck)
        {
            Name = name;
            Balance = 5000;
        }

        public void PlaceBet(int bet)
        {
            if (bet > 0 && bet <= Balance)
            {
                CurrentBet = bet;
                Balance -= bet;
            }
        }
        
        public void WonGame()
        {
            Balance += CurrentBet * 2;
            CurrentBet = 0;
        }

        public void LostGame()
        {
            CurrentBet = 0;
        }

        public void Split(Deck deck)
        {
            if(this.CanSplit() && SplitHand == null)
            {
                SplitHand = new Hand();
                SplitHand.AddCard(cardsInHand[1]);
                this.cardsInHand.RemoveAt(1);
                this.AddFromDeck(deck);
                SplitHand.AddFromDeck(deck);
            }
        }

        public override void ClearHand()
        {
            base.ClearHand();
            SplitHand = null;
            CurrentBet = 0;
        }


    }
}
