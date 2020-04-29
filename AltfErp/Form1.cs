using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AltfErp;

namespace AltfErp
{
    public partial class frmPrincipal : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public frmPrincipal()
        {
            InitializeComponent();
            string sql = String.Format(@"SELECT * FROM PROPRIETARIO");
            this.Text = MetodosSql.GetField(sql, "NOMEFANTASIA");

        }
        private void SalvaLayout(object sender, EventArgs args)
        {
            
        }
      
     

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            frmVisaoProdutos frm = new frmVisaoProdutos();
            frm.MdiParent = this;
            
            frm.Show();
            
        }

     

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoClientes frm = new frmVisaoClientes();
            frm.MdiParent = this;
            frm.Show();
            

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoProdutos frm = new frmVisaoProdutos();
            frm.MdiParent = this;
            
            frm.Show();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoCiaSeguradora frm = new frmVisaoCiaSeguradora();
            frm.MdiParent = this;
            
            frm.Show();
        }
            
            

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoEntradaProduto frm = new frmVisaoEntradaProduto();
            frm.MdiParent = this;
            
            frm.Show();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoEstoque frm = new frmVisaoEstoque();
            frm.MdiParent = this;
            
            frm.Show();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoVenda frm = new frmVisaoVenda();
            frm.MdiParent = this;
            
            frm.Show();

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoOrdemServico frm = new frmVisaoOrdemServico();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoRecebimento frm = new frmVisaoRecebimento();
            frm.MdiParent = this;
            
            frm.Show();
        }


        private void skinRibbonGalleryBarItem1_GalleryItemCheckedChanged(object sender, DevExpress.XtraBars.Ribbon.GalleryItemEventArgs e)
        {
            
        }

        private void skinRibbonGalleryBarItem1_DownChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void ribbonControl1_StyleChanged(object sender, EventArgs e)
        {
            MessageBox.Show(defaultLookAndFeel1.LookAndFeel.SkinName);
        }

        private void btnSalvaSkin_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Properties.Settings.Default.Skin = defaultLookAndFeel1.LookAndFeel.SkinName;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.Upgrade();
        }

        private void btnProprietario_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCadastroProprietario frm = new frmCadastroProprietario(false , null);
            frm.Show();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Format(@"SELECT * FROM PROPRIETARIO");
                this.Text = MetodosSql.GetField(sql, "NOMEFANTASIA");
                defaultLookAndFeel1.LookAndFeel.SkinName = Properties.Settings.Default.Skin;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void barButtonItem4_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string sql = String.Format(@"SELECT * FROM PROPRIETARIO");
            this.Text = MetodosSql.GetField(sql, "NOMEFANTASIA");
        }

        private void btnVisaoVencimentos_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoVencimento frm = new frmVisaoVencimento();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem7_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoVendedores frm = new frmVisaoVendedores();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem10_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoRelCliente frm = new frmVisaoRelCliente();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoRelVenda frm = new frmVisaoRelVenda();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem13_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmVisaoRelComissoes frm = new frmVisaoRelComissoes();
            frm.MdiParent = this;
            frm.Show();
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCadastroPorcentagens frm = new frmCadastroPorcentagens();
            frm.ShowDialog();
        }
    }
}
