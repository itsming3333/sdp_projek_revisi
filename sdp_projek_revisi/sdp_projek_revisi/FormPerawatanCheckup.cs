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
    public partial class FormPerawatanCheckup : Form
    {
        Form1 mainParent;
        public FormPerawatanCheckup()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormPerawatanCheckup_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Button6_Click(object sender, EventArgs e)
        {

        }
    }
}
