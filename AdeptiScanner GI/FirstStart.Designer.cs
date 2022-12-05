
namespace AdeptiScanner_GI
{
    partial class FirstStart
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_AutoUpdateQuestion = new System.Windows.Forms.Label();
            this.button_AllUpdates = new System.Windows.Forms.Button();
            this.button_DataOnly = new System.Windows.Forms.Button();
            this.button_VersionOnly = new System.Windows.Forms.Button();
            this.button_NoUpdates = new System.Windows.Forms.Button();
            this.label_UpdateChangeLater = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label_AutoUpdateQuestion
            // 
            this.label_AutoUpdateQuestion.AutoSize = true;
            this.label_AutoUpdateQuestion.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label_AutoUpdateQuestion.Location = new System.Drawing.Point(12, 9);
            this.label_AutoUpdateQuestion.Name = "label_AutoUpdateQuestion";
            this.label_AutoUpdateQuestion.Size = new System.Drawing.Size(480, 26);
            this.label_AutoUpdateQuestion.TabIndex = 0;
            this.label_AutoUpdateQuestion.Text = "Do you want to check for updates automatically?";
            // 
            // button_AllUpdates
            // 
            this.button_AllUpdates.Location = new System.Drawing.Point(12, 71);
            this.button_AllUpdates.Name = "button_AllUpdates";
            this.button_AllUpdates.Size = new System.Drawing.Size(480, 23);
            this.button_AllUpdates.TabIndex = 1;
            this.button_AllUpdates.Text = "Yes, version and game data";
            this.button_AllUpdates.UseVisualStyleBackColor = true;
            this.button_AllUpdates.Click += new System.EventHandler(this.button_AllUpdates_Click);
            // 
            // button_DataOnly
            // 
            this.button_DataOnly.Location = new System.Drawing.Point(12, 100);
            this.button_DataOnly.Name = "button_DataOnly";
            this.button_DataOnly.Size = new System.Drawing.Size(480, 23);
            this.button_DataOnly.TabIndex = 2;
            this.button_DataOnly.Text = "Game data only";
            this.button_DataOnly.UseVisualStyleBackColor = true;
            this.button_DataOnly.Click += new System.EventHandler(this.button_DataOnly_Click);
            // 
            // button_VersionOnly
            // 
            this.button_VersionOnly.Location = new System.Drawing.Point(12, 129);
            this.button_VersionOnly.Name = "button_VersionOnly";
            this.button_VersionOnly.Size = new System.Drawing.Size(480, 23);
            this.button_VersionOnly.TabIndex = 3;
            this.button_VersionOnly.Text = "Scanner version only";
            this.button_VersionOnly.UseVisualStyleBackColor = true;
            this.button_VersionOnly.Click += new System.EventHandler(this.button_VersionOnly_Click);
            // 
            // button_NoUpdates
            // 
            this.button_NoUpdates.Location = new System.Drawing.Point(12, 158);
            this.button_NoUpdates.Name = "button_NoUpdates";
            this.button_NoUpdates.Size = new System.Drawing.Size(480, 23);
            this.button_NoUpdates.TabIndex = 4;
            this.button_NoUpdates.Text = "No, do not check for updates";
            this.button_NoUpdates.UseVisualStyleBackColor = true;
            this.button_NoUpdates.Click += new System.EventHandler(this.button_NoUpdates_Click);
            // 
            // label_UpdateChangeLater
            // 
            this.label_UpdateChangeLater.AutoSize = true;
            this.label_UpdateChangeLater.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label_UpdateChangeLater.Location = new System.Drawing.Point(91, 35);
            this.label_UpdateChangeLater.Name = "label_UpdateChangeLater";
            this.label_UpdateChangeLater.Size = new System.Drawing.Size(321, 20);
            this.label_UpdateChangeLater.TabIndex = 5;
            this.label_UpdateChangeLater.Text = "(This can be changed later under Advanced)";
            // 
            // FirstStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 361);
            this.Controls.Add(this.label_UpdateChangeLater);
            this.Controls.Add(this.button_NoUpdates);
            this.Controls.Add(this.button_VersionOnly);
            this.Controls.Add(this.button_DataOnly);
            this.Controls.Add(this.button_AllUpdates);
            this.Controls.Add(this.label_AutoUpdateQuestion);
            this.Name = "FirstStart";
            this.Text = "AdeptiScanner First Start";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_AutoUpdateQuestion;
        private System.Windows.Forms.Button button_AllUpdates;
        private System.Windows.Forms.Button button_DataOnly;
        private System.Windows.Forms.Button button_VersionOnly;
        private System.Windows.Forms.Button button_NoUpdates;
        private System.Windows.Forms.Label label_UpdateChangeLater;
    }
}