using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackjackProject
{
    internal class GameLogic
    {
        public Deck deck;
        public Dealer dealer;
        public Player player;

        public GameLogic(string playerName)
        {
            deck = new Deck();
            deck.Shuffle();
            dealer = new Dealer();
            player = new Player(playerName);
        }

        public void StartNewRound(int betAmmount)
        {
            deck.Reset();
            dealer.ClearHand();
            player.ClearHand();
            player.PlaceBet(betAmount);
            dealer.DealStartingHand(deck);
            player.DealStartingHand(deck);
        }

       

        public void PlayerSplit()
        {
            player.Split(deck);
        }

        public void PlayerStand()
        {
            dealer.PlayUntillEnd(deck);
            ResolveMainHand();
        }

        public void PlayerStandSplit()
        {
            dealer.PlayUntillEnd(deck);
            ResolveSplitHand();
        }

        public void PlayerHit()
        {
            player.AddFromDeck(deck);
        }

        public void PlayerHitSplit()
        {
            if (player.SplitHand != null)
            {
                player.SplitHand.AddFromDeck(deck);
            }
        }

        public void ResolveMainHand()
        {
            int playerValue = player.CalculateHandValue();
            int dealerValue = dealer.CalculateHandValue();

            if (player.IsBust())
            {
                player.LostGame();
            }
            else if (dealer.IsBust() || playerValue > dealerValue)
            {
                player.WonGame();
            }
            else if (playerValue == dealerValue)
            {
                player.Balance += player.CurrentBet;
                player.CurrentBet = 0;
            }
            else
            {
                player.LostGame();
            }
        }

        public void ResolveSplitHand()
        {
            if (player.SplitHand == null)
                return;

            int splitValue = player.SplitHand.CalculateHandValue();
            int dealerValue = dealer.CalculateHandValue();
            int splitBet = player.CurrentBet;

            if (splitValue > 21)
            {
                //nu se intampla nimic
            }
            else if (dealerValue > 21 || splitValue > dealerValue)
            {
                player.Balance += 2 * splitBet;
            }
            else if (splitValue == dealerValue)
            {
                player.Balance += splitBet;
            }
            
            player.SplitHand = null;
        }
    }
}
