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
    public partial class FormNewObat : Form
    {
        Form1 mainParent;
        public FormNewObat()
        {
            InitializeComponent();
        }

        private void FormNewObat_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            clear();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        public void setParent(Form1 frm)
        {
            this.mainParent = frm;
        }
        private void clear()
        {
            textBox2.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox3.Text = "";
            label13.Text = "";
            label14.Text = "";
            label5.Text = "";
            warningNIK.Text = "";
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String id = textBox1.Text.ToUpper();
            String nama = textBox2.Text.ToUpper();
            String ket = textBox8.Text.ToUpper();
            String harga = textBox7.Text.ToUpper();
            String jenis = "OBAT";
            String satuan = textBox3.Text.ToUpper();

            try
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO SUPPLY VALUES('"+id+"','"+nama+"','"+ket+"',"+harga+",'"+jenis+"','"+satuan+"')", mainParent.oc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil menambahkan obat baru.");
                clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
            {
                OracleCommand cmd = new OracleCommand("SELECT MAX(ID_SUPPLY) FROM SUPPLY", mainParent.oc);
                int top_num = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(2, 3));
                top_num++;
                String id = "SO" + top_num.ToString().PadLeft(3, '0');
                textBox1.Text = id;
            }
            else
            {
                textBox1.Text = "XXXXX";
            }
        }
    }
}
