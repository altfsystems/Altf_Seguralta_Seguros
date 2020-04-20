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
    public partial class frmServico : Form
    {

        Boolean Editar;
        string Cod;
        string sql;
        List<Produto> produtos = new List<Produto>();
        string IDITEM = null;

        public class Produto
        {

            public String IDPRODUTO { get; set; }
            public String DESCRICAO { get; set; }
            public String PRECOUNITARIO { get; set; }
            public String QUANTIDADE { get; set; }
            public String VALORTOTAL { get; set; }
        }




        public frmServico(bool Editar_, string Cod_)
        {
            InitializeComponent();
            Editar = Editar_;
            Cod = Cod_;

            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            txtDataInclusao.Text = DateTime.Now.ToString();
            gridView1.BestFitColumns();


        }

        private void CalculaTotal()
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtCodigoProduto.Text))
                {

                    double total = (Convert.ToDouble(txtValorUnitario.Text) * Convert.ToDouble(txtQuantidade.Text));
                    txtValorTotal.Text = String.Format("{0:N}", total);

                    double valorUnitario = Convert.ToDouble(txtValorUnitario.Text);
                    txtValorUnitario.Text = String.Format("{0:N}", valorUnitario);

                    double quantidade = Convert.ToDouble(txtQuantidade.Text);
                    txtQuantidade.Text = String.Format("{0:N}", quantidade);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
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
                }

            }
            txtStatus.Text = "A";
        }

        private void btnOk_Click(object sender, EventArgs e)
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

        private void frmServico_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = Cod;
            try
            {
                if (Editar)
                {
                    txtCodigo.Text = Cod;

                    string sql = String.Format(@"select * from ORDEM where IDORDEM = {0}", Cod);
                    txtCodigoFcfo.Text = MetodosSql.GetField(sql, "IDFCFO");
                    txtDescricao.Text = MetodosSql.GetField(sql, "DESCRICAO");
                    txtConclusao.Text = MetodosSql.GetField(sql, "CONCLUSAO");
                    txtObservacao.Text = MetodosSql.GetField(sql, "OBSERVACAO");
                    txtStatus.Text = MetodosSql.GetField(sql, "STATUS");
                    txtNomeCliente.Text = MetodosSql.GetField(String.Format(@"select * from ORDEM INNER JOIN FCFO ON FCFO.IDFCFO = ORDEM.IDFCFO WHERE IDORDEM = {0}", Cod), "NOME");
                    txtSobrenome.Text = MetodosSql.GetField(String.Format(@"select * from ORDEM inner join FCFO ON FCFO.IDFCFO = ORDEM.IDFCFO WHERE IDORDEM = {0}", Cod), "NOMEFANTASIA");
                    txtDataInclusao.Text = MetodosSql.GetField(String.Format(@"select  DATAINCLUSAO as 'Nasc' from ORDEM where IDORDEM = {0}", Cod), "Nasc");
                    AtualizaGrid();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void Cadastro()
        {
            try
            {
                if (Editar)
                {
                    string SQL = String.Format(@"update ORDEM
                                                      set IDFCFO = '{0}' ,
                                                      DESCRICAO = '{1}' ,                  
                                                      CONCLUSAO = '{2}'  ,  
                                                      OBSERVACAO = '{3}'  ,
                                                      STATUS = '{4}' , 
                                                      DATAINCLUSAO = getdate()
                                                      where IDORDEM = '{5}' ", txtCodigoFcfo.Text, txtDescricao.Text, txtConclusao.Text, txtObservacao.Text,
                                                       txtStatus.Text, Cod);
                    Clipboard.SetText(SQL);
                    MetodosSql.ExecQuery(SQL);


                }
                else
                {

                    sql = String.Format(@"insert into ORDEM (IDFCFO , DESCRICAO , CONCLUSAO , OBSERVACAO , STATUS , DATAINCLUSAO ) values
                  ('{0}' , '{1}' , '{2}' , '{3}' , 'A' , getdate()) select SCOPE_IDENTITY() ", txtCodigoFcfo.Text, txtDescricao.Text, txtConclusao.Text, txtObservacao.Text, txtStatus.Text, txtDataInclusao.Text);


                    object IDORDEM = MetodosSql.ExecScalar(sql);
                    txtCodigo.Text = IDORDEM.ToString();




                    foreach (Produto p in produtos)
                    {
                        sql = String.Format("insert into ITEMMOVIMENTO (IDORDEM, IDPRODUTO, VALOR, QUANTIDADE, DATAINCLUSAO) values ('{0}','{1}','{2}','{3}', GETDATE())",
                                              /*{0}*/ IDORDEM.ToString(),
                                              /*{1}*/ p.IDPRODUTO,
                                              /*{2}*/ p.PRECOUNITARIO.Replace(".", "").Replace(",", "."),
                                              /*{3}*/ p.QUANTIDADE.Replace(",", "."));

                        MetodosSql.ExecQuery(sql);
                    }


                    Editar = true;
                    AtualizaGrid();


                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmVisaoSelecionaClienteOrdem frm = new frmVisaoSelecionaClienteOrdem();
            frm.ShowDialog();

            txtCodigoFcfo.Text = frm.CODIGO;
            txtNomeCliente.Text = frm.NOME;
            txtSobrenome.Text = frm.SOBRENOME;
        }

        private void btnSelecionaProduto_Click(object sender, EventArgs e)
        {
            frmVisaoSelecionaProdutoOrdem frm = new frmVisaoSelecionaProdutoOrdem();
            frm.ShowDialog();



            txtCodigoProduto.Text = frm.CODIGO;
            txtDescricaoProduto.Text = frm.DESCRICAO;
            txtValorUnitario.Text = frm.VALOR;
            CalculaTotal();


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
	                                                                                  P.DESCRICAO, 
	                                                                                  cast(IM.VALOR as numeric(20,2)) as 'VALOR UNITARIO', 
	                                                                                  cast(IM.QUANTIDADE as numeric(20,2)) as 'QUANTIDADE',
	                                                                                  cast((IM.QUANTIDADE*IM.VALOR) as numeric(20,2)) as 'TOTAL'
	   
                                                                               from ITEMMOVIMENTO IM

                                                                               inner join PRODUTO P
                                                                               on P.IDPRODUTO = IM.IDPRODUTO 
                                        
                                                                               where IDORDEM = '{0}'", txtCodigo.Text));

                    gridView1.BestFitColumns();
                    txtTotalVenda.Text = MetodosSql.GetField(String.Format(@"select cast(sum(VALOR * QUANTIDADE)as numeric(20, 2)) AS 'TOTAL' FROM ITEMMOVIMENTO WHERE IDORDEM = '{0}'", txtCodigo.Text), "TOTAL");

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

        private void btnAdicionar_Click(object sender, EventArgs e)
        {




            try
            {
                if (String.IsNullOrWhiteSpace(txtValorUnitario.Text))
                {
                    throw new Exception("Selecionar um Produto");
                }
                else
                {


                    if (!String.IsNullOrWhiteSpace(txtCodigoProduto.Text))
                    {
                        if (Editar)
                        {


                            if (String.IsNullOrWhiteSpace(IDITEM))
                            {
                                sql = String.Format("insert into ITEMMOVIMENTO (IDORDEM, IDPRODUTO, VALOR, QUANTIDADE, DATAINCLUSAO) values ('{0}','{1}','{2}','{3}', GETDATE())",
                                                      /*{0}*/ txtCodigo.Text,
                                                      /*{1}*/ txtCodigoProduto.Text,
                                                      /*{2}*/ txtValorUnitario.Text.Replace(".", "").Replace(",", "."),
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
                }
                AtualizaGrid();
                LimpaCampos();

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtValorUnitario_Leave(object sender, EventArgs e)
        {
            CalculaTotal();
        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            CalculaTotal();
        }

        private void txtValorTotal_Leave(object sender, EventArgs e)
        {
            CalculaTotal();
        }

        private void LimpaCampos()
        {
            txtCodigoProduto.Text = String.Empty;
            txtDescricaoProduto.Text = String.Empty;
            txtQuantidade.Text = "1";
            txtValorUnitario.Text = String.Empty;
            txtValorTotal.Text = String.Empty;
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

                LimpaCampos();
                AtualizaGrid();
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

       
    }
}
