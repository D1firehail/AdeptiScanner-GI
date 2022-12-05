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
    public partial class UpdatePrompt : Form
    {
        private string versionTitle = "";
        private string dataTitle = "";
        private string dataBody = "";

        public UpdatePrompt(string versionTitle, string versionBody, string dataTitle, string dataBody)
        {
            InitializeComponent();
            this.versionTitle = versionTitle;
            this.dataTitle = dataTitle;
            this.dataBody = dataBody;
            if (versionTitle.Length > 0)
            {
                label_versionUpdate.Text += " " + versionTitle;
                textBox_updateVersionBody.AppendText(versionBody);
            } else
            {
                panel_versionUpdate.Visible = false;
                panel_dataUpdate.Top = panel_versionUpdate.Top;
            }
            if (dataTitle.Length > 0)
            {
                label_dataUpdate.Text += " " + dataTitle;
            }
            else
            {
                panel_dataUpdate.Visible = false;
            }
        }

        private void versionDone()
        {
            if (dataTitle == "")
            {
                this.Close();
            }
            else
            {
                versionTitle = "";
                panel_versionUpdate.Visible = false;
                panel_dataUpdate.Top = panel_versionUpdate.Top;
            }
        }

        private void dataDone()
        {
            if (versionTitle == "")
            {
                this.Close();
            }
            else
            {
                dataTitle = "";
                panel_dataUpdate.Visible = false;
            }
        }

        private void button_OpenRelease_Click(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.readyVersionUpdate();
            }
            versionDone();
        }

        private void button_ignoreVersionTemporary_Click(object sender, EventArgs e)
        {
            versionDone();
        }

        private void button_ignoreVersionPermanent_Click(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.setIgnoredVersions(versionTitle, "");
            }
            versionDone();
        }

        private void button_updateData_Click(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.executeDataUpdate(dataBody);
            }
            dataDone();
        }

        private void button_ignoreDataTemporary_Click(object sender, EventArgs e)
        {
            dataDone();
        }

        private void button_ignoreDataPermanent_Click(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.setIgnoredVersions("", dataTitle);
            }
            dataDone();
        }
    }
}
