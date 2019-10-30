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
    public partial class FormLogin : Form
    {
        Form1 parent;
        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            parent = (Form1)this.MdiParent;
            Dock = DockStyle.Fill;
            label4.Text = "";
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            parent.form_pengguna();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String id = textBox1.Text.ToUpper();
            String password = textBox2.Text;
            String jabatan = "";
            OracleDataAdapter oda = new OracleDataAdapter("SELECT J.NAMA_JABATAN FROM PEGAWAI M, JABATAN J WHERE M.ID_JABATAN=J.ID_JABATAN AND M.ID_PEGAWAI='"+id+"' AND M.PASSWORD_PEGAWAI='"+password+"'", parent.oc);
            DataTable selectedMember = new DataTable();
            oda.Fill(selectedMember);

            if (selectedMember.Rows.Count > 0)
            {
                jabatan = selectedMember.Rows[0].Field<String>(0);
                parent.login_as(jabatan);
                this.Close();
            }
            else
            {
                label4.Text = "ID/Password anda salah!";
            }
        }
    }
}
