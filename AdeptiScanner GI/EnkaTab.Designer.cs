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
            label_Title = new System.Windows.Forms.Label();
            label_uid = new System.Windows.Forms.Label();
            text_UID = new System.Windows.Forms.TextBox();
            btn_Fetch = new System.Windows.Forms.Button();
            text_remainingCharacters = new System.Windows.Forms.TextBox();
            label_remainingChars = new System.Windows.Forms.Label();
            label_cooldown = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // label_Title
            // 
            label_Title.AutoSize = true;
            label_Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            label_Title.Location = new System.Drawing.Point(4, 10);
            label_Title.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_Title.Name = "label_Title";
            label_Title.Size = new System.Drawing.Size(251, 20);
            label_Title.TabIndex = 13;
            label_Title.Text = "Fetch characters via enka.network";
            // 
            // label_uid
            // 
            label_uid.AutoSize = true;
            label_uid.Location = new System.Drawing.Point(5, 44);
            label_uid.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_uid.Name = "label_uid";
            label_uid.Size = new System.Drawing.Size(29, 15);
            label_uid.TabIndex = 14;
            label_uid.Text = "UID:";
            // 
            // text_UID
            // 
            text_UID.Location = new System.Drawing.Point(46, 40);
            text_UID.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            text_UID.Name = "text_UID";
            text_UID.Size = new System.Drawing.Size(116, 23);
            text_UID.TabIndex = 15;
            // 
            // btn_Fetch
            // 
            btn_Fetch.Location = new System.Drawing.Point(169, 38);
            btn_Fetch.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_Fetch.Name = "btn_Fetch";
            btn_Fetch.Size = new System.Drawing.Size(88, 27);
            btn_Fetch.TabIndex = 16;
            btn_Fetch.Text = "Fetch";
            btn_Fetch.UseVisualStyleBackColor = true;
            btn_Fetch.Click += btn_Fetch_Click;
            // 
            // text_remainingCharacters
            // 
            text_remainingCharacters.Location = new System.Drawing.Point(8, 107);
            text_remainingCharacters.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            text_remainingCharacters.Multiline = true;
            text_remainingCharacters.Name = "text_remainingCharacters";
            text_remainingCharacters.ReadOnly = true;
            text_remainingCharacters.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            text_remainingCharacters.Size = new System.Drawing.Size(381, 152);
            text_remainingCharacters.TabIndex = 17;
            text_remainingCharacters.Text = "(this updates on successful fetch)";
            // 
            // label_remainingChars
            // 
            label_remainingChars.AutoSize = true;
            label_remainingChars.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            label_remainingChars.Location = new System.Drawing.Point(5, 89);
            label_remainingChars.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_remainingChars.Name = "label_remainingChars";
            label_remainingChars.Size = new System.Drawing.Size(182, 13);
            label_remainingChars.TabIndex = 18;
            label_remainingChars.Text = "Missing characters known from scan:";
            // 
            // label_cooldown
            // 
            label_cooldown.AutoSize = true;
            label_cooldown.Location = new System.Drawing.Point(265, 44);
            label_cooldown.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_cooldown.Name = "label_cooldown";
            label_cooldown.Size = new System.Drawing.Size(87, 15);
            label_cooldown.TabIndex = 19;
            label_cooldown.Text = "Cooldown: XXs";
            // 
            // EnkaTab
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(label_cooldown);
            Controls.Add(label_remainingChars);
            Controls.Add(text_remainingCharacters);
            Controls.Add(btn_Fetch);
            Controls.Add(label_uid);
            Controls.Add(text_UID);
            Controls.Add(label_Title);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "EnkaTab";
            Size = new System.Drawing.Size(407, 263);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label_Title;
        private System.Windows.Forms.Label label_uid;
        private System.Windows.Forms.Button btn_Fetch;
        internal System.Windows.Forms.TextBox text_UID;
        private System.Windows.Forms.TextBox text_remainingCharacters;
        private System.Windows.Forms.Label label_remainingChars;
        private System.Windows.Forms.Label label_cooldown;
    }
}
