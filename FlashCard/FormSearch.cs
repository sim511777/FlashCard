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

            // 메인폼 리스트 박스에서 그룹 리스트 가져옴
            var groupList = this.frmMain.lbxCard.Items.Cast<Tuple<string, IGrouping<string, EfficiencyVoca>>>();
            // 그룹 리스트 에서 각각의 그룹에 속한 카드타이틀과 구룹번호의 튜플 리스트로 평활화
            var cardAndGroups = groupList.SelectMany((group, groupIdx) => group.Item2.Select(card => Tuple.Create(card.GetTitle(), groupIdx)));
            // (카드타이틀, 그룹번호) 에서 카드 타이틀이 검색어를 포함하는것만 필터링
            var items = cardAndGroups.Where(item => item.Item1.Contains(word));
            // 서치 리스트 박스에 추가
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
