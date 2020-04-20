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
    public partial class frmVisaoOrdemServico : Form
    {
       
        public string NOME { get; set; }
        string Filtro;
        public string Data { get; set; }
        public string RetornoNome { get; set; }
        public string Nulo { get; set; }
        

        public frmVisaoOrdemServico()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            grdVisaoServico.EmbeddedNavigator.Buttons.Append.Visible = false;
            grdVisaoServico.EmbeddedNavigator.Buttons.Remove.Visible = false;
            Filtra();
            gridView1.BestFitColumns();
        }

        private void Filtra()
        {
            frmFiltroServico frmFiltro = new frmFiltroServico();
            frmFiltro.ShowDialog();
            Filtro = frmFiltro.RETORNO;
            Data = frmFiltro.RETORNODATA;
            Nulo = frmFiltro.NULO;
            RetornoNome = frmFiltro.RETORNONOME;



            if (String.IsNullOrWhiteSpace(frmFiltro.RETORNO))
            {
                if (String.IsNullOrWhiteSpace(frmFiltro.RETORNODATA))
                {
                    Data = "1=0";
                    Filtro = "1=0";
                }
                else
                {
                    AtualizaGrid();
                }
               
                
            }
            else
            {
                AtualizaGrid();
            }

            
            

            

            
        }

        private void AtualizaGrid()
        {
            try
            {
                string sql = String.Format(@"select ORDEM.IDORDEM , ORDEM.IDFCFO as IDCLIENTE , FCFO.NOME , FCFO.NOMEFANTASIA AS SOBRENOME, ORDEM.DESCRICAO , ORDEM.CONCLUSAO , ORDEM.OBSERVACAO, CAST(sum(ITEMMOVIMENTO.QUANTIDADE * ITEMMOVIMENTO.VALOR)AS NUMERIC(20,2) ) as 'VALORTOTAL', ORDEM.STATUS , 
                                            CONVERT(VARCHAR , CONVERT(DATETIME , ORDEM.DATAINCLUSAO , 121),103) AS DATAINCLUSAO FROM ORDEM
                                            INNER JOIN FCFO
                                            ON FCFO.IDFCFO = ORDEM.IDFCFO 

                                            INNER JOIN ITEMMOVIMENTO
                                            ON ITEMMOVIMENTO.IDORDEM = ORDEM.IDORDEM
                                            where ORDEM.IDORDEM = ORDEM.IDORDEM AND ORDEM.STATUS = {0} {3} AND CONVERT(VARCHAR , CONVERT(DATETIME , ORDEM.DATAINCLUSAO , 121),103) = {2}'{1}'



                                            group by ORDEM.IDORDEM , ORDEM.IDFCFO , FCFO.NOME , FCFO.NOMEFANTASIA, ORDEM.DESCRICAO , ORDEM.CONCLUSAO , ORDEM.OBSERVACAO, ORDEM.STATUS, ORDEM.DATAINCLUSAO

                                            ORDER BY IDORDEM DESC", Filtro, Data, Nulo, RetornoNome );

                grdVisaoServico.DataSource = MetodosSql.GetDT(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void UpdateCadastro()
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;
                var obj = gridView1.GetRowCellValue(rowHandle, "IDORDEM");
                NOME = gridView1.GetRowCellValue(rowHandle, "NOME").ToString();
                var status = gridView1.GetRowCellValue(rowHandle, "STATUS").ToString();

               if(status == "F")
                {
                    frmServico frm = new frmServico(true, obj.ToString());
                    frm.txtCodigoFcfo.Enabled = false;
                    frm.simpleButton1.Enabled = false;
                    frm.txtNomeCliente.Enabled = false;
                    frm.txtSobrenome.Enabled = false;
                    frm.txtDescricao.Enabled = false;
                    frm.txtValorUnitario.Enabled = false;
                    frm.txtQuantidade.Enabled = false;
                    frm.txtValorTotal.Enabled = false;
                    frm.txtTotalVenda.Enabled = false;
                    frm.btnAdicionar.Enabled = false;
                    frm.btnExcluir.Enabled = false;
                    frm.txtObservacao.Enabled = false;
                    frm.txtConclusao.Enabled = false;
                    frm.btnOk.Enabled = false;
                    frm.btnSalvar.Enabled = false;
                    frm.btnSelecionaProduto.Enabled = false;
                    frm.ShowDialog();
                    AtualizaGrid();
                }
               else
                {
                    frmServico frm = new frmServico(true, obj.ToString());
                    frm.ShowDialog();
                    AtualizaGrid();
                }

               

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmServico frm = new frmServico(false, null); 
            frm.ShowDialog();
            AtualizaGrid();
        }

        private void grdVisaoServico_DoubleClick(object sender, EventArgs e)
        {
            UpdateCadastro();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            UpdateCadastro();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                var rowHandle = gridView1.FocusedRowHandle;

                var obj = gridView1.GetRowCellValue(rowHandle, "IDORDEM");
                var status = gridView1.GetRowCellValue(rowHandle, "STATUS");
                

                

               

                if(status.ToString() == "F")
                {
                    MessageBox.Show("Você Não Pode Excluir um Orçamento Faturado" , "Aviso" , MessageBoxButtons.OK , MessageBoxIcon.Exclamation);
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("Deseja mesmo exluir?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        MetodosSql.ExecQuery(String.Format(@"delete from ORDEM where IDORDEM = {0}", obj.ToString()));
                        AtualizaGrid();
                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            gridView1.BestFitColumns();
        }

        private void InsereEstoque(String IDVENDA)
        {
           String sql = String.Format(@"select IDPRODUTO, QUANTIDADE from ITEMMOVIMENTO where IDVENDA = '{0}'", IDVENDA);

            DataTable Produtos = MetodosSql.GetDT(sql);

            foreach (DataRow Produto in Produtos.Rows)
            {
                sql = String.Format(@"update ESTOQUE
                                             set QUANTIDADE = QUANTIDADE - {0}
                                             where IDPRODUTO = '{1}'", Produto["QUANTIDADE"].ToString().Replace(",", "."), Produto["IDPRODUTO"].ToString());
                MetodosSql.ExecQuery(sql);
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;

            var obj = gridView1.GetRowCellValue(rowHandle, "IDORDEM");
           var total = gridView1.GetRowCellValue(rowHandle, "VALORTOTAL");
            

            string sql = String.Format(@"select STATUS from ORDEM where IDORDEM = {0}" , obj.ToString() );




            if (MetodosSql.GetField(sql , "STATUS") == "F")
            {
                MessageBox.Show("Esta Venda ja Foi Faturada" , "Aviso");
                
            }
            else
            {



                if (DialogResult.Yes == MessageBox.Show("Deseja Faturar Esta Venda?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    string SQL = String.Format(@"insert into VENDA (IDORDEM, IDFCFO,OBSERVACAO)( select IDORDEM, IDFCFO, OBSERVACAO From ORDEM WHERE IDORDEM = {0} )
                                                                                                                                             select SCOPE_IDENTITY()", obj.ToString());
                    object IDVENDA = MetodosSql.ExecScalar(SQL);

                    SQL = String.Format(@"UPDATE ITEMMOVIMENTO SET IDVENDA = {0} where IDORDEM = {1} ", IDVENDA, obj);
                    MetodosSql.ExecQuery(SQL);

                    SQL = String.Format(@"UPDATE ORDEM SET STATUS = ('F') WHERE IDORDEM = {0} ", obj.ToString());
                    MetodosSql.ExecQuery(SQL);

                    InsereEstoque(IDVENDA.ToString());
                    frmPopUpSelecionaTipoPagamento frm = new frmPopUpSelecionaTipoPagamento(IDVENDA.ToString(), false , null, total.ToString());
                    frm.VALORTOTAL = total.ToString();
                    frm.ShowDialog();


                    AtualizaGrid();
                }


            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            Filtra();

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            AtualizaGrid();
        }
    }
}

                

                


        

            
            
                
             
            
                    
