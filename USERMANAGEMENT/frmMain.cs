using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusinessLayer;
using USERMANAGEMENT.MyComponents;

namespace USERMANAGEMENT
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        public frmMain()
        {
            InitializeComponent();
        }
        MyTreeViewCombo _treeView;
        CONGTY _congty;
        DONVI _donvi;
        SYS_USER _sysUser;
        bool _isRoot;
        string _macty;
        string _madvi;
        private void frmMain_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            _sysUser = new SYS_USER();
            _isRoot = true;
            loadTreeView();
            loadUser("CTME", "~");
        }
        public void loadUser(string macty, string madvi)
        {
            _sysUser = new SYS_USER();
            gcUser.DataSource = _sysUser.getUserbyDVI(macty, madvi);
            gvUser.OptionsBehavior.Editable = false;
        }
        void loadTreeView()
        {
            _treeView = new MyTreeViewCombo(pnNhom.Width, 400);
            _treeView.Font = new Font("Tahoma", 10, FontStyle.Bold);
            var lstCTY = _congty.getAll();
            foreach ( var item in lstCTY )
            {
                TreeNode parentNode = new TreeNode();
                parentNode.Text = item.MACTY + " - " + item.TENCTY;
                parentNode.Tag = item.MACTY;
                parentNode.Name = item.MACTY;
                foreach ( var dv in _donvi.getAll(item.MACTY))
                {
                    TreeNode childNode = new TreeNode();
                    childNode.Text = dv.MADVI + " - " + dv.TENDVI;
                    childNode.Tag = dv.MACTY + "." + dv.MADVI;
                    childNode.Name = dv.MACTY + "." + dv.MADVI; //CT01.CTKSNW
                    // Thêm child vào parent
                    parentNode.Nodes.Add(childNode);
                }
                // Sau khi đã thêm xong childNode, thêm parentNode vào TreeView
                _treeView.TreeView.Nodes.Add(parentNode);
            }
            _treeView.TreeView.ExpandAll();
            pnNhom.Controls.Add( _treeView );   
            _treeView.Width = pnNhom.Width;
            _treeView.Height = pnNhom.Height;
            _treeView.TreeView.AfterSelect += TreeView_AfterSelect;
            _treeView.TreeView.Click += TreeView_Click;
        }
        private void TreeView_Click( object sender, EventArgs e)
        {
            _treeView.dropDown.Focus();
            _treeView.SelectAll();
        }
        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _treeView.Text = _treeView.TreeView.SelectedNode.Text;
            if (_treeView.TreeView.SelectedNode.Parent == null)
            {
                _isRoot = true;
                _macty = _treeView.TreeView.SelectedNode.Tag.ToString();
                _madvi = "~"; // văn phòng hành chánh, tuyển dụng nhân viên
            }
            else
            {
                _isRoot = false;
                _macty = _treeView.TreeView.SelectedNode.Name.Substring(0,4);
                _madvi = _treeView.TreeView.SelectedNode.Name.Substring(5);
            }
            loadUser(_macty, _madvi);
            _treeView.dropDown.Close();
            
        }

        private void btnGroup_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_treeView.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Đơn vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmGroup frm = new frmGroup();
            frm._them = true;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (_treeView.Text == "")
            {
                MessageBox.Show("Vui lòng chọn Đơn vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            frmUser frm = new frmUser();
            frm._them = true;
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
            frm.ShowDialog();
        }

        private void btnCapNhat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gvUser.RowCount > 0 && gvUser.GetFocusedRowCellValue("ISGROUP").Equals(true))
            {
                frmGroup frm = new frmGroup();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
            else
            {
                frmUser frm = new frmUser();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
        }

        private void btnChucNang_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPhanQuyenChucNang frm = new frmPhanQuyenChucNang();
            frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
            frm._macty = _macty;
            frm._madvi = _madvi;
            frm.ShowDialog();
        }

        private void btnBaoCao_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void btnThoat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void gvUser_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "ISGROUP" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.Team_16x16;
                int x = e.Bounds.X + (e.Bounds.Width - img.Width) / 2;
                int y = e.Bounds.Y + (e.Bounds.Height - img.Height) / 2;
                e.Graphics.DrawImage(img, x - 5, y - 3);
                e.Handled = true;
            }
            if (e.Column.Name == "ISGROUP" && bool.Parse(e.CellValue.ToString()) == false)
            {
                Image img = Properties.Resources.Customer_16x16;
                int x = e.Bounds.X + (e.Bounds.Width - img.Width) / 2;
                int y = e.Bounds.Y + (e.Bounds.Height - img.Height) / 2;
                e.Graphics.DrawImage(img, x - 1, y);
                e.Handled = true;
            }
            if (e.Column.Name == "DISABLED" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.del_icon_28px;
                int x = e.Bounds.X + (e.Bounds.Width - img.Width) / 2;
                int y = e.Bounds.Y + (e.Bounds.Height - img.Height) / 2;
                e.Graphics.DrawImage(img, x - 1, y);
                e.Handled = true;
            }
        }
        private void gvUser_DoubleClick(object sender, EventArgs e)
        {
            if (gvUser.RowCount > 0 && gvUser.GetFocusedRowCellValue("ISGROUP").Equals(true))
            {
                frmGroup frm = new frmGroup();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
            else
            {
                frmUser frm = new frmUser();
                frm._them = false;
                frm._idUser = int.Parse(gvUser.GetFocusedRowCellValue("IDUSER").ToString());
                frm.ShowDialog();
            }
        }
    }
}
