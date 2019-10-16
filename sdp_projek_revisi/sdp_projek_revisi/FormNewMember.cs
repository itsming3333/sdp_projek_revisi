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
            if(textBox2.Text.Length >= 2)
            {
                String nama = textBox2.Text;
                if (nama.Length >= 2)
                {
                    String[] splitNama = nama.Split(' ');
                    String id = "";
                    if(splitNama.Length < 2 || splitNama[1].Length == 0)
                    {
                        id = splitNama[0].Substring(0, 2);
                    }
                    else
                    {
                        id = splitNama[0].Substring(0, 1) + splitNama[1].Substring(0, 1);
                    }
                    id = id.ToUpper();

                    OracleCommand cmd = new OracleCommand("SELECT AUTO_GEN_ID_MEMBER('" + id + "') FROM DUAL", mainParent.oc);
                    id += cmd.ExecuteScalar().ToString();
                    textBox1.Text = id.ToUpper();
                }
            }
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
                if (textBox7.Text.Length > 12)
                {
                    label14.Text = "Nomor Telp/HP terlalu panjang";
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
                        String id = textBox1.Text.ToUpper();
                        String nik = textBox8.Text.ToUpper();
                        String nama = textBox2.Text.ToUpper();
                        String telp = textBox7.Text.ToUpper();
                        String alamat = textBox4.Text.ToUpper();
                        String pekerjaan = textBox5.Text.ToUpper();
                        String agama = textBox10.Text.ToUpper();
                        String day = dateTimePicker1.Value.Day.ToString().ToUpper();
                        String mon = dateTimePicker1.Value.Month.ToString().ToUpper();
                        String year = dateTimePicker1.Value.Year.ToString().ToUpper();
                        String jk = "L".ToUpper();
                        if(radioButton2.Checked == true)
                        {
                            jk = "P".ToUpper();
                        }
                        else
                        {
                            jk = "L".ToUpper();
                        }
                        String golDarah = comboBox1.Text.ToUpper();

                        OracleCommand cmd = new OracleCommand("INSERT INTO MEMBER VALUES('"+id+"','"+nama+ "',TO_DATE(LPAD('" + day + "',2,'0')||'/'||LPAD('" + mon + "',2,'0')||'/'||LPAD('" + year + "',4,'0'),'DD/MM/YYYY'),'"+alamat+"','"+telp+"','"+golDarah+"','"+pekerjaan+"','"+agama+"','"+jk+"','"+nik+"',0,0)", mainParent.oc);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Berhasil menambah member baru");
                        textBox2.Text = "";
                        textBox8.Text = "";
                        textBox7.Text = "";
                        textBox4.Text = "";
                        textBox5.Text = "";
                        textBox1.Text = "";
                        textBox10.Text = "";
                        comboBox1.SelectedIndex = 0;
                        radioButton1.Checked = true;
                        dateTimePicker1.Value = DateTime.Now;
                        clearWarning();
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
