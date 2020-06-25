using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AltfErp;

namespace AltfErp
{
    public partial class frmEntradaProduto : Form
    {

        Boolean Editar;
        string Cod;

        public frmEntradaProduto(bool Editar_, string Cod_)
        {
            InitializeComponent();
            Editar = Editar_;
            Cod = Cod_;
            txtDataInclusao.Text = DateTime.Now.ToString();
        



        }

        private void LimpaCampos()
        {
            txtCodigo.Clear();
            txtCodigoProduto.Clear();
            txtDescricaoProduto.Clear();
            txtPrecoVenda.Clear();
            txtPrecoEntrada.Clear();
            txtQuantidadeEntrada.Clear();
            txtValorEntrada.Clear();
            txtMargemVenda.Clear();
            txtValorTotal.Clear();
            txtDocumento.Clear();
            txtChaveNfe.Clear();
            txtObservacao.Clear();
            
            
        }

        
        private void AlteraEstoque()
        {
            string sql = String.Empty;

            if (Editar)
            {
                sql = String.Format(@"update ESTOQUE
                                             set QUANTIDADE = QUANTIDADE - (select QUANTIDADEENTRADA from ENTRADAPRODUTO where IDENTRADA = '{0}')
                                             where IDPRODUTO = '{1}'", txtCodigo.Text, txtCodigoProduto.Text);
                MetodosSql.ExecQuery(sql);
            }
        }

        private void InsereEstoque()
        {
            string sql = String.Empty;

            if (String.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                sql = String.Format(@"update ESTOQUE
                                             set QUANTIDADE = QUANTIDADE + {0}
                                             where IDPRODUTO = '{1}'",txtQuantidadeEntrada.Text,  txtCodigoProduto.Text);
                MetodosSql.ExecQuery(sql);
            }
        }

     


        private void Validar()
        {

            
            
                if (String.IsNullOrWhiteSpace(txtCodigoProduto.Text))
                {
                    throw new Exception("Selecione um Produto");
                }

                if (String.IsNullOrWhiteSpace(txtCodigoFornecedor.Text))
                {
                    throw new Exception("Selecione um Fornecedor");
                }

                if (String.IsNullOrWhiteSpace(txtQuantidadeEntrada.Text))
                {
                    throw new Exception("Digite a Quantidade");
                }

                if (String.IsNullOrWhiteSpace(txtValorEntrada.Text))
                {
                    throw new Exception("Digite o Valor Unitário");
                }                                     
                             
        }          
           


        private void Cadastro()
        {
            try
            {

                Validar();
                string SQL = String.Format(@"insert into ENTRADAPRODUTO (IDPRODUTO , IDFCFO , VALORUNENTRADA , QUANTIDADEENTRADA , CHAVENFE , DOCUMENTO , OBSERVACAO , DATAINCLUSAO) values ('{0}' , '{1}' , REPLACE('{2}',',','.'), REPLACE('{3}',',','.'), '{4}' , '{5}' , '{6}' , CONVERT(varchar,CONVERT(DATETIME, '{7}', 103), 121) ) select SCOPE_IDENTITY()"
               , txtCodigoProduto.Text, txtCodigoFornecedor.Text, txtValorEntrada.Text.Replace(".", "").Replace(",", "."), txtQuantidadeEntrada.Text, txtChaveNfe.Text, txtDocumento.Text, txtObservacao.Text, txtDataInclusao.Text);

                string sql = String.Format(@"update produto set PRECOUNENTRADA = {0} WHERE IDPRODUTO = {1}", txtValorEntrada.Text.Replace(".", "").Replace(",", "."), txtCodigoProduto.Text);
                MetodosSql.ExecQuery(sql);
                InsereEstoque();




                object Ncad = MetodosSql.ExecScalar(SQL);
                txtCodigo.Text = Ncad.ToString();


                Editar = true;
            }
            catch (Exception)
            {
                MessageBox.Show("A(s) Coluna(s) de Texto Não Foram Preenchida(s) Corretamente ");
                
            } 
            
            
          
        }

        private void Salvar()
        {
            try
            {
                

                if (Editar)
                {
                    AlteraEstoque();
                    Validar();
                    string SQL = String.Format(@"update ENTRADAPRODUTO
	                                             set IDPRODUTO = ('{0}'),
			                                         IDFCFO = ('{1}'),
                                                     VALORUNENTRADA = REPLACE('{2}',',','.'),  
                                                     QUANTIDADEENTRADA = REPLACE('{3}',',','.'),
                                                     CHAVENFE = ('{4}'),
                                                     DOCUMENTO = ('{5}'),
                                                     OBSERVACAO = ('{6}'),
                                                     DATAINCLUSAO = CONVERT(DATETIME, '{7}', 103)
                                                     where IDENTRADA = '{8}'

                                                     
		                                             
                                                   ", txtCodigoProduto.Text, txtCodigoFornecedor.Text,
                                  txtValorEntrada.Text.Replace(".", "").Replace(",", "."), txtQuantidadeEntrada.Text, txtChaveNfe.Text, txtDocumento.Text, txtObservacao.Text, txtDataInclusao.Text, txtCodigo.Text);

                    string sql = String.Format(@"update produto set PRECOUNENTRADA = {0} , MARGEMVENDA = {2} WHERE IDPRODUTO = {1}", txtValorEntrada.Text.Replace(".", "").Replace(",", "."), txtCodigoProduto.Text , txtMargemVenda.Text.Replace(".", "").Replace(",", "."));
                    MetodosSql.ExecQuery(sql);


                    Clipboard.SetText(SQL);


                    MetodosSql.ExecQuery(SQL);
                }
                else
                {
                    Validar();
                    string SQL = String.Format(@"insert into ENTRADAPRODUTO (IDPRODUTO , IDFCFO , VALORUNENTRADA , QUANTIDADEENTRADA , CHAVENFE , DOCUMENTO , OBSERVACAO , DATAINCLUSAO) values ('{0}' , '{1}' , REPLACE('{2}',',','.'), REPLACE('{3}',',','.'), '{4}' , '{5}' , '{6}' , CONVERT(varchar,CONVERT(DATETIME, '{7}', 103), 121) ) select SCOPE_IDENTITY()"
                   , txtCodigoProduto.Text, txtCodigoFornecedor.Text, txtValorEntrada.Text.Replace(".", "").Replace(",", "."), txtQuantidadeEntrada.Text, txtChaveNfe.Text, txtDocumento.Text, txtObservacao.Text, txtDataInclusao.Text);

                    string sql = String.Format(@"update produto set PRECOUNENTRADA = {0} WHERE IDPRODUTO = {1}" ,txtValorEntrada.Text.Replace("." , "").Replace(",",".") , txtCodigoProduto.Text);
                    InsereEstoque();
                    MetodosSql.ExecQuery(sql);


                    

                    object Ncad = MetodosSql.ExecScalar(SQL);
                    txtCodigo.Text = Ncad.ToString();

                    
                    Editar = true;

                    
                }

                InsereEstoque();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEntradaProduto_Load(object sender, EventArgs e)
        {
            try
            {
                if (Editar)
                {
                    txtCodigo.Text = Cod;

                    string sql = String.Format(@"select EP.*, CAST((EP.VALORUNENTRADA*EP.QUANTIDADEENTRADA) as numeric(20,2)) as 'VALORTOTAL', P.DESCRICAO, C.NOME, C.NOMEFANTASIA from ENTRADAPRODUTO EP

                                                 inner join PRODUTO P
                                                 on P.IDPRODUTO = EP.IDPRODUTO

                                                 inner join FCFO C
                                                 on C.IDFCFO = EP.IDFCFO
                                                 and C.TIPO = 'F'

                                                 where IDENTRADA = {0}", Cod);

                    txtCodigoProduto.Text = MetodosSql.GetField(sql, "IDPRODUTO");
                    txtCodigoFornecedor.Text = MetodosSql.GetField(sql, "IDFCFO");
                    //txtValorEntrada.Text = MetodosSql.GetField(sql, "VALORUNENTRADA");
                    txtValorEntrada.Text = MetodosSql.GetField(String.Format(@"select CAST(VALORUNENTRADA as numeric(20,2))as 'VE' from ENTRADAPRODUTO where IDENTRADA = {0}", Cod), "VE");
                    //txtQuantidadeEntrada.Text = MetodosSql.GetField(sql, "QUANTIDADEENTRADA");
                    txtQuantidadeEntrada.Text = MetodosSql.GetField(String.Format(@"select CAST(QUANTIDADEENTRADA as numeric(20,2))as 'QE' from ENTRADAPRODUTO where IDENTRADA = {0}", Cod), "QE");
                    txtChaveNfe.Text = MetodosSql.GetField(sql, "CHAVENFE");
                    txtDocumento.Text = MetodosSql.GetField(sql, "DOCUMENTO");
                    txtObservacao.Text = MetodosSql.GetField(sql, "OBSERVACAO");
                    txtDescricaoProduto.Text = MetodosSql.GetField(sql, "DESCRICAO");
                    txtNomeFornecedor.Text = MetodosSql.GetField(sql, "NOME");
                    txtNomeFantasia.Text = MetodosSql.GetField(sql, "NOMEFANTASIA");
                    txtPrecoVenda.Text = MetodosSql.GetField(String.Format("select PRECOUNVENDA FROM PRODUTO where IDPRODUTO = {0}", txtCodigoProduto.Text), "PRECOUNVENDA");
                    txtPrecoEntrada.Text = MetodosSql.GetField(String.Format("select PRECOUNENTRADA FROM PRODUTO WHERE IDPRODUTO = {0}", txtCodigoProduto.Text), "PRECOUNENTRADA");
                    txtDataInclusao.Text = MetodosSql.GetField(String.Format(@"select CONVERT(varchar, CONVERT(DATETIME, DATAINCLUSAO, 121), 103) as 'Nasc' from ENTRADAPRODUTO where IDENTRADA = {0}", Cod), "Nasc");
                    txtValorTotal.Text = MetodosSql.GetField(sql, "VALORTOTAL");
                    double precovenda = Convert.ToDouble(txtPrecoVenda.Text);
                    double valorUnitario = Convert.ToDouble(txtValorEntrada.Text);
                    double porcentagem = ((precovenda * 100) / valorUnitario) - 100;
                    txtMargemVenda.Text = porcentagem.ToString();
                    txtMargemVenda.Text = String.Format("{0:N}", porcentagem);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            
            Salvar();
        }


        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            try
            {

            frmVisaoSelecionaProduto frm = new frmVisaoSelecionaProduto();
            frm.ShowDialog();
            txtCodigoProduto.Text = frm.CODIGO;
            txtDescricaoProduto.Text = frm.DESCRICAO;
            //txtPrecoVenda.Text = frm.PRECOVENDA;
            //txtPrecoEntrada.Text = frm.PRECOENTRADA;
            //double Venda = Convert.ToDouble(txtPrecoVenda.Text);
            //txtPrecoVenda.Text = String.Format("{0:N}", Venda);
            //double Entrada = Convert.ToDouble(txtPrecoEntrada.Text);
            //txtPrecoEntrada.Text = String.Format("{0:N}", Entrada);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            frmVisaoSelecionaFornecedor frm = new frmVisaoSelecionaFornecedor();
            frm.ShowDialog();
            txtCodigoFornecedor.Text = frm.CODIGO;
            txtNomeFornecedor.Text = frm.NOME;
            txtNomeFantasia.Text = frm.NOMEFANTASIA;


        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
            Salvar();
            this.Close();
        }

        private void txtValorTotal_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtQuantidadeEntrada.Text) && !String.IsNullOrWhiteSpace(txtValorTotal.Text))
                {
                    
                    double valorTotal = Convert.ToDouble(txtValorTotal.Text);
                    double quantidade = Convert.ToDouble(txtQuantidadeEntrada.Text);
                    double valorUnitario = valorTotal / quantidade;
                    txtValorTotal.Text = String.Format("{0:N}", valorTotal);
                    txtValorEntrada.Text = String.Format("{0:N}", valorUnitario);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtValorEntrada_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtQuantidadeEntrada.Text) && !String.IsNullOrWhiteSpace(txtValorEntrada.Text))
                {
                    double precovenda = Convert.ToDouble(txtPrecoVenda.Text);
                    double valorUnitario = Convert.ToDouble(txtValorEntrada.Text);
                    double quantidade = Convert.ToDouble(txtQuantidadeEntrada.Text);
                    double valorTotal = valorUnitario * quantidade;
                    double porcentagem = ((precovenda * 100) / valorUnitario) - 100;
                    txtMargemVenda.Text = porcentagem.ToString();
                    txtValorEntrada.Text = String.Format("{0:N}", Convert.ToDouble(txtValorEntrada.Text));
                    txtValorTotal.Text = String.Format("{0:N}", valorTotal);
                    txtMargemVenda.Text = String.Format("{0:N}", porcentagem);
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtQuantidadeEntrada_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(txtQuantidadeEntrada.Text) && !String.IsNullOrWhiteSpace(txtValorEntrada.Text))
                {
                    double valorUnitario = Convert.ToDouble(txtValorEntrada.Text);
                    double quantidade = Convert.ToDouble(txtQuantidadeEntrada.Text);
                    double valorTotal = valorUnitario * quantidade;

                    txtValorTotal.Text = valorTotal.ToString();
                }else if (!String.IsNullOrWhiteSpace(txtQuantidadeEntrada.Text) && !String.IsNullOrWhiteSpace(txtValorTotal.Text))
                {
                    double valorTotal = Convert.ToDouble(txtValorTotal.Text);
                    double quantidade = Convert.ToDouble(txtQuantidadeEntrada.Text);
                    double valorUnitario = valorTotal / quantidade;
                    txtValorEntrada.Text = valorUnitario.ToString();
                }
             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

 
            

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txtCodigoProduto.Text) && String.IsNullOrWhiteSpace(txtQuantidadeEntrada.Text))
            {
                this.Close();
            }
            else
            {
                Cadastro();
                this.Close();
            }

        }

        private void btnSalvar2_Click(object sender, EventArgs e)
        {
            
            if (String.IsNullOrWhiteSpace(txtCodigoProduto.Text) && String.IsNullOrWhiteSpace(txtDescricaoProduto.Text))
            {
                MessageBox.Show("Selecione um produto");

            }
            else
            {
                dataGridView1.Rows.Add(txtCodigoProduto.Text, txtDescricaoProduto.Text, txtNomeFantasia.Text, txtValorEntrada.Text, txtPrecoVenda.Text, txtMargemVenda.Text);
                Cadastro();
                LimpaCampos();
                btnOk2.Visible = true;
            }

        }

       
    }
}
