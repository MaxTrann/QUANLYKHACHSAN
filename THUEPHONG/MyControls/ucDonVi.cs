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
namespace THUEPHONG.MyControls
{
    public partial class ucDonVi : UserControl
    {
        public ucDonVi()
        {
            InitializeComponent();
        }
        CONGTY _congty;
        DONVI _donvi;

        private void ucDonVi_Load(object sender, EventArgs e)
        {
            _congty = new CONGTY();
            _donvi = new DONVI();
            loadCongTy();
            cboCongTy.Enabled = false;
            cboCongTy.SelectedIndexChanged += CboCongTy_SelectedIndexChanged;
            loadDonVi();
            if (myFunctions._madvi == "~")
            {
                cboDonVi.Enabled = true;
            }
            else
            {
                cboDonVi.SelectedValue = myFunctions._madvi;
                cboDonVi.Enabled = false;
            }
        }

        private void CboCongTy_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadDonVi();
        }

        void loadCongTy()
        {
            cboCongTy.DataSource = _congty.getAll();
            cboCongTy.DisplayMember = "TENCTY";
            cboCongTy.ValueMember = "MACTY";
            cboCongTy.SelectedValue = myFunctions._macty;
        }
        void loadDonVi()
        {
            cboDonVi.DataSource = _donvi.getAll(cboCongTy.SelectedValue.ToString());
            cboDonVi.DisplayMember = "TENDVi";
            cboDonVi.ValueMember = "MADVI";
        }
    }
}
