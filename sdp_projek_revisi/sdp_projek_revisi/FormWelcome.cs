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
    public partial class FormWelcome : Form
    {
        Form1 mainParent;
        public FormWelcome()
        {
            InitializeComponent();
        }

        private void FormWelcome_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
        }

        private void setParent(Form1 mainParent)
        {
            this.mainParent = mainParent;
        }
    }
}
