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
    public partial class frmVisaoEstoque : Form
    {
        public frmVisaoEstoque()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            grdVisaoEstoque.EmbeddedNavigator.Buttons.Append.Visible = false;
            grdVisaoEstoque.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
            gridView1.BestFitColumns();
        }

        private void AtualizaGrid()
        {
            grdVisaoEstoque.DataSource = MetodosSql.GetDT(@"SELECT ESTOQUE.IDPRODUTO, PRODUTO.DESCRICAO ,ESTOQUE.QUANTIDADE, CAST(PRODUTO.PRECOUNVENDA AS NUMERIC (20,2)) AS PRECOUNVENDA, ESTOQUE.OBSERVACAO 
                                                               FROM ESTOQUE
                                                               INNER JOIN PRODUTO 
                                                               ON PRODUTO.IDPRODUTO = ESTOQUE.IDPRODUTO");
        }

  

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDPRODUTO");

                if (obj != null)
                {
                    frmAlteraQuantidade frm = new frmAlteraQuantidade(true, obj.ToString());
                    frm.ShowDialog();
                    AtualizaGrid();
                }
                else
                {
                    MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }
            catch (Exception)
            {
                MessageBox.Show("Selecione um item");
            }
        }
    }
}
