using Microsoft.SqlServer.Server;
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
            txtNome.Select();
            
        }

        private void frmCadastroProprietario_Load(object sender, EventArgs e)
        {
                                       
            try
            {

                string sql = String.Format(@"select * from PROPRIETARIO");
                txtNome.Text = MetodosSql.GetField(sql, "NOME");
                txtNomeFantasia.Text = MetodosSql.GetField(sql, "NOMEFANTASIA");
                txtCpf.Text = MetodosSql.GetField(sql, "CPF");
                txtCnpj.Text = MetodosSql.GetField(sql, "CNPJ");
                txtTelefone.Text = MetodosSql.GetField(sql, "TELEFONE");
                txtTelefone2.Text = MetodosSql.GetField(sql, "TELEFONE2");
                txtCelular.Text = MetodosSql.GetField(sql, "CELULAR");
                txtCelular2.Text = MetodosSql.GetField(sql, "CELULAR2");
                txtEmail.Text = MetodosSql.GetField(sql, "EMAIL");
                txtEmail2.Text = MetodosSql.GetField(sql, "EMAIL2");
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
                                                set NOME = '{0}', 
                                                    NOMEFANTASIA = '{1}' , 
                                                    CPF = '{2}' , 
                                                    CNPJ = '{3}' ,
                                                    TELEFONE = '{4}',
                                                    TELEFONE2 = '{5}',
                                                    CELULAR = '{6}',
                                                    CELULAR2 = '{7}',
                                                    EMAIL = '{8}',
                                                    EMAIL2 = '{9}',
                                                    CONTATO = '{10}' , 
                                                    RUA = '{11}', 
                                                    LOGRADOURO = '{12}', 
                                                    NUMERO = '{13}' , 
                                                    BAIRRO = '{14}' , 
                                                    CIDADE = '{15}', 
                                                    CEP = '{16}' , 
                                                    ESTADO = '{17}' , 
                                                    COMPLEMENTO = '{18}' , 
                                                    DATAINCLUSAO = getdate()",
                   /*{0}*/ txtNome.Text,
                   /*{1}*/ txtNomeFantasia.Text,
                   /*{2}*/ txtCpf.Text,
                   /*{3}*/  txtCnpj.Text,
                   /*{4}*/  txtTelefone.Text,
                   /*{5}*/  txtTelefone2.Text,
                   /*{6}*/  txtCelular.Text,
                   /*{7}*/  txtCelular2.Text,
                   /*{8}*/  txtEmail.Text,
                   /*{9}*/  txtEmail2.Text,
                   /*{10}*/  txtContato.Text,
                   /*{11}*/  txtRua.Text,
                   /*{12}*/  txtLogradouro.Text,
                   /*{13}*/  txtNumero.Text,
                   /*{14}*/  txtBairro.Text,
                   /*{15}*/  txtCidade.Text,
                   /*{16}*/  txtCep.Text,
                   /*{17}*/  txtEstado.Text,
                   /*{18}*/  txtComplemento.Text);


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

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
