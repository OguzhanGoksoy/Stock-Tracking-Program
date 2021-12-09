using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace alisveris
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

          
            frmStoklar fmust = new frmStoklar();
            fmust.ShowDialog();
       
        }

        private void button2_Click(object sender, EventArgs e)
        {

            
            satis_1 fmust = new satis_1();
            fmust.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }

        private void Menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
        
            

            
            StokHaraketleri fmust = new StokHaraketleri();
            fmust.ShowDialog();
            
            
        }

        private void cariEkleBtn_Click(object sender, EventArgs e)
        {
           
            carieklem fmust = new carieklem();
            fmust.ShowDialog();
           
           
        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }
    }
}
