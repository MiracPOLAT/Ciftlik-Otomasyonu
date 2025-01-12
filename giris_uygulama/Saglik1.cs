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
    public partial class Saglik1 : Form
    {
        public Saglik1()
        {
            InitializeComponent();
            FillInekId();
            populate();
        }
        SqlConnection sconn = new SqlConnection(@"Data Source ="".\SQLEXPRESS""; Initial Catalog = ciftlikdb ; Integrated Security=True");
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
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

        private void label16_Click(object sender, EventArgs e)
        {
            GostergePaneli1 Ob = new GostergePaneli1();
            Ob.Show();
            this.Hide();
        }
        private void Temizle()
        {
            InekIdCb.Text = string.Empty;
            InekIsmiTb.Text = string.Empty;
            OlayTb.Text = string.Empty;
            TeshisTb.Text = string.Empty;
            TedaviTb.Text = string.Empty;
            TedaviUcretiTb.Text = string.Empty;
            VeterinerTb.Text = string.Empty;

        }
        private void FillInekId()
        {
            sconn.Open();
            SqlCommand cmd = new SqlCommand("select InekID from InekTbl", sconn);
            SqlDataReader Rdr;
            Rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("InekID", typeof(int));
            dt.Load(Rdr);
            InekIdCb.ValueMember = "InekID";
            InekIdCb.DataSource = dt;
            sconn.Close();
        }
        private void populate()
        {
            sconn.Open();
            string query = "select * from SaglikTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sconn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            SaglikDGW.DataSource = ds.Tables[0];
            sconn.Close();

        }
        private void GetInekIsmi()
        {
            sconn.Open();
            string query = "select*from InekTbl where InekID =" + InekIdCb.SelectedValue.ToString() + " ";
            SqlCommand cmd = new SqlCommand(query, sconn);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                InekIsmiTb.Text = dr["InekIsmi"].ToString();
            }

            sconn.Close();
        }

        private void InekIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetInekIsmi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (InekIdCb.SelectedIndex == -1 || string.IsNullOrEmpty(InekIsmiTb.Text) || string.IsNullOrEmpty(OlayTb.Text) || string.IsNullOrEmpty(TeshisTb.Text) || string.IsNullOrEmpty(TedaviTb.Text) || string.IsNullOrEmpty(TedaviUcretiTb.Text) || string.IsNullOrEmpty(VeterinerTb.Text))
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
            else
            {
                    sconn.Open();
                    string query = "INSERT INTO SaglikTbl (InekID, InekIsmi, Olay, Teshis, Tedavi, TedaviMasrafi, VeterinerIsmi,RaporTarihi) " +
                                   "VALUES (@InekID, @InekIsmi, @Olay, @Teshis, @Tedavi, @TedaviMasrafi, @VeterinerIsmi, @RaporTarihi)";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@InekID", InekIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@InekIsmi", InekIsmiTb.Text);
                    cmd.Parameters.AddWithValue("@Olay", OlayTb.Text);
                    cmd.Parameters.AddWithValue("@Teshis", TeshisTb.Text);
                    cmd.Parameters.AddWithValue("@Tedavi", TedaviTb.Text);
                    cmd.Parameters.AddWithValue("@TedaviMasrafi", TedaviUcretiTb.Text);
                    cmd.Parameters.AddWithValue("@VeterinerIsmi", VeterinerTb.Text);
                    cmd.Parameters.AddWithValue("@RaporTarihi", RaporTarihi.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rapor Başarıyla Kaydedildi");
                    sconn.Close();
                    populate();
                    Temizle();
                
            }

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (InekIdCb.SelectedIndex == -1 || string.IsNullOrEmpty(InekIsmiTb.Text) || string.IsNullOrEmpty(OlayTb.Text) || string.IsNullOrEmpty(TeshisTb.Text) || string.IsNullOrEmpty(TedaviTb.Text) || string.IsNullOrEmpty(TedaviUcretiTb.Text) || string.IsNullOrEmpty(VeterinerTb.Text) || string.IsNullOrEmpty(RaporTarihi.Text))
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
            else if(SaglikDGW.SelectedRows.Count > 0)
            {
                int selectedRowIndex = SaglikDGW.SelectedRows[0].Index;
                int RaporId = Convert.ToInt32(SaglikDGW.Rows[selectedRowIndex].Cells["RaporID"].Value);
                
                
                    sconn.Open();
                    string query = "UPDATE SaglikTbl SET InekID=@InekID, InekIsmi=@InekIsmi, Olay=@Olay, Teshis=@Teshis, Tedavi=@Tedavi, TedaviMasrafi=@TedaviMasrafi, VeterinerIsmi=@VeterinerIsmi, RaporTarihi=@RaporTarihi WHERE RaporID=@RaporID";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@InekID", InekIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@InekIsmi", InekIsmiTb.Text);
                    cmd.Parameters.AddWithValue("@Olay", OlayTb.Text);
                    cmd.Parameters.AddWithValue("@Teshis", TeshisTb.Text);
                    cmd.Parameters.AddWithValue("@Tedavi", TedaviTb.Text);
                    cmd.Parameters.AddWithValue("@TedaviMasrafi", TedaviUcretiTb.Text);
                    cmd.Parameters.AddWithValue("@VeterinerIsmi", VeterinerTb.Text);
                    cmd.Parameters.AddWithValue("@RaporTarihi", RaporTarihi.Value.Date);
                    cmd.Parameters.AddWithValue("@RaporID", RaporId);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Rapor Başarıyla Güncellendi");
                    sconn.Close();
                    populate();
                    Temizle();
                
            }

        }
        int key = 0;

        private void SaglikDGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                InekIdCb.Text = SaglikDGW.Rows[e.RowIndex].Cells[1].Value.ToString();
                InekIsmiTb.Text = SaglikDGW.Rows[e.RowIndex].Cells[2].Value.ToString();
                RaporTarihi.Text = SaglikDGW.Rows[e.RowIndex].Cells[3].Value.ToString();
                OlayTb.Text = SaglikDGW.Rows[e.RowIndex].Cells[4].Value.ToString();
                TeshisTb.Text = SaglikDGW.Rows[e.RowIndex].Cells[5].Value.ToString();
                TedaviTb.Text = SaglikDGW.Rows[e.RowIndex].Cells[6].Value.ToString();
                TedaviUcretiTb.Text = SaglikDGW.Rows[e.RowIndex].Cells[7].Value.ToString();
                VeterinerTb.Text = SaglikDGW.Rows[e.RowIndex].Cells[8].Value.ToString();

                if (InekIsmiTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    if (SaglikDGW.SelectedRows.Count > 0 && SaglikDGW.SelectedRows[0].Cells[0].Value != null)
                    {
                        string value = SaglikDGW.SelectedRows[0].Cells[0].Value.ToString();
                        if (int.TryParse(value, out int key))
                        {
                            // key değeri geçerli, işlemleri devam ettir
                            // ...
                        }
                        else
                        {
                            // value, geçerli bir tam sayıya dönüştürülemedi, hata işleme yapılabilir
                            // MessageBox.Show("Geçersiz değer: " + value);
                        }
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (SaglikDGW.SelectedRows.Count > 0)
            {
                // Seçilen satırın SaglikID değerini al
                int selectedRowIndex = SaglikDGW.SelectedRows[0].Index;
                int RaporID = Convert.ToInt32(SaglikDGW.Rows[selectedRowIndex].Cells["RaporID"].Value);

             
                
                    sconn.Open();
                    string query = "DELETE FROM SaglikTbl WHERE RaporID=@RaporID";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@RaporID", RaporID);

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Rapor Başarıyla Silindi");
                    sconn.Close();
                    populate();
                    Temizle();
              
            }
            else
            {
                MessageBox.Show("Silmek için bir satır seçiniz.");
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
