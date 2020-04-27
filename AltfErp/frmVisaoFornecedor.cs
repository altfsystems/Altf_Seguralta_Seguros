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
    public partial class frmVisaoCiaSeguradora : Form
    {
        public string CODIGO { get; set; }

        public frmVisaoCiaSeguradora()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
            gridView1.BestFitColumns();
        }

        private void AtualizaGrid()
        {
            gridControl1.DataSource = MetodosSql.GetDT(@"SELECT IDSEGURADORA AS COD, NOME, NOMEFANTASIA, CPF, CNPJ, RG, OBSDOCUMENTO, OBSERVACAO FROM FCFOSEGURADORA ORDER BY NOME");
        }

      

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCadastroCiaSeguradora frm = new frmCadastroCiaSeguradora(false, null);
            frm.ShowDialog();
            AtualizaGrid();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "COD");

                frmCadastroCiaSeguradora frm = new frmCadastroCiaSeguradora(true, obj.ToString());
                frm.ShowDialog();
                AtualizaGrid();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    var rowHandle = gridView1.FocusedRowHandle;

                    var obj = gridView1.GetRowCellValue(rowHandle, "COD");

                    if (DialogResult.Yes == MessageBox.Show("Deseja mesmo exluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        MetodosSql.ExecQuery(String.Format(@"delete from FCFOSEGURADORA where IDSEGURADORA = {0}", obj.ToString()));
                        AtualizaGrid();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "COD");

                if(obj == null)
                {
                    MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    frmCadastroCiaSeguradora frm = new frmCadastroCiaSeguradora(true, obj.ToString());
                    frm.ShowDialog();
                    AtualizaGrid();
                }

               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        
    }
}
