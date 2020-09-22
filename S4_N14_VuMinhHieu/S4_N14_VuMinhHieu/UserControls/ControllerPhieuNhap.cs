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
    class ControllerPhieuNhap : Controller
    {        
        public SqlDataAdapter adapterDPN;
        public DataSet dsDPN;
        public SqlCommandBuilder cbDPN;

        public ControllerPhieuNhap() : base()
        {
            dsDPN = new DataSet();
            adapterDPN = new SqlDataAdapter();
        }

        public override DataTable Load()
        {
            string query = "SELECT * FROM DBO.PhieuNhap";
            SqlCommand command = new SqlCommand(query, connection);

            adapter.SelectCommand = command;
            cb = new SqlCommandBuilder(adapter);
            adapter.Fill(ds, "PhieuNhap");

            DataTable dt = ds.Tables["PhieuNhap"];
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
                adapter.Update(ds, "PhieuNhap");
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

        public void UpdateDPN()
        {
            try
            {
                adapterDPN.Update(dsDPN, "DongPhieuNhap");
                cbDPN.RefreshSchema();
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
                string query = "SELECT* FROM DBO.PhieuNhap" + sqlselect;
                adapter.SelectCommand = new SqlCommand(query, connection);
                ds.Clear();
                adapter.Fill(ds, "PhieuNhap");
                dt = ds.Tables["PhieuNhap"];
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

        public DataTable LoadDongPhieuNhap(string MaPN)
        {
            string query = "SELECT * FROM DBO.DongPhieuNhap WHERE MaPhieuNhap = " + MaPN;
            SqlCommand _command = new SqlCommand(query, connection);

            dsDPN.Clear();
            adapterDPN.SelectCommand = _command;
            cbDPN = new SqlCommandBuilder(adapterDPN);
            adapterDPN.Fill(dsDPN, "DongPhieuNhap");

            DataTable dt = dsDPN.Tables["DongPhieuNhap"];
            connection.Close();
            return dt;

        }

    }
}
