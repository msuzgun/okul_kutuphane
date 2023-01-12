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
    public partial class kitaplar : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        string kisi_id;
        string kisi_ad_soyad;
        //string kitap_id;
        string kitap_adi;
        string emanet_id;
        bool emanet;
        string idsi;
        public void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
            da = new OleDbDataAdapter("Select id,eser_ad,raf_no,basim_tarih,basim_yer,yazar_ad,emanet_durum from kitaplar", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kitaplar");
            dataGridView1.DataSource = ds.Tables["kitaplar"];
            con.Close();
        }
        void griddoldur(string str)
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
            da = new OleDbDataAdapter(str, con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kitaplar");
            dataGridView1.DataSource = ds.Tables["kitaplar"];
            con.Close();
        }

        public kitaplar()
        {
            InitializeComponent();
        }

        private void kitaplar_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
                griddoldur("SELECT id,eser_ad,raf_no,basim_tarih,basim_yer,yazar_ad,emanet_durum FROM kitaplar WHERE id =" + textBox1.Text);
            else
                griddoldur();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != "")
                griddoldur("SELECT id,eser_ad,raf_no,basim_tarih,basim_yer,yazar_ad,emanet_durum FROM kitaplar WHERE eser_ad LIKE '%" + textBox2.Text + "%'");
            else
                griddoldur();
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            //MessageBox.Show(DateTime.Now.ToLongTimeString());
            kitap_adi= dataGridView1.CurrentRow.Cells[1].Value.ToString();
            idsi = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            emanet =Convert.ToBoolean(dataGridView1.CurrentRow.Cells[6].Value.ToString());
            label9.Text = ":";
            label10.Text = ":";
            label11.Text = ":";
            if (emanet)
            {
                button1.Enabled = false;
                button2.Enabled = true;
                con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
                con.Open();
                cmd = new OleDbCommand("SELECT u.id,u.ad,u.soyad,u.okul_no,e.id,e.al_tarih FROM emanet e,kisiler u WHERE e.kisi_id=u.id AND  e.kitap_id=" + idsi + " ORDER BY al_tarih", con);
                OleDbDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    label9.Text = ": " + "Emanet Verildi";
                    label10.Text = ": " + dr["ad"].ToString() + " " + dr["soyad"].ToString();
                    label11.Text = ": " + dr["al_tarih"].ToString();
                    kisi_id = dr[0].ToString();
                    kisi_ad_soyad = dr["ad"].ToString() + " " + dr["soyad"].ToString();
                    emanet_id = dr[4].ToString();
                    //Veya
                    //textBox1.Text = dr.GetString(0);
                    //textBox2.Text = dr.GetString(1);
                    //textBox3.Text = dr.GetString(2);

                }
               
                con.Close();
 
            }
           

            
           
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show(kisi_ad_soyad + " adlı kişiden " + kitap_adi+ " kitabını teslim aldınız mı?", "Kitap Teslim Alma", MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                try
                {
                    cmd = new OleDbCommand();
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE kitaplar SET emanet_durum=0  WHERE id=" + idsi;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    griddoldur();

                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = "UPDATE emanet SET ver_tarih ='" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "'  WHERE id=" + emanet_id;
                    cmd.ExecuteNonQuery();
                    con.Close();
                    griddoldur();
                }
                catch {
                    MessageBox.Show("Bir sorun oluştu");
                }
            }
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            uye_liste uyeler = new uye_liste();
            uyeler.Show();

            geneldegisken ktp = new geneldegisken();
            ktp.kitap_id_al(idsi);
        }

        private void kitaplar_Activated(object sender, EventArgs e)
        {
            //griddoldur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

       
       

     

        private void kitaplar_MouseClick(object sender, MouseEventArgs e)
        {
            geneldegisken ktp = new geneldegisken();


            if (ktp.kayit_yapildimi())
            {
                MessageBox.Show(ktp.kayit_yapildimi().ToString());
                griddoldur();
                ktp.kayit_yenilendi();
            }
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            geneldegisken ktp = new geneldegisken();


            if (ktp.kayit_yapildimi())
            {
                MessageBox.Show(ktp.kayit_yapildimi().ToString());
                griddoldur();
                ktp.kayit_yenilendi();
            }

        }

      

      
    }
}
