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
    public partial class SutUretimi : Form
    {
        public SutUretimi()
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

        private void label16_Click(object sender, EventArgs e)
        {
            GostergePaneli1 Ob = new GostergePaneli1();
            Ob.Show();
            this.Hide();
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
            string query = "select * from SutUretimTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sconn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            SutUretimDGW.DataSource = ds.Tables[0];
            sconn.Close();

        }
        private void Temizle()
        {
            InekismiTb.Text = string.Empty;
            sabahTb.Text = string.Empty;
            ogleTb.Text = string.Empty;
            aksamTb.Text = string.Empty;
            toplamTb.Text = string.Empty;

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
                InekismiTb.Text = dr["InekIsmi"].ToString();
            }

            sconn.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (InekIdCb.SelectedIndex == -1 || string.IsNullOrEmpty(InekismiTb.Text) || string.IsNullOrEmpty(sabahTb.Text) || string.IsNullOrEmpty(ogleTb.Text) || string.IsNullOrEmpty(aksamTb.Text) || string.IsNullOrEmpty(toplamTb.Text))
            {
                MessageBox.Show("Eksik bilgi girdiniz");

            }
            else
            {
                sconn.Open();
                String Query = "insert into SutUretimTbl values(" + InekIdCb.SelectedValue.ToString() + ",'" + InekismiTb.Text + "','" + sabahTb.Text + "','" + ogleTb.Text + "','" + aksamTb.Text + "','" + toplamTb.Text + "','" + date.Value.Date + "')";
                SqlCommand cmd = new SqlCommand(Query, sconn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Süt Başarıyla Kaydedildi");
                sconn.Close();
                populate();
                Temizle();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void InekIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetInekIsmi();
        }

       

        private void aksamTb_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int sabah = string.IsNullOrEmpty(sabahTb.Text) ? 0 : Convert.ToInt32(sabahTb.Text);
                int ogle = string.IsNullOrEmpty(ogleTb.Text) ? 0 : Convert.ToInt32(ogleTb.Text);
                int aksam = string.IsNullOrEmpty(aksamTb.Text) ? 0 : Convert.ToInt32(aksamTb.Text);

                int total = sabah + ogle + aksam;
                toplamTb.Text = total.ToString();
            }
            catch (FormatException ex)
            {
                
                MessageBox.Show("Geçersiz giriş: Lütfen sadece sayısal değerler girin.");
             
            }
        }
        int key = 0;
        private void SutUretimDGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                InekIdCb.Text = SutUretimDGW.Rows[e.RowIndex].Cells[1].Value.ToString();
                InekismiTb.Text = SutUretimDGW.Rows[e.RowIndex].Cells[2].Value.ToString();
                sabahTb.Text = SutUretimDGW.Rows[e.RowIndex].Cells[3].Value.ToString();
                ogleTb.Text = SutUretimDGW.Rows[e.RowIndex].Cells[4].Value.ToString();
                aksamTb.Text = SutUretimDGW.Rows[e.RowIndex].Cells[5].Value.ToString();
                toplamTb.Text = SutUretimDGW.Rows[e.RowIndex].Cells[6].Value.ToString();
                date.Text = SutUretimDGW.Rows[e.RowIndex].Cells[7].Value.ToString();

                if (InekismiTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    if (SutUretimDGW.SelectedRows.Count > 0 && SutUretimDGW.SelectedRows[0].Cells[0].Value != null)
                    {
                        string value = SutUretimDGW.SelectedRows[0].Cells[0].Value.ToString();
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


            if (SutUretimDGW.SelectedRows.Count > 0)
            {
                int selectedRowIndex = SutUretimDGW.SelectedRows[0].Index;
                int inekID = Convert.ToInt32(SutUretimDGW.Rows[selectedRowIndex].Cells[0].Value);

                sconn.Open();
                string query = "DELETE FROM SutUretimTbl WHERE SutUretimID = @SutUretimID";
                SqlCommand cmd = new SqlCommand(query, sconn);
                cmd.Parameters.AddWithValue("@SutUretimID", inekID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Süt Kaydı Başarıyla Silindi");
                sconn.Close();
                populate();
                Temizle();
            }
            else
            {
                MessageBox.Show("Silmek İstediğiniz Süt Kaydının Satırını Seçiniz");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (SutUretimDGW.SelectedRows.Count > 0)
            {
                int selectedRowIndex = SutUretimDGW.SelectedRows[0].Index;
                int sutUretimID = Convert.ToInt32(SutUretimDGW.Rows[selectedRowIndex].Cells["SutUretimID"].Value);

                using (SqlConnection sconn = new SqlConnection(@"Data Source ="".\SQLEXPRESS""; Initial Catalog = ciftlikdb ; Integrated Security=True"))
                {
                    sconn.Open();
                    string query = @"UPDATE SutUretimTbl 
                         SET InekID = @InekID, 
                             InekIsmi = @InekIsmi, 
                             SabahSutu = @SabahSutu, 
                             OgleSutu = @OgleSutu, 
                             AksamSutu = @AksamSutu, 
                             ToplamSut = @ToplamSut,
                             UretimTarihi = @UretimTarihi
                         WHERE SutUretimID = @SutUretimID";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@InekID", InekIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@InekIsmi", InekismiTb.Text);
                    cmd.Parameters.AddWithValue("@SabahSutu", sabahTb.Text);
                    cmd.Parameters.AddWithValue("@OgleSutu", ogleTb.Text);
                    cmd.Parameters.AddWithValue("@AksamSutu", aksamTb.Text);
                    cmd.Parameters.AddWithValue("@ToplamSut", toplamTb.Text);
                    cmd.Parameters.AddWithValue("@UretimTarihi", date.Value.Date);
                    cmd.Parameters.AddWithValue("@SutUretimID", sutUretimID);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Süt Başarıyla Düzenlendi");
                    sconn.Close();
                    populate(); // Veriyi yeniden yükleme işlemi
                    Temizle(); // Formu temizleme veya diğer işlemler
                }
            }
            else
            {
                MessageBox.Show("Düzenlemek istediğiniz satırın en başına tıklayınız ardından düzenleye basınız");
            }
           
        }
    }
}