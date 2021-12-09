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
    public partial class StokHaraketleri : Form
    {
        public StokHaraketleri()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommand cmd;
        DataSet ds;

        void griddoldur()
        {
            con = new SqlConnection("Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=urun;Integrated Security=True");
            da = new SqlDataAdapter("Select urunAdi,urunModeli, urunBarkodu,urunAdedi,stokTarihi,KayitYapanKisi, urunfoto ,Barkodfoto From urun", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "urun");
            dataGridView1.DataSource = ds.Tables["urun"];
            con.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            griddoldur();
        }
        private void StokHaraketleri_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

       
        private void button1_Click(object sender, EventArgs e)
        {
            Menu frm = new Menu();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from urun where urunAdi like '%" + textBox1.Text + "%'", con);
            ara.Fill(tbl);
            con.Close();
            dataGridView1.DataSource = tbl;
        }

        private void StokHaraketleri_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
