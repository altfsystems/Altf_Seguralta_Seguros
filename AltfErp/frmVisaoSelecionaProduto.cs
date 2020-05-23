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
    public partial class frmVisaoSelecionaProduto : Form
    {

        public string CODIGO { get; set; }
        public string DESCRICAO { get; set; }
        public string CIASEGURADORA { get; set; }
        public string OBSERVACAO { get; set; }


        // public string PRECOVENDA { get; set; }
        //public string PRECOENTRADA { get; set; }

        public frmVisaoSelecionaProduto()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.DataSource = MetodosSql.GetDT(@"SELECT PRODUTO.IDPRODUTO , PRODUTO.DESCRICAO, PRODUTO.TIPOSEGURO,
                                                        PRODUTO.OBSERVACAO
                                                        FROM PRODUTO
                                                        ORDER by PRODUTO.IDPRODUTO");

            gridView1.BestFitColumns();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDPRODUTO");

                CODIGO = obj.ToString();
                obj = gridView1.GetRowCellValue(rowHandle, "DESCRICAO");
                DESCRICAO = obj.ToString();

                obj = gridView1.GetRowCellValue(rowHandle, "OBSERVACAO");
                OBSERVACAO = obj.ToString();
                //obj = gridView1.GetRowCellValue(rowHandle, "PRECOUNVENDA");
                //PRECOVENDA = obj.ToString();
                //obj = gridView1.GetRowCellValue(rowHandle, "PRECOUNENTRADA");
                //PRECOENTRADA = obj.ToString();

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSelecionar_Click_1(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDPRODUTO");

                if (obj != null)
                {
                    CODIGO = obj.ToString();
                    obj = gridView1.GetRowCellValue(rowHandle, "DESCRICAO");
                    DESCRICAO = obj.ToString();
                                        
                    obj = gridView1.GetRowCellValue(rowHandle, "OBSERVACAO");
                    OBSERVACAO = obj.ToString();
                    //obj = gridView1.GetRowCellValue(rowHandle, "PRECOUNVENDA");
                    //PRECOVENDA = obj.ToString();
                    //obj = gridView1.GetRowCellValue(rowHandle, "PRECOUNENTRADA");
                    //PRECOENTRADA = obj.ToString();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
