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
    public partial class frmCadastroVenda : Form
    {
        Boolean Editar;
        List<Produto> produtos = new List<Produto>();
        string IDITEM = null;
        bool vendaClick;
        string data1, idVendedor, status, SALVAR, Cod, sql;
        int ano, dia, mes;
        string diaVencimento, mesVencimento;
        public String VENCIMENTOANUAL { get; set; }



        public class Produto
        {
            public String IDPRODUTO { get; set; }
            public String DESCRICAO { get; set; }
            public String PRECOUNITARIO { get; set; }
            public String QUANTIDADE { get; set; }
            public String VALORTOTAL { get; set; }
            public String CODPARCELA { get; set; }

        }

        public frmCadastroVenda(bool Editar_, string Cod_, string status_)
        {
            InitializeComponent();
            Editar = Editar_;
            Cod = Cod_;
            status = status_;
            txtDataInclusao.Text = DateTime.Now.ToString();
            string[] vet = txtDataInclusao.Text.Split('/');
            diaVencimento = vet[0];
            mesVencimento = vet[1];
            ano = int.Parse(vet[2]) + 1;
            data1 = (diaVencimento + "/" + mesVencimento + "/" + ano);
            txtDataVencimento.Text = data1;
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            txtQuantidade.Text = "1";





        }





        private void InsertParcela()
        {

            string[] vet = data1.Split('/');
            dia = int.Parse(vet[0]);
            mes = int.Parse(vet[1]);
            ano = int.Parse(vet[2]);



            sql = String.Format(@"select PARCELAS from TIPOPAGAMENTO where IDTIPOPAGAMENTO = {0}", txtTipoPagamento.Text);
            int NParcelas = int.Parse(MetodosSql.GetField(sql, "PARCELAS"));

            for (int parcela = 1; parcela <= NParcelas; parcela++)
            {
                sql = String.Format(@"insert into PARCELA(NPARCELA, IDVENDA, IDFCFO, VALOR, DATAVENCIMENTO) values ({0},{1},{2},cast({3} as numeric(20,2))/{4}, CONVERT(DATETIME, CONVERT(VARCHAR,'{5}', 121),103)) select SCOPE_IDENTITY()", parcela, txtCodigo.Text, txtIdCliente.Text, txtTotalVenda.Text.Replace(".", "").Replace(",", "."), NParcelas, data1);
                object Codparcela = MetodosSql.ExecScalar(sql);
                Cod = Codparcela.ToString();
                if (mes == 12)
                {
                    ano += 1;
                    mes = 1;
                }
                else
                {
                    mes += 1;
                }

                data1 = (dia + "/" + mes + "/" + ano);

            }
        }

        private void AtualizaGrid()
        {
            try
            {
                if (Editar)
                {
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = MetodosSql.GetDT(String.Format(@"select IM.IDITEM, 
                    IM.IDPRODUTO, 
	                cast(IM.QUANTIDADE as numeric(20,2)) as 'QUANTIDADE',
	                P.DESCRICAO, 
	                cast(IM.VALOR as numeric(20,2)) as 'VALOR UNITARIO', 
	                cast((IM.QUANTIDADE*IM.VALOR) as numeric(20,2)) as 'TOTAL'
                    from ITEMMOVIMENTO IM
                    inner join PRODUTO P
                    on P.IDPRODUTO = IM.IDPRODUTO 
                    where IDVENDA = '{0}'
                    ORDER BY IDITEM DESC", txtCodigo.Text));


                    gridView1.BestFitColumns();
                    txtTotalVenda.Text = MetodosSql.GetField(String.Format(@"select cast(sum(QUANTIDADE* VALOR) as numeric(20, 2)) as 'TOTAL' from ITEMMOVIMENTO where IDVENDA = '{0}'", txtCodigo.Text), "TOTAL");
                    txtTotalDesconto.Text = MetodosSql.GetField(String.Format(@"SELECT VD.IDVENDA, VD.IDFCFO,  cast(SUM(IT.VALOR * IT.QUANTIDADE) - VD.DESCONTO as numeric(20,2)) AS TOTAL_VENDA FROM VENDA VD
                    INNER JOIN ITEMMOVIMENTO IT
                    ON IT.IDVENDA = VD.IDVENDA WHERE VD.IDVENDA IS NOT NULL AND VD.IDVENDA = {0}
                    GROUP BY VD.DESCONTO, VD.IDVENDA, VD.IDFCFO", txtCodigo.Text), "TOTAL_VENDA");



                }
                else
                {
                    gridView1.ClearDocument();
                    gridControl1.DataSource = produtos;
                    gridControl1.RefreshDataSource();
                    double Total = 0;

                    foreach (Produto P in produtos)
                    {
                        Total += Convert.ToDouble(P.VALORTOTAL);

                    }

                    txtTotalVenda.Text = String.Format("{0:N}", Total);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void AlteraEstoque()
        {
            string SQL = String.Format(@"select IDPRODUTO, QUANTIDADE from ITEMMOVIMENTO where IDVENDA = '{0}'", txtCodigo.Text);

            DataTable Produtos = MetodosSql.GetDT(sql);

            foreach (DataRow Produto in Produtos.Rows)
            {
                SQL = String.Format(@"update ESTOQUE
                                             set QUANTIDADE = QUANTIDADE + {0}
                                             where IDPRODUTO = '{1}'", Produto["QUANTIDADE"].ToString().Replace(",", "."), Produto["IDPRODUTO"].ToString());
                MetodosSql.ExecQuery(sql);
            }
        }

        private void InsereEstoque()
        {
            sql = String.Format(@"select IDPRODUTO, QUANTIDADE from ITEMMOVIMENTO where IDVENDA = '{0}'", txtCodigo.Text);

            DataTable Produtos = MetodosSql.GetDT(sql);

            foreach (DataRow Produto in Produtos.Rows)
            {
                sql = String.Format(@"update ESTOQUE
                                             set QUANTIDADE = QUANTIDADE - {0}
                                             where IDPRODUTO = '{1}'", Produto["QUANTIDADE"].ToString().Replace(",", "."), Produto["IDPRODUTO"].ToString());
                MetodosSql.ExecQuery(sql);
            }
        }

        private void Cadastro()
        {
            try
            {
                if (txtTipoPagamento.Text != "1")
                {

                    frmDataVencimento data = new frmDataVencimento();
                    data.ShowDialog();
                    data1 = data.dataVencimento;
                }

                if (String.IsNullOrWhiteSpace(txtDesconto.Text))
                {
                    txtDesconto.Text = "0";
                }

                sql = String.Format(@"insert into VENDA (IDFCFO, IDVENDEDOR, IDORDEM, TIPOPAGAMENTO, DESCONTO, OBSERVACAO, STATUS, DATAINCLUSAO, DATAPAGAMENTO, DATAVENCIMENTO) values('{0}' ,{4}, null, '{1}' ,{2}, '{3}' , 'A' , getdate() , null, CONVERT(DATETIME, CONVERT(VARCHAR,'{5}', 121),103)) select SCOPE_IDENTITY()"
              , txtIdCliente.Text, txtTipoPagamento.Text, txtDesconto.Text.Replace(".", "").Replace(",", "."), txtObservacao.Text, txtIdVendedor.Text, txtDataVencimento.Text);

                object IDVENDA = MetodosSql.ExecScalar(sql);
                txtCodigo.Text = IDVENDA.ToString();




                foreach (Produto p in produtos)
                {
                    sql = String.Format("insert into ITEMMOVIMENTO (IDVENDA, IDPRODUTO, VALOR, QUANTIDADE, DATAINCLUSAO) values ('{0}','{1}','{2}','{3}', GETDATE())",
                                                  /*{0}*/ IDVENDA.ToString(),
                                                  /*{1}*/ p.IDPRODUTO,
                                                  /*{2}*/ p.PRECOUNITARIO.Replace(".", "").Replace(",", "."),
                                                  /*{3}*/ p.QUANTIDADE.Replace(",", "."));



                    MetodosSql.ExecQuery(sql);

                }

                Editar = true;


                double TotalDesconto = Convert.ToDouble(txtTotalVenda.Text) - Convert.ToDouble(txtDesconto.Text);
                txtTotalVenda.Text = String.Format("{0:N}", TotalDesconto.ToString());



                InsertParcela();

                //if (vendaClick == false)
                //{
                //    frmIdPagamento frm = new frmIdPagamento(false, null);
                //    frm.txtValorRestante.Enabled = false;
                //    frm.label7.Enabled = false;
                //    frm.CODIGOVENDA = IDVENDA.ToString();
                //    frm.CODIGOPARCELA = Cod.ToString();
                //    frm.CODIGOCLIENTE = txtIdCliente.Text;
                //    frm.ShowDialog();
                //}

                InsereEstoque();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void frmCadastroVenda_Load(object sender, EventArgs e)
        {
            try
            {
                if (Editar)
                {
                    txtCodigo.Text = Cod;

                    sql = String.Format(@"select * from VENDA where IDVENDA = {0}", Cod);
                    idVendedor = MetodosSql.GetField(sql, "IDVENDEDOR");
                    txtDesconto.Text = MetodosSql.GetField(sql, "DESCONTO");
                    txtIdCliente.Text = MetodosSql.GetField(sql, "IDFCFO");
                    txtIdOrdem.Text = MetodosSql.GetField(sql, "IDORDEM");
                    txtTipoPagamento.Text = MetodosSql.GetField(sql, "TIPOPAGAMENTO");
                    txtObservacao.Text = MetodosSql.GetField(sql, "OBSERVACAO");
                    txtStatus.Text = status;
                    txtDataInclusao.Text = MetodosSql.GetField(String.Format(@"select CONVERT(varchar, CONVERT(varchar, DATAINCLUSAO, 103)) as 'Nasc' from VENDA where IDVENDA = {0}", Cod), "Nasc");
                    txtNome.Text = MetodosSql.GetField(String.Format(@"select NOME from FCFO where IDFCFO = '{0}'", txtIdCliente.Text), "NOME");
                    txtSobrenome.Text = MetodosSql.GetField(String.Format(@"select NOMEFANTASIA FROM FCFO WHERE IDFCFO = {0}", txtIdCliente.Text), "NOMEFANTASIA");
                    sql = String.Format(@"select NOME, SOBRENOME FROM VENDEDORES WHERE IDVENDEDOR = '{0}'", idVendedor.ToString());
                    txtNomeVendedor.Text = MetodosSql.GetField(sql, "NOME");
                    txtSobrenomeVendedor.Text = MetodosSql.GetField(sql, "SOBRENOME");
                    txtIdVendedor.Text = idVendedor.ToString();
                    sql = String.Format(@"select DESCRICAO from TIPOPAGAMENTO WHERE IDTIPOPAGAMENTO = {0}", txtTipoPagamento.Text);
                    txtDescricaoTipoPagamento.Text = MetodosSql.GetField(sql, "DESCRICAO");
                    txtDataVencimento.Text = MetodosSql.GetField(String.Format(@"select CONVERT(varchar, CONVERT(varchar, DATAVENCIMENTO, 103)) as 'Vencimento' from VENDA where IDVENDA = {0}", Cod), "Vencimento");




                }

                AtualizaGrid();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frmVisaoSelecionaCliente frm = new frmVisaoSelecionaCliente();
            frm.ShowDialog();
            txtIdCliente.Text = frm.CODIGO;
            txtNome.Text = frm.NOME;
            txtSobrenome.Text = frm.SOBRENOME;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (SALVAR == "0")
            {
                this.Close();
            }
            else
            {
                if (String.IsNullOrWhiteSpace(txtTipoPagamento.Text))
                {
                    MessageBox.Show("Selecione Um Tipo De Pagameto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (txtTotalVenda.Text == "0,00")
                    {
                        MessageBox.Show("Selecione um Produto");


                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(txtTotalVenda.Text))
                        {
                            MessageBox.Show("Selecione um Produto");

                        }
                        else
                        {
                            Cadastro();

                            this.Close();
                        }

                    }
                }

            }




        }

        private void CalculaTotal()
        {
            try
            {
                double total = (Convert.ToDouble(txtValorUnitario.Text) * Convert.ToDouble(txtQuantidade.Text));
                txtValorTotal.Text = String.Format("{0:N}", total);

                double valorUnitario = Convert.ToDouble(txtValorUnitario.Text);
                txtValorUnitario.Text = String.Format("{0:N}", valorUnitario);

                double quantidade = Convert.ToDouble(txtQuantidade.Text);
                txtQuantidade.Text = String.Format("{0:N}", quantidade);

                double totalvenda = Convert.ToDouble(txtTotalVenda.Text);
                txtTotalVenda.Text = String.Format("{0:N}", totalvenda);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void LimpaCampos()
        {
            txtCodigoProduto.Text = String.Empty;
            txtDescricaoProduto.Text = String.Empty;
            txtQuantidade.Text = "1";
            txtValorUnitario.Text = String.Empty;
            txtValorTotal.Text = String.Empty;
        }

        private void btnSelecionaProduto_Click(object sender, EventArgs e)
        {
            frmVisaoSelecionaProduto frm = new frmVisaoSelecionaProduto();
            frm.ShowDialog();
            txtCodigoProduto.Text = frm.CODIGO;
            txtDescricaoProduto.Text = frm.DESCRICAO;



            sql = String.Format(@"select * from PRODUTO where IDPRODUTO = '{0}'", frm.CODIGO);
            txtValorUnitario.Text = MetodosSql.GetField(sql, "PRECOUNVENDA");


            CalculaTotal();
        }

        private void txtValorUnitario_Leave(object sender, EventArgs e)
        {
            CalculaTotal();
        }

        private void btnSelecionaVendedor_Click(object sender, EventArgs e)
        {
            frmVisaoVendedoresVenda frm = new frmVisaoVendedoresVenda();
            frm.ShowDialog();
            if (!String.IsNullOrWhiteSpace(frm.CodVendedor))
            {
                idVendedor = frm.CodVendedor.ToString();

                string sql = String.Format(@"SELECT NOME, SOBRENOME FROM VENDEDORES WHERE IDVENDEDOR = '{0}'", idVendedor);
                txtIdVendedor.Text = idVendedor.ToString();
                txtNomeVendedor.Text = MetodosSql.GetField(sql, "NOME");
                txtSobrenomeVendedor.Text = MetodosSql.GetField(sql, "SOBRENOME");
            }
        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            CalculaTotal();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {



                if (!String.IsNullOrWhiteSpace(txtCodigoProduto.Text))
                {
                    if (Editar)
                    {


                        AlteraEstoque();

                        if (String.IsNullOrWhiteSpace(IDITEM))
                        {
                            sql = String.Format("insert into ITEMMOVIMENTO (IDVENDA, IDPRODUTO, VALOR, QUANTIDADE, DATAINCLUSAO) values ('{0}','{1}','{2}','{3}', GETDATE())",
                                                  /*{0}*/ txtCodigo.Text,
                                                  /*{1}*/ txtCodigoProduto.Text,
                                                  /*{2}*/ txtValorUnitario.Text.Replace(",", "."),
                                                  /*{3}*/ txtQuantidade.Text.Replace(",", "."));

                            MetodosSql.ExecQuery(sql);
                        }
                        else
                        {

                            sql = String.Format(@"update ITEMMOVIMENTO
	                                                   set  VALOR = '{0}',
			                                                QUANTIDADE = '{1}',
			                                                DATAINCLUSAO = GETDATE()
		                                                where IDITEM = '{2}'",
                                                    /*{0}*/ txtValorUnitario.Text.Replace(",", "."),
                                                    /*{1}*/ txtQuantidade.Text.Replace(",", "."),
                                                    /*{2}*/ IDITEM);

                            MetodosSql.ExecQuery(sql);

                            IDITEM = null;
                        }



                    }
                    else
                    {
                        if (String.IsNullOrWhiteSpace(IDITEM))
                        {
                            Produto produto = new Produto();

                            produto.IDPRODUTO = txtCodigoProduto.Text;
                            produto.DESCRICAO = txtDescricaoProduto.Text;
                            produto.PRECOUNITARIO = txtValorUnitario.Text;
                            produto.QUANTIDADE = txtQuantidade.Text;
                            produto.VALORTOTAL = txtValorTotal.Text;

                            produtos.Add(produto);
                        }
                        else
                        {
                            int indice = int.Parse(IDITEM);
                            produtos[indice].PRECOUNITARIO = txtValorUnitario.Text;
                            produtos[indice].QUANTIDADE = txtQuantidade.Text;
                            produtos[indice].VALORTOTAL = txtValorTotal.Text;
                        }

                    }
                }


                AtualizaGrid();

                LimpaCampos();
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
                var rowHandle = gridView1.FocusedRowHandle;

                if (Editar)
                {


                    IDITEM = gridView1.GetRowCellValue(rowHandle, "IDITEM").ToString();

                    sql = String.Format(@"select * from ITEMMOVIMENTO where IDITEM = '{0}'", IDITEM);

                    txtCodigoProduto.Text = MetodosSql.GetField(sql, "IDPRODUTO");
                    txtValorUnitario.Text = MetodosSql.GetField(sql, "VALOR");
                    txtQuantidade.Text = MetodosSql.GetField(sql, "QUANTIDADE");


                    CalculaTotal();

                    sql = String.Format(@"select DESCRICAO from PRODUTO where IDPRODUTO = '{0}'", txtCodigoProduto.Text);

                    txtDescricaoProduto.Text = MetodosSql.GetField(sql, "DESCRICAO");
                }
                else
                {
                    int indice = int.Parse(rowHandle.ToString());
                    txtCodigoProduto.Text = produtos[indice].IDPRODUTO;
                    txtValorUnitario.Text = produtos[indice].PRECOUNITARIO;
                    txtQuantidade.Text = produtos[indice].QUANTIDADE;
                    txtValorTotal.Text = produtos[indice].VALORTOTAL;
                    txtDescricaoProduto.Text = produtos[indice].DESCRICAO;

                    IDITEM = indice.ToString();
                }

                btnExcluir.Visible = true;
                btnSelecionaProduto.Enabled = false;

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
                if (Editar)
                {


                    sql = String.Format(@"delete from ITEMMOVIMENTO where IDITEM = '{0}'", IDITEM);

                    MetodosSql.ExecQuery(sql);

                }
                else
                {
                    produtos.RemoveAt(int.Parse(IDITEM));
                }

                btnExcluir.Visible = false;
                btnSelecionaProduto.Enabled = true;

                IDITEM = null;

                AtualizaGrid();
                LimpaCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnTipoPagamento_Click(object sender, EventArgs e)
        {
            frmTipoPagamento frm = new frmTipoPagamento();
            frm.ShowDialog();
            txtTipoPagamento.Text = frm.CODIGO;
            txtDescricaoTipoPagamento.Text = frm.DESCRICAO;
        }



        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SALVAR = "0";
            vendaClick = true;
            if (String.IsNullOrWhiteSpace(txtTipoPagamento.Text))
            {
                MessageBox.Show("Selecione Um Tipo De Pagamento", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                if (String.IsNullOrWhiteSpace(txtTotalVenda.Text))
                {
                    MessageBox.Show("Selecione um Produto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
                else
                {
                    Cadastro();
                }
            }
        }










        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }


    }
}



























