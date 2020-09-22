using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S4_N14_VuMinhHieu.UserControls
{
    public class ControllerNCC : Controller
    {

        public ControllerNCC(): base()
        {

        }
        public override DataTable Load()
        {
            string query = "SELECT * FROM DBO.NhaCungCap";
            SqlCommand command = new SqlCommand(query, connection);

            adapter.SelectCommand = command;
            cb = new SqlCommandBuilder(adapter);
            adapter.Fill(ds, "NhaCungCap");

            DataTable dt = ds.Tables["NhaCungCap"];
            connection.Close();
            return dt;
        }

        public DataTable Search(string sqlselect)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT* FROM DBO.NhaCungCap" + sqlselect;
                adapter.SelectCommand = new SqlCommand(query, connection);
                ds.Clear();
                adapter.Fill(ds, "NhaCungCap");
                dt = ds.Tables["NhaCungCap"];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return dt;

        }
        public void Update()
        {
            try
            {
             
               
                adapter.Update(ds,"NhaCungCap");
                
                cb.RefreshSchema();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cập nhật thất bại");
            }
            finally
            {
                connection.Close();
            }
        }

        public void Update(string query)
        {
            using (var command = new SqlCommand { Connection = connection })
            {
                connection.Open();
                command.CommandText = query;
                var count = command.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("cập nhật thành công", "thông báo", MessageBoxButtons.OK);
                }
                connection.Close();
            }

        }
        public void Insert(string query)
        {
            using (var command = new SqlCommand { Connection = connection })
            {
                connection.Open();
                command.CommandText = query;
                var count = command.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("cập nhật thành công", "thông báo", MessageBoxButtons.OK);
                }
                connection.Close();
            }
        }

        public DataTable LoadCungUng(string MaNCC)
        {
            string query = "SELECT * FROM DBO.CungUng WHERE MaNCC = "+ MaNCC;
            SqlCommand command1 = new SqlCommand(query, connection);
            SqlDataAdapter adapterCU = new SqlDataAdapter();
            adapterCU.SelectCommand = command1;
          
            DataTable dt = new DataTable();          
            adapterCU.Fill(dt);           
            connection.Close();
            return dt;
        }

        public DataTable LoadPhieuNhap(string MaNCC)
        {
            string query = "SELECT * FROM DBO.PhieuNhap WHERE MaNCC = " + MaNCC ;            
            SqlCommand command1 = new SqlCommand(query, connection);
            SqlDataAdapter adapterCU = new SqlDataAdapter();
            adapterCU.SelectCommand = command1;

            DataTable dt = new DataTable();
            adapterCU.Fill(dt);
            connection.Close();
            return dt;
        }
    }
}
