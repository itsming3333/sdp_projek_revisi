using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace sdp_projek_revisi
{
    public partial class FormDonor : Form
    {
        Form1 mainParent;
        String id_selected_member = "";
        String id_trans = "";
        String kantong_darah = "";
        String psuhu = "";
        String ptensi = "";
        String prhesus = "";
        String pnadi = "";

        public FormDonor()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }
        int ctr = 0;
        private void FormDonor_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            reset();
            timer1.Start();
            showData();
            label18.Text = "sebanyak " + ctr + " kali tahun ini";
        }
        private void reset()
        {
            comboBox1.SelectedIndex = 0;
            radioButton1.Checked = true;
            numericUpDown3.Value = 1;
            numericUpDown4.Value = 1;
            numericUpDown5.Value = 1;
            radioButton7.Checked = true;
            radioButton4.Checked = true;
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER", mainParent.oc);
            DataTable member = new DataTable();
            oda.Fill(member);
            dataGridView1.DataSource = member;

            oda = new OracleDataAdapter("SELECT TO_CHAR(D.TGL_DONOR,'DD/MM/YYYY') AS TANGGAL, S.NAMA_SUPPLY AS DONOR FROM DONOR D, MEMBER M, SUPPLY S WHERE D.ID_MEMBER = M.ID_MEMBER AND D.ID_SUPPLY = S.ID_SUPPLY AND M.ID_MEMBER='"+id_selected_member+"'", mainParent.oc);
            DataTable trans_donor = new DataTable();
            oda.Fill(trans_donor);
            dataGridView2.DataSource = trans_donor;
        }

        private void updateLayak()
        {
            int berat = Convert.ToInt32(numericUpDown1.Value);
            bool kadar = true;
            if (radioButton2.Checked == true)
            {
                kadar = false;
            }

            int tensi = Convert.ToInt32(numericUpDown3.Value);
            int suhu = Convert.ToInt32(numericUpDown5.Value);
            int nadi = Convert.ToInt32(numericUpDown4.Value);

            if (kadar == false || berat < 45 || tensi < 70 || tensi > 170 || suhu < 36 || suhu > 38 || nadi < 50 || nadi > 100)
            {
                label25.Text = "TIDAK LAYAK";
            }
            else
            {
                label25.Text = "LAYAK";
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
                id_selected_member = label3.Text;
                reset();
                updateLayak();
                oda = new OracleDataAdapter("SELECT TO_CHAR(D.TGL_DONOR,'DD/MM/YYYY') AS TANGGAL, S.NAMA_SUPPLY AS DONOR FROM DONOR D, MEMBER M, SUPPLY S WHERE D.ID_MEMBER = M.ID_MEMBER AND D.ID_SUPPLY = S.ID_SUPPLY AND M.ID_MEMBER='" + id_selected_member + "'", mainParent.oc);
                DataTable trans_donor = new DataTable();
                oda.Fill(trans_donor);
                dataGridView2.DataSource = trans_donor;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            updateLayak();
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            updateLayak();
        }

        private void NumericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            updateLayak();
        }

        private void NumericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            updateLayak();
        }

        private void NumericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            updateLayak();
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            updateLayak();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            updateLayak();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String gol_darah = label8.Text.Remove(0, 17);
            String kantong = "0CC";

            if(radioButton4.Checked == true)
            {
                kantong = "350CC";
            }else if(radioButton3.Checked == true)
            {
                kantong = "450CC";
            }
            else
            {
                kantong = "600CC";
            }

            String id_supply = "";
            OracleCommand cmd = new OracleCommand("SELECT ID_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='KANTONG DARAH "+gol_darah+" "+kantong+"'", mainParent.oc);
            id_supply += cmd.ExecuteScalar().ToString();
            String tensi = numericUpDown3.Value.ToString();
            String suhu = numericUpDown5.Value.ToString();
            String reaksi = "BELUM ADA TINDAKAN";
            String nadi = numericUpDown4.Value.ToString();
            String rhesus = "";
            if(radioButton7.Checked == true)
            {
                rhesus = "+";
            }
            else
            {
                rhesus = "-";
            }

            if (label25.Text == "TIDAK LAYAK")
            {
                MessageBox.Show("Member tidak layak untuk melanjutkan donor");
            }
            else
            {
                if (MessageBox.Show("Pastikan data sudah benar!\nLanjutkan transaksi ?", "Cetak Nota", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    String dd = DateTime.Now.Day.ToString();
                    String mm = DateTime.Now.Month.ToString();
                    String yyyy = DateTime.Now.Year.ToString();
                    String id = "D" + dd + mm + yyyy;

                    cmd = new OracleCommand("SELECT AUTO_GEN_ID_DONOR('" + id + "') FROM DUAL", mainParent.oc);
                    id += cmd.ExecuteScalar().ToString();

                    String id_member = label3.Text;
                    
                    
                    
                    try
                    {
                        //INSERT
                        cmd = new OracleCommand("INSERT INTO DONOR VALUES('" + id + "','" + id_member + "','" + id_supply + "','" + tensi + "','" + suhu + "','" + reaksi + "','',TO_DATE(LPAD('" + dd + "',2,'0')||'/'||LPAD('" + mm + "',2,'0')||'/'||LPAD('" + yyyy + "',4,'0'),'DD/MM/YYYY'),'" + nadi + "','" + rhesus + "','')", mainParent.oc);
                        cmd.ExecuteNonQuery();
                        id_trans = id;
                        kantong_darah = id_supply;
                        ptensi = tensi+"";
                        prhesus = rhesus + "";
                        psuhu = suhu + "";
                        pnadi = nadi + "";
                        //print
                        PrintReceipt();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            ctr++;
            kosong();
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

            graphics.DrawString("TIKET DONOR DARAH", new Font("Courier New", 18), new SolidBrush(Color.Black), startx, starty);
            graphics.DrawString(id_trans, new Font("Courier New", 14), new SolidBrush(Color.Black), startx, starty+offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString(DateTime.Now.ToLongDateString(), new Font("Courier New", 6), new SolidBrush(Color.Black), startx, starty + offset);

            OracleCommand cmd = new OracleCommand("SELECT NAMA_MEMBER FROM MEMBER WHERE ID_MEMBER='"+id_selected_member+"'", mainParent.oc);
            String nama = cmd.ExecuteScalar().ToString();
            cmd = new OracleCommand("SELECT NAMA_SUPPLY FROM SUPPLY WHERE ID_SUPPLY='" + kantong_darah + "'", mainParent.oc);
            String supply = cmd.ExecuteScalar().ToString();
            offset += (int)fontHeight + 15;
            graphics.DrawString(nama, new Font("Courier New", 12), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString("================", new Font("Courier New", 12), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString(supply, new Font("Courier New", 9), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString("Tensi : "+ptensi+" mm Hg", new Font("Courier New", 6), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString("Rhesus : "+prhesus, new Font("Courier New", 6), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString("Suhu : "+psuhu + " Celcius", new Font("Courier New", 6), new SolidBrush(Color.Black), startx, starty + offset);
            offset += (int)fontHeight + 5;
            graphics.DrawString("Nadi : " + pnadi + " BPM", new Font("Courier New", 6), new SolidBrush(Color.Black), startx, starty + offset);
        }

        private void Label18_Click(object sender, EventArgs e)
        {

        }

        public void kosong()
        {
            numericUpDown1.Value = 1;
            numericUpDown2.Value = 1;
            numericUpDown3.Value = 1;
            numericUpDown4.Value = 1;
            numericUpDown5.Value = 1;
            groupBox2.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kosong();
        }
    }
}
