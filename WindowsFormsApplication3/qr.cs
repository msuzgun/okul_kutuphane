using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MessagingToolkit.QRCode;
using MessagingToolkit.QRCode.Codec;
using System.Data.OleDb;
namespace WindowsFormsApplication3
{
    public partial class qr : Form
    {
        OleDbConnection con;
        OleDbDataAdapter da;
        OleDbCommand cmd;
        DataTable dt;
        string idsi;
        public void griddoldur()
        {
            con = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0;Data Source=KUTUPHANE.accdb");
            con.Open();
            da = new OleDbDataAdapter("Select * from kitaplar ", con);
            dt = new DataTable();
            da.Fill(dt);
            CrystalReport3 cr = new CrystalReport3();
            cr.SetDataSource(dt);
            crystalReportViewer1.ReportSource = cr;
            con.Close();
        }
        void griddoldur(string str)
        {
            try
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
            catch (Exception e)
            {
                MessageBox.Show("Hata Oluştu" + e.ToString());
            }
        }

      
  

     

        private void CrystalReport31_InitReport(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void qr_Load(object sender, EventArgs e)
        {

        }
    }
}
