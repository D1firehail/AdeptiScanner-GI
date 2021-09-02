
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
            this.image_preview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.image_preview.Location = new System.Drawing.Point(12, 12);
            this.image_preview.MinimumSize = new System.Drawing.Size(100, 100);
            this.image_preview.Name = "image_preview";
            this.image_preview.Size = new System.Drawing.Size(384, 509);
            this.image_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image_preview.TabIndex = 1;
            this.image_preview.TabStop = false;
            // 
            // text_Set
            // 
            this.text_Set.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_Set.Location = new System.Drawing.Point(493, 12);
            this.text_Set.Name = "text_Set";
            this.text_Set.ReadOnly = true;
            this.text_Set.Size = new System.Drawing.Size(279, 20);
            this.text_Set.TabIndex = 2;
            // 
            // text_Type
            // 
            this.text_Type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_Type.Location = new System.Drawing.Point(493, 38);
            this.text_Type.Name = "text_Type";
            this.text_Type.ReadOnly = true;
            this.text_Type.Size = new System.Drawing.Size(279, 20);
            this.text_Type.TabIndex = 3;
            // 
            // text_statMain
            // 
            this.text_statMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_statMain.Location = new System.Drawing.Point(493, 64);
            this.text_statMain.Name = "text_statMain";
            this.text_statMain.ReadOnly = true;
            this.text_statMain.Size = new System.Drawing.Size(279, 20);
            this.text_statMain.TabIndex = 4;
            // 
            // text_statSub1
            // 
            this.text_statSub1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_statSub1.Location = new System.Drawing.Point(493, 90);
            this.text_statSub1.Name = "text_statSub1";
            this.text_statSub1.ReadOnly = true;
            this.text_statSub1.Size = new System.Drawing.Size(279, 20);
            this.text_statSub1.TabIndex = 5;
            // 
            // text_statSub2
            // 
            this.text_statSub2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_statSub2.Location = new System.Drawing.Point(493, 116);
            this.text_statSub2.Name = "text_statSub2";
            this.text_statSub2.ReadOnly = true;
            this.text_statSub2.Size = new System.Drawing.Size(279, 20);
            this.text_statSub2.TabIndex = 6;
            // 
            // text_statSub3
            // 
            this.text_statSub3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_statSub3.Location = new System.Drawing.Point(493, 142);
            this.text_statSub3.Name = "text_statSub3";
            this.text_statSub3.ReadOnly = true;
            this.text_statSub3.Size = new System.Drawing.Size(279, 20);
            this.text_statSub3.TabIndex = 7;
            // 
            // text_statSub4
            // 
            this.text_statSub4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_statSub4.Location = new System.Drawing.Point(493, 168);
            this.text_statSub4.Name = "text_statSub4";
            this.text_statSub4.ReadOnly = true;
            this.text_statSub4.Size = new System.Drawing.Size(279, 20);
            this.text_statSub4.TabIndex = 14;
            // 
            // label_Set
            // 
            this.label_Set.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Set.AutoSize = true;
            this.label_Set.Location = new System.Drawing.Point(464, 19);
            this.label_Set.Name = "label_Set";
            this.label_Set.Size = new System.Drawing.Size(23, 13);
            this.label_Set.TabIndex = 8;
            this.label_Set.Text = "Set";
            // 
            // label_Type
            // 
            this.label_Type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_Type.AutoSize = true;
            this.label_Type.Location = new System.Drawing.Point(456, 45);
            this.label_Type.Name = "label_Type";
            this.label_Type.Size = new System.Drawing.Size(31, 13);
            this.label_Type.TabIndex = 9;
            this.label_Type.Text = "Type";
            // 
            // label_statMain
            // 
            this.label_statMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_statMain.AutoSize = true;
            this.label_statMain.Location = new System.Drawing.Point(435, 71);
            this.label_statMain.Name = "label_statMain";
            this.label_statMain.Size = new System.Drawing.Size(52, 13);
            this.label_statMain.TabIndex = 10;
            this.label_statMain.Text = "Main Stat";
            // 
            // label_statSub1
            // 
            this.label_statSub1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_statSub1.AutoSize = true;
            this.label_statSub1.Location = new System.Drawing.Point(435, 97);
            this.label_statSub1.Name = "label_statSub1";
            this.label_statSub1.Size = new System.Drawing.Size(52, 13);
            this.label_statSub1.TabIndex = 11;
            this.label_statSub1.Text = "Substat 1";
            // 
            // label_statSub2
            // 
            this.label_statSub2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_statSub2.AutoSize = true;
            this.label_statSub2.Location = new System.Drawing.Point(435, 123);
            this.label_statSub2.Name = "label_statSub2";
            this.label_statSub2.Size = new System.Drawing.Size(52, 13);
            this.label_statSub2.TabIndex = 12;
            this.label_statSub2.Text = "Substat 2";
            // 
            // label_statSub3
            // 
            this.label_statSub3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_statSub3.AutoSize = true;
            this.label_statSub3.Location = new System.Drawing.Point(435, 149);
            this.label_statSub3.Name = "label_statSub3";
            this.label_statSub3.Size = new System.Drawing.Size(52, 13);
            this.label_statSub3.TabIndex = 13;
            this.label_statSub3.Text = "Substat 3";
            // 
            // label_statSub4
            // 
            this.label_statSub4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_statSub4.AutoSize = true;
            this.label_statSub4.Location = new System.Drawing.Point(435, 175);
            this.label_statSub4.Name = "label_statSub4";
            this.label_statSub4.Size = new System.Drawing.Size(52, 13);
            this.label_statSub4.TabIndex = 15;
            this.label_statSub4.Text = "Substat 4";
            // 
            // btn_capture
            // 
            this.btn_capture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_capture.Location = new System.Drawing.Point(535, 516);
            this.btn_capture.Name = "btn_capture";
            this.btn_capture.Size = new System.Drawing.Size(75, 23);
            this.btn_capture.TabIndex = 16;
            this.btn_capture.Text = "Capture";
            this.btn_capture.UseVisualStyleBackColor = true;
            this.btn_capture.Click += new System.EventHandler(this.btn_capture_Click);
            // 
            // btn_Filter
            // 
            this.btn_Filter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Filter.Location = new System.Drawing.Point(616, 516);
            this.btn_Filter.Name = "btn_Filter";
            this.btn_Filter.Size = new System.Drawing.Size(75, 23);
            this.btn_Filter.TabIndex = 17;
            this.btn_Filter.Text = "Filter";
            this.btn_Filter.UseVisualStyleBackColor = true;
            this.btn_Filter.Click += new System.EventHandler(this.btn_Filter_Click);
            // 
            // btn_OCR
            // 
            this.btn_OCR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OCR.Location = new System.Drawing.Point(697, 516);
            this.btn_OCR.Name = "btn_OCR";
            this.btn_OCR.Size = new System.Drawing.Size(75, 23);
            this.btn_OCR.TabIndex = 18;
            this.btn_OCR.Text = "OCR";
            this.btn_OCR.UseVisualStyleBackColor = true;
            // 
            // text_raw
            // 
            this.text_raw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_raw.Location = new System.Drawing.Point(493, 204);
            this.text_raw.Multiline = true;
            this.text_raw.Name = "text_raw";
            this.text_raw.ReadOnly = true;
            this.text_raw.Size = new System.Drawing.Size(279, 306);
            this.text_raw.TabIndex = 19;
            // 
            // label_raw
            // 
            this.label_raw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_raw.AutoSize = true;
            this.label_raw.Location = new System.Drawing.Point(458, 204);
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
            this.ClientSize = new System.Drawing.Size(784, 561);
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
            this.MinimumSize = new System.Drawing.Size(600, 600);
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

