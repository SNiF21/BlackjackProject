using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using static Blackjack_s.GameLogic;

namespace Blackjack_s
{
    public partial class Match_Screen_1 : Form
    {
        private GameLogic game;
        private ServerNetwork server;
        private bool isPlayingSplitHand = false;

        public Match_Screen_1()
        {
            InitializeComponent();

            game = new GameLogic("Player 1");

            server = new ServerNetwork();
            server.CommandReceived += OnCommandReceived;
            server.Start();
        }

        private void OnCommandReceived(string command)
        {
            Invoke(new Action(() =>
            {
                ProcessCommand(command);
                UpdateUI();
            }));
        }

        private void ProcessCommand(string command)
        {
            if (command.StartsWith("SERVER:START"))
            {
                int bet = int.Parse(command.Split(' ')[1]);
                game.StartNewRound(bet);
                SendState();
                return;
            }

            if (command == "SERVER:HIT")
            {
                if (game.CurrentTurn != Turn.Server)
                    return;

                game.PlayerHit();

                if (game.IsPlayerBust())
                    game.PlayerStand();

                SendState();
                return;
            }

            if (command == "SERVER:STAND")
            {
                if (game.CurrentTurn != Turn.Server)
                    return;

                game.PlayerStand();
                SendState();
                return;
            }

            if (command == "CLIENT:HIT")
            {
                if (game.CurrentTurn != Turn.Client)
                    return;

                game.ClientHit();

                if (game.ClientPlayer.IsBust())
                    game.ClientStand();

                SendState();
                return;
            }

            if (command == "CLIENT:STAND")
            {
                if (game.CurrentTurn != Turn.Client)
                    return;

                game.ClientStand();
                SendState();
                return;
            }

            SendState();
        }

        private void SendState()
        {
            server.Send("STATE");

            SendDealerState();
            SendServerPlayerState();
            SendClientPlayerState();

            server.Send("BALANCE:" + ((Player)game.Player).Balance);
            server.Send("BET:" + ((Player)game.Player).CurrentBet);
            server.Send("TURN:" + game.CurrentTurn.ToString());

            server.Send("END");
        }

        private void SendDealerState()
        {
            StringBuilder builder = new StringBuilder("DEALER:");
            List<Cards> cards = ((Dealer)game.Dealer).cardsInHand;

            for (int i = 0; i < cards.Count; i++)
            {
                builder.Append(cards[i].CardSuit);
                builder.Append("-");
                builder.Append(cards[i].CardRank);
                builder.Append("-");
                builder.Append(cards[i].IsFaceDown);
                builder.Append("|");
            }

            server.Send(builder.ToString());
        }

        private void SendServerPlayerState()
        {
            StringBuilder builder = new StringBuilder("SERVER:");
            List<Cards> cards = game.Player.cardsInHand;

            for (int i = 0; i < cards.Count; i++)
            {
                builder.Append(cards[i].CardSuit);
                builder.Append("-");
                builder.Append(cards[i].CardRank);
                builder.Append("-false|");
            }

            server.Send(builder.ToString());
        }

        private void SendClientPlayerState()
        {
            StringBuilder builder = new StringBuilder("CLIENT:");
            List<Cards> cards = game.ClientPlayer.cardsInHand;

            for (int i = 0; i < cards.Count; i++)
            {
                builder.Append(cards[i].CardSuit);
                builder.Append("-");
                builder.Append(cards[i].CardRank);
                builder.Append("-false|");
            }

            server.Send(builder.ToString());
        }

        private void UpdateUI()
        {
            ShowDealerCards();
            ShowPlayerMainHand();
            ShowPlayerSplitHand();

            lblBalance.Text = "Current balance is: " + ((Player)game.Player).Balance;
            lblBet.Text = "Current bet is: " + ((Player)game.Player).CurrentBet;

            if (isPlayingSplitHand)
                lblActiveHand.Text = "Playing: Client hand";
            else
                lblActiveHand.Text = "Playing: Server hand";

            bool serverTurn = game.CurrentTurn == Turn.Server;

            btnHit.Enabled = serverTurn;
            btnStand.Enabled = serverTurn;
            btnStart.Enabled = game.CurrentTurn == Turn.None;
        }

        private void ShowDealerCards()
        {
            PictureBox[] slots = { pbDealer1, pbDealer2, pbDealer3, pbDealer4, pbDealer5 };
            List<Cards> hand = ((Dealer)game.Dealer).cardsInHand;

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < hand.Count)
                {
                    slots[i].Image = GetCardImage(hand[i]);
                    slots[i].Visible = true;
                }
                else
                {
                    slots[i].Visible = false;
                }
            }
        }

        private void ShowPlayerMainHand()
        {
            PictureBox[] slots = { pbPlayer1, pbPlayer2, pbPlayer3, pbPlayer4, pbPlayer5 };
            List<Cards> hand = game.Player.cardsInHand;

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < hand.Count)
                {
                    slots[i].Image = GetCardImage(hand[i]);
                    slots[i].Visible = true;
                }
                else
                {
                    slots[i].Visible = false;
                }
            }
        }

        private void ShowPlayerSplitHand()
        {
            PictureBox[] slots = { pbSplit1, pbSplit2, pbSplit3, pbSplit4, pbSplit5 };
            List<Cards> hand = game.ClientPlayer.cardsInHand;

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < hand.Count)
                {
                    slots[i].Image = GetCardImage(hand[i]);
                    slots[i].Visible = true;
                }
                else
                {
                    slots[i].Visible = false;
                }
            }
        }

        private Image GetCardImage(Cards card)
        {
            if (card.IsFaceDown)
                return Properties.Resources.card_back;

            string suit = card.CardSuit.ToString();
            string rank;

            if (card.CardRank == Cards.Rank.ace)
                rank = "A";
            else if ((int)card.CardRank >= 10)
                rank = "10";
            else
                rank = ((int)card.CardRank).ToString("D2");

            return (Image)Properties.Resources.ResourceManager.GetObject($"card_{suit}_{rank}");
        }

        private void btnHit_Click(object sender, EventArgs e)
        {
            ProcessCommand("SERVER:HIT");
            UpdateUI();
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            ProcessCommand("SERVER:STAND");
            UpdateUI();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int bet = int.Parse(txtBet.Text);
            ProcessCommand("SERVER:START " + bet);
            UpdateUI();
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            game.PlayerSplit();
            isPlayingSplitHand = false;
            UpdateUI();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            new Player_Select().Show();
        }
    }
}
