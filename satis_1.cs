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
    public partial class satis_1 : Form
    {
        public satis_1()
        {
            InitializeComponent();
        }
        public static string yapis;

        SqlConnection bag = new SqlConnection();
        SqlDataAdapter dad = new SqlDataAdapter();
        DataSet ds = new DataSet();
        Double a, b, c, d;
        String x;

        void Bagla()
        {
            bag.ConnectionString = "Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=urun;Integrated Security=True";


            bag.Open();

        }
        void gridDoldur()
        {

            ds.Clear();

            dad = new SqlDataAdapter("Select * from cariurun", bag);

            dad.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            bag.Close();
        }

        private void güncelle()
        {
            dad = new SqlDataAdapter("Select * from cariurun", bag);
            ds = new DataSet();
            bag.Open();
            dad.Fill(ds, "tablo");
            bag.Close();
            dataGridView1.DataSource = ds.Tables["tablo"];
        }
        void bosalt()
        {
            carikodu.Text = "";
            cariadi.Text = "";
            urunadi.Text = "";
            urunmodeli.Text = "";
            urunbarkodu.Text = "";
            urunadedi.Text = "";
            satısfiyati.Text = "";
            satıstarihi.Text = "";

        }

        private void satıs_1_Load(object sender, EventArgs e)
        {
            Bagla();

            gridDoldur();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            alisveris.kullanıcı_ekle2 ffmust = new alisveris.kullanıcı_ekle2();

            ffmust.ShowDialog();
            if (ffmust.ok)
            {
                cariadi.Text = ffmust.a1;
                carikodu.Text = ffmust.a2;
            }

            islem = "yeni";
        }


        string islem;
        private void button3_Click_1(object sender, EventArgs e)
        {
            if (urunadi.Text == "" || urunmodeli.Text == "" || urunadedi.Text == "" || urunbarkodu.Text == "" || satısfiyati.Text == "" || cariadi.Text == "" || carikodu.Text == "")
            {
                MessageBox.Show("Boş Alan Bırakmayınız!!");
            }
            else
            {
                bag.Open();

                SqlCommand cmd = new SqlCommand();
                if (islem == "yeni")
                {

                    cmd = new SqlCommand("insert into cariurun (urunAdi,urunModeli,urunAdedi,urunBarkodu,satısFiyati,urunSatısTarihi,cariAdi,cariKodu) values(@urunadi,@urunModeli,@urunAdedi,@urunBarkodu,@satısFiyati,@urunSatısTarihi,@cariAdi,@cariKodu)", bag);

                    

                }
                else if (islem == "düzenle")
                {

                    cmd = new SqlCommand("update cariurun set urunAdi=@urunadi ,urunModeli=@urunModeli,urunAdedi=@urunAdedi, urunBarkodu=@urunBarkodu , satısFiyati=@satısFiyati, urunSatısTarihi=@urunSatısTarihi, cariAdi=@cariAdi, cariKodu=@cariKodu where urunid=@id ", bag);
                    cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[8].Value);

                }

                cmd.Parameters.AddWithValue("@urunadi", urunadi.Text);
                cmd.Parameters.AddWithValue("@urunModeli", urunmodeli.Text);
                cmd.Parameters.AddWithValue("@urunAdedi", urunadedi.Text);
                cmd.Parameters.AddWithValue("@urunBarkodu", urunbarkodu.Text);
                cmd.Parameters.AddWithValue("@satısFiyati", satısfiyati.Text);
                cmd.Parameters.AddWithValue("@urunSatısTarihi", satıstarihi.Value.Date);
                cmd.Parameters.AddWithValue("@cariAdi", cariadi.Text);
                cmd.Parameters.AddWithValue("@cariKodu", carikodu.Text);

                cmd.ExecuteNonQuery();

                string sql = string.Format("update urun set urunAdedi=(select urunAdedi from urun where urunid={0}) - {1} where urunid={0}",
                        int.Parse(textBox2.Text), int.Parse(urunadedi.Text));
                SqlCommand cmd1 = new SqlCommand(sql, bag); cmd1.ExecuteNonQuery();

                bag.Close();
                güncelle();
                bosalt();

            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            urunadi.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            urunmodeli.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();

            urunadedi.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            urunbarkodu.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            satısfiyati.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            satıstarihi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            cariadi.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            carikodu.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();


        }
        private void button2_Click(object sender, EventArgs e)
        {
            alisveris.urunekle fmuusst = new alisveris.urunekle();
            fmuusst.ShowDialog();

            if (fmuusst.ok)
            {
                urunadi.Text = fmuusst.s1;
                urunmodeli.Text = fmuusst.s2;

                satısfiyati.Text = fmuusst.s3;

                urunbarkodu.Text = fmuusst.s4;
                textBox2.Text = fmuusst.s9;
            }
        }

        private void checkBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked)
            {
                a = Convert.ToDouble(satısfiyati.Text);
                b = (a - ((a * 18) / 100));

                satısfiyati.Text = Convert.ToString(b);

            }
            //değilse karakterlerin yerine * koy.
            else
            {
                a = Convert.ToDouble(satısfiyati.Text);
                b = a * 18;
                c = b / 100;
                d = a + c;


                satısfiyati.Text = Convert.ToString(d);

            }
        }
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            bag.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from cariurun where urunAdi like '%" + textBox1.Text + "%'", bag);
            ara.Fill(tbl);
            bag.Close();
            dataGridView1.DataSource = tbl;
        }

        private void satis_1_FormClosed(object sender, FormClosedEventArgs e)
        {

            alisveris.Stok_cıkıs formkapa = new alisveris.Stok_cıkıs();
            formkapa.Close();
            Menu form = new Menu();
            form.Show();
            this.Hide();
        }
        private void button4_Click_1(object sender, EventArgs e)
        {
            Menu frm = new Menu();
            this.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            islem = "düzenle";

        }
        private void satis_1_Load(object sender, EventArgs e)
        {

            Bagla();

            gridDoldur();

        }
        private void satis_1_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }

        }
        private void urunsil_Click(object sender, EventArgs e)
        {
            DialogResult secim = MessageBox.Show("Kayıt Silinecek. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                Bagla();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("delete from cariurun where urunid=@id", bag);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[8].Value);
                cmd.ExecuteNonQuery();
                gridDoldur();
                bag.Close();
                MessageBox.Show("kayıt Silinmiştir");
            }
        }
    }
}


