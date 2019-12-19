using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
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
    public partial class FormAllRepost : Form
    {
        public FormAllRepost()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                //Uang  
                ReportKeuangen cr1 = new ReportKeuangen();
                cr1.SetDatabaseLogon("system", "michael123", "laptop-c8ps48dq", "");
                cr1.SetParameterValue("DateAwal", dateTimePicker1.Value);
                cr1.SetParameterValue("DateAkhir", dateTimePicker2.Value);

                crystalReportViewer1.ReportSource = cr1;
                crystalReportViewer1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                //gudang
                ReportStok cr1 = new ReportStok();
                cr1.SetDatabaseLogon("system", "michael123", "laptop-c8ps48dq", "");
                cr1.SetParameterValue("DateAwal", dateTimePicker1.Value);
                cr1.SetParameterValue("DateAkhir", dateTimePicker2.Value);

                crystalReportViewer1.ReportSource = cr1;
                crystalReportViewer1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 2)
            {
                //Rawat
                LaporanPerawatan cr1 = new LaporanPerawatan();
                cr1.SetDatabaseLogon("system", "michael123", "laptop-c8ps48dq", "");
                cr1.SetParameterValue("DateAwal", dateTimePicker1.Value);
                cr1.SetParameterValue("DateAkhir", dateTimePicker2.Value);

                crystalReportViewer1.ReportSource = cr1;
                crystalReportViewer1.Refresh();
            }
            else if (comboBox1.SelectedIndex == 3)
            {
                //Donor
                ReportDonor cr1 = new ReportDonor();
                cr1.SetDatabaseLogon("system", "michael123", "laptop-c8ps48dq", "");
                cr1.SetParameterValue("DateAwal", dateTimePicker1.Value);
                cr1.SetParameterValue("DateAkhir", dateTimePicker2.Value);

                crystalReportViewer1.ReportSource = cr1;
                crystalReportViewer1.Refresh();
            }
        }
    }
}
