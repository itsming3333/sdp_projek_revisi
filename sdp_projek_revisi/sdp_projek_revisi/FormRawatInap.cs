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
    public partial class FormRawatInap : Form
    {
        Form1 mainParent;
        public FormRawatInap()
        {
            InitializeComponent();
        }
        private void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER", mainParent.oc);
            DataTable member = new DataTable();
            oda.Fill(member);

            dataGridView1.DataSource = member;
            oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR, JENIS_RUANG AS JENIS, STATUS_RUANG AS STATUS FROM RUANG", mainParent.oc);
            DataTable ruang = new DataTable();
            oda.Fill(ruang);

            dataGridView2.DataSource = ruang;
        }       

        private void FormRawatInap_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            showData();
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
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

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
