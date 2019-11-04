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
    public partial class FormNewPegawai : Form
    {
        Form1 mainParent;
        public FormNewPegawai()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormNewPegawai_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            timer1.Start();
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }
    }
}
