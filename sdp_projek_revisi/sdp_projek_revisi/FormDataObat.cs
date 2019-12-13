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
    public partial class FormDataObat : Form
    {
        Form1 mainParent;
        public FormDataObat()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormDataObat_Load(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
            Dock = DockStyle.Fill;
            timer1.Start();
            showData();
        }

        private void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NAMA_SUPPLY AS OBAT, HARGA_SUPPLY AS HARGA, SATUAN FROM SUPPLY WHERE JENIS_SUPPLY='OBAT'", mainParent.oc);
            DataTable obat = new DataTable();
            oda.Fill(obat);
            dataGridView1.DataSource = obat;
        }

        public void setParent(Form1 frm)
        {
            this.mainParent = frm;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM SUPPLY WHERE NAMA_SUPPLY='" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + "' AND JENIS_SUPPLY='OBAT'", mainParent.oc);
                DataTable sObat = new DataTable();
                oda.Fill(sObat);

                label3.Text = sObat.Rows[0].Field<String>(0);
                textBox2.Text = sObat.Rows[0].Field<String>(1);
                textBox8.Text = sObat.Rows[0].Field<String>(2);
                numericUpDown1.Value = Convert.ToInt32(sObat.Rows[0].Field<Int64>(3));
                textBox9.Text = sObat.Rows[0].Field<String>(5);
                groupBox1.Enabled = true;
            }
            catch (Exception ex)
            {}
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void clear()
        {
            label3.Text = "ID Obat";
            textBox2.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            numericUpDown1.Value = 0;
            groupBox1.Enabled = false;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda yakin ingin menghapus obat ?", "Hapus Obat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("DELETE FROM SUPPLY WHERE ID_SUPPLY='" + label3.Text + "'", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil menghapus obat.");
                    showData();
                    clear();
                }
                catch (Exception)
                {}
            }
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String id = label3.Text;
            String nama = textBox2.Text;
            String desc = textBox8.Text;
            String harga = numericUpDown1.Value.ToString();
            String satuan = textBox9.Text;

            if (MessageBox.Show("Apakah anda yakin ingin mengganti data obat ?", "Edit Obat", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("UPDATE SUPPLY SET NAMA_SUPPLY='"+nama+"', DESKRIPSI_SUPPLY='"+desc+"', HARGA_SUPPLY="+harga+", SATUAN='"+satuan+"' WHERE ID_SUPPLY='"+id+"'", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    clear();
                    MessageBox.Show("Berhasil mengganti data obat.");
                    showData();
                }
                catch (Exception)
                {}
            }
        }
    }
}
