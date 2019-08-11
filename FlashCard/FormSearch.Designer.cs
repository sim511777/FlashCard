namespace FlashCard {
    partial class FormSearch {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.tbxWord = new System.Windows.Forms.TextBox();
            this.lbxResult = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // tbxWord
            // 
            this.tbxWord.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbxWord.Location = new System.Drawing.Point(0, 0);
            this.tbxWord.Name = "tbxWord";
            this.tbxWord.Size = new System.Drawing.Size(406, 21);
            this.tbxWord.TabIndex = 0;
            this.tbxWord.TextChanged += new System.EventHandler(this.tbxWord_TextChanged);
            // 
            // lbxResult
            // 
            this.lbxResult.DisplayMember = "Item1";
            this.lbxResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxResult.FormattingEnabled = true;
            this.lbxResult.ItemHeight = 12;
            this.lbxResult.Location = new System.Drawing.Point(0, 21);
            this.lbxResult.Name = "lbxResult";
            this.lbxResult.Size = new System.Drawing.Size(406, 391);
            this.lbxResult.TabIndex = 1;
            this.lbxResult.ValueMember = "Item2";
            this.lbxResult.SelectedIndexChanged += new System.EventHandler(this.lbxResult_SelectedIndexChanged);
            // 
            // FormSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(406, 412);
            this.Controls.Add(this.lbxResult);
            this.Controls.Add(this.tbxWord);
            this.Name = "FormSearch";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormSearch_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxWord;
        private System.Windows.Forms.ListBox lbxResult;
    }
}