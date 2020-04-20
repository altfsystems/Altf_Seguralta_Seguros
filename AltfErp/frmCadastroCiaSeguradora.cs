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
    public partial class frmCadastroCiaSeguradora : Form
    {
        Boolean Editar;
        String Cod;
        public frmCadastroCiaSeguradora(bool editar, string cod)
        {
            InitializeComponent();
            Editar = editar;
            Cod = cod;
        }

        private void Cadastro()
        {
            try
            {
                if(Editar)
                {
                    string sql = String.Format(@"UPDATE FCFOSEGURADORA SET NOME = '{0}', NOMEFANTASIA = '{1}', CPF = '{2}', CNPJ = '{3}', RG = '{4}', OBSDOCUMENTO = '{5}',
                                                 SEXO = '{6}', TELEFONE1 = '{7}', TELEFONE2 = '{8}', CELULAR1 = '{9}', CELULAR2 = '{10}', EMAIL = '{11}', EMAIL2 = '{12}',
                                                 OBSERVACAO = '{13}', TIPOPESSOA = '{14}'",
                                                  /*{0}*/  txtNome.Text,
                                                  /*{1}*/  txtNomeFantasia.Text,
                                                  /*{2}*/  txtCpf.Text,
                                                  /*{3}*/  txtCNPJ.Text,
                                                  /*{4}*/  txtRg.Text,
                                                  /*{5}*/  txtObservacaoDocumento.Text,
                                                  /*{6}*/  txtSexo.Text,
                                                  /*{7}*/  txtTelefone.Text,
                                                  /*{8}*/  txtTelefone2.Text,
                                                 /*{9}*/   txtCelular.Text,
                                                 /*{10}*/  txtCelular2.Text,
                                                 /*{11}*/  txtEmail.Text,
                                                 /*{12}*/  txtEmail2.Text,
                                                 /*{13}*/  txtObservacao.Text,
                                                 /*{14}*/  txtTipoPessoa.Text);

                                                 MetodosSql.ExecQuery(sql);

                }
                else
                {
                    string sql = String.Format(@"INSERT INTO FCFOSEGURADORA VALUES('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}',
                                                '{10}', '{11}', '{12}', '{13}', '{14}', getdate())",
                                                  /*{0}*/  txtNome.Text,
                                                  /*{1}*/  txtNomeFantasia.Text,
                                                  /*{2}*/  txtCpf.Text,
                                                  /*{3}*/  txtCNPJ.Text,
                                                  /*{4}*/  txtRg.Text,
                                                  /*{5}*/  txtObservacaoDocumento.Text,
                                                  /*{6}*/  txtSexo.Text,
                                                  /*{7}*/  txtTelefone.Text,
                                                  /*{8}*/  txtTelefone2.Text,
                                                 /*{9}*/   txtCelular.Text,
                                                 /*{10}*/  txtCelular2.Text,
                                                 /*{11}*/  txtEmail.Text,
                                                 /*{12}*/  txtEmail2.Text,
                                                 /*{13}*/  txtObservacao.Text,
                                                 /*{14}*/  txtTipoPessoa.Text);

                                                 MetodosSql.ExecQuery(sql);
                    Editar = true;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            


        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Cadastro();
            this.Close();
        }

        private void frmCadastroCiaSeguradora_Load(object sender, EventArgs e)
        {
            try
            {
                if(Editar)
                {
                    string sql = String.Format(@"SELECT * FROM FCFOSEGURADORA WHERE IDSEGURADORA = '{0}'", Cod);
                    txtNome.Text = MetodosSql.GetField(sql, "NOME");
                    txtNomeFantasia.Text = MetodosSql.GetField(sql, "NOMEFANTASIA");
                    txtSexo.Text = MetodosSql.GetField(sql, "SEXO");
                    txtCelular.Text = MetodosSql.GetField(sql, "CELULAR");
                    txtCelular2.Text = MetodosSql.GetField(sql, "CELULAR2");
                    txtTelefone.Text = MetodosSql.GetField(sql, "TELEFONE1");
                    txtTelefone2.Text = MetodosSql.GetField(sql, "TELEFONE2");
                    txtEmail.Text = MetodosSql.GetField(sql, "EMAIL");
                    txtEmail2.Text = MetodosSql.GetField(sql, "EMAIL2");
                    txtObservacao.Text = MetodosSql.GetField(sql, "OBSERVACAO");
                    txtCpf.Text = MetodosSql.GetField(sql, "CPF");
                    txtCNPJ.Text = MetodosSql.GetField(sql, "CNPJ");
                    txtRg.Text = MetodosSql.GetField(sql, "RG");
                    txtObservacaoDocumento.Text = MetodosSql.GetField(sql, "OBSDOCUMENTO");
                    txtTipoPessoa.Text = MetodosSql.GetField(sql, "TIPOPESSOA");
                    txtDataInclusao.Text = MetodosSql.GetField(String.Format(@"SELECT CONVERT(VARCHAR, DATAINCLUSAO, 103) AS DATAINCLUSAO FROM FCFOSEGURADORA WHERE IDSEGURADORA = '{0}'", Cod), "DATAINCLUSAO");
                    txtCodigo.Text = MetodosSql.GetField(sql, "IDSEGURADORA");
                }
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
