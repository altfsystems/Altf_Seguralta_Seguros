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

namespace AltfErp
{
    public partial class frmVisaoRelVenda : Form
    {
        private string Inicio, Fim;

        public frmVisaoRelVenda()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            grdVisaoRelVendas.EmbeddedNavigator.Buttons.Append.Visible = false;
            grdVisaoRelVendas.EmbeddedNavigator.Buttons.Remove.Visible = false;
            Filtro();
            AtualizaGrid();
        }




        private void Filtro()
        {
            frmFiltroDataVencimento frm = new frmFiltroDataVencimento();
            frm.ShowDialog();
            Inicio = frm.DataInicio;
            Fim = frm.DataFim;

        }

        private void btnRelatorioVenda_Click(object sender, EventArgs e)
        {
            relVendaMes report = new relVendaMes(Inicio, Fim);

            using (ReportPrintTool printTool = new ReportPrintTool(report))
            {
                printTool.ShowPreviewDialog();
            }
        }

        private void AtualizaGrid()
        {
            grdVisaoRelVendas.DataSource = MetodosSql.GetDT(String.Format(@"  SELECT VD.IDVENDA, FCFO.NOME, FCFO.NOMEFANTASIA AS SOBRENOME, VENDEDORES.NOME AS VENDEDOR, VENDEDORES.SOBRENOME, 
            (SELECT TIP.DESCRICAO WHERE TIP.IDTIPOPAGAMENTO = VD.TIPOPAGAMENTO) AS PAGAMENTO , VD.STATUS,
            CONVERT(VARCHAR, VD.DATAINCLUSAO, 103) AS DATAVENDA,
            CONVERT(VARCHAR, VD.DATAVENCIMENTO, 103) AS DATAVENCIMENTO, VD.DESCONTO, 
            ROUND(CAST(SUM(PAR.VALOR * ITM.QUANTIDADE)AS NUMERIC(20,2)), 2) AS TOTALVENDA
            FROM VENDA VD
            INNER JOIN PARCELA PAR
            ON VD.IDVENDA = PAR.IDVENDA
            INNER JOIN ITEMMOVIMENTO ITM
            ON VD.IDVENDA = ITM.IDVENDA
            INNER JOIN FCFO
            ON VD.IDFCFO = FCFO.IDFCFO
            INNER JOIN VENDEDORES
            ON VD.IDVENDEDOR = VENDEDORES.IDVENDEDOR
            INNER JOIN TIPOPAGAMENTO TIP
            ON VD.TIPOPAGAMENTO = TIP.PARCELAS
            WHERE CONVERT(VARCHAR,VD.DATAINCLUSAO, 103) >= '{0}' AND CONVERT(VARCHAR,VD.DATAINCLUSAO, 103) <= '{1}'
            GROUP BY VD.IDVENDA, FCFO.NOME, FCFO.NOMEFANTASIA, VENDEDORES.NOME, VENDEDORES.SOBRENOME, VD.TIPOPAGAMENTO,TIP.DESCRICAO, TIP.IDTIPOPAGAMENTO,
            VD.DESCONTO, VD.STATUS, VD.DATAINCLUSAO, VD.DATAVENCIMENTO", Inicio, Fim));
        }
    }
}


