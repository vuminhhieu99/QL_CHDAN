using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S4_N14_VuMinhHieu.UserControls
{
    public class ControllerNghiepVu : Controller
    {
        public ControllerNghiepVu(): base()
        {
            ;
        }
        public DataTable Load(int ma_loai, string dau_ky, string cuoi_ky)
        {
            connection.Open();
            
            SqlCommand command = new SqlCommand("SELECT * FROM BaoCao_TonKho("+ma_loai+ ", '"+dau_ky+"', '" +cuoi_ky+"')", connection);
            adapter.SelectCommand = command;
            cb = new SqlCommandBuilder(adapter);

            adapter.Fill(ds, "BaoCao");
            DataTable dt = ds.Tables["BaoCao"];
            connection.Close();
            return dt;
        }

        public override DataTable Load()
        {
            throw new NotImplementedException();
        }

        public DataTable LoadLoaiHang()
        {
            
            string query = "SELECT * FROM DBO.LoaiHang";
            SqlCommand command = new SqlCommand(query, connection);

            adapter.SelectCommand = command;
            cb = new SqlCommandBuilder(adapter);
            adapter.Fill(ds, "LoaiHang");

            DataTable dt = ds.Tables["LoaiHang"];
            connection.Close();
            return dt;
        }
    }
}
