
namespace GenshinArtifactOCR
{
    partial class Form1
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
            this.image_preview = new System.Windows.Forms.PictureBox();
            this.text_Set = new System.Windows.Forms.TextBox();
            this.text_Type = new System.Windows.Forms.TextBox();
            this.text_statMain = new System.Windows.Forms.TextBox();
            this.text_statSub1 = new System.Windows.Forms.TextBox();
            this.text_statSub2 = new System.Windows.Forms.TextBox();
            this.text_statSub3 = new System.Windows.Forms.TextBox();
            this.text_statSub4 = new System.Windows.Forms.TextBox();
            this.label_Set = new System.Windows.Forms.Label();
            this.label_Type = new System.Windows.Forms.Label();
            this.label_statMain = new System.Windows.Forms.Label();
            this.label_statSub1 = new System.Windows.Forms.Label();
            this.label_statSub2 = new System.Windows.Forms.Label();
            this.label_statSub3 = new System.Windows.Forms.Label();
            this.label_statSub4 = new System.Windows.Forms.Label();
            this.btn_capture = new System.Windows.Forms.Button();
            this.btn_Filter = new System.Windows.Forms.Button();
            this.btn_OCR = new System.Windows.Forms.Button();
            this.text_raw = new System.Windows.Forms.TextBox();
            this.label_raw = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.image_preview)).BeginInit();
            this.SuspendLayout();
            // 
            // image_preview
            // 
            this.image_preview.Location = new System.Drawing.Point(12, 12);
            this.image_preview.Name = "image_preview";
            this.image_preview.Size = new System.Drawing.Size(390, 552);
            this.image_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.image_preview.TabIndex = 1;
            this.image_preview.TabStop = false;
            // 
            // text_Set
            // 
            this.text_Set.Location = new System.Drawing.Point(483, 37);
            this.text_Set.Name = "text_Set";
            this.text_Set.ReadOnly = true;
            this.text_Set.Size = new System.Drawing.Size(279, 20);
            this.text_Set.TabIndex = 2;
            // 
            // text_Type
            // 
            this.text_Type.Location = new System.Drawing.Point(483, 63);
            this.text_Type.Name = "text_Type";
            this.text_Type.ReadOnly = true;
            this.text_Type.Size = new System.Drawing.Size(279, 20);
            this.text_Type.TabIndex = 3;
            // 
            // text_statMain
            // 
            this.text_statMain.Location = new System.Drawing.Point(483, 89);
            this.text_statMain.Name = "text_statMain";
            this.text_statMain.ReadOnly = true;
            this.text_statMain.Size = new System.Drawing.Size(279, 20);
            this.text_statMain.TabIndex = 4;
            // 
            // text_statSub1
            // 
            this.text_statSub1.Location = new System.Drawing.Point(483, 115);
            this.text_statSub1.Name = "text_statSub1";
            this.text_statSub1.ReadOnly = true;
            this.text_statSub1.Size = new System.Drawing.Size(279, 20);
            this.text_statSub1.TabIndex = 5;
            // 
            // text_statSub2
            // 
            this.text_statSub2.Location = new System.Drawing.Point(483, 141);
            this.text_statSub2.Name = "text_statSub2";
            this.text_statSub2.ReadOnly = true;
            this.text_statSub2.Size = new System.Drawing.Size(279, 20);
            this.text_statSub2.TabIndex = 6;
            // 
            // text_statSub3
            // 
            this.text_statSub3.Location = new System.Drawing.Point(483, 167);
            this.text_statSub3.Name = "text_statSub3";
            this.text_statSub3.ReadOnly = true;
            this.text_statSub3.Size = new System.Drawing.Size(279, 20);
            this.text_statSub3.TabIndex = 7;
            // 
            // text_statSub4
            // 
            this.text_statSub4.Location = new System.Drawing.Point(483, 193);
            this.text_statSub4.Name = "text_statSub4";
            this.text_statSub4.ReadOnly = true;
            this.text_statSub4.Size = new System.Drawing.Size(279, 20);
            this.text_statSub4.TabIndex = 14;
            // 
            // label_Set
            // 
            this.label_Set.AutoSize = true;
            this.label_Set.Location = new System.Drawing.Point(454, 44);
            this.label_Set.Name = "label_Set";
            this.label_Set.Size = new System.Drawing.Size(23, 13);
            this.label_Set.TabIndex = 8;
            this.label_Set.Text = "Set";
            // 
            // label_Type
            // 
            this.label_Type.AutoSize = true;
            this.label_Type.Location = new System.Drawing.Point(446, 70);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(31, 13);
            this.label_Type.TabIndex = 9;
            this.label_Type.Text = "Type";
            // 
            // label_statMain
            // 
            this.label_statMain.AutoSize = true;
            this.label_statMain.Location = new System.Drawing.Point(425, 96);
            this.label_statMain.Name = "label_statMain";
            this.label_statMain.Size = new System.Drawing.Size(52, 13);
            this.label_statMain.TabIndex = 10;
            this.label_statMain.Text = "Main Stat";
            // 
            // label_statSub1
            // 
            this.label_statSub1.AutoSize = true;
            this.label_statSub1.Location = new System.Drawing.Point(425, 122);
            this.label_statSub1.Name = "label_statSub1";
            this.label_statSub1.Size = new System.Drawing.Size(52, 13);
            this.label_statSub1.TabIndex = 11;
            this.label_statSub1.Text = "Substat 1";
            // 
            // label_statSub2
            // 
            this.label_statSub2.AutoSize = true;
            this.label_statSub2.Location = new System.Drawing.Point(425, 148);
            this.label_statSub2.Name = "label_statSub2";
            this.label_statSub2.Size = new System.Drawing.Size(52, 13);
            this.label_statSub2.TabIndex = 12;
            this.label_statSub2.Text = "Substat 2";
            // 
            // label_statSub3
            // 
            this.label_statSub3.AutoSize = true;
            this.label_statSub3.Location = new System.Drawing.Point(425, 174);
            this.label_statSub3.Name = "label_statSub3";
            this.label_statSub3.Size = new System.Drawing.Size(52, 13);
            this.label_statSub3.TabIndex = 13;
            this.label_statSub3.Text = "Substat 3";
            // 
            // label_statSub4
            // 
            this.label_statSub4.AutoSize = true;
            this.label_statSub4.Location = new System.Drawing.Point(425, 200);
            this.label_statSub4.Name = "label_statSub4";
            this.label_statSub4.Size = new System.Drawing.Size(52, 13);
            this.label_statSub4.TabIndex = 15;
            this.label_statSub4.Text = "Substat 4";
            // 
            // btn_capture
            // 
            this.btn_capture.Location = new System.Drawing.Point(428, 541);
            this.btn_capture.Name = "btn_capture";
            this.btn_capture.Size = new System.Drawing.Size(75, 23);
            this.btn_capture.TabIndex = 16;
            this.btn_capture.Text = "Capture";
            this.btn_capture.UseVisualStyleBackColor = true;
            // 
            // btn_Filter
            // 
            this.btn_Filter.Location = new System.Drawing.Point(509, 541);
            this.btn_Filter.Name = "btn_Filter";
            this.btn_Filter.Size = new System.Drawing.Size(75, 23);
            this.btn_Filter.TabIndex = 17;
            this.btn_Filter.Text = "Filter";
            this.btn_Filter.UseVisualStyleBackColor = true;
            // 
            // btn_OCR
            // 
            this.btn_OCR.Location = new System.Drawing.Point(590, 541);
            this.btn_OCR.Name = "btn_OCR";
            this.btn_OCR.Size = new System.Drawing.Size(75, 23);
            this.btn_OCR.TabIndex = 18;
            this.btn_OCR.Text = "OCR";
            this.btn_OCR.UseVisualStyleBackColor = true;
            // 
            // text_raw
            // 
            this.text_raw.Location = new System.Drawing.Point(483, 229);
            this.text_raw.Multiline = true;
            this.text_raw.Name = "text_raw";
            this.text_raw.ReadOnly = true;
            this.text_raw.Size = new System.Drawing.Size(279, 306);
            this.text_raw.TabIndex = 19;
            // 
            // label_raw
            // 
            this.label_raw.AutoSize = true;
            this.label_raw.Location = new System.Drawing.Point(448, 229);
            this.label_raw.Name = "label_raw";
            this.label_raw.Size = new System.Drawing.Size(29, 13);
            this.label_raw.TabIndex = 20;
            this.label_raw.Text = "Raw";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(800, 652);
            this.Controls.Add(this.label_raw);
            this.Controls.Add(this.text_raw);
            this.Controls.Add(this.btn_OCR);
            this.Controls.Add(this.btn_Filter);
            this.Controls.Add(this.btn_capture);
            this.Controls.Add(this.label_statSub4);
            this.Controls.Add(this.text_statSub4);
            this.Controls.Add(this.label_statSub3);
            this.Controls.Add(this.label_statSub2);
            this.Controls.Add(this.label_statSub1);
            this.Controls.Add(this.label_statMain);
            this.Controls.Add(this.label_Type);
            this.Controls.Add(this.label_Set);
            this.Controls.Add(this.text_statSub3);
            this.Controls.Add(this.text_statSub2);
            this.Controls.Add(this.text_statSub1);
            this.Controls.Add(this.text_statMain);
            this.Controls.Add(this.text_Type);
            this.Controls.Add(this.text_Set);
            this.Controls.Add(this.image_preview);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.image_preview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox image_preview;
        private System.Windows.Forms.TextBox text_Set;
        private System.Windows.Forms.TextBox text_Type;
        private System.Windows.Forms.TextBox text_statMain;
        private System.Windows.Forms.TextBox text_statSub1;
        private System.Windows.Forms.TextBox text_statSub2;
        private System.Windows.Forms.TextBox text_statSub3;
        private System.Windows.Forms.Label label_Set;
        private System.Windows.Forms.Label label_Type;
        private System.Windows.Forms.Label label_statMain;
        private System.Windows.Forms.Label label_statSub1;
        private System.Windows.Forms.Label label_statSub2;
        private System.Windows.Forms.Label label_statSub3;
        private System.Windows.Forms.Label label_statSub4;
        private System.Windows.Forms.TextBox text_statSub4;
        private System.Windows.Forms.Button btn_capture;
        private System.Windows.Forms.Button btn_Filter;
        private System.Windows.Forms.Button btn_OCR;
        private System.Windows.Forms.TextBox text_raw;
        private System.Windows.Forms.Label label_raw;
    }
}

