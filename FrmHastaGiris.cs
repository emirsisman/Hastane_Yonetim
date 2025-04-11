﻿using System;
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
    public partial class FrmHastaGiris: Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }
        SqlBaglanti sql = new SqlBaglanti();
        private void lnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit frm = new FrmHastaKayit();
            frm.Show();

        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From Hastalar Where HastaTC = @p1 AND HastaSifre = @p2", sql.baglanti());
            command.Parameters.AddWithValue("@p1", maskTC.Text);
            command.Parameters.AddWithValue("@p2", txtSifre.Text);

            SqlDataReader dr = command.ExecuteReader();
            if (dr.Read())
            {
                FrmHastaDetay frm = new FrmHastaDetay();
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
