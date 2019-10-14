﻿using System;
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
    public partial class Form1 : Form
    {
        public Form curForm = null;
        public Button[] btnMenu;
        public Form1()
        {
            InitializeComponent();
            tabControl1.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            form_pengguna();
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
            tabControl1.Show();
            if(j == "kasir")
            {
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
                btnMenu[1].Text = "New Member";
                btnMenu[2].Text = "Rawat Spesialis";
                btnMenu[3].Text = "Rawat Jalan";
                btnMenu[4].Text = "Rawat Inap";
                btnMenu[5].Text = "Donor";
                btnMenu[6].Text = "Logout";btnMenu[6].Click += logout;

                for (int i = 0; i < 7; i++)
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


        private void data_member(object sender, EventArgs e)
        {
            FormDataMember frm = new FormDataMember();
            addTab(frm);
        }
        private void logout(object sender, EventArgs e)
        {
            for(int i = 0; i<btnMenu.Length; i++)
            {
                Controls.Remove(btnMenu[i]);
            }
            tabControl1.Hide();
            login_form();
        }
    }
}
