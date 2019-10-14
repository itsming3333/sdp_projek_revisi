using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sdp_projek_revisi
{
    
    public partial class FormPengguna : Form
    {
        Form1 parent;
        public FormPengguna()
        {
            InitializeComponent();
        }

        private void FormPengguna_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            parent = (Form1)this.MdiParent;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            parent.login_form();
            this.Close();
        }
    }
}
