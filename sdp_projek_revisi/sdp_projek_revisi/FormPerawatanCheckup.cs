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
    public partial class FormPerawatanCheckup : Form
    {
        Form1 mainParent;
        String id_trans;
        public FormPerawatanCheckup()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormPerawatanCheckup_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            comboBox2.SelectedIndex = 0;
            resetData();
        }

        private void resetData()
        {
            textBox2.Text = "";
            checkBox2.Checked = false;
            groupBox4.Enabled = false;
            groupBox3.Enabled = false;
            listBox2.Items.Clear();
            textBox5.Text = "";
            comboBox2.SelectedIndex = 0;
            radioButton1.Checked = true;

            OracleDataAdapter oda = new OracleDataAdapter("SELECT NAMA_PERAWATAN FROM PERAWATAN", mainParent.oc);
            DataTable tindak = new DataTable();
            oda.Fill(tindak);
            comboBox1.DataSource = tindak;
            comboBox1.DisplayMember = "NAMA_PERAWATAN";
            textBox3.Text = "";
            label21.Text = "Rp. 0";

            label18.Text = "ID Member";
            label17.Text = "nik_member";
            label16.Text = "nama_member";
            label9.Text = "No. Telp/HP";
            label6.Text = "Alamat";
            label7.Text = "Pekerjaan";
            label12.Text = "Agama";
            label10.Text = "Tanggal Lahir";
            label8.Text = "Golongan Darah";
            label11.Text = "Jenis Kelamin";
            label14.Text = "Berat : ";
            label15.Text = "Tinggi : ";

            label5.Text = mainParent.id_login;
            OracleCommand cmd = new OracleCommand("SELECT NAMA_PEGAWAI FROM PEGAWAI WHERE ID_PEGAWAI='"+mainParent.id_login+"'", mainParent.oc);
            label4.Text = cmd.ExecuteScalar().ToString();

            oda = new OracleDataAdapter("SELECT NAMA_SUPPLY FROM SUPPLY WHERE JENIS_SUPPLY='"+comboBox2.Text.ToUpper()+"'", mainParent.oc);
            DataTable supply = new DataTable();
            oda.Fill(supply);
            listBox1.DataSource = supply;
            listBox1.DisplayMember = "NAMA_SUPPLY";
        }

        private void refreshSubtotalObat()
        {
            try
            {
                int total = 0;
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    String temp = listBox2.Items[i].ToString();
                    String[] supply = temp.Split('|');
                    OracleCommand cmd = new OracleCommand("SELECT HARGA_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='" + supply[1] + "'", mainParent.oc);
                    int subtotal = Convert.ToInt32(cmd.ExecuteScalar()) * Convert.ToInt32(supply[0]);
                    total += subtotal;
                }
                label21.Text = "Rp. " + total;
            }
            catch (Exception ex)
            { }
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            String hasil = textBox2.Text;
            String tindak = "";
            if (radioButton1.Checked)
            {
                tindak = "INAP";
            }else if (radioButton2.Checked)
            {
                tindak = comboBox1.Text;
            }
            else
            {
                tindak = textBox3.Text;
            }
            String id_pegawai = label5.Text;

            if(checkBox2.Checked = true)
            {
                //TAMBAH SUPPLY
                id_pegawai = label5.Text;
                int ctr_supply = -1;
                OracleCommand cmd = new OracleCommand("SELECT MAX(CTR_SUPPLY) FROM DTRANS_SUPPLY WHERE ID_TRANS='" + id_trans + "'", mainParent.oc);
                if(cmd.ExecuteScalar().ToString() != "") {
                    ctr_supply = Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1;
                }
                else
                {
                    ctr_supply = 0;
                }
                

                try
                {
                    for (int i = 0; i < listBox2.Items.Count; i++)
                    {
                        String[] supply = listBox2.Items[i].ToString().Split('|');
                        cmd = new OracleCommand("SELECT ID_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='" + supply[1] + "'", mainParent.oc);
                        String id_supply = cmd.ExecuteScalar().ToString();
                        cmd = new OracleCommand("SELECT HARGA_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='" + supply[1] + "'", mainParent.oc);
                        int harga = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                        int subtotal = Convert.ToInt32(supply[0]) * harga;

                        cmd = new OracleCommand("INSERT INTO DTRANS_SUPPLY VALUES('" + id_supply + "','" + id_trans + "','" + id_pegawai + "'," + supply[0] + "," + subtotal + ",'n'," + ctr_supply + ",'n')", mainParent.oc);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            //konfirmasi data
            try
            {
                OracleCommand cmd = new OracleCommand("UPDATE DTRANS_PERAWATAN_INAP SET KETERANGAN_CHECKUP='"+hasil+"', TINDAK_LANJUT='"+tindak+"' WHERE ID_TRANS='"+id_trans+"'", mainParent.oc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Pasien telah selesai dirawat.");
                resetData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            id_trans = textBox1.Text.ToUpper();
            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM TRANSAKSI WHERE ID_TRANS='" + id_trans + "' AND JENIS_RAWAT='CHECKUP' AND KONDISI_KELUAR='ANTRI'", mainParent.oc);
            int ada = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            if (ada > 0)
            {
                enableData();
            }
            else
            {
                MessageBox.Show("ID Transaksi salah/Pasien sudah dirawat.\nPastikan ID Transaksi benar.");
            }
        }

        private void enableData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM TRANSAKSI WHERE ID_TRANS='" + id_trans + "'", mainParent.oc);
            DataTable selectedTrans = new DataTable();
            oda.Fill(selectedTrans);

            //isi data
            oda = new OracleDataAdapter("SELECT * FROM MEMBER WHERE ID_MEMBER='"+selectedTrans.Rows[0].Field<String>(5)+"'", mainParent.oc);
            DataTable member = new DataTable();
            oda.Fill(member);
            label18.Text = member.Rows[0].Field<String>(0);
            label17.Text = member.Rows[0].Field<String>(9);
            label16.Text = member.Rows[0].Field<String>(1);
            label9.Text = "No. Telp/HP : "+member.Rows[0].Field<String>(4);
            label6.Text = "Alamat : "+member.Rows[0].Field<String>(3);
            label7.Text = "Pekerjaan : "+member.Rows[0].Field<String>(6);
            label12.Text = "Agama : "+ member.Rows[0].Field<String>(7);
            label10.Text = "Tanggal Lahir : "+ member.Rows[0].Field<DateTime>(2).ToShortDateString();
            label8.Text = "Golongan Darah : "+ member.Rows[0].Field<String>(5);
            label11.Text = "Jenis Kelamin : "+ member.Rows[0].Field<String>(8);
            label14.Text = "Berat : "+ member.Rows[0].Field<Int16>(10).ToString()+" Kg";
            label15.Text = "Tinggi : "+ member.Rows[0].Field<Int16>(11).ToString()+" Cm";
            groupBox3.Enabled = true;
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            resetData();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NAMA_SUPPLY FROM SUPPLY WHERE JENIS_SUPPLY='" + comboBox2.Text.ToUpper() + "'", mainParent.oc);
            DataTable supply = new DataTable();
            oda.Fill(supply);
            listBox1.DataSource = supply;
            listBox1.DisplayMember = "NAMA_SUPPLY";
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                bool baru = true;
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    String temp = listBox2.Items[i].ToString();
                    String[] supply = temp.Split('|');
                    if (supply[1] == listBox1.Text)
                    {
                        int total = Convert.ToInt32(supply[0]) + 1;
                        listBox2.Items[i] = total + "|" + supply[1];
                        baru = false;
                    }
                }
                if (baru)
                {
                    listBox2.Items.Add("1|" + listBox1.Text);
                }
                refreshSubtotalObat();
            }
            catch (Exception ex)
            {
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                String[] supply = listBox2.Text.Split('|');
                if (supply[0] == "1")
                {
                    listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                }
                else
                {
                    int total = Convert.ToInt32(supply[0]) - 1;
                    listBox2.Items[listBox2.SelectedIndex] = total + "|" + supply[1];
                }

                refreshSubtotalObat();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                groupBox4.Enabled = true;
            }
            else
            {
                groupBox4.Enabled = false;
            }
        }
    }
}
