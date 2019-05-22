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
            this.TopMost = true;
            this.ChangeDeck();
        }

        private void ChangeDeck() {
            this.ReadBook();
            this.ShowCard();
            this.cbxCard.SelectedIndex = Settings.Default.lastIndex;
        }

        private Tuple<Type, byte[]>[] decks = {
            Tuple.Create(typeof(EfficiencyVoca[]), Properties.Resources.EffeciencyVoca),
            Tuple.Create(typeof(DrawingVocaMs[]), Properties.Resources.DrawingVoca_MiddleSchool),
            Tuple.Create(typeof(DrawingVoca[]), Properties.Resources.DrawingVoca_Csat),
            Tuple.Create(typeof(DrawingVoca[]), Properties.Resources.DrawingVoca_Toeic),
            Tuple.Create(typeof(Voca13000[]), Properties.Resources.Voca13000),
        };
        private Voca[] cards;
        private void ReadBook() {
            var deck = this.decks[this.cbxDeck.SelectedIndex];
            this.cards = Voca.ReadBook(deck.Item1, deck.Item2);
            var wordList = this.cards.Select(card => card.GetTitle()).ToArray();
            this.cbxCard.Items.Clear();
            this.cbxCard.Items.AddRange(wordList);
        }

        private void ShowCard() {
            if (Settings.Default.lastIndex < 0)
                Settings.Default.lastIndex = 0;
            if (Settings.Default.lastIndex >= this.cards.Length)
                Settings.Default.lastIndex = this.cards.Length - 1;
            var card = this.cards[Settings.Default.lastIndex];
            var html = card.GetHtml();
            this.browser.DocumentText = html;
        }

        private void BtnNext_Click(object sender, EventArgs e) {
            Settings.Default.lastIndex = (Settings.Default.lastIndex + 1) % this.cards.Length;
            Settings.Default.Save();
            this.ShowCard();
            this.cbxCard.SelectedIndex = Settings.Default.lastIndex;
        }

        private void BtnPrev_Click(object sender, EventArgs e) {
            Settings.Default.lastIndex = (Settings.Default.lastIndex - 1 + this.cards.Length) % this.cards.Length;
            Settings.Default.Save();
            this.ShowCard();
            this.cbxCard.SelectedIndex = Settings.Default.lastIndex;
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
            Settings.Default.Save();
            this.ShowCard();
        }

        private void CbxDeck_SelectionChangeCommitted(object sender, EventArgs e) {
            this.ChangeDeck();
            Settings.Default.deckIndex = this.cbxDeck.SelectedIndex;
            Settings.Default.Save();
        }
    }
}
