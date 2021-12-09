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
    public partial class Stok_cıkıs : Form
    {
        public Stok_cıkıs()
        {
            InitializeComponent();
        }

        SqlConnection bag = new SqlConnection();
        SqlDataAdapter dad = new SqlDataAdapter();
        DataSet ds = new DataSet();

        void Bagla()
        {
            bag.ConnectionString = "Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=urun;Integrated Security=True";
            bag.Open();
        }

        void gridDoldur()
        {

            ds.Clear();

            dad = new SqlDataAdapter("Select * from urun", bag);

            dad.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];

            bag.Close();
        }




        private void Form3_Load(object sender, EventArgs e)
        {
            Bagla();

            gridDoldur();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        void bosalt()
        {
       
            txturunAdedi.Text = "";
           
        }

        string exeyolu;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
            txtadi.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txturunModeli.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txturunBarkodu.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txturunAdedi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtstokTarihi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtKayitYapanKisi.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            //exeyolu1= System.Reflection.Assembly.GetEntryAssembly().Location;
            exeyolu = Application.StartupPath;
            pictureBox1.ImageLocation=exeyolu+"\\res\\"+ dataGridView1.CurrentRow.Cells[7].Value.ToString();
            pictureBox2.ImageLocation = exeyolu + "\\res\\" + dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        void modDuzenleme()
        {
            
            btnDuzenle.Enabled = false;
            btnKaydet.Enabled = true;
           
            btnIptal.Enabled = true;

            txtadi.Enabled = false;
            txturunModeli.Enabled = false;
            txturunBarkodu.Enabled = false;
            txturunAdedi.Enabled = true;
            txtstokTarihi.Enabled = false;
            txtKayitYapanKisi.Enabled = false;
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;    
        }

        void modGezinti()
        {
           
            btnDuzenle.Enabled = true;
            btnKaydet.Enabled = false;
         
            
            btnIptal.Enabled = false;

            txtadi.Enabled = false;
            txturunModeli.Enabled = false;
            txturunBarkodu.Enabled = false;
            txturunAdedi.Enabled = false;
            txtstokTarihi.Enabled = false;
            txtKayitYapanKisi.Enabled = false;
            pictureBox1.Enabled = false;
            pictureBox2.Enabled = false;       
        }

        private void btnIptal_Click(object sender, EventArgs e)
        {
            DialogResult secim = MessageBox.Show("Kayıt İptal Edilecektir. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                modGezinti();
            }
        }
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            islem = "düzenle";
            modDuzenleme();
            bosalt();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            islem = "yeni";
            bosalt();
            modDuzenleme();
        }
        string islem;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            DialogResult secim = MessageBox.Show("Kayıt Edilecektir. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                if (txturunAdedi.Text == "")
                {
                    MessageBox.Show("Boş Alan Bırakmayınız!!");
                }

                else
                {
                    Bagla();
                    SqlCommand cmd = new SqlCommand();

                    if (islem == "yeni")
                    {
                        cmd = new SqlCommand("insert into urun (urunAdi,urunModeli, urunBarkodu,urunAdedi,stokTarihi,KayitYapanKisi, urunfoto ,Barkodfoto) values( @urunAdi,@urunModeli,@urunBarkodu,@urunAdedi,@stokTarihi,@KayitYapanKisi,@foto,@Barkodfoto)", bag);
                    }
                    else if (islem == "düzenle")
                    {

                        cmd = new SqlCommand("update urun set urunAdedi=urunAdedi- @urunAdedi where urunid=@id", bag);
                        cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);

                    }




                    cmd.Parameters.AddWithValue("@urunAdedi", txturunAdedi.Text);

                    cmd.ExecuteNonQuery();


                    gridDoldur();
                    bag.Close();
                    MessageBox.Show("Stok Unuz Kayıt Edilmiş ve Düzenlenmiştir");
                    modGezinti();


                }
            }
        }
        private void btnSil_Click(object sender, EventArgs e)
        {
            modGezinti();
            DialogResult secim = MessageBox.Show("Kayıt Silinecek. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (secim==DialogResult.Yes) {
                Bagla();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("delete from urun where urunid=@id", bag);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
                cmd.ExecuteNonQuery();
                gridDoldur();
                bag.Close();
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult secim = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                alisveris.Stok_cıkıs formkapa = new alisveris.Stok_cıkıs();
                formkapa.Close();
                Menu form = new Menu();
                form.Show();
                this.Hide();
            }
        }


        public object id { get; set; }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            openFileDialog2.ShowDialog();
        }

        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            pictureBox2.ImageLocation = openFileDialog2.FileName;
        }

        private void frmStoklar_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult secim = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                Kullanıcı_ekle formkapa = new Kullanıcı_ekle();
                formkapa.Close();
                Menu form = new Menu();
                form.Show();
                this.Hide();
            }
        }

        private void button1_Click(object sender, EventArgs e)
            
        {
          
        }

       

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bag.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from urun where urunAdi like '%" + textBox1.Text + "%'", bag);
            ara.Fill(tbl);
            bag.Close();
            dataGridView1.DataSource = tbl;
        }

        private void txtadi_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
            

                }

        private void txturunAdedi_TextChanged(object sender, EventArgs e)
        {

        }

        private void txturunAdedi_KeyPress(object sender, KeyPressEventArgs e)
        {
            

     e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

            }
        }
    

