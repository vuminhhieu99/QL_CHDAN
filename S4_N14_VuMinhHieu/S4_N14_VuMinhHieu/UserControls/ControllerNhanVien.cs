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
    public class ControllerNhanVien: Controller
    {
        

        public ControllerNhanVien():base()
        {
            ;
            
        }
        
        public DataTable Search(string sqlselect)
        {
            DataTable dt = new DataTable();
            try
            {                
                string query = "SELECT* FROM DBO.NhanVien" + sqlselect;
                adapter.SelectCommand = new SqlCommand(query, connection);
                ds.Clear();
                adapter.Fill(ds, "NhanVien");
                dt = ds.Tables["NhanVien"];                
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
                adapter.Update(ds, "NhanVien");
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

        public override DataTable Load()
        {
            string query = "SELECT * FROM DBO.NhanVien";
            SqlCommand command = new SqlCommand(query, connection);

            adapter.SelectCommand = command;
            cb = new SqlCommandBuilder(adapter);
            adapter.Fill(ds, "NhanVien");

            DataTable dt = ds.Tables["NhanVien"];
            connection.Close();
            return dt;
        }
    }
}
