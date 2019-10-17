using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;

namespace sdp_projek_revisi
{
    public partial class FormTindakanDonor : Form
    {
        Form1 mainParent;
        public FormTindakanDonor()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormTindakanDonor_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            groupBox2.Enabled = false;
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }
    }
}
