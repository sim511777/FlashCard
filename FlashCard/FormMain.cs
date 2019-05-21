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
            this.Location = Settings.Default.windowLocation;
            this.Size = Settings.Default.windowSize;
            this.Activate();
            this.ReadBook();
            this.ShowCard();
        }

        private string[] cards;
        private void ReadBook() {
            var allText = Properties.Resources.VocaRoot;
            cards = allText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
        }

        private void ShowCard() {
            var lines = this.cards[Settings.Default.lastIndex].Split(new string[] { " || " }, StringSplitOptions.RemoveEmptyEntries);
            this.lblCard.Text = Settings.Default.lastIndex.ToString() + ". " + string.Join("\r\n", lines);
        }

        private void BtnNext_Click(object sender, EventArgs e) {
            Settings.Default.lastIndex = (Settings.Default.lastIndex + 1) % this.cards.Length;
            Settings.Default.Save();
            this.ShowCard();
        }

        private void BtnPrev_Click(object sender, EventArgs e) {
            Settings.Default.lastIndex = (Settings.Default.lastIndex - 1 + this.cards.Length) % this.cards.Length;
            Settings.Default.Save();
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
    }
}
