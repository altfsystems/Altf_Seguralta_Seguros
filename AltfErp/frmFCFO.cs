using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Data.Linq;

namespace AltfErp
{
    public partial class frmFCFO : Form
    {

        bool Editar, validaBtn1, validaBtn2;
        string Cod;
        string tipo;
        int validacao;
        string valida;
        Bitmap bmpImagem1, bmpImagem2;


        public frmFCFO(Boolean _Editar, String _Cod, String _tipo)
        {
            InitializeComponent();
            Editar = _Editar;
            Cod = _Cod;
            tipo = _tipo;
            txtDataInclusao.Text = DateTime.Now.ToString();
            txtEstado.SelectedIndex = 24;
           

        }




        private void Validar()
        {
            if (String.IsNullOrWhiteSpace(txtNome.Text))
            {
                throw new Exception("Digite o nome");

            }

            if (String.IsNullOrWhiteSpace(txtNomeFantasia.Text))
            {
                if (tipo == "F")
                {
                    throw new Exception("Digite o nome fantasia");
                }
                else
                {
                    throw new Exception("Digite o sobrenome");
                }

            }

            if (String.IsNullOrWhiteSpace(txtTipoPessoa.Text))
            {
                throw new Exception("Selecione o tipo de pessoa");
            }




        }

        private void frmFCFO_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = Cod;
            validaBtn1 = false;
            validaBtn2 = false;
            txtTipoPessoa.Select();
                        
            try
            {
                if (Editar)
                {

                    string sql = String.Format(@"select * from FCFO where IDFCFO = {0}", Cod);

                    txtNome.Text = MetodosSql.GetField(sql, "NOME");
                    txtTipoPessoa.Text = MetodosSql.GetField(sql, "TIPOPESSOA");
                    txtNomeFantasia.Text = MetodosSql.GetField(sql, "NOMEFANTASIA");
                    txtCpf.Text = MetodosSql.GetField(sql, "CPF");
                    txtCNPJ.Text = MetodosSql.GetField(sql, "CNPJ");
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
                    txtRg.Text = MetodosSql.GetField(sql, "RG");
                    txtCnh.Text = MetodosSql.GetField(sql, "CNH");
                    txtEstadoCivil.Text = MetodosSql.GetField(sql, "ESTADOCIVIL");
                    txtApolice.Text = MetodosSql.GetField(sql, "APOLICE");
                    txtTipoResidencia.Text = MetodosSql.GetField(sql, "TIPORESIDENCIA");
                    txtObservacaoDocumento.Text = MetodosSql.GetField(sql, "OBSDOCUMENTO");
                    sql = String.Format("SELECT * FROM FCFOIMAGEM WHERE IDFCFO = {0}", Cod);
                    valida = MetodosSql.GetField(sql, "IMAGEM1");
                    if (valida != "")
                    {
                        pbImagemDoc1.Image = Image.FromStream(MetodosSql.GetImage(sql, "IMAGEM1"));
                    }
                    valida = MetodosSql.GetField(sql, "IMAGEM2");
                    if (valida != "")
                    {
                        pbDoc2.Image = Image.FromStream(MetodosSql.GetImage(sql, "IMAGEM2"));
                    }
                    txtDataInclusao.Text = MetodosSql.GetField(String.Format(@"select CONVERT(varchar, CONVERT(DATETIME, DATAINCLUSAO, 121), 103) as 'Nasc' from FCFO where IDFCFO = {0}", Cod), "Nasc");
                    



                }





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cadastro()
        {
            try
            {
                Validar();
                validacao = 0;


            

                if (Editar)
                {
                    string SQL = String.Format(@"update FCFO
	                                             set NOME = '{0}',  NOMEFANTASIA =  '{1}',  CPF = '{2}', CNPJ = '{3}', RG = '{4}', DTANASCIMENTO = '{5}', SEXO = '{6}',
                                                     TELEFONE1 = '{7}', TELEFONE2 =  '{8}', CELULAR = '{9}', CELULAR2 =  '{10}', EMAIL = '{11}',  EMAIL2 = '{12}', 
                                                     RUA = '{13}', LOGRADOURO = '{14}', NUMERO = '{15}', BAIRRO = '{16}',
                                                     CIDADE = '{17}', CEP = '{18}', ESTADO = '{19}', COMPLEMENTO = '{20}', OBSERVACAO = '{21}', TIPOPESSOA = '{22}', CNH = '{23}',
                                                     ESTADOCIVIL = '{24}', TIPORESIDENCIA = '{25}', OBSDOCUMENTO = '{26}' ,DATAINCLUSAO = getdate(), APOLICE = '{28}' where IDFCFO = '{27}'",
                                           /*{0}*/   txtNome.Text,
                                           /*{1}*/   txtNomeFantasia.Text,
                                           /*{2}*/   txtCpf.Text,
                                           /*{3}*/   txtCNPJ.Text,
                                           /*{4}*/   txtRg.Text,
                                           /*{5}*/   txtDataNasc.Text,
                                           /*{6}*/   txtSexo.Text,
                                           /*{7}*/   txtTelefone.Text,
                                           /*{8}*/   txtTelefone2.Text,
                                           /*{9}*/   txtCelular.Text,
                                           /*{10}*/  txtCelular2.Text,
                                           /*{11}*/  txtEmail.Text,
                                           /*{12}*/  txtEmail2.Text,
                                           /*{13}*/  txtRua.Text,
                                           /*{14}*/  txtLogradouro.Text,
                                           /*{15}*/  txtNumero.Text,
                                           /*{16}*/  txtBairro.Text,
                                           /*{17}*/  txtCidade.Text,
                                           /*{18}*/  txtCep.Text,
                                           /*{19}*/  txtEstado.Text,
                                           /*{20}*/  txtComplemento.Text,
                                           /*{21}*/  txtObservacao.Text,
                                           /*{22}*/  txtTipoPessoa.Text,
                                           /*{23}*/  txtCnh.Text,
                                           /*{24}*/  txtEstadoCivil.Text,
                                           /*{25}*/  txtTipoResidencia.Text,
                                           /*{26}*/  txtObservacaoDocumento.Text,
                                           /*{27}*/  Cod,
                                           /*{28}*/  txtApolice.Text);
                                             
		                                             



                    MetodosSql.ExecQuery(SQL);

                    Image Valida1, Valida2;

                    Valida1 = pbImagemDoc1.Image;
                    Valida2 = pbDoc2.Image;

                    if (Valida2 != null && Valida1 != null)
                    {
                        if (validaBtn1 == true && validaBtn2 == true)
                        {
                            MetodosSql.InsereImagem(String.Format(@"UPDATE FCFOIMAGEM SET IMAGEM1 = @Imagem, IMAGEM2 = @Imagem2 WHERE IDFCFO = {0}", Cod), bmpImagem1, bmpImagem2);
                        }
                        else if (validaBtn1 == true && validaBtn2 == false)
                        {
                            MetodosSql.InsereImagem(String.Format(@"UPDATE FCFOIMAGEM SET IMAGEM1 = @Imagem WHERE IDFCFO = {0}", Cod), bmpImagem1, bmpImagem2);
                        }
                        else if (validaBtn1 == false && validaBtn2 == true)
                        {
                            MetodosSql.InsereImagem(String.Format(@"UPDATE FCFOIMAGEM SET IMAGEM2 = @Imagem2 WHERE IDFCFO = {0}", Cod), bmpImagem1, bmpImagem2);
                        }

                    }
                    else if (Valida1 == null && Valida2 != null)
                    {
                        MetodosSql.InsereImagem(String.Format(@"UPDATE FCFOIMAGEM SET IMAGEM2 = @Imagem2 WHERE IDFCFO = {0}", Cod), bmpImagem1, bmpImagem2);
                    }
                    else if (Valida2 == null && Valida1 != null)
                    {
                        MetodosSql.InsereImagem(String.Format(@"UPDATE FCFOIMAGEM SET IMAGEM1 = @Imagem WHERE IDFCFO = {0}", Cod), bmpImagem1, bmpImagem2);
                    }
                    else if (Valida2 == null && Valida1 == null)
                    {
                        MetodosSql.ExecQuery(String.Format("UPDATE FCFOIMAGEM SET IMAGEM1 = NULL, IMAGEM2 = NULL WHERE IDFCFO = {0}", Cod));
                    }


                }
                else
                {


                    string SQL = String.Format(@"insert into FCFO (NOME , NOMEFANTASIA, CPF, CNPJ, RG, CNH, TELEFONE1, TELEFONE2, CELULAR, CELULAR2, EMAIL, EMAIL2, RUA, LOGRADOURO, NUMERO, BAIRRO, CIDADE,
                       CEP,ESTADO , COMPLEMENTO, OBSERVACAO, TIPO, DTANASCIMENTO, SEXO, ESTADOCIVIL, TIPORESIDENCIA, OBSDOCUMENTO, TIPOPESSOA, DATAINCLUSAO, APOLICE) values ('{0}','{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}','{10}','{11}','{12}','{13}','{14}',
                       '{15}','{16}','{17}','{18}','{19}' , '{20}', '{21}' , '{22}' , '{23}' , '{24}' , '{25}' , '{26}','{27}', getdate(), '{28}' ) select SCOPE_IDENTITY()",
                                  /*{0}*/ txtNome.Text,
                                  /*{1}*/ txtNomeFantasia.Text,
                                  /*{2}*/ txtCpf.Text,
                                  /*{3}*/ txtCNPJ.Text,
                                  /*{4}*/ txtRg.Text,
                                  /*{5}*/ txtCnh.Text,
                                  /*{6}*/ txtTelefone.Text,
                                  /*{7}*/ txtTelefone2.Text,
                                  /*{8}*/ txtCelular.Text,
                                  /*{9}*/ txtCelular2.Text,
                                 /*{10}*/ txtEmail.Text,
                                 /*{11}*/ txtEmail2.Text,
                                 /*{12}*/ txtRua.Text,
                                 /*{13}*/ txtLogradouro.Text,
                                 /*{14}*/ txtNumero.Text,
                                 /*{15}*/ txtBairro.Text,
                                 /*{16}*/ txtCidade.Text,
                                 /*{17}*/ txtCep.Text,
                                 /*{18}*/ txtEstado.Text,
                                 /*{19}*/ txtComplemento.Text,
                                 /*{20}*/ txtObservacao.Text,
                                 /*{21}*/ tipo,
                                 /*{22}*/ txtDataNasc.Text,
                                 /*{23}*/ txtSexo.Text,
                                 /*{24}*/ txtEstadoCivil.Text,
                                 /*{25}*/ txtTipoResidencia.Text,
                                 /*{26}*/ txtObservacaoDocumento.Text,
                                 /*{27}*/ txtTipoPessoa.Text,
                                 /*{28}*/  txtApolice.Text);
                            



                    object Ncad = MetodosSql.ExecScalar(SQL);
                    txtCodigo.Text = Ncad.ToString();
                    Image Valida1, Valida2;

                    Valida1 = pbImagemDoc1.Image;
                    Valida2 = pbDoc2.Image;

                    if (Valida2 != null && Valida1 != null)
                    {
                        MetodosSql.InsereImagem(String.Format(@"INSERT INTO FCFOIMAGEM(IDFCFO, IMAGEM1, IMAGEM2) VALUES ({0} ,@Imagem, @Imagem2)", Ncad), bmpImagem1, bmpImagem2);
                    }
                    else if (Valida1 == null && Valida2 != null)
                    {
                        MetodosSql.InsereImagem(String.Format(@"INSERT INTO FCFOIMAGEM(IDFCFO, IMAGEM2) VALUES({0}, @Imagem2) ", Ncad), bmpImagem1, bmpImagem2);
                    }
                    else if (Valida2 == null && Valida1 != null)
                    {
                        MetodosSql.InsereImagem(String.Format(@"INSERT INTO FCFOIMAGEM(IDFCFO, IMAGEM1) VALUES({0}, @Imagem) ", Ncad), bmpImagem1, bmpImagem2);
                    }
                    else if (Valida2 == null && Valida1 == null)
                    {
                        MetodosSql.ExecQuery(String.Format("INSERT INTO FCFOIMAGEM(IDFCFO) VALUES({0}) ", Ncad));
                    }

                    Editar = true;

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validacao = 1;
            }
        }

        private void txtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtTelefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtTelefone2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCelular_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCelular2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtDatanascimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtSexo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

   

        private void txtEstado_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        



        private void btnOk_Click_1(object sender, EventArgs e)
        {
            Cadastro();
            if (validacao != 1)
            {

                this.Close();
            }
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            Cadastro();
        }

        private void txtApolice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtCnh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnLimpar1_Click(object sender, EventArgs e)
        {
            pbImagemDoc1.Image = null;
        }

        private void btnLimpar2_Click(object sender, EventArgs e)
        {
            pbDoc2.Image = null;
        }

        private void txtNumero_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtSexo_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)))
            {

                e.Handled = true;

            }
        }

        private void txtEstadoCivil_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)))
            {

                e.Handled = true;

            }
        }

        private void txtEstado_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar) || char.IsControl(e.KeyChar)))
            {

                e.Handled = true;

            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAbrir1_Click(object sender, EventArgs e)
        {
            validaBtn1 = true;
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string imagem1 = open.FileName;
                    bmpImagem1 = new Bitmap(imagem1);
                    pbImagemDoc1.Image = bmpImagem1;
                }
                catch(Exception)
                {
                    MessageBox.Show("O arquivo de imagem não suporta arquivos .pdf", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                




            }
        }

        private void btnAbrir2_Click(object sender, EventArgs e)
        {
            validaBtn2 = true;
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string imagem2 = open.FileName;
                    bmpImagem2 = new Bitmap(imagem2);
                    pbDoc2.Image = bmpImagem2;
                }
                catch (Exception)
                {
                    MessageBox.Show("O arquivo de imagem não suporta arquivos .pdf", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                

            }
        }
    }
}





























