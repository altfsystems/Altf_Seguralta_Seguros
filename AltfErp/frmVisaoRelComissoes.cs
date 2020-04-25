using DevExpress.XtraReports.UI;
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
    public partial class frmVisaoRelComissoes : Form
    {
        private string Inicio, Fim;
        public frmVisaoRelComissoes()
        {
            InitializeComponent();
            frmFiltroDataVencimento frm = new frmFiltroDataVencimento();
            frm.ShowDialog();
            Inicio = frm.DataInicio;
            Fim = frm.DataFim;
            AtualizaGrid();
        }

        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            relComissoes report = new relComissoes(Inicio, Fim);

            using(ReportPrintTool print = new ReportPrintTool(report))
            {
                print.ShowPreviewDialog();
            }
        }

        private void AtualizaGrid()
        {
            string sql = String.Format(@"SELECT VC.IDVENDA, VC.TOTALVENDA, SUM(VC.TOTALVENDA) AS PROJETADA,
            CAST(SUM(ISNULL(REC.VALORDINHEIRO, 0) + ISNULL(REC.VALORCHEQUE, 0) +
            ISNULL(REC.VALORCARTAODEBITO, 0) + ISNULL(REC.VALORCARTAOCREDITO, 0))AS NUMERIC (20,2)) AS RECEBIDA,
            VC.COCORRETAGEM, VC.VALORSEGURALTA, CONVERT(VARCHAR, VD.DATAINCLUSAO, 103) AS DATAVENDA FROM VENDACOMISSAO VC
            INNER JOIN RECEBIMENTO REC
            ON VC.IDVENDA = REC.IDVENDA
            INNER JOIN VENDA VD
            ON VC.IDVENDA = VD.IDVENDA
            WHERE CONVERT(VARCHAR,VD.DATAINCLUSAO, 103) >= '{0}'
            AND CONVERT(VARCHAR,VD.DATAINCLUSAO, 103) <= '{1}'
            GROUP BY VC.IDVENDA , VC.TOTALVENDA, VC.COCORRETAGEM, VC.VALORSEGURALTA, VD.DATAINCLUSAO", Inicio, Fim);
            grdVisaoRelComissoes.DataSource = MetodosSql.GetDT(sql);
            

        }
    }
}
