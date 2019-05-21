namespace FlashCard {
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
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
         this.splitContainer1.Panel1.SuspendLayout();
         this.splitContainer1.Panel2.SuspendLayout();
         this.splitContainer1.SuspendLayout();
         this.SuspendLayout();
         // 
         // btnPrev
         // 
         this.btnPrev.Dock = System.Windows.Forms.DockStyle.Fill;
         this.btnPrev.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.btnPrev.Location = new System.Drawing.Point(0, 0);
         this.btnPrev.Name = "btnPrev";
         this.btnPrev.Size = new System.Drawing.Size(72, 154);
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
         this.btnNext.Size = new System.Drawing.Size(72, 166);
         this.btnNext.TabIndex = 1;
         this.btnNext.Text = "=>";
         this.btnNext.UseVisualStyleBackColor = true;
         this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
         // 
         // browser
         // 
         this.browser.Dock = System.Windows.Forms.DockStyle.Fill;
         this.browser.Location = new System.Drawing.Point(0, 0);
         this.browser.MinimumSize = new System.Drawing.Size(20, 20);
         this.browser.Name = "browser";
         this.browser.Size = new System.Drawing.Size(732, 324);
         this.browser.TabIndex = 4;
         // 
         // splitContainer1
         // 
         this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Right;
         this.splitContainer1.Location = new System.Drawing.Point(732, 0);
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
         this.splitContainer1.Size = new System.Drawing.Size(72, 324);
         this.splitContainer1.SplitterDistance = 154;
         this.splitContainer1.TabIndex = 5;
         // 
         // FormMain
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(804, 324);
         this.Controls.Add(this.browser);
         this.Controls.Add(this.splitContainer1);
         this.Name = "FormMain";
         this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
         this.Text = "Flash Card";
         this.Move += new System.EventHandler(this.FormMain_Move);
         this.Resize += new System.EventHandler(this.FormMain_Resize);
         this.splitContainer1.Panel1.ResumeLayout(false);
         this.splitContainer1.Panel2.ResumeLayout(false);
         ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
         this.splitContainer1.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btnPrev;
      private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}

