using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Windows.Forms;

namespace sdp_projek_revisi
{
    public partial class FormDonor : Form
    {
        Form1 mainParent;
        String id_selected_member = "";
        public FormDonor()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormDonor_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            showData();
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER", mainParent.oc);
            DataTable member = new DataTable();
            oda.Fill(member);
            dataGridView1.DataSource = member;

            oda = new OracleDataAdapter("SELECT TO_CHAR(D.TGL_DONOR,'DD/MM/YYYY') AS TANGGAL, S.NAMA_SUPPLY AS DONOR FROM DONOR D, MEMBER M, SUPPLY S WHERE D.ID_MEMBER = M.ID_MEMBER AND D.ID_SUPPLY = S.ID_SUPPLY", mainParent.oc);
            DataTable trans_donor = new DataTable();
            oda.Fill(trans_donor);
            dataGridView2.DataSource = trans_donor;
        }
    }
}
