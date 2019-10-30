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

            oda = new OracleDataAdapter("SELECT * FROM PERAWATAN", mainParent.oc);
            DataTable perawatan = new DataTable();
            oda.Fill(perawatan);
            comboBox2.DataSource = perawatan;
            comboBox2.DisplayMember = "NAMA_PERAWATAN";
            comboBox2.ValueMember = "ID_PERAWATAN";

            
        }

        private void FormRawatSpesialis_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            comboBox2.SelectedIndex = 0;
            timer1.Start();
            showData();
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM MEMBER WHERE ID_MEMBER='" + dataGridView1[0, row].Value.ToString() + "'", mainParent.oc);
                DataTable selectedMember = new DataTable();
                oda.Fill(selectedMember);
                String id = selectedMember.Rows[0].Field<String>(0);
                DateTime tgllahir = selectedMember.Rows[0].Field<DateTime>(2);
                String nama = selectedMember.Rows[0].Field<String>(1);
                String alamat = selectedMember.Rows[0].Field<String>(3);
                String telp = selectedMember.Rows[0].Field<String>(4);
                String golDarah = selectedMember.Rows[0].Field<String>(5);
                String pekerjaan = selectedMember.Rows[0].Field<String>(6);
                String agama = selectedMember.Rows[0].Field<String>(7);
                String jk = selectedMember.Rows[0].Field<String>(8);
                String nik = selectedMember.Rows[0].Field<String>(9);
                Int16 berat = selectedMember.Rows[0].Field<Int16>(10);
                Int16 tinggi = selectedMember.Rows[0].Field<Int16>(11);

                label3.Text = id;
                label4.Text = nik;
                label5.Text = nama;
                label9.Text = "No. Telp/HP : " + telp;
                label6.Text = "Alamat : " + alamat;
                label7.Text = "Pekerjaan : " + pekerjaan;
                label12.Text = "Agama : " + agama;
                label10.Text = "Tanggal Lahir : " + tgllahir.Day + "/" + tgllahir.Month + "/" + tgllahir.Year;
                label8.Text = "Golongan Darah : " + golDarah;
                label11.Text = "Jenis Kelamin : " + jk;
                numericUpDown1.Value = berat;
                numericUpDown2.Value = tinggi;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT PR.NAMA_PERAWATAN AS PERAWATAN, P.NAMA_PEGAWAI AS DOKTER, R.NOMOR_RUANG AS RUANGAN FROM SHIFT_SPESIALIS SR, PERAWATAN PR, PEGAWAI P, RUANG R WHERE SR.ID_RUANG = R.ID_RUANG AND SR.ID_PERAWATAN = PR.ID_PERAWATAN AND SR.ID_PEGAWAI = P.ID_PEGAWAI AND PR.ID_PERAWATAN='"+comboBox2.SelectedValue+"'", mainParent.oc);
            DataTable shift = new DataTable();
            oda.Fill(shift);
            dataGridView2.DataSource = shift;
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                OracleDataAdapter oda = new OracleDataAdapter("SELECT R.NOMOR_RUANG, P.NAMA_PEGAWAI, PR.NAMA_PERAWATAN, TO_CHAR(SR.WAKTU_MULAI,'HH24:MI'), TO_CHAR(SR.WAKTU_SELESAI,'HH24:MI'), SR.HARGA_SHIFT FROM SHIFT_SPESIALIS SR, PERAWATAN PR, PEGAWAI P, RUANG R WHERE SR.ID_RUANG = R.ID_RUANG AND SR.ID_PERAWATAN = PR.ID_PERAWATAN AND SR.ID_PEGAWAI = P.ID_PEGAWAI AND R.NOMOR_RUANG='" + dataGridView2[2, row].Value.ToString() + "'", mainParent.oc);
                DataTable selectedMember = new DataTable();
                oda.Fill(selectedMember);
                
                label20.Text = selectedMember.Rows[0].Field<String>(0);
                label30.Text = selectedMember.Rows[0].Field<String>(1);
                label31.Text = selectedMember.Rows[0].Field<String>(2);
                label32.Text = selectedMember.Rows[0].Field<String>(3) + " - " + selectedMember.Rows[0].Field<String>(4);
                label21.Text = "Estimasi Biaya Rawat Rp. "+selectedMember.Rows[0].Field<Int64>(5).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = label5.Text;
            textBox5.Text = label9.Text.Remove(0, 14);
            textBox6.Text = "Pribadi";
        }
    }
}
