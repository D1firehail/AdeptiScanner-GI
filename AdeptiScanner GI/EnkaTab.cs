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
    public partial class EnkaTab : UserControl
    {
        public EnkaTab()
        {
            InitializeComponent();
        }

        private void btn_Fetch_Click(object sender, EventArgs e)
        {
            string uid = string.Copy(text_UID.Text);
            EnkaApi.RequestUid(uid);
        }
    }
}
