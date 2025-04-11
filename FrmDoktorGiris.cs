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
    public partial class FrmDoktorGiris: Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        SqlBaglanti sql = new SqlBaglanti();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From Doktorlar Where DoktorTc=@p1 and DoktorSifre=@p2", sql.baglanti());
            command.Parameters.AddWithValue("@p1", maskTC.Text);
            command.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                FrmDoktorDetay frm = new FrmDoktorDetay();
                frm.tc = maskTC.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre Girdiniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            sql.baglanti().Close();
        }
    }
}
