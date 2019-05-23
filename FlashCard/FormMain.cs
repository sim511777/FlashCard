using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlashCard.Properties;

namespace FlashCard {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
            this.cbxDeck.Items.AddRange(decks.Select(deck=>deck.Item1).ToArray());
            this.cbxDeck.SelectedIndex = Properties.Settings.Default.deckIndex;
            Point pt = Settings.Default.windowLocation;
            if (pt.X < 0)
                pt.X = 0;
            if (pt.Y < 0)
                pt.Y = 0;
            Settings.Default.windowLocation = pt;
            this.Location = Settings.Default.windowLocation;
            this.Size = Settings.Default.windowSize;
            this.chkAutoChange.Checked = Settings.Default.autoChange;
            this.TopMost = true;
            if (this.chkAutoChange.Checked)
                Settings.Default.lastIndex = (Settings.Default.lastIndex + 1);
            this.ChangeDeck();
        }

        private void ChangeDeck() {
            this.ReadBook();
            this.ShowCard();
        }

        private Tuple<Type, byte[]>[] decks = {
            Tuple.Create(typeof(EfficiencyVoca[]), Properties.Resources.EffeciencyVoca),
            Tuple.Create(typeof(DrawingVocaMs[]), Properties.Resources.DrawingVoca_MiddleSchool),
            Tuple.Create(typeof(DrawingVoca[]), Properties.Resources.DrawingVoca_Csat),
            Tuple.Create(typeof(DrawingVoca[]), Properties.Resources.DrawingVoca_Toeic),
            Tuple.Create(typeof(Voca13000[]), Properties.Resources.Voca13000),
        };
        public Voca[] cards;
        private void ReadBook() {
            var deck = this.decks[this.cbxDeck.SelectedIndex];
            this.cards = Voca.ReadBook(deck.Item1, deck.Item2);
            var wordList = this.cards.Select(card => card.GetTitle()).ToArray();
            this.cbxCard.Items.Clear();
            this.cbxCard.Items.AddRange(wordList);
        }

        LinkedList<int> cardHistory = new LinkedList<int>();
        int historyPointer = 0;
        private void HistoryAdd(int index) {
            while (historyPointer < cardHistory.Count-1)
                cardHistory.RemoveLast();
            cardHistory.AddLast(index);
            if (cardHistory.Count > 10000)
                cardHistory.RemoveFirst();
            historyPointer = cardHistory.Count - 1;
        }

        private void HistoryUndo() {
            if (historyPointer <= 0)
                return;
            historyPointer--;
            Settings.Default.lastIndex = cardHistory.ElementAt(historyPointer);
            ShowCard(false);
        }

        private void HistoryRedo() {
            if (historyPointer >= cardHistory.Count-1)
                return;
            historyPointer++;
            Settings.Default.lastIndex = cardHistory.ElementAt(historyPointer);
            ShowCard(false);
        }

        private void ShowCard(bool addHistory = true) {
            if (Settings.Default.lastIndex < 0)
                Settings.Default.lastIndex = 0;
            if (Settings.Default.lastIndex >= this.cards.Length)
                Settings.Default.lastIndex = this.cards.Length - 1;
            var card = this.cards[Settings.Default.lastIndex];
            var html = card.GetHtml();
            this.browser.DocumentText = html;
            this.cbxCard.SelectedIndex = Settings.Default.lastIndex;
            Settings.Default.Save();
            if (addHistory == true) {
                HistoryAdd(Settings.Default.lastIndex);
            }
        }

        public void ShowCardAndChangeCombo(int index) {
            Settings.Default.lastIndex = index;
            this.ShowCard();
        }

        private void BtnNext_Click(object sender, EventArgs e) {
            Settings.Default.lastIndex = (Settings.Default.lastIndex + 1) % this.cards.Length;
            this.ShowCard();
        }

        private void BtnPrev_Click(object sender, EventArgs e) {
            Settings.Default.lastIndex = (Settings.Default.lastIndex - 1 + this.cards.Length) % this.cards.Length;
            this.ShowCard();
        }

        private void FormMain_Move(object sender, EventArgs e) {
            Settings.Default.windowLocation = this.Location;
            Settings.Default.Save();
        }

        private void FormMain_Resize(object sender, EventArgs e) {
            Settings.Default.windowSize = this.Size;
            Settings.Default.Save();
        }

        private void cbxCard_SelectionChangeCommitted(object sender, EventArgs e) {
            Settings.Default.lastIndex = this.cbxCard.SelectedIndex;
            this.ShowCard();
        }

        private void CbxDeck_SelectionChangeCommitted(object sender, EventArgs e) {
            Settings.Default.deckIndex = this.cbxDeck.SelectedIndex;
            this.ChangeDeck();
        }

        private void chkAutoChange_Click(object sender, EventArgs e) {
            Properties.Settings.Default.autoChange = this.chkAutoChange.Checked;
            Properties.Settings.Default.Save();
        }

        public FormSearch frmSearch = null;
        private void btnSearch_Click(object sender, EventArgs e) {
            if (frmSearch == null) {
                this.frmSearch = new FormSearch(this);
                this.frmSearch.Left = this.Right;
                this.frmSearch.Top = this.Top;
                this.frmSearch.Height = this.Height;
            }
             if (!this.frmSearch.Visible)
                this.frmSearch.Show(this);
             else
                this.frmSearch.BringToFront();
             this.frmSearch.WindowState = FormWindowState.Normal;
         }

        private void btnUndo_Click(object sender, EventArgs e) {
            this.HistoryUndo();
        }

        private void btnRedo_Click(object sender, EventArgs e) {
            this.HistoryRedo();
        }
    }
}
