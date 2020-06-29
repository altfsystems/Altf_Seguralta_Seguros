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
using System.Security.Cryptography;

namespace AltfErp
{
    public partial class frmFCFO : Form
    {
        Valida val = new Valida();
        bool Editar;
        List<Anexos> anexosVisao = new List<Anexos>();
        string Cod;
        string codFcfo;
        string tipo;
        int validacao;
        string valida;        
        string indiceItem;

        public frmFCFO(Boolean _Editar, String _Cod, String _tipo)
        {
            InitializeComponent();
            Editar = _Editar;
            Cod = _Cod;
            tipo = _tipo;
            txtDataInclusao.Text = DateTime.Now.ToString();
            txtEstado.SelectedIndex = 24;
        }

        public class Anexos
        {
            public String Nome { get; set; }
            public String Arquivo { get; set; }
            public String Extensão { get; set; }
        }

        private void Insert()
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
            codFcfo = Ncad.ToString();
            txtCodigo.Text = Ncad.ToString();

            InsereImagem();

            Editar = true;
        }

        private void Validar()
        {
            if (String.IsNullOrWhiteSpace(txtNome.Text))
            {
                throw new Exception("Por favor, digite o nome");

            }
            if (String.IsNullOrWhiteSpace(txtNomeFantasia.Text))
            {
                if (tipo == "F")
                {
                    throw new Exception("Por favor, digite o nome fantasia");
                }
                else
                {
                    throw new Exception("Por favor, digite o sobrenome");
                }
            }
            if (String.IsNullOrWhiteSpace(txtTipoPessoa.Text))
            {
                throw new Exception("Por favor, selecione o tipo de pessoa");
            }
            if (txtCpf.Text == "___.___.___-__" && txtTipoPessoa.Text == "Pessoa Física")
            {
                throw new Exception("Por favor, digite o cpf");
            }
        }
        private void frmFCFO_Load(object sender, EventArgs e)
        {
            txtCodigo.Text = Cod;
            txtTipoPessoa.Select();
            try
            {
                if (Editar)
                {

                    string sql = String.Format(@"select * from FCFO where IDFCFO = {0}", Cod);

                    gridControl1.DataSource = MetodosSql.GetDT("SELECT IDIMAGEM, NOMEANEXO, EXTENSAO FROM FCFOIMAGEM WHERE IDFCFO = " + Cod);
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
                    valida = MetodosSql.GetField(sql, "IMAGEM2");
                    txtDataInclusao.Text = MetodosSql.GetField(String.Format(@"select CONVERT(varchar, CONVERT(DATETIME, DATAINCLUSAO, 121), 103) as 'Nasc' from FCFO where IDFCFO = {0}", Cod), "Nasc");
                }
                else
                {
                    btnDownload.Text = "Abrir";
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
                    Cod = txtCodigo.Text;
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
                    InsereImagem();
                }
                else
                {

                    string sql = String.Format(@"SELECT CPF FROM FCFO WHERE CPF = '{0}'", txtCpf.Text);
                    string valida = MetodosSql.GetField(sql, "CPF");

                    if (valida == "___.___.___-__")
                    {
                        Insert();
                    }
                    else if (valida == "")
                    {

                        Insert();
                    }
                    else
                    {
                        MessageBox.Show("Já existe um cadastro com esse cpf!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        validacao = 1;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                validacao = 1;
            }
        }

        private void btnOk_Click_1(object sender, EventArgs e)
        {
            if (!val.ValidaEmail(txtEmail.Text) || !val.ValidaEmail(txtEmail2.Text))
            {
                MessageBox.Show("Insira um EMAIL válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!val.ValidaCNPJ(txtCNPJ.Text))
            {
                MessageBox.Show("Insira um CNPJ válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (!val.ValidaCPF(txtCpf.Text))
            {
                MessageBox.Show("Insira um CPF válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtCpf.Text == "___.___.___ - __" && txtTipoPessoa.Text == "Pessoa Física")
            {
                MessageBox.Show("Por favor, informar o CPF", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Cadastro();
                if (validacao != 1)
                {
                    this.Close();
                }
            }

        }
        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            if (!val.ValidaEmail(txtEmail.Text) || !val.ValidaEmail(txtEmail2.Text))
            {
                MessageBox.Show("Insira um EMAIL válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!val.ValidaCNPJ(txtCNPJ.Text))
            {
                MessageBox.Show("Insira um CNPJ válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!val.ValidaCPF(txtCpf.Text))
            {
                MessageBox.Show("Insira um CPF válido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                Cadastro();
            }
        }
        private void InsereImagem()
        {
            try
            {
                if (!Editar)
                {
                    foreach (Anexos anexos in anexosVisao)
                    {
                        string caminho = anexos.Arquivo;
                        byte[] imagem = System.IO.File.ReadAllBytes(caminho);
                        MetodosSql.InsereImagem(String.Format(@"INSERT INTO FCFOIMAGEM(IDFCFO, IMAGEM1, NOMEANEXO, EXTENSAO, CAMINHO) VALUES('{0}', @Imagem, '{1}', '{2}', '{3}')", codFcfo, anexos.Nome, anexos.Extensão, caminho), imagem);
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (Editar)
                {
                    var rowHandle = gridView1.FocusedRowHandle;
                    var nomeAnexo = gridView1.GetRowCellValue(rowHandle, "NOMEANEXO");
                    var idImagem = gridView1.GetRowCellValue(rowHandle, "IDIMAGEM");
                    string sql = "SELECT IMAGEM1, EXTENSAO FROM FCFOIMAGEM WHERE IDFCFO = " + Cod + " AND IDIMAGEM = " + idImagem;
                    string extensao = MetodosSql.GetField(sql, "EXTENSAO");

                    if (idImagem != null)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.FileName = nomeAnexo.ToString();
                        if (extensao == ".pdf")
                        {
                            sfd.Filter = "Arquivos (.pdf)|*.pdf";
                        }
                        else if (extensao == ".jpg")
                        {
                            sfd.Filter = "Arquivos (.jpg)|*.jpg";
                        }
                        else if (extensao == ".jpeg")
                        {
                            sfd.Filter = "Arquivos (.jpeg)|*.jpeg";
                        }
                        else if (extensao == ".png")
                        {
                            sfd.Filter = "Arquivos (.png)|*.png";
                        }
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            string caminho = sfd.FileName;
                            string nome = Path.GetFileName(caminho);
                            byte[] imagem = MetodosSql.GetImagePdf(sql, "IMAGEM1");
                            System.IO.File.WriteAllBytes(caminho, imagem);
                            MessageBox.Show("Salvo com sucesso!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Por favor, selecione um cadastro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    var rowHandle = gridView1.FocusedRowHandle;
                    var extensao = gridView1.GetRowCellValue(rowHandle, "Extensão");
                    var fileName = gridView1.GetRowCellValue(rowHandle, "Arquivo");
                    if (extensao.ToString() == ".pdf")
                    {
                        frmVisualizaPdf frm = new frmVisualizaPdf(fileName.ToString());
                        frm.ShowDialog();
                    }
                    else
                    {
                        frmVisualizaImagem frm = new frmVisualizaImagem(fileName.ToString());
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                if (Editar)
                {
                    if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir este anexo?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        var rowHandle = gridView1.FocusedRowHandle;
                        var idImagem = gridView1.GetRowCellValue(rowHandle, "IDIMAGEM");
                        MetodosSql.ExecQuery("DELETE FROM FCFOIMAGEM WHERE IDIMAGEM = " + idImagem);
                        gridControl1.DataSource = MetodosSql.GetDT("SELECT IDIMAGEM, NOMEANEXO, CAMINHO, EXTENSAO FROM FCFOIMAGEM WHERE IDFCFO = " + txtCodigo.Text);
                    }
                }
                else
                {
                    if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir este anexo?", "Pergunta", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        var rowHandle = gridView1.FocusedRowHandle;
                        var nome = gridView1.GetRowCellValue(rowHandle, "NOME");
                        anexosVisao.RemoveAt(int.Parse(indiceItem.ToString()));
                        gridView1.ClearDocument();
                        gridControl1.DataSource = anexosVisao;
                        gridControl1.RefreshDataSource();
                    }
                }
                btnExcluir.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            if (!Editar)
            {
                int indice = int.Parse(rowHandle.ToString());
                indiceItem = indice.ToString();
            }
            btnExcluir.Enabled = true;
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
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog file = new OpenFileDialog() { ValidateNames = true, Multiselect = false, Filter = "Arquivos de Imagem|*.pdf;*.jpg;*.JPEG;*.png" })
                {
                    if (file.ShowDialog() == DialogResult.OK)
                    {
                        string extensao = Path.GetExtension(file.FileName);
                        string nome = Path.GetFileName(file.FileName);
                        string arquivo = file.FileName;
                        byte[] imagem = File.ReadAllBytes(file.FileName);

                        if (Editar)
                        {
                            MetodosSql.InsereImagem(String.Format("INSERT INTO FCFOIMAGEM(IDFCFO, IMAGEM1, NOMEANEXO, EXTENSAO, CAMINHO) VALUES('{0}', @Imagem, '{1}', '{2}', '{3}')", txtCodigo.Text, nome, extensao, arquivo), imagem);
                            gridControl1.DataSource = MetodosSql.GetDT("SELECT IDIMAGEM, NOMEANEXO, EXTENSAO FROM FCFOIMAGEM WHERE IDFCFO = " + txtCodigo.Text);
                        }
                        else
                        {

                            Anexos classe = new Anexos();
                            gridView1.ClearDocument();
                            classe.Nome = nome;
                            classe.Arquivo = arquivo;
                            classe.Extensão = extensao;
                            anexosVisao.Add(classe);
                            gridControl1.DataSource = anexosVisao;
                            gridControl1.RefreshDataSource();
                        }

                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Selecione um arquivo válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}





































































