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
    public partial class FrmDuyurular: Form
    {
        public FrmDuyurular()
        {
            InitializeComponent();
        }
        SqlBaglanti sql = new SqlBaglanti();
        private void FrmDuyurular_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Duyurular", sql.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
