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
    public partial class urunekle : Form
    {


        public urunekle()
        {
            InitializeComponent();
        }
        public  string s1, s2, s3, s4, s5, s9;
        public bool ok = false;
         

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

            carialma.DataSource = ds.Tables[0];

            bag.Close();
        }
        private void urunekle_Load(object sender, EventArgs e)
        {
            Bagla();

            gridDoldur();
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
            this.Hide();
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

        private void carialma_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            s1 = carialma.CurrentRow.Cells[0].Value.ToString();
            s2 = carialma.CurrentRow.Cells[1].Value.ToString();
            s3 = carialma.CurrentRow.Cells[2].Value.ToString();
            s4 = carialma.CurrentRow.Cells[3].Value.ToString();
            s5 = carialma.CurrentRow.Cells[5].Value.ToString();
            s9 = carialma.CurrentRow.Cells[9].Value.ToString();
            ok = true;
            this.Close();
             
        }
    }
}
