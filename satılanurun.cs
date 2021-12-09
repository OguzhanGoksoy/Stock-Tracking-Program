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
    public partial class satılanurun : Form
    {
        public satılanurun()
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
            da = new SqlDataAdapter("Select *From cariurun ", con);
            ds = new DataSet();
            con.Open();
            da.Fill(ds, "urun");
            dataGridView1.DataSource = ds.Tables["urun"];
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            yonetmenu frm = new yonetmenu();
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from cariurun  where cariAdi like '%" + textBox1.Text + "%'", con);
            ara.Fill(tbl);
            con.Close();
            dataGridView1.DataSource = tbl;
        }

        private void satılanurun_Load(object sender, EventArgs e)
        {
            griddoldur();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from cariurun  where urunAdi like '%" + textBox3.Text + "%'", con);
            ara.Fill(tbl);
            con.Close();
            dataGridView1.DataSource = tbl;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            DataTable tbl = new DataTable();
            SqlDataAdapter ara = new SqlDataAdapter("Select * from cariurun  where urunSatısTarihi like '%" + textBox2.Text + "%'", con);
            ara.Fill(tbl);
            con.Close();
            dataGridView1.DataSource = tbl;
        }

        private void satılanurun_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
