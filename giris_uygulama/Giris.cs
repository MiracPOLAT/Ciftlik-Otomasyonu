using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing.Imaging;


namespace giris_uygulama
{

    public partial class Giris : Form
    {


        SqlDataReader dr;
        SqlCommand cmd;

        public Giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RoleCb.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir rol seçin.");
            }
            else if (string.IsNullOrEmpty(SifreTb.Text) || string.IsNullOrEmpty(KullaniciAdiTb.Text))
            {
                MessageBox.Show("Kullanıcı adı ve şifre alanlarını doldurun.");
            }
            else
            {
                string role = RoleCb.SelectedItem.ToString();
                string username = KullaniciAdiTb.Text;
                string password = SifreTb.Text;

                SqlConnection sconn = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=ciftlikdb; Integrated Security=True");


                    sconn.Open();
                string query = "";
                SqlCommand cmd;

                if (role == "Yönetici")
                {
                    query = "SELECT COUNT(*) FROM giris WHERE kullanici_adi = @username AND sifre = @password";
                    cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = (int)cmd.ExecuteScalar();
                    if (count > 0)
                    {
                        Calisan yoneticiForm = new Calisan();
                        yoneticiForm.Show();
                    }
                }
                else
                {
                    query = "SELECT COUNT(*) FROM CalisanTbl WHERE CalisanIsmi = @username AND CalisanSifre = @password";
                    cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Inekler calisanForm = new Inekler();
                        calisanForm.Show();
                    }
                    else
                    {
                        MessageBox.Show("Giriş başarısız. Lütfen bilgilerinizi kontrol edin.");
                    }

                    sconn.Close();
                }
            }
        }
            
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Giris_Load(object sender, EventArgs e)
        {

        }
    }
}

        

        

        

    

