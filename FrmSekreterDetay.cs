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
    public partial class FrmSekreterDetay: Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public string tc;
        SqlBaglanti sql = new SqlBaglanti();


        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {

            lblSekreterTc.Text = tc;
            SqlCommand command = new SqlCommand("Select SekreterAd, SekreterSoyad From Sekreterler Where SekreterTC=@p1", sql.baglanti());
            command.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblSekreterAdSoyad.Text = (dr[0] + " " + dr[1]);
            }
            sql.baglanti().Close();


            //Branşları DataGride Aktarma
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select BransAd From Branşlar", sql.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşları Combobox'a Aktarma
            SqlCommand command1 = new SqlCommand("Select BransAd From Branşlar", sql.baglanti());
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                cmbBrans.Items.Add(dr1[0]);
            }
            sql.baglanti().Close();

            //Doktorları DataGride Aktarma
            DataTable dt1 = new DataTable();
            SqlDataAdapter adapter1 = new SqlDataAdapter("Select (DoktorAd + ' ' +DoktorSoyad) AS DoktorAdSoyad, DoktorBrans From Doktorlar", sql.baglanti());
            adapter1.Fill(dt1);
            dataGridView2.DataSource = dt1;


        }

        private void btnRandevuKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert into Randevular (RandevuTarihi, RandevuSaat, RandevuBrans, RandevuDoktor) " +
                "Values(@p1, @p2, @p3, @p4)", sql.baglanti());
            command.Parameters.AddWithValue("@p1", maskTxtTarih.Text);
            command.Parameters.AddWithValue("@p2", maskTxtSaat.Text);
            command.Parameters.AddWithValue("@p3", cmbBrans.Text);
            command.Parameters.AddWithValue("@p4", cmbDoktor.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();
            SqlCommand command = new SqlCommand("Select DoktorAd, DoktorSoyad From Doktorlar Where DoktorBrans=@p1", sql.baglanti());
            command.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                cmbDoktor.Items.Add(dr[0] + " " + dr[1]);
            }
            sql.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert INTO Duyurular (Duyuru) VALUES (@p1)", sql.baglanti());
            command.Parameters.AddWithValue("@p1", richTextBox1.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnRandevuGüncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Randevular SET RandevuTarihi=@p1, RandevuSaat=@p2, RandevuBrans=@p3, " +
                "RandevuDoktor=@p4, RandevuDurum=@p5, HastaTC=@p6 Where RandevuId=@p7", sql.baglanti());
            command.Parameters.AddWithValue("@p1", maskTxtTarih.Text);
            command.Parameters.AddWithValue("@p2", maskTxtSaat.Text);
            command.Parameters.AddWithValue("@p3", cmbBrans.Text);
            command.Parameters.AddWithValue("@p4", cmbDoktor.Text);
            command.Parameters.AddWithValue("@p5", chkDurum.Text);
            command.Parameters.AddWithValue("@p6", maskTxtTc.Text);
            command.Parameters.AddWithValue("@p7", txtId.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Randevu Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnDoktorForm_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frm = new FrmDoktorPaneli();
            frm.Show();
        }

        private void btnRandevuForm_Click(object sender, EventArgs e)
        {
            FrmRandevuPaneli frm = new FrmRandevuPaneli();
            frm.Show();
        }

        private void btnBransForm_Click(object sender, EventArgs e)
        {
            FrmBransPaneli frm = new FrmBransPaneli();
            frm.Show();
        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm = new FrmDuyurular();
            frm.Show();
        }

    }
}
