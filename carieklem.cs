using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace alisveris
{
    public partial class carieklem : Form
    {
        public carieklem()
        {
            InitializeComponent();
        }

        void random()
        {

            int sayi1;

            Random rastgeleSayi = new Random();
            sayi1 = rastgeleSayi.Next(0, 999999999);
            textBox2.Text = sayi1.ToString();
        }
        private void güncelle()
        {
            dad = new SqlDataAdapter("Select * from cari", baglanti);
            ds = new DataSet();
            baglanti.Open();
            dad.Fill(ds, "tablo");
            baglanti.Close();
            dataGridView1.DataSource = ds.Tables["tablo"];
        }

        public void arama(string aranan)
        {
            string sondan_arama = "select * from cari where cariAdi like'%" + aranan + "'";
            string bastan_arama = "select * from cari where cariAdilike'" + aranan + "'%";
            string ortadan_arama = "select * from cari where cariAdi like'%" + aranan + "%'";


            //hangi arama türünü kullancaksak onu seçeceğiz. Ben örnekte ortadan aramayı kullandım.
            OleDbDataAdapter adaptor = new OleDbDataAdapter(ortadan_arama, bağlantı);
            DataTable tablo = new DataTable();
            adaptor.Fill(tablo);
            dataGridView1.DataSource = tablo;

        }

         static string conString = "Data Source=ALBATROS\\TEW_SQLEXPRESS;Initial Catalog=urun;Integrated Security=True";

        SqlConnection baglanti = new SqlConnection(conString);
        SqlDataAdapter dad = new SqlDataAdapter();
        DataSet ds = new DataSet();
       
        OleDbDataAdapter da;
        DataTable dt;
        string sql = "SELECT * FROM cari";
        
        OleDbConnection bağlantı;
        OleDbCommand sqlkomutu;
        OleDbDataAdapter dab;
        DataSet dss;

          private void kayitGetir()
        {
            baglanti.Open();
            string kayit = "SELECT * from cari";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            SqlCommand komut = new SqlCommand(kayit, baglanti);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.
            SqlDataAdapter da = new SqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView1.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            baglanti.Close();
        }

         void Bagla()
        {
            baglanti.ConnectionString = "Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=urun;Integrated Security=True";
            baglanti.Open();
            
        }

       

        void gridDoldur()
        {

            baglanti = new SqlConnection("Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=urun;Integrated Security=True");
            dad = new SqlDataAdapter("Select *From cari", baglanti);
            ds = new DataSet();
            baglanti.Open();
            dad.Fill(ds, "cari");
            dataGridView1.DataSource = ds.Tables["cari"];
            baglanti.Close();
        }

         void bosalt()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";

        }

         string exeyolu;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


        }

          string islem;

        private void carieklem_Load(object sender, EventArgs e)
        {
            Bagla();
            
            gridDoldur();
            textBox2.Enabled = false;

        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private void cıkıs_Click(object sender, EventArgs e)
        {
            Menu frm = new Menu();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            try 
            {
                
                if (baglanti.State == ConnectionState.Closed)
                   



                if (textBox2.Text == "" || textBox3.Text == "" || textBox5.Text == ""|| textBox6.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakmayınız!!");
                }

                else
                {
                    baglanti.Open();
                    SqlCommand cmd = new SqlCommand();


                    if (islem == "yeni")
                    {
                        

                        cmd = new SqlCommand ( "insert into cari(cariAdi,cariKodu,caritelefonu,cariadres) values (@cariAdi,@cariKodu,@caritelefonu,@cariadres)",baglanti);
                       

                    }

                    else if (islem == "düzzenle")
                    {
                        cmd = new SqlCommand("update cari set cariAdi=@urunadi ,cariKodu=@urunModeli,caritelefonu=@urunAdedi, cariadres=@urunBarkodu", baglanti);
                        cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[4].Value);
                    }

                    cmd.Parameters.AddWithValue("@cariAdi", textBox3.Text);
                    cmd.Parameters.AddWithValue("@cariKodu", textBox2.Text);
                    cmd.Parameters.AddWithValue("@caritelefonu", textBox5.Text);
                    cmd.Parameters.AddWithValue("@cariadres", textBox6.Text);

                    //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                    cmd.ExecuteNonQuery();
                    //Veritabanında değişiklik yapacak komut işlemi bu satırda gerçekleşiyor.
                    baglanti.Close();
                    MessageBox.Show("Müşteri Kayıt İşlemi Gerçekleşti.");
                     }
                
            }

            catch (Exception hata)
            {
                MessageBox.Show("İşlem Sırasında Hata Oluştu." + hata.Message);
            }

            gridDoldur();
            baglanti.Close();
            bosalt();
            güncelle();
                }

       

        private void textBox3_MouseClick(object sender, MouseEventArgs e)
        {
            islem = "yeni";
            random();
        }

      

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from cari where cariAdi like '%" + textBox1.Text + "%'", baglanti);
            ara.Fill(tbl);
            baglanti.Close();
            dataGridView1.DataSource = tbl;
        }

       
    
        private void carieklem_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnSil_Click_1(object sender, EventArgs e)
        {
            DialogResult secim = MessageBox.Show("Kayıt Silinecek. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                Bagla();

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("delete from cari where id=@id", baglanti);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[4].Value);
                cmd.ExecuteNonQuery();
                gridDoldur();
                baglanti.Close();
                MessageBox.Show("Kullanıcı Silinmiştir");
                bosalt();

            }
        }
        
        }
    }



