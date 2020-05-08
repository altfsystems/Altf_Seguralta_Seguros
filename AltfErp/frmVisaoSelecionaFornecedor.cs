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
    public partial class frmVisaoSelecionaFornecedor : Form
    {
        public string CODIGO { get; set; }
        public string NOME { get; set; }
        public string NOMEFANTASIA { get; set; }

        public frmVisaoSelecionaFornecedor()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.DataSource = MetodosSql.GetDT("select IDFCFO, NOME, NOMEFANTASIA, CPFCNPJ from FCFO where TIPO = 'F'");
            gridView1.BestFitColumns();
        }

        private void Selecionar()
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDFCFO");

                if(obj != null)
                {


                    CODIGO = obj.ToString();
                    obj = gridView1.GetRowCellValue(rowHandle, "NOME");
                    NOME = obj.ToString();
                    obj = gridView1.GetRowCellValue(rowHandle, "NOMEFANTASIA");
                    NOMEFANTASIA = obj.ToString();


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

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            Selecionar();
        }

        private void btnSelecionar_Click_1(object sender, EventArgs e)
        {
            Selecionar();
        }
    }
}
