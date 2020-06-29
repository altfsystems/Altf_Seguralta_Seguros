using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AltfErp
{
    public partial class frmCadastroUsuario : Form
    {
        Boolean Editar;
        String Cod;
        public frmCadastroUsuario(bool editar, string cod)
        {
            InitializeComponent();
            Editar = editar;
            Cod = cod;
        }

        private bool validaCampos()
        {
            if (string.IsNullOrWhiteSpace(txtUsuario.Text))
            {
                MessageBox.Show("Por favor, insira um usuário!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                MessageBox.Show("Por favor, insira um nome!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (string.IsNullOrWhiteSpace(txtSenha.Text))
            {
                MessageBox.Show("Por favor, insira uma senha!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("As senhas não conferem!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else
            {
                return true;
            }
        }

        private static string CriptografaSenha(string _senha)
        {
            StringBuilder senha = new StringBuilder();

            MD5 md5 = MD5.Create();
            byte[] entrada = Encoding.ASCII.GetBytes(_senha);
            byte[] hash = md5.ComputeHash(entrada);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                senha.Append(hash[i].ToString("X2"));
            }
            return senha.ToString();
        }

        private void Cadastro()
        {
            if (Editar)
            {
                string sql = String.Format(@"UPDATE LOGIN SET NOME = '{0}', USUARIO = '{1}', SENHA = '{2}', DATAINCLUSAO = GETDATE() WHERE ID = '{3}'", txtNome.Text, txtUsuario.Text, CriptografaSenha(txtSenha.Text), Cod);
                MetodosSql.ExecQuery(sql);
            }
            else
            {
                string sql = String.Format(@"INSERT INTO LOGIN VALUES('{0}', '{1}', '{2}', getdate())", txtNome.Text, txtUsuario.Text, CriptografaSenha(txtSenha.Text));
                MetodosSql.ExecQuery(sql);
                Editar = true;
            }

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                Cadastro();
                this.Close();
            }
        }

        private void frmCadastroUsuario_Load(object sender, EventArgs e)
        {
            if (Editar)
            {
                string sql = String.Format(@"SELECT * FROM LOGIN WHERE ID = '{0}'", Cod);
                txtNome.Text = MetodosSql.GetField(sql, "NOME");
                txtUsuario.Text = MetodosSql.GetField(sql, "USUARIO");
                //txtSenha.Text = MetodosSql.GetField(sql, "SENHA");
                //txtConfirmarSenha.Text = MetodosSql.GetField(sql, "SENHA");
                txtData.Text = MetodosSql.GetField(String.Format("SELECT CONVERT(VARCHAR, CONVERT(DATETIME, DATAINCLUSAO, 121), 103) AS DATAINCLUSAO FROM LOGIN WHERE ID = '{0}'", Cod), "DATAINCLUSAO");
                txtCod.Text = Cod;
            }

        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                Cadastro();
            }
        }
    }
}
