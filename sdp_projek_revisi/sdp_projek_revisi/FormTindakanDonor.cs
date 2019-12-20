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
            OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM PEGAWAI WHERE ID_PEGAWAI='" + mainParent.id_login + "'", mainParent.oc);
            DataTable login = new DataTable();
            oda.Fill(login);

            label3.Text = login.Rows[0].Field<String>(0);
            label4.Text = login.Rows[0].Field<String>(2);
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void reset()
        {
            groupBox3.Enabled = false;
            groupBox2.Enabled = false;
            id_donor = "";
            textBox1.Text = "";
            textBox1.Enabled = true;
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            id_donor = textBox1.Text.ToUpper();

            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM DONOR WHERE ID_DONOR='"+id_donor+"' AND REAKSI_DONOR='BELUM ADA TINDAKAN'", mainParent.oc);
            int ada = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            if(ada > 0)
            {
                enableData();
                textBox1.Enabled = false;
            }
            else
            {
                MessageBox.Show("ID Donor salah/sudah ditindaklanjuti.\nPastikan ID Donor benar.");
            }
        }

        private void enableData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM DONOR WHERE ID_DONOR='"+id_donor+"'", mainParent.oc);
            DataTable selectedDonor = new DataTable();
            oda.Fill(selectedDonor);
            String id_member = selectedDonor.Rows[0].Field<String>(1);

            oda = new OracleDataAdapter("SELECT * FROM MEMBER WHERE ID_MEMBER='" + id_member + "'", mainParent.oc);
            DataTable selectedMember = new DataTable();
            oda.Fill(selectedMember);

            label8.Text = id_member;
            String nik = selectedMember.Rows[0].Field<String>(9);
            String nama = selectedMember.Rows[0].Field<String>(1);
            String berat = selectedMember.Rows[0].Field<Int16>(10).ToString();
            String tinggi = selectedMember.Rows[0].Field<Int16>(11).ToString();
            String tensi = selectedDonor.Rows[0].Field<String>(3);
            String suhu = selectedDonor.Rows[0].Field<Int16>(4).ToString();
            String id_supply = selectedDonor.Rows[0].Field<String>(2).ToString();

            label7.Text = nik;
            label6.Text = nama;
            label14.Text = "Berat : "+berat+" kg";
            label15.Text = "Tinggi : "+tinggi+" cm";
            label20.Text = "Tensi : " + tensi + " mmHg";
            label22.Text = "Suhu : " + suhu + " Celcius";
            groupBox2.Enabled = true;

            oda = new OracleDataAdapter("SELECT * FROM SUPPLY WHERE ID_SUPPLY='" + id_supply+ "'", mainParent.oc);
            DataTable selectedSupply = new DataTable();
            oda.Fill(selectedSupply);

            String nama_supply = selectedSupply.Rows[0].Field<String>(1);
            label18.Text = nama_supply;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            bool validate = true;
            
            //VALIDATION ERROR

            if (validate)
            {
                String keterangan = "";
                String barcode = textBox4.Text;
                if (radioButton1.Checked)
                {
                    keterangan = "SUKSES-" + textBox3.Text;
                }
                else
                {
                    keterangan = "GAGAL-berhenti pada : "+numericUpDown1.Value.ToString()+ "CC-" + textBox3.Text;
                }
                String reaksi = "";
                if (radioButton3.Checked)
                {
                    reaksi = "Pingsan";
                }else if (radioButton4.Checked)
                {
                    reaksi = "Pusing";
                }else if (radioButton5.Checked)
                {
                    reaksi = "Bocor";
                }
                else
                {
                    reaksi = textBox2.Text;
                }

                
                String petugas = mainParent.id_login;
                OracleCommand cmd = new OracleCommand("UPDATE DONOR SET ID_PETUGAS_DONOR='"+petugas+"',KETERANGAN_DONOR='"+keterangan+"', REAKSI_DONOR='"+reaksi+"'", mainParent.oc);
                cmd.ExecuteNonQuery();
                cmd = new OracleCommand("SELECT ID_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='"+label18.Text+"'", mainParent.oc);
                String id_supply = cmd.ExecuteScalar().ToString();
                MessageBox.Show(id_supply+" - "+barcode);
                cmd = new OracleCommand("INSERT INTO DSUPPLY VALUES('"+barcode+"','"+id_supply+ "',TO_DATE('30/12/9999','DD/MM/YYYY'),1)", mainParent.oc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil melakukan donor.");

                reset();
            }
            else
            {
                //NOT VALIDATE
            }
        }

        private void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                numericUpDown1.Enabled = true;
            }
            else
            {
                numericUpDown1.Enabled = false;
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            reset();
        }
    }
}
