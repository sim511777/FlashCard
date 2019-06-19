using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlashCard {
    public partial class FormSearch : Form {
        FormMain frmMain;
        public FormSearch(FormMain frmMain) {
            this.frmMain = frmMain;
            InitializeComponent();
        }

        private void FormSearch_FormClosing(object sender, FormClosingEventArgs e) {
            this.frmMain.frmSearch = null;
        }

        private void tbxWord_TextChanged(object sender, EventArgs e) {
            this.lbxResult.Items.Clear();
            var word = this.tbxWord.Text;
            if (word.Length < 1)
                return;
            var items = this.frmMain.cards.Select((voca, idx) => Tuple.Create(voca.GetTitle(), idx)).Where(item => item.Item1.Contains(word));
            this.lbxResult.Items.AddRange(items.ToArray());
        }

        private void lbxResult_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.lbxResult.SelectedItem == null)
                return;
            var selItem = (Tuple<string, int>)this.lbxResult.SelectedItem;
            int vocaIdx = selItem.Item2;
            this.frmMain.CardListChange(vocaIdx);
        }
    }
}
