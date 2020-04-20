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
    public partial class frmVisaoRecebimento : Form
    {
       


        public frmVisaoRecebimento()
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

           string sql = String.Format(@"select C.IDFCFO AS IDCLIENTE,
	                                                     C.NOME,
	   	                                                     C.NOMEFANTASIA AS SOBRENOME,
	   	                                                     C.CPF, C.CNPJ,
	   	                                                     ROUND(cast(isnull(P.TOTAL,0) - isnull(R.PAGO,0) as numeric(20,2)),-1) as 'DEVENDO'
													
	                                                     from FCFO C

	                                                     inner join (select IDFCFO, sum(isnull(VALOR,0)) as 'TOTAL' from PARCELA group by IDFCFO) P
	                                                     on P.IDFCFO = C.IDFCFO

	                                                     left join (select IDFCFO, sum(isnull(VALORDINHEIRO,0) + 
			                                                     isnull(VALORCHEQUE,0) + 
			                                                     isnull(VALORCARTAODEBITO,0) + 
			                                                     isnull(VALORCARTAOCREDITO,0)) as 'PAGO' from RECEBIMENTO
			                                                     group by IDFCFO) R
	                                                     on R.IDFCFO = C.IDFCFO ORDER BY C.NOME");



                 Clipboard.SetText(sql);
            gridControl1.DataSource = MetodosSql.GetDT(sql);


        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmPagamento frm = new frmPagamento(false, null);
            frm.ShowDialog();
            AtualizaGrid();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                var obj = gridView1.GetRowCellValue(rowHandle, "IDCLIENTE");

               

                frmPagamento frm = new frmPagamento(true, obj.ToString());
                frm.ShowDialog();
                AtualizaGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                var obj = gridView1.GetRowCellValue(rowHandle, "IDCLIENTE");

                frmPagamento frm = new frmPagamento(true, obj.ToString());
                frm.ShowDialog();
                AtualizaGrid();

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
