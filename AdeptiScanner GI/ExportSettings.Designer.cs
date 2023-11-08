
namespace AdeptiScanner_GI
{
    partial class ExportSettings
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
            checkBox_exportEquipped = new System.Windows.Forms.CheckBox();
            checkbox_exportTemplate = new System.Windows.Forms.CheckBox();
            btn_Clear = new System.Windows.Forms.Button();
            trackBar_maxrarity = new System.Windows.Forms.TrackBar();
            trackBar_minrarity = new System.Windows.Forms.TrackBar();
            label_maxraritynumber = new System.Windows.Forms.Label();
            label_maxraritytext = new System.Windows.Forms.Label();
            label_minraritynumber = new System.Windows.Forms.Label();
            label_minraritytext = new System.Windows.Forms.Label();
            label_filterheadline = new System.Windows.Forms.Label();
            label_maxlevelnumber = new System.Windows.Forms.Label();
            label_maxleveltext = new System.Windows.Forms.Label();
            trackBar_maxlevel = new System.Windows.Forms.TrackBar();
            label_minlevelnumber = new System.Windows.Forms.Label();
            label_minleveltext = new System.Windows.Forms.Label();
            trackBar_minlevel = new System.Windows.Forms.TrackBar();
            checkBox_exportEquipStatus = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)trackBar_maxrarity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_minrarity).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_maxlevel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_minlevel).BeginInit();
            SuspendLayout();
            // 
            // checkBox_exportEquipped
            // 
            checkBox_exportEquipped.AutoSize = true;
            checkBox_exportEquipped.Checked = true;
            checkBox_exportEquipped.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox_exportEquipped.Location = new System.Drawing.Point(108, 205);
            checkBox_exportEquipped.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_exportEquipped.Name = "checkBox_exportEquipped";
            checkBox_exportEquipped.Size = new System.Drawing.Size(158, 19);
            checkBox_exportEquipped.TabIndex = 5;
            checkBox_exportEquipped.Text = "Always include equipped";
            checkBox_exportEquipped.UseVisualStyleBackColor = true;
            checkBox_exportEquipped.CheckedChanged += checkBox_exportEquipped_CheckedChanged;
            // 
            // checkbox_exportTemplate
            // 
            checkbox_exportTemplate.AutoSize = true;
            checkbox_exportTemplate.Location = new System.Drawing.Point(7, 205);
            checkbox_exportTemplate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkbox_exportTemplate.Name = "checkbox_exportTemplate";
            checkbox_exportTemplate.Size = new System.Drawing.Size(95, 19);
            checkbox_exportTemplate.TabIndex = 4;
            checkbox_exportTemplate.Text = "Use template";
            checkbox_exportTemplate.UseVisualStyleBackColor = true;
            checkbox_exportTemplate.CheckedChanged += checkbox_exportTemplate_CheckedChanged;
            // 
            // btn_Clear
            // 
            btn_Clear.Location = new System.Drawing.Point(308, 224);
            btn_Clear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_Clear.Name = "btn_Clear";
            btn_Clear.Size = new System.Drawing.Size(88, 27);
            btn_Clear.TabIndex = 6;
            btn_Clear.Text = "Clear results";
            btn_Clear.UseVisualStyleBackColor = true;
            btn_Clear.Click += btn_Clear_Click;
            // 
            // trackBar_maxrarity
            // 
            trackBar_maxrarity.Location = new System.Drawing.Point(306, 145);
            trackBar_maxrarity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trackBar_maxrarity.Maximum = 5;
            trackBar_maxrarity.Name = "trackBar_maxrarity";
            trackBar_maxrarity.Size = new System.Drawing.Size(90, 45);
            trackBar_maxrarity.TabIndex = 3;
            trackBar_maxrarity.Value = 5;
            trackBar_maxrarity.Scroll += trackBar_maxrarity_Scroll;
            // 
            // trackBar_minrarity
            // 
            trackBar_minrarity.Location = new System.Drawing.Point(96, 147);
            trackBar_minrarity.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trackBar_minrarity.Maximum = 5;
            trackBar_minrarity.Name = "trackBar_minrarity";
            trackBar_minrarity.Size = new System.Drawing.Size(90, 45);
            trackBar_minrarity.TabIndex = 2;
            trackBar_minrarity.Value = 5;
            trackBar_minrarity.Scroll += trackBar_minrarity_Scroll;
            // 
            // label_maxraritynumber
            // 
            label_maxraritynumber.AutoSize = true;
            label_maxraritynumber.Location = new System.Drawing.Point(240, 182);
            label_maxraritynumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_maxraritynumber.Name = "label_maxraritynumber";
            label_maxraritynumber.Size = new System.Drawing.Size(13, 15);
            label_maxraritynumber.TabIndex = 9;
            label_maxraritynumber.Text = "5";
            // 
            // label_maxraritytext
            // 
            label_maxraritytext.AutoSize = true;
            label_maxraritytext.Location = new System.Drawing.Point(196, 157);
            label_maxraritytext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_maxraritytext.Name = "label_maxraritytext";
            label_maxraritytext.Size = new System.Drawing.Size(95, 15);
            label_maxraritytext.TabIndex = 8;
            label_maxraritytext.Text = "Maximum rarity:";
            // 
            // label_minraritynumber
            // 
            label_minraritynumber.AutoSize = true;
            label_minraritynumber.Location = new System.Drawing.Point(48, 182);
            label_minraritynumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_minraritynumber.Name = "label_minraritynumber";
            label_minraritynumber.Size = new System.Drawing.Size(13, 15);
            label_minraritynumber.TabIndex = 7;
            label_minraritynumber.Text = "5";
            // 
            // label_minraritytext
            // 
            label_minraritytext.AutoSize = true;
            label_minraritytext.Location = new System.Drawing.Point(4, 157);
            label_minraritytext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_minraritytext.Name = "label_minraritytext";
            label_minraritytext.Size = new System.Drawing.Size(93, 15);
            label_minraritytext.TabIndex = 6;
            label_minraritytext.Text = "Minimum rarity:";
            // 
            // label_filterheadline
            // 
            label_filterheadline.AutoSize = true;
            label_filterheadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point);
            label_filterheadline.Location = new System.Drawing.Point(9, 18);
            label_filterheadline.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_filterheadline.Name = "label_filterheadline";
            label_filterheadline.Size = new System.Drawing.Size(97, 20);
            label_filterheadline.TabIndex = 12;
            label_filterheadline.Text = "Export filters";
            // 
            // label_maxlevelnumber
            // 
            label_maxlevelnumber.AutoSize = true;
            label_maxlevelnumber.Location = new System.Drawing.Point(70, 123);
            label_maxlevelnumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_maxlevelnumber.Name = "label_maxlevelnumber";
            label_maxlevelnumber.Size = new System.Drawing.Size(19, 15);
            label_maxlevelnumber.TabIndex = 5;
            label_maxlevelnumber.Text = "20";
            // 
            // label_maxleveltext
            // 
            label_maxleveltext.AutoSize = true;
            label_maxleveltext.Location = new System.Drawing.Point(4, 102);
            label_maxleveltext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_maxleveltext.Name = "label_maxleveltext";
            label_maxleveltext.Size = new System.Drawing.Size(92, 15);
            label_maxleveltext.TabIndex = 4;
            label_maxleveltext.Text = "Maximum level:";
            // 
            // trackBar_maxlevel
            // 
            trackBar_maxlevel.Location = new System.Drawing.Point(96, 102);
            trackBar_maxlevel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trackBar_maxlevel.Maximum = 20;
            trackBar_maxlevel.Name = "trackBar_maxlevel";
            trackBar_maxlevel.Size = new System.Drawing.Size(300, 45);
            trackBar_maxlevel.TabIndex = 1;
            trackBar_maxlevel.Value = 20;
            trackBar_maxlevel.Scroll += trackBar_maxlevel_Scroll;
            // 
            // label_minlevelnumber
            // 
            label_minlevelnumber.AutoSize = true;
            label_minlevelnumber.Location = new System.Drawing.Point(74, 72);
            label_minlevelnumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_minlevelnumber.Name = "label_minlevelnumber";
            label_minlevelnumber.Size = new System.Drawing.Size(13, 15);
            label_minlevelnumber.TabIndex = 2;
            label_minlevelnumber.Text = "0";
            // 
            // label_minleveltext
            // 
            label_minleveltext.AutoSize = true;
            label_minleveltext.Location = new System.Drawing.Point(4, 47);
            label_minleveltext.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_minleveltext.Name = "label_minleveltext";
            label_minleveltext.Size = new System.Drawing.Size(90, 15);
            label_minleveltext.TabIndex = 1;
            label_minleveltext.Text = "Minimum level:";
            // 
            // trackBar_minlevel
            // 
            trackBar_minlevel.Location = new System.Drawing.Point(96, 47);
            trackBar_minlevel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            trackBar_minlevel.Maximum = 20;
            trackBar_minlevel.Name = "trackBar_minlevel";
            trackBar_minlevel.Size = new System.Drawing.Size(300, 45);
            trackBar_minlevel.TabIndex = 0;
            trackBar_minlevel.Scroll += trackBar_minlevel_Scroll;
            // 
            // checkBox_exportEquipStatus
            // 
            checkBox_exportEquipStatus.AutoSize = true;
            checkBox_exportEquipStatus.Checked = true;
            checkBox_exportEquipStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox_exportEquipStatus.Location = new System.Drawing.Point(108, 230);
            checkBox_exportEquipStatus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_exportEquipStatus.Name = "checkBox_exportEquipStatus";
            checkBox_exportEquipStatus.Size = new System.Drawing.Size(127, 19);
            checkBox_exportEquipStatus.TabIndex = 13;
            checkBox_exportEquipStatus.Text = "Export equip status";
            checkBox_exportEquipStatus.UseVisualStyleBackColor = true;
            checkBox_exportEquipStatus.CheckedChanged += checkBox_ExportEquipStatus_CheckedChanged;
            // 
            // ExportSettings
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(checkBox_exportEquipStatus);
            Controls.Add(checkBox_exportEquipped);
            Controls.Add(checkbox_exportTemplate);
            Controls.Add(btn_Clear);
            Controls.Add(label_filterheadline);
            Controls.Add(trackBar_maxrarity);
            Controls.Add(label_minleveltext);
            Controls.Add(label_maxraritynumber);
            Controls.Add(trackBar_minrarity);
            Controls.Add(label_maxraritytext);
            Controls.Add(trackBar_minlevel);
            Controls.Add(label_minlevelnumber);
            Controls.Add(label_maxleveltext);
            Controls.Add(label_maxlevelnumber);
            Controls.Add(label_minraritynumber);
            Controls.Add(trackBar_maxlevel);
            Controls.Add(label_minraritytext);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ExportSettings";
            Size = new System.Drawing.Size(407, 263);
            ((System.ComponentModel.ISupportInitialize)trackBar_maxrarity).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_minrarity).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_maxlevel).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBar_minlevel).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.CheckBox checkBox_exportEquipped;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Label label_filterheadline;
        private System.Windows.Forms.TrackBar trackBar_maxrarity;
        private System.Windows.Forms.TrackBar trackBar_minrarity;
        private System.Windows.Forms.Label label_maxraritynumber;
        private System.Windows.Forms.Label label_maxraritytext;
        private System.Windows.Forms.CheckBox checkbox_exportTemplate;
        private System.Windows.Forms.Label label_minraritynumber;
        private System.Windows.Forms.Label label_minraritytext;
        private System.Windows.Forms.Label label_maxlevelnumber;
        private System.Windows.Forms.Label label_maxleveltext;
        private System.Windows.Forms.TrackBar trackBar_maxlevel;
        private System.Windows.Forms.Label label_minlevelnumber;
        private System.Windows.Forms.Label label_minleveltext;
        private System.Windows.Forms.TrackBar trackBar_minlevel;
        private System.Windows.Forms.CheckBox checkBox_exportEquipStatus;
    }
}
