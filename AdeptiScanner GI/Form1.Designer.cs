
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
            image_preview = new System.Windows.Forms.PictureBox();
            btn_capture = new System.Windows.Forms.Button();
            btn_OCR = new System.Windows.Forms.Button();
            text_full = new System.Windows.Forms.TextBox();
            label_full = new System.Windows.Forms.Label();
            checkbox_OCRcapture = new System.Windows.Forms.CheckBox();
            checkbox_saveImages = new System.Windows.Forms.CheckBox();
            button_auto = new System.Windows.Forms.Button();
            button_softCancel = new System.Windows.Forms.Button();
            button_hardCancel = new System.Windows.Forms.Button();
            button_resume = new System.Windows.Forms.Button();
            button_export = new System.Windows.Forms.Button();
            label_traveler = new System.Windows.Forms.Label();
            text_traveler = new System.Windows.Forms.TextBox();
            label_appversion = new System.Windows.Forms.Label();
            label_dataversion = new System.Windows.Forms.Label();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            tabControl1 = new System.Windows.Forms.TabControl();
            tabPage1 = new System.Windows.Forms.TabPage();
            exportSettings1 = new ExportSettings();
            tabPage2 = new System.Windows.Forms.TabPage();
            artifactDetails1 = new ArtifactDetails();
            tabPage3 = new System.Windows.Forms.TabPage();
            checkBox_ProcessHandleFeatures = new System.Windows.Forms.CheckBox();
            button_checkUpdateManual = new System.Windows.Forms.Button();
            checkBox_updateVersion = new System.Windows.Forms.CheckBox();
            checkBox_updateData = new System.Windows.Forms.CheckBox();
            button_resetSettings = new System.Windows.Forms.Button();
            label_recheckwait = new System.Windows.Forms.Label();
            text_RecheckWait = new System.Windows.Forms.TextBox();
            label_scrolltestwait = new System.Windows.Forms.Label();
            text_ScrollTestWait = new System.Windows.Forms.TextBox();
            label_scrollsleepwait = new System.Windows.Forms.Label();
            label_clicksleepwait = new System.Windows.Forms.Label();
            text_ScrollSleepWait = new System.Windows.Forms.TextBox();
            text_clickSleepWait = new System.Windows.Forms.TextBox();
            button_loadArtifacts = new System.Windows.Forms.Button();
            tabPage4 = new System.Windows.Forms.TabPage();
            enkaTab = new EnkaTab();
            label_wanderer = new System.Windows.Forms.Label();
            text_wanderer = new System.Windows.Forms.TextBox();
            checkbox_weaponMode = new System.Windows.Forms.CheckBox();
            checkBox_readHotkey = new System.Windows.Forms.CheckBox();
            label_admin = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)image_preview).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // image_preview
            // 
            image_preview.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            image_preview.Location = new System.Drawing.Point(14, 14);
            image_preview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            image_preview.MinimumSize = new System.Drawing.Size(117, 115);
            image_preview.Name = "image_preview";
            image_preview.Size = new System.Drawing.Size(475, 841);
            image_preview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            image_preview.TabIndex = 1;
            image_preview.TabStop = false;
            // 
            // btn_capture
            // 
            btn_capture.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btn_capture.Location = new System.Drawing.Point(575, 795);
            btn_capture.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_capture.Name = "btn_capture";
            btn_capture.Size = new System.Drawing.Size(88, 27);
            btn_capture.TabIndex = 0;
            btn_capture.Text = "Capture";
            btn_capture.UseVisualStyleBackColor = true;
            btn_capture.Click += btn_capture_Click;
            // 
            // btn_OCR
            // 
            btn_OCR.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            btn_OCR.Enabled = false;
            btn_OCR.Location = new System.Drawing.Point(670, 795);
            btn_OCR.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            btn_OCR.Name = "btn_OCR";
            btn_OCR.Size = new System.Drawing.Size(88, 27);
            btn_OCR.TabIndex = 1;
            btn_OCR.Text = "Read Stats";
            btn_OCR.UseVisualStyleBackColor = true;
            btn_OCR.Click += btn_OCR_Click;
            // 
            // text_full
            // 
            text_full.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            text_full.Location = new System.Drawing.Point(531, 340);
            text_full.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            text_full.Multiline = true;
            text_full.Name = "text_full";
            text_full.ReadOnly = true;
            text_full.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            text_full.Size = new System.Drawing.Size(369, 338);
            text_full.TabIndex = 20;
            // 
            // label_full
            // 
            label_full.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            label_full.AutoSize = true;
            label_full.Location = new System.Drawing.Point(497, 344);
            label_full.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_full.Name = "label_full";
            label_full.Size = new System.Drawing.Size(26, 15);
            label_full.TabIndex = 20;
            label_full.Text = "Full";
            // 
            // checkbox_OCRcapture
            // 
            checkbox_OCRcapture.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkbox_OCRcapture.AutoSize = true;
            checkbox_OCRcapture.Checked = true;
            checkbox_OCRcapture.CheckState = System.Windows.Forms.CheckState.Checked;
            checkbox_OCRcapture.Location = new System.Drawing.Point(539, 709);
            checkbox_OCRcapture.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkbox_OCRcapture.Name = "checkbox_OCRcapture";
            checkbox_OCRcapture.Size = new System.Drawing.Size(111, 19);
            checkbox_OCRcapture.TabIndex = 21;
            checkbox_OCRcapture.Text = "Capture on read";
            checkbox_OCRcapture.UseVisualStyleBackColor = true;
            checkbox_OCRcapture.CheckedChanged += checkbox_OCRcapture_CheckedChanged;
            // 
            // checkbox_saveImages
            // 
            checkbox_saveImages.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkbox_saveImages.AutoSize = true;
            checkbox_saveImages.Location = new System.Drawing.Point(668, 709);
            checkbox_saveImages.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkbox_saveImages.Name = "checkbox_saveImages";
            checkbox_saveImages.Size = new System.Drawing.Size(91, 19);
            checkbox_saveImages.TabIndex = 22;
            checkbox_saveImages.Text = "Save images";
            checkbox_saveImages.UseVisualStyleBackColor = true;
            checkbox_saveImages.CheckedChanged += checkbox_saveImages_CheckedChanged;
            // 
            // button_auto
            // 
            button_auto.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button_auto.Enabled = false;
            button_auto.Location = new System.Drawing.Point(764, 795);
            button_auto.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_auto.Name = "button_auto";
            button_auto.Size = new System.Drawing.Size(88, 27);
            button_auto.TabIndex = 2;
            button_auto.Text = "Start Auto";
            button_auto.UseVisualStyleBackColor = true;
            button_auto.Click += button_auto_Click;
            // 
            // button_softCancel
            // 
            button_softCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button_softCancel.Enabled = false;
            button_softCancel.Location = new System.Drawing.Point(575, 861);
            button_softCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_softCancel.Name = "button_softCancel";
            button_softCancel.Size = new System.Drawing.Size(178, 27);
            button_softCancel.TabIndex = 5;
            button_softCancel.Text = "Stop after processing";
            button_softCancel.UseVisualStyleBackColor = true;
            button_softCancel.Click += button_softCancel_Click;
            // 
            // button_hardCancel
            // 
            button_hardCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button_hardCancel.Enabled = false;
            button_hardCancel.Location = new System.Drawing.Point(670, 828);
            button_hardCancel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_hardCancel.Name = "button_hardCancel";
            button_hardCancel.Size = new System.Drawing.Size(88, 27);
            button_hardCancel.TabIndex = 4;
            button_hardCancel.Text = "Stop now";
            button_hardCancel.UseVisualStyleBackColor = true;
            button_hardCancel.Click += button_hardCancel_Click;
            // 
            // button_resume
            // 
            button_resume.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button_resume.Enabled = false;
            button_resume.Location = new System.Drawing.Point(575, 828);
            button_resume.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_resume.Name = "button_resume";
            button_resume.Size = new System.Drawing.Size(88, 27);
            button_resume.TabIndex = 3;
            button_resume.Text = "Resume Auto";
            button_resume.UseVisualStyleBackColor = true;
            button_resume.Click += button_resume_Click;
            // 
            // button_export
            // 
            button_export.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button_export.Location = new System.Drawing.Point(764, 828);
            button_export.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_export.Name = "button_export";
            button_export.Size = new System.Drawing.Size(88, 59);
            button_export.TabIndex = 6;
            button_export.Text = "Export Results";
            button_export.UseVisualStyleBackColor = true;
            button_export.Click += button_export_Click;
            // 
            // label_traveler
            // 
            label_traveler.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label_traveler.AutoSize = true;
            label_traveler.Location = new System.Drawing.Point(527, 768);
            label_traveler.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_traveler.Name = "label_traveler";
            label_traveler.Size = new System.Drawing.Size(85, 15);
            label_traveler.TabIndex = 38;
            label_traveler.Text = "Traveler Name:";
            // 
            // text_traveler
            // 
            text_traveler.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            text_traveler.Location = new System.Drawing.Point(637, 765);
            text_traveler.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            text_traveler.Name = "text_traveler";
            text_traveler.Size = new System.Drawing.Size(265, 23);
            text_traveler.TabIndex = 24;
            text_traveler.TextChanged += text_traveler_TextChanged;
            // 
            // label_appversion
            // 
            label_appversion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label_appversion.AutoSize = true;
            label_appversion.Location = new System.Drawing.Point(75, 872);
            label_appversion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_appversion.Name = "label_appversion";
            label_appversion.Size = new System.Drawing.Size(141, 15);
            label_appversion.TabIndex = 41;
            label_appversion.Text = "Program version: X.X.XXX";
            // 
            // label_dataversion
            // 
            label_dataversion.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label_dataversion.AutoSize = true;
            label_dataversion.Location = new System.Drawing.Point(233, 872);
            label_dataversion.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_dataversion.Name = "label_dataversion";
            label_dataversion.Size = new System.Drawing.Size(109, 15);
            label_dataversion.TabIndex = 42;
            label_dataversion.Text = "Data version: XX.XX";
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new System.Drawing.Point(14, 872);
            linkLabel1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(43, 15);
            linkLabel1.TabIndex = 7;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Github";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // tabControl1
            // 
            tabControl1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Location = new System.Drawing.Point(496, 14);
            tabControl1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(420, 312);
            tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(exportSettings1);
            tabPage1.Location = new System.Drawing.Point(4, 24);
            tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage1.Size = new System.Drawing.Size(412, 284);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Export Filters";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // exportSettings1
            // 
            exportSettings1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            exportSettings1.Location = new System.Drawing.Point(0, 0);
            exportSettings1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            exportSettings1.Name = "exportSettings1";
            exportSettings1.Size = new System.Drawing.Size(407, 263);
            exportSettings1.TabIndex = 9;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(artifactDetails1);
            tabPage2.Location = new System.Drawing.Point(4, 24);
            tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage2.Size = new System.Drawing.Size(412, 284);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Artifact Details";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // artifactDetails1
            // 
            artifactDetails1.Location = new System.Drawing.Point(0, 3);
            artifactDetails1.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            artifactDetails1.Name = "artifactDetails1";
            artifactDetails1.Size = new System.Drawing.Size(407, 263);
            artifactDetails1.TabIndex = 9;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(checkBox_ProcessHandleFeatures);
            tabPage3.Controls.Add(button_checkUpdateManual);
            tabPage3.Controls.Add(checkBox_updateVersion);
            tabPage3.Controls.Add(checkBox_updateData);
            tabPage3.Controls.Add(button_resetSettings);
            tabPage3.Controls.Add(label_recheckwait);
            tabPage3.Controls.Add(text_RecheckWait);
            tabPage3.Controls.Add(label_scrolltestwait);
            tabPage3.Controls.Add(text_ScrollTestWait);
            tabPage3.Controls.Add(label_scrollsleepwait);
            tabPage3.Controls.Add(label_clicksleepwait);
            tabPage3.Controls.Add(text_ScrollSleepWait);
            tabPage3.Controls.Add(text_clickSleepWait);
            tabPage3.Controls.Add(button_loadArtifacts);
            tabPage3.Location = new System.Drawing.Point(4, 24);
            tabPage3.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage3.Size = new System.Drawing.Size(412, 284);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Advanced";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // checkBox_ProcessHandleFeatures
            // 
            checkBox_ProcessHandleFeatures.AutoSize = true;
            checkBox_ProcessHandleFeatures.Checked = true;
            checkBox_ProcessHandleFeatures.CheckState = System.Windows.Forms.CheckState.Checked;
            checkBox_ProcessHandleFeatures.Location = new System.Drawing.Point(10, 180);
            checkBox_ProcessHandleFeatures.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_ProcessHandleFeatures.Name = "checkBox_ProcessHandleFeatures";
            checkBox_ProcessHandleFeatures.Size = new System.Drawing.Size(150, 19);
            checkBox_ProcessHandleFeatures.TabIndex = 16;
            checkBox_ProcessHandleFeatures.Text = "Process handle features";
            checkBox_ProcessHandleFeatures.UseVisualStyleBackColor = true;
            checkBox_ProcessHandleFeatures.CheckedChanged += checkBox_ProcessHandleFeatures_CheckedChanged;
            // 
            // button_checkUpdateManual
            // 
            button_checkUpdateManual.Location = new System.Drawing.Point(264, 215);
            button_checkUpdateManual.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_checkUpdateManual.Name = "button_checkUpdateManual";
            button_checkUpdateManual.Size = new System.Drawing.Size(136, 27);
            button_checkUpdateManual.TabIndex = 18;
            button_checkUpdateManual.Text = "Check for updates";
            button_checkUpdateManual.UseVisualStyleBackColor = true;
            button_checkUpdateManual.Click += button_checkUpdateManual_Click;
            // 
            // checkBox_updateVersion
            // 
            checkBox_updateVersion.AutoSize = true;
            checkBox_updateVersion.Location = new System.Drawing.Point(10, 153);
            checkBox_updateVersion.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_updateVersion.Name = "checkBox_updateVersion";
            checkBox_updateVersion.Size = new System.Drawing.Size(141, 19);
            checkBox_updateVersion.TabIndex = 15;
            checkBox_updateVersion.Text = "Version Update Check";
            checkBox_updateVersion.UseVisualStyleBackColor = true;
            checkBox_updateVersion.CheckedChanged += checkBox_updateVersion_CheckedChanged;
            // 
            // checkBox_updateData
            // 
            checkBox_updateData.AutoSize = true;
            checkBox_updateData.Location = new System.Drawing.Point(10, 127);
            checkBox_updateData.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_updateData.Name = "checkBox_updateData";
            checkBox_updateData.Size = new System.Drawing.Size(127, 19);
            checkBox_updateData.TabIndex = 14;
            checkBox_updateData.Text = "Data Update Check";
            checkBox_updateData.UseVisualStyleBackColor = true;
            checkBox_updateData.CheckedChanged += checkBox_updateData_CheckedChanged;
            // 
            // button_resetSettings
            // 
            button_resetSettings.Location = new System.Drawing.Point(115, 215);
            button_resetSettings.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_resetSettings.Name = "button_resetSettings";
            button_resetSettings.Size = new System.Drawing.Size(138, 60);
            button_resetSettings.TabIndex = 17;
            button_resetSettings.Text = "Remove Saved Settings";
            button_resetSettings.UseVisualStyleBackColor = true;
            button_resetSettings.Click += button_resetSettings_Click;
            // 
            // label_recheckwait
            // 
            label_recheckwait.AutoSize = true;
            label_recheckwait.Location = new System.Drawing.Point(7, 100);
            label_recheckwait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_recheckwait.Name = "label_recheckwait";
            label_recheckwait.Size = new System.Drawing.Size(75, 15);
            label_recheckwait.TabIndex = 8;
            label_recheckwait.Text = "RecheckWait";
            // 
            // text_RecheckWait
            // 
            text_RecheckWait.Location = new System.Drawing.Point(127, 97);
            text_RecheckWait.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            text_RecheckWait.Name = "text_RecheckWait";
            text_RecheckWait.Size = new System.Drawing.Size(116, 23);
            text_RecheckWait.TabIndex = 13;
            // 
            // label_scrolltestwait
            // 
            label_scrolltestwait.AutoSize = true;
            label_scrolltestwait.Location = new System.Drawing.Point(7, 70);
            label_scrolltestwait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_scrolltestwait.Name = "label_scrolltestwait";
            label_scrolltestwait.Size = new System.Drawing.Size(80, 15);
            label_scrolltestwait.TabIndex = 6;
            label_scrolltestwait.Text = "ScrollTestWait";
            // 
            // text_ScrollTestWait
            // 
            text_ScrollTestWait.Location = new System.Drawing.Point(127, 67);
            text_ScrollTestWait.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            text_ScrollTestWait.Name = "text_ScrollTestWait";
            text_ScrollTestWait.Size = new System.Drawing.Size(116, 23);
            text_ScrollTestWait.TabIndex = 12;
            // 
            // label_scrollsleepwait
            // 
            label_scrollsleepwait.AutoSize = true;
            label_scrollsleepwait.Location = new System.Drawing.Point(7, 40);
            label_scrollsleepwait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_scrollsleepwait.Name = "label_scrollsleepwait";
            label_scrollsleepwait.Size = new System.Drawing.Size(88, 15);
            label_scrollsleepwait.TabIndex = 4;
            label_scrollsleepwait.Text = "ScrollSleepWait";
            // 
            // label_clicksleepwait
            // 
            label_clicksleepwait.AutoSize = true;
            label_clicksleepwait.Location = new System.Drawing.Point(7, 10);
            label_clicksleepwait.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_clicksleepwait.Name = "label_clicksleepwait";
            label_clicksleepwait.Size = new System.Drawing.Size(85, 15);
            label_clicksleepwait.TabIndex = 3;
            label_clicksleepwait.Text = "ClickSleepWait";
            // 
            // text_ScrollSleepWait
            // 
            text_ScrollSleepWait.Location = new System.Drawing.Point(127, 37);
            text_ScrollSleepWait.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            text_ScrollSleepWait.Name = "text_ScrollSleepWait";
            text_ScrollSleepWait.Size = new System.Drawing.Size(116, 23);
            text_ScrollSleepWait.TabIndex = 11;
            // 
            // text_clickSleepWait
            // 
            text_clickSleepWait.Location = new System.Drawing.Point(127, 7);
            text_clickSleepWait.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            text_clickSleepWait.Name = "text_clickSleepWait";
            text_clickSleepWait.Size = new System.Drawing.Size(116, 23);
            text_clickSleepWait.TabIndex = 10;
            // 
            // button_loadArtifacts
            // 
            button_loadArtifacts.Location = new System.Drawing.Point(262, 248);
            button_loadArtifacts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            button_loadArtifacts.Name = "button_loadArtifacts";
            button_loadArtifacts.Size = new System.Drawing.Size(138, 27);
            button_loadArtifacts.TabIndex = 19;
            button_loadArtifacts.Text = "Load Artifact File";
            button_loadArtifacts.UseVisualStyleBackColor = true;
            button_loadArtifacts.Click += button_loadArtifacts_Click;
            // 
            // tabPage4
            // 
            tabPage4.Controls.Add(enkaTab);
            tabPage4.Location = new System.Drawing.Point(4, 24);
            tabPage4.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tabPage4.Size = new System.Drawing.Size(412, 284);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Characters";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // enkaTab
            // 
            enkaTab.Location = new System.Drawing.Point(0, 0);
            enkaTab.Margin = new System.Windows.Forms.Padding(5, 3, 5, 3);
            enkaTab.Name = "enkaTab";
            enkaTab.Size = new System.Drawing.Size(407, 263);
            enkaTab.TabIndex = 0;
            // 
            // label_wanderer
            // 
            label_wanderer.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            label_wanderer.AutoSize = true;
            label_wanderer.Location = new System.Drawing.Point(527, 738);
            label_wanderer.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_wanderer.Name = "label_wanderer";
            label_wanderer.Size = new System.Drawing.Size(96, 15);
            label_wanderer.TabIndex = 47;
            label_wanderer.Text = "Wanderer Name:";
            // 
            // text_wanderer
            // 
            text_wanderer.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            text_wanderer.Location = new System.Drawing.Point(637, 735);
            text_wanderer.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            text_wanderer.Name = "text_wanderer";
            text_wanderer.Size = new System.Drawing.Size(265, 23);
            text_wanderer.TabIndex = 23;
            text_wanderer.TextChanged += text_wanderer_TextChanged;
            // 
            // checkbox_weaponMode
            // 
            checkbox_weaponMode.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkbox_weaponMode.AutoSize = true;
            checkbox_weaponMode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            checkbox_weaponMode.Location = new System.Drawing.Point(794, 711);
            checkbox_weaponMode.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkbox_weaponMode.Name = "checkbox_weaponMode";
            checkbox_weaponMode.Size = new System.Drawing.Size(107, 17);
            checkbox_weaponMode.TabIndex = 48;
            checkbox_weaponMode.Text = "Weapon mode";
            checkbox_weaponMode.UseVisualStyleBackColor = true;
            checkbox_weaponMode.CheckedChanged += checkbox_weaponMode_CheckedChanged;
            // 
            // checkBox_readHotkey
            // 
            checkBox_readHotkey.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            checkBox_readHotkey.AutoSize = true;
            checkBox_readHotkey.Location = new System.Drawing.Point(539, 684);
            checkBox_readHotkey.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            checkBox_readHotkey.Name = "checkBox_readHotkey";
            checkBox_readHotkey.Size = new System.Drawing.Size(109, 19);
            checkBox_readHotkey.TabIndex = 49;
            checkBox_readHotkey.Text = "Read hotkey (P)";
            checkBox_readHotkey.UseVisualStyleBackColor = true;
            checkBox_readHotkey.CheckedChanged += checkBox_readHotkey_CheckedChanged;
            // 
            // label_admin
            // 
            label_admin.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            label_admin.AutoSize = true;
            label_admin.Location = new System.Drawing.Point(350, 872);
            label_admin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label_admin.Name = "label_admin";
            label_admin.Size = new System.Drawing.Size(84, 15);
            label_admin.TabIndex = 50;
            label_admin.Text = "Admin: XXXXX";
            // 
            // ScannerForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.LightGray;
            ClientSize = new System.Drawing.Size(915, 901);
            Controls.Add(label_admin);
            Controls.Add(checkBox_readHotkey);
            Controls.Add(checkbox_weaponMode);
            Controls.Add(label_wanderer);
            Controls.Add(text_wanderer);
            Controls.Add(tabControl1);
            Controls.Add(linkLabel1);
            Controls.Add(label_dataversion);
            Controls.Add(label_appversion);
            Controls.Add(label_traveler);
            Controls.Add(text_traveler);
            Controls.Add(button_export);
            Controls.Add(button_resume);
            Controls.Add(button_hardCancel);
            Controls.Add(button_softCancel);
            Controls.Add(button_auto);
            Controls.Add(checkbox_saveImages);
            Controls.Add(checkbox_OCRcapture);
            Controls.Add(label_full);
            Controls.Add(text_full);
            Controls.Add(btn_OCR);
            Controls.Add(btn_capture);
            Controls.Add(image_preview);
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(756, 686);
            Name = "ScannerForm";
            Text = "AdeptiScanner_GI";
            ((System.ComponentModel.ISupportInitialize)image_preview).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            tabPage4.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.TabPage tabPage4;
        private EnkaTab enkaTab;
        private System.Windows.Forms.CheckBox checkBox_readHotkey;
        private System.Windows.Forms.Label label_admin;
    }
}

