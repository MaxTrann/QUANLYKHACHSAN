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
using BusinessLayer;
using DataLayer;
using THUEPHONG.MyControls;

namespace THUEPHONG
{
    public partial class frmBaoCao : DevExpress.XtraEditors.XtraForm
    {
        public frmBaoCao()
        {
            InitializeComponent();
        }
        public frmBaoCao(tb_SYS_USER user)
        {
            InitializeComponent();
            this._user = user;
        }

        tb_SYS_USER _user;
        SYS_USER _sysUser;
        SYS_REPORT _sysReport;
        SYS_RIGHT_REP _sysRightRep;


        Panel _panel;
        ucTuNgay _uTuNgay;
        ucCongTy _uCongTy;
        ucDonVi _uDonVi;
        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            _sysReport = new SYS_REPORT();
            _sysUser = new SYS_USER();
            _sysRightRep = new SYS_RIGHT_REP();

            lstDanhSach.DataSource = _sysReport.getListByRight(_sysRightRep.getListByUser(_user.IDUSER));
            lstDanhSach.DisplayMember = "DESCRIPTION";
            lstDanhSach.ValueMember = "REP_CODE";
            lstDanhSach.SelectedIndexChanged += LstDanhSach_SelectedIndexChanged;
            loadUserControls();
        }

        private void LstDanhSach_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadUserControls();
        }

        void loadUserControls()
        {
            tb_SYS_REPORT rep = _sysReport.getItem(int.Parse(lstDanhSach.SelectedValue.ToString()));
            if (_panel != null)
            {
                _panel.Dispose();
            }
            _panel = new Panel();
            _panel.Dock = DockStyle.Top;
            _panel.MinimumSize = new Size(_panel.Width, 500);
            List<Control> _ctrl = new List<Control>();
            if (rep.TUNGAY == true)
            {
                _uTuNgay = new ucTuNgay();
                _uTuNgay.Dock = DockStyle.Top;
                _ctrl.Add(_uTuNgay);    

            }
            if (rep.MACTY == true)
            {
                _uCongTy = new ucCongTy();
                _uCongTy.Dock = DockStyle.Top;
                _ctrl.Add(_uCongTy);

            }
            if (rep.MADVI == true)
            {
                _uDonVi = new ucDonVi();
                _uDonVi.Dock = DockStyle.Top;
                _ctrl.Add(_uDonVi);

            }
            _ctrl.Reverse();
            _panel.Controls.AddRange(_ctrl.ToArray());
            this.splBaoCao.Panel2.Controls.Add(_panel);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}