using BusinessLayer;
using DataLayer;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USERMANAGEMENT
{
    public partial class frmPhanQuyenChucNang : DevExpress.XtraEditors.XtraForm
    {
        public frmPhanQuyenChucNang()
        {
            InitializeComponent();
        }
        public int _idUser;
        public string _macty;
        public string _madvi;

        SYS_USER _sysUser;
        SYS_RIGHT _sysRight;
        private void frmPhanQuyenChucNang_Load(object sender, EventArgs e)
        {
            
            _sysUser = new SYS_USER();
            _sysRight = new SYS_RIGHT();
            loadUsers();
            loadFuncByUser();

            gvChucNang.RowStyle += GvChucNang_RowStyle;
        }

        private void GvChucNang_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                bool isRed = Convert.ToBoolean(view.GetRowCellValue(e.RowHandle, view.Columns["ISGROUP"]));
                if (isRed)
                {
                    e.Appearance.BackColor = Color.DeepSkyBlue;
                    e.Appearance.ForeColor = Color.White;
                    e.Appearance.Font = new Font("Tahoma", 12, FontStyle.Bold);
                }
            }
        }

        void loadUsers()
        {
            if (_macty == null && _madvi == null)
            {
                gcUsers.DataSource = _sysUser.getUserbyDViFunc("CTME", "~");
                gvUsers.OptionsBehavior.Editable = false;
            }
            else
            {
                gcUsers.DataSource = _sysUser.getUserbyDViFunc(_macty, _madvi);
                gvUsers.OptionsBehavior.Editable = false;
            }
        }
        void loadFuncByUser()
        {
            VIEW_FUNC_SYS_RIGHT _vFuncRight = new VIEW_FUNC_SYS_RIGHT();
            gcChucNang.DataSource = _vFuncRight.getFuncByUser(_idUser);
            gvChucNang.OptionsBehavior.Editable = false;

            for (int i = 0; i < gvUsers.RowCount; i++)
            {
                if (int.Parse(gvUsers.GetRowCellValue(i, "IDUSER").ToString()) == _idUser)
                {
                    gvUsers.ClearSelection();
                    gvUsers.FocusedRowHandle = i;
                }
            }
            gvChucNang.ClearSelection();
        }

        private void gvUsers_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Name == "ISGROUP" && bool.Parse(e.CellValue.ToString()) == true)
            {
                Image img = Properties.Resources.Team_16x16;
                int x = e.Bounds.X + (e.Bounds.Width - img.Width) / 2;
                int y = e.Bounds.Y + (e.Bounds.Height - img.Height) / 2;
                e.Graphics.DrawImage(img, x - 4, y - 2);
                e.Handled = true;
            }
            if (e.Column.Name == "ISGROUP" && bool.Parse(e.CellValue.ToString()) == false)
            {
                Image img = Properties.Resources.Customer_16x16;
                int x = e.Bounds.X + (e.Bounds.Width - img.Width) / 2;
                int y = e.Bounds.Y + (e.Bounds.Height - img.Height) / 2;
                e.Graphics.DrawImage(img, x, y);
                e.Handled = true;
            }

        }

        private void mnCamQuyen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysRight.update(_idUser, gvChucNang.GetRowCellValue(i, "FUNC_CODE").ToString(), 0);
                }
            }
            loadFuncByUser();
        }

        private void mnChiXem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysRight.update(_idUser, gvChucNang.GetRowCellValue(i, "FUNC_CODE").ToString(), 1);
                }
            }
            loadFuncByUser();
        }

        private void mnToanQuyen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysRight.update(_idUser, gvChucNang.GetRowCellValue(i, "FUNC_CODE").ToString(), 2);
                }
            }
            loadFuncByUser();
        }

        private void gvUsers_Click(object sender, EventArgs e)
        {
            _idUser = int.Parse(gvUsers.GetFocusedRowCellValue("IDUSER").ToString());
            loadFuncByUser();
        }
    }
}