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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace FlashCard {
    public partial class FormMain : Form {
        public FormMain() {
            InitializeComponent();
            Point pt = Settings.Default.windowLocation;
            if (pt.X < 0)
                pt.X = 0;
            if (pt.Y < 0)
                pt.Y = 0;
            Settings.Default.windowLocation = pt;

            this.Location = Settings.Default.windowLocation;
            this.Size = Settings.Default.windowSize;
            this.Activate();
            this.ReadBook();
            this.ShowCard();
            this.cbxCard.SelectedIndex = Settings.Default.lastIndex;
        }

        Card[] cards;
        private void ReadBook() {
            var bytes = Properties.Resources.C_VOCA;
            var allText = Encoding.UTF8.GetString(bytes);
            var ser = new DataContractJsonSerializer(typeof(Card[]));
            using (var ms = new MemoryStream(bytes)) {
                this.cards = (Card[])ser.ReadObject(ms);
            }
            var wordList = this.cards.Select(card => string.Format("{0}. {1} : {2}", card.VOCA_ID, card.VOCABULARY, card.MEANING_INDEX)).ToArray();
            this.cbxCard.Items.AddRange(wordList);
        }

        private string GetHtml(Card card) {
            string html =
$@"<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <meta http-equiv=""X-UA-Compatible"" content=""ie=edge"">
    <title>Document</title>
<style>
{Properties.Resources.main_study}
</style>
</head>
<body>
    <table width=""100%"">
        <tr>
            <td><font size=""6"">{(card.PREFIX_ORD == 0 ? card.VOCABULARY : card.VOCABULARY_TAG)}</font>{card.ORIGIN_APPENDIX}{card.MEANING_TAG}</td>
            <td>{card.ORIGIN_EXP_TAG}</td>
        </tr>
        <tr>
            <td colspan=""2"">{card.DERIVATIVE_TAG}</td>
        </tr>
        <tr>
            <td colspan=""2"">{(card.SENTENCE_TAG.Replace("opacity:0","opacity:100"))}</td>
        </tr>
    </table>
</body>
</html>";
            return html;
        }

        private void ShowCard() {
            var card = this.cards[Settings.Default.lastIndex];
            var html = GetHtml(card);
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
    }
}
