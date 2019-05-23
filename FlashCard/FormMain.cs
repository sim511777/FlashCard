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
        private Settings settings;

        private Tuple<Type, byte[]>[] decks = {
            Tuple.Create(typeof(EfficiencyVoca[]), Properties.Resources.EffeciencyVoca),
            Tuple.Create(typeof(DrawingVocaMs[]), Properties.Resources.DrawingVoca_MiddleSchool),
            Tuple.Create(typeof(DrawingVoca[]), Properties.Resources.DrawingVoca_Csat),
            Tuple.Create(typeof(DrawingVoca[]), Properties.Resources.DrawingVoca_Toeic),
            Tuple.Create(typeof(Voca13000[]), Properties.Resources.Voca13000),
        };

        public FormMain() {
            this.settings = Settings.Load();

            InitializeComponent();
            this.cbxDeck.Items.AddRange(decks.Select(deck=>deck.Item1).ToArray());
            this.cbxDeck.SelectedIndex = this.settings.deckIndex;

            Point pt = this.settings.windowLocation;
            if (pt.X < 0)
                pt.X = 0;
            if (pt.Y < 0)
                pt.Y = 0;
            this.settings.windowLocation = pt;
            this.Location = this.settings.windowLocation;
            this.Size = this.settings.windowSize;
            this.chkAutoChange.Checked = this.settings.autoChange;
            this.TopMost = true;
            if (this.chkAutoChange.Checked)
                this.settings.lastIndex = (this.settings.lastIndex + 1);
            this.ChangeDeck();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            this.settings.windowLocation = this.Location;
            this.settings.windowSize = this.Size;
            this.settings.autoChange = this.chkAutoChange.Checked;
            this.settings.Save();
        }

        private void ChangeDeck() {
            this.ReadBook();
            this.ShowCard();
        }

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
            this.settings.lastIndex = cardHistory.ElementAt(historyPointer);
            ShowCard(false);
        }

        private void HistoryRedo() {
            if (historyPointer >= cardHistory.Count-1)
                return;
            historyPointer++;
            this.settings.lastIndex = cardHistory.ElementAt(historyPointer);
            ShowCard(false);
        }

        private void ShowCard(bool addHistory = true) {
            if (this.settings.lastIndex < 0)
                this.settings.lastIndex = 0;
            if (this.settings.lastIndex >= this.cards.Length)
                this.settings.lastIndex = this.cards.Length - 1;
            var card = this.cards[this.settings.lastIndex];
            var html = card.GetHtml();
            this.browser.DocumentText = html;
            this.cbxCard.SelectedIndex = this.settings.lastIndex;
            if (addHistory == true) {
                HistoryAdd(this.settings.lastIndex);
            }
        }

        public void ShowCardAndChangeCombo(int index) {
            this.settings.lastIndex = index;
            this.ShowCard();
        }

        private void BtnNext_Click(object sender, EventArgs e) {
            this.settings.lastIndex = (this.settings.lastIndex + 1) % this.cards.Length;
            this.ShowCard();
        }

        private void BtnPrev_Click(object sender, EventArgs e) {
            this.settings.lastIndex = (this.settings.lastIndex - 1 + this.cards.Length) % this.cards.Length;
            this.ShowCard();
        }

        private void cbxCard_SelectionChangeCommitted(object sender, EventArgs e) {
            this.settings.lastIndex = this.cbxCard.SelectedIndex;
            this.ShowCard();
        }

        private void CbxDeck_SelectionChangeCommitted(object sender, EventArgs e) {
            this.settings.deckIndex = this.cbxDeck.SelectedIndex;
            this.ChangeDeck();
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
