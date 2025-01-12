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
    public partial class Calisan : Form
    {
        public Calisan()
        {
            InitializeComponent();
            populate();

        }
        SqlConnection sconn = new SqlConnection(@"Data Source ="".\SQLEXPRESS""; Initial Catalog = ciftlikdb ; Integrated Security=True");
        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Calisan_Load(object sender, EventArgs e)
        {

        }
        private void populate()
        {
            sconn.Open();
            string query = "select * from CalisanTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sconn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            CalisanDGW.DataSource = ds.Tables[0];
            sconn.Close();

        }
        private void Temizle()
        {
            CalisanIsmiTb.Text = string.Empty;
            TelNoTb.Text = string.Empty;
            Adres1Tb.Text = string.Empty;
            DogumTarihTb.Text = string.Empty;
            CinsiyetCb.SelectedIndex = -1;
            SifreTb.Text = string.Empty;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CalisanIsmiTb.Text) || string.IsNullOrEmpty(TelNoTb.Text) || string.IsNullOrEmpty(Adres1Tb.Text) || CinsiyetCb.SelectedIndex == -1 || string.IsNullOrEmpty(SifreTb.Text) )
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
            else 
            {
               
              
                    sconn.Open();
                    string query = "INSERT INTO CalisanTbl (CalisanIsmi, CalisanDT, CalisanCinsiyet, CalisanTelNo, CalisanAdres,CalisanSifre) " +
                                   "VALUES (@CalisanIsmi, @CalisanDT, @CalisanCinsiyet, @CalisanTelNo, @CalisanAdres ,@CalisanSifre)";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@CalisanCinsiyet", CinsiyetCb.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@CalisanIsmi", CalisanIsmiTb.Text);
                    cmd.Parameters.AddWithValue("@CalisanDT", DogumTarihTb.Value.Date);
                    cmd.Parameters.AddWithValue("@CalisanTelNo", TelNoTb.Text);
                    cmd.Parameters.AddWithValue("@CalisanAdres", Adres1Tb.Text);
                    cmd.Parameters.AddWithValue("@CalisanSifre", SifreTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Çalışan Başarıyla Kaydedildi");
                    sconn.Close();
                    populate();
                    Temizle();
                
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (CalisanDGW.SelectedRows.Count > 0)
            {
              
                int selectedRowIndex = CalisanDGW.SelectedRows[0].Index;
                int CalisanID = Convert.ToInt32(CalisanDGW.Rows[selectedRowIndex].Cells["CalisanID"].Value);

                
                    sconn.Open();
                    string query = "DELETE FROM CalisanTbl WHERE CalisanID=@CalisanID";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("CalisanID", CalisanID);

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
        int key = 0;
        private void CalisanDGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                CalisanIsmiTb.Text = CalisanDGW.Rows[e.RowIndex].Cells[1].Value.ToString();
                DogumTarihTb.Text = CalisanDGW.Rows[e.RowIndex].Cells[2].Value.ToString();
                CinsiyetCb.Text = CalisanDGW.Rows[e.RowIndex].Cells[3].Value.ToString();
                TelNoTb.Text = CalisanDGW.Rows[e.RowIndex].Cells[4].Value.ToString();
                Adres1Tb.Text = CalisanDGW.Rows[e.RowIndex].Cells[5].Value.ToString();
                SifreTb.Text = CalisanDGW.Rows[e.RowIndex].Cells[6].Value.ToString();
                if (CalisanIsmiTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    if (CalisanDGW.SelectedRows.Count > 0 && CalisanDGW.SelectedRows[0].Cells[0].Value != null)
                    {
                        string value = CalisanDGW.SelectedRows[0].Cells[0].Value.ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(CalisanIsmiTb.Text) || string.IsNullOrEmpty(TelNoTb.Text) || string.IsNullOrEmpty(Adres1Tb.Text) || CinsiyetCb.SelectedIndex == -1)
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
       
            else if (CalisanDGW.SelectedRows.Count > 0)
            {
                int selectedRowIndex = CalisanDGW.SelectedRows[0].Index;
                int CalisanID = Convert.ToInt32(CalisanDGW.Rows[selectedRowIndex].Cells["CalisanID"].Value);

                
                    sconn.Open();
                    string query = "UPDATE CalisanTbl SET CalisanIsmi=@CalisanIsmi, CalisanDT=@CalisanDT, CalisanCinsiyet=@CalisanCinsiyet, CalisanTelNo=@CalisanTelNo, CalisanAdres=@CalisanAdres, CalisanSifre=@CalisanSifre WHERE CalisanID=@CalisanID";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@CalisanIsmi", CalisanIsmiTb.Text);
                    cmd.Parameters.AddWithValue("@CalisanDT", DogumTarihTb.Value.Date);
                    cmd.Parameters.AddWithValue("@CalisanCinsiyet", CinsiyetCb.Text);
                    cmd.Parameters.AddWithValue("@CalisanTelNo", TelNoTb.Text);
                    cmd.Parameters.AddWithValue("@CalisanAdres", Adres1Tb.Text);
                    cmd.Parameters.AddWithValue("@CalisanID", CalisanID);
                    cmd.Parameters.AddWithValue("@CalisanSifre", SifreTb.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarıyla Güncellendi");
                    sconn.Close();
                    populate();
                    Temizle();
                
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
 }

