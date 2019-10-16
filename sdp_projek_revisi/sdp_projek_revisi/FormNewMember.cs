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
    public partial class FormNewMember : Form
    {
        Form1 mainParent;
        public FormNewMember()
        {
            InitializeComponent();
        }

        private void FormNewMember_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            clearWarning();
            radioButton1.Checked = true;
            comboBox1.SelectedIndex = 0;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda yakin untuk reset ulang form pengisian ?", "Reset Data", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                textBox2.Text = "";
                textBox8.Text = "";
                textBox7.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox10.Text = "";
                comboBox1.SelectedIndex = 0;
                radioButton1.Checked = true;
                dateTimePicker1.Value = DateTime.Now;
                clearWarning();
            }
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void TextBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Periksa kembali isi form new member!\nApakah data yang diisi sudah benar ?", "Konfirmasi Data", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                bool validasi = true;
                clearWarning();
                if(textBox8.Text.Length != 16)
                {
                    warningNIK.Text = "NIK tidak valid";
                    validasi = false;
                }
                if(textBox8.Text == "")
                {
                    warningNIK.Text = "NIK harus terisi";
                    validasi = false;
                }
                if(textBox2.Text == "")
                {
                    label13.Text = "Nama harus terisi";
                    validasi = false;
                }
                if (textBox2.Text.Length < 2)
                {
                    label13.Text = "Nama tidak valid";
                    validasi = false;
                }
                if (textBox7.Text == "")
                {
                    label14.Text = "Nomor Telepon/HP harus terisi";
                    validasi = false;
                }
                if (!textBox7.Text.All(char.IsNumber))
                {
                    label14.Text = "Nomor Telp/HP tidak valid";
                    validasi = false;
                }
                if (textBox4.Text == "")
                {
                    label11.Text = "Alamat harus terisi";
                    validasi = false;
                }
                if (textBox5.Text == "")
                {
                    label15.Text = "Pekerjaan harus terisi";
                    validasi = false;
                }
                if (textBox10.Text == "")
                {
                    label16.Text = "Agama harus terisi";
                    validasi = false;
                }

                if (validasi)
                {
                    try
                    {
                        String id = textBox1.Text;
                        String nik = textBox8.Text;
                        String nama = textBox2.Text;
                        String telp = textBox7.Text;
                        String alamat = textBox4.Text;
                        String pekerjaan = textBox5.Text;
                        String agama = textBox10.Text;
                        String day = dateTimePicker1.Value.Day.ToString();
                        String mon = dateTimePicker1.Value.Month.ToString();
                        String year = dateTimePicker1.Value.Year.ToString();
                        String jk = "L";
                        if(radioButton2.Checked == true)
                        {
                            jk = "P";
                        }
                        String golDarah = comboBox1.Text;

                        OracleCommand cmd = new OracleCommand("INSERT INTO MEMBER VALUES('"+id+"','"+nama+ "',TO_DATE(LPAD('" + day + "',2,'0')||'/'||LPAD('" + mon + "',2,'0')||'/'||LPAD('" + year + "',4,'0'),'DD/MM/YYYY'),'"+alamat+"','"+telp+"','"+golDarah+"','"+pekerjaan+"','"+agama+"','"+jk+"','"+nik+"')", mainParent.oc);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil menambah member baru");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    
                }
            }
        }

        private void clearWarning()
        {
            warningNIK.Text = "";
            label13.Text = "";
            label14.Text = "";
            label11.Text = "";
            label15.Text = "";
            label16.Text = "";
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }
    }
}
