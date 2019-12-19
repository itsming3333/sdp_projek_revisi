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
    public partial class FormIsiStok : Form
    {
        decimal total;
        String id_isi;
        Form1 mainParent;
        public FormIsiStok()
        {
            InitializeComponent();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }
        private void gen_id()
        {
            String dd = DateTime.Now.Day.ToString();
            String mm = DateTime.Now.Month.ToString();
            String yyyy = DateTime.Now.Year.ToString();
            id_isi = "S" + dd + mm + yyyy;
            OracleCommand cmd = new OracleCommand("SELECT AUTO_GEN_ID_STOK('"+id_isi+"') FROM DUAL", mainParent.oc);
            if(cmd.ExecuteScalar() == null)
            {
                id_isi += "000";
                textBox1.Text = id_isi;
                
            }
            else
            {
                id_isi += cmd.ExecuteScalar().ToString();
                textBox1.Text = id_isi;
            }
            
        }
        private void clearDetail()
        {
            comboBox2.SelectedIndex = 0;
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            label10.Text = "";
            OracleCommand cmd = new OracleCommand("SELECT HARGA_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='" + comboBox2.Text + "'", mainParent.oc);
            if (cmd.ExecuteScalar() != null)
            {
                numericUpDown2.Value = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "SELECT SATUAN FROM SUPPLY WHERE NAMA_SUPPLY='" + comboBox2.Text + "'";
                label5.Text = cmd.ExecuteScalar().ToString();
            }
        }
        private void resetData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NAMA_PRODUSEN FROM PRODUSEN", mainParent.oc);
            DataTable produsen = new DataTable();
            oda.Fill(produsen);
            comboBox1.DataSource = produsen;
            comboBox1.DisplayMember = "NAMA_PRODUSEN";
             oda = new OracleDataAdapter("SELECT NAMA_SUPPLY FROM SUPPLY", mainParent.oc);
            DataTable supply = new DataTable();
            oda.Fill(supply);
            comboBox2.DataSource = supply;
            comboBox2.DisplayMember = "NAMA_SUPPLY";

            groupBox1.Enabled = true;
            groupBox2.Enabled = false;
            dataGridView1.Rows.Clear();
            textBox3.Text = "";
            total = 0;
            label12.Text = "0";
            gen_id();
            clearDetail();
            comboBox1.Text = "";
            OracleCommand cmd = new OracleCommand("SELECT HARGA_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='" + comboBox2.Text + "'", mainParent.oc);
            if (cmd.ExecuteScalar() != null)
            {
                numericUpDown2.Value = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "SELECT SATUAN FROM SUPPLY WHERE NAMA_SUPPLY='" + comboBox2.Text + "'";
                label5.Text = cmd.ExecuteScalar().ToString();
            }
        }


        private void FormIsiStok_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            resetData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void GroupBox2_Enter(object sender, EventArgs e)
        {

        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Label12_Click(object sender, EventArgs e)
        {

        }
        private void editSubtotal()
        {
            decimal harga = numericUpDown2.Value;
            decimal qty = numericUpDown1.Value;
            decimal total = harga * qty;
            label10.Text = total + "";
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" && textBox3.Text == "")
            {
                MessageBox.Show("HARUS DIISI");
            }
            else
            {
                groupBox1.Enabled = false;
                groupBox2.Enabled = true;
            }
        }

        private void NumericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            editSubtotal();
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            editSubtotal();
        }

        private void NumericUpDown2_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void NumericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void NumericUpDown2_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void NumericUpDown1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void NumericUpDown2_KeyUp(object sender, KeyEventArgs e)
        {
            editSubtotal();
        }

        private void NumericUpDown1_KeyUp(object sender, KeyEventArgs e)
        {
            editSubtotal();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            clearDetail();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            resetData();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            OracleCommand cmd = new OracleCommand("SELECT HARGA_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='" + comboBox2.Text + "'", mainParent.oc);
            if(cmd.ExecuteScalar() != null)
            {
                numericUpDown2.Value = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                cmd.CommandText = "SELECT SATUAN FROM SUPPLY WHERE NAMA_SUPPLY='"+comboBox2.Text+"'";
                label5.Text = cmd.ExecuteScalar().ToString();
            }
            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            bool ada = false;
            int sameRow = -1;
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                try
                {
                    if (dataGridView1.Rows[i].Cells[0].Value.ToString() == comboBox2.Text)
                    {
                        ada = true;
                        sameRow = i;
                    }
                }
                catch (Exception)
                {}
            }
            if (!ada)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dataGridView1);
                row.Cells[0].Value = comboBox2.Text;
                row.Cells[1].Value = label5.Text;
                row.Cells[2].Value = numericUpDown2.Value.ToString();
                row.Cells[3].Value = numericUpDown1.Value.ToString();
                row.Cells[4].Value = label10.Text;
                row.Cells[5].Value = dateTimePicker1.Value.Day.ToString().PadLeft(2,'0') + "/" + dateTimePicker1.Value.Month.ToString().PadLeft(2,'0') + "/" + dateTimePicker1.Value.Year.ToString().PadLeft(4,'0');

                dataGridView1.Rows.Add(row);
            }
            else
            {
                if (MessageBox.Show("Supply yang sama sudah ada apakah ingin mengubah data isi stok supply ?", "Hapus Member", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    dataGridView1.Rows[sameRow].Cells[0].Value = comboBox2.Text;
                    dataGridView1.Rows[sameRow].Cells[1].Value = label5.Text;
                    dataGridView1.Rows[sameRow].Cells[2].Value = numericUpDown2.Value.ToString();
                    dataGridView1.Rows[sameRow].Cells[3].Value = numericUpDown1.Value.ToString();
                    dataGridView1.Rows[sameRow].Cells[4].Value = label10.Text;
                }
            }
            updateTotal();
            clearDetail();
        }

        private void updateTotal()
        {
            int total = 0;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                total += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
            }
            label12.Text = total+"";
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            //add produsen
            String nama_produsen = comboBox1.Text;
            OracleCommand cmd = new OracleCommand("SELECT COUNT(*) FROM PRODUSEN WHERE NAMA_PRODUSEN='"+nama_produsen+"'", mainParent.oc);
            int ada = Convert.ToInt32(cmd.ExecuteScalar().ToString());
            String id_produsen = "";
            if (ada > 0)
            {
                cmd = new OracleCommand("SELECT ID_PRODUSEN FROM PRODUSEN WHERE NAMA_PRODUSEN='"+nama_produsen+"'", mainParent.oc);
                id_produsen = cmd.ExecuteScalar().ToString();
            }
            else
            {
                cmd = new OracleCommand("SELECT MAX(ID_PRODUSEN) FROM PRODUSEN", mainParent.oc);
                int auto_inc;
                if (cmd.ExecuteScalar() != null)
                {
                    auto_inc = 0;
                }
                else
                {
                    auto_inc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(1, 4)) + 1;
                }
               
                id_produsen = "P" + auto_inc.ToString().PadLeft(4, '0');

                cmd = new OracleCommand("INSERT INTO PRODUSEN VALUES('"+id_produsen+"','"+nama_produsen+"')", mainParent.oc);
                cmd.ExecuteNonQuery();
            }
            //Add isi_stok
            String dd = DateTime.Now.Day.ToString();
            String mm = DateTime.Now.Month.ToString();
            String yyyy = DateTime.Now.Year.ToString();
            String tanda = textBox3.Text;

            cmd = new OracleCommand("INSERT INTO ISI_STOK VALUES('"+id_isi+"','"+id_produsen+ "',TO_DATE(LPAD('" + dd + "',2,'0')||'/'||LPAD('" + mm + "',2,'0')||'/'||LPAD('" + yyyy + "',4,'0'),'DD/MM/YYYY'),'" + tanda+"')", mainParent.oc);
            cmd.ExecuteNonQuery();

            //Add Detail Stok
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                try
                {
                    cmd = new OracleCommand("SELECT ID_SUPPLY FROM SUPPLY WHERE NAMA_SUPPLY='" + dataGridView1.Rows[i].Cells[0].Value.ToString() + "'", mainParent.oc);
                    String id_supply = cmd.ExecuteScalar().ToString();
                    String total = dataGridView1.Rows[i].Cells[3].Value.ToString();
                    String harga_beli = dataGridView1.Rows[i].Cells[2].Value.ToString();
                    String expired = dataGridView1.Rows[i].Cells[5].Value.ToString();
                    cmd = new OracleCommand("INSERT INTO DTRANS_STOK VALUES('" + id_isi + "','" + id_supply + "'," + total + "," + harga_beli + ")", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    cmd = new OracleCommand("INSERT INTO DSUPPLY VALUES('"+tanda+"','"+id_supply+"',TO_DATE('"+expired+"','DD/MM/YYYY'),"+total+")", mainParent.oc);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {}
                    
            }

            MessageBox.Show("Berhasil melakukan transaksi isi stok");
            resetData();
        }
    }
}
