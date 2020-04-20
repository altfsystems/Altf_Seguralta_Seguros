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
    public partial class frmPagamentoEntrada : Form
    {
        public string Entrada { get; set; }
        public string Total { get; set; }
        public string IDCLIENTE { get; set; }
        public string TipoPagamento { get; set; }
        Boolean Editar;

        public frmPagamentoEntrada(string Entrada , string Total , string IDCLIENTE , string TipoPagamento , bool Editar_)
        {
            InitializeComponent();
            txtCodigoVenda.Text = Entrada;
            txtValorTotal.Text = Total;
            Editar = Editar_;
            double Valor = Convert.ToDouble(txtValorTotal.Text.Replace(".", ","));
            txtValorTotal.Text = String.Format("{0:N}", Valor);
        }

        private void Recebimento()
        {
            try
            {
                ValorPagamento();

                double ValorTotalRestante = Convert.ToDouble(txtValorTotal.Text.Replace(".", ",")) - (Convert.ToDouble(txtValorCheque.Text.Replace(".", ",")) + Convert.ToDouble(txtValorCredito.Text.Replace(".", ",")) + Convert.ToDouble(txtValorDebito.Text.Replace(".", ",")) + Convert.ToDouble(txtValorDinheiro.Text.Replace(".", ",")));
                txtValorRestante.Text = ValorTotalRestante.ToString();

                double ValorTotal = Convert.ToDouble(txtValorTotal.Text.Replace(".", ","));
                txtValorTotal.Text = ValorTotal.ToString();

                string sql = String.Format(@"insert into RECEBIMENTO(IDVENDA, IDFCFO , VALORDINHEIRO , VALORCHEQUE , VALORCARTAOCREDITO , VALORCARTAODEBITO) VALUES ({0}, {1} , {2} , {3} , {4} , {5})", Entrada, IDCLIENTE, txtValorDinheiro.Text.Replace(",", "."), txtValorCheque.Text.Replace(",", "."), txtValorCredito.Text.Replace(",", "."), txtValorDebito.Text.Replace(",", "."));
                MetodosSql.ExecQuery(sql);

                sql = String.Format(@"select PARCELAS from TIPOPAGAMENTO where IDTIPOPAGAMENTO = {0}", TipoPagamento);
                int NParcelas = int.Parse(MetodosSql.GetField(sql, "PARCELAS"));


                for (int parcela = 1; parcela <= NParcelas; parcela++)
                {
                    sql = String.Format(@"insert into PARCELA values ({0},{1},{2},{3}/{4},'A')", parcela, Entrada, IDCLIENTE, ValorTotalRestante, NParcelas);
                    MetodosSql.ExecQuery(sql);
                }

                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ValorPagamento()
        {
            if(String.IsNullOrWhiteSpace(txtValorDinheiro.Text))
            {
                txtValorDinheiro.Text = "0.00";
            }

            if(String.IsNullOrWhiteSpace(txtValorCheque.Text))
            {
                txtValorCheque.Text = "0.00";
            }

            if(String.IsNullOrWhiteSpace(txtValorCredito.Text))
            {
                txtValorCredito.Text = "0.00";
            }

            if(String.IsNullOrWhiteSpace(txtValorDebito.Text))
            {
                txtValorDebito.Text = "0.00";
            }

            
        }

        private void btnReceber_Click(object sender, EventArgs e)
        {
            Recebimento();
        }

        private void txtValorCheque_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtValorCheque.Text))
            {
            }
            else
            {
                double Valor = Convert.ToDouble(txtValorCheque.Text.Replace(".", ","));
                txtValorCheque.Text = String.Format("{0:N}", Valor);
            }
        }

        private void txtValorCredito_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtValorCredito.Text))
            {
            }
            else
            {
                double Valor = Convert.ToDouble(txtValorCredito.Text.Replace(".", ","));
                txtValorCredito.Text = String.Format("{0:N}", Valor);
            }
        }

        private void txtValorDebito_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtValorDebito.Text))
            {
            }
            else
            {
                double Valor = Convert.ToDouble(txtValorDebito.Text.Replace(".", ","));
                txtValorDebito.Text = String.Format("{0:N}", Valor);
            }
        }

        private void txtValorDinheiro_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtValorDinheiro.Text))
            {
            }
            else
            {
                double Valor = Convert.ToDouble(txtValorDinheiro.Text.Replace(".", ","));
                txtValorDinheiro.Text = String.Format("{0:N}", Valor);
            }
        }

     
    }
                    
}
