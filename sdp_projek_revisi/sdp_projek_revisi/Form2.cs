using Oracle.DataAccess.Client;
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

namespace sdp_projek_revisi
{
    public partial class Form2 : Form
    {
        Form1 mainParent;
        String id_trans; int total;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM PEGAWAI WHERE ID_PEGAWAI='" + mainParent.id_login + "'", mainParent.oc);
            DataTable login = new DataTable();
            oda.Fill(login);

            label10.Text = login.Rows[0].Field<String>(0);
            label9.Text = login.Rows[0].Field<String>(2);
            oda = new OracleDataAdapter("SELECT NAMA_PEMBAYARAN FROM PEMBAYARAN", mainParent.oc);
            DataTable jenisBayar = new DataTable();
            oda.Fill(jenisBayar);
            comboBox1.DataSource = jenisBayar;
            comboBox1.DisplayMember = "NAMA_PEMBAYARAN";
        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void Label7_Click(object sender, EventArgs e)
        {
            kembalian();
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label11_Click(object sender, EventArgs e)
        {

        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            id_trans = textBox4.Text.ToUpper();

            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TRANSAKSI WHERE ID_TRANS='" + id_trans + "' AND JENIS_RAWAT='INAP'", mainParent.oc);
            int ada = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            if (ada > 0)
            {
                enableData();
            }
            else
            {
                MessageBox.Show("ID Transaksi tidak ditemukan.\nPastikan ID Transaksi benar.");
            }
        }

        private void enableData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT DP.CTR_CHECKUP AS PERAWATAN_KE, DP.ID_PERAWATAN AS ID, P.NAMA_PERAWATAN AS TINDAKAN, P.HARGA_PERAWATAN AS HARGA FROM DTRANS_PERAWATAN_INAP DP, PERAWATAN P WHERE DP.ID_PERAWATAN = P.ID_PERAWATAN AND DP.LUNAS_RAWAT='n' AND DP.ID_TRANS='" + id_trans+"'", mainParent.oc);
            DataTable perawatan = new DataTable();
            oda.Fill(perawatan);
            dataGridView1.DataSource = perawatan;

            oda = new OracleDataAdapter("SELECT CTR_SUPPLY AS INPUT_KE, SUM(SUBTOTAL) AS TOTAL FROM DTRANS_SUPPLY WHERE ID_TRANS='"+id_trans+"' AND LUNAS_SUPPLY='n' GROUP BY CTR_SUPPLY", mainParent.oc);
            DataTable supply = new DataTable();
            oda.Fill(supply);
            dataGridView2.DataSource = supply;

            oda = new OracleDataAdapter("SELECT DR.ID_RUANG AS ID, R.NAMA_RUANG AS RUANG, DR.TOTAL_HARI AS HARI, DR.SUBTOTAL AS SUBTOTAL FROM RUANG R, DTRANS_RUANG DR WHERE DR.ID_RUANG=R.ID_RUANG AND DR.ID_TRANS='"+id_trans+"' AND DR.LUNAS_RUANG='N'", mainParent.oc);
            DataTable ruang = new DataTable();
            oda.Fill(ruang);
            dataGridView3.DataSource = ruang;

            listBox1.Enabled = true;
            comboBox1.Enabled = true;
            groupBox2.Enabled = true;
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            try
            {
                if(dataGridView1.Rows[row].Cells[0].Value.ToString() != "" && dataGridView1.Rows[row].Cells[1].Value.ToString() != "")
                {
                    String jenis = "rawat";
                    String detail = "";

                    detail = dataGridView1.Rows[row].Cells[3].Value.ToString() + "$" +dataGridView1.Rows[row].Cells[0].Value.ToString() + "$" + dataGridView1.Rows[row].Cells[1].Value.ToString();
                    String isi = jenis + "$" + detail;

                    bool ada = false;
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        if (listBox1.Items[i].ToString() == isi)
                        {
                            ada = true;
                        }
                    }

                    if (!ada)
                    {
                        listBox1.Items.Add(isi);
                        retotal();
                    }
                }
            }
            catch (Exception)
            {}
        }

        private void retotal()
        {
            total = 0;
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                String[] line = listBox1.Items[i].ToString().Split('$');
                int subtotal = Convert.ToInt32(line[1]);
                total += subtotal;
            }
            label7.Text = total+"";
            if(comboBox1.Text == "TRANSFER BANK")
            {
                numericUpDown1.Value = total;
            }
        }

        private void kembalian()
        {
            int kembalian = Convert.ToInt32(numericUpDown1.Value.ToString()) - total;
            label12.Text = kembalian + "";
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            kembalian();
        }

        private void NumericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            kembalian();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if(Convert.ToInt32(label12.Text) < 0)
            {
                MessageBox.Show("Jumlah Pembayaran anda kurang!");
            }
            else
            {
                
                try
                {
                    for (int i = 0; i < listBox1.Items.Count; i++)
                    {
                        String[] line = listBox1.Items[i].ToString().Split('$');
                        if (line[0] == "rawat")
                        {
                            OracleCommand cmd = new OracleCommand("UPDATE DTRANS_PERAWATAN_INAP SET LUNAS_RAWAT='y' WHERE ID_TRANS='" + id_trans + "' AND ID_PERAWATAN='" + line[3] + "' AND CTR_CHECKUP='" + line[2] + "'", mainParent.oc);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    OracleCommand ocmd = new OracleCommand("SELECT ID_PEMBAYARAN FROM PEMBAYARAN WHERE NAMA_PEMBAYARAN='" + comboBox1.Text + "'", mainParent.oc);
                    String id_bayar = ocmd.ExecuteScalar().ToString();
                    ocmd = new OracleCommand("SELECT COUNT(*) FROM DTRANS_PEMBAYARAN WHERE ID_TRANS='" + id_trans + "' AND ID_PEMBAYARAN='" + id_bayar + "'", mainParent.oc);
                    if (Convert.ToInt32(ocmd.ExecuteScalar().ToString()) == 0)
                    {

                        ocmd = new OracleCommand("INSERT INTO DTRANS_PEMBAYARAN VALUES('" + id_bayar + "','" + id_trans + "'," + total + ")", mainParent.oc);
                        ocmd.ExecuteNonQuery();
                    }
                    else
                    {
                        ocmd = new OracleCommand("SELECT JUMLAH_PEMBAYARAN FROM DTRANS_PEMBAYARAN WHERE ID_PEMBAYARAN='" + id_bayar + "' AND ID_TRANS='" + id_trans + "'", mainParent.oc);
                        int totalBefore = Convert.ToInt32(ocmd.ExecuteScalar().ToString());
                        totalBefore += total;
                        ocmd = new OracleCommand("UPDATE DTRANS_PEMBAYARAN SET JUMLAH_PEMBAYARAN=" + totalBefore + " WHERE ID_TRANS='" + id_trans + "' AND ID_PEMBAYARAN='" + id_bayar + "'", mainParent.oc);
                        ocmd.ExecuteNonQuery();
                    }
                    PrintReceipt();
                    reset();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void reset()
        {

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

            graphics.DrawString("BUKTI PEMBAYARAN", new Font("Courier New", 14), new SolidBrush(Color.Black), startx, starty);
            graphics.DrawString(id_trans, new Font("Courier New", 14), new SolidBrush(Color.Black), startx, starty + offset);

        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            try
            {
                if (dataGridView2.Rows[row].Cells[0].Value.ToString() != "" && dataGridView2.Rows[row].Cells[1].Value.ToString() != "")
                {

                }
            }
            catch (Exception)
            { }
        }
    }
}
