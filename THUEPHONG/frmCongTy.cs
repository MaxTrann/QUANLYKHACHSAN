using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using CrystalDecisions.Windows.Forms;





namespace THUEPHONG
{
    public partial class frmCongTy : DevExpress.XtraEditors.XtraForm
    {
        public frmCongTy()
        {
            InitializeComponent();
        }
        CONGTY _congty;
        bool _them;
        string _macty;
        private void frmCongTy_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            loadData();
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
        }
        void showHideControl(bool t)
        {
            btnThem.Visible = t;
            btnSua.Visible = t;
            btnXoa.Visible = t;
            btnThoat.Visible = t;
            btnLuu.Visible = !t;
            btnBoQua.Visible = !t;
        }

        void _enabled(bool t)
        {
            txtTen.Enabled = t;
            txtDienThoai.Enabled = t;
            txtFax.Enabled = t;
            txtEmail.Enabled = t;
            txtDiaChi.Enabled = t;
            chkDisabled.Enabled = t;
        }
        void _reset()
        {
            txtMa.Text = "";
            txtTen.Text = "";
            txtDienThoai.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";

            chkDisabled.Checked = false;
        }
        void loadData()
        {
            gcDanhSach.DataSource = _congty.getAll();
            gvDanhSach.OptionsBehavior.Editable = false;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            _them = true;
            showHideControl(false);
            _enabled(true);
            _reset();
            txtMa.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            _them = false;
            _enabled(true);
            txtMa.Enabled = false;
            showHideControl(false);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _congty.delete(_macty);
            }
            loadData();
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (_them)
            {
                tb_CongTy cty = new tb_CongTy();
                cty.MACTY = txtMa.Text;
                cty.TENCTY = txtTen.Text;
                cty.DIACHI = txtDiaChi.Text;
                cty.SDT = txtDienThoai.Text;
                cty.FAX = txtFax.Text;
                cty.EMAIL = txtEmail.Text;
                cty.DISABLED = chkDisabled.Checked;
                _congty.add(cty);

            }
            else
            {
                tb_CongTy cty = _congty.getItem(_macty);
                cty.TENCTY = txtTen.Text;
                cty.DIACHI = txtDiaChi.Text;
                cty.SDT = txtDienThoai.Text;
                cty.FAX = txtFax.Text;
                cty.EMAIL = txtEmail.Text;
                cty.DISABLED = chkDisabled.Checked;
                _congty.update(cty);
            }
            _them = false;
            loadData();
            _enabled(false);
            showHideControl(true);
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            _them = false;
            showHideControl(true);
            _enabled(false);
            txtMa.Enabled = false;
            _reset();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvDanhSach_Click(object sender, EventArgs e)
        {
            if (gvDanhSach.RowCount > 0)
            {
                _macty = gvDanhSach.GetFocusedRowCellValue("MACTY").ToString();
                txtMa.Text = gvDanhSach.GetFocusedRowCellValue("MACTY").ToString();
                txtTen.Text = gvDanhSach.GetFocusedRowCellValue("TENCTY").ToString();
                txtDienThoai.Text = gvDanhSach.GetFocusedRowCellValue("SDT").ToString();
                txtFax.Text = gvDanhSach.GetFocusedRowCellValue("FAX").ToString();
                txtEmail.Text = gvDanhSach.GetFocusedRowCellValue("EMAIL").ToString();
                txtDiaChi.Text = gvDanhSach.GetFocusedRowCellValue("DIACHI").ToString();
                chkDisabled.Checked = bool.Parse(gvDanhSach.GetFocusedRowCellValue("DISABLED").ToString());
            }
        }

        private void gvDanhSach_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.del_icon_28px;
                e.Graphics.DrawImage(img, e.Bounds.X, e.Bounds.Y);
                e.Handled = true;

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            XuatReport("DM_CONGTY", "DANH MỤC CÔNG TY");
        }
        private void XuatReport(string _rpName, string _rpTitle)
        {
            if (_macty != null)
            {
                Form frm = new Form();
                CrystalReportViewer crv = new CrystalReportViewer();
                crv.ShowGroupTreeButton = false;
                crv.ShowParameterPanelButton = false;
                crv.ToolPanelView = ToolPanelViewType.None;
                TableLogOnInfo Thongtin;
                ReportDocument doc = new ReportDocument();
                doc.Load(System.Windows.Forms.Application.StartupPath + "\\Reports\\" + _rpName + ".rpt");
                Thongtin = doc.Database.Tables[0].LogOnInfo;
                Thongtin.ConnectionInfo.ServerName = myFunctions._srv;
                Thongtin.ConnectionInfo.DatabaseName = myFunctions._db;
                Thongtin.ConnectionInfo.UserID = myFunctions._us;
                Thongtin.ConnectionInfo.Password = myFunctions._pw;
                doc.Database.Tables[0].ApplyLogOnInfo(Thongtin);
                try
                {
                    doc.SetParameterValue("macty", _macty.ToString());
                    crv.Dock = DockStyle.Fill;
                    crv.ReportSource = doc;
                    crv.Refresh();
                    frm.Controls.Add(crv);
                    frm.Text = _rpTitle;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.ToString());
                }
            }
            else
            {
                MessageBox.Show("Không có dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}