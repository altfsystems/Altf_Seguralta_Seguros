using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace AltfErp
{
    public partial class frmCadastroVenda : Form
    {
        Boolean Editar;
        List<Produto> produtos = new List<Produto>();
        string IDITEM = null;
        bool vendaClick;
        string data1, idVendedor, status, SALVAR, Cod, sql, IDPRODUTO;
        int ano, dia, mes;
        string diaVencimento, mesVencimento;
        public string VENCIMENTOANUAL { get; set; }
        public string OBS { get; set; }


        public class Produto
        {
            public String IDPRODUTO { get; set; }
            public String DESCRICAO { get; set; }
            public String CIASEGURADORA { get; set; }
            public String OBSERVACAO { get; set; }


            //public String PRECOUNITARIO { get; set; }
            //public String QUANTIDADE { get; set; }
            //public String VALORTOTAL { get; set; }
            //public String CODPARCELA { get; set; }



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

        private void CalculaTotalDesconto()
        {
            if (String.IsNullOrWhiteSpace(txtDesconto.Text))
            {
                txtTotalDesconto.Text = txtTotalVenda.Text; 
                
            }
            else
            {
                double TotalDesconto = Convert.ToDouble(txtTotalVenda.Text) - Convert.ToDouble(txtDesconto.Text);
                txtTotalDesconto.Text =  TotalDesconto.ToString("F2");
            }
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
                string totalVendaString = txtTotalDesconto.Text.Replace(".", "");
                double totalVenda = (double)double.Parse(totalVendaString) / NParcelas;
                string totalVendaInsert = totalVenda.ToString("F2", CultureInfo.InvariantCulture);


                sql = String.Format(@"insert into PARCELA(NPARCELA, IDVENDA, IDFCFO, VALOR, DATAVENCIMENTO, STATUS) values ({0},{1},{2}, '{3}', CONVERT(DATETIME, CONVERT(VARCHAR,'{4}', 121),103), 'A') select SCOPE_IDENTITY()", parcela, txtCodigo.Text, txtIdCliente.Text, totalVendaInsert, data1);
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

        private void InsertVenda()
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




            sql = String.Format(@"insert into VENDA (IDFCFO, IDVENDEDOR, IDORDEM, TIPOPAGAMENTO, DESCONTO, OBSERVACAO, STATUS, DATAINCLUSAO, DATAPAGAMENTO, DATAVENCIMENTO) values(
                                    '{0}' ,{4}, null, '{1}' ,{2}, '{3}' , 'A' , getdate() , null, CONVERT(DATETIME, CONVERT(VARCHAR,'{5}', 121),103)) select SCOPE_IDENTITY()",
                                txtIdCliente.Text, txtTipoPagamento.Text, txtDesconto.Text.Replace(".", "").Replace(",", "."), txtObservacao.Text, txtIdVendedor.Text,
                                txtDataVencimento.Text);
            object IDVENDA = MetodosSql.ExecScalar(sql);
            txtCodigo.Text = IDVENDA.ToString();



            string valor;
            char coCorretagem;
            double valorSeguralta;
            string query = @"INSERT INTO VENDACOMISSAO(IDVENDA, IDCLIENTE, VALORLIQUIDO, IOF, TOTALVENDA, COMISSAO, COMISSAOVENDA, COCORRETAGEM, VALORSEGURALTA)
                               VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}')";


            sql = String.Format(@"SELECT * FROM PORCENTAGENS ");
            double percCorretora = double.Parse(MetodosSql.GetField(sql, "PERCSEGURALTA")) / 100;
            double impostoNota = double.Parse(MetodosSql.GetField(sql, "PERCIMPOSTONOTA")) / 100;


            if (cbCoCorretagem.Checked == true)
            {
                coCorretagem = 'S';
                valorSeguralta = (double.Parse(txtComissaoVenda.Text) * percCorretora);
                valor = valorSeguralta.ToString("F2", CultureInfo.InvariantCulture);

                sql = String.Format(query,
                              /*{0}*/ IDVENDA,
                              /*{1}*/ txtIdCliente.Text,
                              /*{2}*/ txtValorLiquido.Text.Replace(".", "").Replace(",", "."),
                              /*{3}*/ txtIof.Text.Replace(".", "").Replace(",", "."),
                              /*{4}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."),
                              /*{5}*/ txtComissao.Text.Replace(".", "").Replace(",", "."),
                              /*{6}*/ txtComissaoVenda.Text.Replace(".", "").Replace(",", "."),
                              /*{7}*/ coCorretagem,
                              /*{8}*/ valor);
            }
            else
            {
                coCorretagem = 'N';
                valorSeguralta = (double.Parse(txtComissaoVenda.Text) * percCorretora * impostoNota);
                valor = valorSeguralta.ToString("F2", CultureInfo.InvariantCulture);

                sql = String.Format(query,
                              /*{0}*/ IDVENDA,
                              /*{1}*/ txtIdCliente.Text,
                              /*{2}*/ txtValorLiquido.Text.Replace(".", "").Replace(",", "."),
                              /*{3}*/ txtIof.Text.Replace(".", "").Replace(",", "."),
                              /*{4}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."),
                              /*{5}*/ txtComissao.Text.Replace(".", "").Replace(",", "."),
                              /*{6}*/ txtComissaoVenda.Text.Replace(".", "").Replace(",", "."),
                              /*{7}*/ coCorretagem,
                              /*{8}*/ valor);
            }
            MetodosSql.ExecQuery(sql);


            foreach (Produto p in produtos)
            {
                sql = String.Format("insert into ITEMMOVIMENTO (IDVENDA, IDPRODUTO, VALOR, QUANTIDADE, DATAINCLUSAO) values ('{0}','{1}','{2}','1', GETDATE())",
                                              /*{0}*/ IDVENDA.ToString(),
                                              /*{1}*/ p.IDPRODUTO,
                                              /*{2}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."));
                MetodosSql.ExecQuery(sql);
            }


            Editar = true;
            
            InsertParcela();



            if (vendaClick == false)
            {
                frmIdPagamento frm = new frmIdPagamento(false, null, true);
                frm.txtValorRestante.Enabled = false;
                frm.label7.Enabled = false;
                frm.CODIGOVENDA = IDVENDA.ToString();
                frm.CODIGOPARCELA = Cod.ToString();
                frm.CODIGOCLIENTE = txtIdCliente.Text;
                frm.ShowDialog();
            }
        }

        private void UpdateVenda()
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

                string sql = String.Format(@"UPDATE VENDA SET IDFCFO = '{0}', IDVENDEDOR = '{1}', TIPOPAGAMENTO = '{2}', DESCONTO = '{3}', OBSERVACAO = '{4}', STATUS = 'A',
                                             DATAINCLUSAO = GETDATE(), DATAVENCIMENTO = '{5}' WHERE IDVENDA = '{6}' ",
                                           /*{0}*/  txtIdCliente.Text,
                                           /*{1}*/  txtIdVendedor.Text,
                                           /*{2}*/  txtTipoPagamento.Text,
                                           /*{3}*/  txtDesconto.Text.Replace(".", "").Replace(",", "."),
                                           /*{4}*/  txtObservacao.Text,
                                           /*{5}*/  txtDataVencimento.Text,
                                           /*{6}*/  txtCodigo.Text);
                MetodosSql.ExecQuery(sql);


                string valor;
                char coCorretagem;
                double valorSeguralta;
                string query = @"UPDATE VENDACOMISSAO SET IDCLIENTE = '{0}', VALORLIQUIDO = '{1}', IOF = '{2}', TOTALVENDA = '{3}', COMISSAO = '{4}', COMISSAOVENDA = '{5}',
                                    COCORRETAGEM = '{6}', VALORSEGURALTA = '{7}' WHERE IDVENDA = '{8}'";


                sql = String.Format(@"SELECT * FROM PORCENTAGENS ");
                double percCorretora = double.Parse(MetodosSql.GetField(sql, "PERCSEGURALTA")) / 100;
                double impostoNota = double.Parse(MetodosSql.GetField(sql, "PERCIMPOSTONOTA")) / 100;

                if (cbCoCorretagem.Checked == true)
                {
                    coCorretagem = 'S';
                    valorSeguralta = (double.Parse(txtComissaoVenda.Text) * percCorretora);
                    valor = valorSeguralta.ToString("F2", CultureInfo.InvariantCulture);

                    sql = String.Format(query,
                           /*{0}*/ txtIdCliente.Text,
                           /*{1}*/ txtValorLiquido.Text.Replace(".", "").Replace(",", "."),
                           /*{2}*/ txtIof.Text.Replace(".", "").Replace(",", "."),
                           /*{3}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."),
                           /*{4}*/ txtComissao.Text.Replace(".", "").Replace(",", "."),
                           /*{5}*/ txtComissaoVenda.Text.Replace(".", "").Replace(",", "."),
                           /*{6}*/ coCorretagem,
                           /*{7}*/ valor,
                           /*{8}*/ txtCodigo.Text);

                }
                else
                {
                    coCorretagem = 'N';
                    valorSeguralta = (double.Parse(txtComissaoVenda.Text) * percCorretora * impostoNota);
                    valor = valorSeguralta.ToString("F2", CultureInfo.InvariantCulture);

                    sql = String.Format(query,
                           /*{0}*/ txtIdCliente.Text,
                           /*{1}*/ txtValorLiquido.Text.Replace(".", "").Replace(",", "."),
                           /*{2}*/ txtIof.Text.Replace(".", "").Replace(",", "."),
                           /*{3}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."),
                           /*{4}*/ txtComissao.Text.Replace(".", "").Replace(",", "."),
                           /*{5}*/ txtComissaoVenda.Text.Replace(".", "").Replace(",", "."),
                           /*{6}*/ coCorretagem,
                           /*{7}*/ valor,
                           /*{8}*/ txtCodigo.Text);
                }
                MetodosSql.ExecQuery(sql);

                  sql = String.Format(@"SELECT * FROM ITEMMOVIMENTO WHERE IDVENDA = '{0}'", txtCodigo.Text);
                IDPRODUTO = MetodosSql.GetField(sql, "IDPRODUTO");

                    sql = String.Format("UPDATE ITEMMOVIMENTO SET IDPRODUTO = '{0}', VALOR = '{1}', QUANTIDADE = 1, DATAINCLUSAO = getdate() WHERE IDVENDA = '{2}'",
                                                  /*{0}*/ IDPRODUTO,
                                                  /*{1}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."),
                                                  /*{2}*/ txtCodigo.Text);
                    MetodosSql.ExecQuery(sql);
                

                 MetodosSql.ExecQuery(String.Format(@"DELETE FROM PARCELA WHERE IDVENDA = '{0}'", txtCodigo.Text));
                InsertParcela();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void Cadastro()
        {
            try
            {
                if(Editar)
                {
                    UpdateVenda();
                }
               else
                {
                    InsertVenda();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
	                P.DESCRICAO ,
                    P.CIASEGURADORA AS CIASEGURADORA,
                    P.OBSERVACAO
                    from ITEMMOVIMENTO IM
                    inner join PRODUTO P
                    on P.IDPRODUTO = IM.IDPRODUTO 
                    where IDVENDA = '{0}'
                    ORDER BY IDITEM DESC", txtCodigo.Text));
                    gridView1.BestFitColumns();
	                
	                
	                 
                    //txtTotalVenda.Text = MetodosSql.GetField(String.Format(@"select cast(sum(QUANTIDADE* VALOR) as numeric(20, 2)) as 'TOTAL' from ITEMMOVIMENTO where IDVENDA = '{0}'", txtCodigo.Text), "TOTAL");
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
                    //double Total = 0;

                    //foreach (Produto P in produtos)
                    //{
                    //    Total += Convert.ToDouble(P.VALORTOTAL);
                    //}
                    //txtTotalVenda.Text = String.Format("{0:N}", Total);

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

        



        private void frmCadastroVenda_Load(object sender, EventArgs e)
        {
            try
            {
                
                double valorLiquido, iof, valorTotal, comissaoVenda;
                if (Editar)
                {
                    txtCodigo.Text = Cod;
                    btnSalvar.Enabled = false;
                    sql = String.Format(@"select * from VENDA where IDVENDA = {0}", Cod);
                    idVendedor = MetodosSql.GetField(sql, "IDVENDEDOR");
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
                    sql = String.Format(@"SELECT * FROM VENDACOMISSAO WHERE IDVENDA = '{0}'", Cod);
                    valorLiquido = double.Parse(MetodosSql.GetField(sql, "VALORLIQUIDO"));
                    txtValorLiquido.Text = valorLiquido.ToString("F2");
                    iof = double.Parse(MetodosSql.GetField(sql, "IOF"));
                    txtIof.Text = iof.ToString("F2");
                    comissaoVenda = double.Parse(MetodosSql.GetField(sql, "COMISSAO"));
                    txtComissao.Text = comissaoVenda.ToString("F2");
                    valorTotal = double.Parse(MetodosSql.GetField(sql, "TOTALVENDA"));
                    txtTotalVenda.Text = valorTotal.ToString("F2");
                    txtValorTotal.Text = txtTotalVenda.Text;
                    comissaoVenda = double.Parse(MetodosSql.GetField(sql, "COMISSAOVENDA"));
                    txtComissaoVenda.Text = comissaoVenda.ToString("F2");
                     string desconto = String.Format(@"select * from VENDA where IDVENDA = {0}", Cod);
                    double Desconto = double.Parse(MetodosSql.GetField(desconto, "DESCONTO"));
                    txtDesconto.Text = Desconto.ToString("F2");
                    if (MetodosSql.GetField(sql, "COCORRETAGEM") == "S")
                    {
                        cbCoCorretagem.Checked = true;
                    }
                    
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
            if(Editar)
            {
                string sql = String.Format(@"SELECT COUNT(IDPRODUTO) AS QUANTIDADE FROM ITEMMOVIMENTO WHERE IDVENDA = '{0}'", txtCodigo.Text);
                string count = MetodosSql.GetField(sql, "QUANTIDADE");
                if (SALVAR == "0")
                {
                    this.Close();
                }
                else if (String.IsNullOrWhiteSpace(txtTipoPagamento.Text))
                {
                    MessageBox.Show("Selecione Um Tipo De Pagameto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (String.IsNullOrWhiteSpace(txtTotalVenda.Text))
                {
                    MessageBox.Show("Selecione um Produto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrWhiteSpace(txtIdVendedor.Text))
                {
                    MessageBox.Show("Selecione um vendedor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (count != "0")
                {
                    Cadastro();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(@"Por favor, selecione um seguro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
           else
            {
                string count = produtos.Count.ToString();
                if (SALVAR == "0")
                {
                    this.Close();
                }
                else if (String.IsNullOrWhiteSpace(txtTipoPagamento.Text))
                {
                    MessageBox.Show("Selecione Um Tipo De Pagameto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (String.IsNullOrWhiteSpace(txtTotalVenda.Text))
                {
                    MessageBox.Show("Selecione um Produto", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrWhiteSpace(txtIdVendedor.Text))
                {
                    MessageBox.Show("Selecione um vendedor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (count != "0")
                {
                    Cadastro();
                    this.Close();
                }
                else
                {
                    MessageBox.Show(@"Por favor, selecione um seguro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
               
            
            
        }
       

        private void LimpaCampos()
        {
            txtCodigoProduto.Text = String.Empty;
            txtDescricaoProduto.Text = String.Empty;
            txtQuantidade.Text = "1";
            txtCiaSeguradora.Text = String.Empty;
        }



        private void btnSelecionaProduto_Click(object sender, EventArgs e)
        {
            frmVisaoSelecionaProduto frm = new frmVisaoSelecionaProduto();
            frm.ShowDialog();
            txtCodigoProduto.Text = frm.CODIGO;
            txtDescricaoProduto.Text = frm.DESCRICAO;
            txtCiaSeguradora.Text = frm.CIASEGURADORA;
            OBS = frm.OBSERVACAO;

            //sql = String.Format(@"select * from PRODUTO where IDPRODUTO = '{0}'", frm.CODIGO);
            //txtValorUnitario.Text = MetodosSql.GetField(sql, "PRECOUNVENDA");
            //CalculaTotal();
        }
        private void txtValorLiquido_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtValorLiquido.Text))
            {
                txtValorLiquido.Text = "0,00";
            }
            else
            {
                double valorLiquido = Convert.ToDouble(txtValorLiquido.Text);
                txtValorLiquido.Text = String.Format("{0:N}", valorLiquido).Replace(".", "").Replace(".", ",");
            }
        }

        private void txtIof_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtIof.Text))
            {
                txtIof.Text = "0,00";
                double iof = Convert.ToDouble(txtIof.Text);
                txtIof.Text = String.Format("{0:N}", iof).Replace(".", "").Replace(".", ",");

                if (!String.IsNullOrWhiteSpace(txtValorLiquido.Text))
                {
                    double valorLiquido = double.Parse(txtValorLiquido.Text);
                    double valorTotal = Convert.ToDouble(valorLiquido + iof);
                    txtValorTotal.Text = String.Format("{0:N}", valorTotal);

                }
                else
                {
                    txtValorLiquido.Text = "0,00";
                    double valorLiquido = double.Parse(txtValorLiquido.Text);
                    double valorTotal = Convert.ToDouble(valorLiquido + iof);
                    txtValorTotal.Text = String.Format("{0:N}", valorTotal);
                }
                txtTotalVenda.Text = txtValorTotal.Text;
                txtTotalDesconto.Text = txtValorTotal.Text;
            }
            else
            {
                double iof = Convert.ToDouble(txtIof.Text);
                txtIof.Text = String.Format("{0:N}", iof).Replace(".", "").Replace(".", ",");

                if (!String.IsNullOrWhiteSpace(txtValorLiquido.Text))
                {
                    double valorLiquido = double.Parse(txtValorLiquido.Text);
                    double valorTotal = Convert.ToDouble(valorLiquido + iof);
                    txtValorTotal.Text = String.Format("{0:N}", valorTotal);

                }
                else
                {
                    txtValorLiquido.Text = "0,00";
                    double valorLiquido = double.Parse(txtValorLiquido.Text);
                    double valorTotal = Convert.ToDouble(valorLiquido + iof);
                    txtValorTotal.Text = String.Format("{0:N}", valorTotal);
                }
                txtTotalVenda.Text = txtValorTotal.Text;
            }


        }
        private void txtComissao_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtComissao.Text))
            {
                txtComissao.Text = "0,00";
                double valorLiquido = double.Parse(txtValorLiquido.Text);
                double comissao = double.Parse(txtComissao.Text);
                double comissaoVenda = valorLiquido * comissao / 100;

                txtComissaoVenda.Text = String.Format("{0:N}", comissaoVenda);
            }
            else
            {
                double valorLiquido = double.Parse(txtValorLiquido.Text);
                double comissao = double.Parse(txtComissao.Text);
                double comissaoVenda = valorLiquido * comissao / 100;

                txtComissaoVenda.Text = String.Format("{0:N}", comissaoVenda);
            }
        }
        private void txtValorLiquido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }


        private void txtDesconto_TextChanged_1(object sender, EventArgs e)
        {
            CalculaTotalDesconto();
        }

        private void txtDesconto_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void txtIof_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void txtComissao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
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
                                                  /*{2}*/ txtValorTotal.Text.Replace(".", "").Replace(",", "."),
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
                                                    /*{0}*/ txtIof.Text.Replace(",", "."),
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
                            produto.CIASEGURADORA = txtCiaSeguradora.Text;
                            produto.OBSERVACAO = OBS;
                            //produto.PRECOUNITARIO = txtIof.Text;
                            //produto.QUANTIDADE = txtQuantidade.Text;
                            //produto.VALORTOTAL = txtValorTotal.Text;
                            produtos.Add(produto);
                        }
                        else
                        {
                            //int indice = int.Parse(IDITEM);
                            //produtos[indice].PRECOUNITARIO = txtIof.Text;
                            //produtos[indice].QUANTIDADE = txtQuantidade.Text;
                            //produtos[indice].VALORTOTAL = txtValorTotal.Text;
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
                    
                    sql = String.Format(@"select DESCRICAO from PRODUTO where IDPRODUTO = '{0}'", txtCodigoProduto.Text);
                    txtDescricaoProduto.Text = MetodosSql.GetField(sql, "DESCRICAO");
                    

                    //CalculaTotal();


                }
                else
                {
                    int indice = int.Parse(rowHandle.ToString());
                    txtCodigoProduto.Text = produtos[indice].IDPRODUTO;
                    //txtIof.Text = produtos[indice].PRECOUNITARIO;
                    //txtQuantidade.Text = produtos[indice].QUANTIDADE;
                    //txtValorTotal.Text = produtos[indice].VALORTOTAL;
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

                txtIof.Text = String.Empty;
                txtValorLiquido.Text = String.Empty;
                txtComissao.Text = String.Empty;
                txtComissaoVenda.Text = String.Empty;
                txtTotalVenda.Text = String.Empty;
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


                    




















             










































































































































