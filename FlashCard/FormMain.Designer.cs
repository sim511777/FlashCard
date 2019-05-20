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
         this.lblCard = new System.Windows.Forms.Label();
         this.chkHideAnswer = new System.Windows.Forms.CheckBox();
         this.SuspendLayout();
         // 
         // btnPrev
         // 
         this.btnPrev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnPrev.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.btnPrev.Location = new System.Drawing.Point(569, 189);
         this.btnPrev.Name = "btnPrev";
         this.btnPrev.Size = new System.Drawing.Size(107, 72);
         this.btnPrev.TabIndex = 1;
         this.btnPrev.Text = "<=";
         this.btnPrev.UseVisualStyleBackColor = true;
         this.btnPrev.Click += new System.EventHandler(this.BtnPrev_Click);
         // 
         // btnNext
         // 
         this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnNext.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
         this.btnNext.Location = new System.Drawing.Point(569, 268);
         this.btnNext.Name = "btnNext";
         this.btnNext.Size = new System.Drawing.Size(107, 72);
         this.btnNext.TabIndex = 1;
         this.btnNext.Text = "=>";
         this.btnNext.UseVisualStyleBackColor = true;
         this.btnNext.Click += new System.EventHandler(this.BtnNext_Click);
         // 
         // lblCard
         // 
         this.lblCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
         this.lblCard.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
         this.lblCard.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
         this.lblCard.Location = new System.Drawing.Point(12, 9);
         this.lblCard.Name = "lblCard";
         this.lblCard.Size = new System.Drawing.Size(551, 331);
         this.lblCard.TabIndex = 2;
         this.lblCard.Text = "Flash Card";
         this.lblCard.Click += new System.EventHandler(this.LblCard_Click);
         // 
         // chkHideAnswer
         // 
         this.chkHideAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
         this.chkHideAnswer.AutoSize = true;
         this.chkHideAnswer.Location = new System.Drawing.Point(580, 12);
         this.chkHideAnswer.Name = "chkHideAnswer";
         this.chkHideAnswer.Size = new System.Drawing.Size(96, 16);
         this.chkHideAnswer.TabIndex = 3;
         this.chkHideAnswer.Text = "Hide Answer";
         this.chkHideAnswer.UseVisualStyleBackColor = true;
         // 
         // FormMain
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(688, 349);
         this.Controls.Add(this.chkHideAnswer);
         this.Controls.Add(this.lblCard);
         this.Controls.Add(this.btnNext);
         this.Controls.Add(this.btnPrev);
         this.Name = "FormMain";
         this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
         this.Text = "Flash Card";
         this.Move += new System.EventHandler(this.FormMain_Move);
         this.Resize += new System.EventHandler(this.FormMain_Resize);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button btnPrev;
      private System.Windows.Forms.Button btnNext;
      private System.Windows.Forms.Label lblCard;
      private System.Windows.Forms.CheckBox chkHideAnswer;
   }
}

