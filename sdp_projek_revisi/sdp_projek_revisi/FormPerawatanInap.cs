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
    public partial class FormPerawatanInap : Form
    {
        Form1 mainParent;
        String id_trans;
        public FormPerawatanInap()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void isiData()
        {
            String jenis_rawat = comboBox1.Text.ToUpper();

            if(jenis_rawat == "CHECKUP")
            {
                jenis_rawat = "PRIMARY";
            }
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NAMA_PERAWATAN AS NAMA, HARGA_PERAWATAN AS HARGA FROM PERAWATAN WHERE JENIS_PERAWATAN='"+jenis_rawat+"'", mainParent.oc);
            DataTable perawatan = new DataTable();
            oda.Fill(perawatan);
            dataGridView1.DataSource = perawatan;

            String jenis_ruang = comboBox3.Text.ToUpper();
            oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR, NAMA_RUANG AS JENIS FROM RUANG WHERE JENIS_RUANG='"+jenis_ruang+"' AND STATUS_RUANG='OPEN'", mainParent.oc);
            DataTable ruang = new DataTable();
            oda.Fill(ruang);
            dataGridView2.DataSource = ruang;

            String jenis_supply = comboBox2.Text.ToUpper();
            String nama_supply = textBox5.Text.ToUpper();
            oda = new OracleDataAdapter("SELECT ID_SUPPLY, NAMA_SUPPLY AS NAMA, HARGA_SUPPLY AS HARGA FROM SUPPLY WHERE JENIS_SUPPLY='" + jenis_supply + "' AND NAMA_SUPPLY LIKE '%"+nama_supply+"%'", mainParent.oc);
            DataTable supply = new DataTable();
            oda.Fill(supply);

            listBox1.DataSource = supply;
            listBox1.DisplayMember = "NAMA";
            listBox1.ValueMember = "ID_SUPPLY";
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
                    OracleCommand cmd = new OracleCommand("SELECT HARGA_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='"+supply[1]+"'", mainParent.oc);
                    int subtotal = Convert.ToInt32(cmd.ExecuteScalar()) * Convert.ToInt32(supply[0]);
                    total += subtotal;
                }
                label11.Text = "Rp. " + total;
            }
            catch (Exception ex)
            {}
        }

        public void cekCheckbox()
        {
            if(checkBox1.Checked == false)
            {
                groupBox2.Enabled = false;
            }
            else
            {
                groupBox2.Enabled = true;
            }
            if (checkBox2.Checked == false)
            {
                groupBox3.Enabled = false;
            }
            else
            {
                groupBox3.Enabled = true;
            }
            if (checkBox3.Checked == false)
            {
                groupBox4.Enabled = false;
            }
            else
            {
                groupBox4.Enabled = true;
            }
        }
        private void FormPerawatanInap_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            timer1.Start();
            disableAll();
            isiData();

            OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM PEGAWAI WHERE ID_PEGAWAI='" + mainParent.id_login + "'", mainParent.oc);
            DataTable login = new DataTable();
            oda.Fill(login);

            label3.Text = login.Rows[0].Field<String>(0);
            label4.Text = login.Rows[0].Field<String>(2);
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            cekCheckbox();
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            cekCheckbox();
        }

        private void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            cekCheckbox();
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void disableAll()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox1.Enabled = false;
            checkBox2.Enabled = false;
            checkBox3.Enabled = false;
            cekCheckbox();
            textBox1.Enabled = false;
            textBox5.Enabled = false;
            radioButton1.Checked = true;
            textBox2.Enabled = false;
            groupBox5.Enabled = false;
        }

        private void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void GroupBox3_Enter(object sender, EventArgs e)
        {

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
                MessageBox.Show("ID Transaksi salah/Pasien sudah keluar.\nPastikan ID Transaksi benar.");
            }
        }

        private void enableData()
        {
            textBox4.Enabled = false;
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox1.Enabled = true;
            checkBox2.Enabled = true;
            checkBox3.Enabled = true;
            cekCheckbox();
            textBox1.Enabled = true;
            textBox5.Enabled = true;
            radioButton1.Checked = true;
            textBox2.Enabled = true;
            groupBox5.Enabled = true;
        }

        private void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiData();
        }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiData();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiData();
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            isiData();
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
                    if(supply[1] == listBox1.Text)
                    {
                        int total = Convert.ToInt32(supply[0]) + 1;
                        listBox2.Items[i] = total + "|" + supply[1];
                        baru = false;
                    }
                }
                if (baru)
                {
                    listBox2.Items.Add("1|"+listBox1.Text);
                }             
                refreshSubtotalObat();
            }
            catch (Exception ex)
            {
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String dd = DateTime.Now.Day.ToString();
            String mm = DateTime.Now.Month.ToString();
            String yyyy = DateTime.Now.Year.ToString();
            if (checkBox1.Checked)
            {
                //Penambahan Perawatan
                String id_pegawai = label3.Text;
                OracleCommand cmd = new OracleCommand("SELECT ID_PERAWATAN FROM PERAWATAN WHERE NAMA_PERAWATAN = '"+label6.Text+"'", mainParent.oc);
                String id_rawat = cmd.ExecuteScalar().ToString();
                cmd = new OracleCommand("SELECT MAX(CTR_CHECKUP) FROM DTRANS_PERAWATAN_INAP WHERE ID_TRANS='"+id_trans+"'", mainParent.oc);
                int ctr;

                if (cmd.ExecuteScalar().ToString() != "")
                {
                   ctr = Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1;
                }
                else
                {
                    ctr = 0;
                }
                String keterangan = textBox2.Text;
                String keluhan = textBox1.Text;
                String tindak_lanjut = textBox3.Text;
                if (radioButton1.Checked)
                {
                    tindak_lanjut = "KELUAR";
                }else if (radioButton2.Checked)
                {
                    tindak_lanjut = numericUpDown1.Value.ToString() + ":" + numericUpDown2.Value.ToString() + " - Tindak Lanjut : " + comboBox4.Text;
                }

                try
                {
                    cmd = new OracleCommand("INSERT INTO DTRANS_PERAWATAN_INAP VALUES('"+id_trans+"','"+id_rawat+"','"+id_pegawai+"',"+ctr+",'"+keterangan+"','"+keluhan+"','"+tindak_lanjut+ "',TO_DATE(LPAD('" + dd + "',2,'0')||'/'||LPAD('" + mm + "',2,'0')||'/'||LPAD('" + yyyy + "',4,'0'),'DD/MM/YYYY'),'n')", mainParent.oc);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (checkBox2.Checked)
            {
                //TAMBAH SUPPLY
                String id_pegawai = label3.Text;
                OracleCommand cmd = new OracleCommand("SELECT MAX(CTR_SUPPLY) FROM DTRANS_SUPPLY WHERE ID_TRANS='"+id_trans+"'", mainParent.oc);
                int ctr_supply = 0;

                if(cmd.ExecuteScalar().ToString() != "")
                {
                    ctr_supply = Convert.ToInt32(cmd.ExecuteScalar().ToString()) + 1;
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
            if (checkBox3.Checked)
            {
                //Tambah Ruang
                String nomor = label20.Text;
                String nama = namaruangan.Text;
                
                if (checkBox4.Checked)
                {
                    try
                    {
                        OracleDataAdapter oda = new OracleDataAdapter("SELECT R.ID_RUANG, R.HARGA_RUANG, DR.TOTAL_HARI FROM DTRANS_RUANG DR, RUANG R WHERE DR.ID_RUANG = R.ID_RUANG AND DR.ID_TRANS='" + id_trans + "' AND R.STATUS_RUANG='CLOSED'", mainParent.oc);
                        DataTable inap = new DataTable();
                        oda.Fill(inap);
                        String id_ruang = inap.Rows[0].Field<String>(0);
                        int harga = Convert.ToInt32(inap.Rows[0].Field<int>(1));
                        int total_hari = Convert.ToInt32(inap.Rows[0].Field<int>(2));

                        total_hari++;
                        int subtotal = harga * total_hari;

                        OracleCommand cmd = new OracleCommand("UPDATE DTRANS_RUANG SET TOTAL_HARI=" + total_hari + ", SUBTOTAL=" + subtotal + " WHERE ID_RUANG='" + id_ruang + "' AND ID_TRANS='" + id_trans + "'", mainParent.oc);
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int row = e.RowIndex;
                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM PERAWATAN WHERE NAMA_PERAWATAN='" + dataGridView1[0, row].Value.ToString() + "'", mainParent.oc);
                DataTable selectedMember = new DataTable();
                oda.Fill(selectedMember);
                String nama = selectedMember.Rows[0].Field<String>(1);
                String biaya = selectedMember.Rows[0].Field<Int64>(3).ToString();
                
                label6.Text = nama;
                label7.Text = "Rp. "+biaya;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                String[] supply = listBox2.Text.Split('|');
                if(supply[0] == "1")
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

        private void Button2_Click(object sender, EventArgs e)
        {
            disableAll();
        }

        private void ComboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            isiData();
        }

        private void ComboBox3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            isiData();
        }

        private void CheckBox4_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                dataGridView2.Enabled = false;
                comboBox3.Enabled = false;
                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM RUANG R, DTRANS_RUANG DR WHERE R.ID_RUANG = DR.ID_RUANG AND R.JENIS_RUANG = 'KAMAR'", mainParent.oc);
                DataTable inapNow = new DataTable();
                oda.Fill(inapNow);
                label20.Text = inapNow.Rows[0].Field<String>(1);
                namaruangan.Text = inapNow.Rows[0].Field<String>(5);
                hargaruangan.Text = "Rp. " + inapNow.Rows[0].Field<Int64>(3).ToString() + "/hari";
            }
            else
            {
                dataGridView2.Enabled = true;
                comboBox3.Enabled = true;
                label20.Text = "-";
                namaruangan.Text = "Nama Ruang/Kamar";
                hargaruangan.Text = "Rp. -/hari";
            }
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM RUANG WHERE NOMOR_RUANG='" + dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString() + "' AND NAMA_RUANG='" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "' AND JENIS_RUANG='" + comboBox3.Text.ToUpper() + "'", mainParent.oc);
                DataTable selectedRuang = new DataTable();
                oda.Fill(selectedRuang);
                label20.Text = selectedRuang.Rows[0].Field<String>(1);
                namaruangan.Text = selectedRuang.Rows[0].Field<String>(5);
                hargaruangan.Text = "Rp. " + selectedRuang.Rows[0].Field<Int64>(3).ToString() + "/hari";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
