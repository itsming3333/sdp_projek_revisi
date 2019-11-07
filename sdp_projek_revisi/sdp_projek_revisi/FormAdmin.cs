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
    public partial class FormAdmin : Form
    {
        Form1 parent;
        public FormAdmin()
        {
            InitializeComponent();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            parent = (Form1)this.MdiParent;
            label5.Text = "";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            parent.form_pengguna();
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == parent.passAdmin)
            {
                parent.login_as("ADMIN");
                this.Close();
            }
            else
            {
                label5.Text = "Kode administrasi salah!";
            }
        }
    }
}
