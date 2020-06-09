using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.LookAndFeel;
using DevExpress.XtraReports.UI;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.DataAccess.Sql;
using Microsoft.Win32;
using System.Diagnostics;
using DevExpress.XtraSplashScreen;

namespace AltfErp
{
    public partial class frmVisaoVenda : Form
    {
        string Filtro;
        string STATUS = String.Empty;
        public string Data { get; set; }
        public string Nulo { get; set; }
        public int IDITEM { get; set; }
        public int IDVENDA { get; set; }


        public frmVisaoVenda()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            Filtra();
            gridView1.BestFitColumns();
        }

        private Boolean TestaUpdate()
        {
            Boolean Validar;
            double Valor;
            string sql = String.Format(@"SELECT SUM(ISNULL(VALORDINHEIRO, 0) + ISNULL(VALORCHEQUE, 0) + ISNULL(VALORCARTAOCREDITO, 0) + ISNULL(VALORCARTAODEBITO, 0)) AS VALOR
                                        FROM RECEBIMENTO WHERE IDVENDA = '{0}' AND ESTORNO = 0", IDVENDA);
            if(MetodosSql.GetField(sql, "VALOR") == "")
            {
                Valor = 0;
            }
            else
            {
                Valor = double.Parse(MetodosSql.GetField(sql, "VALOR"));
            }
            
            if (Valor == 0)
            {
                Validar = true;
            }
            else
            {
                Validar = false;
            }
            return Validar;
        }

        private void ChamaCadastro()
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var obj = gridView1.GetRowCellValue(rowHandle, "IDVENDA");
            var status = gridView1.GetRowCellValue(rowHandle, "STATUS");

            if(obj != null)
            {
                frmCadastroVenda Cad = new frmCadastroVenda(true, obj.ToString(), status.ToString());
                Cad.btnAdicionar.Enabled = false;
                Cad.btnTipoPagamento.Enabled = false;
                Cad.btnSelecionaProduto.Enabled = false;
                Cad.btnSalvar.Enabled = false;
                Cad.btnOk.Enabled = false;
                Cad.btnExcluir.Enabled = false;
                Cad.txtIof.ReadOnly = true;
                Cad.btnSelecionaCliente.Enabled = false;
                Cad.txtValorTotal.ReadOnly = true;
                Cad.txtTotalVenda.ReadOnly = true;
                Cad.txtObservacao.ReadOnly = true;
                Cad.txtDesconto.ReadOnly = true;
                Cad.btnSelecionaVendedor.Enabled = false;
                Cad.txtComissao.ReadOnly = true;
                Cad.txtValorLiquido.ReadOnly = true;
                Cad.txtIof.ReadOnly = true;
                Cad.cbCoCorretagem.Enabled = false;
                Cad.txtDataVencimento.ReadOnly = true;
                Cad.ShowDialog();

                AtualizaGrid();
            }
            else
            {
                MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                

            
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
                string sql = String.Format(@"SELECT VD.IDVENDA, VD.IDFCFO AS IDCLIENTE, FC.NOME, FC.NOMEFANTASIA AS SOBRENOME, 
                                            (SELECT cast(SUM(VALOR ) AS numeric(20,2)) FROM PARCELA WHERE IDVENDA = VD.IDVENDA ) AS TOTALVENDA,
                                            VD.DESCONTO, (X.TOTAL_RECEBIMENTO) AS TOTAL_RECEBIMENTO ,
		                                    (SELECT CAST(SUM(VALOR) AS NUMERIC(20,2)) FROM PARCELA WHERE IDVENDA = VD.IDVENDA) - (X.TOTAL_RECEBIMENTO) AS TOTAL_RESTANTE,
                                            VD.OBSERVACAO, CONVERT(VARCHAR , CONVERT(DATETIME , VD.DATAINCLUSAO , 121) , 103) AS DATAINCLUSAO , 
                                            CONVERT(varchar, CONVERT(DATETIME,VD.DATAPAGAMENTO,121),103) AS DATAPAGAMENTO,
		                                    case
	                                        WHEN (SELECT SUM(VALOR) FROM PARCELA WHERE IDVENDA = VD.IDVENDA) - (X.TOTAL_RECEBIMENTO) <= 0 THEN 'P'
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
                                            FROM RECEBIMENTO WHERE ESTORNO != 1
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

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var IdVenda = gridView1.GetRowCellValue(rowHandle, "IDVENDA");
            var status = gridView1.GetRowCellValue(rowHandle, "STATUS");

            frmCadastroVenda frm = new frmCadastroVenda(true, IdVenda.ToString(), status.ToString());
            frm.ShowDialog();
        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            frmCadastroVenda frm = new frmCadastroVenda(false, null, null);
            frm.ShowDialog();
            AtualizaGrid();
        }

     
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                var obj = gridView1.GetRowCellValue(rowHandle, "IDVENDA");
                IDVENDA = int.Parse(obj.ToString());
                int LINHA, IDITEM, IDPRODUTO;
                double QUANTIDADE;
                
                if (obj != null)
                {
                    if(TestaUpdate())
                    {
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
                            MetodosSql.ExecQuery(String.Format(@"delete from VENDA where IDVENDA = {0}", obj));
                            MetodosSql.ExecQuery(String.Format(@"DELETE FROM VENDACOMISSAO WHERE IDVENDA = {0}", obj));
                            MetodosSql.ExecQuery(String.Format(@"DELETE FROM RECEBIMENTO WHERE IDVENDA = '{0}'", obj));
                            AtualizaGrid();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Já existem pagamentos nesta venda. A exclusão está bloqueada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            gridView1.BestFitColumns();

        }

        private void btnVizu_Click(object sender, EventArgs e)
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

        private void btnEditar_Click(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var idVenda = gridView1.GetRowCellValue(rowHandle, "IDVENDA");
            var status = gridView1.GetRowCellValue(rowHandle, "STATUS");

            if(idVenda != null)
            {
                IDVENDA = int.Parse(idVenda.ToString());
                if (TestaUpdate())
                {
                    frmCadastroVenda frm = new frmCadastroVenda(true, idVenda.ToString(), status.ToString());
                    frm.ShowDialog();
                    AtualizaGrid();
                }
                else
                {
                    MessageBox.Show("Já existem pagamentos nesta venda. A edição está bloqueada!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
              
            }
            else
            {
                MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
           
        }
    }



                

                


                







               













        




}
