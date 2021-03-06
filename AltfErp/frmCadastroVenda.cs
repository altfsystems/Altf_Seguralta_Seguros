﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using DevExpress.ClipboardSource.SpreadsheetML;
using System.IO;

namespace AltfErp
{
    public partial class frmCadastroVenda : Form
    {
        Boolean Editar;
        List<Produto> produtos = new List<Produto>();
        List<Anexos> anexosVisao = new List<Anexos>();
        string IDITEM = null;
        string IDCIA = null;
        string DESCIA = null;
        string IDVENDA;
        string indiceItem;
        bool vendaClick;
        string data1, idVendedor, status, Cod, sql, IDPRODUTO;
        int ano, dia, mes;
        string diaVencimento, mesVencimento, idCia;
        public string VENCIMENTOANUAL { get; set; }
        public string OBS { get; set; }
        public class Produto
        {
            public String IDPRODUTO { get; set; }
            public String DESCRICAO { get; set; }
            public String CIASEGURADORA { get; set; }
            public String OBSERVACAO { get; set; }           
        }
        public class Anexos
        {
            public String Nome { get; set; }
            public String Arquivo { get; set; }
            public String Extensão { get; set; }
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
            btnSelecionaVendedor.Select();
        }
        private void InsereImagem()
        {
            try
            {
                if(!Editar)
                {
                    foreach(Anexos anexo in anexosVisao)
                    {
                        string caminho = anexo.Arquivo;
                        byte[] imagem = File.ReadAllBytes(caminho);
                        MetodosSql.InsereImagem(String.Format("INSERT INTO FCFOIMAGEM(IDVENDA, IMAGEM1, NOMEANEXO, EXTENSAO, CAMINHO) VALUES('{0}', @Imagem, '{1}', '{2}', '{3}')", IDVENDA, anexo.Nome, anexo.Extensão, caminho), imagem);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private Boolean ValidaCadastro()
        {
            int count;
            if (Editar)
            {
                string sql = String.Format(@"SELECT COUNT(IDPRODUTO) AS QUANTIDADE FROM ITEMMOVIMENTO WHERE IDVENDA = '{0}'", txtCodigo.Text);
                count = int.Parse(MetodosSql.GetField(sql, "QUANTIDADE"));
            }
            else
            {
                count = int.Parse(produtos.Count.ToString());
            }
            if(txtComissao.Text == "0,00")
            {
                txtComissaoVenda.Text = "0,00";
            }

            
            if (String.IsNullOrWhiteSpace(txtIdVendedor.Text))
            {
                MessageBox.Show("Selecione um vendedor", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (String.IsNullOrWhiteSpace(txtIdCliente.Text))
            {
                MessageBox.Show("Selecione um cliente", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (count == 0)
            {
                MessageBox.Show(@"Por favor, selecione um seguro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (String.IsNullOrWhiteSpace(txtTipoPagamento.Text))
            {
                MessageBox.Show("Selecione Um Tipo De Pagameto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            else if (String.IsNullOrWhiteSpace(txtComissao.Text))
            {
                MessageBox.Show("Insira uma comissão", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!TestaData())
            {
                MessageBox.Show("Selecione um vencimento válido para as parcelas", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }

        }
        private Boolean TestaData()
        {
            Boolean retorno;

            if (String.IsNullOrWhiteSpace(txtDia.Text) || String.IsNullOrWhiteSpace(txtMes.Text) || String.IsNullOrWhiteSpace(txtAno.Text)) { retorno = false; }
            else { retorno = true; }
            return retorno;
        }
        private void Calculos()
        {
            if (String.IsNullOrWhiteSpace(txtValorLiquido.Text)) { txtValorLiquido.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtIof.Text)) { txtIof.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtComissao.Text)) { txtComissao.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtDesconto.Text)) { txtDesconto.Text = "0,00"; txtTotalDesconto.Text = txtTotalVenda.Text; txtComissaoVenda.Text = "0,00"; }

            double valorLiquido = Convert.ToDouble(txtValorLiquido.Text);
            txtValorLiquido.Text = String.Format("{0:N}", valorLiquido).Replace(".", "").Replace(".", ",");


            double iof = Convert.ToDouble(txtIof.Text);
            txtIof.Text = String.Format("{0:N}", iof).Replace(".", "").Replace(".", ",");

            valorLiquido = double.Parse(txtValorLiquido.Text);
            double valorTotal = Convert.ToDouble(valorLiquido + iof);
            txtValorTotal.Text = String.Format("{0:N}", valorTotal);

            txtTotalVenda.Text = txtValorTotal.Text;

            valorLiquido = double.Parse(txtValorLiquido.Text);
            valorTotal = Convert.ToDouble(valorLiquido + iof);
            txtValorTotal.Text = String.Format("{0:N}", valorTotal);
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

                if (parcela == 1)
                {
                    Cod = Codparcela.ToString();
                }
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
            if (String.IsNullOrWhiteSpace(txtDesconto.Text))
            {
                txtDesconto.Text = "0";
            }
            data1 = txtDia.Text + "/" + txtMes.Text + "/" + txtAno.Text;

            sql = String.Format(@"insert into VENDA (IDFCFO, IDVENDEDOR, IDORDEM, TIPOPAGAMENTO, DESCONTO, OBSERVACAO, STATUS, DATAINCLUSAO, DATAPAGAMENTO, DATAVENCIMENTO, IDCIASEGURADORA) values(
                                    '{0}' ,{4}, null, '{1}' ,{2}, '{3}' , 'A' , getdate() , null, CONVERT(DATETIME, CONVERT(VARCHAR,'{5}', 121),103), '{6}') select SCOPE_IDENTITY()",
                                txtIdCliente.Text, txtTipoPagamento.Text, txtDesconto.Text.Replace(".", "").Replace(",", "."), txtObservacao.Text, txtIdVendedor.Text,
                                txtDataVencimento.Text, idCia);
            object idVenda = MetodosSql.ExecScalar(sql);
            txtCodigo.Text = idVenda.ToString();
            IDVENDA = idVenda.ToString();


            string valor;
            char coCorretagem;
            double valorSeguralta;
            string query = @"INSERT INTO VENDACOMISSAO(IDVENDA, IDCLIENTE, VALORLIQUIDO, IOF, TOTALVENDAORIGINAL, COMISSAO, COMISSAOVENDA, COCORRETAGEM, VALORSEGURALTA, TOTALVENDADESCONTO, DATAPARCELA)
                               VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', CONVERT(DATETIME, CONVERT(VARCHAR,'{10}', 121),103))";
            data1 = txtDia.Text + "/" + txtMes.Text + "/" + txtAno.Text;

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
                              /*{4}*/ txtValorTotal.Text.Replace(".", "").Replace(",", "."),
                              /*{5}*/ txtComissao.Text.Replace(".", "").Replace(",", "."),
                              /*{6}*/ txtComissaoVenda.Text.Replace(".", "").Replace(",", "."),
                              /*{7}*/ coCorretagem,
                              /*{8}*/ valor,
                              /*{9}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."),
                             /*{10}*/ data1);

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
                              /*{4}*/ txtValorTotal.Text.Replace(".", "").Replace(",", "."),
                              /*{5}*/ txtComissao.Text.Replace(".", "").Replace(",", "."),
                              /*{6}*/ txtComissaoVenda.Text.Replace(".", "").Replace(",", "."),
                              /*{7}*/ coCorretagem,
                              /*{8}*/ valor,
                              /*{9}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."),
                             /*{10}*/ data1);

            }
            MetodosSql.ExecQuery(sql);


            foreach (Produto p in produtos)
            {
                sql = String.Format("insert into ITEMMOVIMENTO (IDVENDA, IDPRODUTO, QUANTIDADE, DATAINCLUSAO) values ('{0}','{1}','1', GETDATE())",
                                              /*{0}*/ IDVENDA.ToString(),
                                              /*{1}*/ p.IDPRODUTO);

                MetodosSql.ExecQuery(sql);
            }

            InsertParcela();


            if (!vendaClick)
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
                if (String.IsNullOrWhiteSpace(txtDesconto.Text))
                {
                    txtDesconto.Text = "0";
                }
                data1 = txtDia.Text + "/" + txtMes.Text + "/" + txtAno.Text;

                string sql = String.Format(@"UPDATE VENDA SET IDFCFO = '{0}', IDVENDEDOR = '{1}', TIPOPAGAMENTO = '{2}', DESCONTO = '{3}', OBSERVACAO = '{4}', STATUS = 'A',
                                             DATAINCLUSAO = GETDATE(), DATAVENCIMENTO = CONVERT(DATETIME, CONVERT(VARCHAR,'{5}', 121),103), IDCIASEGURADORA = '{6}'  WHERE IDVENDA = '{7}' ",
                                           /*{0}*/  txtIdCliente.Text,
                                           /*{1}*/  txtIdVendedor.Text,
                                           /*{2}*/  txtTipoPagamento.Text,
                                           /*{3}*/  txtDesconto.Text.Replace(".", "").Replace(",", "."),
                                           /*{4}*/  txtObservacao.Text,
                                           /*{5}*/  txtDataVencimento.Text,
                                           /*{6}*/  idCia,
                                           /*{7}*/  txtCodigo.Text);
                MetodosSql.ExecQuery(sql);


                string valor;
                char coCorretagem;
                double valorSeguralta;
                string query = @"UPDATE VENDACOMISSAO SET IDCLIENTE = '{0}', VALORLIQUIDO = '{1}', IOF = '{2}', TOTALVENDAORIGINAL = '{3}', COMISSAO = '{4}', COMISSAOVENDA = '{5}',
                                    COCORRETAGEM = '{6}', VALORSEGURALTA = '{7}', TOTALVENDADESCONTO = '{8}' WHERE IDVENDA = '{9}'";


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
                           /*{3}*/ txtValorTotal.Text.Replace(".", "").Replace(",", "."),
                           /*{4}*/ txtComissao.Text.Replace(".", "").Replace(",", "."),
                           /*{5}*/ txtComissaoVenda.Text.Replace(".", "").Replace(",", "."),
                           /*{6}*/ coCorretagem,
                           /*{7}*/ valor,
                           /*{8}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."),
                           /*{9}*/ txtCodigo.Text);

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
                           /*{3}*/ txtValorTotal.Text.Replace(".", "").Replace(",", "."),
                           /*{4}*/ txtComissao.Text.Replace(".", "").Replace(",", "."),
                           /*{5}*/ txtComissaoVenda.Text.Replace(".", "").Replace(",", "."),
                           /*{6}*/ coCorretagem,
                           /*{7}*/ valor,
                           /*{8}*/ txtTotalDesconto.Text.Replace(".", "").Replace(",", "."),
                           /*{9}*/ txtCodigo.Text);
                }
                MetodosSql.ExecQuery(sql);
                                    


                
                foreach(Produto p in produtos)
                {
                    IDPRODUTO = p.IDPRODUTO;
                    sql = String.Format("UPDATE ITEMMOVIMENTO SET IDPRODUTO = '{0}', QUANTIDADE = 1, DATAINCLUSAO = getdate() WHERE IDVENDA = '{1}' AND IDPRODUTO = '{2}'",
                                             /*{0}*/ IDPRODUTO,
                                             /*{1}*/ txtCodigo.Text,
                                             /*{2}*/ p.IDPRODUTO);
                                             
                    MetodosSql.ExecQuery(sql);
                }


               MetodosSql.ExecQuery(String.Format(@"DELETE FROM PARCELA WHERE IDVENDA = '{0}'", txtCodigo.Text));
                InsertParcela();

                if(!vendaClick)
                {
                    frmIdPagamento frm = new frmIdPagamento(false, null, true);
                    frm.txtValorRestante.Enabled = false;
                    frm.label7.Enabled = false;
                    frm.CODIGOVENDA = txtCodigo.Text;
                    frm.CODIGOPARCELA = Cod.ToString();
                    frm.CODIGOCLIENTE = txtIdCliente.Text;
                    frm.ShowDialog();
                }
                
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
                if (Editar)
                {
                    UpdateVenda();
                    InsereImagem();
                }
                else
                {
                    InsertVenda();
                    InsereImagem();
                    Editar = true;
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
                    string IDCIASEG = MetodosSql.GetField(String.Format("SELECT IDCIASEGURADORA FROM VENDA WHERE IDVENDA = '{0}'", txtCodigo.Text), "IDCIASEGURADORA");
                    string DESCIASEG = MetodosSql.GetField(String.Format("SELECT NOMEFANTASIA FROM FCFOSEGURADORA WHERE IDSEGURADORA = '{0}'", IDCIASEG), "NOMEFANTASIA");
                    IDCIA = IDCIASEG;
                    DESCIA = DESCIASEG;
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = MetodosSql.GetDT(String.Format(@"select IM.IDITEM, 
                    IM.IDPRODUTO, 
	                P.DESCRICAO ,
                    '{1}' AS CIASEGURADORA,
                    P.OBSERVACAO
                    from ITEMMOVIMENTO IM
                    inner join PRODUTO P
                    on P.IDPRODUTO = IM.IDPRODUTO 
                    where IDVENDA = '{0}'
                    ORDER BY IDITEM DESC", txtCodigo.Text, DESCIASEG));
                    gridView1.BestFitColumns();




                    //txtTotalVenda.Text = MetodosSql.GetField(String.Format(@"select cast(sum(QUANTIDADE* VALOR) as numeric(20, 2)) as 'TOTAL' from ITEMMOVIMENTO where IDVENDA = '{0}'", txtCodigo.Text), "TOTAL");

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
        private void frmCadastroVenda_Load(object sender, EventArgs e)
        {
            try
            {


                double valorLiquido, iof, valorTotal, comissaoVenda;
                if (Editar)
                {
                    string dataParcela = MetodosSql.GetField(String.Format(@"SELECT CONVERT(varchar, CONVERT(varchar, DATAPARCELA, 103)) AS DATAPARCELA FROM VENDACOMISSAO WHERE IDVENDA = '{0}'", Cod), "DATAPARCELA");
                    string[] vet = dataParcela.Split('/');
                    string dia = vet[0];
                    string mes = vet[1];
                    string ano = vet[2];

                    txtCodigo.Text = Cod;
                    IDVENDA = txtCodigo.Text;
                    gridControl2.DataSource = MetodosSql.GetDT("SELECT IDIMAGEM, NOMEANEXO, EXTENSAO FROM FCFOIMAGEM WHERE IDVENDA = "+txtCodigo.Text);
                    sql = String.Format(@"select * from VENDA where IDVENDA = {0}", Cod);
                    idVendedor = MetodosSql.GetField(sql, "IDVENDEDOR");
                    txtIdCliente.Text = MetodosSql.GetField(sql, "IDFCFO");
                    txtTipoPagamento.Text = MetodosSql.GetField(sql, "TIPOPAGAMENTO");
                    txtObservacao.Text = MetodosSql.GetField(sql, "OBSERVACAO");
                    txtStatus.Text = status;
                    txtDia.Text = dia; txtMes.Text = mes; txtAno.Text = ano;
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
                    txtValorLiquido.Text = String.Format("{0:N}", valorLiquido).Replace(".", "").Replace(".", ",");
                    iof = double.Parse(MetodosSql.GetField(sql, "IOF"));
                    txtIof.Text = String.Format("{0:N}", iof).Replace(".", "").Replace(".", ",");
                    comissaoVenda = double.Parse(MetodosSql.GetField(sql, "COMISSAO"));
                    txtComissao.Text = comissaoVenda.ToString("F2");
                    valorTotal = double.Parse(MetodosSql.GetField(sql, "TOTALVENDAORIGINAL"));
                    txtTotalVenda.Text = String.Format("{0:N}", valorTotal.ToString("F2")).Replace(".", "").Replace(".", ",");
                    txtValorTotal.Text = txtTotalVenda.Text;
                    comissaoVenda = double.Parse(MetodosSql.GetField(sql, "COMISSAOVENDA"));
                    txtComissaoVenda.Text = String.Format("{0:N}", comissaoVenda).Replace(".", "").Replace(".", ",");
                    string desconto = String.Format(@"select * from VENDA where IDVENDA = {0}", Cod);
                    double Desconto = double.Parse(MetodosSql.GetField(desconto, "DESCONTO"));
                    txtDesconto.Text = String.Format("{0:N}", Desconto).Replace(".", "").Replace(".", ",");
                    if (MetodosSql.GetField(sql, "COCORRETAGEM") == "S")
                    {
                        cbCoCorretagem.Checked = true;
                    }
                    sql = String.Format(@"SELECT CAST(TOTALVENDADESCONTO AS NUMERIC(20,2)) AS TOTALVENDADESCONTO FROM VENDACOMISSAO WHERE IDVENDA = '{0}'", Cod);
                    txtTotalDesconto.Text = String.Format("{0:N}", MetodosSql.GetField(sql, "TOTALVENDADESCONTO")).Replace(".", "").Replace(".", ",");
                }
                else
                {
                    btnDownload.Text = "Abrir";
                }
                AtualizaGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            vendaClick = false;
            if (Editar)
            {
                if (ValidaCadastro())
                {
                    Cadastro(); 
                    this.Close();
                }
            }
            else
            {
                string count = produtos.Count.ToString();

                if (ValidaCadastro())
                {
                    Cadastro();
                    this.Close();
                }
            }
        }
        private void LimpaCampos()
        {
            txtCodigoProduto.Text = String.Empty;
            txtDescricaoProduto.Text = String.Empty;
            txtCiaSeguradora.Text = String.Empty;
            txtIdCia.Text = String.Empty;
        }     
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            frmVisaoSelecionaCliente frm = new frmVisaoSelecionaCliente();
            frm.ShowDialog();
            txtIdCliente.Text = frm.CODIGO;
            txtNome.Text = frm.NOME;
            txtSobrenome.Text = frm.SOBRENOME;
        }               
        private void txtDesconto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }
        private void txtDesconto_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtValorLiquido.Text)) { txtValorLiquido.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtIof.Text)) { txtIof.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtComissao.Text)) { txtComissao.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtTotalVenda.Text)) { txtTotalVenda.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtDesconto.Text))
            {
                txtTotalDesconto.Text = txtTotalVenda.Text;
            }
            else
            {
                double TotalDesconto = Convert.ToDouble(txtTotalVenda.Text) - Convert.ToDouble(txtDesconto.Text);
                txtTotalDesconto.Text = String.Format("{0:N}", TotalDesconto);
            }
        }
        private void btnAdicionarImagem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog file = new OpenFileDialog() { ValidateNames = true, Multiselect = false, Filter = "Arquivos de Imagem|*.pdf;*.jpg;*.JPEG;*.png" })
                {
                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        string extensao = Path.GetExtension(file.FileName);
                        string nome = Path.GetFileName(file.FileName);
                        string caminho = file.FileName;
                        byte[] imagem = File.ReadAllBytes(file.FileName);

                        if (Editar)
                        {
                            MetodosSql.InsereImagem(String.Format("INSERT INTO FCFOIMAGEM(IDVENDA, IMAGEM1, NOMEANEXO, EXTENSAO, CAMINHO) VALUES('{0}', @Imagem, '{1}', '{2}', '{3}')", txtCodigo.Text, nome, extensao, caminho), imagem);
                            gridControl2.DataSource = MetodosSql.GetDT("SELECT IDIMAGEM, NOMEANEXO, EXTENSAO FROM FCFOIMAGEM WHERE IDVENDA = " + txtCodigo.Text);
                        }
                        else
                        {
                            Anexos classe = new Anexos();
                            classe.Arquivo = caminho;
                            classe.Extensão = extensao;
                            classe.Nome = nome;
                            anexosVisao.Add(classe);
                            gridControl2.DataSource = anexosVisao;
                            gridControl2.RefreshDataSource();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }            
        }
        private void btnExcluirImagem_Click(object sender, EventArgs e)
        {
            try
            {
                if (Editar)
                {
                    if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir este anexo?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        var rowHandle = gridView2.FocusedRowHandle;
                        var idImagem = gridView2.GetRowCellValue(rowHandle, "IDIMAGEM");
                        MetodosSql.ExecQuery("DELETE FROM FCFOIMAGEM WHERE IDIMAGEM = " + idImagem);
                        gridControl2.DataSource = MetodosSql.GetDT("SELECT IDIMAGEM, NOMEANEXO, CAMINHO, EXTENSAO FROM FCFOIMAGEM WHERE IDVENDA = " + txtCodigo.Text);
                    }
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir este anexo?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        var rowHandle = gridView2.FocusedRowHandle;
                        var nome = gridView2.GetRowCellValue(rowHandle, "NOME");
                        anexosVisao.RemoveAt(int.Parse(indiceItem.ToString()));
                        gridView2.ClearDocument();
                        gridControl2.DataSource = anexosVisao;
                        gridControl2.RefreshDataSource();
                    }
                }
                btnExcluirImagem.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            var rowHandle = gridView2.FocusedRowHandle;
            if (!Editar)
            {
                int indice = int.Parse(rowHandle.ToString());
                indiceItem = indice.ToString();
            }
            btnExcluirImagem.Enabled = true;
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Editar)
                {
                    var rowHandle = gridView2.FocusedRowHandle;
                    var nomeAnexo = gridView2.GetRowCellValue(rowHandle, "NOMEANEXO");
                    var idImagem = gridView2.GetRowCellValue(rowHandle, "IDIMAGEM");
                    string sql = "SELECT IMAGEM1, EXTENSAO FROM FCFOIMAGEM WHERE IDVENDA = " + IDVENDA + " AND IDIMAGEM = " + idImagem;
                    string extensao = MetodosSql.GetField(sql, "EXTENSAO");

                    if (idImagem != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.FileName = nomeAnexo.ToString();
                        if (extensao == ".pdf")
                        {
                            sfd.Filter = "Arquivos (.pdf)|*.pdf";
                        }
                        else if (extensao == ".jpg")
                        {
                            sfd.Filter = "Arquivos (.jpg)|*.jpg";
                        }
                        else if (extensao == ".jpeg")
                        {
                            sfd.Filter = "Arquivos (.jpeg)|*.jpeg";
                        }
                        else if (extensao == ".png")
                        {
                            sfd.Filter = "Arquivos (.png)|*.png";
                        }
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            string caminho = sfd.FileName;
                            string nome = Path.GetFileName(caminho);
                            byte[] imagem = MetodosSql.GetImagePdf(sql, "IMAGEM1");
                            File.WriteAllBytes(caminho, imagem);
                            MessageBox.Show("Salvo com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor, selecione um cadastro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    var rowHandle = gridView2.FocusedRowHandle;
                    var extensao = gridView2.GetRowCellValue(rowHandle, "Extensão");
                    var fileName = gridView2.GetRowCellValue(rowHandle, "Arquivo");
                    if (extensao.ToString() == ".pdf")
                    {
                        frmVisualizaPdf frm = new frmVisualizaPdf(fileName.ToString());
                        frm.ShowDialog();
                    }
                    else
                    {
                        frmVisualizaImagem frm = new frmVisualizaImagem(fileName.ToString());
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSelecionaCliente_Click(object sender, EventArgs e)
        {
            frmVisaoSelecionaCliente frm = new frmVisaoSelecionaCliente();
            frm.ShowDialog();
            txtIdCliente.Text = frm.CODIGO;
            txtNome.Text = frm.NOME;
            txtSobrenome.Text = frm.SOBRENOME;
        }
        private void btnSelecionaProduto_Click(object sender, EventArgs e)
        {
            frmVisaoSelecionaProduto frm = new frmVisaoSelecionaProduto();
            frm.ShowDialog();
            txtCodigoProduto.Text = frm.CODIGO;
            txtDescricaoProduto.Text = frm.DESCRICAO;
            OBS = frm.OBSERVACAO;
        }
        private void txtIof_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }
        private void txtComissao_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }
        private void txtValorLiquido_Leave(object sender, EventArgs e)
        {            
            if (String.IsNullOrWhiteSpace(txtValorLiquido.Text)) { txtValorLiquido.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtIof.Text)) { txtIof.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtDesconto.Text)) { txtDesconto.Text = "0,00"; }

            double valorLiquido = Convert.ToDouble(txtValorLiquido.Text);
            txtValorLiquido.Text = String.Format("{0:N}", valorLiquido).Replace(".", "").Replace(".", ",");

            double iof = Convert.ToDouble(txtIof.Text);
            txtIof.Text = String.Format("{0:N}", iof).Replace(".", "").Replace(".", ",");

            valorLiquido = double.Parse(txtValorLiquido.Text);
            double valorTotal = Convert.ToDouble(valorLiquido + iof);
            txtValorTotal.Text = String.Format("{0:N}", valorTotal);
            txtTotalVenda.Text = txtValorTotal.Text;
            txtTotalDesconto.Text = txtValorTotal.Text;
            txtDesconto.Text = "0,00";
        }
        private void txtIof_Leave_1(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtValorLiquido.Text)) { txtValorLiquido.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtIof.Text)) { txtIof.Text = "0,00"; }
            if (String.IsNullOrWhiteSpace(txtDesconto.Text)) { txtDesconto.Text = "0,00"; }

            double iof = Convert.ToDouble(txtIof.Text);
            txtIof.Text = String.Format("{0:N}", iof).Replace(".", "").Replace(".", ",");
            double valorLiquido = Convert.ToDouble(txtValorLiquido.Text);
            txtValorLiquido.Text = String.Format("{0:N}", valorLiquido).Replace(".", "").Replace(".", ",");

            valorLiquido = double.Parse(txtValorLiquido.Text);
            double valorTotal = Convert.ToDouble(valorLiquido + iof);
            txtValorTotal.Text = String.Format("{0:N}", valorTotal);
            txtTotalVenda.Text = txtValorTotal.Text;
            txtTotalDesconto.Text = txtValorTotal.Text;
        }
        private void txtComissao_Leave(object sender, EventArgs e)
        {
            //Calculos();
            if (!String.IsNullOrWhiteSpace(txtComissao.Text))
            {
                if (String.IsNullOrWhiteSpace(txtValorLiquido.Text)) { txtValorLiquido.Text = "0,00"; }
                if (String.IsNullOrWhiteSpace(txtIof.Text)) { txtIof.Text = "0,00"; }
                if (String.IsNullOrWhiteSpace(txtComissao.Text)) { txtComissao.Text = "0,00"; }
                if (String.IsNullOrWhiteSpace(txtDesconto.Text)) { txtDesconto.Text = "0,00"; txtTotalDesconto.Text = txtTotalVenda.Text; txtComissaoVenda.Text = "0,00"; }
                if (String.IsNullOrWhiteSpace(txtTotalDesconto.Text)) { txtTotalDesconto.Text = "0,00"; }
                double valorLiquido = Convert.ToDouble(txtValorLiquido.Text);
                double comissao = Convert.ToDouble(txtComissao.Text);
                double comissaoVenda = (valorLiquido * comissao) / 100;
                txtComissaoVenda.Text = String.Format("{0:N}", comissaoVenda);
                txtComissao.Text = String.Format("{0:N}", comissao);
            }
            else
            {
                txtComissaoVenda.Text = String.Empty;
            }
        }
        private void btnTipoPagamento_Click(object sender, EventArgs e)
        {
            frmTipoPagamento frm = new frmTipoPagamento();
            frm.ShowDialog();
            txtTipoPagamento.Text = frm.CODIGO;
            txtDescricaoTipoPagamento.Text = frm.DESCRICAO;
        }
        private void txtValorLiquido_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
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
                    txtIdCia.Text = IDCIA;
                    txtCiaSeguradora.Text = DESCIA;
                    sql = String.Format(@"select * from ITEMMOVIMENTO where IDITEM = '{0}'", IDITEM);
                    txtCodigoProduto.Text = MetodosSql.GetField(sql, "IDPRODUTO");
                    sql = String.Format(@"select DESCRICAO from PRODUTO where IDPRODUTO = '{0}'", txtCodigoProduto.Text);
                    txtDescricaoProduto.Text = MetodosSql.GetField(sql, "DESCRICAO");                    
                }
                else
                {
                    int indice = int.Parse(rowHandle.ToString());
                    txtCodigoProduto.Text = produtos[indice].IDPRODUTO;                    
                    txtDescricaoProduto.Text = produtos[indice].DESCRICAO;
                    IDITEM = indice.ToString();
                }
                btnExcluir.Enabled = true;
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

                btnExcluir.Enabled = false;
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
        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtCodigoProduto.Text) && !String.IsNullOrWhiteSpace(txtIdCia.Text))
                {
                    if (Editar)
                    {

                        if (String.IsNullOrWhiteSpace(IDITEM))
                        {
                            sql = String.Format("insert into ITEMMOVIMENTO (IDVENDA, IDPRODUTO, VALOR, QUANTIDADE, DATAINCLUSAO) values ('{0}','{1}','{2}','1', GETDATE())",
                                                  /*{0}*/ txtCodigo.Text,
                                                  /*{1}*/ txtCodigoProduto.Text,
                                                  /*{2}*/ txtValorTotal.Text.Replace(".", "").Replace(",", "."));


                            MetodosSql.ExecQuery(sql);
                        }
                        else
                        {

                            sql = String.Format(@"update ITEMMOVIMENTO
	                                                set  VALOR = '{0}',
			                                        QUANTIDADE = '1',
			                                        DATAINCLUSAO = GETDATE()
		                                            where IDITEM = '{1}'",
                                                    /*{0}*/ txtIof.Text.Replace(",", "."),
                                                    /*{1}*/ IDITEM);

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
                    idCia = txtIdCia.Text;
                    AtualizaGrid();
                    LimpaCampos();
                }
                else if (String.IsNullOrWhiteSpace(txtIdCia.Text))
                {
                    MessageBox.Show("Por favor, selecione uma cia seguradora", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (String.IsNullOrWhiteSpace(txtCodigoProduto.Text))
                {
                    MessageBox.Show("Por favor, selecione um seguro", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnSelecioaCiaSeguradora_Click_1(object sender, EventArgs e)
        {
            frmVisaoSelecionaCiaSeuradora frm = new frmVisaoSelecionaCiaSeuradora();
            frm.ShowDialog();
            Cod = frm.Codigo;

            string sql = String.Format("SELECT IDSEGURADORA, NOMEFANTASIA FROM FCFOSEGURADORA WHERE IDSEGURADORA = '{0}'", Cod);
            txtIdCia.Text = MetodosSql.GetField(sql, "IDSEGURADORA");
            txtCiaSeguradora.Text = MetodosSql.GetField(sql, "NOMEFANTASIA");
        }
        private void txtDesconto_Leave_1(object sender, EventArgs e)
        {
            Calculos();
        }                      
        private void btnSelecionaVendedor_Click_1(object sender, EventArgs e)
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
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            vendaClick = true;
            if (ValidaCadastro())
            {
                btnSalvar.Enabled = false;
                Cadastro();
            }
        }        
    }
}



























































































































































































