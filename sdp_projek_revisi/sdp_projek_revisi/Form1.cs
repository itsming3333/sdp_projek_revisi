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
    public partial class Form1 : Form
    {
        public OracleConnection oc;
        public Form curForm = null;
        public Button[] btnMenu;
        public String passAdmin = "sdp2019";
        public Form1()
        {
            InitializeComponent();
            tabControl1.Hide();
            button1.Hide();

            //INISIALISASI CONNECTION
            try{
                oc = new OracleConnection("user id=n217116635;password=217116635;data source = orcl");
                oc.Open();
                MessageBox.Show("Connection Open Ming");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fail Ivan : " + ex.Message);
                //TAMBAH KONEKSI LAINNYA
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form_pengguna();
        }

        public void admin_form()
        {
            FormAdmin fl = new FormAdmin();
            fl.MdiParent = this;
            fl.Show();
            if (curForm != null)
            {
                curForm.Close();
            }
            curForm = fl;
        }

        public void login_form()
        {
            FormLogin fl = new FormLogin();
            fl.MdiParent = this;
            fl.Show();
            if(curForm != null)
            {
                curForm.Close();
            }
            curForm = fl;
        }
        public void form_pengguna()
        {
            FormPengguna fp = new FormPengguna();
            fp.MdiParent = this;
            fp.Show();
            if (curForm != null)
            {
                curForm.Close();
            }
            curForm = fp;
        }

        public void login_as(String j)
        {
            tabControl1.Size = new Size(1150, 600);
            tabControl1.TabPages.Clear();
            button1.Show();
            tabControl1.Show();

            if (j == "kasir")
            {
                FormWelcome frm = new FormWelcome();
                addTab(frm);
                btnMenu = new Button[7];
                for(int i = 0; i<7; i++)
                {
                    btnMenu[i] = new Button();
                    btnMenu[i].Height = 100;
                    btnMenu[i].Width = 100;
                    btnMenu[i].BackColor = Color.LightBlue;
                    btnMenu[i].Location = new Point(i*100+15, 15);
                    btnMenu[i].Font = new Font("MS Reference Sans Serif", 12);
                }
                btnMenu[0].Text = "Data Member";btnMenu[0].Click += data_member;
                btnMenu[1].Text = "Register Member";btnMenu[1].Click += new_member;
                btnMenu[2].Text = "Rawat Spesialis";btnMenu[2].Click += rawat_spesialis;
                btnMenu[3].Text = "Rawat Jalan";btnMenu[3].Click += rawat_jalan;
                btnMenu[4].Text = "Rawat Inap";btnMenu[4].Click += rawat_inap;
                btnMenu[5].Text = "Donor";btnMenu[5].Click += donor;
                btnMenu[6].Text = "Logout";btnMenu[6].Click += logout;

                for (int i = 0; i < 7; i++)
                {
                    Controls.Add(btnMenu[i]);
                }
            }else if(j == "admin")
            {
                FormWelcome frm = new FormWelcome();
                addTab(frm);
                btnMenu = new Button[4];
                for (int i = 0; i < 4; i++)
                {
                    btnMenu[i] = new Button();
                    btnMenu[i].Height = 100;
                    btnMenu[i].Width = 100;
                    btnMenu[i].BackColor = Color.LightBlue;
                    btnMenu[i].Location = new Point(i * 100 + 15, 15);
                    btnMenu[i].Font = new Font("MS Reference Sans Serif", 12);
                }
                btnMenu[0].Text = "Data Pegawai"; btnMenu[0].Click += data_pegawai;
                btnMenu[1].Text = "Register Pegawai"; btnMenu[1].Click += new_pegawai;
                btnMenu[2].Text = "Admin Rumah Sakit"; btnMenu[2].Click += data_rs;
                btnMenu[3].Text = "Logout"; btnMenu[3].Click += logout;

                for (int i = 0; i < 4; i++)
                {
                    Controls.Add(btnMenu[i]);
                }
            }
        }
        
        public void addTab(Form frm)
        {
            TabPage tab = new TabPage(frm.Text);
            frm.TopLevel = false;
            frm.Parent = tab;
            frm.Visible = true;
            frm.Location = new Point((tab.Width - frm.Width) / 2, (tab.Height - frm.Height) / 2);
            tabControl1.SelectedTab = tab;
            tabControl1.TabPages.Add(tab);
        }
        private void rawat_jalan(object sender, EventArgs e)
        {
            FormRawatJalan frm = new FormRawatJalan();
            frm.setParent(this);
            addTab(frm);
        }
        private void data_pegawai(object sender, EventArgs e)
        {
            FormDataPegawai frm = new FormDataPegawai();
            frm.setParent(this);
            addTab(frm);
        }
        private void new_pegawai(object sender, EventArgs e)
        {
            FormNewPegawai frm = new FormNewPegawai();
            frm.setParent(this);
            addTab(frm);
        }
        private void rawat_spesialis(object sender, EventArgs e)
        {
            FormRawatSpesialis frm = new FormRawatSpesialis();
            frm.setParent(this);
            addTab(frm);
        }
        private void donor(object sender, EventArgs e)
        {
            FormDonor frm = new FormDonor();
            frm.setParent(this);
            addTab(frm);
        }
        private void data_rs(object sender, EventArgs e)
        {
            FormDataRs frm = new FormDataRs();
            frm.setParent(this);
            addTab(frm);
        }
        private void rawat_inap(object sender, EventArgs e)
        {
            FormRawatInap frm = new FormRawatInap();
            frm.setMainParent(this);
            addTab(frm);
        }

        private void data_member(object sender, EventArgs e)
        {
            FormDataMember frm = new FormDataMember();
            frm.setMainParent(this);
            addTab(frm);
        }

        private void new_member(object sender, EventArgs e)
        {
            FormNewMember frm = new FormNewMember();
            frm.setParent(this);
            addTab(frm);
        }
        private void logout(object sender, EventArgs e)
        {
            for(int i = 0; i<btnMenu.Length; i++)
            {
                Controls.Remove(btnMenu[i]);
            }
            button1.Hide();
            tabControl1.Hide();
            form_pengguna();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if(tabControl1.TabPages.Count > 0)
            {
                tabControl1.TabPages.Remove(tabControl1.SelectedTab);
            }

            if(tabControl1.TabPages.Count == 0)
            {
                FormWelcome frm = new FormWelcome();
                addTab(frm);
            }
        }
    }
}
