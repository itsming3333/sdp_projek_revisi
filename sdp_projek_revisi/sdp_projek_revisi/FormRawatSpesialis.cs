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
    public partial class FormRawatSpesialis : Form
    {
        Form1 mainParent;
        public FormRawatSpesialis()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }
        public void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER", mainParent.oc);
            DataTable member = new DataTable();
            oda.Fill(member);
            dataGridView1.DataSource = member;

            oda = new OracleDataAdapter("SELECT PR.NAMA_PERAWATAN AS PERAWATAN, P.NAMA_PEGAWAI AS DOKTER, R.NOMOR_RUANG AS RUANGAN FROM SHIFT_SPESIALIS SR, PERAWATAN PR, PEGAWAI P, RUANG R WHERE SR.ID_RUANG = R.ID_RUANG AND SR.ID_PERAWATAN = PR.ID_PERAWATAN AND SR.ID_PEGAWAI = P.ID_PEGAWAI", mainParent.oc);
            DataTable shift = new DataTable();
            oda.Fill(shift);
            dataGridView2.DataSource = shift;
        }

        private void FormRawatSpesialis_Load(object sender, EventArgs e)
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
    }
}
