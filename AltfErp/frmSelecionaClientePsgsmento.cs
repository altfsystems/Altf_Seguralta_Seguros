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
    

    public partial class frmSelecionaClientePsgsmento : Form
    {
        public string CODIGO { get; set; }
        


        public frmSelecionaClientePsgsmento()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
        }

        private void AtualizaGrid()
        {
            gridControl1.DataSource = MetodosSql.GetDT(@"select IDFCFO , NOME , NOMEFANTASIA AS SOBRENOME , OBSERVACAO from FCFO where TIPO = 'C'");
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDFCFO");
                CODIGO = obj.ToString();
               

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
