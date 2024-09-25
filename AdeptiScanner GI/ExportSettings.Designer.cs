
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
            label_maxraritynumber = new System.Windows.Forms.Label();
            label_maxraritytext = new System.Windows.Forms.Label();
            label_filterheadline = new System.Windows.Forms.Label();
            label_maxlevelnumber = new System.Windows.Forms.Label();
            label_maxleveltext = new System.Windows.Forms.Label();
            label_minlevelnumber = new System.Windows.Forms.Label();
            label_minleveltext = new System.Windows.Forms.Label();
            checkBox_exportEquipStatus = new System.Windows.Forms.CheckBox();
            tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            label_minraritytext = new System.Windows.Forms.Label();
            label_minraritynumber = new System.Windows.Forms.Label();
            tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            dpiIgnoringSlider_minlevel = new DpiIgnoringSlider();
            dpiIgnoringSlider_maxlevel = new DpiIgnoringSlider();
            dpiIgnoringSlider_minrarity = new DpiIgnoringSlider();
            dpiIgnoringSlider_maxrarity = new DpiIgnoringSlider();
            tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            tableLayoutPanel5.SuspendLayout();
            tableLayoutPanel6.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            SuspendLayout();
            // 
            // checkBox_exportEquipped
            // 
            checkBox_exportEquipped.AutoSize = true;
            checkBox_exportEquipped.Checked = true;
            checkBox_exportEquipped.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox_exportEquipped.Location = new System.Drawing.Point(122, 3);
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
            checkbox_exportTemplate.Location = new System.Drawing.Point(19, 3);
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
            btn_Clear.Location = new System.Drawing.Point(288, 36);
            btn_Clear.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_Clear.Name = "btn_Clear";
            btn_Clear.Size = new System.Drawing.Size(100, 27);
            btn_Clear.TabIndex = 6;
            btn_Clear.Text = "Clear results";
            btn_Clear.UseVisualStyleBackColor = true;
            btn_Clear.Click += btn_Clear_Click;
            // 
            // label_maxraritynumber
            // 
            label_maxraritynumber.AutoSize = true;
            label_maxraritynumber.Location = new System.Drawing.Point(4, 30);
            label_maxraritynumber.Margin = new System.Windows.Forms.Padding(4);
            label_maxraritynumber.Name = "label_maxraritynumber";
            label_maxraritynumber.Size = new System.Drawing.Size(13, 15);
            label_maxraritynumber.TabIndex = 9;
            label_maxraritynumber.Text = "5";
            // 
            // label_maxraritytext
            // 
            label_maxraritytext.AutoSize = true;
            label_maxraritytext.Location = new System.Drawing.Point(4, 4);
            label_maxraritytext.Margin = new System.Windows.Forms.Padding(4);
            label_maxraritytext.Name = "label_maxraritytext";
            label_maxraritytext.Size = new System.Drawing.Size(63, 15);
            label_maxraritytext.TabIndex = 8;
            label_maxraritytext.Text = "Max rarity:";
            // 
            // label_filterheadline
            // 
            label_filterheadline.AutoSize = true;
            tableLayoutPanel4.SetColumnSpan(label_filterheadline, 2);
            label_filterheadline.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Pixel);
            label_filterheadline.Location = new System.Drawing.Point(4, 0);
            label_filterheadline.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_filterheadline.Name = "label_filterheadline";
            label_filterheadline.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            label_filterheadline.Size = new System.Drawing.Size(112, 21);
            label_filterheadline.TabIndex = 12;
            label_filterheadline.Text = "Export filters";
            label_filterheadline.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_maxlevelnumber
            // 
            label_maxlevelnumber.AutoSize = true;
            label_maxlevelnumber.Location = new System.Drawing.Point(4, 30);
            label_maxlevelnumber.Margin = new System.Windows.Forms.Padding(4);
            label_maxlevelnumber.Name = "label_maxlevelnumber";
            label_maxlevelnumber.Size = new System.Drawing.Size(19, 15);
            label_maxlevelnumber.TabIndex = 5;
            label_maxlevelnumber.Text = "20";
            // 
            // label_maxleveltext
            // 
            label_maxleveltext.AutoSize = true;
            label_maxleveltext.Location = new System.Drawing.Point(4, 4);
            label_maxleveltext.Margin = new System.Windows.Forms.Padding(4);
            label_maxleveltext.Name = "label_maxleveltext";
            label_maxleveltext.Size = new System.Drawing.Size(60, 15);
            label_maxleveltext.TabIndex = 4;
            label_maxleveltext.Text = "Max level:";
            // 
            // label_minlevelnumber
            // 
            label_minlevelnumber.AutoSize = true;
            label_minlevelnumber.Location = new System.Drawing.Point(4, 30);
            label_minlevelnumber.Margin = new System.Windows.Forms.Padding(4);
            label_minlevelnumber.Name = "label_minlevelnumber";
            label_minlevelnumber.Size = new System.Drawing.Size(13, 15);
            label_minlevelnumber.TabIndex = 2;
            label_minlevelnumber.Text = "0";
            // 
            // label_minleveltext
            // 
            label_minleveltext.AutoSize = true;
            label_minleveltext.Location = new System.Drawing.Point(4, 4);
            label_minleveltext.Margin = new System.Windows.Forms.Padding(4);
            label_minleveltext.Name = "label_minleveltext";
            label_minleveltext.Size = new System.Drawing.Size(58, 15);
            label_minleveltext.TabIndex = 1;
            label_minleveltext.Text = "Min level:";
            // 
            // checkBox_exportEquipStatus
            // 
            checkBox_exportEquipStatus.AutoSize = true;
            checkBox_exportEquipStatus.Checked = true;
            checkBox_exportEquipStatus.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox_exportEquipStatus.Location = new System.Drawing.Point(122, 36);
            checkBox_exportEquipStatus.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_exportEquipStatus.Name = "checkBox_exportEquipStatus";
            checkBox_exportEquipStatus.Size = new System.Drawing.Size(127, 19);
            checkBox_exportEquipStatus.TabIndex = 13;
            checkBox_exportEquipStatus.Text = "Export equip status";
            checkBox_exportEquipStatus.UseVisualStyleBackColor = true;
            checkBox_exportEquipStatus.CheckedChanged += checkBox_ExportEquipStatus_CheckedChanged;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            tableLayoutPanel1.Controls.Add(checkbox_exportTemplate, 0, 0);
            tableLayoutPanel1.Controls.Add(checkBox_exportEquipped, 1, 0);
            tableLayoutPanel1.Controls.Add(btn_Clear, 2, 1);
            tableLayoutPanel1.Controls.Add(checkBox_exportEquipStatus, 1, 1);
            tableLayoutPanel1.Location = new System.Drawing.Point(0, 197);
            tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new System.Drawing.Size(407, 66);
            tableLayoutPanel1.TabIndex = 14;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 1;
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(label_maxraritytext, 0, 0);
            tableLayoutPanel2.Controls.Add(label_maxraritynumber, 0, 1);
            tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel2.Location = new System.Drawing.Point(202, 127);
            tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 2;
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new System.Drawing.Size(101, 53);
            tableLayoutPanel2.TabIndex = 15;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.AutoSize = true;
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel3.Controls.Add(label_minraritytext, 0, 0);
            tableLayoutPanel3.Controls.Add(label_minraritynumber, 0, 1);
            tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel3.Location = new System.Drawing.Point(0, 127);
            tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new System.Drawing.Size(101, 53);
            tableLayoutPanel3.TabIndex = 17;
            // 
            // label_minraritytext
            // 
            label_minraritytext.AutoSize = true;
            label_minraritytext.Location = new System.Drawing.Point(4, 4);
            label_minraritytext.Margin = new System.Windows.Forms.Padding(4);
            label_minraritytext.Name = "label_minraritytext";
            label_minraritytext.Size = new System.Drawing.Size(61, 15);
            label_minraritytext.TabIndex = 6;
            label_minraritytext.Text = "Min rarity:";
            // 
            // label_minraritynumber
            // 
            label_minraritynumber.AutoSize = true;
            label_minraritynumber.Location = new System.Drawing.Point(4, 30);
            label_minraritynumber.Margin = new System.Windows.Forms.Padding(4);
            label_minraritynumber.Name = "label_minraritynumber";
            label_minraritynumber.Size = new System.Drawing.Size(13, 15);
            label_minraritynumber.TabIndex = 7;
            label_minraritynumber.Text = "5";
            // 
            // tableLayoutPanel5
            // 
            tableLayoutPanel5.AutoSize = true;
            tableLayoutPanel5.ColumnCount = 1;
            tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel5.Controls.Add(label_minleveltext, 0, 0);
            tableLayoutPanel5.Controls.Add(label_minlevelnumber, 0, 1);
            tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel5.Location = new System.Drawing.Point(0, 21);
            tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel5.Name = "tableLayoutPanel5";
            tableLayoutPanel5.RowCount = 2;
            tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel5.Size = new System.Drawing.Size(101, 53);
            tableLayoutPanel5.TabIndex = 18;
            // 
            // tableLayoutPanel6
            // 
            tableLayoutPanel6.AutoSize = true;
            tableLayoutPanel6.ColumnCount = 1;
            tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            tableLayoutPanel6.Controls.Add(label_maxleveltext, 0, 0);
            tableLayoutPanel6.Controls.Add(label_maxlevelnumber, 0, 1);
            tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel6.Location = new System.Drawing.Point(0, 74);
            tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            tableLayoutPanel6.Name = "tableLayoutPanel6";
            tableLayoutPanel6.RowCount = 2;
            tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            tableLayoutPanel6.Size = new System.Drawing.Size(101, 53);
            tableLayoutPanel6.TabIndex = 19;
            // 
            // dpiIgnoringSlider_minlevel
            // 
            tableLayoutPanel4.SetColumnSpan(dpiIgnoringSlider_minlevel, 3);
            dpiIgnoringSlider_minlevel.Dock = System.Windows.Forms.DockStyle.Fill;
            dpiIgnoringSlider_minlevel.Location = new System.Drawing.Point(104, 24);
            dpiIgnoringSlider_minlevel.MaxValue = 20;
            dpiIgnoringSlider_minlevel.MinValue = 0;
            dpiIgnoringSlider_minlevel.Name = "dpiIgnoringSlider_minlevel";
            dpiIgnoringSlider_minlevel.Size = new System.Drawing.Size(297, 47);
            dpiIgnoringSlider_minlevel.TabIndex = 20;
            dpiIgnoringSlider_minlevel.Text = "dpiIgnoringSlider1";
            dpiIgnoringSlider_minlevel.TickInterval = 1;
            dpiIgnoringSlider_minlevel.Value = 0;
            dpiIgnoringSlider_minlevel.Scroll += dpiIgnoringSlider_minlevel_Scroll;
            // 
            // dpiIgnoringSlider_maxlevel
            // 
            tableLayoutPanel4.SetColumnSpan(dpiIgnoringSlider_maxlevel, 3);
            dpiIgnoringSlider_maxlevel.Dock = System.Windows.Forms.DockStyle.Fill;
            dpiIgnoringSlider_maxlevel.Location = new System.Drawing.Point(104, 77);
            dpiIgnoringSlider_maxlevel.MaxValue = 20;
            dpiIgnoringSlider_maxlevel.MinValue = 0;
            dpiIgnoringSlider_maxlevel.Name = "dpiIgnoringSlider_maxlevel";
            dpiIgnoringSlider_maxlevel.Size = new System.Drawing.Size(297, 47);
            dpiIgnoringSlider_maxlevel.TabIndex = 21;
            dpiIgnoringSlider_maxlevel.Text = "dpiIgnoringSlider1";
            dpiIgnoringSlider_maxlevel.TickInterval = 1;
            dpiIgnoringSlider_maxlevel.Value = 0;
            dpiIgnoringSlider_maxlevel.Scroll += dpiIgnoringSlider_maxlevel_Scroll;
            // 
            // dpiIgnoringSlider_minrarity
            // 
            dpiIgnoringSlider_minrarity.Dock = System.Windows.Forms.DockStyle.Fill;
            dpiIgnoringSlider_minrarity.Location = new System.Drawing.Point(104, 130);
            dpiIgnoringSlider_minrarity.MaxValue = 5;
            dpiIgnoringSlider_minrarity.MinValue = 0;
            dpiIgnoringSlider_minrarity.Name = "dpiIgnoringSlider_minrarity";
            dpiIgnoringSlider_minrarity.Size = new System.Drawing.Size(95, 47);
            dpiIgnoringSlider_minrarity.TabIndex = 22;
            dpiIgnoringSlider_minrarity.Text = "dpiIgnoringSlider1";
            dpiIgnoringSlider_minrarity.TickInterval = 1;
            dpiIgnoringSlider_minrarity.Value = 0;
            dpiIgnoringSlider_minrarity.Scroll += dpiIgnoringSlider_minrarity_Scroll;
            // 
            // dpiIgnoringSlider_maxrarity
            // 
            dpiIgnoringSlider_maxrarity.Dock = System.Windows.Forms.DockStyle.Fill;
            dpiIgnoringSlider_maxrarity.Location = new System.Drawing.Point(306, 130);
            dpiIgnoringSlider_maxrarity.MaxValue = 5;
            dpiIgnoringSlider_maxrarity.MinValue = 0;
            dpiIgnoringSlider_maxrarity.Name = "dpiIgnoringSlider_maxrarity";
            dpiIgnoringSlider_maxrarity.Size = new System.Drawing.Size(95, 47);
            dpiIgnoringSlider_maxrarity.TabIndex = 23;
            dpiIgnoringSlider_maxrarity.Text = "dpiIgnoringSlider1";
            dpiIgnoringSlider_maxrarity.TickInterval = 1;
            dpiIgnoringSlider_maxrarity.Value = 0;
            dpiIgnoringSlider_maxrarity.Scroll += dpiIgnoringSlider_maxrarity_Scroll;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 4;
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tableLayoutPanel4.Controls.Add(dpiIgnoringSlider_maxrarity, 3, 3);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel2, 2, 3);
            tableLayoutPanel4.Controls.Add(dpiIgnoringSlider_minrarity, 1, 3);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel3, 0, 3);
            tableLayoutPanel4.Controls.Add(dpiIgnoringSlider_maxlevel, 1, 2);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel6, 0, 2);
            tableLayoutPanel4.Controls.Add(dpiIgnoringSlider_minlevel, 1, 1);
            tableLayoutPanel4.Controls.Add(tableLayoutPanel5, 0, 1);
            tableLayoutPanel4.Controls.Add(label_filterheadline, 0, 0);
            tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 4;
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.3333321F));
            tableLayoutPanel4.Size = new System.Drawing.Size(404, 180);
            tableLayoutPanel4.TabIndex = 21;
            // 
            // ExportSettings
            // 
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            Controls.Add(tableLayoutPanel4);
            Controls.Add(tableLayoutPanel1);
            Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            Name = "ExportSettings";
            Size = new System.Drawing.Size(407, 263);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            tableLayoutPanel3.ResumeLayout(false);
            tableLayoutPanel3.PerformLayout();
            tableLayoutPanel5.ResumeLayout(false);
            tableLayoutPanel5.PerformLayout();
            tableLayoutPanel6.ResumeLayout(false);
            tableLayoutPanel6.PerformLayout();
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.CheckBox checkBox_exportEquipped;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Label label_filterheadline;
        private System.Windows.Forms.Label label_maxraritynumber;
        private System.Windows.Forms.Label label_maxraritytext;
        private System.Windows.Forms.CheckBox checkbox_exportTemplate;
        private System.Windows.Forms.Label label_maxlevelnumber;
        private System.Windows.Forms.Label label_maxleveltext;
        private System.Windows.Forms.Label label_minlevelnumber;
        private System.Windows.Forms.Label label_minleveltext;
        private System.Windows.Forms.CheckBox checkBox_exportEquipStatus;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label_minraritytext;
        private System.Windows.Forms.Label label_minraritynumber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private DpiIgnoringSlider dpiIgnoringSlider_minlevel;
        private DpiIgnoringSlider dpiIgnoringSlider_maxlevel;
        private DpiIgnoringSlider dpiIgnoringSlider_minrarity;
        private DpiIgnoringSlider dpiIgnoringSlider_maxrarity;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
    }
}
