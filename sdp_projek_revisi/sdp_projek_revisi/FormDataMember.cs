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
    public partial class FormDataMember : Form
    {
        Form1 mainParent;
        int row = -1;
        public FormDataMember()
        {
            InitializeComponent();
            radioButton1.Checked = true;
            groupBox1.Enabled = false;
            comboBox1.SelectedIndex = 0;
            clearWarning();
        }

        private void FormDataMember_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            comboBox1.SelectedIndex = 0;
            showData();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            TabPage parent = (TabPage)this.Parent;
            this.Close();
        }

        private void showData()
        {
            
            OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER", mainParent.oc);
            DataTable member = new DataTable();
            oda.Fill(member);

            dataGridView1.DataSource = member;
            
        }

        public void setMainParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void deleteMember()
        {
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE FROM MEMBER WHERE ID_MEMBER='"+dataGridView1[0,row].Value.ToString()+"'", mainParent.oc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Member berhasil dihapus.");
                row = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                row = e.RowIndex;
                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM MEMBER WHERE ID_MEMBER='"+dataGridView1[0,row].Value.ToString()+"'", mainParent.oc);
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
                String nik= selectedMember.Rows[0].Field<String>(9);

                groupBox1.Enabled = true;
                label3.Text = id;
                label4.Text = nik;
                label5.Text = nama;
                textBox7.Text = telp;
                textBox4.Text = alamat;
                textBox5.Text = pekerjaan;
                textBox10.Text = agama;
                dateTimePicker1.Value = tgllahir;
                if(jk == "P")
                {
                    radioButton2.Checked = true;
                }
                else
                {
                    radioButton1.Checked = true;
                }
                comboBox2.Text = golDarah;
            }
            catch (Exception ex)
            {
                
            }
        }
        private void clearWarning()
        {
            label14.Text = "";
            label15.Text = "";
            label11.Text = "";
            label16.Text = "";
        }

        private void GroupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda yakin ingin menghapus member ?", "Hapus Member", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                deleteMember();
                textBox7.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                label3.Text = "ID MEMBER";
                label4.Text = "NIK";
                label5.Text = "NAMA MEMBER";
                textBox10.Text = "";
                dateTimePicker1.Value = DateTime.Now;
                radioButton1.Checked = true;
                comboBox1.SelectedIndex = 0;
                groupBox1.Enabled = false;
                showData();
            }
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            String id = label3.Text;
            String telp = textBox7.Text.ToUpper();
            String alamat = textBox4.Text.ToUpper();
            String pekerjaan = textBox5.Text.ToUpper();
            String agama = textBox10.Text.ToUpper();
            String day = dateTimePicker1.Value.Day.ToString().ToUpper();
            String mon = dateTimePicker1.Value.Month.ToString().ToUpper();
            String year = dateTimePicker1.Value.Year.ToString().ToUpper();
            String jk = "L";
            if(radioButton2.Checked == true)
            {
                jk = "P";
            }
            else
            {
                jk = "L";
            }
            String golDarah = comboBox2.Text;
            bool validasi = true;
            if(telp.Length > 12)
            {
                validasi = false;
                label14.Text = "Nomor Telp/HP terlalu panjang";
            }
            if(telp == "")
            {
                validasi = false;
                label14.Text = "Nomor Telp/HP harus diisi";
            }
            if(alamat == "")
            {
                validasi = false;
                label11.Text = "Alamat harus diisi";
            }
            if(pekerjaan == "")
            {
                validasi = false;
                label15.Text = "Pekerjaan harus diisi";
            }
            if(agama == "")
            {
                validasi = false;
                label16.Text = "Agama harus diisi";
            }

            if (validasi)
            {
                if (MessageBox.Show("Apakah anda yakin ingin mengubah data member ?", "Edit Member", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        OracleCommand cmd = new OracleCommand("UPDATE MEMBER SET TELP_MEMBER='"+telp+"',ALAMAT_MEMBER='"+alamat+"',PEKERJAAN_MEMBER='"+pekerjaan+"',AGAMA='"+agama+"',TGLLAHIR_MEMBER=TO_DATE(LPAD('" + day + "',2,'0')||'/'||LPAD('" + mon + "',2,'0')||'/'||LPAD('" + year + "',4,'0'),'DD/MM/YYYY'),JK_MEMBER='"+jk+"',GOL_DARAH='"+golDarah+"' WHERE ID_MEMBER = '" + id+"'", mainParent.oc);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil mengubah data member");
                        showData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            String search = textBox1.Text.ToUpper();
            //0 ID
            //1 Nama
            //2 NIK
            //3 HP
            if (comboBox1.SelectedIndex == 0)
            {
                OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER WHERE ID_MEMBER LIKE '%" + search + "%'", mainParent.oc);
                DataTable member = new DataTable();
                oda.Fill(member);
                dataGridView1.DataSource = member;
            }else if (comboBox1.SelectedIndex == 1)
            {
                OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER WHERE NAMA_MEMBER LIKE '%" + search + "%'", mainParent.oc);
                DataTable member = new DataTable();
                oda.Fill(member);
                dataGridView1.DataSource = member;
            }else if (comboBox1.SelectedIndex == 2)
            {
                OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER WHERE NIK LIKE '%" + search + "%'", mainParent.oc);
                DataTable member = new DataTable();
                oda.Fill(member);
                dataGridView1.DataSource = member;
            }else if (comboBox1.SelectedIndex == 3)
            {
                OracleDataAdapter oda = new OracleDataAdapter("SELECT ID_MEMBER AS ID, NAMA_MEMBER AS NAMA FROM MEMBER WHERE TELP_MEMBER LIKE '%" + search + "%'", mainParent.oc);
                DataTable member = new DataTable();
                oda.Fill(member);
                dataGridView1.DataSource = member;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
