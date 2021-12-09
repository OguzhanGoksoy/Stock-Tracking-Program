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
    public partial class kullanıcı_ekle2 : Form
    {
        public kullanıcı_ekle2()
        {
            InitializeComponent();
        }

        public bool ok = false;
        public string a1, a2, a3, a4;

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

            dad = new SqlDataAdapter("Select * from cari", bag);

            dad.Fill(ds);

            carialma.DataSource = ds.Tables[0];

            bag.Close();
        }

        private void kullanıcı_ekle2_Load(object sender, EventArgs e)
        {
            Bagla();

            gridDoldur();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            bag.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from cari where cariAdi like '%" + textBox1.Text + "%'", bag);
            ara.Fill(tbl);
            bag.Close();
            carialma.DataSource = tbl;
        }

        private void kullanıcı_ekle2_MouseClick(object sender, MouseEventArgs e)
        {
            this.Hide();

        }


        private void carialma_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            a1 = carialma.CurrentRow.Cells[0].Value.ToString();
            a2 = carialma.CurrentRow.Cells[1].Value.ToString();

            ok = true;
            this.Close();
        }
    }
}
