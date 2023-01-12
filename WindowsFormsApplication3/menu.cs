using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication3
{

    public partial class menu : Form
    {
        
        public menu()
        {
            InitializeComponent();
        }
        
     
        private void menu_Load(object sender, EventArgs e)
        {
          
           
        }

        private void menu_Shown(object sender, EventArgs e)
        {
            kullanici kull = new kullanici();
            if (kull.kontrol() == 0)
            {

                kull.Show();
                kull.Activate();
                this.Hide();
      
            }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kitaplar kitaps = new kitaplar();
            kitaps.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            kisiler kisi = new kisiler();
            kisi.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            kitapekle kekle = new kitapekle();
            kekle.Show();
        }

        private void menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            barkod qrkod = new barkod();
            qrkod.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.muratsuzgun.info");
        }
    }
}
