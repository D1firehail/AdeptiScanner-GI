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
                trackBar_minlevel.Value = ScannerForm.INSTANCE.minLevel;
                label_minlevelnumber.Text = "" + trackBar_minlevel.Value;
                trackBar_maxlevel.Value = ScannerForm.INSTANCE.maxLevel;
                label_maxlevelnumber.Text = "" + trackBar_maxlevel.Value;
                trackBar_minrarity.Value = ScannerForm.INSTANCE.minRarity;
                label_minraritynumber.Text = "" + trackBar_minrarity.Value;
                trackBar_maxrarity.Value = ScannerForm.INSTANCE.maxRarity;
                label_maxraritynumber.Text = "" + trackBar_maxrarity.Value;
                checkBox_exportEquipped.Checked = ScannerForm.INSTANCE.exportAllEquipped;
                checkbox_exportTemplate.Checked = ScannerForm.INSTANCE.useTemplate;
            }
        }

        private void trackBar_minlevel_Scroll(object sender, EventArgs e)
        {
            label_minlevelnumber.Text = "" + trackBar_minlevel.Value;
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.minLevel = trackBar_minlevel.Value;
            }
        }

        private void trackBar_maxlevel_Scroll(object sender, EventArgs e)
        {
            label_maxlevelnumber.Text = "" + trackBar_maxlevel.Value;
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.maxLevel = trackBar_maxlevel.Value;
            }
        }

        private void trackBar_minrarity_Scroll(object sender, EventArgs e)
        {
            label_minraritynumber.Text = "" + trackBar_minrarity.Value; 
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.minRarity = trackBar_minrarity.Value;
            }
        }

        private void trackBar_maxrarity_Scroll(object sender, EventArgs e)
        {
            label_maxraritynumber.Text = "" + trackBar_maxrarity.Value; 
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.maxRarity = trackBar_maxrarity.Value;
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

                DialogResult dialogResult = MessageBox.Show("This will clear " + ScannerForm.INSTANCE.scannedItems.Count + " artifacts from the results." + Environment.NewLine + "Are you sure?", "Clear Results", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    ScannerForm.INSTANCE.AppendStatusText("Cleared " + ScannerForm.INSTANCE.scannedItems.Count + " items from results" + Environment.NewLine, false);
                    ScannerForm.INSTANCE.scannedItems.Clear();
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
    }
}
