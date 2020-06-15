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
    public partial class frmIdPagamento : Form
    {

        Boolean Editar, EntradaVenda;
        string Cod;
        public string CODIGOPARCELA { get; set; }
        public string CODIGOVENDA { get; set; }
        public string RESTANTE { get; set; }
        public string CODIGOCLIENTE { get; set; }
        string sql = String.Empty;
        string STATUS = String.Empty;


        public frmIdPagamento(bool Editar_, string Cod_, bool entradaVenda)
        {
            InitializeComponent();
            Cod = Cod_;
            Editar = Editar_;
            txtCodigo.Visible = false;
            EntradaVenda = entradaVenda;
            txtDataPagamento.Text = DateTime.Now.ToString();
            if(EntradaVenda)
            {
                txtDataPagamento.Visible = false;
                lblDataPagamento.Visible = false;
            }

        }



        private void Insert()
        {
            txtCodigo.Text = Cod;

            string SQL = String.Format(@"insert into RECEBIMENTO(IDVENDA , IDFCFO , IDPARCELA , VALORDINHEIRO , VALORCHEQUE, VALORCARTAOCREDITO , VALORCARTAODEBITO , OBSERVACAO, ESTORNO )
                                                                       values ('{0}' , '{6}' , '{1}' , '{2}' , '{3}' , '{4}' , '{5}', NULL, 0 )select SCOPE_IDENTITY()", txtCodigoVenda.Text, txtCodigoParcela.Text, txtValorDinheiro.Text.Replace(".", "").Replace(",", "."), txtValorCheque.Text.Replace(".", "").Replace(",", "."),
                                                               txtValorCredito.Text.Replace(".", "").Replace(",", "."), txtValorDebito.Text.Replace(".", "").Replace(",", "."), CODIGOCLIENTE);


            Clipboard.SetText(SQL);

            double Resultado = Convert.ToDouble(txtValorCheque.Text) + Convert.ToDouble(txtValorCredito.Text) + Convert.ToDouble(txtValorDebito.Text) + Convert.ToDouble(txtValorDinheiro.Text);
            if (Resultado < 0)
            {
                throw new Exception("O Valor Excede a Parcela");
            }


            object IDRECEBIMENTO = MetodosSql.ExecScalar(SQL);
        }

        private void Validar()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(txtValorDinheiro.Text))
                {
                    txtValorDinheiro.Text = "0";
                }

                if (String.IsNullOrWhiteSpace(txtValorCheque.Text))
                {
                    txtValorCheque.Text = "0";
                }

                if (String.IsNullOrWhiteSpace(txtValorDebito.Text))
                {
                    txtValorDebito.Text = "0";
                }

                if (String.IsNullOrWhiteSpace(txtValorCredito.Text))
                {
                    txtValorCredito.Text = "0";
                }

                string sql = String.Format(@"select cast(isnull(P.TOTAL,0) - isnull(R.PAGO,0)as numeric (20,2)) as 'DEVENDO'
													
                                      from  (select IDPARCELA, cast(sum(isnull(VALOR,0))as numeric(20,2) ) as 'TOTAL' from PARCELA group by IDPARCELA) P

                                    left join (select IDPARCELA, cast(sum(isnull(VALORDINHEIRO,0) + 
		                                    isnull(VALORCHEQUE,0) + 
		                                    isnull(VALORCARTAODEBITO,0) + 
		                                    isnull(VALORCARTAOCREDITO,0))as numeric (20,2)) as 'PAGO' from RECEBIMENTO WHERE ESTORNO != 1
		                                    group by IDPARCELA) R
                                    on R.IDPARCELA = P.IDPARCELA

                                    where P.IDPARCELA = '{0}' ", txtCodigoParcela.Text);

                double Restante = Convert.ToDouble(MetodosSql.GetField(sql, "DEVENDO"));
                double Resultado = Convert.ToDouble(txtValorCheque.Text.Replace(".", ",")) + Convert.ToDouble(txtValorCredito.Text.Replace(".", ",")) + Convert.ToDouble(txtValorDebito.Text.Replace(".", ",")) + Convert.ToDouble(txtValorDinheiro.Text.Replace(".", ","));
                double ValorRestante = Convert.ToDouble(Restante) - Convert.ToDouble(Resultado);


                if (ValorRestante == 0)
                {
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmIdPagamento_Load(object sender, EventArgs e)
        {
            if(EntradaVenda)
            {
                txtValorRestante.Text = RESTANTE;
                txtCodigoVenda.Text = CODIGOVENDA;
                txtCodigoParcela.Text = CODIGOPARCELA;
                sql = String.Format(@"select cast(VALOR as numeric(20,2)) as 'VALOR' from PARCELA where IDPARCELA = '{0}'", CODIGOPARCELA);
                txtValorParcela.Text = MetodosSql.GetField(sql, "VALOR");
                txtValorRestante.Text = txtValorParcela.Text;
            }
            else
            {
                txtValorRestante.Text = RESTANTE;
                txtCodigoVenda.Text = CODIGOVENDA;
                txtCodigoParcela.Text = CODIGOPARCELA;
                sql = String.Format(@"select cast(VALOR as numeric(20,2)) as 'VALOR' from PARCELA where IDPARCELA = '{0}'", CODIGOPARCELA);
                txtValorParcela.Text = MetodosSql.GetField(sql, "VALOR");
            }
                
        }
                
        private Boolean ChecaValor()
        {
            Validar();

            sql = String.Format(@"select cast(isnull(P.TOTAL,0) - isnull(R.PAGO,0)as numeric (20,2)) as 'DEVENDO'
                                from  (select IDPARCELA, cast(sum(isnull(VALOR,0))as numeric(20,2) ) as 'TOTAL' from PARCELA group by IDPARCELA) P
                                left join (select IDPARCELA, cast(sum(isnull(VALORDINHEIRO,0) + 
		                        isnull(VALORCHEQUE,0) + 
                                isnull(VALORCARTAODEBITO,0) + 
		                        isnull(VALORCARTAOCREDITO,0))as numeric (20,2)) as 'PAGO' from RECEBIMENTO 
		                        group by IDPARCELA) R
                                on R.IDPARCELA = P.IDPARCELA
                                where P.IDPARCELA = '{0}'", txtCodigoParcela.Text);

													

            string RestanteString = txtValorRestante.Text;
            double Restante = double.Parse(RestanteString);
            double Resultado = Convert.ToDouble(txtValorCheque.Text.Replace(".", ",")) + Convert.ToDouble(txtValorCredito.Text.Replace(".", ",")) + Convert.ToDouble(txtValorDebito.Text.Replace(".", ",")) + Convert.ToDouble(txtValorDinheiro.Text.Replace(".", ","));

            if (Resultado > Restante)
            {
                throw new Exception("O Valor Excede a Parcela");
            }
            else
            {
                if (Restante == Resultado)
                {
                    STATUS = "P";
                }
                else
                {
                    STATUS = "A";
                }

                return true;
            }
        }
                
        private void CadastroRecebimento()
        {
            try
            {
                
                string SQL = String.Format(@"update PARCELA set STATUS = '{0}' where IDPARCELA = {1} ", STATUS, txtCodigoParcela.Text);
                MetodosSql.ExecQuery(SQL);

                if (Editar)
                {


                    SQL = String.Format(@"update RECEBIMENTO Set VALORDINHEIRO = '{0}' , 
                                                                        VALORCHEQUE = '{1}' , 
                                                                        VALORCARTAOCREDITO = '{2}' , 
                                                                        VALORCARTAODEBITO = '{3}'
                                                                         where IDPARCELA = '{4}' ", txtValorDinheiro.Text, txtValorCheque.Text, txtValorCredito.Text, txtValorDebito.Text, txtCodigoParcela.Text);
                    MetodosSql.ExecQuery(SQL);

                }
                else
                {


                    txtCodigo.Text = Cod;

                    SQL = String.Format(@"insert into RECEBIMENTO(IDVENDA , IDFCFO , IDPARCELA , VALORDINHEIRO , VALORCHEQUE, VALORCARTAOCREDITO , VALORCARTAODEBITO , OBSERVACAO, ESTORNO )
                                                                       values ('{0}' , '{6}' , '{1}' , '{2}' , '{3}' , '{4}' , '{5}', NULL, 0 )select SCOPE_IDENTITY()", txtCodigoVenda.Text, txtCodigoParcela.Text, txtValorDinheiro.Text, txtValorCheque.Text,
                                                                       txtValorCredito.Text, txtValorDebito.Text, CODIGOCLIENTE);


                    object IDRECEBIMENTO = MetodosSql.ExecScalar(SQL);

                    double ValorPagamento = Convert.ToDouble(txtValorCheque.Text.Replace(".", ",")) + Convert.ToDouble(txtValorCredito.Text.Replace(".", ",")) + Convert.ToDouble(txtValorDebito.Text.Replace(".", ",")) + Convert.ToDouble(txtValorDinheiro.Text.Replace(".", ","));

                    if (ValorPagamento.ToString() != "0")
                    {
                        string sql = String.Format(@"UPDATE PARCELA set DATAPAGAMENTO = getdate() where IDPARCELA = {0}", CODIGOPARCELA);
                        MetodosSql.ExecQuery(sql);

                    }






                }

                Editar = true;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReceber_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChecaValor())
                {
                    CadastroRecebimento();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
                
        private String CampoLeave(TextBox txt)
        {
            if (String.IsNullOrWhiteSpace(txt.Text))
            {
                return String.Format("{0:N}", 0).Replace(",", ".");
            }
            else
            {
                double valor = Convert.ToDouble(txt.Text.Replace(".", ","));
                return String.Format("{0:N}", valor).Replace(".", "").Replace(",", ".");
            }
        }

        private void txtValorCheque_Leave(object sender, EventArgs e)
        {
            txtValorCheque.Text = CampoLeave(txtValorCheque);
        }
          
        private void txtValorCredito_Leave(object sender, EventArgs e)
        {
            txtValorCredito.Text = CampoLeave(txtValorCredito);
        }

        private void txtValorDebito_Leave(object sender, EventArgs e)
        {
            txtValorDebito.Text = CampoLeave(txtValorDebito);
        }

        private void txtValorDinheiro_Leave(object sender, EventArgs e)
        {
            txtValorDinheiro.Text = CampoLeave(txtValorDinheiro);
        }
    }
}





























