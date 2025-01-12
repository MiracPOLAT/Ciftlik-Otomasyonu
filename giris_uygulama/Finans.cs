using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace giris_uygulama
{
    public partial class Finans : Form
    {
        public Finans()
        {
            InitializeComponent();
            populateHarcama();
            populateGelir();
            FillCalisanId();
            
            
        }
        SqlConnection sconn = new SqlConnection(@"Data Source ="".\SQLEXPRESS""; Initial Catalog = ciftlikdb ; Integrated Security=True");

       
        private void label11_Click(object sender, EventArgs e)
        {
            Inekler Ob = new Inekler();
            Ob.Show();
            this.Hide();
        }

        private void label10_Click(object sender, EventArgs e)
        {
            SutUretimi Ob = new SutUretimi();
            Ob.Show();
            this.Hide();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Saglik1 Ob = new Saglik1();
            Ob.Show();
            this.Hide();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            Yavrulama Ob = new Yavrulama();
            Ob.Show();
            this.Hide();
        }

        private void label14_Click(object sender, EventArgs e)
        {
            SutSatisi Ob = new SutSatisi();
            Ob.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
            GostergePaneli1 Ob = new GostergePaneli1();
            Ob.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void populateHarcama()
        {
            sconn.Open();
            string query = "select * from HarcamaTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sconn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            HarcamaDGW.DataSource = ds.Tables[0];
            sconn.Close();

        }
        private void FillCalisanId()
        {
            sconn.Open();
            SqlCommand cmd = new SqlCommand("select CalisanID from CalisanTbl", sconn);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("CalisanID", typeof(int));
            dt.Load(Rdr);
            CalisanIdCb.ValueMember = "CalisanID";
            CalisanIdCb.DataSource = dt;
            sconn.Close();
        }
        private void populateGelir()
        {
            sconn.Open();
            string query = "select * from GelirTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sconn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            GelirDGW.DataSource = ds.Tables[0];
            sconn.Close();

        }
        private void TemizleHarcama()
        {
            AmacCb.SelectedIndex = -1;
            HarcamaMiktarTb.Text = string.Empty;
            CalisanIdCb.SelectedIndex = -1;

            
        }
        private void TemizleGelir()
        {
            GelirTipiCb.SelectedIndex = -1;
            GelirMiktarTb.Text = string.Empty;
            CalisanIdCb.SelectedIndex = -1;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AmacCb.SelectedIndex == -1 || string.IsNullOrEmpty(HarcamaMiktarTb.Text) || CalisanIdCb.SelectedIndex == -1)
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
            else
            {
                
                    sconn.Open();
                    string query = "INSERT INTO HarcamaTbl (HarcamaAmaci, HarcamaMiktarı, CalisanID, HarcamaTarihi) " +
                                   "VALUES (@HarcamaAmaci, @HarcamaMiktarı,  @CalisanID, @HarcamaTarihi)";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@HarcamaAmaci", AmacCb.SelectedItem);
                    cmd.Parameters.AddWithValue("@HarcamaMiktarı", HarcamaMiktarTb.Text);
                    cmd.Parameters.AddWithValue("@CalisanID", CalisanIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@HarcamaTarihi", HarcamaTarihi.Value.Date);
                    
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Harcama Başarıyla Kaydedildi");
                    sconn.Close();
                    populateHarcama();
                TemizleHarcama();
                    
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (GelirTipiCb.SelectedIndex == -1 || string.IsNullOrEmpty(GelirMiktarTb.Text) || CalisanIdCb.SelectedIndex == -1)
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
            else
            {
                
                    sconn.Open();
                    string query = "INSERT INTO GelirTbl (GelirTipi, GelirMiktari, CalisanID, GelirTarihi) " +
                                   "VALUES (@GelirTipi, @GelirMiktari,  @CalisanID, @GelirTarihi)";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@GelirTipi", GelirTipiCb.SelectedItem);
                    cmd.Parameters.AddWithValue("@GelirMiktari", GelirMiktarTb.Text);
                    cmd.Parameters.AddWithValue("@CalisanID", CalisanIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@GelirTarihi", GelirTarihi.Value.Date);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gelir Başarıyla Kaydedildi");
                    sconn.Close();
                    populateGelir();
                TemizleGelir();

                
            }
        }
       
   

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            populateGelir();
        }

        private void Finans_Load(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            TemizleHarcama();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TemizleGelir();
        }
    }
}
