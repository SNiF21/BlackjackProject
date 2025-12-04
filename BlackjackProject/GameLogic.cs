using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackProject
{
    internal class GameLogic
    {
        private Deck deck;
        private Hand dealer;
        private Hand player;

        public Hand Dealer => dealer;
        public Hand Player => player;

        public GameLogic(string playerName)
        {
            deck = new Deck();
            deck.Shuffle();
            dealer = new Dealer();
            player = new Player(playerName);
        }

        public bool StartNewRound(int betAmount)
        {
            if (betAmount <= 0 || betAmount > ((Player)player).Balance)
                return false;

            deck.Reset();
            player.ClearHand();
            dealer.ClearHand();
            ((Player)player).PlaceBet(betAmount);
            ((Dealer)dealer).DealStartingHand(deck);
            player.DealStartingHand(deck);
            return true;
        }

        public int GetBalance()
        {
            return ((Player)player).Balance;
        }

        public bool IsPlayerBroke()
        {
            return ((Player)player).Balance <= 0;
        }

        public void PlayerSplit()
        {
            ((Player)player).Split(deck);
        }

        public void PlayerStand()
        {
            ((Dealer)dealer).PlayUntillEnd(deck);
            ResolveMainHand();
        }

        public void PlayerStandSplit()
        {
            ((Dealer)dealer).PlayUntillEnd(deck);
            ResolveSplitHand();
        }

        public void PlayerHit()
        {
            player.AddFromDeck(deck);
        }

        public void PlayerHitSplit()
        {
            if (((Player)player).SplitHand != null)
            {
                ((Player)player).SplitHand.AddFromDeck(deck);
            }
        }

        public void ResolveMainHand()
        {
            int playerValue = player.CalculateHandValue();
            int dealerValue = dealer.CalculateHandValue();

            if (player.IsBust())
            {
                ((Player)player).LostGame();
            }
            else if (dealer.IsBust() || playerValue > dealerValue)
            {
                ((Player)player).WonGame();
            }
            else if (playerValue == dealerValue)
            {
                ((Player)player).RefundBet();
            }
            else
            {
                ((Player)player).LostGame();
            }

            ((Player)player).ResetBet();
        }

        public bool IsPlayerBust()
        {
            return player.IsBust();
        }

        public bool IsDealerBust()
        {
            return dealer.IsBust();
        }

        public void ResolveSplitHand()
        {
            if (((Player)player).SplitHand == null)
                return;

            int splitValue = ((Player)player).SplitHand.CalculateHandValue();
            int dealerValue = dealer.CalculateHandValue();
            int splitBet = ((Player)player).CurrentBet;

            if (splitValue > 21)
            {
                //nu se intampla nimic
            }
            else if (dealerValue > 21 || splitValue > dealerValue)
            {
                ((Player)player).WinSplit();
            }
            else if (splitValue == dealerValue)
            {
                ((Player)player).TieSplit();
            }

            ((Player)player).ResetBet();
        }
    }
}
