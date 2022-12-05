
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
            this.panel_versionUpdate = new System.Windows.Forms.Panel();
            this.button_ignoreVersionPermanent = new System.Windows.Forms.Button();
            this.button_ignoreVersionTemporary = new System.Windows.Forms.Button();
            this.button_OpenRelease = new System.Windows.Forms.Button();
            this.textBox_updateVersionBody = new System.Windows.Forms.TextBox();
            this.label_versionUpdate = new System.Windows.Forms.Label();
            this.panel_dataUpdate = new System.Windows.Forms.Panel();
            this.button_ignoreDataPermanent = new System.Windows.Forms.Button();
            this.button_ignoreDataTemporary = new System.Windows.Forms.Button();
            this.button_updateData = new System.Windows.Forms.Button();
            this.label_dataUpdate = new System.Windows.Forms.Label();
            this.panel_versionUpdate.SuspendLayout();
            this.panel_dataUpdate.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_versionUpdate
            // 
            this.panel_versionUpdate.Controls.Add(this.button_ignoreVersionPermanent);
            this.panel_versionUpdate.Controls.Add(this.button_ignoreVersionTemporary);
            this.panel_versionUpdate.Controls.Add(this.button_OpenRelease);
            this.panel_versionUpdate.Controls.Add(this.textBox_updateVersionBody);
            this.panel_versionUpdate.Controls.Add(this.label_versionUpdate);
            this.panel_versionUpdate.Location = new System.Drawing.Point(12, 12);
            this.panel_versionUpdate.Name = "panel_versionUpdate";
            this.panel_versionUpdate.Size = new System.Drawing.Size(776, 311);
            this.panel_versionUpdate.TabIndex = 0;
            // 
            // button_ignoreVersionPermanent
            // 
            this.button_ignoreVersionPermanent.Location = new System.Drawing.Point(351, 280);
            this.button_ignoreVersionPermanent.Name = "button_ignoreVersionPermanent";
            this.button_ignoreVersionPermanent.Size = new System.Drawing.Size(149, 23);
            this.button_ignoreVersionPermanent.TabIndex = 3;
            this.button_ignoreVersionPermanent.Text = "Skip update";
            this.button_ignoreVersionPermanent.UseVisualStyleBackColor = true;
            this.button_ignoreVersionPermanent.Click += new System.EventHandler(this.button_ignoreVersionPermanent_Click);
            // 
            // button_ignoreVersionTemporary
            // 
            this.button_ignoreVersionTemporary.Location = new System.Drawing.Point(196, 280);
            this.button_ignoreVersionTemporary.Name = "button_ignoreVersionTemporary";
            this.button_ignoreVersionTemporary.Size = new System.Drawing.Size(149, 23);
            this.button_ignoreVersionTemporary.TabIndex = 1;
            this.button_ignoreVersionTemporary.Text = "Ignore for now";
            this.button_ignoreVersionTemporary.UseVisualStyleBackColor = true;
            this.button_ignoreVersionTemporary.Click += new System.EventHandler(this.button_ignoreVersionTemporary_Click);
            // 
            // button_OpenRelease
            // 
            this.button_OpenRelease.Location = new System.Drawing.Point(16, 280);
            this.button_OpenRelease.Name = "button_OpenRelease";
            this.button_OpenRelease.Size = new System.Drawing.Size(174, 23);
            this.button_OpenRelease.TabIndex = 2;
            this.button_OpenRelease.Text = "Open on github";
            this.button_OpenRelease.UseVisualStyleBackColor = true;
            this.button_OpenRelease.Click += new System.EventHandler(this.button_OpenRelease_Click);
            // 
            // textBox_updateVersionBody
            // 
            this.textBox_updateVersionBody.Location = new System.Drawing.Point(15, 32);
            this.textBox_updateVersionBody.Multiline = true;
            this.textBox_updateVersionBody.Name = "textBox_updateVersionBody";
            this.textBox_updateVersionBody.ReadOnly = true;
            this.textBox_updateVersionBody.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_updateVersionBody.Size = new System.Drawing.Size(758, 242);
            this.textBox_updateVersionBody.TabIndex = 1;
            // 
            // label_versionUpdate
            // 
            this.label_versionUpdate.AutoSize = true;
            this.label_versionUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label_versionUpdate.Location = new System.Drawing.Point(12, 9);
            this.label_versionUpdate.Name = "label_versionUpdate";
            this.label_versionUpdate.Size = new System.Drawing.Size(234, 20);
            this.label_versionUpdate.TabIndex = 1;
            this.label_versionUpdate.Text = "New Scanner Update available: ";
            // 
            // panel_dataUpdate
            // 
            this.panel_dataUpdate.Controls.Add(this.button_ignoreDataPermanent);
            this.panel_dataUpdate.Controls.Add(this.button_ignoreDataTemporary);
            this.panel_dataUpdate.Controls.Add(this.button_updateData);
            this.panel_dataUpdate.Controls.Add(this.label_dataUpdate);
            this.panel_dataUpdate.Location = new System.Drawing.Point(12, 329);
            this.panel_dataUpdate.Name = "panel_dataUpdate";
            this.panel_dataUpdate.Size = new System.Drawing.Size(776, 95);
            this.panel_dataUpdate.TabIndex = 1;
            // 
            // button_ignoreDataPermanent
            // 
            this.button_ignoreDataPermanent.Location = new System.Drawing.Point(351, 49);
            this.button_ignoreDataPermanent.Name = "button_ignoreDataPermanent";
            this.button_ignoreDataPermanent.Size = new System.Drawing.Size(149, 23);
            this.button_ignoreDataPermanent.TabIndex = 3;
            this.button_ignoreDataPermanent.Text = "Skip update";
            this.button_ignoreDataPermanent.UseVisualStyleBackColor = true;
            this.button_ignoreDataPermanent.Click += new System.EventHandler(this.button_ignoreDataPermanent_Click);
            // 
            // button_ignoreDataTemporary
            // 
            this.button_ignoreDataTemporary.Location = new System.Drawing.Point(196, 49);
            this.button_ignoreDataTemporary.Name = "button_ignoreDataTemporary";
            this.button_ignoreDataTemporary.Size = new System.Drawing.Size(149, 23);
            this.button_ignoreDataTemporary.TabIndex = 1;
            this.button_ignoreDataTemporary.Text = "Ignore for now";
            this.button_ignoreDataTemporary.UseVisualStyleBackColor = true;
            this.button_ignoreDataTemporary.Click += new System.EventHandler(this.button_ignoreDataTemporary_Click);
            // 
            // button_updateData
            // 
            this.button_updateData.Location = new System.Drawing.Point(16, 49);
            this.button_updateData.Name = "button_updateData";
            this.button_updateData.Size = new System.Drawing.Size(174, 23);
            this.button_updateData.TabIndex = 2;
            this.button_updateData.Text = "Install and restart";
            this.button_updateData.UseVisualStyleBackColor = true;
            this.button_updateData.Click += new System.EventHandler(this.button_updateData_Click);
            // 
            // label_dataUpdate
            // 
            this.label_dataUpdate.AutoSize = true;
            this.label_dataUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label_dataUpdate.Location = new System.Drawing.Point(12, 9);
            this.label_dataUpdate.Name = "label_dataUpdate";
            this.label_dataUpdate.Size = new System.Drawing.Size(209, 20);
            this.label_dataUpdate.TabIndex = 1;
            this.label_dataUpdate.Text = "New Data Update available: ";
            // 
            // UpdatePrompt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel_dataUpdate);
            this.Controls.Add(this.panel_versionUpdate);
            this.Name = "UpdatePrompt";
            this.Text = "UpdatePrompt";
            this.panel_versionUpdate.ResumeLayout(false);
            this.panel_versionUpdate.PerformLayout();
            this.panel_dataUpdate.ResumeLayout(false);
            this.panel_dataUpdate.PerformLayout();
            this.ResumeLayout(false);

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