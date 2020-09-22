using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace S4_N14_VuMinhHieu.UserControls
{
    public partial class UCNhanVien : UserControl
    {
        ControllerNhanVien ctrl = new ControllerNhanVien();
        DataTable dt = new DataTable();
        bool addNhanVien;

        public UCNhanVien()
        {
            InitializeComponent();
        }

        private void UCNhanVien_Load(object sender, EventArgs e)
        {
            dt = ctrl.Load();
            dtgv_NhanVien.DataSource = dt;
        }

        private void bt_add_Click(object sender, EventArgs e)
        {
            // paint
            pn_main2.BackColor = System.Drawing.Color.DeepSkyBlue;
            //
            addNhanVien = true;
            int row = dtgv_NhanVien.Rows.Count - 1;
            if (row < 0)
            {
                return;
            }
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            dtgv_NhanVien.ClearSelection();
            dtgv_NhanVien.Rows[row].Selected = true;

            tb_MaNV.DataBindings.Clear();
            tb_tenNV.DataBindings.Clear();
            rb_Male.DataBindings.Clear();
            tb_ngaySinh.DataBindings.Clear();
            tb_SDT.DataBindings.Clear();
            tb_Luong.DataBindings.Clear();
            tb_MaQL.DataBindings.Clear();
            tb_CaLam.DataBindings.Clear();
        }

        private void cb_searchMaNV_Click(object sender, EventArgs e)
        {
            cb_searchMaNV.DataSource = dt;
            cb_searchMaNV.DisplayMember = dt.Columns["MaNV"].ToString();
            cb_searchMaNV.ValueMember = dt.Columns["MaNV"].ToString();
        }

        private void cb_searchManguoiQL_Click(object sender, EventArgs e)
        {
            cb_searchManguoiQL.DataSource = dt;
            cb_searchManguoiQL.DisplayMember = dt.Columns["MaNguoiQL"].ToString();
            cb_searchManguoiQL.ValueMember = dt.Columns["MaNguoiQL"].ToString();
        }

        private void cb_searchName_Click(object sender, EventArgs e)
        {
            cb_searchName.DataSource = dt;
            cb_searchName.DisplayMember = dt.Columns["TenNV"].ToString();
            cb_searchName.ValueMember = dt.Columns["TenNV"].ToString();
        }

        private void bt_resetTimKiemNC_Click(object sender, EventArgs e)
        {
            cb_searchName.DataSource = null;
            cb_searchMaNV.DataSource = null;
            cb_searchManguoiQL.DataSource = null;
            tb_searchPhone.Text = null;
            tb_searchLuongMin.Text = null;
            rb_searchFemale.Checked = false;
            rb_searchMale.Checked = false;

            cb_searchName.Text = "";
            cb_searchMaNV.Text = "";
            cb_searchManguoiQL.Text = "";
            cb_searchDay.Text = "";
            cb_searchDay.Text = "";
            cb_searchYear.Text = "";
        }

        private void tb_searchNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void bt_search_Click(object sender, EventArgs e)
        {
            string sqlSelect = "";
            string MaNV = cb_searchMaNV.Text.Trim();
            string TenNV = cb_searchName.Text.Trim();
            string ngaySinh = cb_searchDay.Text.Trim();
            string thangSinh = cb_searchMonth.Text.Trim();
            string namSinh = cb_searchYear.Text.Trim();
            string sdt = tb_searchPhone.Text.Trim();
            string MaNguoiQL = cb_searchManguoiQL.Text.Trim();
            string LuongMin = tb_searchLuongMin.Text.Trim();
            string LuongMax = tb_searchLuongMax.Text.Trim();

            if (MaNV != "") { sqlSelect = sqlSelect + " and MaNV like '%" + MaNV + "%'"; }
            if (TenNV != "") { sqlSelect = sqlSelect + " and TenNV like N'%" + TenNV + "%'"; }
            if (ngaySinh != "") { sqlSelect = sqlSelect + " and DAY(NgaySinh) = " + ngaySinh; }
            if (thangSinh != "") { sqlSelect = sqlSelect + " and MONTH(NgaySinh) = " + thangSinh; }
            if (namSinh != "") { sqlSelect = sqlSelect + " and YEAR(NgaySinh) = " + namSinh; }
            if (rb_searchMale.Checked) { sqlSelect = sqlSelect + " and GioiTinh = 1"; }
            if (rb_searchFemale.Checked) { sqlSelect = sqlSelect + " and GioiTinh = 0"; }
            if (sdt != "") { sqlSelect = sqlSelect + " and sdt like '%" + sdt + "%'"; }
            if (MaNguoiQL != "") { sqlSelect = sqlSelect + " and MaNguoiQL like '%" + MaNguoiQL + "%'"; }
            if (LuongMin != "") { sqlSelect = sqlSelect + " and Luong >= " + LuongMin; }
            if (LuongMax != "") { sqlSelect = sqlSelect + " and Luong <= " + LuongMax; }

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

        private void dtgv_NhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            addNhanVien = false;
            int row = e.RowIndex;
            if (row < 0)
            {
                return;
            }

            tb_MaNV.DataBindings.Clear();
            tb_MaNV.DataBindings.Add("Text", dtgv_NhanVien.Rows[row].Cells[0], "Value", true, DataSourceUpdateMode.OnPropertyChanged);//OnPropertyChanged: thay đổi từ 2 phía
            tb_tenNV.DataBindings.Clear();
            tb_tenNV.DataBindings.Add("Text", dtgv_NhanVien.Rows[row].Cells[1], "Value", true);
            rb_Male.DataBindings.Clear();
            rb_Male.DataBindings.Add("Checked", dtgv_NhanVien.Rows[row].Cells[2], "Value", true, DataSourceUpdateMode.OnPropertyChanged);
            if (!rb_Male.Checked) { rb_Female.Checked = true; }
            try
            {
                tb_ngaySinh.DataBindings.Clear();
                Binding reportsToBinding4 = tb_ngaySinh.DataBindings.Add("Text", dtgv_NhanVien.Rows[row].Cells[3], "Value", true);
                if (tb_ngaySinh.Text != "")
                {
                    tb_ngaySinh.Text = tb_ngaySinh.Text.Remove(tb_ngaySinh.Text.IndexOf(" "));
                }
                reportsToBinding4.NullValue = "";
            }
            catch
            {
                MessageBox.Show("ngày sinh phải có dạng: tháng/ngày/năm");
            }


            tb_SDT.DataBindings.Clear();
            tb_SDT.DataBindings.Add("Text", dtgv_NhanVien.Rows[row].Cells[4], "Value", true);
            tb_Luong.DataBindings.Clear();

            Binding reportsToBinding = tb_Luong.DataBindings.Add("Text", dtgv_NhanVien.DataSource, "Luong", true, DataSourceUpdateMode.OnPropertyChanged);
            reportsToBinding.NullValue = "";
            tb_MaQL.DataBindings.Clear();

            tb_MaQL.DataBindings.Add("Text", dtgv_NhanVien.DataSource, "MaNguoiQL", true, DataSourceUpdateMode.OnValidation, "");

            tb_CaLam.DataBindings.Clear();
            Binding reportsToBinding3 = tb_CaLam.DataBindings.Add("Text", dtgv_NhanVien.DataSource, "CaLam", true, DataSourceUpdateMode.OnPropertyChanged);
            reportsToBinding3.NullValue = "";
        }

        private void bt_delete_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dtgv_NhanVien.SelectedRows)
                {
                    if (row.Cells[0].Value != null) // tranh truong hop delete final row
                    {
                        dtgv_NhanVien.Rows.Remove(row);
                    }
                }
                ctrl.Update();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bôi đen cả dòng rồi nhấn xóa xóa", "Cách xóa", MessageBoxButtons.OK);
            }
        }

        private void lb_resetData_Click(object sender, EventArgs e)
        {
            dt.Clear();
            dt = ctrl.Load();
            dtgv_NhanVien.DataSource = dt;
        }

        private void bt_resetNhanVien_Click(object sender, EventArgs e)
        {
            tb_MaNV.DataBindings.Clear();
            tb_tenNV.DataBindings.Clear();
            rb_Male.DataBindings.Clear();
            rb_Male.Checked = false;
            rb_Female.Checked = true;
            tb_ngaySinh.DataBindings.Clear();
            tb_SDT.DataBindings.Clear();
            tb_Luong.DataBindings.Clear();
            tb_MaQL.DataBindings.Clear();
            tb_CaLam.DataBindings.Clear();

            tb_MaNV.Text = "";
            tb_tenNV.Text = "";
            rb_Male.Text = "";
            tb_ngaySinh.Text = "";
            tb_SDT.Text = "";
            tb_Luong.Text = "";
            tb_MaQL.Text = "";
            tb_CaLam.Text = "";
        }

        private void bt_confirm_Click(object sender, EventArgs e)
        {
            var maNV = tb_MaNV.Text;
            var tenNV = "null"; if (tb_tenNV.Text != "") { tenNV = "N'" + tb_tenNV.Text + "'"; }
            var gioitinhNV = "null";
            if (rb_Male.Checked) { gioitinhNV = "1"; }
            if (rb_Male.Checked) { gioitinhNV = "0"; }
            var ngaysinhNV = "null"; if (tb_ngaySinh.Text != "") { ngaysinhNV = "'" + tb_tenNV.Text + "'"; }
            var sdtNV = "null"; if (tb_SDT.Text != "") { sdtNV = "N'" + tb_SDT.Text + "'"; }
            var calamNV = "null"; if (tb_CaLam.Text != "") { calamNV = "N'" + tb_CaLam.Text + "'"; }
            var luongNV = tb_Luong.Text; if (luongNV == "") { luongNV = "null"; }
            var qlNV = "null"; if (tb_MaQL.Text != "") { qlNV = "N'" + tb_MaQL.Text + "'"; }
            if (addNhanVien == true)
            {
                //paint
                pn_main2.BackColor = System.Drawing.Color.LightSkyBlue;

                tb_MaNV.DataBindings.Clear();
                tb_tenNV.DataBindings.Clear();
                rb_Male.DataBindings.Clear();
                tb_ngaySinh.DataBindings.Clear();
                tb_SDT.DataBindings.Clear();
                tb_Luong.DataBindings.Clear();
                tb_MaQL.DataBindings.Clear();
                tb_CaLam.DataBindings.Clear();
                try
                {
                    string query = "INSERT DBO.NhanVien(TenNV, GioiTinh, NgaySinh, SDT, CaLam, Luong, MaNguoiQL) VALUES ( " + tenNV + ", " + gioitinhNV + ", " + ngaysinhNV + ", " + sdtNV + ", " + calamNV + ", " + luongNV + "," + qlNV + ")";
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
                    string query = "Update dbo.NhanVien set TenNV = " + tenNV + ", GioiTinh = " + gioitinhNV + ", NgaySinh = " + ngaysinhNV + ", SDT = " + sdtNV + ", CaLam = " + calamNV + ", Luong = " + luongNV + ", MaNguoiQL = " + qlNV + " where MaNV = " + maNV;
                    ctrl.Update(query);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            lb_resetData_Click(sender, e);
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
        }
    }     
    
}
