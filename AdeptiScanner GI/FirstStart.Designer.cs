
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
            label_AutoUpdateQuestion = new System.Windows.Forms.Label();
            button_AllUpdates = new System.Windows.Forms.Button();
            button_DataOnly = new System.Windows.Forms.Button();
            button_VersionOnly = new System.Windows.Forms.Button();
            button_NoUpdates = new System.Windows.Forms.Button();
            label_UpdateChangeLater = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // label_AutoUpdateQuestion
            // 
            label_AutoUpdateQuestion.AutoSize = true;
            label_AutoUpdateQuestion.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            label_AutoUpdateQuestion.Location = new System.Drawing.Point(14, 10);
            label_AutoUpdateQuestion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_AutoUpdateQuestion.Name = "label_AutoUpdateQuestion";
            label_AutoUpdateQuestion.Size = new System.Drawing.Size(479, 28);
            label_AutoUpdateQuestion.TabIndex = 0;
            label_AutoUpdateQuestion.Text = "Do you want to check for updates automatically?";
            // 
            // button_AllUpdates
            // 
            button_AllUpdates.Location = new System.Drawing.Point(14, 82);
            button_AllUpdates.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_AllUpdates.Name = "button_AllUpdates";
            button_AllUpdates.Size = new System.Drawing.Size(560, 27);
            button_AllUpdates.TabIndex = 1;
            button_AllUpdates.Text = "Yes, version and game data";
            button_AllUpdates.UseVisualStyleBackColor = true;
            button_AllUpdates.Click += button_AllUpdates_Click;
            // 
            // button_DataOnly
            // 
            button_DataOnly.Location = new System.Drawing.Point(14, 115);
            button_DataOnly.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_DataOnly.Name = "button_DataOnly";
            button_DataOnly.Size = new System.Drawing.Size(560, 27);
            button_DataOnly.TabIndex = 2;
            button_DataOnly.Text = "Game data only";
            button_DataOnly.UseVisualStyleBackColor = true;
            button_DataOnly.Click += button_DataOnly_Click;
            // 
            // button_VersionOnly
            // 
            button_VersionOnly.Location = new System.Drawing.Point(14, 149);
            button_VersionOnly.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_VersionOnly.Name = "button_VersionOnly";
            button_VersionOnly.Size = new System.Drawing.Size(560, 27);
            button_VersionOnly.TabIndex = 3;
            button_VersionOnly.Text = "Scanner version only";
            button_VersionOnly.UseVisualStyleBackColor = true;
            button_VersionOnly.Click += button_VersionOnly_Click;
            // 
            // button_NoUpdates
            // 
            button_NoUpdates.Location = new System.Drawing.Point(14, 182);
            button_NoUpdates.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_NoUpdates.Name = "button_NoUpdates";
            button_NoUpdates.Size = new System.Drawing.Size(560, 27);
            button_NoUpdates.TabIndex = 4;
            button_NoUpdates.Text = "No, do not check for updates";
            button_NoUpdates.UseVisualStyleBackColor = true;
            button_NoUpdates.Click += button_NoUpdates_Click;
            // 
            // label_UpdateChangeLater
            // 
            label_UpdateChangeLater.AutoSize = true;
            label_UpdateChangeLater.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            label_UpdateChangeLater.Location = new System.Drawing.Point(106, 40);
            label_UpdateChangeLater.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_UpdateChangeLater.Name = "label_UpdateChangeLater";
            label_UpdateChangeLater.Size = new System.Drawing.Size(312, 21);
            label_UpdateChangeLater.TabIndex = 5;
            label_UpdateChangeLater.Text = "(This can be changed later under Advanced)";
            // 
            // FirstStart
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(588, 417);
            Controls.Add(label_UpdateChangeLater);
            Controls.Add(button_NoUpdates);
            Controls.Add(button_VersionOnly);
            Controls.Add(button_DataOnly);
            Controls.Add(button_AllUpdates);
            Controls.Add(label_AutoUpdateQuestion);
            Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "FirstStart";
            Text = "AdeptiScanner First Start";
            ResumeLayout(false);
            PerformLayout();
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