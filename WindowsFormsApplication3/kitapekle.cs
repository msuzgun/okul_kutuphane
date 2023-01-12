using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
namespace WindowsFormsApplication3
{
    public partial class kitapekle : Form
    {

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        string idsi;
        public void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
            da = new OleDbDataAdapter("Select * from kitaplar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kitaplar");
            dataGridView1.DataSource = ds.Tables["kitaplar"];
            con.Close();
        }
        void griddoldur(string str)
        {
            try
            {
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
                da = new OleDbDataAdapter(str, con);
                ds = new DataSet();
                con.Open();
                da.Fill(ds, "kitaplar");
                dataGridView1.DataSource = ds.Tables["kitaplar"];
                con.Close();
            }
            catch(Exception e) {
                MessageBox.Show("Hata Oluştu"+e.ToString());
            }
        }
        public kitapekle()
        {
            InitializeComponent();
        }

        private void kitapekle_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox16.Text = "";
            if (textBox13.Text != "")
                griddoldur("SELECT * FROM kitaplar WHERE yazar_ad like '%" + textBox3.Text + "%'");
            else
                griddoldur();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            idsi = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox8.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox12.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
            textBox11.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            textBox10.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            textBox9.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            textBox15.Text = dataGridView1.CurrentRow.Cells[13].Value.ToString();
            textBox14.Text = dataGridView1.CurrentRow.Cells[14].Value.ToString();

         
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show(textBox1.Text + " isimli kitabı silmek istediğinizden eminmisiniz. Kitabı silmeniz halinde ilişkilendirildiği geçmiş kayıtlarda silinecektir", "Önemli Uyarı", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from kitaplar where id=" + idsi;
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            idsi = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
           
            textBox8.Text = "";
            textBox7.Text = "";
            textBox6.Text = "";
            textBox5.Text = "";
            textBox12.Text = "";
            textBox11.Text = "";
            textBox10.Text = "1";
            textBox9.Text = "";
            textBox15.Text = "";
            textBox14.Text = "";
            dateTimePicker1.Text="";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (idsi == "")
            {
                if (textBox1.Text == "" || textBox7.Text == "")
                {
                    MessageBox.Show("Eser Adı ve Yazar Adı alanlarını boş bırakmayınız");
                }
                else
                {
                    try
                    {

                        cmd = new OleDbCommand();
                        con.Open();
                        cmd.Connection = con;
                        string donustur = dateTimePicker1.Value.ToString("dd.MM.yyyy");
                        cmd.CommandText = "INSERT INTO kitaplar (eser_ad,boyut,konu,basim_tarih,basim_yer,yazar_ad,cild_cins,sayfa_sayi,dili,cilt_no,miktar,birim_fiyat,kdv,raf_no ) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox4.Text + "','" + donustur + "','" + textBox8.Text + "','" + textBox7.Text + "','" + textBox6.Text + "','" + textBox5.Text + "','" + textBox12.Text + "','" + textBox11.Text + "','" + textBox10.Text + "','" + textBox9.Text + "','" + textBox15.Text + "','" + textBox14.Text + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        griddoldur("SELECT * FROM kitaplar ORDER BY id DESC");
                    }
                    catch (Exception es)
                    {
                        MessageBox.Show(cmd.CommandText + es.ToString());

                    }
                }
            }
            else 
            {
                MessageBox.Show("Aynı id/barkod ile sadece tek kayıt olabilir. Lütfen Yeni kayıt için 'Yeni Kayıt' butonuna basın");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {

                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                string donustur = dateTimePicker1.Value.ToString("dd.MM.yyyy");
                cmd.CommandText = "UPDATE kitaplar SET eser_ad = '" + textBox1.Text + "',boyut = '" + textBox2.Text + "',konu='" + textBox4.Text + "',basim_tarih='" + donustur + "',basim_yer = '" + textBox8.Text + "',yazar_ad='" + textBox7.Text + "',cild_cins = '" + textBox6.Text + "',sayfa_sayi='" + textBox5.Text + "',dili='" + textBox12.Text + "',cilt_no='" + textBox11.Text + "',miktar='" + textBox10.Text + "',birim_fiyat='" + textBox9.Text + "',kdv='" + textBox15.Text + "',raf_no='" + textBox14.Text + "' WHERE id="+idsi;
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
            }
            catch (Exception es)
            {
                MessageBox.Show(cmd.CommandText + es.ToString());

            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            textBox13.Text = "";
            textBox16.Text = "";
            if (textBox3.Text != "")
                griddoldur("SELECT * FROM kitaplar WHERE yazar_ad like '%" + textBox3.Text + "%'");
            else
                griddoldur();
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox13.Text = "";
            if (textBox16.Text != "")
                griddoldur("SELECT * FROM kitaplar WHERE raf_no like '%" + textBox16.Text + "%'");
            else
                griddoldur();
        }
    }
}
