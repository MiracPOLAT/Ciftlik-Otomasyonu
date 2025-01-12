using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace giris_uygulama
{
    public partial class GöstergePaneli : Form
    {
        public GöstergePaneli()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Inekler Ob = new Inekler();
            Ob.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            SütÜretimi  Ob = new SütÜretimi();
            Ob.Show();
            this.Hide();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            Sağlık Ob = new Sağlık();
            Ob.Show();
            this.Hide();
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {
            Yavrulama Ob = new Yavrulama();
            Ob.Show();
            this.Hide();
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            SütSatışı Ob = new SütSatışı();
            Ob.Show();
            this.Hide();
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {
            Finans Ob = new Finans();
            Ob.Show();
            this.Hide();
        }

    }
}
