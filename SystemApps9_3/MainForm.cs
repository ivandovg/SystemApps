using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SystemApps9_3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //Environment.GetCommandLineArgs
            //var files = System.IO.Directory.GetFiles(@"C:\Program Files\Microsoft SQL Server\", "*.*", System.IO.SearchOption.AllDirectories);
            //lsbReg.Items.AddRange(files);
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            lsbReg.Items.Clear();
            if (string.IsNullOrEmpty(edPath.Text))
                return;

            RegistryKey key = Registry.CurrentUser.OpenSubKey(edPath.Text, false);
            if (key == null)
            {
                MessageBox.Show("Error open key");
                return;
            }

            foreach (string item in key.GetSubKeyNames())
            {
                lsbReg.Items.Add(item);
                var sk = key.OpenSubKey(item);
                foreach (string sub in sk.GetValueNames())
                {
                    lsbReg.Items.Add($"  >> {sub} = {sk.GetValue(sub)}");
                }
            }
        }
    }
}
