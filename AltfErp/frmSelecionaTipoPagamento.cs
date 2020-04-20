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
    public partial class frmSelecionaTipoPagamento : Form
    {
        public string TIPOPAGAMENTO { get; set; }
        public string DESCPAGAMENTO { get; set; }


        public frmSelecionaTipoPagamento()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
        }

        private void AtualizaGrid()
        {
            gridControl1.DataSource = MetodosSql.GetDT(@"select * from TIPOPAGAMENTO");

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDTIPOPAGAMENTO");
                TIPOPAGAMENTO = obj.ToString();
                DESCPAGAMENTO = gridView1.GetRowCellValue(rowHandle, "DESCRICAO").ToString();






                this.Close();   
            }
           catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

         


        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDTIPOPAGAMENTO");
                TIPOPAGAMENTO = obj.ToString();
                DESCPAGAMENTO = gridView1.GetRowCellValue(rowHandle, "DESCRICAO").ToString();






                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
