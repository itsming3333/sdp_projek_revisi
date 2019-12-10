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
    public partial class FormNewPegawai : Form
    {
        Form1 mainParent;
        //valid

        public FormNewPegawai()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormNewPegawai_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            timer1.Start();
            isiData();
            clear();
        }

        private void isiData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR, STATUS_RUANG AS STATUS FROM RUANG WHERE JENIS_RUANG='KANTOR'", mainParent.oc);
            DataTable kantor = new DataTable();
            oda.Fill(kantor);
            dataGridView1.DataSource = kantor;
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void clear()
        {
            //hilangin valdiation
            warningNIK.Text = "";
            label13.Text = "";
            label14.Text = "";
            label11.Text = "";
            label19.Text = "";
            label16.Text = "";
            label7.Text = "";
            label15.Text = "";


            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;

            textBox8.Text = "";
            textBox2.Text = "";
            textBox7.Text = "";
            textBox4.Text = "";
            textBox3.Text = "";
            textBox10.Text = "";
            textBox9.Text = "";
            textBox6.Text = "";
            label21.Text = "-";
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            bool validation = true;
            String id = textBox1.Text;
            String password = textBox2.Text.Substring(0, textBox2.Text.IndexOf(" ")).ToUpper();
            String nama = textBox2.Text.ToUpper();
            String gol = comboBox1.Text;
            String telp = textBox7.Text;
            String dd = dateTimePicker1.Value.Day.ToString();
            String mm = dateTimePicker1.Value.Month.ToString();
            String yyyy = dateTimePicker1.Value.Year.ToString();
            String alamat = textBox4.Text.ToUpper();
            String nik = textBox8.Text;
            OracleCommand cmd = new OracleCommand("SELECT ID_RUANG FROM RUANG WHERE NOMOR_RUANG='" + label21.Text.ToUpper() + "' AND JENIS_RUANG='KANTOR'", mainParent.oc);
            String id_kantor = cmd.ExecuteScalar().ToString();
            cmd = new OracleCommand("SELECT ID_JABATAN FROM JABATAN WHERE NAMA_JABATAN='" + comboBox2.Text.ToUpper() + "'", mainParent.oc);
            String id_jabatan = cmd.ExecuteScalar().ToString();
            String agama = textBox10.Text;
            String jk = "";
            if (radioButton1.Checked)
            {
                jk = "L";
            }
            else
            {
                jk = "P";
            }
            String npwp = textBox3.Text;
            String wali = textBox9.Text.ToUpper();
            String kontak_wali = textBox6.Text;

            //CEK VALID
            if (validation)
            {
                //INPUT
                try
                {
                    cmd = new OracleCommand("INSERT INTO PEGAWAI VALUES('" + id + "','" + password + "','" + nama + "','" + gol + "','" + telp + "',TO_DATE(LPAD('" + dd + "',2,'0')||'/'||LPAD('" + mm + "',2,'0')||'/'||LPAD('" + yyyy + "',4,'0'),'DD/MM/YYYY'),'" + alamat + "','" + nik + "','" + id_jabatan + "','" + id_kantor + "','" + agama + "','" + jk + "','" + npwp + "','" + wali + "','" + kontak_wali + "')", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    isiData();
                    clear();
                    MessageBox.Show("Berhasil register pegawai baru!");
                }
                catch (Exception ex)
                {}
            }
            else
            {
                //NOT VALID
            }
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                label21.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
            catch (Exception)
            {}
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            //AUTO GEN ID PEGAWAI
            if(textBox2.Text != "")
            {
                String id = "P" + textBox2.Text.Substring(0, 1).ToUpper();

                OracleCommand cmd = new OracleCommand("SELECT AUTO_GEN_ID_PEGAWAI('" + id + "') FROM DUAL", mainParent.oc);
                id += cmd.ExecuteScalar().ToString();
                textBox1.Text = id.ToUpper();
            }
            else
            {
                textBox1.Text = "XXXXX";
            }
            
        }
    }
}
