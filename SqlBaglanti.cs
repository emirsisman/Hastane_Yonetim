using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Hastane_Yonetim
{
    class SqlBaglanti
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection("Data Source=DESKTOP-6VAF54B; Initial Catalog=HastaneVT; Integrated Security=True");
            baglan.Open();
            return baglan;
        }
    }
}
