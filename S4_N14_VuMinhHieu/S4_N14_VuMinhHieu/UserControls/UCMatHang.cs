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
    public partial class UCMatHang : UserControl
    {
        ControllerMatHang ctrl = new ControllerMatHang();
        DataTable dt = new DataTable();

        bool addMH;
        bool addLH;

        string MaHang;
         
        public UCMatHang()
        {
            InitializeComponent();
            addMH = true;
            addLH = true;
            MaHang = "";
        }

        private void lb_resetDataMH_Click(object sender, EventArgs e)
        {
            dt.Clear();
            dt = ctrl.Load();
            dtgv_MatHang.DataSource = dt;
            resetMatHang();
        }

        private void bt_deleteMT_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dtgv_MatHang.SelectedRows)
                {
                    if (row.Cells[0].Value != null) // tranh truong hop delete final row
                    {
                        dtgv_MatHang.Rows.Remove(row);
                    }
                }
                ctrl.Update();
                resetMatHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bôi đen cả dòng rồi nhấn xóa xóa", "Cách xóa", MessageBoxButtons.OK);
            }
        }

        private void bt_updateMH_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl.Update();
                resetMatHang();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_addMH_Click(object sender, EventArgs e)
        {
            addMH = true;
            int row = dtgv_MatHang.Rows.Count - 1;
            if (row < 0)
            {
                return;
            }
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);

            dtgv_MatHang.ClearSelection();
            dtgv_MatHang.Rows[row].Selected = true;
            resetMatHang();
        }       

        private void UCMatHang_Load(object sender, EventArgs e)
        {
            dt = ctrl.Load();
            dtgv_MatHang.DataSource = dt;
            
            DataTable dt_Loai = new DataTable();
            dt_Loai = ctrl.LoadLoaiHang();
            cb_searchTenLoai.DataSource = dt_Loai;
            cb_searchTenLoai.DisplayMember = dt_Loai.Columns["TenLoai"].ToString();
            cb_searchTenLoai.ValueMember = dt_Loai.Columns["MaLoai"].ToString();
            cb_searchTenLoai.Text = "";

            cb_MaLoai.DataSource = dt_Loai;
            cb_MaLoai.DisplayMember = dt_Loai.Columns["MaLoai"].ToString();
            cb_MaLoai.ValueMember = dt_Loai.Columns["MaLoai"].ToString();
            cb_MaLoai.Text = "";

            DataTable dt_MaDPN = new DataTable();
            dt_MaDPN = ctrl.LoadDPN();
            cb_MaDPN.DataSource = dt_MaDPN;
            cb_MaDPN.DisplayMember = dt_MaDPN.Columns["MaDPN"].ToString();
            cb_MaDPN.Text = "";
            
        }

        private void dtgv_MatHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            addMH = false;
            int row = e.RowIndex;
            if (row < 0)
            {
                return;
            }
            tb_MaHang.Text = dtgv_MatHang.Rows[row].Cells["txtMaHang"].Value.ToString();
            MaHang = tb_MaHang.Text;
            
            if (MaHang != null && MaHang != "")
            {
                ctrl.dsCU.Clear();
                ctrl.dsLH.Clear();
                dtgv_LoHang.DataSource = ctrl.LoadLoHang(MaHang);
                dtgv_CungUng.DataSource = ctrl.LoadCungUng(MaHang);
            }
            else
            {
                addMH = true;
            }

            tb_TenHang.Text = dtgv_MatHang.Rows[row].Cells["txtTenHang"].Value.ToString();
            tb_DV.Text = dtgv_MatHang.Rows[row].Cells["txtDvTinh"].Value.ToString();
            tb_DonGia.Text = dtgv_MatHang.Rows[row].Cells["txtDonGia"].Value.ToString();
            cb_MaLoai.Text = dtgv_MatHang.Rows[row].Cells["txtMaLoai"].Value.ToString();

            tb_MaHang_LH.Text = MaHang;
        }

        private void bt_confirmMH_Click(object sender, EventArgs e)
        {
            var maHang = tb_MaHang.Text;           
            var tenHang = "null"; if (tb_TenHang.Text != "") { tenHang = "N'" + tb_TenHang.Text + "'"; }
            var dvt = "null"; if (tb_DV.Text != "") { dvt = "N'" + tb_DV.Text + "'"; }
            var donGia = "null"; if (tb_DonGia.Text != "") { donGia = tb_DonGia.Text; }
            var maLoai = cb_MaLoai.Text; if (maLoai == "") { maLoai = "null"; }
            
            if (addMH == true)
            {
                try
                {
                    string query = "INSERT DBO.MatHang( TenHang ,DvTinh ,DonGia ,MaLoai) VALUES ( " + tenHang + ", " + dvt + ", " + donGia + ", " + maLoai + ")";
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
                    string query = "Update dbo.MatHang set TenHang = " + tenHang + ", DvTinh = " + dvt + ", DonGia = " + donGia + ", MaLoai = " + maLoai + " where MaHang = " + maHang;
                    ctrl.Update(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            lb_resetDataMH_Click(sender, e);
        }

        private void bt_resetDataLH_Click(object sender, EventArgs e)
        {
            dtgv_LoHang.DataSource = ctrl.LoadLoHang(MaHang);
            resetLoHang();
        }

        private void bt_deleteLH_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dtgv_LoHang.SelectedRows)
                {
                    if (row.Cells[0].Value != null) // tranh truong hop delete final row
                    {
                        dtgv_LoHang.Rows.Remove(row);
                    }
                }
                ctrl.UpdateLH();
                resetLoHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bôi đen cả dòng rồi nhấn xóa xóa", "Cách xóa", MessageBoxButtons.OK);
            }
        }

        private void bt_updateLH_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl.UpdateLH();
                resetLoHang();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_addLH_Click(object sender, EventArgs e)
        {
            addLH = true;
            int row = dtgv_LoHang.Rows.Count - 1;
            if (row < 0)
            {
                return;
            }
            dtgv_LoHang.ClearSelection();
            dtgv_LoHang.Rows[row].Selected = true;

            resetLoHang();
        }

        private void dtgv_LoHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (MaHang == "")
            {
                return;
            }
            addLH = false;
            int row = e.RowIndex;
            if (row < 0)
            {
                return;
            }
            tb_MaLo.Text = dtgv_LoHang.Rows[row].Cells["txtMaLo"].Value.ToString();
            string MaLo = tb_MaLo.Text;
            if (MaLo == null || MaLo == "")
            {
                dtgv_LoHang.Rows[row].Cells[1].Value = MaHang;
                addLH = true;
            }
            if (dtgv_LoHang.Rows[row].Cells["txtHSD"].Value.ToString() != "")
            {
                dtp_HSD.Value = Convert.ToDateTime(dtgv_LoHang.Rows[row].Cells["txtHSD"].Value);
            }
            tb_TonKho.Text = dtgv_LoHang.Rows[row].Cells["txtTonKho"].Value.ToString();
            cb_MaDPN.Text = dtgv_LoHang.Rows[row].Cells["txtMaDPN"].Value.ToString();
        }

        private void resetLoHang()
        {
            tb_MaLo.Text = "";
            tb_TonKho.Text = "";
            cb_MaDPN.Text = "";
        }

        private void resetMatHang()
        {
            tb_MaHang.Text = "";
            tb_TenHang.Text = "";
            tb_DV.Text = "";
            tb_DonGia.Text = "";
            cb_MaLoai.Text = "";
        }


        private void bt_confirmLH_Click(object sender, EventArgs e)
        {
            var MaLo = tb_MaLo.Text;
            var HSD = "null"; if (dtp_HSD.Value.ToString() != "") { HSD = "'" + dtp_HSD.Value.ToShortDateString() + "'"; }
            var TonKho = tb_TonKho.Text; if (TonKho == "") { TonKho = "0"; }
            var MaDPN = "null"; if( cb_MaDPN.Text != "") { MaDPN = cb_MaDPN.Text; }
            
            if (addLH == true)
            {
                try
                {
                    string query = "INSERT DBO.LoHang(HSD, TonKho, MaHang, MaDPN) VALUES ( " + HSD + ", " + TonKho + ", " + MaHang + ", " + MaDPN  + ")";
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
                    string query = "Update dbo.LoHang set HSD = " + HSD + " , TonKho = " + TonKho + ", MaHang = " + MaHang + ", MaDPN = " + MaDPN+ " where MaLo = " + MaLo;
                    ctrl.Update(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            bt_resetDataLH_Click(sender, e);
        }

        private void bt_resetDataCU_Click(object sender, EventArgs e)
        {
            dtgv_CungUng.DataSource = ctrl.LoadCungUng(MaHang);         
        }

        private void bt_deleteCU_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dtgv_CungUng.SelectedRows)
                {
                    if (row.Cells[0].Value != null) // tranh truong hop delete final row
                    {
                        dtgv_CungUng.Rows.Remove(row);
                    }
                }
                ctrl.UpdateCU();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bôi đen cả dòng rồi nhấn xóa xóa", "Cách xóa", MessageBoxButtons.OK);
            }
        }

        private void bt_updateCU_Click(object sender, EventArgs e)
        {
            try
            {
                ctrl.UpdateCU();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bt_addCU_Click(object sender, EventArgs e)
        {
            int row = dtgv_LoHang.Rows.Count - 1;
            if (row < 0)
            {
                return;            }
            
            dtgv_CungUng.ClearSelection();
            dtgv_CungUng.Rows[row].Selected = true;

        }

        private void bt_search_Click(object sender, EventArgs e)
        {
            try
            {
                string sqlSelect = "";

                string TenMH = cb_searchName.Text.Trim();
                string MaMH = cb_searchMaHang.Text.Trim();

                string MaLoai = "";
                if (cb_searchTenLoai.Text != "")
                {
                    MaLoai = cb_searchTenLoai.SelectedValue.ToString().Trim();
                }

                string DVT = tb_searchDVT.Text.Trim();
                string MinDonGia = tb_searchDonGiaMin.Text.Trim();
                string MaxDonGia = tb_searchDonGiaMax.Text.Trim();

                if (TenMH != "") { sqlSelect = sqlSelect + " and TenHang like N'%" + TenMH + "%'"; }
                if (MaMH != "") { sqlSelect = sqlSelect + " and MaHang like '%" + MaMH + "%'"; }
                if (MaLoai != "") { sqlSelect = sqlSelect + " and MaLoai = '%" + MaLoai + "%'"; }
                if (DVT != "") { sqlSelect = sqlSelect + " and DvTinh = N'%" + DVT + "%'"; }
                if (MinDonGia != "") { sqlSelect = sqlSelect + " and DonGia >= " + MinDonGia; }
                if (MaxDonGia != "") { sqlSelect = sqlSelect + " and DonGia <= " + MaxDonGia; }

                if (sqlSelect != "")
                {
                    sqlSelect = sqlSelect.Remove(0, 4); // xoa chu " and" dau tien
                    sqlSelect = " WHERE" + sqlSelect;
                    dt = ctrl.Search(sqlSelect);
                }
                else
                {
                    lb_resetDataMH_Click(sender, e);
                }
            }
            catch
            {
                ;
            }
            
        }

        private void cb_searchName_Click(object sender, EventArgs e)
        {
            cb_searchName.DataSource = dt;
            cb_searchName.DisplayMember = dt.Columns["TenHang"].ToString();
        }

        private void cb_searchMaHang_Click(object sender, EventArgs e)
        {
            cb_searchMaHang.DataSource = dt;
            cb_searchMaHang.DisplayMember = dt.Columns["MaHang"].ToString();
        }

        private void bt_resetTimKiemNC_Click(object sender, EventArgs e)
        {
            cb_searchName.Text = "";
            cb_searchMaHang.Text = "";
            cb_searchTenLoai.Text = "";
            tb_searchDVT.Text = "";
            tb_searchDonGiaMin.Text = "";
            tb_searchDonGiaMax.Text = "";
        }
    }
}
