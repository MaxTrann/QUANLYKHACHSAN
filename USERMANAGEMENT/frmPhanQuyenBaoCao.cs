using BusinessLayer;
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

namespace USERMANAGEMENT
{
    public partial class frmPhanQuyenBaoCao : DevExpress.XtraEditors.XtraForm
    {
        public frmPhanQuyenBaoCao()
        {
            InitializeComponent();
        }
        public int _idUser;
        public string _macty;
        public string _madvi;

        SYS_USER _sysUser;
        SYS_RIGHT_REP _sysRightRep; 
        private void frmPhanQuyenBaoCao_Load(object sender, EventArgs e)
        {
            _sysUser = new SYS_USER();
            _sysRightRep = new SYS_RIGHT_REP();
            loadUsers();    
            loadRepByUser();
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

        void loadRepByUser()
        {
            VIEW_REP_SYS_RIGHT_REP _vFuncRight = new VIEW_REP_SYS_RIGHT_REP();
            gcChucNang.DataSource = _vFuncRight.getRepByUser(_idUser);
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
                    _sysRightRep.update(_idUser,int.Parse(gvChucNang.GetRowCellValue(i, "REP_CODE").ToString()),false);
                }
            }
            loadRepByUser();
        }

        private void mnToanQuyen_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gvChucNang.RowCount; i++)
            {
                if (gvChucNang.IsRowSelected(i))
                {
                    _sysRightRep.update(_idUser, int.Parse(gvChucNang.GetRowCellValue(i, "REP_CODE").ToString()), true);
                }
            }
            loadRepByUser();
        }

        private void gvUsers_Click(object sender, EventArgs e)
        {
            _idUser = int.Parse(gvUsers.GetFocusedRowCellValue("IDUSER").ToString());
            loadRepByUser();
        }
    }
}