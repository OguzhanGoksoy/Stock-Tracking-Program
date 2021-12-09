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
using System.Data.Sql;




namespace alisveris
{
    public partial class giristakip : Form
    {
        public giristakip()
        {
            InitializeComponent();
        }

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;


        void bosalt()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            string user = textBox1.Text;
            string pass = textBox2.Text;
            con = new SqlConnection("Data Source=IBRAHIMGOKSOY\\SQLEXPRESS;Initial Catalog=urun;Integrated Security=True");
            cmd = new SqlCommand();
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Uye where KullaniciAdi='" + textBox1.Text + "' AND KullaniciSifresi='" + textBox2.Text + "'";
            dr = cmd.ExecuteReader();
            bool success = dr.Read(); dr.Close(); 
            if (success)
            {
                bosalt();

                this.Hide();
                alisveris.Menu fmust = new alisveris.Menu();
                fmust.ShowDialog();
                this.Show();
                con.Close(); return;

               
            }
           

           

            cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM yonet where YonetAdi='" + textBox1.Text + "' AND YonetSifre='" + textBox2.Text + "'";
            dr = cmd.ExecuteReader();
            success = dr.Read(); dr.Close(); con.Close(); bosalt();
            if (success)
            {
                this.Hide();
                alisveris.yonetmenu fmust = new alisveris.yonetmenu();
                fmust.ShowDialog();
                this.Show();
               
            }
                 
            else
            {
                bosalt();
            }
            bosalt();
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

        private void btncıkıs_Click(object sender, EventArgs e)
        {
            Application.Exit();
            this.Close();
        }

        private void giristakip_Load(object sender, EventArgs e)
        {
            

        }
    }
}

