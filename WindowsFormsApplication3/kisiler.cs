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
    public partial class kisiler : Form
    {
       
        public kisiler()
        {
            InitializeComponent();
        }

        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataSet ds;
        string tc_nos;

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
            catch (Exception e){
                MessageBox.Show(e.ToString());
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void button4_Click(object sender, EventArgs e)
        {

            griddoldur("SELECT tc_no,ad,soyad, okul_no, sinif  FROM kisiler WHERE tc_no LIKE '%"+textBox1.Text + "%' AND ad LIKE '%" + textBox2.Text + "%' AND soyad LIKE '%" + textBox3.Text + "%' AND sinif LIKE '%" + textBox4.Text + "%' AND okul_no LIKE '%" + textBox5.Text + "%'");
       
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
            con.Open();
            cmd = new OleDbCommand("SELECT * FROM kisiler WHERE tc_no='" + textBox1.Text + "'", con);
            OleDbDataReader dr = cmd.ExecuteReader();
            string id_kontrol="";
            while (dr.Read())
            {
                id_kontrol= dr["id"].ToString();
            }

            con.Close();

            if (id_kontrol == "")
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "insert into kisiler (tc_no,ad,soyad, okul_no, sinif) values ('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox5.Text + "','" + textBox4.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            else { MessageBox.Show("Aynı TC No ile iki kayıt yapılamaz lütfen farklı bir kayıt ekleyin"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cmd = new OleDbCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "update kisiler set tc_no='" + textBox1.Text + "',ad='" + textBox2.Text + "',soyad='" + textBox3.Text + "' ,okul_no='" + textBox5.Text + "' ,sinif='" + textBox4.Text + "' where tc_no='" + tc_nos + "'";
            cmd.ExecuteNonQuery();
            con.Close();
            griddoldur();

        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            tc_nos  = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result1 = MessageBox.Show(textBox1.Text+" TC Nolu kaydı silmek istediğinizden eminmisiniz","Önemli Uyarı",MessageBoxButtons.YesNo);
            if (result1 == DialogResult.Yes)
            {
                cmd = new OleDbCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "delete from kisiler where tc_no='" + tc_nos + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                griddoldur();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       
    }
}
