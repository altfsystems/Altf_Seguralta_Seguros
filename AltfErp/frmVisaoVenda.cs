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
    public partial class frmVisaoVenda : Form
    {
        string Filtro;
        string STATUS = String.Empty;
        public string Data { get; set; }
        public string Nulo { get; set; }
        public int IDITEM { get; set; }


        public frmVisaoVenda()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            Filtra();
            gridView1.BestFitColumns();
        }

        private void ChamaCadastro()
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var obj = gridView1.GetRowCellValue(rowHandle, "IDVENDA");
            var status = gridView1.GetRowCellValue(rowHandle, "STATUS");



            frmCadastroVenda Cad = new frmCadastroVenda(true, obj.ToString(), status.ToString());
            Cad.btnAdicionar.Enabled = false;
            Cad.btnTipoPagamento.Enabled = false;
            Cad.btnSelecionaProduto.Enabled = false;
            Cad.btnSalvar.Enabled = false;
            Cad.btnOk.Enabled = false;
            Cad.btnExcluir.Enabled = false;
            Cad.txtIof.Enabled = false;
            Cad.simpleButton2.Enabled = false;
            Cad.txtQuantidade.Enabled = false;
            Cad.txtValorTotal.Enabled = false;
            Cad.txtTotalVenda.Enabled = false;
            Cad.txtObservacao.Enabled = false;
            Cad.txtDesconto.Enabled = false;
            Cad.txtTotalDesconto.Visible = true;
            Cad.lblTotalDesconto.Visible = true;
            Cad.btnSelecionaVendedor.Enabled = false;
            Cad.txtComissao.Enabled = false;
            Cad.txtValorLiquido.Enabled = false;
            Cad.txtIof.Enabled = false;
            Cad.cbCoCorretagem.Enabled = false;
            Cad.ShowDialog();

            AtualizaGrid();
        }
        private void Filtra()
        {
            frmFiltroVenda frm = new frmFiltroVenda();
            frm.ShowDialog();
            Filtro = frm.RETORNO;
            Data = frm.RETORNODATA;
            Nulo = frm.NULO;

            if (String.IsNullOrWhiteSpace(frm.RETORNO))
            {
                Filtro = "1=1";
            }

            AtualizaGrid();
        }







        private void AtualizaGrid()
        {
            try
            {

                string sql = String.Format(@"SELECT VD.IDVENDA, VD.IDFCFO AS IDCLIENTE, FC.NOME, FC.NOMEFANTASIA AS SOBRENOME, cast(SUM(IT.VALOR * IT.QUANTIDADE) - VD.DESCONTO as numeric(20,2)) AS TOTAL_VENDA, VD.DESCONTO, ROUND(X.TOTAL_RECEBIMENTO, 2) AS TOTAL_RECEBIMENTO ,
		                                        ROUND(cast(SUM(IT.VALOR * IT.QUANTIDADE) - VD.DESCONTO as numeric(20,2)) - ROUND(X.TOTAL_RECEBIMENTO, 2), 2) AS TOTAL_RESTANTE, VD.OBSERVACAO, CONVERT(VARCHAR , CONVERT(DATETIME , VD.DATAINCLUSAO , 121) , 103) AS DATAINCLUSAO , CONVERT(varchar, CONVERT(DATETIME,VD.DATAPAGAMENTO,121),103) AS DATAPAGAMENTO,
		                                           case
	                                               WHEN ROUND(cast(SUM(IT.VALOR * IT.QUANTIDADE) - VD.DESCONTO as numeric(20,2)) - ROUND(X.TOTAL_RECEBIMENTO, 2), 2) <= 0 THEN 'P'
                                                   ELSE
	                                               'A'
                                                   END AS STATUS
                                                    FROM VENDA VD

                                                    inner join FCFO FC
                                                    on VD.IDFCFO = FC.IDFCFO
                            
                                                    INNER JOIN ITEMMOVIMENTO IT
                                                    ON IT.IDVENDA = VD.IDVENDA
                                                    Left JOIN (SELECT IDVENDA, 
                                                    CAST(SUM(VALORCARTAOCREDITO+VALORCARTAODEBITO+VALORCHEQUE+VALORDINHEIRO)AS numeric(20,2)) AS TOTAL_RECEBIMENTO
                                                    FROM RECEBIMENTO
													
													
                                                    GROUP BY IDVENDA)X ON X.IDVENDA = VD.IDVENDA

                                                    WHERE VD.IDVENDA IS NOT NULL AND {0} AND CONVERT(VARCHAR, CONVERT(DATETIME, VD.DATAINCLUSAO , 121) , 103) = {2}'{1}'

                                                    GROUP BY VD.IDVENDA, VD.IDFCFO, X.TOTAL_RECEBIMENTO, FC.NOME, FC.NOMEFANTASIA, VD.OBSERVACAO, VD.DATAINCLUSAO, VD.DATAPAGAMENTO, VD.DESCONTO
													ORDER BY IDVENDA DESC", Filtro, Data, Nulo);





                gridControl1.DataSource = MetodosSql.GetDT(sql);




            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }






        }



        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCadastroVenda frm = new frmCadastroVenda(false, null, null);
            frm.ShowDialog();
            AtualizaGrid();
        }




        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {

                ChamaCadastro();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }










        private void btnDelete_Click(object sender, EventArgs e)
        {

            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                var obj = gridView1.GetRowCellValue(rowHandle, "IDVENDA");
                int LINHA, IDITEM, IDPRODUTO;
                double QUANTIDADE;


                if (DialogResult.Yes == MessageBox.Show("Deseja mesmo exluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {

                    string sql = String.Format(@"select count(IDITEM) as LINHA FROM ITEMMOVIMENTO WHERE IDVENDA = {0}", obj);
                    LINHA = int.Parse(MetodosSql.GetField(sql, "LINHA"));

                    sql = String.Format(@"SELECT MIN(IDITEM) AS MINIDITEM FROM ITEMMOVIMENTO WHERE IDVENDA = {0}", obj);
                    IDITEM = int.Parse(MetodosSql.GetField(sql, "MINIDITEM"));

                    for (int i = 1; i <= LINHA; i++)
                    {

                        sql = String.Format(@"SELECT IDPRODUTO, QUANTIDADE FROM ITEMMOVIMENTO WHERE IDITEM = {0}", IDITEM);

                        IDPRODUTO = int.Parse(MetodosSql.GetField(sql, "IDPRODUTO"));
                        QUANTIDADE = double.Parse(MetodosSql.GetField(sql, "QUANTIDADE"));

                        string estoque = String.Format(@"UPDATE ESTOQUE SET QUANTIDADE = QUANTIDADE + {0} WHERE IDPRODUTO = {1} ", QUANTIDADE, IDPRODUTO);
                        IDITEM++;

                        MetodosSql.ExecQuery(sql);
                        MetodosSql.ExecQuery(estoque);
                    }




                    MetodosSql.ExecQuery(String.Format(@"DELETE FROM PARCELA WHERE IDVENDA = {0}", obj));
                    MetodosSql.ExecQuery(String.Format(@"delete from VENDA where IDVENDA = {0}", obj.ToString()));
                    AtualizaGrid();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            gridView1.BestFitColumns();

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                ChamaCadastro();



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            Filtra();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var IdVenda = gridView1.GetRowCellValue(rowHandle, "IDVENDA");
            var IdCliente = gridView1.GetRowCellValue(rowHandle, "IDCLIENTE");
            string IDVENDA = String.Format(@"AND VENDA.IDVENDA = {0}", IdVenda);

            frmPagamento frm = new frmPagamento(false, IdCliente.ToString());
            frm.CODIGOVENDA = IDVENDA.ToString();
            frm.CarregaNome();
            frm.ShowDialog();
        }
    }
}
