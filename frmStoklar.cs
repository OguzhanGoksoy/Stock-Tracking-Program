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
    public partial class frmStoklar : Form
    {
        public frmStoklar()
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
            txtadi.Text = "";
            txturunModeli.Text = "";
            txturunBarkodu.Text = "";
            txturunAdedi.Text = "";
            txtstokTarihi.Text = "";
            txtKayitYapanKisi.Text = "";
            pictureBox1.ImageLocation = "";
            pictureBox2.ImageLocation = "";
        }

        string exeyolu;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            txtadi.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txturunModeli.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txturunBarkodu.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txturunAdedi.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtstokTarihi.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            txtKayitYapanKisi.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            //exeyolu1= System.Reflection.Assembly.GetEntryAssembly().Location;
            exeyolu = Application.StartupPath;
            pictureBox1.ImageLocation = exeyolu + "\\res\\" + dataGridView1.CurrentRow.Cells[7].Value.ToString();

            pictureBox2.ImageLocation = exeyolu + "\\res\\" + dataGridView1.CurrentRow.Cells[8].Value.ToString();
        }

        void modDuzenleme()
        {
            btnYeni.Enabled = false;
            btnDuzenle.Enabled = false;
            btnKaydet.Enabled = true;
            btnSil.Enabled = false;
            btnIptal.Enabled = true;

            txtadi.Enabled = true;
            txturunModeli.Enabled = true;
            txturunBarkodu.Enabled = true;
            txturunAdedi.Enabled = true;
            txtstokTarihi.Enabled = false;
            txtKayitYapanKisi.Enabled = true;
            pictureBox1.Enabled = true;
            pictureBox2.Enabled = true;
        }

        void modGezinti()
        {
            btnYeni.Enabled = true;
            btnDuzenle.Enabled = true;
            btnKaydet.Enabled = false;
            btnSil.Enabled = true;

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
            DialogResult secim = MessageBox.Show("İptal Edilcektir. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                modGezinti();
            }
        }

        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            islem = "düzenle";
            modDuzenleme();
        }

        private void btnYeni_Click(object sender, EventArgs e)
        {
            islem = "yeni";
            bosalt();
            modDuzenleme();
            DateTime zaman = DateTime.Today;
            txtstokTarihi.Text = zaman.ToShortDateString();
            txtstokTarihi.Enabled = false;
        }
        string islem;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            DialogResult secim = MessageBox.Show("Kayıt Edilecektir . Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                if (txtadi.Text == "" || txturunModeli.Text == "" || txturunBarkodu.Text == "" || txturunAdedi.Text == "" || txtstokTarihi.Text == "" || txtKayitYapanKisi.Text == "")
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
                        cmd = new SqlCommand("update urun set urunAdi=@urunAdi,urunModeli=@urunModeli,urunBarkodu=@urunBarkodu,urunAdedi=@urunAdedi,stokTarihi=@stokTarihi,KayitYapanKisi=@KayitYapanKisi,urunfoto=@foto,Barkodfoto=@Barkodfoto where urunid=@id ", bag);
                        cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[9].Value);

                    }


                    cmd.Parameters.AddWithValue("@urunAdi", txtadi.Text);
                    cmd.Parameters.AddWithValue("@urunModeli", txturunModeli.Text);
                    cmd.Parameters.AddWithValue("@urunBarkodu", txturunBarkodu.Text);
                    cmd.Parameters.AddWithValue("@urunAdedi", txturunAdedi.Text);
                    cmd.Parameters.AddWithValue("@stokTarihi", txtstokTarihi.Text);
                    cmd.Parameters.AddWithValue("@KayitYapanKisi", txtKayitYapanKisi.Text);
                    cmd.Parameters.AddWithValue("@foto", openFileDialog1.SafeFileName);
                    cmd.Parameters.AddWithValue("@Barkodfoto", openFileDialog2.SafeFileName);
                    cmd.ExecuteNonQuery();

                    gridDoldur();
                    bag.Close();
                    MessageBox.Show("Ürün Kayıt Edilmiş ve Düzenlenmiştir");
                    modGezinti();
                }
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            modGezinti();
            DialogResult secim = MessageBox.Show("Kayıt Silinecek. Emin Misiniz?", "UYARI!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (secim == DialogResult.Yes)
            {
                Bagla();
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("delete from urun where urunid=@id", bag);
                cmd.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[9].Value);
                cmd.ExecuteNonQuery();
                gridDoldur();
                bag.Close();
                MessageBox.Show("kayıt Silinmiştir");
            }
        }


        private void button10_Click(object sender, EventArgs e)
        {
            Menu frm = new Menu();
            this.Close();
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
                alisveris.frmStoklar formkapa = new alisveris.frmStoklar();
                formkapa.Close();
                Menu form = new Menu();
                form.Show();
                this.Hide();
            }
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

        private void txturunAdedi_KeyPress(object sender, KeyPressEventArgs e)
        {


            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        

        private void txturunBarkodu_KeyPress(object sender, KeyPressEventArgs e)
        {

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void txtadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void txturunModeli_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }

        private void frmStoklar_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            
        }

       
    }
}
