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
    public partial class FormRawatJalan : Form
    {
        String id_selected_member = "";
        String id_selected_trans = "";
        Form1 mainParent;
        public FormRawatJalan()
        {
            InitializeComponent();
        }
        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void FormRawatJalan_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            comboBox1.SelectedIndex = 0;
            showData();
        }

        private void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER", mainParent.oc);
            DataTable member = new DataTable();
            oda.Fill(member);
            dataGridView1.DataSource = member;

            oda = new OracleDataAdapter("SELECT TO_CHAR(T.TGL_KELUAR,'DD/MM/YYYY') AS KELUAR, P.NAMA_PERAWATAN AS UTAMA FROM TRANSAKSI T,DTRANS_PERAWATAN_INAP DP, PERAWATAN P WHERE T.ID_MEMBER='"+id_selected_member+"' AND T.ID_TRANS = DP.ID_TRANS AND DP.ID_PERAWATAN = P.ID_PERAWATAN AND DP.KETERANGAN_CHECKUP='UTAMA'", mainParent.oc);
            DataTable transaksi = new DataTable();
            oda.Fill(transaksi);
            dataGridView2.DataSource = transaksi;

            oda = new OracleDataAdapter("SELECT PR.NAMA_PERAWATAN AS PERAWATAN, P.NAMA_PEGAWAI AS DOKTER, R.NOMOR_RUANG AS RUANGAN FROM SHIFT_SPESIALIS SR, PERAWATAN PR, PEGAWAI P, RUANG R WHERE SR.ID_RUANG = R.ID_RUANG AND SR.ID_PERAWATAN = PR.ID_PERAWATAN AND SR.ID_PEGAWAI = P.ID_PEGAWAI", mainParent.oc);
            DataTable shift = new DataTable();
            oda.Fill(shift);
            dataGridView3.DataSource = shift;

            oda = new OracleDataAdapter("SELECT S.NAMA_SUPPLY AS OBAT, D.JUMLAH AS JUMLAH FROM DTRANS_SUPPLY D, SUPPLY S WHERE D.ID_TRANS = '"+id_selected_trans+"' AND D.ID_SUPPLY = S.ID_SUPPLY", mainParent.oc);
            DataTable dtrans_obat = new DataTable();
            oda.Fill(dtrans_obat);
            dataGridView4.DataSource = dtrans_obat;

            oda = new OracleDataAdapter("SELECT PR.NAMA_PERAWATAN AS PERAWATAN, P.NAMA_PEGAWAI AS DOKTER FROM DTRANS_PERAWATAN_INAP D, PERAWATAN PR, PEGAWAI P WHERE D.ID_TRANS = '"+id_selected_trans+"' AND D.ID_PEGAWAI = P.ID_PEGAWAI AND D.ID_PERAWATAN = PR.ID_PERAWATAN", mainParent.oc);
            DataTable dtrans_rawat = new DataTable();
            oda.Fill(dtrans_rawat);
            dataGridView5.DataSource = dtrans_rawat;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void Label22_Click(object sender, EventArgs e)
        {

        }
    }
}
