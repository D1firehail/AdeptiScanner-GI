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
    public partial class FirstStart : Form
    {
        public FirstStart()
        {
            InitializeComponent();
        }

        private void button_AllUpdates_Click(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.SetUpdatePreferences(true, true);
            }
            this.Close();
        }

        private void button_DataOnly_Click(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.SetUpdatePreferences(true, false);
            }
            this.Close();
        }

        private void button_VersionOnly_Click(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.SetUpdatePreferences(false, true);
            }
            this.Close();
        }

        private void button_NoUpdates_Click(object sender, EventArgs e)
        {
            if (ScannerForm.INSTANCE != null)
            {
                ScannerForm.INSTANCE.SetUpdatePreferences(false, false);
            }
            this.Close();
        }
    }
}
