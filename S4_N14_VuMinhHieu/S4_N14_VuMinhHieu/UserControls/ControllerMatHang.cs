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
    class ControllerMatHang : Controller
    {
        
        public SqlDataAdapter adapterLH;
        public SqlDataAdapter adapterCU;
        public DataSet dsLH;
        public DataSet dsCU;
        public SqlCommandBuilder cbLH;
        public SqlCommandBuilder cbCU;

        public ControllerMatHang() : base()
        {
            dsLH = new DataSet();
            dsCU = new DataSet();
            adapterLH = new SqlDataAdapter();
            adapterCU = new SqlDataAdapter();
        }

        public override DataTable Load()
        {
            string query = "SELECT * FROM DBO.MatHang";
            SqlCommand command = new SqlCommand(query, connection);

            adapter.SelectCommand = command;
            cb = new SqlCommandBuilder(adapter);
            adapter.Fill(ds, "MatHang");

            DataTable dt = ds.Tables["MatHang"];
            connection.Close();
            return dt;
        }

        public DataTable LoadLoaiHang()
        {
            string query = "SELECT * FROM DBO.LoaiHang";
            SqlCommand _command = new SqlCommand(query, connection);
            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;

            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            connection.Close();
            return dt;
        }

        public DataTable LoadDPN()
        {
            string query = "SELECT MaDPN FROM DBO.DongPhieuNhap";
            SqlCommand _command = new SqlCommand(query, connection);
            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;

            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            connection.Close();
            return dt;
        }

        public DataTable LoadLoHang(string MaHang)
        {
            string query = "SELECT * FROM DBO.LoHang WHERE MaHang = " + MaHang;
            SqlCommand _command = new SqlCommand(query, connection);

            dsLH.Clear();
            adapterLH.SelectCommand = _command;
            cbLH = new SqlCommandBuilder(adapterLH);
            adapterLH.Fill(dsLH, "LoHang");

            DataTable dt = dsLH.Tables["LoHang"];
            connection.Close();
            return dt;

        }

        public DataTable LoadCungUng(string MaHang)
        {
            string query = "SELECT * FROM DBO.CungUng WHERE MaHang = " + MaHang;
            SqlCommand _command = new SqlCommand(query, connection);

            dsCU.Clear();
            adapterCU.SelectCommand = _command;
            cbCU = new SqlCommandBuilder(adapterCU);
            adapterCU.Fill(dsCU, "CungUng");

            DataTable dt = dsCU.Tables["CungUng"];
            connection.Close();
            return dt;

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

        public void Update()
        {
            try
            {
                adapter.Update(ds, "MatHang");
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

        public void UpdateLH()
        {
            try
            {
                adapterLH.Update(dsLH, "LoHang");
                cbLH.RefreshSchema();
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

        public void UpdateCU()
        {
            try
            {
                adapterCU.Update(dsCU, "CungUng");
                cbCU.RefreshSchema();
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

        public DataTable Search(string sqlselect)
        {
            DataTable dt = new DataTable();
            try
            {
                string query = "SELECT* FROM DBO.MatHang" + sqlselect;
                adapter.SelectCommand = new SqlCommand(query, connection);
                ds.Clear();
                adapter.Fill(ds, "MatHang");
                dt = ds.Tables["MatHang"];
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
    }
}
