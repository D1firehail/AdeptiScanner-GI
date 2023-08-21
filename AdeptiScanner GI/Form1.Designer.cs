
namespace AdeptiScanner_GI
{
    partial class ScannerForm
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
            this.btn_capture = new System.Windows.Forms.Button();
            this.btn_OCR = new System.Windows.Forms.Button();
            this.text_full = new System.Windows.Forms.TextBox();
            this.label_full = new System.Windows.Forms.Label();
            this.checkbox_OCRcapture = new System.Windows.Forms.CheckBox();
            this.checkbox_saveImages = new System.Windows.Forms.CheckBox();
            this.button_auto = new System.Windows.Forms.Button();
            this.button_softCancel = new System.Windows.Forms.Button();
            this.button_hardCancel = new System.Windows.Forms.Button();
            this.button_resume = new System.Windows.Forms.Button();
            this.button_export = new System.Windows.Forms.Button();
            this.label_traveler = new System.Windows.Forms.Label();
            this.text_traveler = new System.Windows.Forms.TextBox();
            this.label_appversion = new System.Windows.Forms.Label();
            this.label_dataversion = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.exportSettings1 = new AdeptiScanner_GI.ExportSettings();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.artifactDetails1 = new AdeptiScanner_GI.ArtifactDetails();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.checkBox_ProcessHandleFeatures = new System.Windows.Forms.CheckBox();
            this.button_checkUpdateManual = new System.Windows.Forms.Button();
            this.checkBox_updateVersion = new System.Windows.Forms.CheckBox();
            this.checkBox_updateData = new System.Windows.Forms.CheckBox();
            this.button_resetSettings = new System.Windows.Forms.Button();
            this.label_recheckwait = new System.Windows.Forms.Label();
            this.text_RecheckWait = new System.Windows.Forms.TextBox();
            this.label_scrolltestwait = new System.Windows.Forms.Label();
            this.text_ScrollTestWait = new System.Windows.Forms.TextBox();
            this.label_scrollsleepwait = new System.Windows.Forms.Label();
            this.label_clicksleepwait = new System.Windows.Forms.Label();
            this.text_ScrollSleepWait = new System.Windows.Forms.TextBox();
            this.text_clickSleepWait = new System.Windows.Forms.TextBox();
            this.button_loadArtifacts = new System.Windows.Forms.Button();
            this.label_wanderer = new System.Windows.Forms.Label();
            this.text_wanderer = new System.Windows.Forms.TextBox();
            this.checkbox_weaponMode = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.image_preview)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
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
            this.image_preview.Size = new System.Drawing.Size(407, 729);
            this.image_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.image_preview.TabIndex = 1;
            this.image_preview.TabStop = false;
            // 
            // btn_capture
            // 
            this.btn_capture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_capture.Location = new System.Drawing.Point(493, 689);
            this.btn_capture.Name = "btn_capture";
            this.btn_capture.Size = new System.Drawing.Size(75, 23);
            this.btn_capture.TabIndex = 0;
            this.btn_capture.Text = "Capture";
            this.btn_capture.UseVisualStyleBackColor = true;
            this.btn_capture.Click += new System.EventHandler(this.btn_capture_Click);
            // 
            // btn_OCR
            // 
            this.btn_OCR.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_OCR.Enabled = false;
            this.btn_OCR.Location = new System.Drawing.Point(574, 689);
            this.btn_OCR.Name = "btn_OCR";
            this.btn_OCR.Size = new System.Drawing.Size(75, 23);
            this.btn_OCR.TabIndex = 1;
            this.btn_OCR.Text = "Read Stats";
            this.btn_OCR.UseVisualStyleBackColor = true;
            this.btn_OCR.Click += new System.EventHandler(this.btn_OCR_Click);
            // 
            // text_full
            // 
            this.text_full.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.text_full.Location = new System.Drawing.Point(493, 295);
            this.text_full.Multiline = true;
            this.text_full.Name = "text_full";
            this.text_full.ReadOnly = true;
            this.text_full.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.text_full.Size = new System.Drawing.Size(279, 313);
            this.text_full.TabIndex = 20;
            // 
            // label_full
            // 
            this.label_full.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_full.AutoSize = true;
            this.label_full.Location = new System.Drawing.Point(465, 298);
            this.label_full.Name = "label_full";
            this.label_full.Size = new System.Drawing.Size(23, 13);
            this.label_full.TabIndex = 20;
            this.label_full.Text = "Full";
            // 
            // checkbox_OCRcapture
            // 
            this.checkbox_OCRcapture.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkbox_OCRcapture.AutoSize = true;
            this.checkbox_OCRcapture.Checked = true;
            this.checkbox_OCRcapture.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkbox_OCRcapture.Location = new System.Drawing.Point(492, 614);
            this.checkbox_OCRcapture.Name = "checkbox_OCRcapture";
            this.checkbox_OCRcapture.Size = new System.Drawing.Size(102, 17);
            this.checkbox_OCRcapture.TabIndex = 21;
            this.checkbox_OCRcapture.Text = "Capture on read";
            this.checkbox_OCRcapture.UseVisualStyleBackColor = true;
            this.checkbox_OCRcapture.CheckedChanged += new System.EventHandler(this.checkbox_OCRcapture_CheckedChanged);
            // 
            // checkbox_saveImages
            // 
            this.checkbox_saveImages.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkbox_saveImages.AutoSize = true;
            this.checkbox_saveImages.Location = new System.Drawing.Point(592, 614);
            this.checkbox_saveImages.Name = "checkbox_saveImages";
            this.checkbox_saveImages.Size = new System.Drawing.Size(87, 17);
            this.checkbox_saveImages.TabIndex = 22;
            this.checkbox_saveImages.Text = "Save images";
            this.checkbox_saveImages.UseVisualStyleBackColor = true;
            this.checkbox_saveImages.CheckedChanged += new System.EventHandler(this.checkbox_saveImages_CheckedChanged);
            // 
            // button_auto
            // 
            this.button_auto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_auto.Enabled = false;
            this.button_auto.Location = new System.Drawing.Point(655, 689);
            this.button_auto.Name = "button_auto";
            this.button_auto.Size = new System.Drawing.Size(75, 23);
            this.button_auto.TabIndex = 2;
            this.button_auto.Text = "Start Auto";
            this.button_auto.UseVisualStyleBackColor = true;
            this.button_auto.Click += new System.EventHandler(this.button_auto_Click);
            // 
            // button_softCancel
            // 
            this.button_softCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_softCancel.Enabled = false;
            this.button_softCancel.Location = new System.Drawing.Point(493, 746);
            this.button_softCancel.Name = "button_softCancel";
            this.button_softCancel.Size = new System.Drawing.Size(153, 23);
            this.button_softCancel.TabIndex = 5;
            this.button_softCancel.Text = "Stop after processing";
            this.button_softCancel.UseVisualStyleBackColor = true;
            this.button_softCancel.Click += new System.EventHandler(this.button_softCancel_Click);
            // 
            // button_hardCancel
            // 
            this.button_hardCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_hardCancel.Enabled = false;
            this.button_hardCancel.Location = new System.Drawing.Point(574, 718);
            this.button_hardCancel.Name = "button_hardCancel";
            this.button_hardCancel.Size = new System.Drawing.Size(75, 23);
            this.button_hardCancel.TabIndex = 4;
            this.button_hardCancel.Text = "Stop now";
            this.button_hardCancel.UseVisualStyleBackColor = true;
            this.button_hardCancel.Click += new System.EventHandler(this.button_hardCancel_Click);
            // 
            // button_resume
            // 
            this.button_resume.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_resume.Enabled = false;
            this.button_resume.Location = new System.Drawing.Point(493, 718);
            this.button_resume.Name = "button_resume";
            this.button_resume.Size = new System.Drawing.Size(75, 23);
            this.button_resume.TabIndex = 3;
            this.button_resume.Text = "Resume Auto";
            this.button_resume.UseVisualStyleBackColor = true;
            this.button_resume.Click += new System.EventHandler(this.button_resume_Click);
            // 
            // button_export
            // 
            this.button_export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_export.Location = new System.Drawing.Point(655, 718);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(75, 51);
            this.button_export.TabIndex = 6;
            this.button_export.Text = "Export Results";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.button_export_Click);
            // 
            // label_traveler
            // 
            this.label_traveler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_traveler.AutoSize = true;
            this.label_traveler.Location = new System.Drawing.Point(490, 666);
            this.label_traveler.Name = "label_traveler";
            this.label_traveler.Size = new System.Drawing.Size(80, 13);
            this.label_traveler.TabIndex = 38;
            this.label_traveler.Text = "Traveler Name:";
            // 
            // text_traveler
            // 
            this.text_traveler.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.text_traveler.Location = new System.Drawing.Point(584, 663);
            this.text_traveler.Name = "text_traveler";
            this.text_traveler.Size = new System.Drawing.Size(190, 20);
            this.text_traveler.TabIndex = 24;
            this.text_traveler.TextChanged += new System.EventHandler(this.text_traveler_TextChanged);
            // 
            // label_appversion
            // 
            this.label_appversion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_appversion.AutoSize = true;
            this.label_appversion.Location = new System.Drawing.Point(64, 756);
            this.label_appversion.Name = "label_appversion";
            this.label_appversion.Size = new System.Drawing.Size(130, 13);
            this.label_appversion.TabIndex = 41;
            this.label_appversion.Text = "Program version: X.X.XXX";
            // 
            // label_dataversion
            // 
            this.label_dataversion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_dataversion.AutoSize = true;
            this.label_dataversion.Location = new System.Drawing.Point(200, 756);
            this.label_dataversion.Name = "label_dataversion";
            this.label_dataversion.Size = new System.Drawing.Size(104, 13);
            this.label_dataversion.TabIndex = 42;
            this.label_dataversion.Text = "Data version: XX.XX";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(12, 756);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(38, 13);
            this.linkLabel1.TabIndex = 7;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Github";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(425, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(360, 270);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.exportSettings1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(352, 244);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Export Filters";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // exportSettings1
            // 
            this.exportSettings1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.exportSettings1.Location = new System.Drawing.Point(0, 0);
            this.exportSettings1.Name = "exportSettings1";
            this.exportSettings1.Size = new System.Drawing.Size(349, 228);
            this.exportSettings1.TabIndex = 9;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.artifactDetails1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(352, 244);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Artifact Details";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // artifactDetails1
            // 
            this.artifactDetails1.Location = new System.Drawing.Point(0, 3);
            this.artifactDetails1.Name = "artifactDetails1";
            this.artifactDetails1.Size = new System.Drawing.Size(349, 228);
            this.artifactDetails1.TabIndex = 9;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.checkBox_ProcessHandleFeatures);
            this.tabPage3.Controls.Add(this.button_checkUpdateManual);
            this.tabPage3.Controls.Add(this.checkBox_updateVersion);
            this.tabPage3.Controls.Add(this.checkBox_updateData);
            this.tabPage3.Controls.Add(this.button_resetSettings);
            this.tabPage3.Controls.Add(this.label_recheckwait);
            this.tabPage3.Controls.Add(this.text_RecheckWait);
            this.tabPage3.Controls.Add(this.label_scrolltestwait);
            this.tabPage3.Controls.Add(this.text_ScrollTestWait);
            this.tabPage3.Controls.Add(this.label_scrollsleepwait);
            this.tabPage3.Controls.Add(this.label_clicksleepwait);
            this.tabPage3.Controls.Add(this.text_ScrollSleepWait);
            this.tabPage3.Controls.Add(this.text_clickSleepWait);
            this.tabPage3.Controls.Add(this.button_loadArtifacts);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(352, 244);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Advanced";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBox_ProcessHandleFeatures
            // 
            this.checkBox_ProcessHandleFeatures.AutoSize = true;
            this.checkBox_ProcessHandleFeatures.Checked = true;
            this.checkBox_ProcessHandleFeatures.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ProcessHandleFeatures.Location = new System.Drawing.Point(9, 156);
            this.checkBox_ProcessHandleFeatures.Name = "checkBox_ProcessHandleFeatures";
            this.checkBox_ProcessHandleFeatures.Size = new System.Drawing.Size(140, 17);
            this.checkBox_ProcessHandleFeatures.TabIndex = 16;
            this.checkBox_ProcessHandleFeatures.Text = "Process handle features";
            this.checkBox_ProcessHandleFeatures.UseVisualStyleBackColor = true;
            this.checkBox_ProcessHandleFeatures.CheckedChanged += new System.EventHandler(this.checkBox_ProcessHandleFeatures_CheckedChanged);
            // 
            // button_checkUpdateManual
            // 
            this.button_checkUpdateManual.Location = new System.Drawing.Point(226, 186);
            this.button_checkUpdateManual.Name = "button_checkUpdateManual";
            this.button_checkUpdateManual.Size = new System.Drawing.Size(117, 23);
            this.button_checkUpdateManual.TabIndex = 18;
            this.button_checkUpdateManual.Text = "Check for updates";
            this.button_checkUpdateManual.UseVisualStyleBackColor = true;
            this.button_checkUpdateManual.Click += new System.EventHandler(this.button_checkUpdateManual_Click);
            // 
            // checkBox_updateVersion
            // 
            this.checkBox_updateVersion.AutoSize = true;
            this.checkBox_updateVersion.Location = new System.Drawing.Point(9, 133);
            this.checkBox_updateVersion.Name = "checkBox_updateVersion";
            this.checkBox_updateVersion.Size = new System.Drawing.Size(133, 17);
            this.checkBox_updateVersion.TabIndex = 15;
            this.checkBox_updateVersion.Text = "Version Update Check";
            this.checkBox_updateVersion.UseVisualStyleBackColor = true;
            this.checkBox_updateVersion.CheckedChanged += new System.EventHandler(this.checkBox_updateVersion_CheckedChanged);
            // 
            // checkBox_updateData
            // 
            this.checkBox_updateData.AutoSize = true;
            this.checkBox_updateData.Location = new System.Drawing.Point(9, 110);
            this.checkBox_updateData.Name = "checkBox_updateData";
            this.checkBox_updateData.Size = new System.Drawing.Size(121, 17);
            this.checkBox_updateData.TabIndex = 14;
            this.checkBox_updateData.Text = "Data Update Check";
            this.checkBox_updateData.UseVisualStyleBackColor = true;
            this.checkBox_updateData.CheckedChanged += new System.EventHandler(this.checkBox_updateData_CheckedChanged);
            // 
            // button_resetSettings
            // 
            this.button_resetSettings.Location = new System.Drawing.Point(99, 186);
            this.button_resetSettings.Name = "button_resetSettings";
            this.button_resetSettings.Size = new System.Drawing.Size(118, 52);
            this.button_resetSettings.TabIndex = 17;
            this.button_resetSettings.Text = "Remove Saved Settings";
            this.button_resetSettings.UseVisualStyleBackColor = true;
            this.button_resetSettings.Click += new System.EventHandler(this.button_resetSettings_Click);
            // 
            // label_recheckwait
            // 
            this.label_recheckwait.AutoSize = true;
            this.label_recheckwait.Location = new System.Drawing.Point(6, 87);
            this.label_recheckwait.Name = "label_recheckwait";
            this.label_recheckwait.Size = new System.Drawing.Size(73, 13);
            this.label_recheckwait.TabIndex = 8;
            this.label_recheckwait.Text = "RecheckWait";
            // 
            // text_RecheckWait
            // 
            this.text_RecheckWait.Location = new System.Drawing.Point(109, 84);
            this.text_RecheckWait.Name = "text_RecheckWait";
            this.text_RecheckWait.Size = new System.Drawing.Size(100, 20);
            this.text_RecheckWait.TabIndex = 13;
            // 
            // label_scrolltestwait
            // 
            this.label_scrolltestwait.AutoSize = true;
            this.label_scrolltestwait.Location = new System.Drawing.Point(6, 61);
            this.label_scrolltestwait.Name = "label_scrolltestwait";
            this.label_scrolltestwait.Size = new System.Drawing.Size(76, 13);
            this.label_scrolltestwait.TabIndex = 6;
            this.label_scrolltestwait.Text = "ScrollTestWait";
            // 
            // text_ScrollTestWait
            // 
            this.text_ScrollTestWait.Location = new System.Drawing.Point(109, 58);
            this.text_ScrollTestWait.Name = "text_ScrollTestWait";
            this.text_ScrollTestWait.Size = new System.Drawing.Size(100, 20);
            this.text_ScrollTestWait.TabIndex = 12;
            // 
            // label_scrollsleepwait
            // 
            this.label_scrollsleepwait.AutoSize = true;
            this.label_scrollsleepwait.Location = new System.Drawing.Point(6, 35);
            this.label_scrollsleepwait.Name = "label_scrollsleepwait";
            this.label_scrollsleepwait.Size = new System.Drawing.Size(82, 13);
            this.label_scrollsleepwait.TabIndex = 4;
            this.label_scrollsleepwait.Text = "ScrollSleepWait";
            // 
            // label_clicksleepwait
            // 
            this.label_clicksleepwait.AutoSize = true;
            this.label_clicksleepwait.Location = new System.Drawing.Point(6, 9);
            this.label_clicksleepwait.Name = "label_clicksleepwait";
            this.label_clicksleepwait.Size = new System.Drawing.Size(79, 13);
            this.label_clicksleepwait.TabIndex = 3;
            this.label_clicksleepwait.Text = "ClickSleepWait";
            // 
            // text_ScrollSleepWait
            // 
            this.text_ScrollSleepWait.Location = new System.Drawing.Point(109, 32);
            this.text_ScrollSleepWait.Name = "text_ScrollSleepWait";
            this.text_ScrollSleepWait.Size = new System.Drawing.Size(100, 20);
            this.text_ScrollSleepWait.TabIndex = 11;
            // 
            // text_clickSleepWait
            // 
            this.text_clickSleepWait.Location = new System.Drawing.Point(109, 6);
            this.text_clickSleepWait.Name = "text_clickSleepWait";
            this.text_clickSleepWait.Size = new System.Drawing.Size(100, 20);
            this.text_clickSleepWait.TabIndex = 10;
            // 
            // button_loadArtifacts
            // 
            this.button_loadArtifacts.Location = new System.Drawing.Point(225, 215);
            this.button_loadArtifacts.Name = "button_loadArtifacts";
            this.button_loadArtifacts.Size = new System.Drawing.Size(118, 23);
            this.button_loadArtifacts.TabIndex = 19;
            this.button_loadArtifacts.Text = "Load Artifact File";
            this.button_loadArtifacts.UseVisualStyleBackColor = true;
            this.button_loadArtifacts.Click += new System.EventHandler(this.button_loadArtifacts_Click);
            // 
            // label_wanderer
            // 
            this.label_wanderer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_wanderer.AutoSize = true;
            this.label_wanderer.Location = new System.Drawing.Point(490, 640);
            this.label_wanderer.Name = "label_wanderer";
            this.label_wanderer.Size = new System.Drawing.Size(88, 13);
            this.label_wanderer.TabIndex = 47;
            this.label_wanderer.Text = "Wanderer Name:";
            // 
            // text_wanderer
            // 
            this.text_wanderer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.text_wanderer.Location = new System.Drawing.Point(584, 637);
            this.text_wanderer.Name = "text_wanderer";
            this.text_wanderer.Size = new System.Drawing.Size(190, 20);
            this.text_wanderer.TabIndex = 23;
            this.text_wanderer.TextChanged += new System.EventHandler(this.text_wanderer_TextChanged);
            // 
            // checkbox_weaponMode
            // 
            this.checkbox_weaponMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.checkbox_weaponMode.AutoSize = true;
            this.checkbox_weaponMode.Location = new System.Drawing.Point(676, 614);
            this.checkbox_weaponMode.Name = "checkbox_weaponMode";
            this.checkbox_weaponMode.Size = new System.Drawing.Size(96, 17);
            this.checkbox_weaponMode.TabIndex = 48;
            this.checkbox_weaponMode.Text = "Weapon mode";
            this.checkbox_weaponMode.UseVisualStyleBackColor = true;
            this.checkbox_weaponMode.CheckedChanged += new System.EventHandler(this.checkbox_weaponMode_CheckedChanged);
            // 
            // ScannerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightGray;
            this.ClientSize = new System.Drawing.Size(784, 781);
            this.Controls.Add(this.checkbox_weaponMode);
            this.Controls.Add(this.label_wanderer);
            this.Controls.Add(this.text_wanderer);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label_dataversion);
            this.Controls.Add(this.label_appversion);
            this.Controls.Add(this.label_traveler);
            this.Controls.Add(this.text_traveler);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.button_resume);
            this.Controls.Add(this.button_hardCancel);
            this.Controls.Add(this.button_softCancel);
            this.Controls.Add(this.button_auto);
            this.Controls.Add(this.checkbox_saveImages);
            this.Controls.Add(this.checkbox_OCRcapture);
            this.Controls.Add(this.label_full);
            this.Controls.Add(this.text_full);
            this.Controls.Add(this.btn_OCR);
            this.Controls.Add(this.btn_capture);
            this.Controls.Add(this.image_preview);
            this.MinimumSize = new System.Drawing.Size(650, 600);
            this.Name = "ScannerForm";
            this.Text = "AdeptiScanner_GI";
            ((System.ComponentModel.ISupportInitialize)(this.image_preview)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox image_preview;
        private System.Windows.Forms.Button btn_capture;
        private System.Windows.Forms.Button btn_OCR;
        private System.Windows.Forms.TextBox text_full;
        private System.Windows.Forms.Label label_full;
        private System.Windows.Forms.CheckBox checkbox_OCRcapture;
        private System.Windows.Forms.CheckBox checkbox_saveImages;
        private System.Windows.Forms.Button button_auto;
        private System.Windows.Forms.Button button_softCancel;
        private System.Windows.Forms.Button button_hardCancel;
        private System.Windows.Forms.Button button_resume;
        private System.Windows.Forms.Button button_export;
        private System.Windows.Forms.Label label_traveler;
        private System.Windows.Forms.TextBox text_traveler;
        private System.Windows.Forms.Label label_appversion;
        private System.Windows.Forms.Label label_dataversion;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private ExportSettings exportSettings1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button_loadArtifacts;
        private ArtifactDetails artifactDetails1;
        private System.Windows.Forms.Label label_scrollsleepwait;
        private System.Windows.Forms.Label label_clicksleepwait;
        private System.Windows.Forms.TextBox text_ScrollSleepWait;
        private System.Windows.Forms.TextBox text_clickSleepWait;
        private System.Windows.Forms.Label label_recheckwait;
        private System.Windows.Forms.TextBox text_RecheckWait;
        private System.Windows.Forms.Label label_scrolltestwait;
        private System.Windows.Forms.TextBox text_ScrollTestWait;
        private System.Windows.Forms.Button button_resetSettings;
        private System.Windows.Forms.CheckBox checkBox_updateData;
        private System.Windows.Forms.CheckBox checkBox_updateVersion;
        private System.Windows.Forms.Button button_checkUpdateManual;
        private System.Windows.Forms.Label label_wanderer;
        private System.Windows.Forms.TextBox text_wanderer;
        private System.Windows.Forms.CheckBox checkBox_ProcessHandleFeatures;
        private System.Windows.Forms.CheckBox checkbox_weaponMode;
    }
}

