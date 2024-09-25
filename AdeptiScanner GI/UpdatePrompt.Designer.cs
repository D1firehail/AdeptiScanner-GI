
namespace AdeptiScanner_GI
{
    partial class UpdatePrompt
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
            panel_versionUpdate = new System.Windows.Forms.Panel();
            button_ignoreVersionPermanent = new System.Windows.Forms.Button();
            button_ignoreVersionTemporary = new System.Windows.Forms.Button();
            button_OpenRelease = new System.Windows.Forms.Button();
            textBox_updateVersionBody = new System.Windows.Forms.TextBox();
            label_versionUpdate = new System.Windows.Forms.Label();
            panel_dataUpdate = new System.Windows.Forms.Panel();
            button_ignoreDataPermanent = new System.Windows.Forms.Button();
            button_ignoreDataTemporary = new System.Windows.Forms.Button();
            button_updateData = new System.Windows.Forms.Button();
            label_dataUpdate = new System.Windows.Forms.Label();
            panel_versionUpdate.SuspendLayout();
            panel_dataUpdate.SuspendLayout();
            SuspendLayout();
            // 
            // panel_versionUpdate
            // 
            panel_versionUpdate.Controls.Add(button_ignoreVersionPermanent);
            panel_versionUpdate.Controls.Add(button_ignoreVersionTemporary);
            panel_versionUpdate.Controls.Add(button_OpenRelease);
            panel_versionUpdate.Controls.Add(textBox_updateVersionBody);
            panel_versionUpdate.Controls.Add(label_versionUpdate);
            panel_versionUpdate.Location = new System.Drawing.Point(14, 14);
            panel_versionUpdate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel_versionUpdate.Name = "panel_versionUpdate";
            panel_versionUpdate.Size = new System.Drawing.Size(905, 359);
            panel_versionUpdate.TabIndex = 0;
            // 
            // button_ignoreVersionPermanent
            // 
            button_ignoreVersionPermanent.Location = new System.Drawing.Point(410, 323);
            button_ignoreVersionPermanent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_ignoreVersionPermanent.Name = "button_ignoreVersionPermanent";
            button_ignoreVersionPermanent.Size = new System.Drawing.Size(174, 27);
            button_ignoreVersionPermanent.TabIndex = 3;
            button_ignoreVersionPermanent.Text = "Skip update";
            button_ignoreVersionPermanent.UseVisualStyleBackColor = true;
            button_ignoreVersionPermanent.Click += button_ignoreVersionPermanent_Click;
            // 
            // button_ignoreVersionTemporary
            // 
            button_ignoreVersionTemporary.Location = new System.Drawing.Point(229, 323);
            button_ignoreVersionTemporary.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_ignoreVersionTemporary.Name = "button_ignoreVersionTemporary";
            button_ignoreVersionTemporary.Size = new System.Drawing.Size(174, 27);
            button_ignoreVersionTemporary.TabIndex = 1;
            button_ignoreVersionTemporary.Text = "Ignore for now";
            button_ignoreVersionTemporary.UseVisualStyleBackColor = true;
            button_ignoreVersionTemporary.Click += button_ignoreVersionTemporary_Click;
            // 
            // button_OpenRelease
            // 
            button_OpenRelease.Location = new System.Drawing.Point(19, 323);
            button_OpenRelease.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_OpenRelease.Name = "button_OpenRelease";
            button_OpenRelease.Size = new System.Drawing.Size(203, 27);
            button_OpenRelease.TabIndex = 2;
            button_OpenRelease.Text = "Open on github";
            button_OpenRelease.UseVisualStyleBackColor = true;
            button_OpenRelease.Click += button_OpenRelease_Click;
            // 
            // textBox_updateVersionBody
            // 
            textBox_updateVersionBody.Location = new System.Drawing.Point(18, 37);
            textBox_updateVersionBody.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            textBox_updateVersionBody.Multiline = true;
            textBox_updateVersionBody.Name = "textBox_updateVersionBody";
            textBox_updateVersionBody.ReadOnly = true;
            textBox_updateVersionBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBox_updateVersionBody.Size = new System.Drawing.Size(884, 279);
            textBox_updateVersionBody.TabIndex = 1;
            // 
            // label_versionUpdate
            // 
            label_versionUpdate.AutoSize = true;
            label_versionUpdate.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            label_versionUpdate.Location = new System.Drawing.Point(14, 10);
            label_versionUpdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_versionUpdate.Name = "label_versionUpdate";
            label_versionUpdate.Size = new System.Drawing.Size(228, 21);
            label_versionUpdate.TabIndex = 1;
            label_versionUpdate.Text = "New Scanner Update available: ";
            // 
            // panel_dataUpdate
            // 
            panel_dataUpdate.Controls.Add(button_ignoreDataPermanent);
            panel_dataUpdate.Controls.Add(button_ignoreDataTemporary);
            panel_dataUpdate.Controls.Add(button_updateData);
            panel_dataUpdate.Controls.Add(label_dataUpdate);
            panel_dataUpdate.Location = new System.Drawing.Point(14, 380);
            panel_dataUpdate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            panel_dataUpdate.Name = "panel_dataUpdate";
            panel_dataUpdate.Size = new System.Drawing.Size(905, 110);
            panel_dataUpdate.TabIndex = 1;
            // 
            // button_ignoreDataPermanent
            // 
            button_ignoreDataPermanent.Location = new System.Drawing.Point(410, 57);
            button_ignoreDataPermanent.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_ignoreDataPermanent.Name = "button_ignoreDataPermanent";
            button_ignoreDataPermanent.Size = new System.Drawing.Size(174, 27);
            button_ignoreDataPermanent.TabIndex = 3;
            button_ignoreDataPermanent.Text = "Skip update";
            button_ignoreDataPermanent.UseVisualStyleBackColor = true;
            button_ignoreDataPermanent.Click += button_ignoreDataPermanent_Click;
            // 
            // button_ignoreDataTemporary
            // 
            button_ignoreDataTemporary.Location = new System.Drawing.Point(229, 57);
            button_ignoreDataTemporary.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_ignoreDataTemporary.Name = "button_ignoreDataTemporary";
            button_ignoreDataTemporary.Size = new System.Drawing.Size(174, 27);
            button_ignoreDataTemporary.TabIndex = 1;
            button_ignoreDataTemporary.Text = "Ignore for now";
            button_ignoreDataTemporary.UseVisualStyleBackColor = true;
            button_ignoreDataTemporary.Click += button_ignoreDataTemporary_Click;
            // 
            // button_updateData
            // 
            button_updateData.Location = new System.Drawing.Point(19, 57);
            button_updateData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_updateData.Name = "button_updateData";
            button_updateData.Size = new System.Drawing.Size(203, 27);
            button_updateData.TabIndex = 2;
            button_updateData.Text = "Install and restart";
            button_updateData.UseVisualStyleBackColor = true;
            button_updateData.Click += button_updateData_Click;
            // 
            // label_dataUpdate
            // 
            label_dataUpdate.AutoSize = true;
            label_dataUpdate.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            label_dataUpdate.Location = new System.Drawing.Point(14, 10);
            label_dataUpdate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_dataUpdate.Name = "label_dataUpdate";
            label_dataUpdate.Size = new System.Drawing.Size(204, 21);
            label_dataUpdate.TabIndex = 1;
            label_dataUpdate.Text = "New Data Update available: ";
            // 
            // UpdatePrompt
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            ClientSize = new System.Drawing.Size(933, 519);
            Controls.Add(panel_dataUpdate);
            Controls.Add(panel_versionUpdate);
            Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "UpdatePrompt";
            Text = "UpdatePrompt";
            panel_versionUpdate.ResumeLayout(false);
            panel_versionUpdate.PerformLayout();
            panel_dataUpdate.ResumeLayout(false);
            panel_dataUpdate.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel_versionUpdate;
        private System.Windows.Forms.Label label_versionUpdate;
        private System.Windows.Forms.TextBox textBox_updateVersionBody;
        private System.Windows.Forms.Button button_OpenRelease;
        private System.Windows.Forms.Button button_ignoreVersionTemporary;
        private System.Windows.Forms.Button button_ignoreVersionPermanent;
        private System.Windows.Forms.Panel panel_dataUpdate;
        private System.Windows.Forms.Button button_ignoreDataPermanent;
        private System.Windows.Forms.Button button_ignoreDataTemporary;
        private System.Windows.Forms.Button button_updateData;
        private System.Windows.Forms.Label label_dataUpdate;
    }
}