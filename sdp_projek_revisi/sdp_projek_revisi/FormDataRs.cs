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
    public partial class FormDataRs : Form
    {
        Form1 mainParent;
        String id_selected_perawatan = "";
        public FormDataRs()
        {
            InitializeComponent();
        }

        private void FormDataRs_Load(object sender, EventArgs e)
        {
            Dock = DockStyle.Fill;
            label2.Text = DateTime.Now.ToString();
            timer1.Start();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            showData();
            clearPerawatan();
            clearRuang();
            clearShift();
            button7.Enabled = false;
            button7.Enabled = true;
        }

        public void showData()
        {
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NAMA_PERAWATAN AS NAMA, DESKRIPSI_PERAWATAN AS DESKRIPSI, HARGA_PERAWATAN AS HARGA FROM PERAWATAN", mainParent.oc);
            DataTable perawatan = new DataTable();
            oda.Fill(perawatan);

            dataGridView1.DataSource = perawatan;

            oda = new OracleDataAdapter("SELECT NOMOR_RUANG AS NOMOR, NAMA_RUANG AS NAMA, JENIS_RUANG AS JENIS FROM RUANG", mainParent.oc);
            DataTable ruang = new DataTable();
            oda.Fill(ruang);

            dataGridView2.DataSource = ruang;

            oda = new OracleDataAdapter("SELECT SH.HARI_SHIFT AS HARI, TO_CHAR(SH.WAKTU_MULAI,'HH24:MI:SS') AS MULAI, TO_CHAR(SH.WAKTU_SELESAI,'HH24:MI:SS') AS SELESAI, P.ID_PEGAWAI AS DOKTER FROM SHIFT_SPESIALIS SH, PEGAWAI P, PERAWATAN PR,JABATAN J, RUANG R WHERE SH.ID_PERAWATAN = '" + id_selected_perawatan+"' AND SH.ID_PERAWATAN = PR.ID_PERAWATAN AND SH.ID_PEGAWAI = P.ID_PEGAWAI AND SH.ID_RUANG = R.ID_RUANG AND J.ID_JABATAN = P.ID_JABATAN AND J.NAMA_JABATAN='DOKTER'", mainParent.oc);
            DataTable jadwalShift = new DataTable();
            oda.Fill(jadwalShift);

            dataGridView4.DataSource = jadwalShift;

            oda = new OracleDataAdapter("SELECT NAMA_PERAWATAN FROM PERAWATAN", mainParent.oc);
            DataTable spesialis = new DataTable();
            oda.Fill(spesialis);
            comboBox6.DataSource = spesialis;
            comboBox6.DisplayMember = "NAMA_PERAWATAN";
            comboBox6.ValueMember = "NAMA_PERAWATAN";
            OracleCommand cmd = new OracleCommand("SELECT HARGA_PERAWATAN FROM PERAWATAN WHERE NAMA_PERAWATAN='" + id_selected_perawatan + "'", mainParent.oc);
            label24.Text = cmd.ExecuteScalar().ToString();

            oda = new OracleDataAdapter("SELECT NOMOR_RUANG FROM RUANG WHERE JENIS_RUANG='PRAKTEK' AND STATUS_RUANG='OPEN'", mainParent.oc);
            DataTable ruang_rawat = new DataTable();
            oda.Fill(ruang_rawat);
            comboBox3.DataSource = ruang_rawat;
            comboBox3.DisplayMember = "NOMOR_RUANG";
            comboBox3.ValueMember = "NOMOR_RUANG";

            oda = new OracleDataAdapter("SELECT NAMA_PEGAWAI FROM PEGAWAI WHERE ID_JABATAN='J02'", mainParent.oc);
            DataTable dokter = new DataTable();
            oda.Fill(dokter);
            comboBox4.DataSource = dokter;
            comboBox4.DisplayMember = "NAMA_PEGAWAI";
            comboBox4.ValueMember = "NAMA_PEGAWAI";

            oda = new OracleDataAdapter("SELECT NAMA_PEGAWAI FROM PEGAWAI WHERE ID_JABATAN='J03'", mainParent.oc);
            DataTable suster = new DataTable();
            oda.Fill(suster);
            listBox1.Items.Clear();
            for (int i = 0; i < suster.Rows.Count; i++)
            {
                listBox1.Items.Add(suster.Rows[i].Field<String>(0));
            }
        }

        private void Label9_Click(object sender, EventArgs e)
        {

        }

        private void DataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void setParent(Form1 parent)
        {
            this.mainParent = parent;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToString();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            clearPerawatan();
        }

        private void clearPerawatan()
        {
            textBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;
            comboBox1.Text = "";

            button1.Show();
            button2.Hide();
            button3.Hide();
            button12.Hide();
        }
        private void clearRuang()
        {
            numericUpDown2.Value = 0;
            textBox4.Text = "";
            textBox5.Text = "";
            comboBox2.Text ="";
            numericUpDown3.Value = 0;
            label12.Text = "NAMA RUANGAN";
            label13.Text = "NOMOR";


            button4.Show();
            button13.Hide();
            button5.Hide();
            button6.Hide();
        }
        private void clearShift()
        {
            textBox6.Text = "";
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
            comboBox5.SelectedIndex = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            numericUpDown7.Value = 0;
            numericUpDown6.Value = 0;
            listBox2.DataSource = null;
            listBox2.Items.Clear();
            OracleDataAdapter oda = new OracleDataAdapter("SELECT NAMA_PEGAWAI FROM PEGAWAI WHERE ID_JABATAN='J03'", mainParent.oc);
            DataTable suster = new DataTable();
            oda.Fill(suster);
            listBox1.Items.Clear();
            for (int i = 0; i < suster.Rows.Count; i++)
            {
                listBox1.Items.Add(suster.Rows[i].Field<String>(0));
            }


            button7.Show();
            button8.Hide();
            button14.Hide();
            button9.Hide();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            clearRuang();
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            clearShift();
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                button1.Hide();
                button2.Show();
                button3.Show();
                button12.Show();
                int row = e.RowIndex;
                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM PERAWATAN WHERE NAMA_PERAWATAN='"+dataGridView1.Rows[row].Cells[0].Value.ToString()+"'", mainParent.oc);
                DataTable rawat = new DataTable();
                oda.Fill(rawat);
                textBox3.Text = rawat.Rows[0].Field<String>(0);
                textBox1.Text = rawat.Rows[0].Field<String>(1);
                textBox2.Text = rawat.Rows[0].Field<String>(2);
                numericUpDown1.Value = Convert.ToInt32(rawat.Rows[0].Field<Int64>(3));
                comboBox1.Text = rawat.Rows[0].Field<String>(4);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            try
            {
                button4.Hide();
                button13.Show();
                button5.Show();
                button6.Show();
                int row = e.RowIndex;
                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM RUANG WHERE NOMOR_RUANG='" + dataGridView2.Rows[row].Cells[0].Value.ToString() + "' AND NAMA_RUANG='" + dataGridView2.Rows[row].Cells[1].Value.ToString() + "' AND JENIS_RUANG='" + dataGridView2.Rows[row].Cells[2].Value.ToString() + "'", mainParent.oc);
                DataTable rawat = new DataTable();
                oda.Fill(rawat);

                numericUpDown2.Value = Convert.ToInt32(rawat.Rows[0].Field<String>(1).Substring(1, 1));
                textBox7.Text = rawat.Rows[0].Field<String>(1).Substring(2, 2);
                textBox4.Text = rawat.Rows[0].Field<String>(0);
                textBox5.Text = rawat.Rows[0].Field<String>(5);
                comboBox2.Text = rawat.Rows[0].Field<String>(2);
                numericUpDown3.Value = Convert.ToInt32(rawat.Rows[0].Field<Int64>(3));
                label12.Text = textBox5.Text;
                label13.Text = rawat.Rows[0].Field<String>(1);

                
            }
            catch (Exception ex)
            {}
            
        }

        private void DataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            try
            {
                clearShift();
                button7.Hide();
                button8.Show();
                button14.Show();
                button9.Show();
                OracleCommand cmd = new OracleCommand("SELECT ID_PEGAWAI FROM PEGAWAI WHERE NAMA_PEGAWAI='"+dataGridView4.Rows[row].Cells[3].Value.ToString()+"'", mainParent.oc);
                String id_pegawai = cmd.ExecuteScalar().ToString();
                cmd = new OracleCommand("SELECT ID_PERAWATAN FROM PERAWATAN WHERE NAMA_PERAWATAN='" + comboBox6.Text + "'", mainParent.oc);
                String id_rawat = cmd.ExecuteScalar().ToString();
                String waktu_mulai = dataGridView4.Rows[row].Cells[1].Value.ToString();
                String waktu_selesai = dataGridView4.Rows[row].Cells[2].Value.ToString();
                String hari = dataGridView4.Rows[row].Cells[0].Value.ToString();

                OracleDataAdapter oda = new OracleDataAdapter("SELECT * FROM SHIFT_SPESIALIS WHERE ID_PEGAWAI='"+id_pegawai+"' AND ID_PERAWATAN='"+id_rawat+"' AND TO_CHAR(WAKTU_MULAI,'HH24:MI:SS')='"+waktu_mulai+"' AND TO_CHAR(WAKTU_SELESAI,'HH24:MI:SS') = '"+waktu_selesai+"' AND HARI_SHIFT='"+hari+"'", mainParent.oc);
                DataTable selectedShift = new DataTable();
                oda.Fill(selectedShift);

                textBox6.Text = selectedShift.Rows[0].Field<String>(5);
                comboBox5.Text = selectedShift.Rows[0].Field<String>(7);
                numericUpDown4.Value = Convert.ToInt32(selectedShift.Rows[0].Field<DateTime>(2).Hour.ToString());
                numericUpDown5.Value = Convert.ToInt32(selectedShift.Rows[0].Field<DateTime>(2).Minute.ToString());
                numericUpDown7.Value = Convert.ToInt32(selectedShift.Rows[0].Field<DateTime>(3).Hour.ToString());
                numericUpDown6.Value = Convert.ToInt32(selectedShift.Rows[0].Field<DateTime>(3).Minute.ToString());

                cmd = new OracleCommand("SELECT NOMOR_RUANG FROM RUANG WHERE ID_RUANG='"+ selectedShift.Rows[0].Field<String>(4) +"'", mainParent.oc);
                comboBox3.Text = cmd.ExecuteScalar().ToString();
                cmd = new OracleCommand("SELECT NAMA_PEGAWAI FROM PEGAWAI WHERE ID_PEGAWAI='"+ selectedShift.Rows[0].Field<String>(0) +"'", mainParent.oc);
                comboBox4.Text = cmd.ExecuteScalar().ToString();
                String id_shift = selectedShift.Rows[0].Field<String>(5);

                oda = new OracleDataAdapter("SELECT NAMA_PEGAWAI FROM PEGAWAI WHERE ID_JABATAN='J03'", mainParent.oc);
                DataTable suster = new DataTable();
                oda.Fill(suster);
                listBox1.Items.Clear();
                for (int i = 0; i < suster.Rows.Count; i++)
                {
                    listBox1.Items.Add(suster.Rows[i].Field<String>(0));
                }

                oda = new OracleDataAdapter("SELECT P.NAMA_PEGAWAI FROM PEGAWAI P, SHIFT_SPESIALIS SS WHERE SS.ID_PEGAWAI = P.ID_PEGAWAI AND SS.ID_SHIFT = '"+id_shift+"' AND P.ID_JABATAN='J03'", mainParent.oc);
                DataTable susterKerja = new DataTable();
                oda.Fill(susterKerja);

                for (int i = 0; i < susterKerja.Rows.Count; i++)
                {
                    listBox2.Items.Add(susterKerja.Rows[i].Field<String>(0));
                    listBox1.Items.Remove(susterKerja.Rows[i].Field<String>(0));
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ComboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            id_selected_perawatan = comboBox6.Text;
            OracleDataAdapter oda = new OracleDataAdapter("SELECT SH.HARI_SHIFT AS HARI, TO_CHAR(SH.WAKTU_MULAI,'HH24:MI:SS') AS MULAI, TO_CHAR(SH.WAKTU_SELESAI,'HH24:MI:SS') AS SELESAI, P.NAMA_PEGAWAI AS DOKTER FROM SHIFT_SPESIALIS SH, PEGAWAI P, PERAWATAN PR,JABATAN J, RUANG R WHERE PR.NAMA_PERAWATAN = '" + id_selected_perawatan + "' AND SH.ID_PERAWATAN = PR.ID_PERAWATAN AND SH.ID_PEGAWAI = P.ID_PEGAWAI AND SH.ID_RUANG = R.ID_RUANG AND J.ID_JABATAN = P.ID_JABATAN AND J.NAMA_JABATAN='DOKTER'", mainParent.oc);
            DataTable jadwalShift = new DataTable();
            oda.Fill(jadwalShift);

            dataGridView4.DataSource = jadwalShift;
            OracleCommand cmd;
            try
            {
                cmd = new OracleCommand("SELECT HARGA_PERAWATAN FROM PERAWATAN WHERE NAMA_PERAWATAN='" + id_selected_perawatan + "'", mainParent.oc);
                label24.Text = cmd.ExecuteScalar().ToString();
            }
            catch (Exception)
            {}

            if (button7.Visible)
            {
                try
                {
                    cmd = new OracleCommand("SELECT ID_PERAWATAN FROM PERAWATAN WHERE NAMA_PERAWATAN='" + comboBox6.Text + "'", mainParent.oc);
                    String id_rawat = cmd.ExecuteScalar().ToString();
                    cmd = new OracleCommand("SELECT MAX(ID_SHIFT) FROM SHIFT_SPESIALIS WHERE ID_SHIFT LIKE '%" + id_rawat + "%'", mainParent.oc);
                    String id_shift = "";
                    if (cmd.ExecuteScalar() != null)
                    {
                        int auto_inc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(5, 3));
                        auto_inc++;
                        id_shift = id_rawat + auto_inc.ToString().PadLeft(3, '0');
                    }
                    else
                    {
                        id_shift = id_rawat + "000";
                    }
                    textBox6.Text = id_shift;
                }
                catch (Exception)
                { }
            }            
        }

        private void GroupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void Button10_Click(object sender, EventArgs e)
        {
            try
            {
                listBox2.Items.Add(listBox1.Items[listBox1.SelectedIndex]);
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
            catch (Exception)
            {
            }
            
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Add(listBox2.Items[listBox2.SelectedIndex]);
                listBox2.Items.RemoveAt(listBox2.SelectedIndex);
            }
            catch (Exception)
            { }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda yakin ingin menghapus perawatan ?", "Hapus Perawatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                OracleCommand cmd = new OracleCommand("DELETE FROM PERAWATAN WHERE ID_PERAWATAN = '" + textBox3.Text + "'", mainParent.oc);
                cmd.ExecuteNonQuery();
                showData();
                clearPerawatan();
                MessageBox.Show("Perawatan berhasil dihapus.");
            }
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda yakin ingin menghapus ruangan ?", "Hapus Ruang", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                OracleCommand cmd = new OracleCommand("SELECT STATUS_RUANG FROM RUANG WHERE ID_RUANG='"+textBox4.Text+"'", mainParent.oc);
                String status = cmd.ExecuteScalar().ToString();

                if(status == "CLOSED")
                {
                    MessageBox.Show("Gagal menghapus ruangan!\nPastikan ruangan dalam keadaan kosong.");
                }
                else
                {
                    cmd = new OracleCommand("DELETE FROM RUANG WHERE ID_RUANG = '" + textBox4.Text + "'", mainParent.oc);
                    cmd.ExecuteNonQuery();
                    showData();
                    clearRuang();
                    MessageBox.Show("Ruangan berhasil dihapus.");
                }

            }
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah anda yakin ingin menghapus shift ?", "Hapus Shift", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                OracleCommand cmd = new OracleCommand("DELETE FROM SHIFT_SPESIALIS WHERE ID_SHIFT='"+textBox6.Text+"'", mainParent.oc);
                cmd.ExecuteNonQuery();
                showData();
                clearShift();
                MessageBox.Show("Shift Pilihan berhasil dihapus.");
            }
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            String nama = textBox1.Text;
            String desc = textBox2.Text;
            String harga = numericUpDown1.Value.ToString();
            String jenis = comboBox1.Text;

            OracleCommand cmd = new OracleCommand("UPDATE PERAWATAN SET NAMA_PERAWATAN='"+nama+"', DESKRIPSI_PERAWATAN='"+desc+"', HARGA_PERAWATAN="+harga+", JENIS_PERAWATAN='"+jenis+"' WHERE ID_PERAWATAN='"+textBox3.Text+"'", mainParent.oc);
            if (MessageBox.Show("Apakah anda yakin ingin mengubah data perawatan ?", "Edit Perawatan", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                cmd.ExecuteNonQuery();
                showData();
                clearRuang();
                MessageBox.Show("Berhasil mengubah data perawatan.");
            }
        }

        private void Button13_Click(object sender, EventArgs e)
        {
            String kode_nomor = "";
            if(comboBox2.SelectedIndex == 0)
            {
                kode_nomor = "I";
            }else if (comboBox2.SelectedIndex == 1)
            {
                kode_nomor = "P";
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                kode_nomor = "O";
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                kode_nomor = "C";
            }
            else if (comboBox2.SelectedIndex == 4)
            {
                kode_nomor = "R";
            }
            else if (comboBox2.SelectedIndex == 5)
            {
                kode_nomor = "G";
            }
            else if (comboBox2.SelectedIndex == 6)
            {
                kode_nomor = "L";
            }
            else if (comboBox2.SelectedIndex == 7)
            {
                kode_nomor = "S";
            }
            else if (comboBox2.SelectedIndex == 8)
            {
                kode_nomor = "A";
            }
            else if (comboBox2.SelectedIndex == 9)
            {
                kode_nomor = "B";
            }
            else if (comboBox2.SelectedIndex == 10)
            {
                kode_nomor = "K";
            }

            String lantai = numericUpDown2.Value.ToString();
            String unique = textBox7.Text;
            String nomor = kode_nomor.ToUpper() + lantai.ToUpper() + unique.ToUpper();
            String nama = textBox5.Text.ToUpper();
            String jenis = comboBox2.Text.ToUpper();
            String harga = numericUpDown3.Value.ToString();

            try
            {
                OracleCommand cmd = new OracleCommand("UPDATE RUANG SET NOMOR_RUANG='" + nomor + "', JENIS_RUANG='" + jenis + "', HARGA_RUANG=" + harga + ", NAMA_RUANG='" + nama + "' WHERE ID_RUANG='" + textBox4.Text + "'", mainParent.oc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil mengubah data ruang.");
                clearRuang();
                showData();
            }
            catch (Exception)
            {}
        }

        private void TextBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button14_Click(object sender, EventArgs e)
        {
            String id_shift = textBox6.Text;
            try
            {
                OracleCommand cmd = new OracleCommand("DELETE FROM SHIFT_SPESIALIS WHERE ID_SHIFT='"+id_shift+"'",mainParent.oc);
                cmd.ExecuteNonQuery();
                String hari = comboBox5.Text;
                String hhAwal = numericUpDown4.Value.ToString();
                String mmAwal = numericUpDown5.Value.ToString();
                String hhAkhir = numericUpDown7.Value.ToString();
                String mmAkhir = numericUpDown6.Value.ToString();
                cmd = new OracleCommand("SELECT ID_RUANG FROM RUANG WHERE NOMOR_RUANG='"+comboBox3.Text+"' AND JENIS_RUANG='PRAKTEK'",mainParent.oc);
                String id_ruang = cmd.ExecuteScalar().ToString();
                cmd = new OracleCommand("SELECT ID_PEGAWAI FROM PEGAWAI WHERE NAMA_PEGAWAI='"+comboBox4.Text+"'", mainParent.oc);
                String id_dokter = cmd.ExecuteScalar().ToString();
                cmd = new OracleCommand("SELECT ID_PERAWATAN FROM PERAWATAN WHERE NAMA_PERAWATAN='"+comboBox6.Text+"'", mainParent.oc);
                String id_rawat = cmd.ExecuteScalar().ToString();
                String harga = label24.Text;
                //DOKTER
                cmd = new OracleCommand("INSERT INTO SHIFT_SPESIALIS VALUES('"+id_dokter+"','"+id_rawat+"',TO_DATE('"+hhAwal+":"+mmAwal+ ":00','HH24:MI:SS'),TO_DATE('" + hhAkhir + ":" + mmAkhir + ":00','HH24:MI:SS'),'"+id_ruang+"','"+id_shift+"',"+harga+",'"+hari+"')", mainParent.oc);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    cmd = new OracleCommand("SELECT ID_PEGAWAI FROM PEGAWAI WHERE NAMA_PEGAWAI='"+listBox2.Items[i].ToString()+"'", mainParent.oc);
                    String id_suster = cmd.ExecuteScalar().ToString();
                    cmd = new OracleCommand("INSERT INTO SHIFT_SPESIALIS VALUES('" + id_suster + "','" + id_rawat + "',TO_DATE('" + hhAwal + ":" + mmAwal + ":00','HH24:MI:SS'),TO_DATE('" + hhAkhir + ":" + mmAkhir + ":00','HH24:MI:SS'),'" + id_ruang + "','" + id_shift + "'," + harga + ",'" + hari + "')", mainParent.oc);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Berhasil mengganti data shift.");
                clearShift();
                showData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            String id_rawat = textBox3.Text;
            String nama = textBox1.Text;
            String desc = textBox2.Text;
            String harga = numericUpDown1.Value.ToString();
            String jenis = comboBox1.Text;

            try
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO PERAWATAN VALUES('"+textBox3.Text+"','"+nama+"','"+desc+"',"+harga+",'"+jenis+"')", mainParent.oc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil menambah perawatan baru.");
                clearPerawatan();
                showData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (button1.Visible)
            {
                if(textBox1.Text.Length > 0)
                {
                    OracleCommand cmd = new OracleCommand("SELECT MAX(ID_PERAWATAN) FROM PERAWATAN", mainParent.oc);
                    String id_rawat = "";
                    if (cmd.ExecuteScalar() != null)
                    {
                        int auto_inc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(2,3));
                        auto_inc++;
                        id_rawat = "XS" + auto_inc.ToString().PadLeft(3, '0');
                    }
                    else
                    {
                        id_rawat = "XS000";
                    }

                    textBox3.Text = id_rawat;
                }
                else
                {
                    textBox3.Text = "";
                }
            }
        }

        private void NumericUpDown2_ValueChanged(object sender, EventArgs e)
        {}

        private void Button4_Click(object sender, EventArgs e)
        {
            String id_ruang = textBox4.Text;
            String kode_nomor = "";
            if (comboBox2.SelectedIndex == 0)
            {
                kode_nomor = "I";
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                kode_nomor = "P";
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                kode_nomor = "O";
            }
            else if (comboBox2.SelectedIndex == 3)
            {
                kode_nomor = "C";
            }
            else if (comboBox2.SelectedIndex == 4)
            {
                kode_nomor = "R";
            }
            else if (comboBox2.SelectedIndex == 5)
            {
                kode_nomor = "G";
            }
            else if (comboBox2.SelectedIndex == 6)
            {
                kode_nomor = "L";
            }
            else if (comboBox2.SelectedIndex == 7)
            {
                kode_nomor = "S";
            }
            else if (comboBox2.SelectedIndex == 8)
            {
                kode_nomor = "A";
            }
            else if (comboBox2.SelectedIndex == 9)
            {
                kode_nomor = "B";
            }
            else if (comboBox2.SelectedIndex == 10)
            {
                kode_nomor = "K";
            }
            String nomor = kode_nomor + numericUpDown2.Value.ToString() + textBox7.Text;
            String nama = textBox5.Text;
            String jenis = comboBox2.Text;
            String harga = numericUpDown3.Value.ToString();
            try
            {
                OracleCommand cmd = new OracleCommand("INSERT INTO RUANG VALUES('"+id_ruang+"','"+nomor+"','"+jenis+"',"+harga+",'OPEN','"+nama+"')", mainParent.oc);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Berhasil menambah ruang baru.");
                clearRuang();
                showData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBox5_TextChanged(object sender, EventArgs e)
        {
            if (button4.Visible)
            {
                if(textBox5.Text.Length > 0)
                {
                    OracleCommand cmd = new OracleCommand("SELECT MAX(ID_RUANG) FROM RUANG", mainParent.oc);
                    String id_ruang = "";
                    if (cmd.ExecuteScalar() != null)
                    {
                        int auto_inc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(1, 4));
                        auto_inc++;
                        id_ruang = "R" + auto_inc.ToString().PadLeft(4, '0');
                    }
                    else
                    {
                        id_ruang = "R0000";
                    }
                    textBox4.Text = id_ruang;
                }
                else
                {
                    textBox4.Text = "";
                }
            }
        }

        private void Button7_EnabledChanged(object sender, EventArgs e)
        {
            
        }

        private void Button7_VisibleChanged(object sender, EventArgs e)
        {
            try
            {
                OracleCommand cmd = new OracleCommand("SELECT ID_PERAWATAN FROM PERAWATAN WHERE NAMA_PERAWATAN='" + comboBox6.Text + "'", mainParent.oc);
                String id_rawat = cmd.ExecuteScalar().ToString();
                cmd = new OracleCommand("SELECT MAX(ID_SHIFT) FROM SHIFT_SPESIALIS WHERE ID_SHIFT LIKE '%" + id_rawat + "%'", mainParent.oc);
                String id_shift = "";
                if (cmd.ExecuteScalar() != null)
                {
                    int auto_inc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(5, 3));
                    auto_inc++;
                    id_shift = id_rawat + auto_inc.ToString().PadLeft(3, '0');
                }
                else
                {
                    id_shift = id_rawat + "000";
                }

                textBox6.Text = id_shift;
            }
            catch (Exception)
            {}
            
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            String id_shift = textBox6.Text;
            try
            {
                String hari = comboBox5.Text;
                String hhAwal = numericUpDown4.Value.ToString();
                String mmAwal = numericUpDown5.Value.ToString();
                String hhAkhir = numericUpDown7.Value.ToString();
                String mmAkhir = numericUpDown6.Value.ToString();
                OracleCommand cmd = new OracleCommand("SELECT ID_RUANG FROM RUANG WHERE NOMOR_RUANG='" + comboBox3.Text + "' AND JENIS_RUANG='PRAKTEK'", mainParent.oc);
                String id_ruang = cmd.ExecuteScalar().ToString();
                cmd = new OracleCommand("SELECT ID_PEGAWAI FROM PEGAWAI WHERE NAMA_PEGAWAI='" + comboBox4.Text + "'", mainParent.oc);
                String id_dokter = cmd.ExecuteScalar().ToString();
                cmd = new OracleCommand("SELECT ID_PERAWATAN FROM PERAWATAN WHERE NAMA_PERAWATAN='" + comboBox6.Text + "'", mainParent.oc);
                String id_rawat = cmd.ExecuteScalar().ToString();
                String harga = label24.Text;
                //DOKTER
                cmd = new OracleCommand("INSERT INTO SHIFT_SPESIALIS VALUES('" + id_dokter + "','" + id_rawat + "',TO_DATE('" + hhAwal + ":" + mmAwal + ":00','HH24:MI:SS'),TO_DATE('" + hhAkhir + ":" + mmAkhir + ":00','HH24:MI:SS'),'" + id_ruang + "','" + id_shift + "'," + harga + ",'" + hari + "')", mainParent.oc);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    cmd = new OracleCommand("SELECT ID_PEGAWAI FROM PEGAWAI WHERE NAMA_PEGAWAI='" + listBox2.Items[i].ToString() + "'", mainParent.oc);
                    String id_suster = cmd.ExecuteScalar().ToString();
                    cmd = new OracleCommand("INSERT INTO SHIFT_SPESIALIS VALUES('" + id_suster + "','" + id_rawat + "',TO_DATE('" + hhAwal + ":" + mmAwal + ":00','HH24:MI:SS'),TO_DATE('" + hhAkhir + ":" + mmAkhir + ":00','HH24:MI:SS'),'" + id_ruang + "','" + id_shift + "'," + harga + ",'" + hari + "')", mainParent.oc);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Berhasil menambah data shift.");
                clearShift();
                showData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
