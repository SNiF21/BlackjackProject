using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blackjack_c
{
    public partial class Match_Screen_1 : Form
    {
        private ClientNetwork client;
        private List<string> stateBuffer = new List<string>();
        private string currentTurn = "None";

        private List<Cards> dealerCards = new List<Cards>();
        private List<Cards> playerCards = new List<Cards>();
        private List<Cards> clientCards = new List<Cards>();

        public Match_Screen_1()
        {
            InitializeComponent();
            client = new ClientNetwork();
            client.MessageReceived += OnMessageReceived;
            client.Connect();
        }

        private void OnMessageReceived(string line)
        {
            Invoke(new Action(() =>
            {
                if (line == "STATE")
                {
                    stateBuffer.Clear();
                }
                else if (line == "END")
                {
                    ApplyState(stateBuffer);
                }
                else
                {
                    stateBuffer.Add(line);
                }
            }));
        }

        private void ApplyState(List<string> state)
        {
            dealerCards.Clear();
            playerCards.Clear();
            clientCards.Clear();

            for (int i = 0; i < state.Count; i++)
            {
                string line = state[i];

                if (line.StartsWith("DEALER:"))
                    ParseCards(line.Substring(7), dealerCards);

                else if (line.StartsWith("SERVER:"))
                    ParseCards(line.Substring(7), playerCards);

                else if (line.StartsWith("CLIENT:"))
                    ParseCards(line.Substring(7), clientCards);

                else if (line.StartsWith("BALANCE:"))
                    lblBalance.Text = "Current balance is: " + line.Substring(8);

                else if (line.StartsWith("BET:"))
                    lblBet.Text = "Current bet is: " + line.Substring(4);

                else if (line.StartsWith("TURN:"))
                    currentTurn = line.Substring(5);
            }

            UpdateButtonsForTurn();
            ShowDealerCards();
            ShowPlayerMainHand();
            ShowPlayerSplitHand();
        }

        private void UpdateButtonsForTurn()
        {
            bool myTurn = currentTurn == "Client";

            btnHit.Enabled = myTurn;
            btnStand.Enabled = myTurn;
        }

        private void ParseCards(string data, List<Cards> target)
        {
            target.Clear();

            if (string.IsNullOrEmpty(data))
                return;

            string[] cardStrings = data.Split('|');

            for (int i = 0; i < cardStrings.Length; i++)
            {
                if (string.IsNullOrEmpty(cardStrings[i]))
                    continue;

                string[] parts = cardStrings[i].Split('-');
                if (parts.Length != 3)
                    continue;

                Cards.Suit suit = (Cards.Suit)Enum.Parse(typeof(Cards.Suit), parts[0]);
                Cards.Rank rank = (Cards.Rank)Enum.Parse(typeof(Cards.Rank), parts[1]);
                bool isFaceDown = bool.Parse(parts[2]);

                Cards card = new Cards(suit, rank, isFaceDown);
                target.Add(card);
            }
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

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < dealerCards.Count)
                {
                    slots[i].Image = GetCardImage(dealerCards[i]);
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

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < playerCards.Count)
                {
                    slots[i].Image = GetCardImage(playerCards[i]);
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

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < clientCards.Count)
                {
                    slots[i].Image = GetCardImage(clientCards[i]);
                    slots[i].Visible = true;
                }
                else
                {
                    slots[i].Visible = false;
                }
            }
        }

        /*
                 private void UpdateUI()
                 {
                     ShowDealerCards();
                     ShowPlayerMainHand();
                     ShowPlayerSplitHand();

                     bool hasFaceDown = false;
                     for (int i = 0; i < ((Dealer)game.Dealer).cardsInHand.Count; i++)
                     {
                         if (((Dealer)game.Dealer).cardsInHand[i].IsFaceDown)
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
                         lblDealerHand.Text = game.Dealer.CalculateHandValue().ToString();
                     }

                     lblMainHand.Text = game.Player.CalculateHandValue().ToString();
                     if (((Player)game.Player).SplitHand != null)
                         lblSplitHand.Text = ((Player)game.Player).SplitHand.CalculateHandValue().ToString();
                     else
                         lblSplitHand.Text = string.Empty;

                     lblBalance.Text = "Current balance is: " + ((Player)game.Player).Balance.ToString();
                     lblBet.Text = "Current bet is: " + ((Player)game.Player).CurrentBet.ToString();

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
                     btnSplit.Enabled = game.Player.CanSplit();
                 }

                 private void SetButtonsAfterRound()
                 {
                     btnHit.Enabled = false;
                     btnStand.Enabled = false;
                     btnSplit.Enabled = false;
                 }

                 private void CheckGameOver()
                 {
                     if (((Player)game.Player).Balance <= 0)
                     {
                         MessageBox.Show("Out of credits. Game over.");
                         btnStart.Enabled = false;
                         btnHit.Enabled = false;
                         btnStand.Enabled = false;
                         btnSplit.Enabled = false;
                     }
                 }
         */

        private void btnHit_Click(object sender, EventArgs e)
        {
            client.Send("CLIENT:HIT");
        }

        private void btnStand_Click(object sender, EventArgs e)
        {
            client.Send("CLIENT:STAND");
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Hide();
            new Player_Select().Show();
        }
    }
}
