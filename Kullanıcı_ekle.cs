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
    public partial class Kullanıcı_ekle : Form
    {
        public Kullanıcı_ekle()
        {
            InitializeComponent();
        }
        static string conString = "Data Source=ALBATROS\\TEW_SQLEXPRESS;Initial Catalog=urun;Integrated Security=True";

        SqlConnection baglanti = new SqlConnection(conString);
        SqlDataAdapter dad = new SqlDataAdapter();
        DataSet ds = new DataSet();
       
        OleDbDataAdapter da;
        DataTable dt;
        string sql = "SELECT * FROM Uye";
        
        OleDbConnection bağlantı;
        OleDbCommand sqlkomutu;
        OleDbDataAdapter dab;
        DataSet dss;
 
     
  

        private void VeriGoster_Load(object sender, EventArgs e)
        {
            kayitGetir();
            //Tüm kayıtları gösterecek olan kayitGetir() metodumuzu çağırıyoruz.
        }

        private void kayitGetir()
        {
            baglanti.Open();
            string kayit = "SELECT * from Uye";
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
        private void güncelle()
        {
            da = new OleDbDataAdapter("Select * from Uye", bağlantı);
            ds = new DataSet();
            bağlantı.Open();
            da.Fill(ds, "tablo");
            bağlantı.Close();
            dataGridView1.DataSource = ds.Tables["tablo"];
        }


       
        void Bagla()
        {
            baglanti.ConnectionString = "Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=urun;Integrated Security=True";
            baglanti.Open();
            
            
            // TODO: This line of code loads data into the 'listeDataSet1.tablo' table. You can move, or remove it, as needed.
         
        }

        public void arama(string aranan)
        {
           string sondan_arama = "select * from isimler where isim like'%" + aranan + "'";
        string bastan_arama = "select * from isimler where isim like'" + aranan + "'%";
        string ortadan_arama = "select * from isimler where isim like'%" + aranan + "%'";
       
            
            //hangi arama türünü kullancaksak onu seçeceğiz. Ben örnekte ortadan aramayı kullandım.
        OleDbDataAdapter adaptor = new OleDbDataAdapter(ortadan_arama, bağlantı);
            DataTable tablo = new DataTable();
            adaptor.Fill(tablo);
            dataGridView1.DataSource = tablo;
            
        }

        void gridDoldur()
        {

            baglanti = new SqlConnection("Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=urun;Integrated Security=True");
            dad = new SqlDataAdapter("Select *From Uye", baglanti);
            ds = new DataSet();
            baglanti.Open();
            dad.Fill(ds, "Uye");
            dataGridView1.DataSource = ds.Tables["Uye"];
            baglanti.Close();
        }

        void bosalt()
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        string exeyolu;
        
        string islem;

        private void btnSil_Click(object sender, EventArgs e)
        {
             DialogResult secim = MessageBox.Show("Kayıt Silinecek. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
             if (secim == DialogResult.Yes)
             {
                 Bagla();

                 SqlCommand cmd = new SqlCommand();
                 cmd = new SqlCommand("delete from Uye where id=@id", baglanti);
                 cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[3].Value);
                 cmd.ExecuteNonQuery();
                 gridDoldur();
                 baglanti.Close();
                 MessageBox.Show("Kullanıcı Silinmiştir");
                 bosalt();

             }

        }

        private void button2_Click(object sender, EventArgs e)
        { 
      
            try 
            {
                
                if (baglanti.State == ConnectionState.Closed)
                   



                if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakmayınız!!");
                }

                else
                {
                    baglanti.Open();

                    // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                    string kayit = "insert into Uye(KullaniciAdi,KullaniciSifresi,İsimler) values (@KullaniciAdi,@KullaniciSifresi,@İsimler)";
                    // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                    SqlCommand komut = new SqlCommand(kayit, baglanti);
                    //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.


                    komut.Parameters.AddWithValue("@KullaniciAdi", textBox3.Text);
                    komut.Parameters.AddWithValue("@KullaniciSifresi", textBox2.Text);
                    komut.Parameters.AddWithValue("@İsimler", textBox4.Text);

                    //Parametrelerimize Form üzerinde ki kontrollerden girilen verileri aktarıyoruz.
                    komut.ExecuteNonQuery();
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

        }

        private void Kullanıcı_ekle_Load(object sender, EventArgs e)
        {
            Bagla();
           
            gridDoldur();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        

        public OleDbConnection baglantii { get; set; }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from Uye where İsimler like '%" + textBox1.Text + "%'", baglanti);
            ara.Fill(tbl);
            baglanti.Close();
            dataGridView1.DataSource = tbl;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            yonetmenu frm = new yonetmenu();
            this.Close();
        }

        

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            {
                //checkBox işaretli ise
                if (checkBox1.Checked)
                {
                    //karakteri göster.
                    textBox2.PasswordChar = '\0';
                }
                //değilse karakterlerin yerine * koy.
                else
                {
                    textBox2.PasswordChar = '#';
                }
            }
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                button2.Enabled = true;
            else
                button2.Enabled = false;
        }

        private void Kullanıcı_ekle_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
    

