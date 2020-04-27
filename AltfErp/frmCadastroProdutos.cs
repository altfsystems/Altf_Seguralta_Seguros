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
using System.CodeDom;

namespace AltfErp
{
    public partial class frmCadastroProdutos : Form
    {
        Boolean Editar;
        string Cod, codCliente;
       

        public frmCadastroProdutos(bool Editar_, string Cod_)
        {
            InitializeComponent();
            txtDataInclusao.Text = DateTime.Now.ToString();
            Editar = Editar_;
            Cod = Cod_;
            
           
            
        }

        private void ValidarReplace()
        {
            //double PrecoEntrada = Convert.ToDouble(txtPrecoEntrada.Text);
            //txtPrecoEntrada.Text = String.Format("{0:N}", PrecoEntrada);
        }

        private void Validar()
        {
            if(String.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                throw new Exception("Digite um Nome ou Descrição");
            }

           

            //if(String.IsNullOrWhiteSpace(txtPrecoVenda.Text))
            //{
            //    throw new Exception("Digite um Preço de Venda");
            //}

         

        
        }

        private void LimpaCampos()
        {
            txtCodigo.Clear();
            txtDescricao.Clear();
            txtObservacao.Clear();
            txtDataInclusao.Clear();
            //txtPrecoEntrada.Clear();
            //txtPrecoVenda.Clear();
            //txtMargemVenda.Clear();

        }

        private void Cadastro()
        {
            try
            {
                if(Editar)
                {
                    string SQL = String.Format(@"update PRODUTO
	                                             set DESCRICAO = ('{0}'),
                                                     TIPOSEGURO = '{1}',
                                                     CIASEGURADORA = '{2}',
                                                     NUMEROQUIVER = '{3}',
                                                     OBSERVACAO = '{4}'
                                                     where IDPRODUTO = '{5}'",


                   /*{0}*/  txtDescricao.Text,
                   /*{1}*/ txtTipoSeguro.Text,
                   /*{2}*/ txtCiaSeguradora.Text,
                   /*{3}*/ txtNumeroQuiver.Text,
                   /*{4}*/ txtObservacao.Text,
                   /*{5}*/ Cod);
                    MetodosSql.ExecQuery(SQL);






                    //string sql = String.Format(@"update ESTOQUE set QUANTIDADE = {0} WHERE IDPRODUTO = {1}", txtQuantidadeEmEstoque.Text, txtCodigo.Text);
                    //MetodosSql.ExecQuery(sql);

                }
                else
                {
                    
                    
                    //if (String.IsNullOrWhiteSpace(txtQuantidadeEmEstoque.Text))
                    //{
                    //    txtQuantidadeEmEstoque.Text = "0";
                    //}

                    ValidarReplace();

                    string SQL = String.Format(@"insert into PRODUTO (DESCRICAO , TIPOSEGURO, CIASEGURADORA, NUMEROQUIVER , OBSERVACAO , UNIDADECONTROLE , DATAINCLUSAO) 
                                                    values('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', getdate()) select SCOPE_IDENTITY()",
                   /*{0}*/  txtDescricao.Text,
                   /*{1}*/ txtTipoSeguro.Text,
                   /*{2}*/ txtCiaSeguradora.Text,
                   /*{3}*/ txtNumeroQuiver.Text,
                   /*{4}*/ txtObservacao.Text,
                   /*{5}*/ null);


                    object IDPRODUTO = MetodosSql.ExecScalar(SQL);
                    txtCodigo.Text = IDPRODUTO.ToString();


                    //SQL = String.Format(@"insert into estoque (IDPRODUTO , QUANTIDADE) values ('{0}','{1}')", IDPRODUTO.ToString(), "0");
                    //MetodosSql.ExecQuery(SQL);

                    //string sql = String.Format(@"update ESTOQUE set QUANTIDADE = {0} WHERE IDPRODUTO = {1}", txtQuantidadeEmEstoque.Text, IDPRODUTO);
                    //MetodosSql.ExecQuery(sql);

                    Editar = true;
                    
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {


            if (String.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("Digite um Nome ou Descrição", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                
                Cadastro();
            }

        }

        

        private void frmCadastroProdutos_Load(object sender, EventArgs e)
        {
            try
            {
              
              if(String.IsNullOrWhiteSpace(txtCodigo.Text))
              {

                    if(Editar)
                    {
                        txtCodigo.Text = Cod;

                        string sql = String.Format(@"select * from PRODUTO where IDPRODUTO = {0}", Cod);
                        txtDescricao.Text = MetodosSql.GetField(sql, "DESCRICAO");
                        //txtPrecoEntrada.Text = MetodosSql.GetField(sql, "PRECOUNENTRADA");
                        //txtPrecoEntrada.Text = MetodosSql.GetField(String.Format(@"select CAST(PRECOUNENTRADA as numeric(20,2))as 'PE' from PRODUTO where IDPRODUTO = {0}", Cod), "PE");
                        //txtMargemVenda.Text = MetodosSql.GetField(sql, "MARGEMVENDA");
                        //txtMargemVenda.Text = MetodosSql.GetField(String.Format(@"select CAST(MARGEMVENDA as numeric(20,2))as 'MA' from PRODUTO where IDPRODUTO = {0}", Cod), "MA");
                        txtObservacao.Text = MetodosSql.GetField(sql, "OBSERVACAO");
                        txtDataInclusao.Text = MetodosSql.GetField(String.Format(@"select CONVERT(varchar, CONVERT(DATETIME, DATAINCLUSAO, 121), 103) as 'Nasc' from PRODUTO where IDPRODUTO = {0}", Cod), "Nasc");
                        //txtPrecoVenda.Text = MetodosSql.GetField(sql, "PRECOUNVENDA");
                        //txtPrecoVenda.Text = MetodosSql.GetField(String.Format(@"select CAST(PRECOUNVENDA as numeric(20,2))as 'PV' from PRODUTO where IDPRODUTO = {0}", Cod), "PV");
                        txtTipoSeguro.Text = MetodosSql.GetField(sql, "TIPOSEGURO");
                        txtCiaSeguradora.Text = MetodosSql.GetField(sql, "CIASEGURADORA");
                        txtNumeroQuiver.Text = MetodosSql.GetField(sql, "NUMEROQUIVER");
                        
                       

                    }
              }
              else
              {
                    LimpaCampos();
              }

                 
           


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDescricao.Text))
            {
                MessageBox.Show("Digite um Nome ou Descrição", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Cadastro();
                this.Close();
            }
            
        }



        //private void txtPrecoVenda_Leave(object sender, EventArgs e)
        //{

        //    if(String.IsNullOrWhiteSpace(txtPrecoEntrada.Text))
        //    {
        //        txtPrecoEntrada.Text = "0,00";
        //        txtMargemVenda.Text = "0";
        //    }
            
            
        //        if (!String.IsNullOrWhiteSpace(txtPrecoVenda.Text) && !String.IsNullOrWhiteSpace(txtPrecoEntrada.Text))
        //        {
        //            double precoEntrada = Convert.ToDouble(txtPrecoEntrada.Text.Replace(".", ","));
        //            double precoVenda = Convert.ToDouble(txtPrecoVenda.Text.Replace(".", "").Replace(".", ","));
        //            txtPrecoEntrada.Text = String.Format("{0:N}", precoEntrada).Replace(".", "").Replace(".", ",");
        //            txtPrecoVenda.Text = String.Format("{0:N}", precoVenda).Replace(".", "").Replace("." , ",");
        //        }

        //    try
        //    {
        //        if (txtPrecoEntrada.Text == "0,00")
        //        {
        //            txtMargemVenda.Text = "0";
        //        }
        //        else
        //        {
        //            if (!String.IsNullOrWhiteSpace(txtPrecoVenda.Text) && !String.IsNullOrWhiteSpace(txtPrecoEntrada.Text))
        //            {
        //                double precoEntrada = Convert.ToDouble(txtPrecoEntrada.Text.Replace(".", ","));
        //                double precoVenda = Convert.ToDouble(txtPrecoVenda.Text.Replace(".", ","));
        //                double porcentagem = ((precoVenda * 100) / precoEntrada) - 100;
        //                txtMargemVenda.Text = String.Format("{0:N}", porcentagem);
                    

        //            }

        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }


        //}

      

        private void txtPrecoEntrada_KeyPress(object sender, KeyPressEventArgs e)
            {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void txtPrecoVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void txtMargemVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
            {
                e.Handled = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

        

        //private void txtPrecoEntrada_Leave(object sender, EventArgs e)
        //{
        //    if (String.IsNullOrWhiteSpace(txtPrecoEntrada.Text))
        //    {
        //        txtPrecoEntrada.Text = "0,00";
        //    }
        //}

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmVisaoSelecionaCiaSeuradora frm = new frmVisaoSelecionaCiaSeuradora();
            frm.ShowDialog();
            if(!String.IsNullOrWhiteSpace(frm.Codigo))
            {
                codCliente = frm.Codigo;
                txtCiaSeguradora.Text = MetodosSql.GetField(String.Format("SELECT NOMEFANTASIA FROM FCFOSEGURADORA WHERE IDSEGURADORA = {0}", codCliente), "NOMEFANTASIA");
            }
            
        }
    }

}
                    
                    





     

        



            
            
           
            


            
    
