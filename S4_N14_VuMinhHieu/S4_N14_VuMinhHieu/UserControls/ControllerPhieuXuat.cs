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
    class ControllerPhieuXuat : Controller
    {
        public SqlDataAdapter adapterDPX;
        public DataSet dsDPX;
        public SqlCommandBuilder cbDPX;

        public ControllerPhieuXuat() : base()
        {
            dsDPX = new DataSet();
            adapterDPX = new SqlDataAdapter();
        }

        public override DataTable Load()
        {
            string query = "SELECT * FROM DBO.PhieuXuat";
            SqlCommand command = new SqlCommand(query, connection);

            adapter.SelectCommand = command;
            cb = new SqlCommandBuilder(adapter);
            adapter.Fill(ds, "PhieuXuat");

            DataTable dt = ds.Tables["PhieuXuat"];
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
                adapter.Update(ds, "PhieuXuat");
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

        public void UpdateDPX()
        {
            try
            {
                adapterDPX.Update(dsDPX, "DongPhieuXuat");
                cbDPX.RefreshSchema();
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

        public void UpdateDPX(string query)
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
                string query = "SELECT* FROM DBO.PhieuXuat" + sqlselect;
                adapter.SelectCommand = new SqlCommand(query, connection);
                ds.Clear();
                adapter.Fill(ds, "PhieuXuat");
                dt = ds.Tables["PhieuXuat"];
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

        public DataTable LoadMatHang()
        {
            string query = "SELECT * FROM DBO.MatHang";
            SqlCommand _command = new SqlCommand(query, connection);
            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;

            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            connection.Close();
            return dt;
        }

        public DataTable LoadDongPhieuXuat(string MaPN)
        {
            string query = "SELECT * FROM DBO.DongPhieuXuat WHERE MaPhieuXuat = " + MaPN;
            SqlCommand _command = new SqlCommand(query, connection);

            dsDPX.Clear();
            adapterDPX.SelectCommand = _command;
            cbDPX = new SqlCommandBuilder(adapterDPX);
            adapterDPX.Fill(dsDPX, "DongPhieuXuat");

            DataTable dt = dsDPX.Tables["DongPhieuXuat"];
            connection.Close();
            return dt;

        }

        public DataTable LoadLoHang(string MaHang)
        {
            string query = "SELECT * FROM DBO.LoHang WHERE MaHang = " + MaHang;
            SqlCommand _command = new SqlCommand(query, connection);
            SqlDataAdapter _adapter = new SqlDataAdapter();
            _adapter.SelectCommand = _command;
            DataTable dt = new DataTable();
            _adapter.Fill(dt);
            connection.Close();
            return dt;
        }
    }
}