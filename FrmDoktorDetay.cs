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
    public partial class FrmDoktorDetay: Form
    {
        public FrmDoktorDetay()
        {
            InitializeComponent();
        }
        public string tc;
        public string brans;
        SqlBaglanti sql = new SqlBaglanti();

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            lblDoktorTc.Text = tc;
            lblBrans.Text = brans;
            SqlCommand command = new SqlCommand("Select DoktorAd, DoktorSoyad From Doktorlar Where DoktorTc=@p1", sql.baglanti());
            command.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblDoktorAdSoyad.Text = (dr[0] + " " + dr[1]);
            }
            sql.baglanti().Close();

            SqlCommand command1 = new SqlCommand("Select DoktorBrans From Doktorlar Where DoktorTC=@p1", sql.baglanti());
            command1.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                lblBrans.Text = (dr1[0].ToString());
            }

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Randevular Where RandevuDoktor='"+lblDoktorAdSoyad.Text+"'", sql.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            richTxtSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle frm = new FrmDoktorBilgiDuzenle();
            frm.tc = lblDoktorTc.Text;
            frm.Show();
        }

        private void btnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

        private void btnCıkıs_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris frm = new FrmDoktorGiris();
            frm.Show();
            this.Hide();
        }
    }
}
