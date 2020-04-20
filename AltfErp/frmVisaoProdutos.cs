using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using AltfErp;

namespace AltfErp
{
    public partial class frmVisaoProdutos : Form
    {

        
        
        public frmVisaoProdutos()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridView1.BestFitColumns();
            AtualizaGrid();
            
               

      
        }

            
            
            
            
            

        public void AtualizaGrid()
        {
            string sql = String.Format(@"select IDPRODUTO, DESCRICAO, TIPOSEGURO, CIASEGURADORA, NUMEROQUIVER,  
                        CAST(PRECOUNENTRADA as DECIMAL(10, 2)) AS PRECOUNENTRADA,
                        CAST(MARGEMVENDA as DECIMAL(10, 2)) AS MARGEMVENDA,
                        CAST(PRECOUNVENDA as DECIMAL(10, 2)) AS PRECOUNVENDA,
                        OBSERVACAO, DATAINCLUSAO
                        from PRODUTO 
						ORDER BY PRODUTO.IDPRODUTO");
            gridControl1.DataSource = MetodosSql.GetDT(sql);
            MetodosSql.ExecQuery(sql);
        }

       

        public void btnNovo_Click(object sender, EventArgs e)
        {
            frmCadastroProdutos frm = new frmCadastroProdutos(false, null);
            frm.ShowDialog();
            AtualizaGrid();
            
        }

            
        

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDPRODUTO");

                frmCadastroProdutos frm = new frmCadastroProdutos(true, obj.ToString());
                frm.ShowDialog();
                AtualizaGrid();
               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDPRODUTO");

                frmCadastroProdutos frm = new frmCadastroProdutos(true, obj.ToString());
                frm.ShowDialog();
                AtualizaGrid();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            {
                try
                {
                    var rowHandle = gridView1.FocusedRowHandle;

                    var obj = gridView1.GetRowCellValue(rowHandle, "IDPRODUTO");

                    if (DialogResult.Yes == MessageBox.Show("Deseja mesmo exluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {

                        MetodosSql.ExecQuery(String.Format(@"delete from PRODUTO where IDPRODUTO = {0}", obj.ToString()));
                        AtualizaGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void gridControl1_Load(object sender, EventArgs e)
        {
            AtualizaGrid();
        }
    }
}
                

            
            
