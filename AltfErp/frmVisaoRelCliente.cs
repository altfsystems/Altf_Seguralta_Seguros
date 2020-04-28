using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraReports.UI;
using Microsoft.Win32;

namespace AltfErp
{
    public partial class frmVisaoRelCliente : Form
    {
        public frmVisaoRelCliente()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            grdVisaoRelCliente.EmbeddedNavigator.Buttons.Append.Visible = false;
            grdVisaoRelCliente.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
        }

        private void AtualizaGrid()
        {
            grdVisaoRelCliente.DataSource = MetodosSql.GetDT(String.Format(@"select IDFCFO, NOME, NOMEFANTASIA AS 'SOBRENOME',
               CPF, CNPJ, RG, CNH,
               OBSDOCUMENTO, DTANASCIMENTO,
               TELEFONE1, CELULAR, EMAIL,
               CIDADE, ESTADO, OBSERVACAO,
               TIPOPESSOA from FCFO WHERE TIPO = 'C'"));
               




        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            relClientes report = new relClientes(null);

            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.ShowPreviewDialog();
            }
        }

        private void btnInformacoesCliente_Click(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var cod = gridView1.GetRowCellValue(rowHandle, "IDFCFO");

            if(cod != null)
            {
                relInfoCliente report = new relInfoCliente(cod.ToString());

                using (ReportPrintTool printTool = new ReportPrintTool(report))
                {
                    printTool.ShowPreviewDialog();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
}
