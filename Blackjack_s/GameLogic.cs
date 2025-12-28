using System;

namespace Blackjack_s
{
    internal class GameLogic
    {
        private Deck deck;
        private Hand dealer;
        private Hand player;
        private Hand clientPlayer;

        public enum Turn
        {
            Server,
            Client,
            None
        }

        private Turn currentTurn;
        public Turn CurrentTurn
        {
            get { return currentTurn; }
        }

        public Hand Dealer
        {
            get { return dealer; }
        }

        public Hand Player
        {
            get { return player; }
        }

        public Hand ClientPlayer
        {
            get { return clientPlayer; }
        }

        public GameLogic(string playerName)
        {
            deck = new Deck();
            deck.Shuffle();

            dealer = new Dealer();
            player = new Player(playerName);
            clientPlayer = new Player("Client");

            currentTurn = Turn.None;
        }

        public bool StartNewRound(int betAmount)
        {
            Player serverPlayer = (Player)player;

            if (betAmount <= 0)
                return false;

            if (betAmount > serverPlayer.Balance)
                return false;

            deck.Reset();

            dealer.ClearHand();
            player.ClearHand();
            clientPlayer.ClearHand();

            serverPlayer.PlaceBet(betAmount);

            ((Dealer)dealer).DealStartingHand(deck);
            player.DealStartingHand(deck);
            clientPlayer.DealStartingHand(deck);

            currentTurn = Turn.Server;
            return true;
        }

        public void PlayerHit()
        {
            player.AddFromDeck(deck);
        }

        public void PlayerStand()
        {
            currentTurn = Turn.Client;
        }

        public void ClientHit()
        {
            clientPlayer.AddFromDeck(deck);
        }

        public void ClientStand()
        {
            ((Dealer)dealer).PlayUntillEnd(deck);
            ResolveMainHand();
            currentTurn = Turn.None;
        }

        public void PlayerSplit()
        {
            ((Player)player).Split(deck);
        }

        public void PlayerHitSplit()
        {
            Player serverPlayer = (Player)player;

            if (serverPlayer.SplitHand != null)
            {
                serverPlayer.SplitHand.AddFromDeck(deck);
            }
        }

        public void PlayerStandSplit()
        {
            ((Dealer)dealer).PlayUntillEnd(deck);
            ResolveSplitHand();
        }

        public bool IsPlayerBust()
        {
            return player.IsBust();
        }

        public bool IsDealerBust()
        {
            return dealer.IsBust();
        }

        public int GetBalance()
        {
            return ((Player)player).Balance;
        }

        public bool IsPlayerBroke()
        {
            return ((Player)player).Balance <= 0;
        }

        public void SetTurn(Turn t)
        {
            currentTurn = t;
        }

        public void ResolveMainHand()
        {
            Player serverPlayer = (Player)player;

            int playerValue = player.CalculateHandValue();
            int dealerValue = dealer.CalculateHandValue();

            if (player.IsBust())
            {
                serverPlayer.LostGame();
            }
            else if (dealer.IsBust())
            {
                serverPlayer.WonGame();
            }
            else if (playerValue > dealerValue)
            {
                serverPlayer.WonGame();
            }
            else if (playerValue == dealerValue)
            {
                serverPlayer.RefundBet();
            }
            else
            {
                serverPlayer.LostGame();
            }

            serverPlayer.ResetBet();
        }

        public void ResolveSplitHand()
        {
            Player serverPlayer = (Player)player;

            if (serverPlayer.SplitHand == null)
                return;

            int splitValue = serverPlayer.SplitHand.CalculateHandValue();
            int dealerValue = dealer.CalculateHandValue();

            if (splitValue <= 21)
            {
                if (dealerValue > 21 || splitValue > dealerValue)
                {
                    serverPlayer.WinSplit();
                }
                else if (splitValue == dealerValue)
                {
                    serverPlayer.TieSplit();
                }
            }

            serverPlayer.ResetBet();
        }
    }
}
