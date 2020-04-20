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
    public partial class frmVisaoSelecionaProdutoOrdem : Form
    {
        public string CODIGO { get; set; }
        public string DESCRICAO { get; set; }
        public string VALOR { get; set; }

        public frmVisaoSelecionaProdutoOrdem()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.DataSource = MetodosSql.GetDT(@"select IDPRODUTO , DESCRICAO , PRECOUNVENDA , MARGEMVENDA , PRECOUNENTRADA , DATAINCLUSAO , OBSERVACAO , UNIDADECONTROLE FROM PRODUTO");
            gridView1.BestFitColumns();
        }

        private void Selecionar()
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDPRODUTO");
                CODIGO = obj.ToString();
                obj = gridView1.GetRowCellValue(rowHandle, "DESCRICAO");
                DESCRICAO = obj.ToString();
                obj = gridView1.GetRowCellValue(rowHandle, "PRECOUNVENDA");
                VALOR = obj.ToString();
                

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Selecionar();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            Selecionar();
        }
    }
}
