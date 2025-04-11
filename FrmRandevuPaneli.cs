using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Yonetim
{
    public partial class FrmRandevuPaneli: Form
    {
        public FrmRandevuPaneli()
        {
            InitializeComponent();
        }
        SqlBaglanti sql = new SqlBaglanti();


        private void FrmRandevuPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select * From Randevular", sql.baglanti());
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        
    }
}
