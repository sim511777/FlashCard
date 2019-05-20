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
         this.ReadBook();
         this.ShowCard();
      }

      bool front = true;
      private void ShowCard() {
         front = true;
         if (this.chkHideAnswer.Checked)
            this.ShowHalf(front);
         else
            this.ShowAll();
      }

      private void ShowAll() {
         var lines = this.cards[Settings.Default.lastIndex].Split(new string[] { " || " }, StringSplitOptions.RemoveEmptyEntries);
         this.lblCard.Text = Settings.Default.lastIndex.ToString() + ". " + string.Join("\r\n", lines);
      }

      private void ShowHalf(bool front) {
         var lines = this.cards[Settings.Default.lastIndex].Split(new string[] { " || " }, StringSplitOptions.RemoveEmptyEntries);
         if (front)
            this.lblCard.Text = Settings.Default.lastIndex.ToString() + ". " + lines[0];
         else {
            this.lblCard.Text = string.Join("\r\n", lines.Skip(1).ToArray());
         }
      }

      string[] cards;
      private void ReadBook() {
         var allText = Properties.Resources.VocaRoot;
         cards = allText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
      }

      private void BtnNext_Click(object sender, EventArgs e) {
         Settings.Default.lastIndex = (Settings.Default.lastIndex + 1) % this.cards.Length;
         this.ShowCard();
      }

      private void BtnPrev_Click(object sender, EventArgs e) {
         Settings.Default.lastIndex = (Settings.Default.lastIndex - 1 + this.cards.Length) % this.cards.Length;
         this.ShowCard();
      }

      private void LblCard_Click(object sender, EventArgs e) {
         if (this.chkHideAnswer.Checked == false)
            return;
         front = !front;
         this.ShowHalf(front);
      }

      private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
         Settings.Default.Save();
      }
   }
}
