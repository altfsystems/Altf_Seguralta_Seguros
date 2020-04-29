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
    public partial class frmEdiçãoParcela : Form
    {
        Boolean Editar;
        string CodVenda, CodParcela, Restante, ValorParcela;

        private void btnReceber_Click(object sender, EventArgs e)
        {
            if(Editar)
            {
                string sql = String.Format(@"UPDATE RECEBIMENTO SET VALORDINHEIRO = '{0}', VALORCHEQUE = '{1}', VALORCARTAODEBITO = '{2}', VALORCARTAOCREDITO = '{3}' WHERE IDPARCELA = '{4}'",
                  /*{0}*/  txtValorDinheiro.Text.Replace(".", "").Replace(",", "."),
                  /*{1}*/  txtValorCheque.Text.Replace(".", "").Replace(",", "."),
                  /*{2}*/  txtValorDebito.Text.Replace(".", "").Replace(",", "."),
                  /*{3}*/  txtValorCredito.Text.Replace(".", "").Replace(",", "."),
                  /*{4}*/  CodParcela);

                MetodosSql.ExecQuery(sql);
                this.Close();
            }
        }

        public frmEdiçãoParcela(bool editar, string codVenda, string codParcela, string restante, string valorParcela)
        {
            InitializeComponent();
            Editar = editar;
            CodVenda = codVenda;
            CodParcela = codParcela;
            Restante = restante;
            ValorParcela = valorParcela;
        }

        private void frmEdiçãoParcela_Load(object sender, EventArgs e)
        {
            
            if(Editar)
            {
                string dinheiro, cheque, debito, credito;
                string sql = String.Format(@"SELECT cast(VALORDINHEIRO as numeric(20,2)) AS VALORDINHEIRO, CAST(VALORCHEQUE AS NUMERIC(20,2)) AS VALORCHEQUE, CAST(VALORCARTAODEBITO AS NUMERIC(20,2)) AS VALORCARTAODEBITO,
                                            CAST(VALORCARTAOCREDITO AS NUMERIC(20,2)) AS VALORCARTAOCREDITO FROM RECEBIMENTO WHERE IDPARCELA = '{0}'", CodParcela);
                dinheiro = MetodosSql.GetField(sql, "VALORDINHEIRO");
                cheque = MetodosSql.GetField(sql, "VALORCHEQUE");
                debito = MetodosSql.GetField(sql, "VALORCARTAODEBITO");
                credito = MetodosSql.GetField(sql, "VALORCARTAOCREDITO");

                if(dinheiro != "0,00")
                {
                    txtValorDinheiro.Text = dinheiro;
                    txtValorCheque.Text = "0,00";
                    txtValorDebito.Text = "0,00";
                    txtValorCredito.Text = "0,00";
                }
                else if(cheque != "0,00")
                {
                    txtValorDinheiro.Text = "0,00";
                    txtValorCheque.Text = cheque;
                    txtValorDebito.Text = "0,00";
                    txtValorCredito.Text = "0,00";
                }
                else if(debito != "0,00")
                {
                    txtValorDinheiro.Text = "0,00";
                    txtValorCheque.Text = "0,00";
                    txtValorDebito.Text = debito;
                    txtValorCredito.Text = "0,00";
                }
                else if(credito != "0,00")
                {
                    txtValorDinheiro.Text = "0,00";
                    txtValorCheque.Text = "0,00";
                    txtValorDebito.Text = "0,00";
                    txtValorCredito.Text = credito;
                }
                else
                {
                    txtValorDinheiro.Text = "0,00";
                    txtValorCheque.Text = "0,00";
                    txtValorDebito.Text = "0,00";
                    txtValorCredito.Text = "0,00";
                }
                txtCodigoVenda.Text = CodVenda;
                txtCodigoParcela.Text = CodParcela;
                txtValorParcela.Text = ValorParcela;
                txtValorRestante.Text = Restante;
            }
        }
    }
}
