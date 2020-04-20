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
    public partial class frmPagamento : Form
    {
        Boolean Editar;
        string Cod;

        
        
            public string CODIGO { get; set; }
            public string NOME { get; set; }
            public string SOBRENOME { get; set; }
            public string CODIGOPARCELA { get; set; }
            public string CODIGOVENDA { get; set; }
            public string RESTANTE { get; set; }
        


        public frmPagamento(bool Editar_, string Cod_)
        {
            InitializeComponent();
            Editar = Editar_;
            Cod = Cod_;
            gridView1.OptionsBehavior.Editable = false;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridView1.BestFitColumns();


        }

      

        public void AtualizaGrid()
        {
            try
            {
                string sql = String.Format(@"SELECT PARCELA.IDPARCELA , PARCELA.NPARCELA , PARCELA.IDVENDA , PARCELA.IDFCFO , CAST(PARCELA.VALOR AS numeric(20,2)) AS VALOR , cast(isnull(X.VALORPAGO,0.00)as numeric(20,2)) as 'VALOR PAGO' , PARCELA.STATUS , PARCELA.DATAVENCIMENTO ,VENDA.DATAINCLUSAO AS DATAVENDA ,CONVERT(varchar, CONVERT(DATETIME, DATAPAGAMENTO, 121), 103) AS DATAPAGAMENTO , cast((isnull(PARCELA.VALOR, 0) - isnull(X.VALORPAGO, 0)) as numeric(20,2)) as RESTANTE FROM PARCELA
                                                       INNER JOIN VENDA
													   ON PARCELA.IDVENDA = VENDA.IDVENDA
													   
                                                       left JOIN(SELECT IDPARCELA, cast((SUM(isnull((RECEBIMENTO.VALORDINHEIRO),0.00))+ (SUM(isnull((RECEBIMENTO.VALORCHEQUE),0.00)))+ 
							(SUM(isnull((RECEBIMENTO.VALORCARTAOCREDITO),0.00)) + (SUM(isnull((RECEBIMENTO.VALORCARTAODEBITO),0.00)))))as numeric(20,2))
                                                       AS 'VALORPAGO' FROM RECEBIMENTO
													   
													
                                                       GROUP BY IDPARCELA)X
                                                       ON X.IDPARCELA = PARCELA.IDPARCELA
													   where PARCELA.IDFCFO = {0} {1}
                                                       Order by IDPARCELA", txtCodigoCliente.Text, CODIGOVENDA);


                gridView1.BestFitColumns();
                gridControl1.DataSource = MetodosSql.GetDT(sql);

               

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cadastro()
        {
          
        }


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CarregaNome()
        {
            txtCodigoCliente.Text = Cod;
            txtNomeCliente.Text = MetodosSql.GetField(String.Format("select NOME from FCFO where IDFCFO = '{0}'", Cod), "NOME");
            txtSobrenome.Text = MetodosSql.GetField(String.Format("select NOMEFANTASIA from FCFO where IDFCFO = '{0}'", Cod), "NOMEFANTASIA");
            AtualizaGrid();
        }

        private void frmPagamento_Load(object sender, EventArgs e)
        {
            try
            {
                if (Editar)
                {
                    CarregaNome();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cadastro();
            AtualizaGrid();
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            frmSelecionaClientePsgsmento frm = new frmSelecionaClientePsgsmento();
            frm.ShowDialog();
            AtualizaGrid();

            Cod = frm.CODIGO;

            CarregaNome();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Cadastro();
            this.Close();
            
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            
            

            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                object  obj = gridView1.GetRowCellValue(rowHandle, "IDVENDA");
                
                string sql = String.Format(@"select CONVERT(varchar, CONVERT(DATETIME, DATAPAGAMENTO, 121), 103) as 'Nasc' from VENDA where IDVENDA = {0}", obj);
                

                frmIdPagamento frm = new frmIdPagamento(false , null);
                frm.txtDataPagamento.Text = MetodosSql.GetField(String.Format(@"select CONVERT(varchar, CONVERT(DATETIME, DATAPAGAMENTO, 121), 103) as 'Data' from VENDA where IDVENDA = {0}", obj), "Data");
                frm.CODIGOPARCELA = gridView1.GetRowCellValue(rowHandle, "IDPARCELA").ToString();
                frm.CODIGOVENDA = gridView1.GetRowCellValue(rowHandle, "IDVENDA").ToString();
                frm.RESTANTE =   gridView1.GetRowCellValue(rowHandle, "RESTANTE").ToString();
                frm.CODIGOCLIENTE = gridView1.GetRowCellValue(rowHandle, "IDFCFO").ToString();
                frm.ShowDialog();
                AtualizaGrid();
                
               


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void gridControl1_Leave(object sender, EventArgs e)
        {
            
        }

        private void btnVisualizarVenda_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                var obj = gridView1.GetRowCellValue(rowHandle, "IDVENDA");

                frmCadastroVenda frm = new frmCadastroVenda(true, obj.ToString() ,null);
                frm.btnAdicionar.Enabled = false;
                frm.btnExcluir.Enabled = false;
                frm.btnOk.Enabled = false;
                frm.btnSalvar.Enabled = false;
                frm.btnSelecionaProduto.Enabled = false;
                frm.simpleButton2.Enabled = false;
                frm.btnTipoPagamento.Enabled = false;
                frm.txtObservacao.Enabled = false;
                frm.txtValorTotal.Enabled = false;
                frm.txtValorUnitario.Enabled = false;
                frm.txtTotalVenda.Enabled = false;
                frm.txtQuantidade.Enabled = false;
                frm.ShowDialog();
                
               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btnEfetuarPagamento_Click(object sender, EventArgs e)
        {

            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                frmIdPagamento frm = new frmIdPagamento(false, null);

                frm.CODIGOPARCELA = gridView1.GetRowCellValue(rowHandle, "IDPARCELA").ToString();
                frm.CODIGOVENDA = gridView1.GetRowCellValue(rowHandle, "IDVENDA").ToString();
                frm.RESTANTE = gridView1.GetRowCellValue(rowHandle, "RESTANTE").ToString();
                frm.CODIGOCLIENTE = gridView1.GetRowCellValue(rowHandle, "IDFCFO").ToString();
                frm.ShowDialog();
                AtualizaGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione uma Parcela");
            }
        }

       
    }
}




