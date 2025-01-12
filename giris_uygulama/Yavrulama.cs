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
    public partial class Yavrulama : Form
    {
        public Yavrulama()
        {
            InitializeComponent();
            FillInekId();
            populate();
        }
        SqlConnection sconn = new SqlConnection(@"Data Source ="".\SQLEXPRESS""; Initial Catalog = ciftlikdb ; Integrated Security=True");
       

        private void label10_Click(object sender, EventArgs e)
        {
            SutUretimi Ob = new SutUretimi();
            Ob.Show();
            this.Hide();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void Temizle()
        {
            InekIdCb.Text = string.Empty;
            InekIsmiTb.Text = string.Empty;
            CiftlesmeDate.Text = string.Empty;
            YasTb.Text = string.Empty;
            HamilelikDate.Text = string.Empty;
            BeklenenDate.Text = string.Empty;
            GerceklesenDate.Text = string.Empty;
            NotlarTb.Text = string.Empty;

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
            string query = "select * from YavrulamaTbl";
            SqlDataAdapter adapter = new SqlDataAdapter(query, sconn);
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            var ds = new DataSet();
            adapter.Fill(ds);
            YavrulamaDGW.DataSource = ds.Tables[0];
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
                YasTb.Text = dr["InekYasi"].ToString();
            }

            sconn.Close();
        }

     

        private void InekIdCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            GetInekIsmi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Temizle();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (InekIdCb.SelectedIndex == -1 || string.IsNullOrEmpty(InekIsmiTb.Text) || string.IsNullOrEmpty(CiftlesmeDate.Text) || string.IsNullOrEmpty(YasTb.Text) || string.IsNullOrEmpty(BeklenenDate.Text) || string.IsNullOrEmpty(HamilelikDate.Text) || string.IsNullOrEmpty(GerceklesenDate.Text) || string.IsNullOrEmpty(NotlarTb.Text))
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
            else
            {
                
                    sconn.Open();
                    string query = "INSERT INTO YavrulamaTbl (InekID, InekIsmi, CiftlesmeTarihi, HamilelikTarihi, BeklenenDT, GerceklesenDT,Notlar,InekYasi) " +
                                   "VALUES (@InekID, @InekIsmi,  @CiftlesmeTarihi, @HamilelikTarihi, @BeklenenDT, @GerceklesenDT, @Notlar, @InekYasi)";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@InekID", InekIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@InekIsmi", InekIsmiTb.Text);
                    cmd.Parameters.AddWithValue("@CiftlesmeTarihi", CiftlesmeDate.Value.Date);
                    cmd.Parameters.AddWithValue("@HamilelikTarihi", HamilelikDate.Value.Date);
                    cmd.Parameters.AddWithValue("@BeklenenDT", BeklenenDate.Value.Date);
                    cmd.Parameters.AddWithValue("@GerceklesenDT", GerceklesenDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Notlar", NotlarTb.Text);
                    cmd.Parameters.AddWithValue("@InekYasi", YasTb.Text);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Yavrulama Başarıyla Kaydedildi");
                    sconn.Close();
                    populate();
                    Temizle();
                
            }
        }
        int key = 0;
        private void YavrulamaDGW_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                CiftlesmeDate.Text = YavrulamaDGW.Rows[e.RowIndex].Cells[1].Value.ToString();
                InekIdCb.Text = YavrulamaDGW.Rows[e.RowIndex].Cells[2].Value.ToString();
                InekIsmiTb.Text = YavrulamaDGW.Rows[e.RowIndex].Cells[3].Value.ToString();
                HamilelikDate.Text = YavrulamaDGW.Rows[e.RowIndex].Cells[4].Value.ToString();
                BeklenenDate.Text = YavrulamaDGW.Rows[e.RowIndex].Cells[5].Value.ToString();
                GerceklesenDate.Text = YavrulamaDGW.Rows[e.RowIndex].Cells[6].Value.ToString();
                NotlarTb.Text = YavrulamaDGW.Rows[e.RowIndex].Cells[7].Value.ToString();
                YasTb.Text = YavrulamaDGW.Rows[e.RowIndex].Cells[8].Value.ToString();

                if (InekIsmiTb.Text == "")
                {
                    key = 0;
                }
                else
                {
                    if (YavrulamaDGW.SelectedRows.Count > 0 && YavrulamaDGW.SelectedRows[0].Cells[0].Value != null)
                    {
                        string value = YavrulamaDGW.SelectedRows[0].Cells[0].Value.ToString();
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
            if (YavrulamaDGW.SelectedRows.Count > 0)
            {
                // Seçilen satırın SaglikID değerini al
                int selectedRowIndex = YavrulamaDGW.SelectedRows[0].Index;
                int YavrulamaID = Convert.ToInt32(YavrulamaDGW.Rows[selectedRowIndex].Cells["YavrulamaID"].Value);

                
                    sconn.Open();
                    string query = "DELETE FROM YavrulamaTbl WHERE YavrulamaID=@YavrulamaID";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("YavrulamaID", YavrulamaID);

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
            if (InekIdCb.SelectedIndex == -1 || string.IsNullOrEmpty(InekIsmiTb.Text) || string.IsNullOrEmpty(CiftlesmeDate.Text) || string.IsNullOrEmpty(YasTb.Text) || string.IsNullOrEmpty(BeklenenDate.Text) || string.IsNullOrEmpty(HamilelikDate.Text) || string.IsNullOrEmpty(GerceklesenDate.Text) || string.IsNullOrEmpty(NotlarTb.Text))
            {
                MessageBox.Show("Eksik bilgi girdiniz");
            }
            //... (eksik bilgi kontrolü)
            else if (YavrulamaDGW.SelectedRows.Count > 0)
            {
                int selectedRowIndex = YavrulamaDGW.SelectedRows[0].Index;
                int YavrulamaID = Convert.ToInt32(YavrulamaDGW.Rows[selectedRowIndex].Cells["YavrulamaID"].Value);

                
                    sconn.Open();
                    string query = "UPDATE YavrulamaTbl SET InekID=@InekID, InekIsmi=@InekIsmi, CiftlesmeTarihi=@CiftlesmeTarihi, HamilelikTarihi=@HamilelikTarihi, BeklenenDT=@BeklenenDT, GerceklesenDT=@GerceklesenDT, Notlar=@Notlar, InekYasi=@InekYasi WHERE YavrulamaID=@YavrulamaID";

                    SqlCommand cmd = new SqlCommand(query, sconn);
                    cmd.Parameters.AddWithValue("@InekID", InekIdCb.SelectedValue);
                    cmd.Parameters.AddWithValue("@InekIsmi", InekIsmiTb.Text);
                    cmd.Parameters.AddWithValue("@CiftlesmeTarihi", CiftlesmeDate.Value.Date);
                    cmd.Parameters.AddWithValue("@HamilelikTarihi", HamilelikDate.Value.Date);
                    cmd.Parameters.AddWithValue("@BeklenenDT", BeklenenDate.Value.Date);
                    cmd.Parameters.AddWithValue("@GerceklesenDT", GerceklesenDate.Value.Date);
                    cmd.Parameters.AddWithValue("@Notlar", NotlarTb.Text);
                    cmd.Parameters.AddWithValue("@InekYasi", YasTb.Text);
                    cmd.Parameters.AddWithValue("@YavrulamaID", YavrulamaID);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Başarıyla Güncellendi");
                    sconn.Close();
                    populate();
                    Temizle();
                
            }

        }

        private void Yavrulama_Load(object sender, EventArgs e)
        {

        }
    }
    }

