using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlackjackProject
{
    public partial class Match_Screen_1 : Form
    {
        private GameLogic game;
        private bool isPlayingSplitHand = false;
        public Match_Screen_1()
        {
            InitializeComponent();
            game = new GameLogic("Player 1");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            Player_Select playerSelect = new Player_Select();
            playerSelect.Show();
        }

        private Image GetCardImage(Cards card)
        {
            if (card.IsFaceDown)
                return Properties.Resources.card_back;

            string suitPart = card.CardSuit.ToString();

            string rankPart;
            if (card.CardRank == Cards.Rank.two) rankPart = "02";
            else if (card.CardRank == Cards.Rank.three) rankPart = "03";
            else if (card.CardRank == Cards.Rank.four) rankPart = "04";
            else if (card.CardRank == Cards.Rank.five) rankPart = "05";
            else if (card.CardRank == Cards.Rank.six) rankPart = "06";
            else if (card.CardRank == Cards.Rank.seven) rankPart = "07";
            else if (card.CardRank == Cards.Rank.eight) rankPart = "08";
            else if (card.CardRank == Cards.Rank.nine) rankPart = "09";
            else if (card.CardRank == Cards.Rank.ten
                  || card.CardRank == Cards.Rank.jack
                  || card.CardRank == Cards.Rank.queen
                  || card.CardRank == Cards.Rank.king)
                rankPart = "10";
            else if (card.CardRank == Cards.Rank.ace) rankPart = "A";
            else rankPart = "02";

            string resName = $"card_{suitPart}_{rankPart}";
            return (Image)Properties.Resources.ResourceManager.GetObject(resName);

        }

        private void ShowDealerCards()
        {
            PictureBox[] slots = { pbDealer1, pbDealer2, pbDealer3, pbDealer4, pbDealer5 };
            var hand = game.dealer.cardsInHand;

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
            var hand = game.player.cardsInHand;

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

            if (game.player.SplitHand == null)
            {
                for (int i = 0; i < slots.Length; i++)
                    slots[i].Visible = false;
                return;
            }

            var hand = game.player.SplitHand.cardsInHand;

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

        private void UpdateUI()
        {
            ShowDealerCards();
            ShowPlayerMainHand();
            ShowPlayerSplitHand();

            bool hasFaceDown = false;
            for (int i = 0; i < game.dealer.cardsInHand.Count; i++)
            {
                if (game.dealer.cardsInHand[i].IsFaceDown)
                {
                    hasFaceDown = true;
                    break;
                }
            }

            if (hasFaceDown)
            {
                lblDealerHand.Text = "?";
            }
            else
            {
                lblDealerHand.Text = game.dealer.CalculateHandValue().ToString();
            }

            lblMainHand.Text = game.player.CalculateHandValue().ToString();
            if (game.player.SplitHand != null)
                lblSplitHand.Text = game.player.SplitHand.CalculateHandValue().ToString();
            else
                lblSplitHand.Text = string.Empty;

            lblBalance.Text = "Current balance is: " + game.player.Balance.ToString();
            lblBet.Text = "Current bet is: " + game.player.CurrentBet.ToString();

            if (isPlayingSplitHand)
            {
                lblActiveHand.Text = "Playing: Split hand";
            }
            else
            {
                lblActiveHand.Text = "Playing: Main hand";
            }

        }

        private void SetButtonsForPlayerTurn()
        {
            btnHit.Enabled = true;
            btnStand.Enabled = true;
            btnSplit.Enabled = game.player.CanSplit();
        }

        private void SetButtonsAfterRound()
        {
            btnHit.Enabled = false;
            btnStand.Enabled = false;
            btnSplit.Enabled = false;
        }

        private void CheckGameOver()
        {
            if (game.player.Balance <= 0)
            {
                MessageBox.Show("Out of credits. Game over.");
                btnStart.Enabled = false;
                btnHit.Enabled = false;
                btnStand.Enabled = false;
                btnSplit.Enabled = false;
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int bet = int.Parse(txtBet.Text);
            if (bet <= 0 || bet > game.player.Balance)
            {
                MessageBox.Show("Invalid bet.");
                return;
            }

            game.StartNewRound(bet);
            isPlayingSplitHand = false;
            UpdateUI();
            SetButtonsForPlayerTurn();
        }

        private void btnHit_Click(object sender, EventArgs e)
        {
            if (!isPlayingSplitHand)
            {
                game.PlayerHit();
                UpdateUI();

                if (game.IsPlayerBust())
                {
                    if (game.player.SplitHand != null)
                    {
                        isPlayingSplitHand = true;
                        SetButtonsForPlayerTurn();
                    }
                    else
                    {
                        game.PlayerStand();
                        UpdateUI();
                        SetButtonsAfterRound();
                        CheckGameOver();
                    }
                }
                else
                {
                    if (game.player.SplitHand != null && game.player.CalculateHandValue() >= 21)
                    {
                        isPlayingSplitHand = true;
                        SetButtonsForPlayerTurn();
                    }
                }
            }
            else
            {
                game.PlayerHitSplit();
                UpdateUI();

                int splitValue = game.player.SplitHand.CalculateHandValue();
                if (splitValue >= 21)
                {
                    game.PlayerStandSplit();
                    game.ResolveMainHand();
                    UpdateUI();
                    SetButtonsAfterRound();
                    CheckGameOver();
                }
            }
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            if (!isPlayingSplitHand)
            {
                if (game.player.SplitHand != null)
                {
                    isPlayingSplitHand = true;
                    SetButtonsForPlayerTurn();
                    UpdateUI();
                }
                else
                {
                    game.PlayerStand();
                    UpdateUI();
                    SetButtonsAfterRound();
                    CheckGameOver();
                }
            }
            else
            {
                game.PlayerStandSplit();
                game.ResolveMainHand();
                UpdateUI();
                SetButtonsAfterRound();
                CheckGameOver();
            }
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            game.PlayerSplit();
            isPlayingSplitHand = false;
            UpdateUI();
        }
    }
}
