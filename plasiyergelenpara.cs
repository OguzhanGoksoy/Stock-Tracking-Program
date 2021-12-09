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
    public partial class plasiyergelenpara : Form
    {
        public plasiyergelenpara()
        {
            InitializeComponent();
        }

        static string conString = "Data Source=ALBATROS\\TEW_SQLEXPRESS;Initial Catalog=urun;Integrated Security=True";

        SqlConnection baglanti = new SqlConnection(conString);
        SqlDataAdapter dad = new SqlDataAdapter();
        DataSet ds = new DataSet();

        OleDbDataAdapter da;
        DataTable dt;
        string sql = "SELECT * FROM calısanpara";

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
            string kayit = "SELECT * from calısanpara";
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
            da = new OleDbDataAdapter("Select * from calısanpara", bağlantı);
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

        }

        void bosalt()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
        }

        void gridDoldur()
        {

            baglanti = new SqlConnection("Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=urun;Integrated Security=True");
            dad = new SqlDataAdapter("Select *From calısanpara", baglanti);
            ds = new DataSet();
            baglanti.Open();
            dad.Fill(ds, "calısanpara");
            dataGridView1.DataSource = ds.Tables["calısanpara"];
            baglanti.Close();
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
        private void plasiyergelenpara_Load(object sender, EventArgs e)
        {
            Bagla();

            gridDoldur();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (baglanti.State == ConnectionState.Closed)

                    if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox5.Text == "")
                    {
                        MessageBox.Show("Boş Alan Bırakmayınız!!");
                    }

                    else
                    {
                        baglanti.Open();

                        // Bağlantımızı kontrol ediyoruz, eğer kapalıysa açıyoruz.
                        string kayit = "insert into calısanpara(calısan,calısankodu,getirdigipara,aciklama) values (@KullaniciAdi,@KullaniciSifresi,@İsimler,@aciklama)";
                        // müşteriler tablomuzun ilgili alanlarına kayıt ekleme işlemini gerçekleştirecek sorgumuz.
                        SqlCommand komut = new SqlCommand(kayit, baglanti);
                        //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.


                        komut.Parameters.AddWithValue("@KullaniciAdi", textBox1.Text);
                        komut.Parameters.AddWithValue("@KullaniciSifresi", textBox2.Text);
                        komut.Parameters.AddWithValue("@İsimler", textBox3.Text);
                        komut.Parameters.AddWithValue("@aciklama", textBox5.Text);
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

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult secim = MessageBox.Show("Kayıt Silinecek. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                Bagla();

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("delete from calısanpara where id=@id", baglanti);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[3].Value);
                cmd.ExecuteNonQuery();
                gridDoldur();
                baglanti.Close();
                MessageBox.Show("Kayıtınız Silinmiştir");
                bosalt();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            alisveris.calisan fmust = new alisveris.calisan();

            fmust.ShowDialog();
            if (fmust.ok)
            {
                textBox1.Text = fmust.p1;
                textBox2.Text = fmust.p2;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            yonetmenu frm = new yonetmenu();
            this.Close();
        }

        private void plasiyergelenpara_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
           

            baglanti.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from cari where cariAdi like '%" + textBox4.Text + "%'", baglanti);
            ara.Fill(tbl);
            baglanti.Close();
            dataGridView1.DataSource = tbl;
        }
    }
}
