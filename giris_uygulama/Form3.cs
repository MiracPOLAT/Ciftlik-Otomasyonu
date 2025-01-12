using giris_uygulama;
using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Schema;

namespace giris_uygulama
{
    public partial class Form3: Form
    {

        public Form3()
        {
            InitializeComponent();
        }
        bool islem = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
        }

       
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (islem == false)
            {
                progressBar1.Value += 10;
            }
            if (progressBar1.Value == 100)
            {
                islem = true;
            }
            if (islem == true)
            {
                Giris fr = new Giris();
                fr.Show();
                this.Hide();
                timer1.Enabled = false;

            }
        }
        private void Form3_Load(object sender, EventArgs e)
        {


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
