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

            oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR,  STATUS_RUANG AS STATUS FROM RUANG WHERE JENIS_RUANG='KANTOR'", mainParent.oc);
            DataTable ruang = new DataTable();
            oda.Fill(ruang);
            dataGridView2.DataSource = ruang;
        }

        private void reset()
        {
            groupBox1.Enabled = false;
            label14.Text = "";
            label11.Text = "";
            label16.Text = "";
            label13.Text = "";

            comboBox2.SelectedIndex = 0;
            textBox2.Text = "";
            textBox7.Text = "";
            textBox4.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox6.Text = "";
            radioButton1.Checked = true;
            comboBox3.SelectedIndex = 0;
            label6.Text = "-";

            label3.Text = "ID Pegawai";
            label4.Text = "nik_pegawai";
            label20.Text = "NPWP_pegawai";
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

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM PEGAWAI WHERE NAMA_PEGAWAI='"+ dataGridView1[0, row].Value.ToString() + "'", mainParent.oc);
                DataTable selectedPegawai = new DataTable();
                oda.Fill(selectedPegawai);

                label3.Text = selectedPegawai.Rows[0].Field<String>(0);
                label4.Text = selectedPegawai.Rows[0].Field<String>(7);
                label20.Text = selectedPegawai.Rows[0].Field<String>(12);
                String id_jabatan = selectedPegawai.Rows[0].Field<String>(8);
                OracleCommand cmd = new OracleCommand("SELECT NAMA_JABATAN FROM JABATAN WHERE ID_JABATAN='"+id_jabatan+"'", mainParent.oc);
                String jabatan = cmd.ExecuteScalar().ToString();
                comboBox2.Text = jabatan;
                textBox2.Text = selectedPegawai.Rows[0].Field<String>(2);
                textBox7.Text = selectedPegawai.Rows[0].Field<String>(4);
                textBox4.Text = selectedPegawai.Rows[0].Field<String>(6);
                textBox10.Text = selectedPegawai.Rows[0].Field<String>(10);
                dateTimePicker1.Value = selectedPegawai.Rows[0].Field<DateTime>(5);
                textBox9.Text = selectedPegawai.Rows[0].Field<String>(13);
                textBox6.Text = selectedPegawai.Rows[0].Field<String>(14);
                String jk = selectedPegawai.Rows[0].Field<String>(11);
                if(jk == "L")
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }
                String id_kantor = selectedPegawai.Rows[0].Field<String>(9);
                cmd = new OracleCommand("SELECT NOMOR_RUANG FROM RUANG WHERE ID_RUANG='"+id_kantor+"' AND JENIS_RUANG='KANTOR'",mainParent.oc);
                label6.Text = cmd.ExecuteScalar().ToString();
                comboBox3.Text = selectedPegawai.Rows[0].Field<String>(3);
                groupBox1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda yakin ingin menghapus pegawai ?", "Hapus Pegawai", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    OracleCommand cmd = new OracleCommand("DELETE FROM PEGAWAI WHERE ID_PEGAWAI='" + label3.Text + "'", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Pegawai berhasil dihapus.");
                    showData();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            String jabatan = comboBox2.Text;
            OracleCommand cmd = new OracleCommand("SELECT ID_JABATAN FROM JABATAN WHERE NAMA_JABATAN='"+jabatan+"'", mainParent.oc);
            String id_jabatan = cmd.ExecuteScalar().ToString();
            String nama = textBox2.Text;
            String telp = textBox7.Text;
            String alamat = textBox4.Text;
            String agama = textBox10.Text;
            String wali = textBox9.Text;
            String kontak_wali = textBox6.Text;
            String dd = dateTimePicker1.Value.Date.ToString();
            String mm = dateTimePicker1.Value.Month.ToString();
            String yyyy = dateTimePicker1.Value.Year.ToString();
            String jk = "";
            if (radioButton2.Checked)
            {
                jk = "P";
            }
            else
            {
                jk = "L";
            }
            String gol = comboBox3.Text;
            String kantor = label6.Text;
            cmd = new OracleCommand("SELECT ID_RUANG FROM RUANG WHERE NOMOR_RUANG='"+kantor+"' AND JENIS_RUANG='KANTOR'", mainParent.oc);
            String id_ruang = cmd.ExecuteScalar().ToString();

            if (MessageBox.Show("Apakah anda yakin ingin mengupdate data pegawai ?", "Update Pegawai", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    cmd = new OracleCommand("UPDATE PEGAWAI SET NAMA_PEGAWAI='"+nama+"', TELP_PEGAWAI='"+telp+"', ALAMAT_PEGAWAI='"+alamat+"', AGAMA_PEGAWAI='"+agama+ "', TGLLAHIR_PEGAWAI=TO_DATE(LPAD('" + dd + "',2,'0')||'/'||LPAD('" + mm + "',2,'0')||'/'||LPAD('" + yyyy + "',4,'0'),'DD/MM/YYYY'), WALI_PEGAWAI='"+wali+"', KONTAK_WALI_PEGAWAI='"+kontak_wali+"', RUANG_KANTOR='"+id_ruang+"', JK_PEGAWAI='"+jk+"', GOL_DARAH_PEGAWAI='"+gol+"' WHERE ID_PEGAWAI='"+label3.Text+"'", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil mengganti data pegawai.");
                    reset();
                    showData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                label6.Text = dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception)
            {
            }
        }
    }
}
