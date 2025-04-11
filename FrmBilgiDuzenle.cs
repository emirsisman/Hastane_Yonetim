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
    public partial class FrmBilgiDuzenle: Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }

        SqlBaglanti sql = new SqlBaglanti();
        public string tcNo;
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            maskTC.Text = tcNo;
            SqlCommand command = new SqlCommand("Select * From Hastalar Where HastaTC=@p1", sql.baglanti());
            command.Parameters.AddWithValue("@p1", tcNo);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                maskTelefon.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                cmbCinsiyet.Text = dr[6].ToString();
            }
        }

        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Hastalar SET HastaAd=@p1, HastaSoyad=@p2,  " +
                "HastaTelefon= @p3, HastaSifre= @p4, HastaCinsiyet= @p5 where HastaTC=@p6,", sql.baglanti());
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", maskTelefon.Text);
            command.Parameters.AddWithValue("@p4", txtSifre.Text);
            command.Parameters.AddWithValue("@p5", cmbCinsiyet.Text);
            command.Parameters.AddWithValue("@p6", maskTC.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();

            MessageBox.Show("Bilgileriniz Güncellenmiştir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkSifreGoster_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSifreGoster.Checked)
            {
                txtSifre.UseSystemPasswordChar = false; // Şifreyi göster
            }
            else
            {
                txtSifre.UseSystemPasswordChar = true; // Şifreyi gizle
            }
        }
    }
}
