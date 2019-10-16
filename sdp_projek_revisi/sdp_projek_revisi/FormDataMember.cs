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

        }
    }
}
