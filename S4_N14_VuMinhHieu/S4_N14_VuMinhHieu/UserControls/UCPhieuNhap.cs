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
    public partial class UCPhieuNhap : UserControl
    {
        ControllerPhieuNhap ctrl = new ControllerPhieuNhap();
        DataTable dt = new DataTable();
        
        DataTable dtDPN = new DataTable();
        bool addPN;
        bool addDPN;

        string MaPhieuNhap;
        public UCPhieuNhap()
        {
            InitializeComponent();
            addPN = true;
            addDPN = true;
            MaPhieuNhap = "";
        }

        private void UCPhieuNhap_Load(object sender, EventArgs e)
        {
            dt = ctrl.Load();
            dtgv_PhieuNhap.DataSource = dt;
            DataTable dt_mh = new DataTable();
            dt_mh = ctrl.LoadMatHang();
            cb_MatHang.DataSource = dt_mh;
            cb_MatHang.DisplayMember = dt_mh.Columns["TenHang"].ToString();
            cb_MatHang.ValueMember = dt_mh.Columns["MaHang"].ToString();
        }

        private void dtgv_PhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            addPN = false;
            int row = e.RowIndex;
            if (row < 0)
            {
                return;
            }
            tb_MaPN.Text = dtgv_PhieuNhap.Rows[row].Cells["txtMaPhieuNhap"].Value.ToString();
            MaPhieuNhap = tb_MaPN.Text;
            if (dtgv_PhieuNhap.Rows[row].Cells["txtThoigian"].Value.ToString() != "")
            {
                dtp_ThoiGianPN.Value = Convert.ToDateTime(dtgv_PhieuNhap.Rows[row].Cells["txtThoigian"].Value);
             }
            if (MaPhieuNhap != null && MaPhieuNhap != "")
            {
                ctrl.dsDPN.Clear();
                dtgv_DongPhieuNhap.DataSource = ctrl.LoadDongPhieuNhap(MaPhieuNhap);               
            }
            else
            {
                addPN = true;
            }

            tb_MaNV.Text = dtgv_PhieuNhap.Rows[row].Cells["txtMaNV"].Value.ToString();
            tb_MaNCC.Text = dtgv_PhieuNhap.Rows[row].Cells["txtMaNCC"].Value.ToString();
            tb_NoPN.Text = dtgv_PhieuNhap.Rows[row].Cells["txtNoPN"].Value.ToString();
            tb_TongTien.Text = dtgv_PhieuNhap.Rows[row].Cells["txtTongTien"].Value.ToString();

            tb_MaPN_DPN.Text = MaPhieuNhap;
        }

        private void bt_addPN_Click(object sender, EventArgs e)
        {
            addPN = true;
            int row = dtgv_PhieuNhap.Rows.Count - 1;
            if (row < 0)
            {
                return;
            }
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
  
            dtgv_PhieuNhap.ClearSelection();
            dtgv_PhieuNhap.Rows[row].Selected = true;
            reset();
        }

        private void bt_confirm_Click(object sender, EventArgs e)
        {
            var maPN = tb_MaPN.Text;
            var thoigianPN = "null"; if (dtp_ThoiGianPN.Value.ToString() != "") { thoigianPN = "'" + dtp_ThoiGianPN.Value.ToString() + "'"; }
            var maNV = "null"; if (tb_MaNV.Text != "") { maNV = "'" + tb_MaNV.Text + "'"; }
            var maNCC = "null"; if (tb_MaNCC.Text != "") { maNCC = "'" + tb_MaNCC.Text + "'"; }
            var noPN = tb_NoPN.Text; if (noPN == "") { noPN = "null"; }
            var tongtienPN = tb_TongTien.Text; if (tongtienPN == "") { tongtienPN = "null"; }
            if (addPN == true)
            {
                try
                {
                    string query = "INSERT DBO.PhieuNhap(ThoiGian, MaNV, MaNCC, NoPN, TongTien) VALUES ( " + thoigianPN + ", " + maNV + ", " + maNCC + ", " + noPN + ", " + tongtienPN + ")";
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
                    string query = "Update dbo.PhieuNhap set ThoiGian = " + thoigianPN + ", MaNV = " + maNV + ", MaNCC = " + maNCC+ ", NoPN = " + noPN + ", TongTien = " + tongtienPN + " where MaPhieuNhap = " + maPN;
                    ctrl.Update(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
           lb_resetData_Click(sender, e);
        }

        private void lb_resetData_Click(object sender, EventArgs e)
        {
            dt.Clear();
            dt = ctrl.Load();
            dtgv_PhieuNhap.DataSource = dt;
            dtDPN.Clear();
            reset();
        }

        private void reset()
        {
            tb_MaPN.Text = "";
            tb_MaNV.Text = "";
            tb_MaNCC.Text = "";
            tb_NoPN.Text = "";
            tb_TongTien.Text = "";

            cb_MaPN.Text = "";
            cb_searchNam.Text = "";
            cb_searchThang.Text = "";
            cb_searchNam.Text = "";

            cb_MaNV.Text = "";
            cb_MaNCC.Text = "";
            tb_minTongTien.Text = "";
            tb_maxTongTien.Text = "";
        }

        private void bt_resetTimKiem_Click(object sender, EventArgs e)
        {
            reset();
        }

        private void cb_MaPN_Click(object sender, EventArgs e)
        {
            cb_MaPN.DataSource = dt;
            cb_MaPN.DisplayMember = dt.Columns["MaPhieuNhap"].ToString();
        }

        private void cb_MaNV_Click(object sender, EventArgs e)
        {
            cb_MaNV.DataSource = dt;
            cb_MaNV.DisplayMember = dt.Columns["MaNV"].ToString();
        }

        private void cb_MaNCC_Click(object sender, EventArgs e)
        {
            cb_MaNCC.DataSource = dt;
            cb_MaNCC.DisplayMember = dt.Columns["MaNCC"].ToString();
        }

        private void bt_search_Click(object sender, EventArgs e)
        {
            string sqlSelect = "";

            string MaPN = cb_MaPN.Text.Trim();
            string MaNV = cb_MaNV.Text.Trim();
            string Ngay = cb_searchNgay.Text.Trim();
            string Thang = cb_searchThang.Text.Trim();
            string Nam = cb_searchNam.Text.Trim();
            string MaNCC = cb_MaNCC.Text.Trim();            
            string MinTien = tb_minTongTien.Text.Trim();
            string MaxTien = tb_maxTongTien.Text.Trim();

            if (MaPN != "") { sqlSelect = sqlSelect + " and MaPhieNhap like '%" + MaPN + "%'"; }
            if (MaNV != "") { sqlSelect = sqlSelect + " and MaNV like N'%" + MaNV + "%'"; }
            if (Ngay != "") { sqlSelect = sqlSelect + " and DAY(ThoiGian) = " + Ngay; }
            if (Thang != "") { sqlSelect = sqlSelect + " and MONTH(ThoiGian) = " + Thang; }
            if (Nam != "") { sqlSelect = sqlSelect + " and YEAR(ThoiGian) = " + Nam; }
            if (MaNCC != "") { sqlSelect = sqlSelect + " and MaNCC like N'%" + MaNCC + "%'"; }
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

        private void bt_update_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl.Update();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            reset();
        }

        private void bt_deletePN_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dtgv_PhieuNhap.SelectedRows)
                {
                    if (row.Cells[0].Value != null) // tranh truong hop delete final row
                    {
                        dtgv_PhieuNhap.Rows.Remove(row);
                    }
                }
                ctrl.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bôi đen cả dòng rồi nhấn xóa xóa", "Cách xóa", MessageBoxButtons.OK);
            }
            reset();
        }

        private void dtgv_DongPhieuNhap_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(MaPhieuNhap == "")
            {
               return;
            }

            addDPN = false;
            int row = e.RowIndex;
            if (row < 0)
            {
                return;
            }
            tb_MaDPN.Text = dtgv_DongPhieuNhap.Rows[row].Cells["txtMaDPN"].Value.ToString();
            string MaDPN = tb_MaDPN.Text;
            if (MaDPN == null || MaDPN == "")
            {
                dtgv_DongPhieuNhap.Rows[row].Cells[1].Value = MaPhieuNhap;
                addDPN = true;
            }

            tb_SoLuong.Text = dtgv_DongPhieuNhap.Rows[row].Cells["txtSoLuong"].Value.ToString();
            tb_DonGiaNhap.Text = dtgv_DongPhieuNhap.Rows[row].Cells["txtDonGiaNhap"].Value.ToString();
        }

        private void bt_resetDB_Click(object sender, EventArgs e)
        {

            dtgv_DongPhieuNhap.DataSource = ctrl.LoadDongPhieuNhap(MaPhieuNhap);
            reset_information_DPN();
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dtgv_DongPhieuNhap.SelectedRows)
                {
                    if (row.Cells[0].Value != null) // tranh truong hop delete final row
                    {
                        dtgv_DongPhieuNhap.Rows.Remove(row);
                    }
                }
                ctrl.UpdateDPN();
                reset_information_DPN();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bôi đen cả dòng rồi nhấn xóa xóa", "Cách xóa", MessageBoxButtons.OK);
            }
        }

        private void bt_updateDPN_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl.UpdateDPN();
                reset_information_DPN();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_addDPN_Click(object sender, EventArgs e)
        {
            addDPN = true;
            int row = dtgv_DongPhieuNhap.Rows.Count - 1;
            if (row < 0)
            {
                return;
            }
            DataRow newRow = dtDPN.NewRow();
            dtDPN.Rows.Add(newRow);

            dtgv_DongPhieuNhap.ClearSelection();
            dtgv_DongPhieuNhap.Rows[row].Selected = true;
            reset_information_DPN();
        }

        public void resetDPN()
        {
            tb_MaPN_DPN.Text = "";
            tb_SoLuong.Text = "";
            tb_TongTien.Text = "";

            cb_MatHang.Text = "";            
        }

        private void bt_confirmDPN_Click(object sender, EventArgs e)
        {
            var MaDPN = tb_MaDPN.Text;

            var SoLuong = tb_SoLuong.Text; if (SoLuong == "") { SoLuong = "null"; }
            var DonGiaNhap = tb_DonGiaNhap.Text; if (DonGiaNhap == "") { DonGiaNhap = "0"; }

            var MaHang = cb_MatHang.SelectedValue.ToString();
            var HSD = "null"; if (dtp_HSD.Value.ToString() != "") { HSD = "'" + dtp_HSD.Value.ToShortDateString() + "'"; }
            if (addDPN == true)
            { 
                try
                {
                    string query = "EXEC INSERT_DongPhieuNhap_LoHang " + MaPhieuNhap + ", " + SoLuong + ", " + DonGiaNhap + ", " + MaHang + ", " + HSD;
                    ctrl.Insert(query);
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                }
            }
            else // update
            {
                try
                {
                    string query = "Update dbo.DongPhieuNhap set MaPhieuNhap = " + MaPhieuNhap + " , SoLuong = " + SoLuong + ", DonGiaNhap = " + DonGiaNhap + " where MaDPN = " + MaDPN;
                    ctrl.Update(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            lb_resetData_Click(sender, e);
        }

        private void reset_information_DPN()
        {
            tb_SoLuong.Text = "";
            tb_DonGiaNhap.Text = "";
        }
    }
}
