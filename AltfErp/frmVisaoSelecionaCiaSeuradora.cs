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
    public partial class frmVisaoSelecionaCiaSeuradora : Form
    {
        public string Codigo { get; set; }

        public frmVisaoSelecionaCiaSeuradora()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            grdVisaoSelecionaCia.EmbeddedNavigator.Buttons.Append.Visible = false;
            grdVisaoSelecionaCia.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
            gridView1.BestFitColumns();
        }

      private void AtualizaGrid()
        {
            grdVisaoSelecionaCia.DataSource = MetodosSql.GetDT("SELECT IDSEGURADORA AS COD, NOME, NOMEFANTASIA, CPF, CNPJ, RG, CONVERT(VARCHAR, DATAINCLUSAO, 103) AS DATAINCLUSAO FROM FCFOSEGURADORA");
        }


        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var cod = gridView1.GetRowCellValue(rowHandle, "COD");
            if (cod != null)
            {
                Codigo = cod.ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
