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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace giris_uygulama
{
    public partial class GostergePaneli1 : Form
    {
        public GostergePaneli1()
        {
            InitializeComponent();
            Finans();
            Lojistik();
            GetMax();
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

        private void label15_Click(object sender, EventArgs e)
        {
            Finans Ob = new Finans();
            Ob.Show();
            this.Hide();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void Finans()
        {
            sconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select sum(GelirMiktari) from GelirTbl", sconn);
            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(HarcamaMiktarı) from HarcamaTbl", sconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            int glr, hrcm;
            double blnc;
            glr = Convert.ToInt32(dt.Rows[0][0].ToString());
            GelirLbl.Text = dt.Rows[0][0].ToString();

            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            hrcm = Convert.ToInt32(dt1.Rows[0][0].ToString());
            blnc = glr - hrcm;
            HarcamaLbl.Text = dt1.Rows[0][0].ToString();
            BilancoLbl1.Text = "" + blnc;
            sconn.Close();
        }
        private void Lojistik()
        {
            sconn.Open();
            SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from InekTbl", sconn);
            SqlDataAdapter sda1 = new SqlDataAdapter("select sum(ToplamSut) from SutUretimTbl", sconn);
            SqlDataAdapter sda2 = new SqlDataAdapter("select Count(*) from CalisanTbl", sconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            InekNumLbl.Text = dt.Rows[0][0].ToString();

            DataTable dt1 = new DataTable();
            sda1.Fill(dt1);
            string sutLitreLabel = dt1.Rows[0][0].ToString() + "Litre";
            SutLtLbl.Text = sutLitreLabel;

            // Süt litrelerini labelden sayıya dönüştürme
            int sutLitreSayisi;
            bool parseSuccess = int.TryParse(dt1.Rows[0][0].ToString(), out sutLitreSayisi);

            if (parseSuccess)
            {
                int maxLitre = 10000;

                progressBar.Minimum = 0;
                progressBar.Maximum = maxLitre;

                progressBar.Value = sutLitreSayisi;
                SutLtLbl.Text = $"{sutLitreSayisi}Litre/10.000Litre";

                progressBar.Update();
            }

            DataTable dt2 = new DataTable();
            sda2.Fill(dt2);
            CalisanIsmLbl.Text = dt2.Rows[0][0].ToString();
            sconn.Close();
            //progresbar devre dışı bırakılmak istendiğinde bu kod kullanılacak
            //sconn.Open();
            //SqlDataAdapter sda = new SqlDataAdapter("select Count(*) from InekTbl", sconn);
            //SqlDataAdapter sda1 = new SqlDataAdapter("select sum(ToplamSut) from SutUretimTbl", sconn);
            //SqlDataAdapter sda2 = new SqlDataAdapter("select Count(*) from CalisanTbl", sconn);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //InekNumLbl.Text = dt.Rows[0][0].ToString();
            //DataTable dt1 = new DataTable();
            //sda1.Fill(dt1);
            //SutLtLbl.Text = dt1.Rows[0][0].ToString() + "Litre";
            //DataTable dt2 = new DataTable();
            //sda2.Fill(dt2);
            //CalisanIsmLbl.Text = dt2.Rows[0][0].ToString();
            //sconn.Close();
        }
        private void GetMax()
        {
            sconn.Open();

            // En yüksek gelir miktarını bulan sorgu
            SqlDataAdapter sda = new SqlDataAdapter("SELECT MAX(GelirMiktari) FROM GelirTbl", sconn);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                string maxGelir = dt.Rows[0][0].ToString(); // En yüksek gelir miktarını al

                // En yüksek gelir miktarına karşılık gelen tarihi bulan sorgu
                SqlDataAdapter sdaDate = new SqlDataAdapter("SELECT GelirTarihi FROM GelirTbl WHERE GelirMiktari = " + maxGelir, sconn);
                DataTable dtDate = new DataTable();
                sdaDate.Fill(dtDate);

                if (dtDate.Rows.Count > 0)
                {
                    string maxGelirTarihi = dtDate.Rows[0][0].ToString(); // En yüksek gelir miktarının tarihini al
                    SatisLbl.Text = maxGelir; // En yüksek gelir miktarını yazdır
                    YüksekSatisTarihLbl.Text = maxGelirTarihi; // En yüksek gelir tarihini yazdır
                }
            }
                SqlDataAdapter sda1 = new SqlDataAdapter("SELECT MAX(HarcamaMiktarı) FROM HarcamaTbl", sconn);
                DataTable dt1 = new DataTable();
                sda1.Fill(dt1);

                if (dt1.Rows.Count > 0 && dt1.Rows[0][0] != DBNull.Value)
                {
                    string maxHarcama = dt1.Rows[0][0].ToString(); // En yüksek gelir miktarını al

                    // En yüksek gelir miktarına karşılık gelen tarihi bulan sorgu
                    SqlDataAdapter sdaDate1 = new SqlDataAdapter("SELECT HarcamaTarihi FROM HarcamaTbl WHERE HarcamaMiktarı = " + maxHarcama, sconn);
                    DataTable dtDate1 = new DataTable();
                    sdaDate1.Fill(dtDate1);

                    if (dtDate1.Rows.Count > 0)
                    {
                        string maxHarcamaTarihi = dtDate1.Rows[0][0].ToString(); // En yüksek gelir miktarının tarihini al
                        YüksekHrcmLbl.Text = maxHarcama; // En yüksek gelir miktarını yazdır
                        YüksekHrcmTarih.Text = maxHarcamaTarihi; // En yüksek gelir tarihini yazdır
                    }
                }

                sconn.Close();

            
        }

      

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
           this.Close();
        }

        private void GostergePaneli1_Load(object sender, EventArgs e)
        {

        }
    }
    } 
