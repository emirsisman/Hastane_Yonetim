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
    public partial class FrmBransPaneli: Form
    {
        public FrmBransPaneli()
        {
            InitializeComponent();
        }

        SqlBaglanti sql = new SqlBaglanti();
        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Branşlar", sql.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Insert Into Branşlar (BransAd) Values(@p1)", sql.baglanti());
            command.Parameters.AddWithValue("@p1", txtBransAd.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Kaydınız yapıldı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Branşlar", sql.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Delete From Branşlar Where BransId=@p1", sql.baglanti());
            command.Parameters.AddWithValue("@p1", txtBransId.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Kaydınız Silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Branşlar SET BransAd=@p1 where BransId=@p2", sql.baglanti());
            command.Parameters.AddWithValue("@p1", txtBransAd.Text);
            command.Parameters.AddWithValue("@p2", txtBransId.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Kaydınız Güncellendi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtBransAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }
    }
}
