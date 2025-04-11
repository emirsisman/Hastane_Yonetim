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
    public partial class FrmSekreterGiris: Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        SqlBaglanti sql = new SqlBaglanti();
        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From Sekreterler Where SekreterTc=@p1 and SekreterSifre=@p2", sql.baglanti());
            command.Parameters.AddWithValue("@p1", maskTC.Text);
            command.Parameters.AddWithValue("@p2", txtSifre.Text);
            SqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                FrmSekreterDetay frm = new FrmSekreterDetay();
                frm.tc = maskTC.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC veya Şifre Girdiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
