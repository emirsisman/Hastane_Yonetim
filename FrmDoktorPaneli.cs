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
    public partial class FrmDoktorPaneli: Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        SqlBaglanti sql = new SqlBaglanti();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Doktorlar", sql.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşları Çekme
            SqlCommand command = new SqlCommand("Select BransAd From Branşlar", sql.baglanti());
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                cmbBrans.Items.Add(dr[0]);
            }
            sql.baglanti().Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert INTO Doktorlar (DoktorAd, DoktorSoyad, DoktorBrans, DoktorTC, DoktorSifre, DoktorCinsiyet)" +
                "Values(@p1, @p2, @p3, @p4, @p5, @p6)",sql.baglanti());
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", cmbBrans.Text);
            command.Parameters.AddWithValue("@p4", maskTxtTC.Text);
            command.Parameters.AddWithValue("@p5", txtSifre.Text);
            command.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Doktor Eklendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Doktorlar", sql.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete From Doktorlar Where DoktorTC=@p1", sql.baglanti());
            command.Parameters.AddWithValue("@p1", maskTxtTC.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Doktor Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Doktorlar", sql.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Doktorlar SET DoktorAd=@p1, DoktorSoyad=@p2, DoktorBrans=@p3," +
                "DoktorTC=@p4, DoktorSifre=@p5, DoktorCinsiyet=@p6 Where DoktorTC=@p4", sql.baglanti());
            command.Parameters.AddWithValue("@p1", txtAd.Text);
            command.Parameters.AddWithValue("@p2", txtSoyad.Text);
            command.Parameters.AddWithValue("@p3", cmbBrans.Text);
            command.Parameters.AddWithValue("@p4", maskTxtTC.Text);
            command.Parameters.AddWithValue("@p5", txtSifre.Text);
            command.Parameters.AddWithValue("@p6", cmbCinsiyet.Text);
            
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Doktor Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Doktorlar", sql.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbBrans.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            maskTxtTC.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txtSifre.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            cmbCinsiyet.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void maskTxtTC_MouseClick(object sender, MouseEventArgs e)
        {
            maskTxtTC.SelectionStart = 0;
            maskTxtTC.SelectionLength = 0;
        }
    }
}
