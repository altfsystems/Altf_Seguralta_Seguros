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
    public partial class frmAlteraQuantidade : Form
    {
        Boolean Editar;
        string Cod;

        public frmAlteraQuantidade(bool Editar_, string Cod_)
        {
            InitializeComponent();
            Editar = Editar_;
            Cod = Cod_;
        }

        private void Alterar()
        {
            try
            {
            
                string SQL = String.Format(@"UPDATE ESTOQUE SET QUANTIDADE = '{0}' , OBSERVACAO = '{2}' where IDPRODUTO = '{1}'" , txtQuantidade.Text, txtCodigoProduto.Text, txtObservacao.Text );
                       
                

                Clipboard.SetText(SQL);
                Editar = true;

                MetodosSql.ExecQuery(SQL);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }


        }

 

        private void frmAlteraQuantidade_Load(object sender, EventArgs e)
        {
            txtCodigoProduto.Text = Cod;

            string sql = String.Format(@"select * from ESTOQUE where IDPRODUTO = {0}", Cod);
            txtQuantidade.Text = MetodosSql.GetField(sql, "QUANTIDADE");
            txtObservacao.Text = MetodosSql.GetField(sql, "OBSERVACAO");
            
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txtObservacao.Text))
            {
                MessageBox.Show("Você Precisa Preencher o Campo Observação", "Aviso");
            }
            else
            {
                Alterar();
            }

     
        }
            

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(txtObservacao.Text))
            {
                MessageBox.Show("Você Precisa Preencher o Campo Observação" , "Aviso");
            }
            else
            {
                Alterar();
                this.Close();

            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
