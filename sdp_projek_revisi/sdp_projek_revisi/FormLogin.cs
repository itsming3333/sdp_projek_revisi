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
    public partial class FormLogin : Form
    {
        Form1 parent;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.MdiParent;
            Dock = DockStyle.Fill;
            label4.Text = "";
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            parent.form_pengguna();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String jabatan = textBox1.Text;
            if (jabatan == "kasir")
            {
                parent.login_as(jabatan);
                this.Close();
            }
            else
            {
                label4.Text = "ID/Password anda salah!";
            }
        }
    }
}
