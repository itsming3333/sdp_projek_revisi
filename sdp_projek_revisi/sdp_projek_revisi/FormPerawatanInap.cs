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
            oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR, JENIS_RUANG AS JENIS FROM RUANG WHERE JENIS_RUANG='"+jenis_ruang+"'", mainParent.oc);
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
                    OracleCommand cmd = new OracleCommand("SELECT HARGA_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='"+listBox2.Items[i]+"'", mainParent.oc);
                    MessageBox.Show("SELECT HARGA_SUPPLY FROM SUPPLY WHERE ID_SUPPLY='" + listBox2.Items[i] + "'");
                    total += Convert.ToInt32(cmd.ExecuteScalar());
                }
                label11.Text = "Rp. " + total;
            }
            catch (Exception ex)
            {            }
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
            MessageBox.Show("SELECT COUNT(*) FROM TRANSAKSI WHERE ID_TRANS='" + id_trans + "' AND JENIS_RAWAT='INAP'");
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
                listBox2.Items.Add(listBox1.Text);
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
                int ctr = Convert.ToInt32(cmd.ExecuteScalar().ToString())+1;
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
            }
            if (checkBox3.Checked)
            {

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
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
                refreshSubtotalObat();
            }
            catch (Exception ex)
            {
            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            disableAll();
        }
    }
}
