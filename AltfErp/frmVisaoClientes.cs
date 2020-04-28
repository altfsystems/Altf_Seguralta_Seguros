using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using AltfErp;



namespace AltfErp
{
    
    

    

    public partial class frmVisaoClientes : Form
    {
        public frmVisaoClientes()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            grdVisaoCliente.EmbeddedNavigator.Buttons.Append.Visible = false;
            grdVisaoCliente.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
            gridView1.BestFitColumns();
        }

        private void AtualizaGrid()
        {
            grdVisaoCliente.DataSource = MetodosSql.GetDT(@"select IDFCFO AS IDCLIENTE, NOME, NOMEFANTASIA AS SOBRENOME, TELEFONE1, TELEFONE2, CELULAR,CELULAR2, EMAIL, EMAIL2,
                                                               OBSERVACAO, DATAINCLUSAO  FROM FCFO WHERE TIPO = 'C' ORDER BY NOME" );

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmFCFO frm = new frmFCFO(false, null, "C");
            frm.ShowDialog();
            AtualizaGrid();

        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDCLIENTE");

                frmFCFO frm = new frmFCFO(true, obj.ToString(), "C");
                frm.ShowDialog();
                AtualizaGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView1.SelectedRowsCount > 0)
                {
                    var rowHandle = gridView1.FocusedRowHandle;

                    var obj = gridView1.GetRowCellValue(rowHandle, "IDCLIENTE");

                    frmFCFO frm = new frmFCFO(true, obj.ToString(), "C");
                    frm.ShowDialog();
                    AtualizaGrid();
                }
                else
                {
                    MessageBox.Show("Selecione um registro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {

                if (gridView1.SelectedRowsCount > 0)
                {
                    var rowHandle = gridView1.FocusedRowHandle;
                    var id = gridView1.GetRowCellValue(rowHandle, "IDCLIENTE");

                    string sql = String.Format(@"select IDFCFO from VENDA WHERE IDFCFO = " + id.ToString());

                    string teste = MetodosSql.GetField(sql, "IDFCFO");
                    string SQL = String.Format(@"SELECT IDFCFO FROM ORDEM where IDFCFO = " + id.ToString());
                    string IdFcfo = MetodosSql.GetField(SQL, "IDFCFO");


                    if (id.ToString() == teste.ToString())
                    {
                        MessageBox.Show("Há uma Venda no Nome Deste Cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        if (id.ToString() == IdFcfo.ToString())
                        {
                            MessageBox.Show("Há uma Ordem de Serviço no Nome Deste Cliente", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            if (DialogResult.Yes == MessageBox.Show("Deseja Excluir Este Cliente?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                            {
                                sql = String.Format(@"DELETE from FCFO WHERE IDFCFO = " + id.ToString());
                                MetodosSql.ExecQuery(sql);
                                AtualizaGrid();

                            }
                        }

                    }

                }
                else
                {
                    MessageBox.Show("Selecione um registro", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                
        }
    }
}
