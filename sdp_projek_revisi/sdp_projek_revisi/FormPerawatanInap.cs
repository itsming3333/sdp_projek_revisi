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
    public partial class FormPerawatanInap : Form
    {
        Form1 mainParent;
        public FormPerawatanInap()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        public void cekCheckbox()
        {
            if(checkBox1.Checked == false)
            {
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox2.Enabled = true;
            }
            if (checkBox2.Checked == false)
            {
                groupBox3.Enabled = false;
            }
            else
            {
                groupBox3.Enabled = true;
            }
            if (checkBox3.Checked == false)
            {
                groupBox4.Enabled = false;
            }
            else
            {
                groupBox4.Enabled = true;
            }
        }
        private void FormPerawatanInap_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            disableAll();
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            cekCheckbox();
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            cekCheckbox();
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            cekCheckbox();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void disableAll()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            cekCheckbox();
            textBox1.Enabled = false;
            textBox5.Enabled = false;
            radioButton1.Checked = true;
            textBox2.Enabled = false;
            groupBox5.Enabled = false;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GroupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
