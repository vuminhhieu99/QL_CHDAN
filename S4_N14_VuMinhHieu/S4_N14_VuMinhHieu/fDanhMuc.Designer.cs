namespace S4_N14_VuMinhHieu
{
    partial class fDanhMuc
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fDanhMuc));
            this.pn_Menu = new System.Windows.Forms.Panel();
            this.M = new System.Windows.Forms.MenuStrip();
            this.tsmi_MatHang = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PhieuNhap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_PhieuXuat = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_NhaCungCap = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_NhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.pn_about = new System.Windows.Forms.Panel();
            this.lb_about1 = new System.Windows.Forms.Label();
            this.lb_about2 = new System.Windows.Forms.Label();
            this.pn_main = new System.Windows.Forms.Panel();
            this.banner1 = new S4_N14_VuMinhHieu.UserControls.Banner();
            this.ucMatHang1 = new S4_N14_VuMinhHieu.UserControls.UCMatHang();
            this.ucPhieuXuat1 = new S4_N14_VuMinhHieu.UserControls.UCPhieuXuat();
            this.ucPhieuNhap1 = new S4_N14_VuMinhHieu.UserControls.UCPhieuNhap();
            this.ucNhaCungCap1 = new S4_N14_VuMinhHieu.UserControls.UCNhaCungCap();
            this.ucNhanVien1 = new S4_N14_VuMinhHieu.UserControls.UCNhanVien();
            this.bt_toFNghiepVu = new System.Windows.Forms.Button();
            this.pn_logo = new System.Windows.Forms.Panel();
            this.pn_Menu.SuspendLayout();
            this.M.SuspendLayout();
            this.pn_about.SuspendLayout();
            this.pn_main.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_Menu
            // 
            this.pn_Menu.BackColor = System.Drawing.Color.DarkRed;
            this.pn_Menu.Controls.Add(this.M);
            this.pn_Menu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pn_Menu.Location = new System.Drawing.Point(70, 0);
            this.pn_Menu.Margin = new System.Windows.Forms.Padding(0);
            this.pn_Menu.Name = "pn_Menu";
            this.pn_Menu.Size = new System.Drawing.Size(1127, 60);
            this.pn_Menu.TabIndex = 0;
            // 
            // M
            // 
            this.M.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.M.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.M.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_MatHang,
            this.tsmi_PhieuNhap,
            this.tsmi_PhieuXuat,
            this.tsmi_NhaCungCap,
            this.tsmi_NhanVien});
            this.M.Location = new System.Drawing.Point(0, 0);
            this.M.Name = "M";
            this.M.Padding = new System.Windows.Forms.Padding(0);
            this.M.Size = new System.Drawing.Size(1127, 58);
            this.M.TabIndex = 0;
            this.M.Text = "menuStrip1";
            // 
            // tsmi_MatHang
            // 
            this.tsmi_MatHang.AutoSize = false;
            this.tsmi_MatHang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.tsmi_MatHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmi_MatHang.ForeColor = System.Drawing.Color.Snow;
            this.tsmi_MatHang.Name = "tsmi_MatHang";
            this.tsmi_MatHang.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tsmi_MatHang.Size = new System.Drawing.Size(95, 58);
            this.tsmi_MatHang.Text = "Mặt hàng";
            this.tsmi_MatHang.Click += new System.EventHandler(this.tsmi_MatHang_Click);
            // 
            // tsmi_PhieuNhap
            // 
            this.tsmi_PhieuNhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmi_PhieuNhap.ForeColor = System.Drawing.Color.Snow;
            this.tsmi_PhieuNhap.Name = "tsmi_PhieuNhap";
            this.tsmi_PhieuNhap.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tsmi_PhieuNhap.Size = new System.Drawing.Size(112, 58);
            this.tsmi_PhieuNhap.Text = "Phiếu Nhập";
            this.tsmi_PhieuNhap.Click += new System.EventHandler(this.tsmi_PhieuNhap1_Click);
            // 
            // tsmi_PhieuXuat
            // 
            this.tsmi_PhieuXuat.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmi_PhieuXuat.ForeColor = System.Drawing.Color.Snow;
            this.tsmi_PhieuXuat.Name = "tsmi_PhieuXuat";
            this.tsmi_PhieuXuat.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tsmi_PhieuXuat.Size = new System.Drawing.Size(103, 58);
            this.tsmi_PhieuXuat.Text = "Phiếu xuất";
            this.tsmi_PhieuXuat.Click += new System.EventHandler(this.tsmi_PhieuXuat1_Click);
            // 
            // tsmi_NhaCungCap
            // 
            this.tsmi_NhaCungCap.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmi_NhaCungCap.ForeColor = System.Drawing.Color.Snow;
            this.tsmi_NhaCungCap.Name = "tsmi_NhaCungCap";
            this.tsmi_NhaCungCap.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tsmi_NhaCungCap.Size = new System.Drawing.Size(127, 58);
            this.tsmi_NhaCungCap.Text = "Nhà cung cấp";
            this.tsmi_NhaCungCap.Click += new System.EventHandler(this.tsmi_NhaCungCap_Click_1);
            // 
            // tsmi_NhanVien
            // 
            this.tsmi_NhanVien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmi_NhanVien.ForeColor = System.Drawing.Color.Snow;
            this.tsmi_NhanVien.Name = "tsmi_NhanVien";
            this.tsmi_NhanVien.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.tsmi_NhanVien.Size = new System.Drawing.Size(101, 58);
            this.tsmi_NhanVien.Text = "Nhân viên";
            this.tsmi_NhanVien.Click += new System.EventHandler(this.tsmi_NhanVien_Click_1);
            // 
            // pn_about
            // 
            this.pn_about.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pn_about.Controls.Add(this.lb_about1);
            this.pn_about.Controls.Add(this.lb_about2);
            this.pn_about.Location = new System.Drawing.Point(0, 662);
            this.pn_about.Margin = new System.Windows.Forms.Padding(0);
            this.pn_about.Name = "pn_about";
            this.pn_about.Size = new System.Drawing.Size(1388, 40);
            this.pn_about.TabIndex = 2;
            // 
            // lb_about1
            // 
            this.lb_about1.BackColor = System.Drawing.Color.Transparent;
            this.lb_about1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_about1.ForeColor = System.Drawing.Color.Snow;
            this.lb_about1.Location = new System.Drawing.Point(-3, 0);
            this.lb_about1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_about1.Name = "lb_about1";
            this.lb_about1.Size = new System.Drawing.Size(302, 19);
            this.lb_about1.TabIndex = 1;
            this.lb_about1.Text = "Danh mục quản lý kho cửa hàng ăn nhanh";
            // 
            // lb_about2
            // 
            this.lb_about2.BackColor = System.Drawing.Color.Transparent;
            this.lb_about2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lb_about2.ForeColor = System.Drawing.Color.Snow;
            this.lb_about2.Location = new System.Drawing.Point(-3, 19);
            this.lb_about2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb_about2.Name = "lb_about2";
            this.lb_about2.Size = new System.Drawing.Size(413, 15);
            this.lb_about2.TabIndex = 0;
            this.lb_about2.Text = "@ design by Vũ Minh Hiếu";
            // 
            // pn_main
            // 
            this.pn_main.Controls.Add(this.banner1);
            this.pn_main.Controls.Add(this.ucMatHang1);
            this.pn_main.Controls.Add(this.ucPhieuXuat1);
            this.pn_main.Controls.Add(this.ucPhieuNhap1);
            this.pn_main.Controls.Add(this.ucNhaCungCap1);
            this.pn_main.Controls.Add(this.ucNhanVien1);
            this.pn_main.Location = new System.Drawing.Point(0, 60);
            this.pn_main.Name = "pn_main";
            this.pn_main.Size = new System.Drawing.Size(1388, 600);
            this.pn_main.TabIndex = 4;
            // 
            // banner1
            // 
            this.banner1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("banner1.BackgroundImage")));
            this.banner1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.banner1.Location = new System.Drawing.Point(0, 0);
            this.banner1.Margin = new System.Windows.Forms.Padding(0);
            this.banner1.Name = "banner1";
            this.banner1.Size = new System.Drawing.Size(1388, 600);
            this.banner1.TabIndex = 5;
            // 
            // ucMatHang1
            // 
            this.ucMatHang1.BackColor = System.Drawing.Color.White;
            this.ucMatHang1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucMatHang1.BackgroundImage")));
            this.ucMatHang1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucMatHang1.Location = new System.Drawing.Point(0, 0);
            this.ucMatHang1.Margin = new System.Windows.Forms.Padding(0);
            this.ucMatHang1.Name = "ucMatHang1";
            this.ucMatHang1.Size = new System.Drawing.Size(1388, 600);
            this.ucMatHang1.TabIndex = 4;
            // 
            // ucPhieuXuat1
            // 
            this.ucPhieuXuat1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucPhieuXuat1.BackgroundImage")));
            this.ucPhieuXuat1.Location = new System.Drawing.Point(0, 0);
            this.ucPhieuXuat1.Margin = new System.Windows.Forms.Padding(0);
            this.ucPhieuXuat1.Name = "ucPhieuXuat1";
            this.ucPhieuXuat1.Size = new System.Drawing.Size(1388, 600);
            this.ucPhieuXuat1.TabIndex = 3;
            // 
            // ucPhieuNhap1
            // 
            this.ucPhieuNhap1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ucPhieuNhap1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucPhieuNhap1.BackgroundImage")));
            this.ucPhieuNhap1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucPhieuNhap1.Location = new System.Drawing.Point(0, 0);
            this.ucPhieuNhap1.Margin = new System.Windows.Forms.Padding(0);
            this.ucPhieuNhap1.Name = "ucPhieuNhap1";
            this.ucPhieuNhap1.Size = new System.Drawing.Size(1388, 600);
            this.ucPhieuNhap1.TabIndex = 2;
            // 
            // ucNhaCungCap1
            // 
            this.ucNhaCungCap1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("ucNhaCungCap1.BackgroundImage")));
            this.ucNhaCungCap1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ucNhaCungCap1.Location = new System.Drawing.Point(0, 0);
            this.ucNhaCungCap1.Margin = new System.Windows.Forms.Padding(0);
            this.ucNhaCungCap1.Name = "ucNhaCungCap1";
            this.ucNhaCungCap1.Size = new System.Drawing.Size(1388, 600);
            this.ucNhaCungCap1.TabIndex = 1;
            // 
            // ucNhanVien1
            // 
            this.ucNhanVien1.BackColor = System.Drawing.Color.White;
            this.ucNhanVien1.Location = new System.Drawing.Point(0, 0);
            this.ucNhanVien1.Margin = new System.Windows.Forms.Padding(0);
            this.ucNhanVien1.Name = "ucNhanVien1";
            this.ucNhanVien1.Size = new System.Drawing.Size(1388, 600);
            this.ucNhanVien1.TabIndex = 0;
            this.ucNhanVien1.Visible = false;
            // 
            // bt_toFNghiepVu
            // 
            this.bt_toFNghiepVu.BackColor = System.Drawing.Color.Peru;
            this.bt_toFNghiepVu.FlatAppearance.BorderSize = 0;
            this.bt_toFNghiepVu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bt_toFNghiepVu.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_toFNghiepVu.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.bt_toFNghiepVu.Image = global::S4_N14_VuMinhHieu.Properties.Resources.green_Arrow;
            this.bt_toFNghiepVu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_toFNghiepVu.Location = new System.Drawing.Point(1187, 0);
            this.bt_toFNghiepVu.Margin = new System.Windows.Forms.Padding(0);
            this.bt_toFNghiepVu.Name = "bt_toFNghiepVu";
            this.bt_toFNghiepVu.Size = new System.Drawing.Size(203, 60);
            this.bt_toFNghiepVu.TabIndex = 3;
            this.bt_toFNghiepVu.Text = "xử lý Nghiệp Vụ";
            this.bt_toFNghiepVu.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.bt_toFNghiepVu.UseVisualStyleBackColor = false;
            this.bt_toFNghiepVu.Click += new System.EventHandler(this.bt_toFNghiepVu_Click);
            // 
            // pn_logo
            // 
            this.pn_logo.BackColor = System.Drawing.Color.White;
            this.pn_logo.BackgroundImage = global::S4_N14_VuMinhHieu.Properties.Resources.logo;
            this.pn_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pn_logo.Location = new System.Drawing.Point(2, 0);
            this.pn_logo.Margin = new System.Windows.Forms.Padding(0);
            this.pn_logo.Name = "pn_logo";
            this.pn_logo.Size = new System.Drawing.Size(70, 60);
            this.pn_logo.TabIndex = 3;
            this.pn_logo.Click += new System.EventHandler(this.pn_logo_Click);
            // 
            // fDanhMuc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(1384, 701);
            this.Controls.Add(this.pn_main);
            this.Controls.Add(this.bt_toFNghiepVu);
            this.Controls.Add(this.pn_logo);
            this.Controls.Add(this.pn_about);
            this.Controls.Add(this.pn_Menu);
            this.ForeColor = System.Drawing.Color.Snow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 820);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "fDanhMuc";
            this.Text = "Demo Danh Mục";
            this.Load += new System.EventHandler(this.fDemoDanhMuc_Load);
            this.pn_Menu.ResumeLayout(false);
            this.pn_Menu.PerformLayout();
            this.M.ResumeLayout(false);
            this.M.PerformLayout();
            this.pn_about.ResumeLayout(false);
            this.pn_main.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pn_Menu;
        private System.Windows.Forms.Panel pn_about;
        private System.Windows.Forms.Label lb_about1;
        private System.Windows.Forms.Label lb_about2;
        private System.Windows.Forms.Button bt_toFNghiepVu;
        private System.Windows.Forms.Panel pn_logo;
        private System.Windows.Forms.MenuStrip M;
        private System.Windows.Forms.ToolStripMenuItem tsmi_MatHang;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PhieuNhap;
        private System.Windows.Forms.ToolStripMenuItem tsmi_PhieuXuat;
        private System.Windows.Forms.ToolStripMenuItem tsmi_NhaCungCap;
        private System.Windows.Forms.ToolStripMenuItem tsmi_NhanVien;
        private System.Windows.Forms.Panel pn_main;
        private UserControls.UCNhanVien ucNhanVien1;
        private UserControls.UCNhaCungCap ucNhaCungCap1;
        private UserControls.UCPhieuNhap ucPhieuNhap1;
        private UserControls.UCPhieuXuat ucPhieuXuat1;
        private UserControls.UCMatHang ucMatHang1;
        private UserControls.Banner banner1;
    }
}