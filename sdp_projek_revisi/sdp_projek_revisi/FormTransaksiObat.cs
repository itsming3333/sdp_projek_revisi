using Oracle.DataAccess.Client;
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
    public partial class FormTransaksiObat : Form
    {
        String id_trans;
        String ctr;
        Form1 mainParent;
        public FormTransaksiObat()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormTransaksiObat_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
        }
        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            id_trans = textBox1.Text;
            OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM TRANSAKSI WHERE ID_TRANS='"+id_trans+"'", mainParent.oc);
            DataTable trans = new DataTable();
            oda.Fill(trans);

            if(trans.Rows.Count > 0)
            {
                enableData();
            }
            else
            {
                MessageBox.Show("Transaksi tidak ditemukan!\nMasukan kembali nomor transaksi yang benar.");
            }

        }

        private void enableData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT DISTINCT(DS.CTR_SUPPLY) AS INPUT, P.NAMA_PEGAWAI AS PENULIS FROM PEGAWAI P, DTRANS_SUPPLY DS WHERE ID_TRANS='"+id_trans+"' AND P.ID_PEGAWAI = DS.ID_PEGAWAI", mainParent.oc);
            DataTable ctr = new DataTable();
            oda.Fill(ctr);

            dataGridView1.DataSource = ctr;
            groupBox2.Enabled = true;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT DISTINCT(S.NAMA_SUPPLY) AS SUPPLY, DS.JUMLAH, DS.SUBTOTAL FROM SUPPLY S, DTRANS_SUPPLY DS WHERE S.ID_SUPPLY=DS.ID_SUPPLY AND DS.CTR_SUPPLY='"+dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()+"'", mainParent.oc);
            ctr = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            DataTable dtrans_obat = new DataTable();
            oda.Fill(dtrans_obat);
            dataGridView2.DataSource = dtrans_obat;

            oda = new OracleDataAdapter("SELECT * FROM DTRANS_SUPPLY WHERE ID_TRANS='"+id_trans+"' AND CTR_SUPPLY='"+ dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "'", mainParent.oc);
            DataTable dt = new DataTable();
            oda.Fill(dt);

            String ambil = dt.Rows[0].Field<String>(5);
            if(ambil == "n")
            {
                ambil = "Belum Diambil";
                label5.ForeColor = Color.Red;
            }
            else
            {
                ambil = "Sudah Diambil";
                label5.ForeColor = Color.Green;
            }

            String lunas = dt.Rows[0].Field<String>(7);
            if(lunas == "n")
            {
                lunas = "Belum Lunas";
                label6.ForeColor = Color.Red;
            }
            else
            {
                lunas = "LUNAS";
                label6.ForeColor = Color.Green;
            }

            label5.Text = ambil;
            label6.Text = lunas;
            if (ambil == "Belum Diambil")
            {
                label5.ForeColor = Color.Red;
            }
            else
            {
                label5.ForeColor = Color.Green;
            }
            if (lunas == "Belum Lunas")
            {
                label6.ForeColor = Color.Red;
            }
            else
            {
                label6.ForeColor = Color.Green;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void reset()
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            groupBox2.Enabled = false;
            id_trans = "";
            ctr = "";
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("UPDATE DTRANS_SUPPLY SET STATUS_AMBIL='y' WHERE ID_TRANS='"+id_trans+"' AND CTR_SUPPLY = '"+ctr+"'", mainParent.oc);
                MessageBox.Show(cmd.CommandText);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Supply sukses diambil.");
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
