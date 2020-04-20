using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltfErp
{
    public partial class frmVisaoEntradaProduto : Form
    {
        public frmVisaoEntradaProduto()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
            gridView1.BestFitColumns();
        }

        private void AtualizaGrid()
        {
            gridControl1.DataSource = MetodosSql.GetDT(@"select EP.IDENTRADA, 
                                                                EP.IDPRODUTO, 
                                                                C.IDFCFO AS IDFORNECEDOR,
	                                                            P.DESCRICAO,
                                                                C.NOMEFANTASIA,
                                                                EP.OBSERVACAO,
	                                                            cast(EP.QUANTIDADEENTRADA as numeric(20,2)) as 'QUANTIDADEENTRADA', 
	                                                            cast(EP.VALORUNENTRADA as numeric(20,2)) as 'VALORUNENTRADA',
	                                                            EP.DATAINCLUSAO
                                                                 from ENTRADAPRODUTO EP

                                                                 inner join PRODUTO P
                                                                 on P.IDPRODUTO = EP.IDPRODUTO

                                                                 inner join FCFO C
                                                                 on C.IDFCFO = EP.IDFCFO
                                                                 and C.TIPO = 'F'
																 ORDER BY EP.IDENTRADA DESC ");
	                                                            
        }
	                                                            
	                                                            

	   

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmEntradaProduto frm = new frmEntradaProduto(false, null);
            frm.ShowDialog();
            AtualizaGrid();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDENTRADA");

                frmEntradaProduto frm = new frmEntradaProduto(true, obj.ToString());
                frm.btnSalvar2.Visible = false;
                frm.ShowDialog();
                AtualizaGrid();
                


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    var rowHandle = gridView1.FocusedRowHandle;

                    var obj = gridView1.GetRowCellValue(rowHandle, "IDENTRADA");

                    if (DialogResult.Yes == MessageBox.Show("Deseja mesmo exluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        MetodosSql.ExecQuery(String.Format(@"delete from ENTRADAPRODUTO where IDENTRADA = {0}", obj.ToString()));
                        AtualizaGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDENTRADA");

                frmEntradaProduto frm = new frmEntradaProduto(true, obj.ToString());
                frm.ShowDialog();
                AtualizaGrid();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }
    }
}
