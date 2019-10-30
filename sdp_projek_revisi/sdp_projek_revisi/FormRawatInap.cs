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
    public partial class FormRawatInap : Form
    {
        Form1 mainParent;
        String id_ruang = "";
        Int64 harga = 0;
        public FormRawatInap()
        {
            InitializeComponent();
        }
        private void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER", mainParent.oc);
            DataTable member = new DataTable();
            oda.Fill(member);

            dataGridView1.DataSource = member;
            oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR, JENIS_RUANG AS JENIS, STATUS_RUANG AS STATUS FROM RUANG", mainParent.oc);
            DataTable ruang = new DataTable();
            oda.Fill(ruang);
            for (int i = 0; i < dataGridView2.Rows.Count-1; i++)
            {
                if (dataGridView2[1, i].Value.ToString() == "OPEN")
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            dataGridView2.DataSource = ruang;
        }       

        private void FormRawatInap_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            showData();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
        }
        public void setMainParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Label15_Click(object sender, EventArgs e)
        {

        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            bool validasi = true;
            String total = label22.Text.Remove(0, 4);
            String id_member = label3.Text;
            String diagnosa_masuk = textBox2.Text;
            String alergi = textBox3.Text;
            String wali = textBox4.Text;
            String telp_wali = textBox5.Text;
            String relasi_wali = textBox6.Text;


            //validasi error

            if (MessageBox.Show("Periksa kembali isi form rawat inap!\nApakah data yang diisi sudah benar ?", "Konfirmasi Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (validasi)
                {
                    //Auto gen id transaksi
                    String id_trans = "";
                    String dd = DateTime.Now.Day.ToString();
                    String mm = DateTime.Now.Month.ToString();
                    String yyyy = DateTime.Now.Year.ToString();
                    id_trans = dd + mm + yyyy;

                    OracleCommand cmd = new OracleCommand("SELECT AUTO_GEN_ID_TRANS('" + id_trans + "') FROM DUAL", mainParent.oc);
                    id_trans += cmd.ExecuteScalar().ToString();

                    //MENAMBAH TRANSAKSI
                    cmd = new OracleCommand("INSERT INTO TRANSAKSI VALUES('" + id_trans + "',TO_DATE(LPAD('" + dd + "',2,'0')||'/'||LPAD('" + mm + "',2,'0')||'/'||LPAD('" + yyyy + "',4,'0'),'DD/MM/YYYY'),'',''," + total + ",'" + id_member + "','N','" + diagnosa_masuk.ToUpper() + "','" + alergi.ToUpper() + "','" + wali.ToUpper() + "','" + telp_wali + "','" + relasi_wali.ToUpper() + "','INAP')", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    //MENAMBAH RUANG
                    cmd = new OracleCommand("INSERT INTO DTRANS_RUANG VALUES('" + id_ruang + "','" + id_trans + "','1'," + harga + ",'N')", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    cmd = new OracleCommand("UPDATE RUANG SET STATUS_RUANG='CLOSED' WHERE ID_RUANG='" + id_ruang + "'", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Berhasil menambah transaksi baru");
                    refresh();
                }
                else
                {
                    //SHOW ERROR
                }
            }
        }
        private void refresh()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR, JENIS_RUANG AS JENIS, STATUS_RUANG AS STATUS FROM RUANG", mainParent.oc);
            DataTable ruang = new DataTable();
            oda.Fill(ruang);
            for (int i = 0; i < dataGridView2.Rows.Count-1; i++)
            {
                if (dataGridView2[1, i].Value.ToString() == "OPEN")
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
            dataGridView2.DataSource = ruang;
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            groupBox2.Enabled = false;
            groupBox3.Enabled = false;
            label3.Text = "ID_Member";
            label4.Text = "nik_member";
            label5.Text = "nama_member";
            label9.Text = "No. Telp/HP : ";
            label6.Text = "Alamat : ";
            label7.Text = "Pekerjaan : ";
            label12.Text = "Agama : ";
            label10.Text = "Tanggal Lahir : ";
            label8.Text = "Golongan Darah : ";
            label11.Text = "Jenis Kelamin : ";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            String jenis = comboBox2.Text.ToUpper();
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR, STATUS_RUANG AS STATUS FROM RUANG WHERE JENIS_RUANG='KAMAR' AND NAMA_RUANG='"+jenis+"' ORDER BY NOMOR_RUANG", mainParent.oc);
            DataTable selectedJenis = new DataTable();
            oda.Fill(selectedJenis);
            dataGridView2.DataSource = selectedJenis;
            for (int i = 0; i < dataGridView2.Rows.Count-1; i++)
            {
                if(dataGridView2[1, i].Value.ToString() == "OPEN")
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                }
                else
                {
                    dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
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
                label9.Text = "No. Telp/HP : "+telp;
                label6.Text = "Alamat : "+alamat;
                label7.Text = "Pekerjaan : "+pekerjaan;
                label12.Text = "Agama : "+agama;
                label10.Text = "Tanggal Lahir : "+tgllahir.Day+"/"+tgllahir.Month+"/"+tgllahir.Year;
                label8.Text = "Golongan Darah : "+golDarah;
                label11.Text = "Jenis Kelamin : "+jk;
                numericUpDown1.Value = berat;
                numericUpDown2.Value = tinggi;
                groupBox2.Enabled = true;
                groupBox3.Enabled = true;
            }
            catch (Exception ex)
            {
            }
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                int row = e.RowIndex;
                OracleDataAdapter oda = new OracleDataAdapter("SELECT NOMOR_RUANG, NAMA_RUANG, HARGA_RUANG, STATUS_RUANG, ID_RUANG FROM RUANG WHERE NOMOR_RUANG='" + dataGridView2[0, row].Value.ToString() + "'", mainParent.oc);
                DataTable selectedMember = new DataTable();
                oda.Fill(selectedMember);
                if(selectedMember.Rows[0].Field<String>(3) == "OPEN")
                {
                    String nomor = selectedMember.Rows[0].Field<String>(0);
                    String nama = selectedMember.Rows[0].Field<String>(1);
                    harga = selectedMember.Rows[0].Field<Int64>(2);
                    id_ruang = selectedMember.Rows[0].Field<String>(4);

                    label20.Text = nomor;
                    label21.Text = "Jenis " + nama;
                    label22.Text = "Rp. " + harga;
                }
                else
                {
                    MessageBox.Show("Ruangan Sudah Terpakai.");
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox4.Text = label5.Text;
            textBox5.Text = label9.Text.Remove(0, 14);
            textBox6.Text = "Pribadi";

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            refresh();
        }
    }
}
