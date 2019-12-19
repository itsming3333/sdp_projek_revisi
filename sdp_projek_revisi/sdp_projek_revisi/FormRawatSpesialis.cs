using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
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
        String id_trans;
        String nama_member;
        String nomor_ruang;
        String tipe_rawat;

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
            clearwarning();
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            groupBox2.Enabled = true;
            groupBox3.Enabled = true;
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

        private void PrintReceipt()
        {
            PrintDialog printdialog = new PrintDialog();
            PrintDocument printdocument = new PrintDocument();
            printdialog.Document = printdocument;
            printdocument.PrintPage += new PrintPageEventHandler(printdocument_PrintPage);
            DialogResult result = printdialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                printdocument.Print();
            }
        }

        private void printdocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 12);
            float fontHeight = font.GetHeight();
            int startx = 10;
            int starty = 10;
            int offset = 40;

            graphics.DrawString("TIKET CHECKUP", new Font("Courier New", 15), new SolidBrush(Color.Black), startx, starty);
            graphics.DrawString(id_trans, new Font("Courier New", 14), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString(DateTime.Now.ToLongDateString(), new Font("Courier New", 6), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 15;
            graphics.DrawString(nama_member, new Font("Courier New", 12), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString("================", new Font("Courier New", 12), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString(tipe_rawat, new Font("Courier New", 10), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString(nomor_ruang, new Font("Courier New", 36), new SolidBrush(Color.Black), startx, starty + offset);

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Pastikan data sudah benar!\nLanjutkan transaksi ?", "Cetak Nota", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool validation = true;
                String total = label21.Text.Remove(0, 25);
                String id_member = label3.Text;
                String status_pelunasan = "n";
                String diagnosa_masuk = textBox2.Text;
                String alergi = textBox3.Text;
                String wali = textBox4.Text;
                String telp_wali = textBox5.Text;
                String relasi_wali = textBox6.Text;
                String jenis_rawat = "checkup";


                if (textBox2.Text == "")
                {
                    validation = false;
                    label22.Text = "Keluhan Harus Terisi";
                }
                if (label20.Text == "-")
                {
                    validation = false;
                    label26.Text = "Pilih Ruangan";
                }
                if (textBox4.Text == "")
                {
                    validation = false;
                    label27.Text = "Masukan nama kontak rujukan";
                }
                if (textBox5.Text == "")
                {
                    validation = false;
                    label28.Text = "Masukan Telp. kontak rujukan";
                }
                if (!textBox5.Text.All(char.IsNumber))
                {
                    validation = false;
                    label28.Text = "Telp. kontak rujukan harus angka";
                }
                if (textBox6.Text == "")
                {
                    validation = false;
                    label29.Text = "Masukan relasi kontak rujukan";
                }
                if (numericUpDown1.Minimum == 0)
                {
                    validation = false;
                    label24.Text = "Masukan Berat Badannya";
                }
                if (numericUpDown2.Minimum == 0)
                {
                    validation = false;
                    label24.Text = "Masukan Tinggi Badannya";
                }
                //VALIDATION CHECK

                if (validation)
                {
                    OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM PEGAWAI WHERE NAMA_PEGAWAI = '" + label30.Text + "'", mainParent.oc);
                    DataTable selectedPegawai = new DataTable();
                    oda.Fill(selectedPegawai);
                    oda = new OracleDataAdapter("SELECT * FROM PERAWATAN WHERE NAMA_PERAWATAN = '" + label31.Text + "'", mainParent.oc);
                    DataTable selectedRawat = new DataTable();
                    oda.Fill(selectedRawat);

                    String id_rawat = selectedRawat.Rows[0].Field<String>(0);
                    String id_pegawai = selectedPegawai.Rows[0].Field<String>(0);
                    String dd = DateTime.Now.Day.ToString();
                    String mm = DateTime.Now.Month.ToString();
                    String yyyy = DateTime.Now.Year.ToString();
                    String id = dd + mm + yyyy;

                    OracleCommand cmd = new OracleCommand("SELECT AUTO_GEN_ID_TRANS('" + id + "') FROM DUAL", mainParent.oc);
                    id += cmd.ExecuteScalar().ToString();

                    try
                    {
                        //INSERT
                        cmd = new OracleCommand("INSERT INTO TRANSAKSI VALUES('" + id.ToUpper() + "',TO_DATE(LPAD('" + dd + "',2,'0')||'/'||LPAD('" + mm + "',2,'0')||'/'||LPAD('" + yyyy + "',4,'0'),'DD/MM/YYYY'),'','ANTRI'," + total + ",'" + id_member.ToUpper() + "','" + status_pelunasan.ToUpper() + "','" + diagnosa_masuk.ToUpper() + "','" + alergi + "','" + wali.ToUpper() + "','" + telp_wali + "','" + relasi_wali.ToUpper() + "','" + jenis_rawat.ToUpper() + "')", mainParent.oc);
                        cmd.ExecuteNonQuery();
                        cmd = new OracleCommand("INSERT INTO DTRANS_PERAWATAN_INAP VALUES('" + id + "','" + id_rawat + "','" + id_pegawai + "',0,'ANTRI','','ANTRI',TO_DATE(LPAD('" + dd + "',2,'0')||'/'||LPAD('" + mm + "',2,'0')||'/'||LPAD('" + yyyy + "',4,'0'),'DD/MM/YYYY'),'n')", mainParent.oc);
                        cmd.ExecuteNonQuery();

                        //print
                        cmd = new OracleCommand("SELECT NAMA_MEMBER FROM MEMBER WHERE ID_MEMBER='" + id_member + "'", mainParent.oc);
                        nama_member = cmd.ExecuteScalar().ToString();
                        nomor_ruang = label20.Text;
                        tipe_rawat = label31.Text;
                        id_trans = id;
                        PrintReceipt();

                        groupBox2.Enabled = false;
                        groupBox3.Enabled = false;
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox6.Text = "";
                        label20.Text = "-";
                        clearwarning();
                        

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    //VALIDATION ERROR
                }
            }
            
        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        public void clearwarning ()
        {
            label22.Text = "";
            label26.Text = "";
            label27.Text = "";
            label28.Text = "";
            label29.Text = "";
            label33.Text = "";
            label34.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            label20.Text = "-";
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            clearwarning();

        }
    }
}
