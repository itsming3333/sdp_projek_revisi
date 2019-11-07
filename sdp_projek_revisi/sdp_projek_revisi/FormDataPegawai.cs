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
    public partial class FormDataPegawai : Form
    {
        Form1 mainParent;
        public FormDataPegawai()
        {
            InitializeComponent();
            
        }

        private void FormDataPegawai_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            showData();
            reset();
        }

        private void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT P.NAMA_PEGAWAI AS NAMA, J.NAMA_JABATAN AS JABATAN FROM PEGAWAI P, JABATAN J WHERE P.ID_JABATAN = J.ID_JABATAN ORDER BY 2", mainParent.oc);
            DataTable pegawai = new DataTable();
            oda.Fill(pegawai);
            dataGridView1.DataSource = pegawai;

            oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR,  NAMA_RUANG AS NAMA FROM RUANG WHERE JENIS_RUANG='KANTOR'", mainParent.oc);
            DataTable ruang = new DataTable();
            oda.Fill(ruang);
            dataGridView2.DataSource = ruang;
        }

        private void reset()
        {
            groupBox1.Enabled = false;
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void Label25_Click(object sender, EventArgs e)
        {

        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }
    }
}
