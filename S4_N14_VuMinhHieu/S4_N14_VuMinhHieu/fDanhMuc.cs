using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace S4_N14_VuMinhHieu
{
    public partial class fDanhMuc : Form
    {
        public fDanhMuc()
        {
            InitializeComponent();
        }

        private void fDemoDanhMuc_Load(object sender, EventArgs e)
        {
            ucNhanVien1.Visible = false;
            ucNhaCungCap1.Visible = false;
            ucPhieuNhap1.Visible = false;
            ucPhieuXuat1.Visible = false;
            ucMatHang1.Visible = false;
            banner1.Visible = true;
        }



        private void bt_toFNghiepVu_Click(object sender, EventArgs e)
        {
            this.Hide();
            fNghiepVu f = new fNghiepVu();
            f.ShowDialog();
        }

        private void tsmi_NhanVien_Click_1(object sender, EventArgs e)
        {
            ucNhanVien1.Visible = true;
            ucNhaCungCap1.Visible = false;
            ucPhieuNhap1.Visible = false;
            ucPhieuXuat1.Visible = false;
            ucMatHang1.Visible = false;
            banner1.Visible = false;

        }

        private void tsmi_NhaCungCap_Click_1(object sender, EventArgs e)
        {
            ucNhanVien1.Visible = false;
            ucNhaCungCap1.Visible = true;
            ucPhieuNhap1.Visible = false;
            ucPhieuXuat1.Visible = false;
            ucMatHang1.Visible = false;
            banner1.Visible = false;
        }   


        private void tsmi_PhieuXuat1_Click(object sender, EventArgs e)
        {
            ucNhanVien1.Visible = false;
            ucNhaCungCap1.Visible = false;
            ucPhieuNhap1.Visible = false;
            ucPhieuXuat1.Visible = true;
            ucMatHang1.Visible = false;
            banner1.Visible = false;
        }

        private void tsmi_PhieuNhap1_Click(object sender, EventArgs e)
        {
            ucNhanVien1.Visible = false;
            ucNhaCungCap1.Visible = false;
            ucPhieuNhap1.Visible = true;
            ucPhieuXuat1.Visible = false;
            ucMatHang1.Visible = false;
            banner1.Visible = false;
        }

        private void tsmi_MatHang_Click(object sender, EventArgs e)
        {
            ucNhanVien1.Visible = false;
            ucNhaCungCap1.Visible = false;
            ucPhieuNhap1.Visible = false;
            ucPhieuXuat1.Visible = false;
            ucMatHang1.Visible = true;
            banner1.Visible = false;
        }



        private void pn_logo_Click(object sender, EventArgs e)
        {
            ucNhanVien1.Visible = false;
            ucNhaCungCap1.Visible = false;
            ucPhieuNhap1.Visible = false;
            ucPhieuXuat1.Visible = false;
            ucMatHang1.Visible = false;
            banner1.Visible = true;
        }
    }
}
