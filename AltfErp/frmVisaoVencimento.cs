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
    public partial class frmVisaoVencimento : Form
    {
        string Inicio, Fim, Todos, Vencidos;
        public frmVisaoVencimento()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            grdVisaoDataVencimento.EmbeddedNavigator.Buttons.Append.Visible = false;
            grdVisaoDataVencimento.EmbeddedNavigator.Buttons.Remove.Visible = false;
            Filtro();
            if (Inicio != "" && Fim != "")
            {
                AtualizaGrid();
            }
         



        }

        private void Filtro()
        {
            frmFiltroDataVencimento frm = new frmFiltroDataVencimento();
            frm.ShowDialog();
            Inicio = frm.DataInicio;
            Fim = frm.DataFim;
            Todos = frm.todos;
            Vencidos = frm.vencidos;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                var obj = gridView1.GetRowCellValue(rowHandle, "IDVENDA");

                if(obj != null)
                {
                    string sql = String.Format(@"SELECT COCORRETAGEM FROM VENDACOMISSAO WHERE IDVENDA = '{0}'", obj.ToString());
                    string coCorretagem = MetodosSql.GetField(sql, "COCORRETAGEM");
                    frmCadastroVenda frm = new frmCadastroVenda(true, obj.ToString(), null);
                    frm.btnAdicionar.Enabled = false;
                    frm.btnExcluir.Enabled = false;
                    frm.btnOk.Enabled = false;
                    frm.btnSalvar.Enabled = false;
                    frm.btnSelecionaProduto.Enabled = false;
                    frm.simpleButton2.Enabled = false;
                    frm.btnTipoPagamento.Enabled = false;
                    frm.txtObservacao.Enabled = false;
                    frm.txtValorTotal.Enabled = false;
                    frm.txtIof.Enabled = false;
                    frm.txtTotalVenda.Enabled = false;
                    frm.txtQuantidade.Enabled = false;
                    frm.txtValorLiquido.Enabled = false;
                    frm.txtIof.Enabled = false;
                    frm.txtComissao.Enabled = false;
                    if(coCorretagem == "S")
                    {
                        frm.cbCoCorretagem.Checked = true;
                    }
                    frm.cbCoCorretagem.Enabled = false;
                    frm.txtDesconto.Enabled = false;
                    frm.txtDataVencimento.Enabled = false;
                    frm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void AtualizaGrid()
        {
            string sql = String.Format(@"SELECT VENDA.IDVENDA, FCFO.NOME, FCFO.NOMEFANTASIA AS SOBRENOME, VENDEDORES.NOME AS VENDEDOR, VENDEDORES.SOBRENOME,
                                        VENDA.TIPOPAGAMENTO, VENDA.DESCONTO, VENDA.STATUS, CONVERT(VARCHAR, VENDA.DATAVENCIMENTO, 103) AS VENCIMENTO,
                                        CONVERT(VARCHAR, VENDA.DATAINCLUSAO,103) AS DATAVENDA  FROM VENDA 
                                        INNER JOIN FCFO
                                        ON FCFO.IDFCFO = VENDA.IDFCFO
                                        INNER JOIN VENDEDORES
                                        ON VENDEDORES.IDVENDEDOR = VENDA.IDVENDEDOR
                                        WHERE {0} {1} DATAVENCIMENTO >=CONVERT(varchar, CONVERT(DATETIME, '{2}', 103), 121) AND DATAVENCIMENTO <= CONVERT(varchar, CONVERT(datetime, '{3}', 103), 121)
										", Todos, Vencidos, Inicio, Fim);

            grdVisaoDataVencimento.DataSource = MetodosSql.GetDT(sql);

        }
    }
}
