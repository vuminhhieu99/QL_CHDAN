using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S4_N14_VuMinhHieu.UserControls
{
    public partial class UCPhieuXuat : UserControl
    {
        ControllerPhieuXuat ctrl = new ControllerPhieuXuat();
        DataTable dt = new DataTable();
        DataTable dtDPX = new DataTable();

        bool addPX;
        bool addDPX;

        string MaPhieuXuat;    

        public UCPhieuXuat()
        {
            InitializeComponent();
            MaPhieuXuat = "";
        }

        private void UCPhieuXuat_Load(object sender, EventArgs e)
        {
            dt = ctrl.Load();
            dtgv_PhieuXuat.DataSource = dt;
            DataTable dt_mh = new DataTable();
            dt_mh = ctrl.LoadMatHang();
            cb_MatHang.DataSource = dt_mh;
            cb_MatHang.DisplayMember = dt_mh.Columns["TenHang"].ToString();
            cb_MatHang.ValueMember = dt_mh.Columns["MaHang"].ToString();
        }


        private void lb_resetData_Click(object sender, EventArgs e)
        {
            dt.Clear();
            dt = ctrl.Load();
            dtgv_PhieuXuat.DataSource = dt;
            dtDPX.Clear();
            reset();
        }

        private void bt_deletePX_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dtgv_PhieuXuat.SelectedRows)
                {
                    if (row.Cells[0].Value != null) // tranh truong hop delete final row
                    {
                        dtgv_PhieuXuat.Rows.Remove(row);
                    }
                }
                ctrl.Update();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bôi đen cả dòng rồi nhấn xóa xóa", "Cách xóa", MessageBoxButtons.OK);
            }
        }

        private void bt_updatePX_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl.Update();
                reset();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_addPX_Click(object sender, EventArgs e)
        {
            addPX = true;
            int row = dtgv_PhieuXuat.Rows.Count - 1;
            if (row < 0)
            {
                return;
            }
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);

            dtgv_PhieuXuat.ClearSelection();
            dtgv_PhieuXuat.Rows[row].Selected = true;
            reset();
        }

        private void dtgv_PhieuXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            addPX = false;
            int row = e.RowIndex;
            if (row < 0)
            {
                return;
            }
            tb_MaPX.Text = dtgv_PhieuXuat.Rows[row].Cells["txtMaPhieuXuat"].Value.ToString();
            MaPhieuXuat = tb_MaPX.Text;
            if (dtgv_PhieuXuat.Rows[row].Cells["txtThoigian"].Value.ToString() != "")
            {
                dtp_ThoiGianPX.Value = Convert.ToDateTime(dtgv_PhieuXuat.Rows[row].Cells["txtThoigian"].Value);
            }
            if (MaPhieuXuat != null && MaPhieuXuat != "")
            {
                ctrl.dsDPX.Clear();
                dtgv_DongPhieuXuat.DataSource = ctrl.LoadDongPhieuXuat(MaPhieuXuat);
            }
            else
            {
                addPX = true;
            }
            tb_MaNV.Text = dtgv_PhieuXuat.Rows[row].Cells["txtMaNV"].Value.ToString();
            tb_TenNguoiNhan.Text = dtgv_PhieuXuat.Rows[row].Cells["txtTenNguoiNhan"].Value.ToString();
            tb_DiaChiKho.Text = dtgv_PhieuXuat.Rows[row].Cells["txtDiaChiKho"].Value.ToString();
            tb_TongTien.Text = dtgv_PhieuXuat.Rows[row].Cells["txtTongTien"].Value.ToString();

            tb_MaPX_DPX.Text = MaPhieuXuat;
        }

        private void reset()
        {
            tb_MaPX.Text = "";
            tb_MaNV.Text = "";
            tb_TenNguoiNhan.Text = "";
            tb_DiaChiKho.Text = "";
            tb_TongTien.Text = "";

            cb_MaPX.Text = "";
            cb_Ngay.Text = "";
            cb_Thang.Text = "";
            cb_Nam.Text = "";
                        
            cb_MaNV.Text = "";
            tb_minTongTien.Text = "";
            tb_maxTongTien.Text = "";
        }

        private void bt_resetTimKiemNC_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void bt_resetDataDPX_Click(object sender, EventArgs e)
        {
            dtgv_DongPhieuXuat.DataSource = ctrl.LoadDongPhieuXuat(MaPhieuXuat);
            resetDPX();
        }

        private void bt_deleteDPX_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dtgv_DongPhieuXuat.SelectedRows)
                {
                    if (row.Cells[0].Value != null) // tranh truong hop delete final row
                    {
                        dtgv_DongPhieuXuat.Rows.Remove(row);
                    }
                }
                ctrl.UpdateDPX();
                resetDPX();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bôi đen cả dòng rồi nhấn xóa xóa", "Cách xóa", MessageBoxButtons.OK);
            }
        }

        private void bt_updateDPX_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl.UpdateDPX();
                resetDPX();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_addDPX_Click(object sender, EventArgs e)
        {
            addDPX = true;
            int row = dtgv_DongPhieuXuat.Rows.Count - 1;
            if (row < 0)
            {
                return;
            }
            DataRow newRow = dtDPX.NewRow();
            dtDPX.Rows.Add(newRow);
            dtgv_DongPhieuXuat.ClearSelection();
            dtgv_DongPhieuXuat.Rows[row].Selected = true;
            resetDPX();
        }

        private void bt_confirmPX_Click(object sender, EventArgs e)
        {
            var maPX = tb_MaPX.Text;
            var thoigianPX = "null"; if (dtp_ThoiGianPX.Value.ToString() != "") { thoigianPX = "'" + dtp_ThoiGianPX.Value.ToString() + "'"; }
            var maNV = "null"; if (tb_MaNV.Text != "") { maNV = "'" + tb_MaNV.Text + "'"; }
            var tenNguoiNhan = "null"; if (tb_TenNguoiNhan.Text != "") { tenNguoiNhan = "'" + tb_TenNguoiNhan.Text + "'"; }
            var diachikho = "null"; if (tb_DiaChiKho.Text != "") { diachikho = "'" + tb_DiaChiKho.Text + "'"; }
            var tongtienPX = tb_TongTien.Text; if (tongtienPX == "") { tongtienPX = "0"; }
            if (addPX == true)
            {
                try
                {
                    string query = "INSERT DBO.PhieuXuat(ThoiGian, MaNV, TenNguoiNhan, DiaChiKho, TongTien) VALUES ( " + thoigianPX + ", " + maNV + ", " + tenNguoiNhan + ", " + diachikho + ", " + tongtienPX + ")";
                    ctrl.Insert(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else // update
            {
                try
                {
                    string query = "Update dbo.PhieuXuat set ThoiGian = " + thoigianPX + ", MaNV = " + maNV + ", TenNguoiNhan = " + tenNguoiNhan + ", DiaChiKho = " + diachikho + ", TongTien = " + tongtienPX + " where MaPhieuXuat = " + maPX;
                    ctrl.Update(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            lb_resetData_Click(sender, e);
        }

        private void bt_confirmDPX_Click(object sender, EventArgs e)
        {
            var MaLo = cb_MaLo.Text;            
            var SoLuong = tb_SoLuong.Text; if (SoLuong == "") { SoLuong = "0"; }           
            if (addDPX == true)
            {
                try
                {
                    string query = "EXEC INSERT_DongPhieuXuat " + MaLo + ", " + MaPhieuXuat + ", " + SoLuong;
                    ctrl.Insert(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void cb_MaLo_Click(object sender, EventArgs e)
        {
            DataTable _dt = ctrl.LoadLoHang(cb_MatHang.SelectedValue.ToString());
            
            cb_MaLo.DataSource = _dt;
            cb_MaLo.DisplayMember = _dt.Columns["MaLo"].ToString();
        }

        private void cb_MaNV_Click(object sender, EventArgs e)
        {
            cb_MaNV.DataSource = dt;
            cb_MaNV.DisplayMember = dt.Columns["MaNV"].ToString();
        }

        private void cb_MaPX_Click(object sender, EventArgs e)
        {
            cb_MaPX.DataSource = dt;
            cb_MaPX.DisplayMember = dt.Columns["MaPhieuXuat"].ToString();
        }

        private void bt_search_Click(object sender, EventArgs e)
        {
            string sqlSelect = "";

            string MaPX = cb_MaPX.Text.Trim();
            string MaNV = cb_MaNV.Text.Trim();
            string Ngay = cb_Ngay.Text.Trim();
            string Thang = cb_Thang.Text.Trim();
            string Nam = cb_Nam.Text.Trim();          
            string MinTien = tb_minTongTien.Text.Trim();
            string MaxTien = tb_maxTongTien.Text.Trim();

            if (MaPX != "") { sqlSelect = sqlSelect + " and MaPhieNhap like '%" + MaPX + "%'"; }
            if (MaNV != "") { sqlSelect = sqlSelect + " and MaNV like N'%" + MaNV + "%'"; }
            if (Ngay != "") { sqlSelect = sqlSelect + " and DAY(ThoiGian) = " + Ngay; }
            if (Thang != "") { sqlSelect = sqlSelect + " and MONTH(ThoiGian) = " + Thang; }
            if (Nam != "") { sqlSelect = sqlSelect + " and YEAR(ThoiGian) = " + Nam; }            
            if (MinTien != "") { sqlSelect = sqlSelect + " and TongTien >= " + MinTien; }
            if (MaxTien != "") { sqlSelect = sqlSelect + " and TongTien <= " + MaxTien; }

            if (sqlSelect != "")
            {
                sqlSelect = sqlSelect.Remove(0, 4); // xoa chu " and" dau tien
                sqlSelect = " WHERE" + sqlSelect;
                dt = ctrl.Search(sqlSelect);
            }
            else
            {
                lb_resetData_Click(sender, e);
            }
        }


        private void dtgv_DongPhieuXuat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MaPhieuXuat == "")
            {
                return;
            }

            addDPX = false;
            int row = e.RowIndex;
            if (row < 0)
            {
                return;
            }
            cb_MaLo.Text = dtgv_DongPhieuXuat.Rows[row].Cells["txtMaLo"].Value.ToString();

            if (tb_MaPX_DPX.Text == null || tb_MaPX_DPX.Text == "")
            {
                dtgv_DongPhieuXuat.Rows[row].Cells[1].Value = MaPhieuXuat;
                addDPX = true;
            }

            tb_SoLuong.Text = dtgv_DongPhieuXuat.Rows[row].Cells["txtSoLuong"].Value.ToString();
        }

        private void resetDPX()
        {
            cb_MaLo.Text = "";
            tb_SoLuong.Text = "";
        }
    }
}
