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
    public partial class frmPopUpSelecionaTipoPagamento : Form
    {
        bool Editar;
        string CODVENDA;
        string Cod;
        public string VALORTOTAL { get; set; }
        string VALIDAR;

        public frmPopUpSelecionaTipoPagamento(string Valor, Boolean _Editar, string Cod_, string valortotal_)
        {
            InitializeComponent();
            VALORTOTAL = valortotal_;
            CODVENDA = Valor;
            Editar = _Editar;
            Cod = Cod_;
            txtTotalVenda.Text = VALORTOTAL;

        }

        private void InsertParcela()
        {
            if (Editar)
            {
                string sql = String.Format(@"select PARCELAS from TIPOPAGAMENTO where IDTIPOPAGAMENTO = {0}", txtCodigoPagamento.Text);
                int NParcelas = int.Parse(MetodosSql.GetField(sql, "PARCELAS"));

                sql = String.Format(@"select sum(ITEMMOVIMENTO.VALOR * ITEMMOVIMENTO.QUANTIDADE) - VENDA.DESCONTO as 'TOTAL' from ITEMMOVIMENTO
                                    INNER JOIN VENDA ON ITEMMOVIMENTO.IDVENDA = VENDA.IDVENDA
                                    Where VENDA.IDVENDA = {0}
                                    GROUP BY VENDA.DESCONTO", CODVENDA);
                string totalVenda = MetodosSql.GetField(sql, "TOTAL");

                sql = String.Format(@"select IDFCFO from VENDA where IDVENDA = '{0}'", CODVENDA);
                string idCliente = MetodosSql.GetField(sql, "IDFCFO");

                for (int parcela = 1; parcela <= NParcelas; parcela++)
                {
                    sql = String.Format(@"insert into PARCELA values ({0},{1},{2},{3}/{4},null)", parcela, CODVENDA, idCliente, totalVenda.Replace(".", "").Replace(",", "."), NParcelas);
                    MetodosSql.ExecQuery(sql);
                }

                Editar = true;
            }
        }

        private void Cadastro()
        {

            try
            {
                if (String.IsNullOrWhiteSpace(txtDesconto.Text))
                {
                    txtDesconto.Text = "0";
                }
                string SQL = String.Format(@"UPDATE VENDA SET DESCONTO = {2} , TIPOPAGAMENTO = {0} , STATUS = 'A' , DATAINCLUSAO = GETDATE() WHERE IDVENDA = {1}", txtCodigoPagamento.Text, CODVENDA, txtDesconto.Text.Replace("," , "."));
                MetodosSql.ExecQuery(SQL);

                string sql = String.Format(@"select PARCELAS from TIPOPAGAMENTO where IDTIPOPAGAMENTO = {0}", txtCodigoPagamento.Text);
                int NParcelas = int.Parse(MetodosSql.GetField(sql, "PARCELAS"));

                sql = String.Format(@"select sum(ITEMMOVIMENTO.VALOR * ITEMMOVIMENTO.QUANTIDADE) - VENDA.DESCONTO as 'TOTAL' from ITEMMOVIMENTO
                                    INNER JOIN VENDA ON ITEMMOVIMENTO.IDVENDA = VENDA.IDVENDA
                                    Where VENDA.IDVENDA = {0}
                                    GROUP BY VENDA.DESCONTO", CODVENDA);
                string totalVenda = MetodosSql.GetField(sql, "TOTAL");

                sql = String.Format(@"select IDFCFO from VENDA where IDVENDA = '{0}'", CODVENDA);
                string idCliente = MetodosSql.GetField(sql, "IDFCFO");





                for (int parcela = 1; parcela <= NParcelas; parcela++)
                {
                    sql = String.Format(@"insert into PARCELA values ({0},{1},{2},{3}/{4},null) Select SCOPE_IDENTITY()", parcela, CODVENDA, idCliente, totalVenda.Replace(".", "").Replace(",", "."), NParcelas);
                    object CodParcela = MetodosSql.ExecScalar(sql);
                    Cod = CodParcela.ToString();
                }

                frmIdPagamento frm = new frmIdPagamento(false, null);
                frm.CODIGOCLIENTE = idCliente.ToString();
                frm.CODIGOPARCELA = Cod.ToString();
                frm.CODIGOVENDA = CODVENDA.ToString();
                frm.ShowDialog();

                Editar = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            frmSelecionaTipoPagamento frm = new frmSelecionaTipoPagamento();
            frm.ShowDialog();
            txtCodigoPagamento.Text = frm.TIPOPAGAMENTO;
            txtDescricaoPagamento.Text = frm.DESCPAGAMENTO;





        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtCodigoPagamento.Text))
            {
                MessageBox.Show("Selecione um tipo de pagamento", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else
            {
                VALIDAR = "0";
                btnSalvar2.Visible = true;
                Cadastro();

            }

        }



        private void btnOk_Click(object sender, EventArgs e)
        {
            if (VALIDAR == "0")
            {
                this.Close();
            }
            else
            {
                if (String.IsNullOrWhiteSpace(txtCodigoPagamento.Text))
                {
                    MessageBox.Show("Selecione um tipo de pagamento", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Cadastro();
                    this.Close();

                }
            }



        }

        private void btnSalvar2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Clique em 'OK' Para Continuar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
