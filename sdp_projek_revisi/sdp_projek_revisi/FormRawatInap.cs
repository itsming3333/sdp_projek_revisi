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
    public partial class FormRawatInap : Form
    {
        Form1 mainParent;
        public FormRawatInap()
        {
            InitializeComponent();
        }

        

        private void FormRawatInap_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
        }
        public void setMainParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }
    }
}
