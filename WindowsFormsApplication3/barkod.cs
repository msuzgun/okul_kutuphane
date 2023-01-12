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
    public partial class barkod : Form
    {
        public barkod()
        {
            InitializeComponent();
        }
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataTable dt;
        string idsi;
        public void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
            con.Open();
            da = new OleDbDataAdapter("Select * from kitaplar ORDER BY raf_no", con);
            dt = new DataTable();
            da.Fill(dt);
            CrystalReport3 cr = new CrystalReport3();
            cr.SetDataSource(dt);
            crystalReportViewer1.ReportSource = cr;

            con.Close();
        }

        public void griddoldur(string str)
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
            con.Open();
            da = new OleDbDataAdapter(str, con);
            dt = new DataTable();
            da.Fill(dt);
            CrystalReport3 cr = new CrystalReport3();
            cr.SetDataSource(dt);
            crystalReportViewer1.ReportSource = cr;

            con.Close();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string sorgu = "SELECT * FROM kitaplar WHERE eser_ad like '%" + textBox2.Text + "%' AND id like '%" + textBox1.Text + "%'  AND yazar_ad like '%" + textBox3.Text + "%'  AND raf_no like '%" + textBox4.Text + "%'";
            int miktar;
            try {
                miktar = int.Parse(textBox5.Text);
            }
            catch
            {
                miktar = 1; 
            }
            
            if ( miktar> 1)
            {
                for (int i = 1; i < miktar; i++)
                {
                    sorgu += "UNION ALL SELECT * FROM kitaplar WHERE eser_ad like '%" + textBox2.Text + "%' AND id like '%" + textBox1.Text + "%'  AND yazar_ad like '%" + textBox3.Text + "%'  AND raf_no like '%" + textBox4.Text + "%'";
                }
                griddoldur(sorgu);
            }
            else {
                griddoldur("SELECT * FROM kitaplar WHERE eser_ad like '%" + textBox2.Text + "%' AND id like '%" + textBox1.Text + "%'  AND yazar_ad like '%" + textBox3.Text + "%'  AND raf_no like '%" + textBox4.Text + "%'");
            }
           
        }

        private void barkod_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            
        }
    }
}
