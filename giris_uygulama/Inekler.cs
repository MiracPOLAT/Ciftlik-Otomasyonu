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
    public partial class Inekler : Form
    {
        public Inekler()
        {
            InitializeComponent();
            populate();
        }
        SqlConnection sconn = new SqlConnection(@"Data Source ="".\SQLEXPRESS""; Initial Catalog = ciftlikdb ; Integrated Security=True");
        

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

        private void label16_Click(object sender, EventArgs e)
        {
            GostergePaneli1 Ob = new GostergePaneli1();
            Ob.Show();
            this.Hide();
        }

      
        private void populate()
        {
            sconn.Open();
            string query = "select * from InekTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query , sconn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds  = new DataSet();
            adapter.Fill(ds);
            InekDGW.DataSource = ds.Tables[0];
            sconn.Close();

        }
        private void temizle ()
        {
            InekIsmiTb.Text = string.Empty;
            KulakEtiketiTb.Text = string.Empty;
            RenkTb.Text = string.Empty;
            YasTb.Text = string.Empty;
            DogumKilosuTb.Text = string.Empty;
            YemTb.Text = string.Empty;
        }

        int age = 0;

        private void button1_Click(object sender, EventArgs e)
        {

           if( string.IsNullOrEmpty(InekIsmiTb.Text) || string.IsNullOrEmpty(KulakEtiketiTb.Text)  || string.IsNullOrEmpty(RenkTb.Text) || string.IsNullOrEmpty(YasTb.Text) || string.IsNullOrEmpty(DogumKilosuTb.Text) || string.IsNullOrEmpty(YemTb.Text ))
            { 
                MessageBox.Show("Eksik bilgi girdiniz");
            
            }
           else
            {
                sconn.Open();
                String Query = "insert into InekTbl values('" + InekIsmiTb.Text + "','" + KulakEtiketiTb.Text + "','" + RenkTb.Text + "','" + age + "','" + DogumKilosuTb.Text + "','" + YemTb.Text + "')";
                SqlCommand cmd = new SqlCommand(Query,sconn);
                cmd.ExecuteNonQuery();
                MessageBox.Show("İnek Başarıyla Kaydedildi");
                sconn.Close();
                populate();
                temizle();
            }
        }

        private void DogumTarihTb_ValueChanged(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date - DogumTarihTb.Value.Date).Days)/365;
            
        }

        private void DogumTarihTb_MouseLeave(object sender, EventArgs e)
        {
            age = Convert.ToInt32((DateTime.Today.Date - DogumTarihTb.Value.Date).Days) / 365;
            YasTb.Text = "" + age; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            temizle();
        }
        int key = 0;

        private void InekDGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = InekDGW.Rows[e.RowIndex];

                InekIsmiTb.Text = row.Cells["InekIsmi"].Value.ToString();
                KulakEtiketiTb.Text = row.Cells["KulakEtiketi"].Value.ToString();
                RenkTb.Text = row.Cells["InekRengi"].Value.ToString();
                YasTb.Text = row.Cells["InekYasi"].Value.ToString();
                DogumKilosuTb.Text = row.Cells["DogumKilosu"].Value.ToString();
                YemTb.Text = row.Cells["Yem"].Value.ToString();
            }

           
        }

        private void InekDGW_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                InekIsmiTb.Text =InekDGW.Rows[e.RowIndex].Cells[1].Value.ToString();
                KulakEtiketiTb.Text =InekDGW.Rows[e.RowIndex].Cells[2].Value.ToString();
                RenkTb.Text = InekDGW.Rows[e.RowIndex].Cells[3].Value.ToString();
                DogumKilosuTb.Text =InekDGW.Rows[e.RowIndex].Cells[5].Value.ToString();
                YemTb.Text =InekDGW.Rows[e.RowIndex].Cells[6].Value.ToString();
                

                string yasStr = InekDGW.Rows[e.RowIndex].Cells[4].Value.ToString();
                if (int.TryParse(yasStr, out int yas))
                {
                   
                    DateTime bugun = DateTime.Today;

                    DateTime dogumTarihi = bugun.AddYears(-yas);

                    DogumTarihTb.Value = dogumTarihi;
                }
                else
                {
                    
                    MessageBox.Show("Geçersiz yaş bilgisi");
                }



            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (InekDGW.SelectedRows.Count > 0)
            {
                int selectedRowIndex = InekDGW.SelectedRows[0].Index;
                int inekID = Convert.ToInt32(InekDGW.Rows[selectedRowIndex].Cells[0].Value); 

                sconn.Open();
                string query = "DELETE FROM InekTbl WHERE InekID = @InekID";
                SqlCommand cmd = new SqlCommand(query, sconn);
                cmd.Parameters.AddWithValue("@InekID", inekID);
                cmd.ExecuteNonQuery();
                MessageBox.Show("İnek Başarıyla Silindi");
                sconn.Close();
                populate();
                temizle();
            }
            else
            {
                MessageBox.Show("Silmek İstediğiniz İneği Seçiniz");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (InekDGW.SelectedRows.Count > 0)
            {
                int selectedRowIndex = InekDGW.SelectedRows[0].Index;
                int inekID = Convert.ToInt32(InekDGW.Rows[selectedRowIndex].Cells[0].Value); 

                sconn.Open();
                String Query = "UPDATE InekTbl SET InekIsmi=@InekIsmi, KulakEtiketi=@KulakEtiketi, InekRengi=@InekRengi, InekYasi=@InekYasi, DogumKilosu=@DogumKilosu, Yem=@Yem WHERE InekID=@InekID";
                SqlCommand cmd = new SqlCommand(Query, sconn);
                cmd.Parameters.AddWithValue("@InekID", inekID);
                cmd.Parameters.AddWithValue("@InekIsmi", InekIsmiTb.Text);
                cmd.Parameters.AddWithValue("@KulakEtiketi", KulakEtiketiTb.Text);
                cmd.Parameters.AddWithValue("@InekRengi", RenkTb.Text);
                cmd.Parameters.AddWithValue("@InekYasi", age); 
                cmd.Parameters.AddWithValue("@DogumKilosu", DogumKilosuTb.Text);
                cmd.Parameters.AddWithValue("@Yem", YemTb.Text);
               

                cmd.ExecuteNonQuery();
                MessageBox.Show("İnek Başarıyla Düzenlendi");
                sconn.Close();
                populate();
                temizle();
            }
            else
            {
                MessageBox.Show("Düzenlemek İstediğiniz İneği Seçiniz");
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Inekler_Load(object sender, EventArgs e)
        {

        }
    }
    }

