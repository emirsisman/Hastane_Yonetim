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
    public partial class FrmDoktorBilgiDuzenle: Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        SqlBaglanti sql = new SqlBaglanti();
        public string tc;
        private void btnBilgiGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Doktorlar SET DoktorAd=@p1, DoktorSoyad=@p2, DoktorBrans=@p3," +
                "DoktorTC=@p4, DoktorSifre=@p5 Where DoktorTC=@p4", sql.baglanti());
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", cmbBrans.Text);
            command.Parameters.AddWithValue("@p4", maskTC.Text);
            command.Parameters.AddWithValue("@p5", txtSifre.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Bilgiler Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            maskTC.Text = tc;
            SqlCommand command = new SqlCommand("Select * From Doktorlar Where DoktorTC=@p1", sql.baglanti());
            command.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cmbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[5].ToString();
                
            }
            sql.baglanti().Close();

            //Branşları Çek
            SqlCommand command1 = new SqlCommand("Select * From Branşlar", sql.baglanti());
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                cmbBrans.Items.Add(dr1[1]);
            }
            sql.baglanti().Close();
        }
    }
}
