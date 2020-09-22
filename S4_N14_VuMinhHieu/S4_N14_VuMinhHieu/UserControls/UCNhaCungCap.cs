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
    public partial class UCNhaCungCap : UserControl
    {
        ControllerNCC ctrl = new ControllerNCC();
        DataTable dt = new DataTable();
        DataTable dtCungUng =new DataTable();
        public UCNhaCungCap()
        {
            InitializeComponent();
        }

        private void bt_search_Click(object sender, EventArgs e)
        {
            string sqlSelect = "";
            string MaNCC = cb_searchMaNCC.Text.Trim();
            string TenNCC = cb_searchName.Text.Trim();
            if (MaNCC != "") { sqlSelect = sqlSelect + " and MaNCC like '%" + MaNCC + "%'"; }
            if (TenNCC != "") { sqlSelect = sqlSelect + " and TenNCC like N'%" + TenNCC + "%'"; }
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

        private void UCNhaCungCap_Load(object sender, EventArgs e)
        {
            dt = ctrl.Load();
            dtgv_NhaCungCap.DataSource = dt;
            cb_searchName.DataSource = dt;
            cb_searchName.DisplayMember = dt.Columns["TenNCC"].ToString();
            cb_searchMaNCC.DataSource = dt;
            cb_searchMaNCC.DisplayMember = dt.Columns["MaNCC"].ToString();
        }

        private void dtgv_NhaCungCap_CellClick(object sender, DataGridViewCellEventArgs e)
        {            
            int row = e.RowIndex;
            if (row >= 0)
            {
                string MaNCC = dtgv_NhaCungCap.Rows[row].Cells["txtMaNCC"].Value.ToString();
                if (MaNCC != null && MaNCC != "")
                {
                    dtgv_CungUng.DataSource = ctrl.LoadCungUng(MaNCC);
                    dtgv_PhieuNhap.DataSource = ctrl.LoadPhieuNhap(MaNCC);
                }
                tb_MaNCC.DataBindings.Clear();
                tb_MaNCC.DataBindings.Add("Text", dtgv_NhaCungCap.Rows[row].Cells[0], "Value", true);//OnPropertyChanged: thay đổi từ 2 phía
                tb_TenNCC.DataBindings.Clear();
                tb_TenNCC.DataBindings.Add("Text", dtgv_NhaCungCap.Rows[row].Cells[1], "Value", true, DataSourceUpdateMode.OnPropertyChanged);
            }            
        }

        private void lb_resetData_Click(object sender, EventArgs e)
        {
            dt.Clear();
            dt = ctrl.Load();
            dtgv_NhaCungCap.DataSource = dt;            
        }

        private void bt_CapNhat_Click(object sender, EventArgs e)
        {
            var maNCC = tb_MaNCC.Text;
            var tenNCC = "null"; if (tb_TenNCC.Text != "") { tenNCC = "N'" + tb_TenNCC.Text + "'"; }
            try
            {
                string query = "Update dbo.NhaCungCap set TenNCC = " + tenNCC + " where MaNCC = " + maNCC;
                ctrl.Update(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            reset();
            lb_resetData_Click(sender, e);
        }

        public void reset()
        {
            tb_MaNCC.DataBindings.Clear();
            tb_TenNCC.DataBindings.Clear();
            tb_MaNCC.Text = "";
            tb_TenNCC.Text = "";
        }

        private void bt_ThemNCC_Click(object sender, EventArgs e)
        {
            pn_them.Enabled = true;
        }

        private void bt_confirmAdd_Click(object sender, EventArgs e)
        {
            var TenNCC = "null"; if (tb_addName.Text != "") { TenNCC = "N'" + tb_addName.Text + "'"; }
            try
            {
                string query = "INSERT DBO.NhaCungCap(TenNCC) VALUES ( " + TenNCC + ")";
                ctrl.Insert(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            lb_resetData_Click(sender, e);
            pn_them.Enabled = false;
        }

        private void bt_deleteNhaCC_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dtgv_NhaCungCap.SelectedRows)
                {
                    if (row.Cells[0].Value != null) // tranh truong hop delete final row
                    {
                        dtgv_NhaCungCap.Rows.Remove(row);
                    }
                }
               ctrl.Update();
                dtCungUng.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bạn bôi đen cả dòng rồi nhấn xóa xóa", "Cách xóa", MessageBoxButtons.OK);
            }
        }        
    }
}
