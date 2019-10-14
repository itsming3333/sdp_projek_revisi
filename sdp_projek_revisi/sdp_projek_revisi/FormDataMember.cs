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
    public partial class FormDataMember : Form
    {
        public FormDataMember()
        {
            InitializeComponent();
        }

        private void FormDataMember_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            timer1.Start();
            comboBox1.SelectedIndex = 0;
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }
    }
}
