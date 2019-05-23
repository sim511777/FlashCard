﻿namespace FlashCard {
   partial class FormMain {
      /// <summary>
      /// 필수 디자이너 변수입니다.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// 사용 중인 모든 리소스를 정리합니다.
      /// </summary>
      /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form 디자이너에서 생성한 코드

      /// <summary>
      /// 디자이너 지원에 필요한 메서드입니다. 
      /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
      /// </summary>
      private void InitializeComponent() {
         this.btnPrev = new System.Windows.Forms.Button();
         this.btnNext = new System.Windows.Forms.Button();
         this.browser = new System.Windows.Forms.WebBrowser();
         this.splitContainer1 = new System.Windows.Forms.SplitContainer();
         this.cbxCard = new System.Windows.Forms.ComboBox();
         this.panel1 = new System.Windows.Forms.Panel();
         this.cbxDeck = new System.Windows.Forms.ComboBox();
         this.panel2 = new System.Windows.Forms.Panel();
         this.Panel13 = new System.Windows.Forms.Panel();
         this.chkAutoChange = new System.Windows.Forms.CheckBox();
         this.btnSearch = new System.Windows.Forms.Button();
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         this.panel1.SuspendLayout();
         this.panel2.SuspendLayout();
         this.Panel13.SuspendLayout();
         this.SuspendLayout();
         // 
         // btnPrev
         // 
         this.btnPrev.Dock = System.Windows.Forms.DockStyle.Fill;
         this.btnPrev.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.btnPrev.Location = new System.Drawing.Point(0, 0);
         this.btnPrev.Name = "btnPrev";
         this.btnPrev.Size = new System.Drawing.Size(73, 211);
         this.btnPrev.TabIndex = 1;
         this.btnPrev.Text = "<=";
         this.btnPrev.UseVisualStyleBackColor = true;
         this.btnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
         // 
         // btnNext
         // 
         this.btnNext.Dock = System.Windows.Forms.DockStyle.Fill;
         this.btnNext.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.btnNext.Location = new System.Drawing.Point(0, 0);
         this.btnNext.Name = "btnNext";
         this.btnNext.Size = new System.Drawing.Size(73, 232);
         this.btnNext.TabIndex = 1;
         this.btnNext.Text = "=>";
         this.btnNext.UseVisualStyleBackColor = true;
         this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
         // 
         // browser
         // 
         this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
         this.browser.Location = new System.Drawing.Point(0, 20);
         this.browser.MinimumSize = new System.Drawing.Size(20, 20);
         this.browser.Name = "browser";
         this.browser.Size = new System.Drawing.Size(842, 478);
         this.browser.TabIndex = 4;
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
         this.splitContainer1.Location = new System.Drawing.Point(0, 51);
         this.splitContainer1.Name = "splitContainer1";
         this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
         // 
         // splitContainer1.Panel1
         // 
         this.splitContainer1.Panel1.Controls.Add(this.btnPrev);
         // 
         // splitContainer1.Panel2
         // 
         this.splitContainer1.Panel2.Controls.Add(this.btnNext);
         this.splitContainer1.Size = new System.Drawing.Size(73, 447);
         this.splitContainer1.SplitterDistance = 211;
         this.splitContainer1.TabIndex = 5;
         // 
         // cbxCard
         // 
         this.cbxCard.Dock = System.Windows.Forms.DockStyle.Fill;
         this.cbxCard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cbxCard.FormattingEnabled = true;
         this.cbxCard.Location = new System.Drawing.Point(201, 0);
         this.cbxCard.Name = "cbxCard";
         this.cbxCard.Size = new System.Drawing.Size(641, 20);
         this.cbxCard.TabIndex = 6;
         this.cbxCard.SelectionChangeCommitted += new System.EventHandler(this.cbxCard_SelectionChangeCommitted);
         // 
         // panel1
         // 
         this.panel1.Controls.Add(this.cbxCard);
         this.panel1.Controls.Add(this.cbxDeck);
         this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
         this.panel1.Location = new System.Drawing.Point(0, 0);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(842, 20);
         this.panel1.TabIndex = 7;
         // 
         // cbxDeck
         // 
         this.cbxDeck.Dock = System.Windows.Forms.DockStyle.Left;
         this.cbxDeck.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
         this.cbxDeck.FormattingEnabled = true;
         this.cbxDeck.Location = new System.Drawing.Point(0, 0);
         this.cbxDeck.Name = "cbxDeck";
         this.cbxDeck.Size = new System.Drawing.Size(201, 20);
         this.cbxDeck.TabIndex = 7;
         this.cbxDeck.SelectionChangeCommitted += new System.EventHandler(this.CbxDeck_SelectionChangeCommitted);
         // 
         // panel2
         // 
         this.panel2.Controls.Add(this.browser);
         this.panel2.Controls.Add(this.panel1);
         this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
         this.panel2.Location = new System.Drawing.Point(0, 0);
         this.panel2.Name = "panel2";
         this.panel2.Size = new System.Drawing.Size(842, 498);
         this.panel2.TabIndex = 8;
         // 
         // Panel13
         // 
         this.Panel13.Controls.Add(this.splitContainer1);
         this.Panel13.Controls.Add(this.btnSearch);
         this.Panel13.Controls.Add(this.chkAutoChange);
         this.Panel13.Dock = System.Windows.Forms.DockStyle.Right;
         this.Panel13.Location = new System.Drawing.Point(842, 0);
         this.Panel13.Name = "Panel13";
         this.Panel13.Size = new System.Drawing.Size(73, 498);
         this.Panel13.TabIndex = 9;
         // 
         // chkAutoChange
         // 
         this.chkAutoChange.AutoSize = true;
         this.chkAutoChange.Dock = System.Windows.Forms.DockStyle.Top;
         this.chkAutoChange.Location = new System.Drawing.Point(0, 0);
         this.chkAutoChange.Name = "chkAutoChange";
         this.chkAutoChange.Size = new System.Drawing.Size(73, 28);
         this.chkAutoChange.TabIndex = 6;
         this.chkAutoChange.Text = "Auto\r\nChange";
         this.chkAutoChange.UseVisualStyleBackColor = true;
         this.chkAutoChange.Click += new System.EventHandler(this.chkAutoChange_Click);
         // 
         // btnSearch
         // 
         this.btnSearch.Dock = System.Windows.Forms.DockStyle.Top;
         this.btnSearch.Location = new System.Drawing.Point(0, 28);
         this.btnSearch.Name = "btnSearch";
         this.btnSearch.Size = new System.Drawing.Size(73, 23);
         this.btnSearch.TabIndex = 8;
         this.btnSearch.Text = "Search";
         this.btnSearch.UseVisualStyleBackColor = true;
         this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
         // 
         // FormMain
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(915, 498);
         this.Controls.Add(this.panel2);
         this.Controls.Add(this.Panel13);
         this.Name = "FormMain";
         this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
         this.Text = "Flash Card";
         this.Move += new System.EventHandler(this.FormMain_Move);
         this.Resize += new System.EventHandler(this.FormMain_Resize);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.panel1.ResumeLayout(false);
         this.panel2.ResumeLayout(false);
         this.Panel13.ResumeLayout(false);
         this.Panel13.PerformLayout();
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnPrev;
      private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ComboBox cbxCard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbxDeck;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel Panel13;
        private System.Windows.Forms.CheckBox chkAutoChange;
        private System.Windows.Forms.Button btnSearch;
    }
}

