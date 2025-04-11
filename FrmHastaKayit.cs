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

namespace Hastane_Yonetim
{
    public partial class FrmHastaKayit: Form
    {
        SqlBaglanti sql = new SqlBaglanti();
        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        private void btnKayit_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert Into Hastalar(HastaAd, HastaSoyad, HastaTC, HastaTelefon, HastaSifre, HastaCinsiyet) " +
                "Values (@p1, @p2, @p3, @p4, @p5, @p6)", sql.baglanti());
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", maskTC.Text);
            command.Parameters.AddWithValue("@p4", maskTelefon.Text);
            command.Parameters.AddWithValue("@p5", txtSifre.Text);
            command.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Kaydınız yapıldı.", "Bilgi",  MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
