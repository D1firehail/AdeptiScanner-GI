namespace AdeptiScanner_GI
{
    partial class EnkaTab
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Title = new System.Windows.Forms.Label();
            this.label_uid = new System.Windows.Forms.Label();
            this.text_UID = new System.Windows.Forms.TextBox();
            this.btn_Fetch = new System.Windows.Forms.Button();
            this.text_remainingCharacters = new System.Windows.Forms.TextBox();
            this.label_remainingChars = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_Title
            // 
            this.label_Title.AutoSize = true;
            this.label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Title.Location = new System.Drawing.Point(3, 9);
            this.label_Title.Name = "label_Title";
            this.label_Title.Size = new System.Drawing.Size(251, 20);
            this.label_Title.TabIndex = 13;
            this.label_Title.Text = "Fetch characters via enka.network";
            // 
            // label_uid
            // 
            this.label_uid.AutoSize = true;
            this.label_uid.Location = new System.Drawing.Point(4, 38);
            this.label_uid.Name = "label_uid";
            this.label_uid.Size = new System.Drawing.Size(29, 13);
            this.label_uid.TabIndex = 14;
            this.label_uid.Text = "UID:";
            // 
            // text_UID
            // 
            this.text_UID.Location = new System.Drawing.Point(39, 35);
            this.text_UID.Name = "text_UID";
            this.text_UID.Size = new System.Drawing.Size(100, 20);
            this.text_UID.TabIndex = 15;
            // 
            // btn_Fetch
            // 
            this.btn_Fetch.Location = new System.Drawing.Point(145, 33);
            this.btn_Fetch.Name = "btn_Fetch";
            this.btn_Fetch.Size = new System.Drawing.Size(75, 23);
            this.btn_Fetch.TabIndex = 16;
            this.btn_Fetch.Text = "Fetch";
            this.btn_Fetch.UseVisualStyleBackColor = true;
            this.btn_Fetch.Click += new System.EventHandler(this.btn_Fetch_Click);
            // 
            // text_remainingCharacters
            // 
            this.text_remainingCharacters.Location = new System.Drawing.Point(7, 93);
            this.text_remainingCharacters.Multiline = true;
            this.text_remainingCharacters.Name = "text_remainingCharacters";
            this.text_remainingCharacters.ReadOnly = true;
            this.text_remainingCharacters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_remainingCharacters.Size = new System.Drawing.Size(327, 132);
            this.text_remainingCharacters.TabIndex = 17;
            this.text_remainingCharacters.Text = "(this updates on successful fetch)";
            // 
            // label_remainingChars
            // 
            this.label_remainingChars.AutoSize = true;
            this.label_remainingChars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_remainingChars.Location = new System.Drawing.Point(4, 77);
            this.label_remainingChars.Name = "label_remainingChars";
            this.label_remainingChars.Size = new System.Drawing.Size(182, 13);
            this.label_remainingChars.TabIndex = 18;
            this.label_remainingChars.Text = "Missing characters known from scan:";
            // 
            // EnkaTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_remainingChars);
            this.Controls.Add(this.text_remainingCharacters);
            this.Controls.Add(this.btn_Fetch);
            this.Controls.Add(this.label_uid);
            this.Controls.Add(this.text_UID);
            this.Controls.Add(this.label_Title);
            this.Name = "EnkaTab";
            this.Size = new System.Drawing.Size(349, 228);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_uid;
        private System.Windows.Forms.Button btn_Fetch;
        internal System.Windows.Forms.TextBox text_UID;
        private System.Windows.Forms.TextBox text_remainingCharacters;
        private System.Windows.Forms.Label label_remainingChars;
    }
}
