using S4_N14_VuMinhHieu.UserControls;
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
    public partial class fNghiepVu : Form
    {
        string ngayDau_ky;
        string thangDau_ky;
        string namDau_ky;

        string ngayCuoi_ky;
        string thangCuoi_ky;
        string namCuoi_ky;

        string maLoai;
        public ControllerNghiepVu ctrl = new ControllerNghiepVu();
        DataTable dtLoai = new DataTable();
        DataTable dt = new DataTable();



        public string NgayDau_ky { get => ngayDau_ky; set => ngayDau_ky = value; }
        public string ThangDau_ky { get => thangDau_ky; set => thangDau_ky = value; }
        public string NamDau_ky { get => namDau_ky; set => namDau_ky = value; }
        public string NgayCuoi_ky { get => ngayCuoi_ky; set => ngayCuoi_ky = value; }
        public string ThangCuoi_ky { get => thangCuoi_ky; set => thangCuoi_ky = value; }
        public string NamCuoi_ky { get => namCuoi_ky; set => namCuoi_ky = value; }
        public string MaLoai { get => maLoai; set => maLoai = value; }

        public fNghiepVu()
        {
            InitializeComponent();
            dtLoai = ctrl.LoadLoaiHang();
            cb_Maloai.DataSource = dtLoai;
            cb_Maloai.DisplayMember = dtLoai.Columns["TenLoai"].ToString();
            cb_Maloai.ValueMember = dtLoai.Columns["MaLoai"].ToString();

             NgayDau_ky = "1";
            ThangDau_ky = "1";
            NamDau_ky = "2019";

            NgayCuoi_ky = DateTime.Today.Day.ToString();
            ThangCuoi_ky = DateTime.Today.Month.ToString();
            NamCuoi_ky = DateTime.Today.Year.ToString();

            cb_NgayDauKy.Text = NgayDau_ky;
            cb_ThangDauKy.Text = ThangDau_ky;
            cb_NamDauKy.Text = NamDau_ky;

            cb_NgayCuoiKy.Text = NgayCuoi_ky;
            cb_ThangCuoiKy.Text = ThangCuoi_ky;
            cb_NamCuoiKy.Text = NamCuoi_ky;

            MaLoai = "1";

            cb_Maloai.Text = MaLoai;
        }

        

        private void bt_toFNghiepVu_Click(object sender, EventArgs e)
        {
            this.Hide();
            fDanhMuc f = new fDanhMuc();
            f.ShowDialog();
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            cb_NgayCuoiKy.Text = DateTime.Today.Day.ToString();
            cb_ThangCuoiKy.Text = DateTime.Today.Month.ToString();
            cb_NamCuoiKy.Text = DateTime.Today.Year.ToString();
        }

        private void bt_ThongKe_Click(object sender, EventArgs e)
        {
            dt.Clear();
            string daukySTR = cb_ThangDauKy.Text + "/" + cb_NgayDauKy.Text + "/" + cb_NamDauKy.Text;
            string cuoikySTR = cb_ThangCuoiKy.Text + "/" + cb_NgayCuoiKy.Text + "/" + cb_NamCuoiKy.Text;
           
            dt = ctrl.Load(Convert.ToInt32(cb_Maloai.SelectedValue), daukySTR, cuoikySTR);
            dtgv_BaoCao.DataSource = dt;
            double tong_Tien =0;
            int tong_SoLuong = 0;
            foreach (DataGridViewRow row in dtgv_BaoCao.Rows)
            {
                if(row.Cells["txtThanhTien_CuoiKy"].Value != null)
                    tong_Tien = tong_Tien + Convert.ToDouble(row.Cells["txtThanhTien_CuoiKy"].Value.ToString());
                if (row.Cells["txtSoLuong_CuoiKy"].Value != null)
                    tong_SoLuong = tong_SoLuong + Convert.ToInt32(row.Cells["txtSoLuong_CuoiKy"].Value.ToString());
            }
            lb_TongTien.Text = tong_Tien.ToString()+".000";
            lb_TongSL.Text = tong_SoLuong.ToString();
        }

        private void cb_Maloai_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void bt_ExportExcel_Click(object sender, EventArgs e)
        {
            //  khởi tạo excel
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // khởi tạo WorkBook
            Microsoft.Office.Interop.Excel._Workbook workbook= app.Workbooks.Add(Type.Missing);
            // khởi tạo WorkSheet
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;

            app.Visible = true;

            // đinh dạng côt

            worksheet.Range["A1"].ColumnWidth = 4;
            worksheet.Range["B1"].ColumnWidth = 8;
            worksheet.Range["C1"].ColumnWidth = 25;
            worksheet.Range["D1"].ColumnWidth = 6;
            worksheet.Range["E1"].ColumnWidth = 8;
            worksheet.Range["F1"].ColumnWidth = 16;
            worksheet.Range["G1"].ColumnWidth = 10;
            worksheet.Range["H1"].ColumnWidth = 18;
            worksheet.Range["I1"].ColumnWidth = 16;
            worksheet.Range["J1"].ColumnWidth = 16;
            worksheet.Range["K1"].ColumnWidth = 8;
            worksheet.Range["L1"].ColumnWidth = 16;
            worksheet.Range["M1"].ColumnWidth = 12;
            // đinh dạng  FONT
            worksheet.Range["A1", "M1"].Font.Size = 18;
            worksheet.Range["A1", "M1"].MergeCells = true;
            worksheet.Range["A1", "M5"].Font.Bold = true;
            // ĐỔ dữ liệu vào Sheet:
            worksheet.Cells[1,1] = "BÁO CÁO TỔNG HỢP TỒN KHO";
            worksheet.Cells[3, 4] = "Từ Ngày: " + cb_NgayDauKy.Text + "/" + cb_ThangDauKy.Text + "/" + cb_NamDauKy.Text;
            worksheet.Cells[3, 8] = "Đến Ngày: " + cb_NgayCuoiKy.Text + "/" + cb_ThangCuoiKy.Text + "/" + cb_NamCuoiKy.Text;
            worksheet.Cells[5, 5] = "Loại: " + cb_Maloai.Text;

            worksheet.Cells[8, 1] = "STT";
            worksheet.Cells[8, 2] = "Mã hàng";
            worksheet.Cells[8, 3] = "Tên hàng";
            worksheet.Cells[8, 4] = "Mã lô";
            worksheet.Cells[8, 5] = "SL đầu kỳ";
            worksheet.Cells[8, 6] = "Thành tiền đầu kỳ";
            worksheet.Cells[8, 7] = "SL nhập kho";
            worksheet.Cells[8, 8] = "Thành tiền nhập kho";
            worksheet.Cells[8, 9] = "Số lượng xuất kho";
            worksheet.Cells[8, 10] = "Thành tiền xuất kho";
            worksheet.Cells[8, 11] = "SL cuối kỳ";
            worksheet.Cells[8, 12] = "Thành tiền cuối kỳ";
            worksheet.Cells[8, 13] = "HSD";

            for(int i = 0; i< dtgv_BaoCao.RowCount-1; i++)
            {
                worksheet.Cells[i + 9, 1] = i + 1;
                for ( int j=0; j<12; ++j)
                {

                    worksheet.Cells[i + 9, j + 2] = dtgv_BaoCao.Rows[i].Cells[j].Value;
                }
            }
            int index = dtgv_BaoCao.RowCount +9;
            worksheet.Cells[index, 3] = "Tổng tiền cuối kỳ: "+ lb_TongTien.Text;
            worksheet.Cells[index+1, 3] = "Tổng tồn kho cuối kỳ: " + lb_TongSL.Text;
           
        }
    }
}
