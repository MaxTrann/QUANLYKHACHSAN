namespace THUEPHONG
{
    partial class frmDatPhongDon
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDatPhongDon));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnLuu = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnThoat = new System.Windows.Forms.ToolStripButton();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl5 = new DevExpress.XtraEditors.GroupControl();
            this.label10 = new System.Windows.Forms.Label();
            this.txtThanhTien = new System.Windows.Forms.TextBox();
            this.lblThanhToan = new System.Windows.Forms.Label();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.gcSPDV = new DevExpress.XtraGrid.GridControl();
            this.gvSPDV = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.spIDSP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spTENPHONG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spTENSP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spIDPHONG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spSOLUONG = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spDONGIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spTHANHTIEN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.searchKH = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IDKH = new DevExpress.XtraGrid.Columns.GridColumn();
            this.HOTEN = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblPhong = new System.Windows.Forms.Label();
            this.spSoNguoi = new DevExpress.XtraEditors.SpinEdit();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAddNew = new DevExpress.XtraEditors.SimpleButton();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dtNgayTra = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtNgayDat = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gcSanPham = new DevExpress.XtraGrid.GridControl();
            this.gvSanPham = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IDSP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.TENSP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.DONGIA = new DevExpress.XtraGrid.Columns.GridColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).BeginInit();
            this.groupControl5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSPDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSPDV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchKH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSoNguoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSanPham)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLuu,
            this.btnPrint,
            this.btnThoat});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1142, 47);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnLuu
            // 
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(37, 44);
            this.btnLuu.Text = "Lưu";
            this.btnLuu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(29, 44);
            this.btnPrint.Text = "In";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Image = ((System.Drawing.Image)(resources.GetObject("btnThoat.Image")));
            this.btnThoat.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(51, 44);
            this.btnThoat.Text = "Thoát";
            this.btnThoat.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 47);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl5);
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl4);
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1142, 732);
            this.splitContainerControl1.SplitterPosition = 737;
            this.splitContainerControl1.TabIndex = 2;
            // 
            // groupControl5
            // 
            this.groupControl5.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.groupControl5.AppearanceCaption.ForeColor = System.Drawing.Color.Red;
            this.groupControl5.AppearanceCaption.Options.UseFont = true;
            this.groupControl5.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl5.Controls.Add(this.label10);
            this.groupControl5.Controls.Add(this.txtThanhTien);
            this.groupControl5.Controls.Add(this.lblThanhToan);
            this.groupControl5.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl5.Location = new System.Drawing.Point(0, 607);
            this.groupControl5.Name = "groupControl5";
            this.groupControl5.Size = new System.Drawing.Size(737, 125);
            this.groupControl5.TabIndex = 3;
            this.groupControl5.Text = "TỔNG THANH TOÁN";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.Red;
            this.label10.Location = new System.Drawing.Point(425, 48);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(61, 24);
            this.label10.TabIndex = 2;
            this.label10.Text = "đồng";
            // 
            // txtThanhTien
            // 
            this.txtThanhTien.BackColor = System.Drawing.Color.White;
            this.txtThanhTien.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThanhTien.ForeColor = System.Drawing.Color.Red;
            this.txtThanhTien.Location = new System.Drawing.Point(147, 44);
            this.txtThanhTien.Name = "txtThanhTien";
            this.txtThanhTien.ReadOnly = true;
            this.txtThanhTien.Size = new System.Drawing.Size(274, 32);
            this.txtThanhTien.TabIndex = 1;
            this.txtThanhTien.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblThanhToan
            // 
            this.lblThanhToan.AutoSize = true;
            this.lblThanhToan.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblThanhToan.ForeColor = System.Drawing.Color.Red;
            this.lblThanhToan.Location = new System.Drawing.Point(19, 48);
            this.lblThanhToan.Name = "lblThanhToan";
            this.lblThanhToan.Size = new System.Drawing.Size(122, 24);
            this.lblThanhToan.TabIndex = 0;
            this.lblThanhToan.Text = "TỔNG TIỀN";
            // 
            // groupControl4
            // 
            this.groupControl4.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.groupControl4.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.groupControl4.AppearanceCaption.Options.UseFont = true;
            this.groupControl4.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl4.Controls.Add(this.gcSPDV);
            this.groupControl4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl4.Location = new System.Drawing.Point(0, 275);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(737, 332);
            this.groupControl4.TabIndex = 2;
            this.groupControl4.Text = "Danh sách Sản phẩm - Dịch vụ";
            // 
            // gcSPDV
            // 
            this.gcSPDV.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSPDV.Location = new System.Drawing.Point(2, 28);
            this.gcSPDV.MainView = this.gvSPDV;
            this.gcSPDV.Name = "gcSPDV";
            this.gcSPDV.Size = new System.Drawing.Size(733, 302);
            this.gcSPDV.TabIndex = 1;
            this.gcSPDV.UseDisabledStatePainter = false;
            this.gcSPDV.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSPDV});
            // 
            // gvSPDV
            // 
            this.gvSPDV.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.spIDSP,
            this.spTENPHONG,
            this.spTENSP,
            this.spIDPHONG,
            this.spSOLUONG,
            this.spDONGIA,
            this.spTHANHTIEN});
            this.gvSPDV.GridControl = this.gcSPDV;
            this.gvSPDV.Name = "gvSPDV";
            this.gvSPDV.OptionsView.ShowFooter = true;
            this.gvSPDV.OptionsView.ShowGroupPanel = false;
            this.gvSPDV.HiddenEditor += new System.EventHandler(this.gvSPDV_HiddenEditor);
            this.gvSPDV.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvSPDV_CellValueChanged);
            // 
            // spIDSP
            // 
            this.spIDSP.Caption = "IDSP";
            this.spIDSP.FieldName = "IDSP";
            this.spIDSP.MinWidth = 25;
            this.spIDSP.Name = "spIDSP";
            this.spIDSP.Width = 94;
            // 
            // spTENPHONG
            // 
            this.spTENPHONG.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.spTENPHONG.AppearanceHeader.Options.UseFont = true;
            this.spTENPHONG.Caption = "PHÒNG";
            this.spTENPHONG.FieldName = "TENPHONG";
            this.spTENPHONG.MaxWidth = 100;
            this.spTENPHONG.MinWidth = 80;
            this.spTENPHONG.Name = "spTENPHONG";
            this.spTENPHONG.OptionsColumn.AllowEdit = false;
            this.spTENPHONG.Visible = true;
            this.spTENPHONG.VisibleIndex = 0;
            this.spTENPHONG.Width = 94;
            // 
            // spTENSP
            // 
            this.spTENSP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.spTENSP.AppearanceHeader.Options.UseFont = true;
            this.spTENSP.Caption = "TÊN SP - DV";
            this.spTENSP.FieldName = "TENSP";
            this.spTENSP.MaxWidth = 150;
            this.spTENSP.MinWidth = 120;
            this.spTENSP.Name = "spTENSP";
            this.spTENSP.OptionsColumn.AllowEdit = false;
            this.spTENSP.Visible = true;
            this.spTENSP.VisibleIndex = 1;
            this.spTENSP.Width = 120;
            // 
            // spIDPHONG
            // 
            this.spIDPHONG.Caption = "IDPHONG";
            this.spIDPHONG.FieldName = "IDPHONG";
            this.spIDPHONG.MinWidth = 25;
            this.spIDPHONG.Name = "spIDPHONG";
            this.spIDPHONG.Width = 94;
            // 
            // spSOLUONG
            // 
            this.spSOLUONG.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.spSOLUONG.AppearanceHeader.Options.UseFont = true;
            this.spSOLUONG.Caption = "SL";
            this.spSOLUONG.FieldName = "SOLUONG";
            this.spSOLUONG.MaxWidth = 80;
            this.spSOLUONG.MinWidth = 70;
            this.spSOLUONG.Name = "spSOLUONG";
            this.spSOLUONG.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "SOLUONG", "{0:0.##}")});
            this.spSOLUONG.Visible = true;
            this.spSOLUONG.VisibleIndex = 2;
            this.spSOLUONG.Width = 80;
            // 
            // spDONGIA
            // 
            this.spDONGIA.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.spDONGIA.AppearanceHeader.Options.UseFont = true;
            this.spDONGIA.Caption = "ĐƠN GIÁ";
            this.spDONGIA.DisplayFormat.FormatString = "n0";
            this.spDONGIA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spDONGIA.FieldName = "DONGIA";
            this.spDONGIA.MaxWidth = 100;
            this.spDONGIA.MinWidth = 80;
            this.spDONGIA.Name = "spDONGIA";
            this.spDONGIA.OptionsColumn.AllowEdit = false;
            this.spDONGIA.Visible = true;
            this.spDONGIA.VisibleIndex = 3;
            this.spDONGIA.Width = 94;
            // 
            // spTHANHTIEN
            // 
            this.spTHANHTIEN.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.spTHANHTIEN.AppearanceHeader.Options.UseFont = true;
            this.spTHANHTIEN.Caption = "THÀNH TIỀN";
            this.spTHANHTIEN.DisplayFormat.FormatString = "n0";
            this.spTHANHTIEN.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spTHANHTIEN.FieldName = "THANHTIEN";
            this.spTHANHTIEN.MaxWidth = 120;
            this.spTHANHTIEN.MinWidth = 100;
            this.spTHANHTIEN.Name = "spTHANHTIEN";
            this.spTHANHTIEN.OptionsColumn.AllowEdit = false;
            this.spTHANHTIEN.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "THANHTIEN", "{0:n0}")});
            this.spTHANHTIEN.Visible = true;
            this.spTHANHTIEN.VisibleIndex = 4;
            this.spTHANHTIEN.Width = 100;
            // 
            // groupControl1
            // 
            this.groupControl1.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupControl1.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.groupControl1.AppearanceCaption.Options.UseFont = true;
            this.groupControl1.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl1.Controls.Add(this.searchKH);
            this.groupControl1.Controls.Add(this.lblPhong);
            this.groupControl1.Controls.Add(this.spSoNguoi);
            this.groupControl1.Controls.Add(this.label9);
            this.groupControl1.Controls.Add(this.btnAddNew);
            this.groupControl1.Controls.Add(this.txtGhiChu);
            this.groupControl1.Controls.Add(this.label7);
            this.groupControl1.Controls.Add(this.cboTrangThai);
            this.groupControl1.Controls.Add(this.label6);
            this.groupControl1.Controls.Add(this.dtNgayTra);
            this.groupControl1.Controls.Add(this.label2);
            this.groupControl1.Controls.Add(this.dtNgayDat);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.label1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(737, 275);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "Thông tin khách hàng";
            // 
            // searchKH
            // 
            this.searchKH.EditValue = "";
            this.searchKH.Location = new System.Drawing.Point(198, 77);
            this.searchKH.Name = "searchKH";
            this.searchKH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.searchKH.Properties.NullText = "";
            this.searchKH.Properties.PopupView = this.searchLookUpEdit1View;
            this.searchKH.Size = new System.Drawing.Size(399, 22);
            this.searchKH.TabIndex = 38;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IDKH,
            this.HOTEN});
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // IDKH
            // 
            this.IDKH.Caption = "ID";
            this.IDKH.FieldName = "IDKH";
            this.IDKH.MaxWidth = 50;
            this.IDKH.MinWidth = 30;
            this.IDKH.Name = "IDKH";
            this.IDKH.Visible = true;
            this.IDKH.VisibleIndex = 0;
            this.IDKH.Width = 50;
            // 
            // HOTEN
            // 
            this.HOTEN.Caption = "HỌ TÊN KHÁCH HÀNG";
            this.HOTEN.FieldName = "HOTEN";
            this.HOTEN.MaxWidth = 200;
            this.HOTEN.MinWidth = 180;
            this.HOTEN.Name = "HOTEN";
            this.HOTEN.Visible = true;
            this.HOTEN.VisibleIndex = 1;
            this.HOTEN.Width = 180;
            // 
            // lblPhong
            // 
            this.lblPhong.AutoSize = true;
            this.lblPhong.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPhong.ForeColor = System.Drawing.Color.Red;
            this.lblPhong.Location = new System.Drawing.Point(193, 38);
            this.lblPhong.Name = "lblPhong";
            this.lblPhong.Size = new System.Drawing.Size(74, 24);
            this.lblPhong.TabIndex = 37;
            this.lblPhong.Text = "Phòng";
            // 
            // spSoNguoi
            // 
            this.spSoNguoi.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spSoNguoi.Location = new System.Drawing.Point(197, 163);
            this.spSoNguoi.Name = "spSoNguoi";
            this.spSoNguoi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spSoNguoi.Size = new System.Drawing.Size(149, 24);
            this.spSoNguoi.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(110, 165);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(74, 21);
            this.label9.TabIndex = 35;
            this.label9.Text = "Số người";
            // 
            // btnAddNew
            // 
            this.btnAddNew.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddNew.ImageOptions.Image")));
            this.btnAddNew.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            this.btnAddNew.Location = new System.Drawing.Point(603, 73);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(28, 30);
            this.btnAddNew.TabIndex = 33;
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(197, 208);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(434, 23);
            this.txtGhiChu.TabIndex = 32;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(119, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 21);
            this.label7.TabIndex = 31;
            this.label7.Text = "Ghi chú";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(482, 163);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(149, 24);
            this.cboTrangThai.TabIndex = 29;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(390, 165);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 21);
            this.label6.TabIndex = 28;
            this.label6.Text = "Trạng thái";
            // 
            // dtNgayTra
            // 
            this.dtNgayTra.CustomFormat = "dd/MM/yyyy";
            this.dtNgayTra.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayTra.Location = new System.Drawing.Point(482, 120);
            this.dtNgayTra.Name = "dtNgayTra";
            this.dtNgayTra.Size = new System.Drawing.Size(149, 23);
            this.dtNgayTra.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(403, 121);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 21);
            this.label2.TabIndex = 26;
            this.label2.Text = "Ngày trả";
            // 
            // dtNgayDat
            // 
            this.dtNgayDat.CustomFormat = "dd/MM/yyyy";
            this.dtNgayDat.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtNgayDat.Location = new System.Drawing.Point(197, 120);
            this.dtNgayDat.Name = "dtNgayDat";
            this.dtNgayDat.Size = new System.Drawing.Size(149, 23);
            this.dtNgayDat.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(107, 121);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 21);
            this.label5.TabIndex = 24;
            this.label5.Text = "Ngày đặt";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(95, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 21);
            this.label1.TabIndex = 22;
            this.label1.Text = "Khách hàng";
            // 
            // groupControl2
            // 
            this.groupControl2.AppearanceCaption.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.groupControl2.AppearanceCaption.ForeColor = System.Drawing.Color.Blue;
            this.groupControl2.AppearanceCaption.Options.UseFont = true;
            this.groupControl2.AppearanceCaption.Options.UseForeColor = true;
            this.groupControl2.Controls.Add(this.gcSanPham);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(393, 732);
            this.groupControl2.TabIndex = 3;
            this.groupControl2.Text = "Sản phẩm - Dịch vụ";
            // 
            // gcSanPham
            // 
            this.gcSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSanPham.Location = new System.Drawing.Point(2, 28);
            this.gcSanPham.MainView = this.gvSanPham;
            this.gcSanPham.Name = "gcSanPham";
            this.gcSanPham.Size = new System.Drawing.Size(389, 702);
            this.gcSanPham.TabIndex = 0;
            this.gcSanPham.UseDisabledStatePainter = false;
            this.gcSanPham.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSanPham});
            this.gcSanPham.DoubleClick += new System.EventHandler(this.gcSanPham_DoubleClick);
            // 
            // gvSanPham
            // 
            this.gvSanPham.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IDSP,
            this.TENSP,
            this.DONGIA});
            this.gvSanPham.GridControl = this.gcSanPham;
            this.gvSanPham.Name = "gvSanPham";
            this.gvSanPham.OptionsView.ShowGroupPanel = false;
            this.gvSanPham.DoubleClick += new System.EventHandler(this.gvSanPham_DoubleClick);
            // 
            // IDSP
            // 
            this.IDSP.Caption = "IDSP";
            this.IDSP.FieldName = "IDSP";
            this.IDSP.MinWidth = 25;
            this.IDSP.Name = "IDSP";
            this.IDSP.Width = 94;
            // 
            // TENSP
            // 
            this.TENSP.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.TENSP.AppearanceHeader.Options.UseFont = true;
            this.TENSP.Caption = "TÊN SP - DV";
            this.TENSP.FieldName = "TENSP";
            this.TENSP.MaxWidth = 150;
            this.TENSP.MinWidth = 120;
            this.TENSP.Name = "TENSP";
            this.TENSP.Visible = true;
            this.TENSP.VisibleIndex = 0;
            this.TENSP.Width = 120;
            // 
            // DONGIA
            // 
            this.DONGIA.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Bold);
            this.DONGIA.AppearanceHeader.Options.UseFont = true;
            this.DONGIA.Caption = "ĐƠN GIÁ";
            this.DONGIA.DisplayFormat.FormatString = "n0";
            this.DONGIA.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.DONGIA.FieldName = "DONGIA";
            this.DONGIA.MaxWidth = 120;
            this.DONGIA.MinWidth = 100;
            this.DONGIA.Name = "DONGIA";
            this.DONGIA.Visible = true;
            this.DONGIA.VisibleIndex = 1;
            this.DONGIA.Width = 100;
            // 
            // frmDatPhongDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1142, 779);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmDatPhongDon";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đặt phòng khách lẻ";
            this.Load += new System.EventHandler(this.frmDatPhongDon_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl5)).EndInit();
            this.groupControl5.ResumeLayout(false);
            this.groupControl5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSPDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSPDV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.searchKH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spSoNguoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSanPham)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnLuu;
        private System.Windows.Forms.ToolStripButton btnPrint;
        private System.Windows.Forms.ToolStripButton btnThoat;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.Label lblPhong;
        private DevExpress.XtraEditors.SpinEdit spSoNguoi;
        private System.Windows.Forms.Label label9;
        private DevExpress.XtraEditors.SimpleButton btnAddNew;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker dtNgayTra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dtNgayDat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl gcSanPham;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn IDSP;
        private DevExpress.XtraGrid.Columns.GridColumn TENSP;
        private DevExpress.XtraGrid.Columns.GridColumn DONGIA;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraGrid.GridControl gcSPDV;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSPDV;
        private DevExpress.XtraGrid.Columns.GridColumn spIDSP;
        private DevExpress.XtraGrid.Columns.GridColumn spTENPHONG;
        private DevExpress.XtraGrid.Columns.GridColumn spTENSP;
        private DevExpress.XtraGrid.Columns.GridColumn spIDPHONG;
        private DevExpress.XtraGrid.Columns.GridColumn spSOLUONG;
        private DevExpress.XtraGrid.Columns.GridColumn spDONGIA;
        private DevExpress.XtraGrid.Columns.GridColumn spTHANHTIEN;
        private DevExpress.XtraEditors.GroupControl groupControl5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtThanhTien;
        private System.Windows.Forms.Label lblThanhToan;
        private DevExpress.XtraEditors.SearchLookUpEdit searchKH;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn IDKH;
        private DevExpress.XtraGrid.Columns.GridColumn HOTEN;
    }
}