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
    public partial class FormDetailPerawatan : Form
    {
        public FormDetailPerawatan()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void FormDetailPerawatan_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
        }
    }
}
