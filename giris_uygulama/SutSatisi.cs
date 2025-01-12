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
    public partial class SutSatisi : Form
    {
        public SutSatisi()
        {
            InitializeComponent();
            populate();
            FillCalisanId();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void label15_Click(object sender, EventArgs e)
        {
            Finans Ob = new Finans();
            Ob.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {

            GostergePaneli1 Ob = new GostergePaneli1();
            Ob.Show();
            this.Hide();
        }

       

        private void MiktarTb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int fiyat = string.IsNullOrEmpty(FiyatTb.Text) ? 0 : Convert.ToInt32(FiyatTb.Text);
                int miktar = string.IsNullOrEmpty(MiktarTb.Text) ? 0 : Convert.ToInt32(MiktarTb.Text);


                int total = fiyat * miktar;
                ToplamTb.Text = total.ToString();
            }
            catch (FormatException ex)
            {
                MessageBox.Show("Geçersiz giriş: Lütfen sadece sayısal değerler girin.");
              
            }
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
        private void populate()
        {
            sconn.Open();
            string query = "select * from SutSatisTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sconn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            SutSatisDGW.DataSource = ds.Tables[0];
            sconn.Close();

        }
        private void Temizle()
        {
            CalisanIdCb.Text = string.Empty;
            MusteriIsmiTb.Text = string.Empty;
            MusteriNoTb.Text = string.Empty;
            SatisDate.Text = string.Empty;
            FiyatTb.Text = string.Empty;
            MiktarTb.Text = string.Empty;
            ToplamTb.Text = string.Empty;
            

        }
        private void SaveTransaction()
        {
            
            
            
            
                    sconn.Open();
                    string query = "INSERT INTO GelirTbl (GelirTipi, GelirMiktari, CalisanID, GelirTarihi) " +
                                   "VALUES (@GelirTipi, @GelirMiktari,  @CalisanID, @GelirTarihi)";
                String Sales = "Süt Satışı";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@GelirTipi", Sales);
                    cmd.Parameters.AddWithValue("@GelirMiktari", ToplamTb.Text);
                    cmd.Parameters.AddWithValue("@CalisanID", CalisanIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@GelirTarihi", SatisDate.Value.Date);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Gelir Başarıyla Kaydedildi");
                    sconn.Close();
                    

                
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (CalisanIdCb.SelectedIndex == -1 || string.IsNullOrEmpty(MusteriIsmiTb.Text) || string.IsNullOrEmpty(MusteriNoTb.Text) || string.IsNullOrEmpty(SatisDate.Text) || string.IsNullOrEmpty(FiyatTb.Text) || string.IsNullOrEmpty(MiktarTb.Text) || string.IsNullOrEmpty(ToplamTb.Text) )
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
            else
            {
               
                    sconn.Open();
                    string query = "INSERT INTO SutSatisTbl (CalisanID, MusteriIsmi, MusteriTelNo, SatisTarihi, SatisFiyati, SatisMiktari,SatisToplami) " +
                                   "VALUES (@CalisanID, @MusteriIsmi,  @MusteriTelNo, @SatisTarihi, @SatisFiyati, @SatisMiktari, @SatisToplami)";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@CalisanID",CalisanIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@MusteriIsmi", MusteriIsmiTb.Text);
                    cmd.Parameters.AddWithValue("@MusteriTelNo",MusteriNoTb.Text);
                    cmd.Parameters.AddWithValue("@SatisTarihi", SatisDate.Value.Date);
                    cmd.Parameters.AddWithValue("@SatisFiyati", FiyatTb.Text);
                    cmd.Parameters.AddWithValue("@SatisMiktari", MiktarTb.Text);
                    cmd.Parameters.AddWithValue("@SatisToplami", ToplamTb.Text);
                    

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Satış Başarıyla Kaydedildi");
                    sconn.Close();
                    populate();
                    SaveTransaction();
                    Temizle();
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }
        int key = 0;
        private void SutSatisDGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                CalisanIdCb.Text = SutSatisDGW.Rows[e.RowIndex].Cells[1].Value.ToString();
                MusteriIsmiTb.Text = SutSatisDGW.Rows[e.RowIndex].Cells[2].Value.ToString();
                MusteriNoTb.Text = SutSatisDGW.Rows[e.RowIndex].Cells[3].Value.ToString();
                SatisDate.Text = SutSatisDGW.Rows[e.RowIndex].Cells[4].Value.ToString();
                ToplamTb.Text = SutSatisDGW.Rows[e.RowIndex].Cells[5].Value.ToString();
                FiyatTb.Text = SutSatisDGW.Rows[e.RowIndex].Cells[6].Value.ToString();
                MiktarTb.Text = SutSatisDGW.Rows[e.RowIndex].Cells[7].Value.ToString();
                ;

                if (CalisanIdCb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    if (SutSatisDGW.SelectedRows.Count > 0 && SutSatisDGW.SelectedRows[0].Cells[0].Value != null)
                    {
                        string value = SutSatisDGW.SelectedRows[0].Cells[0].Value.ToString();
                        if (int.TryParse(value, out int key))
                        {
                           
                        }
                        else
                        {
                          
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SutSatisDGW.SelectedRows.Count > 0)
            {
            
                int selectedRowIndex = SutSatisDGW.SelectedRows[0].Index;
                int SutSatisID = Convert.ToInt32(SutSatisDGW.Rows[selectedRowIndex].Cells["SutSatisID"].Value);

                    sconn.Open();
                    string query = "DELETE FROM SutSatisTbl WHERE SutSatisID=@SutSatisID";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("SutSatisID", SutSatisID);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Kayıt Başarıyla Silindi");
                    sconn.Close();
                    populate();
                    Temizle();
              
            }
            else
            {
                MessageBox.Show("Silmek için bir satır seçiniz.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CalisanIdCb.SelectedIndex == -1 || string.IsNullOrEmpty(MusteriIsmiTb.Text) || string.IsNullOrEmpty(MusteriNoTb.Text) || string.IsNullOrEmpty(SatisDate.Text) || string.IsNullOrEmpty(FiyatTb.Text) || string.IsNullOrEmpty(MiktarTb.Text) || string.IsNullOrEmpty(ToplamTb.Text))
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
           
            else if (SutSatisDGW.SelectedRows.Count > 0)
            {
                int selectedRowIndex = SutSatisDGW.SelectedRows[0].Index;
                int SutSatisID = Convert.ToInt32(SutSatisDGW.Rows[selectedRowIndex].Cells["SutSatisID"].Value);

                    sconn.Open();
                    string query = "UPDATE SutSatisTbl SET CalisanID=@CalisanID, MusteriIsmi=@MusteriIsmi, MusteriTelNo=@MusteriTelNo, SatisTarihi=@SatisTarihi,SatisFiyati=@SatisFiyati, SatisMiktari=@SatisMiktari, SatisToplami=@SatisToplami WHERE SutSatisID=@SutSatisID";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@CalisanID", CalisanIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@MusteriIsmi", MusteriIsmiTb.Text);
                    cmd.Parameters.AddWithValue("@MusteriTelNo", MusteriNoTb.Text);
                    cmd.Parameters.AddWithValue("@SatisTarihi", SatisDate.Value.Date);
                    cmd.Parameters.AddWithValue("@SatisFiyati", FiyatTb.Text);
                    cmd.Parameters.AddWithValue("@SatisMiktari", MiktarTb.Text);
                    cmd.Parameters.AddWithValue("@SatisToplami", ToplamTb.Text);
                    cmd.Parameters.AddWithValue("@SutSatisID", SutSatisID);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarıyla Güncellendi");
                    sconn.Close();
                    populate();
                    Temizle();
               
            }

        }
    }
}
