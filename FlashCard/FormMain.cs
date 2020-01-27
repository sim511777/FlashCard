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
        // 생성자
        public FormMain() {
            InitializeComponent();
            // UI 추가 초기화
            this.TopMost = true;
            browser.Navigate("about:blank");
            browser.Document.Write(String.Empty);
        }

        private List<Tuple<string,int>> cardAndGroups;
        private void FormMain_Load(object sender, EventArgs e) {
            // 설정 로드
            var settings = Settings.Load();

            // 설정 -> 폼 세팅
            Point pt = settings.windowLocation;
            if (pt.X < 0)
                pt.X = 0;
            if (pt.Y < 0)
                pt.Y = 0;
            settings.windowLocation = pt;
            this.Location = settings.windowLocation;
            this.Size = settings.windowSize;
            this.chkAutoChange.Checked = settings.autoChange;

            // 데크 로드
            this.ReadDeck();
            // 서치 목록 생성
            this.MakeSearchListItem();
            
            // 카드 리스트 선택
            this.CardListChange(settings.lastIndex + (this.chkAutoChange.Checked ? 1 : 0));
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e) {
            // 폼 -> 설정 세팅
            var settings = new Settings();
            settings.windowLocation = this.Location;
            settings.windowSize = this.Size;
            settings.autoChange = this.chkAutoChange.Checked;
            settings.lastIndex = this.lbxCard.SelectedIndex;

            // 설정 저장
            settings.Save();
        }

        private void ReadDeck() {
            var cards = EfficiencyVoca.ReadCards(Resources.EfficiencyVoca);
            var keyGroupPairs = cards.GroupBy(
                keySelector: card => card.DAY_NO + "." + card.PREFIX_GRP,
                resultSelector: (key, group) => Tuple.Create(group.ElementAt(0).GetGroupTitle(), group));
            this.lbxCard.Items.Clear();
            this.lbxCard.Items.AddRange(keyGroupPairs.ToArray());
        }

        private void MakeSearchListItem() {
            // 메인폼 리스트 박스에서 그룹 리스트 가져옴
            var groupList = this.lbxCard.Items.Cast<Tuple<string, IEnumerable<EfficiencyVoca>>>();
            // 그룹 리스트 에서 각각의 그룹에 속한 카드타이틀과 구룹번호의 튜플 리스트로 평활화, 파생어 까지 포함
            this.cardAndGroups = groupList.SelectMany((group, groupIdx) => group.Item2.SelectMany(card => card.GetSearchTitles().Select(title => Tuple.Create(title, groupIdx)))).ToList();
        }

        // 카드 리스트 선택시
        private void lbxCard_SelectedIndexChanged(object sender, EventArgs e) {
            var item = this.lbxCard.SelectedItem as Tuple<string, IEnumerable<EfficiencyVoca>>;
            var group = item.Item2;
            var origin = group.ElementAt(0);
            string color = "black";
            string bgcolor = "white";
            string derivecolor = "black";
            if (origin.VOCABULARY_TAG == "[접두사]") {
                color = "red";
                bgcolor = "#ffcccc";
                derivecolor = "darkblue";
            }
            else if (origin.VOCABULARY_TAG == "[접미사]") {
                color = "blue";
                bgcolor = "#ccccff";
                derivecolor = "darkgreen";
            }
            else if (origin.VOCABULARY_TAG == "[어근]") {
                color = "green";
                bgcolor = "#ccffcc";
                derivecolor = "darkred";
            }
            string dayTag = $"<tr><td align=center colspan=2>DAY{int.Parse(origin.DAY_NO):00}-{int.Parse(origin.PREFIX_GRP)+1}</td></tr>\r\n";
            var html =
                EfficiencyVoca.GetHtmlTop() +
                "<table>\r\n" +
                string.Join("", group.Select(voca => voca.GetHtmlTableRow(color, bgcolor, derivecolor)).ToArray()) +
                "</table>\r\n" + 
                EfficiencyVoca.GetHtmlBottom();
            this.browser.DocumentText = html;
            this.Text = group.ElementAt(0).GetCaptionTitle();
            if (this.historyAdd == true) {
                HistoryAdd(this.lbxCard.SelectedIndex);
            }
        }

        bool historyAdd = true;
        // 카드 리스트 선택
        public void CardListChange(int idx, bool historyAdd = true) {
            if (this.lbxCard.Items.Count <= 0 || idx < 0)
                return;

            idx = Glb.IntRange(idx, 0, this.lbxCard.Items.Count-1);
            this.historyAdd = historyAdd;
            this.lbxCard.SelectedIndex = idx;
            this.historyAdd = true;
            var size = this.lbxCard.Size;
            int cidx = this.lbxCard.IndexFromPoint(size.Width/2, size.Height/2);
            this.lbxCard.TopIndex += idx-cidx;
        }

        // 다음 버튼
        private void BtnNext_Click(object sender, EventArgs e) {
            int cnt = this.lbxCard.Items.Count;
            int idx = (this.lbxCard.SelectedIndex + 1) % cnt;
            this.CardListChange(idx);
        }

        // 이전 버튼
        private void BtnPrev_Click(object sender, EventArgs e) {
            int cnt = this.lbxCard.Items.Count;
            int idx = (this.lbxCard.SelectedIndex - 1 + cnt) % cnt;
            this.CardListChange(idx);
        }

        // 히스토리 기능
        private void ShowHistory() {
            //var histroyTexts = this.cardHistory.Select((cardIdx, idx) => idx == historyPointer ? $"[{cardIdx}]" : $" {cardIdx} ");
            //this.Text = string.Join("-", histroyTexts.ToArray());
        }

        private LinkedList<int> cardHistory = new LinkedList<int>();
        int historyPointer = 0;
        private void HistoryAdd(int index) {
            while (historyPointer < cardHistory.Count-1)
                cardHistory.RemoveLast();
            cardHistory.AddLast(index);
            if (cardHistory.Count > 10000)
                cardHistory.RemoveFirst();
            historyPointer = cardHistory.Count - 1;
            this.ShowHistory();
        }

        private void HistoryUndo() {
            if (historyPointer <= 0)
                return;
            historyPointer--;
            this.CardListChange(cardHistory.ElementAt(historyPointer), false);
            this.ShowHistory();
        }

        private void HistoryRedo() {
            if (historyPointer >= cardHistory.Count-1)
                return;
            historyPointer++;
            this.CardListChange(cardHistory.ElementAt(historyPointer), false);
            this.ShowHistory();
        }
        
        private void btnUndo_Click(object sender, EventArgs e) {
            this.HistoryUndo();
        }

        private void btnRedo_Click(object sender, EventArgs e) {
            this.HistoryRedo();
        }

        // 서치 기능
        private void tbxWord_TextChanged(object sender, EventArgs e) {
            this.lbxResult.Items.Clear();
            var word = this.tbxWord.Text.Trim();
            if (word.Length < 1)
                return;

            // (카드타이틀, 그룹번호) 에서 카드 타이틀이 검색어를 포함하는것만 필터링
            var items = cardAndGroups.Where(tuple =>tuple.Item1.Contains(word)).OrderBy(tuple => tuple.Item1);
            // 서치 리스트 박스에 추가
            this.lbxResult.Items.AddRange(items.ToArray());
        }

        private void lbxResult_SelectedIndexChanged(object sender, EventArgs e) {
            if (this.lbxResult.SelectedItem == null)
                return;
            var selItem = (Tuple<string, int>)this.lbxResult.SelectedItem;
            int vocaIdx = selItem.Item2;
            this.CardListChange(vocaIdx);
        }
    }
}
