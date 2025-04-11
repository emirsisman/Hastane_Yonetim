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
    public partial class FrmHastaDetay: Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        SqlBaglanti sql = new SqlBaglanti();
        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            lblTc.Text = tc;
            SqlCommand command = new SqlCommand("Select HastaAd, HastaSoyad From Hastalar Where HastaTC=@p1", sql.baglanti());
            command.Parameters.AddWithValue("@p1", tc);
            SqlDataReader dr = command.ExecuteReader();
            while (dr.Read())
            {
                lblAdSoyad.Text = (dr[0] + " " + dr[1]);
            }

            sql.baglanti().Close();

            //Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Randevular Where HastaTC=" + tc, sql.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşları çekme
            SqlCommand command1 = new SqlCommand("Select BransAd From Branşlar",sql.baglanti());
            SqlDataReader dr1 = command1.ExecuteReader();
            while (dr1.Read())
            {
                cmbBrans.Items.Add(dr1[0]);
            }

            sql.baglanti().Close();
            
        }

        private void cmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDoktor.Items.Clear();

            SqlCommand command2 = new SqlCommand("Select DoktorAd, DoktorSoyad From Doktorlar Where DoktorBrans=@p1", sql.baglanti());
            command2.Parameters.AddWithValue("@p1", cmbBrans.Text);
            SqlDataReader dr2 = command2.ExecuteReader();
            while (dr2.Read())
            {
                cmbDoktor.Items.Add(dr2[0] + " " + dr2[1]);
            }

            sql.baglanti().Close();
        }

        private void cmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Randevular Where RandevuBrans='" + cmbBrans.Text + "'" + " AND RandevuDoktor='"+cmbDoktor.Text+ "' AND RandevuDurum=0", sql.baglanti());
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void lnklabelBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmBilgiDuzenle frm = new FrmBilgiDuzenle();
            frm.tcNo = lblTc.Text;
            frm.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtId.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Update Randevular SET RandevuDurum=1, HastaTC=@p1, HastaSikayet=@p2 Where RandevuId=@p3", sql.baglanti());
            command.Parameters.AddWithValue("@p1", lblTc.Text);
            command.Parameters.AddWithValue("@p2", richTxtSikayet.Text);
            command.Parameters.AddWithValue("@p3", txtId.Text);
            command.ExecuteNonQuery();
            sql.baglanti().Close();
            MessageBox.Show("Randevu Alındı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Randevular Where HastaTC=" + tc, sql.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
