
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
            this.checkBox_exportEquipped = new System.Windows.Forms.CheckBox();
            this.checkbox_exportTemplate = new System.Windows.Forms.CheckBox();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.trackBar_maxrarity = new System.Windows.Forms.TrackBar();
            this.trackBar_minrarity = new System.Windows.Forms.TrackBar();
            this.label_maxraritynumber = new System.Windows.Forms.Label();
            this.label_maxraritytext = new System.Windows.Forms.Label();
            this.label_minraritynumber = new System.Windows.Forms.Label();
            this.label_minraritytext = new System.Windows.Forms.Label();
            this.label_filterheadline = new System.Windows.Forms.Label();
            this.label_maxlevelnumber = new System.Windows.Forms.Label();
            this.label_maxleveltext = new System.Windows.Forms.Label();
            this.trackBar_maxlevel = new System.Windows.Forms.TrackBar();
            this.label_minlevelnumber = new System.Windows.Forms.Label();
            this.label_minleveltext = new System.Windows.Forms.Label();
            this.trackBar_minlevel = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_maxrarity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_minrarity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_maxlevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_minlevel)).BeginInit();
            this.SuspendLayout();
            // 
            // checkBox_exportEquipped
            // 
            this.checkBox_exportEquipped.AutoSize = true;
            this.checkBox_exportEquipped.Checked = true;
            this.checkBox_exportEquipped.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_exportEquipped.Location = new System.Drawing.Point(93, 191);
            this.checkBox_exportEquipped.Name = "checkBox_exportEquipped";
            this.checkBox_exportEquipped.Size = new System.Drawing.Size(143, 17);
            this.checkBox_exportEquipped.TabIndex = 5;
            this.checkBox_exportEquipped.Text = "Always include equipped";
            this.checkBox_exportEquipped.UseVisualStyleBackColor = true;
            this.checkBox_exportEquipped.CheckedChanged += new System.EventHandler(this.checkBox_exportEquipped_CheckedChanged);
            // 
            // checkbox_exportTemplate
            // 
            this.checkbox_exportTemplate.AutoSize = true;
            this.checkbox_exportTemplate.Location = new System.Drawing.Point(6, 191);
            this.checkbox_exportTemplate.Name = "checkbox_exportTemplate";
            this.checkbox_exportTemplate.Size = new System.Drawing.Size(88, 17);
            this.checkbox_exportTemplate.TabIndex = 4;
            this.checkbox_exportTemplate.Text = "Use template";
            this.checkbox_exportTemplate.UseVisualStyleBackColor = true;
            this.checkbox_exportTemplate.CheckedChanged += new System.EventHandler(this.checkbox_exportTemplate_CheckedChanged);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(264, 187);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(75, 23);
            this.btn_Clear.TabIndex = 6;
            this.btn_Clear.Text = "Clear results";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // trackBar_maxrarity
            // 
            this.trackBar_maxrarity.Location = new System.Drawing.Point(262, 139);
            this.trackBar_maxrarity.Maximum = 5;
            this.trackBar_maxrarity.Name = "trackBar_maxrarity";
            this.trackBar_maxrarity.Size = new System.Drawing.Size(77, 45);
            this.trackBar_maxrarity.TabIndex = 3;
            this.trackBar_maxrarity.Value = 5;
            this.trackBar_maxrarity.Scroll += new System.EventHandler(this.trackBar_maxrarity_Scroll);
            // 
            // trackBar_minrarity
            // 
            this.trackBar_minrarity.Location = new System.Drawing.Point(82, 140);
            this.trackBar_minrarity.Maximum = 5;
            this.trackBar_minrarity.Name = "trackBar_minrarity";
            this.trackBar_minrarity.Size = new System.Drawing.Size(77, 45);
            this.trackBar_minrarity.TabIndex = 2;
            this.trackBar_minrarity.Value = 5;
            this.trackBar_minrarity.Scroll += new System.EventHandler(this.trackBar_minrarity_Scroll);
            // 
            // label_maxraritynumber
            // 
            this.label_maxraritynumber.AutoSize = true;
            this.label_maxraritynumber.Location = new System.Drawing.Point(206, 171);
            this.label_maxraritynumber.Name = "label_maxraritynumber";
            this.label_maxraritynumber.Size = new System.Drawing.Size(13, 13);
            this.label_maxraritynumber.TabIndex = 9;
            this.label_maxraritynumber.Text = "5";
            // 
            // label_maxraritytext
            // 
            this.label_maxraritytext.AutoSize = true;
            this.label_maxraritytext.Location = new System.Drawing.Point(168, 149);
            this.label_maxraritytext.Name = "label_maxraritytext";
            this.label_maxraritytext.Size = new System.Drawing.Size(79, 13);
            this.label_maxraritytext.TabIndex = 8;
            this.label_maxraritytext.Text = "Maximum rarity:";
            // 
            // label_minraritynumber
            // 
            this.label_minraritynumber.AutoSize = true;
            this.label_minraritynumber.Location = new System.Drawing.Point(41, 171);
            this.label_minraritynumber.Name = "label_minraritynumber";
            this.label_minraritynumber.Size = new System.Drawing.Size(13, 13);
            this.label_minraritynumber.TabIndex = 7;
            this.label_minraritynumber.Text = "5";
            // 
            // label_minraritytext
            // 
            this.label_minraritytext.AutoSize = true;
            this.label_minraritytext.Location = new System.Drawing.Point(3, 149);
            this.label_minraritytext.Name = "label_minraritytext";
            this.label_minraritytext.Size = new System.Drawing.Size(76, 13);
            this.label_minraritytext.TabIndex = 6;
            this.label_minraritytext.Text = "Minimum rarity:";
            // 
            // label_filterheadline
            // 
            this.label_filterheadline.AutoSize = true;
            this.label_filterheadline.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_filterheadline.Location = new System.Drawing.Point(8, 16);
            this.label_filterheadline.Name = "label_filterheadline";
            this.label_filterheadline.Size = new System.Drawing.Size(97, 20);
            this.label_filterheadline.TabIndex = 12;
            this.label_filterheadline.Text = "Export filters";
            // 
            // label_maxlevelnumber
            // 
            this.label_maxlevelnumber.AutoSize = true;
            this.label_maxlevelnumber.Location = new System.Drawing.Point(60, 120);
            this.label_maxlevelnumber.Name = "label_maxlevelnumber";
            this.label_maxlevelnumber.Size = new System.Drawing.Size(19, 13);
            this.label_maxlevelnumber.TabIndex = 5;
            this.label_maxlevelnumber.Text = "20";
            // 
            // label_maxleveltext
            // 
            this.label_maxleveltext.AutoSize = true;
            this.label_maxleveltext.Location = new System.Drawing.Point(3, 101);
            this.label_maxleveltext.Name = "label_maxleveltext";
            this.label_maxleveltext.Size = new System.Drawing.Size(79, 13);
            this.label_maxleveltext.TabIndex = 4;
            this.label_maxleveltext.Text = "Maximum level:";
            // 
            // trackBar_maxlevel
            // 
            this.trackBar_maxlevel.Location = new System.Drawing.Point(82, 101);
            this.trackBar_maxlevel.Maximum = 20;
            this.trackBar_maxlevel.Name = "trackBar_maxlevel";
            this.trackBar_maxlevel.Size = new System.Drawing.Size(257, 45);
            this.trackBar_maxlevel.TabIndex = 1;
            this.trackBar_maxlevel.Value = 20;
            this.trackBar_maxlevel.Scroll += new System.EventHandler(this.trackBar_maxlevel_Scroll);
            // 
            // label_minlevelnumber
            // 
            this.label_minlevelnumber.AutoSize = true;
            this.label_minlevelnumber.Location = new System.Drawing.Point(63, 75);
            this.label_minlevelnumber.Name = "label_minlevelnumber";
            this.label_minlevelnumber.Size = new System.Drawing.Size(13, 13);
            this.label_minlevelnumber.TabIndex = 2;
            this.label_minlevelnumber.Text = "0";
            // 
            // label_minleveltext
            // 
            this.label_minleveltext.AutoSize = true;
            this.label_minleveltext.Location = new System.Drawing.Point(3, 54);
            this.label_minleveltext.Name = "label_minleveltext";
            this.label_minleveltext.Size = new System.Drawing.Size(76, 13);
            this.label_minleveltext.TabIndex = 1;
            this.label_minleveltext.Text = "Minimum level:";
            // 
            // trackBar_minlevel
            // 
            this.trackBar_minlevel.Location = new System.Drawing.Point(82, 54);
            this.trackBar_minlevel.Maximum = 20;
            this.trackBar_minlevel.Name = "trackBar_minlevel";
            this.trackBar_minlevel.Size = new System.Drawing.Size(257, 45);
            this.trackBar_minlevel.TabIndex = 0;
            this.trackBar_minlevel.Scroll += new System.EventHandler(this.trackBar_minlevel_Scroll);
            // 
            // ExportSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBox_exportEquipped);
            this.Controls.Add(this.checkbox_exportTemplate);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.label_filterheadline);
            this.Controls.Add(this.trackBar_maxrarity);
            this.Controls.Add(this.label_minleveltext);
            this.Controls.Add(this.label_maxraritynumber);
            this.Controls.Add(this.trackBar_minrarity);
            this.Controls.Add(this.label_maxraritytext);
            this.Controls.Add(this.trackBar_minlevel);
            this.Controls.Add(this.label_minlevelnumber);
            this.Controls.Add(this.label_maxleveltext);
            this.Controls.Add(this.label_maxlevelnumber);
            this.Controls.Add(this.label_minraritynumber);
            this.Controls.Add(this.trackBar_maxlevel);
            this.Controls.Add(this.label_minraritytext);
            this.Name = "ExportSettings";
            this.Size = new System.Drawing.Size(349, 228);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_maxrarity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_minrarity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_maxlevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_minlevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}
