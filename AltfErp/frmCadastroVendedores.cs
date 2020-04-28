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
    public partial class frmCadastroVendedores : Form
    {
        bool Editar;
        string Cod;

        public frmCadastroVendedores(bool editar, string cod)
        {
            InitializeComponent();
            txtDataInclusao.Text = DateTime.Now.ToString();
            txtNome.Select();
            Editar = editar;
            Cod = cod;
        }

        private void Cadastro()
        {
            try
            {
                string sql = String.Format(@"INSERT INTO VENDEDORES VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', GETDATE()) SELECT SCOPE_IDENTITY()",
                                          /*{0}*/  txtNome.Text,
                                          /*{1}*/  txtSobrenome.Text,
                                          /*{2}*/  txtCpf.Text,
                                          /*{3}*/  txtDataNasc.Text,
                                          /*{4}*/  txtSexo.Text,
                                          /*{5}*/  txtTelefone.Text,
                                          /*{6}*/  txtTelefone2.Text,
                                          /*{7}*/  txtCelular.Text,
                                          /*{8}*/  txtCelular2.Text,
                                          /*{9}*/  txtEmail.Text,
                                          /*{10}*/ txtEmail2.Text,
                                          /*{11}*/ txtRua.Text,
                                          /*{12}*/ txtLogradouro.Text,
                                          /*{13}*/ txtNumero.Text,
                                          /*{14}*/ txtBairro.Text,
                                          /*{15}*/ txtCidade.Text,
                                          /*{16}*/ txtCep.Text,
                                          /*{17}*/ txtEstado.Text,
                                          /*{18}*/ txtComplemento.Text,
                                          /*{19}*/ txtObservacao.Text);

                object cod = MetodosSql.ExecScalar(sql);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Update()
        {
            try
            {
                string sql = String.Format(@"UPDATE VENDEDORES SET NOME = '{0}', SOBRENOME = '{1}', CPF = '{2}', DTANASCIMENTO = '{3}', SEXO = '{4}', TELEFONE1 = '{5}',
                                             TELEFONE2 = '{6}', CELULAR = '{7}', CELULAR2 = '{8}', EMAIL = '{9}', EMAIL2 = '{10}', RUA = '{11}', LOGRADOURO = '{12}',
                                             NUMERO = '{13}', BAIRRO = '{14}', CIDADE = '{15}', CEP = '{16}', ESTADO = '{17}', COMPLEMENTO = '{18}', OBSERVACAO = '{19}' WHERE IDVENDEDOR = '{20}'",
                                              /*{0}*/  txtNome.Text,
                                              /*{1}*/  txtSobrenome.Text,
                                              /*{2}*/  txtCpf.Text,
                                              /*{3}*/  txtDataNasc.Text,
                                              /*{4}*/  txtSexo.Text,
                                              /*{5}*/  txtTelefone.Text,
                                              /*{6}*/  txtTelefone2.Text,
                                              /*{7}*/  txtCelular.Text,
                                              /*{8}*/  txtCelular2.Text,
                                              /*{9}*/  txtEmail.Text,
                                              /*{10}*/ txtEmail2.Text,
                                              /*{11}*/ txtRua.Text,
                                              /*{12}*/ txtLogradouro.Text,
                                              /*{13}*/ txtNumero.Text,
                                              /*{14}*/ txtBairro.Text,
                                              /*{15}*/ txtCidade.Text,
                                              /*{16}*/ txtCep.Text,
                                              /*{17}*/ txtEstado.Text,
                                              /*{18}*/ txtComplemento.Text,
                                              /*{19}*/ txtObservacao.Text,
                                              /*{20}*/ Cod);
                MetodosSql.ExecQuery(sql);
                                             
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void frmCadastroVendedores_Load(object sender, EventArgs e)
        {
            if(Editar)
            {
                string sql = String.Format(@"SELECT * FROM VENDEDORES WHERE IDVENDEDOR = {0}", Cod);
                txtCodigo.Text = Cod;
                txtNome.Text = MetodosSql.GetField(sql, "NOME");
                txtSobrenome.Text = MetodosSql.GetField(sql, "SOBRENOME");
                txtCpf.Text = MetodosSql.GetField(sql, "CPF");
                txtDataNasc.Text = MetodosSql.GetField(sql, "DTANASCIMENTO");
                txtSexo.Text = MetodosSql.GetField(sql, "SEXO");
                txtTelefone.Text = MetodosSql.GetField(sql, "TELEFONE1");
                txtTelefone2.Text = MetodosSql.GetField(sql, "TELEFONE2");
                txtCelular.Text = MetodosSql.GetField(sql, "CELULAR");
                txtCelular2.Text = MetodosSql.GetField(sql, "CELULAR2");
                txtEmail.Text = MetodosSql.GetField(sql, "EMAIL");
                txtEmail2.Text = MetodosSql.GetField(sql, "EMAIL2");
                txtRua.Text = MetodosSql.GetField(sql, "RUA");
                txtLogradouro.Text = MetodosSql.GetField(sql, "LOGRADOURO");
                txtNumero.Text = MetodosSql.GetField(sql, "NUMERO");
                txtBairro.Text = MetodosSql.GetField(sql, "BAIRRO");
                txtCidade.Text = MetodosSql.GetField(sql, "CIDADE");
                txtCep.Text = MetodosSql.GetField(sql, "CEP");
                txtEstado.Text = MetodosSql.GetField(sql, "ESTADO");
                txtComplemento.Text = MetodosSql.GetField(sql, "COMPLEMENTO");
                txtObservacao.Text = MetodosSql.GetField(sql, "OBSERVACAO");
                txtDataInclusao.Text = MetodosSql.GetField(sql, "DATAINCLUSAO");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if(Editar)
            {
                Update();
            }
            else
            {
                Cadastro();
            }
            this.Close();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if(Editar)
            {
                Update();
            }
            else
            {
                Cadastro();
                Editar = true;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSexo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)))
            {

                e.Handled = true;

            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
                
}
                
      


