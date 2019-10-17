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
        Form1 mainParent;
        public FormRawatJalan()
        {
            InitializeComponent();
        }

        private void FormRawatJalan_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            comboBox1.SelectedIndex = 0;
        }

        private void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER", mainParent.oc);
            DataTable member = new DataTable();
            oda.Fill(member);

            oda = new OracleDataAdapter("SELECT TO_CHAR(T.TGL_KELUAR,'DD/MM/YYYY') AS KELUAR,  FROM TRANSAKSI T,DTRANS_PERAWATAN_INAP DP, PERAWATAN P WHERE T.ID_MEMBER='"+id_selected_member+"' AND T.ID_TRANS = DP.ID_TRANS AND DP.ID_PERAWATAN = P.ID_PERAWATAN AND DP.KETERANGAN_CHECKUP='UTAMA'", mainParent.oc);
            DataTable transaksi = new DataTable();
            oda.Fill(transaksi);
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
