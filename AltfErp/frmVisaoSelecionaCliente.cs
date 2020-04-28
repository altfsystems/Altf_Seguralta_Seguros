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
    public partial class frmVisaoSelecionaCliente : Form
    {
        public string CODIGO { get; set; }
        public string NOME { get; set; }
        public string SOBRENOME { get; set; }



        public frmVisaoSelecionaCliente()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.DataSource = MetodosSql.GetDT("select IDFCFO , NOME , NOMEFANTASIA AS SOBRENOME ,OBSERVACAO  from FCFO where TIPO = 'C'");
            gridView1.BestFitColumns();
        }

      

        private void Selecionar()
        {

            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                


            
                
                    CODIGO = gridView1.GetRowCellValue(rowHandle, "IDFCFO").ToString();
                    NOME = gridView1.GetRowCellValue(rowHandle, "NOME").ToString();
                    SOBRENOME = gridView1.GetRowCellValue(rowHandle, "SOBRENOME").ToString();

                



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
