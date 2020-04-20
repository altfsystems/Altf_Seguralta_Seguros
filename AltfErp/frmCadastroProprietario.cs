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
    public partial class frmCadastroProprietario : Form
    {
            bool Editar;
            string Cod;
             

        public frmCadastroProprietario(Boolean Editar_, String Cod_)
        {
            InitializeComponent();
            Editar = Editar_;
            Cod = Cod_;
            
        }

        private void frmCadastroProprietario_Load(object sender, EventArgs e)
        {
                                       
            try
            {

                string sql = String.Format(@"select * from PROPRIETARIO");
                txtNome.Text = MetodosSql.GetField(sql, "NOME");
                txtNomeFantasia.Text = MetodosSql.GetField(sql, "NOMEFANTASIA");
                txtCpf.Text = MetodosSql.GetField(sql, "CPFCNPJ");
                txtTelefone.Text = MetodosSql.GetField(sql, "TELEFONE");
                txtEmail.Text = MetodosSql.GetField(sql, "EMAIL");
                txtContato.Text = MetodosSql.GetField(sql, "CONTATO");
                txtRua.Text = MetodosSql.GetField(sql, "RUA");
                txtLogradouro.Text = MetodosSql.GetField(sql, "LOGRADOURO");
                txtNumero.Text = MetodosSql.GetField(sql, "NUMERO");
                txtBairro.Text = MetodosSql.GetField(sql, "BAIRRO");
                txtCidade.Text = MetodosSql.GetField(sql, "CIDADE");
                txtCep.Text = MetodosSql.GetField(sql, "CEP");
                txtEstado.Text = MetodosSql.GetField(sql, "ESTADO");
                txtComplemento.Text = MetodosSql.GetField(sql, "COMPLEMENTO");
                txtDataInclusao.Text = MetodosSql.GetField(String.Format(@"select CONVERT(varchar, CONVERT(DATETIME , DATAINCLUSAO, 121) , 103) AS 'Nasc' from PROPRIETARIO"), "Nasc");
                
                
            }  
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Cadastro()
        {
            try
            {               
                    string sql = String.Format(@"update PROPRIETARIO 
                                                set NOME = ('{0}'), 
                                                    NOMEFANTASIA = ('{1}') , 
                                                    CPFCNPJ = ('{2}') , 
                                                    TELEFONE = ('{3}') , 
                                                    EMAIL = ('{4}') , 
                                                    CONTATO = ('{5}') , 
                                                    RUA = ('{6}'), 
                                                    LOGRADOURO = ('{7}'), 
                                                    NUMERO = ('{8}') , 
                                                    BAIRRO = ('{9}') , 
                                                    CIDADE = ('{10}'), 
                                                    CEP = ('{11}') , 
                                                    ESTADO = ('{12}') , 
                                                    COMPLEMENTO = ('{13}') , 
                                                    DATAINCLUSAO = getdate()
                                                    "
                    , txtNome.Text , txtNomeFantasia.Text , txtCpf.Text , txtTelefone.Text , txtEmail.Text , txtContato.Text , txtRua.Text , txtLogradouro.Text , txtNumero.Text , txtBairro.Text , txtCidade.Text , txtCep.Text , txtEstado.Text , txtComplemento.Text);

                    object CodCad = MetodosSql.ExecScalar(sql);
                    //txtCodigo.Text = CodCad.ToString();
                
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cadastro();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Cadastro();
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
