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
    public partial class FormTindakanDonor : Form
    {
        Form1 mainParent;
        String id_donor;
        public FormTindakanDonor()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormTindakanDonor_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            groupBox2.Enabled = false;
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void reset()
        {
            groupBox3.Enabled = false;
            groupBox2.Enabled = false;
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            id_donor = textBox1.Text;

            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM DONOR WHERE ID_DONOR='"+id_donor+"'", mainParent.oc);
            int ada = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            if(ada > 0)
            {
                enableData();
            }
            else
            {
                MessageBox.Show("ID Donor salah/tidak ada.\nPastikan ID Donor benar.");
            }
        }

        private void enableData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM DONOR WHERE ID_DONOR='"+id_donor+"'", mainParent.oc);
            DataTable selectedDonor = new DataTable();
            oda.Fill(selectedDonor);

            String id_member = selectedDonor.Rows[0].Field<String>(1);

            oda = new OracleDataAdapter("SELECT * FROM DONOR WHERE ID_MEMBER='" + id_member + "'", mainParent.oc);
            DataTable selectedMember = new DataTable();
            oda.Fill(selectedMember);

            label8.Text = id_member;


        }
    }
}
