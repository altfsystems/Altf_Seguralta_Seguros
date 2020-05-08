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
    

    public partial class frmVisaoVendedoresVenda : Form
    {
        public string CodVendedor { get; set; }

        public frmVisaoVendedoresVenda()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            grdVisaoVendedores.EmbeddedNavigator.Buttons.Append.Visible = false;
            grdVisaoVendedores.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
        }

        private void AtualizaGrid()
        {
            grdVisaoVendedores.DataSource = MetodosSql.GetDT(@"SELECT IDVENDEDOR, NOME, SOBRENOME, OBSERVACAO, DATAINCLUSAO FROM VENDEDORES");
        }

        private void grdVisaoVendedores_DoubleClick(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var obj = gridView1.GetRowCellValue(rowHandle, "IDVENDEDOR");
            CodVendedor = obj.ToString();
            this.Close();
        }
        private void btnSelecionar_Click_1(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var obj = gridView1.GetRowCellValue(rowHandle, "IDVENDEDOR");

            if (obj != null)
            {
                CodVendedor = obj.ToString();
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
