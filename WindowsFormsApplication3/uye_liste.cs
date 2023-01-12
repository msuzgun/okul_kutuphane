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
    public partial class uye_liste : Form
    {
        public uye_liste()
        {
            InitializeComponent();
        }
       
     
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
       // string tc_nos;

        void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
            da = new OleDbDataAdapter("Select tc_no,ad,soyad, okul_no, sinif from kisiler", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "kisiler");
            dataGridView1.DataSource = ds.Tables["kisiler"];
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
                da.Fill(ds, "kisiler");
                dataGridView1.DataSource = ds.Tables["kisiler"];
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void ara_Click(object sender, EventArgs e)
        {
            griddoldur("SELECT id,tc_no,ad,soyad FROM kisiler WHERE tc_no LIKE '%" + textBox1.Text + "%' AND ad LIKE '%" + textBox2.Text + "%' AND soyad LIKE '%" + textBox3.Text +"%'");
    
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                geneldegisken ktp = new geneldegisken();
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into emanet (kisi_id,kitap_id,al_tarih) values ('" + kisi_id + "','" + ktp.kitap_id_ver() + "','" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "UPDATE kitaplar SET emanet_durum=1 WHERE id=" + ktp.kitap_id_ver();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Yapılmıştır");
                this.Close();
                ktp.kayit_yapildi();// genel depşkene kayıt yapıldığına dair bilgi ekler
            }
            catch(Exception es) 
            {
                MessageBox.Show("Kayıt yapılamadı :" + es.ToString());
            }
        }

        string kisi_id;
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            kisi_id = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            
        }

       
        
  
    }
}
