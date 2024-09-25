using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdeptiScanner_GI
{
    [ToolboxItem(true)]
    public partial class ExportSettings : UserControl
    {
        public ExportSettings()
        {
            InitializeComponent();
            if (ScannerForm.INSTANCE != null)
            {
                dpiIgnoringSlider_minlevel.Value = ScannerForm.INSTANCE.minLevel;
                label_minlevelnumber.Text = "" + dpiIgnoringSlider_minlevel.Value;
                dpiIgnoringSlider_maxlevel.Value = ScannerForm.INSTANCE.maxLevel;
                label_maxlevelnumber.Text = "" + dpiIgnoringSlider_maxlevel.Value;
                dpiIgnoringSlider_minrarity.Value = ScannerForm.INSTANCE.minRarity;
                label_minraritynumber.Text = "" + dpiIgnoringSlider_minrarity.Value;
                dpiIgnoringSlider_maxrarity.Value = ScannerForm.INSTANCE.maxRarity;
                label_maxraritynumber.Text = "" + dpiIgnoringSlider_maxrarity.Value;
                checkBox_exportEquipped.Checked = ScannerForm.INSTANCE.exportAllEquipped;
                checkbox_exportTemplate.Checked = ScannerForm.INSTANCE.useTemplate;
                checkBox_exportEquipStatus.Checked = ScannerForm.INSTANCE.exportEquipStatus;
            }
        }

        private void dpiIgnoringSlider_minlevel_Scroll(object sender, EventArgs e)
        {
            label_minlevelnumber.Text = "" + dpiIgnoringSlider_minlevel.Value;
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.minLevel = dpiIgnoringSlider_minlevel.Value;
            }
        }

        private void dpiIgnoringSlider_maxlevel_Scroll(object sender, EventArgs e)
        {
            label_maxlevelnumber.Text = "" + dpiIgnoringSlider_maxlevel.Value;
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.maxLevel = dpiIgnoringSlider_maxlevel.Value;
            }
        }

        private void dpiIgnoringSlider_minrarity_Scroll(object sender, EventArgs e)
        {
            label_minraritynumber.Text = "" + dpiIgnoringSlider_minrarity.Value;
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.minRarity = dpiIgnoringSlider_minrarity.Value;
            }
        }

        private void dpiIgnoringSlider_maxrarity_Scroll(object sender, EventArgs e)
        {
            label_maxraritynumber.Text = "" + dpiIgnoringSlider_maxrarity.Value;
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.maxRarity = dpiIgnoringSlider_maxrarity.Value;
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                if (ScannerForm.INSTANCE.autoRunning)
                {
                    ScannerForm.INSTANCE.AppendStatusText("Ignored, auto currently running" + Environment.NewLine, false);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show("This will clear " + ScannerForm.INSTANCE.scannedArtifacts.Count + " artifacts and " + ScannerForm.INSTANCE.scannedWeapons.Count + " weapons from the results." + Environment.NewLine + "Are you sure?", "Clear Results", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    ScannerForm.INSTANCE.AppendStatusText("Cleared " + ScannerForm.INSTANCE.scannedArtifacts.Count + " artifacts and " + ScannerForm.INSTANCE.scannedWeapons.Count + " from results" + Environment.NewLine, false);
                    ScannerForm.INSTANCE.scannedArtifacts.Clear();
                    ScannerForm.INSTANCE.scannedWeapons.Clear();
                }
            }
        }

        private void checkbox_exportTemplate_CheckedChanged(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.useTemplate = checkbox_exportTemplate.Checked;
            }
        }

        private void checkBox_exportEquipped_CheckedChanged(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.exportAllEquipped = checkBox_exportEquipped.Checked;
            }
        }

        private void checkBox_ExportEquipStatus_CheckedChanged(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.exportEquipStatus = checkBox_exportEquipStatus.Checked;
            }
        }
    }
}
