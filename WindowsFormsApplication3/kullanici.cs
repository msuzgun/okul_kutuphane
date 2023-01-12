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
    public  partial class kullanici : Form
    {
        public static int ilkac = 0;
   

        public void ilkacilis(){
            ilkac = 1;
        }
        public int kontrol() {
            if (ilkac == 1)
                return 1;
            else
                return 0;
        }

        

        public kullanici()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "admin" && textBox2.Text == "admin")
            {
                this.Hide();
                menu m = new menu();
                m.Show();
                ilkacilis();
                
             }
        }

        private void kullanici_Load(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void kullanici_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
