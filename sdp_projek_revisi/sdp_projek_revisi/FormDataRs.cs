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
    public partial class FormDataRs : Form
    {
        Form1 mainParent;
        String id_selected_perawatan = "";
        public FormDataRs()
        {
            InitializeComponent();
        }

        private void FormDataRs_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            showData();
        }

        public void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NAMA_PERAWATAN AS NAMA, DESKRIPSI_PERAWATAN AS DESKRIPSI, HARGA_PERAWATAN AS HARGA FROM PERAWATAN", mainParent.oc);
            DataTable perawatan = new DataTable();
            oda.Fill(perawatan);

            dataGridView1.DataSource = perawatan;

            oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR, NAMA_RUANG AS NAMA, JENIS_RUANG AS JENIS FROM RUANG", mainParent.oc);
            DataTable ruang = new DataTable();
            oda.Fill(ruang);

            dataGridView2.DataSource = ruang;

            oda = new OracleDataAdapter("SELECT SH.HARI_SHIFT AS HARI, TO_CHAR(SH.WAKTU_MULAI,'HH24:MI:SS') AS MULAI, TO_CHAR(SH.WAKTU_SELESAI,'HH24:MI:SS') AS SELESAI, P.ID_PEGAWAI AS DOKTER FROM SHIFT_SPESIALIS SH, PEGAWAI P, PERAWATAN PR,JABATAN J, RUANG R WHERE SH.ID_PERAWATAN = '" + id_selected_perawatan+"' AND SH.ID_PERAWATAN = PR.ID_PERAWATAN AND SH.ID_PEGAWAI = P.ID_PEGAWAI AND SH.ID_RUANG = R.ID_RUANG AND J.ID_JABATAN = P.ID_JABATAN AND J.NAMA_JABATAN='DOKTER'", mainParent.oc);
            DataTable jadwalShift = new DataTable();
            oda.Fill(jadwalShift);

            dataGridView4.DataSource = jadwalShift;
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }
    }
}
