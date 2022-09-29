using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SystemApps9_1Dll;

namespace SystemApps9_2
{
    public partial class MainForm : Form
    {
        List<User> users;
        public MainForm()
        {
            InitializeComponent();
            users = new List<User>();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(edUserName.Text))
                return;
            lsbUsers.DataSource = null;
            users.Add(new User() { Name = edUserName.Text });
            lsbUsers.DataSource = users;
        }
    }
}
