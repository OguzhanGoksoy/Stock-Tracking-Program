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
    public partial class yonetmenu : Form
    {
        public yonetmenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Kullanıcı_ekle fmust = new Kullanıcı_ekle();
            fmust.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            plasiyergelenpara fmust = new plasiyergelenpara();
            fmust.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
          
            StokHaraketleri fmust = new StokHaraketleri();
            fmust.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void yonetmenu_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Çıkış Yapılacaktır. Emin Misiniz", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.No)
            {
                e.Cancel = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            satılanurun fmust = new satılanurun();
            fmust.ShowDialog();
            
        }

        private void yonetmenu_Load(object sender, EventArgs e)
        {

        }
    }
}
