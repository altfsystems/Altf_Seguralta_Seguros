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

        private void Cadastro()
        {
            if(Editar)
            {
                string sql = String.Format(@"UPDATE LOGIN SET NOME = '{0}', USUARIO = '{1}', SENHA = '{2}'", txtNome.Text, txtUsuario.Text, txtSenha.Text);
                MetodosSql.ExecQuery(sql);
            }
            else
            {
                string sql = String.Format(@"INSERT INTO LOGIN VALUES('{0}', '{1}', '{2}', getdate())", txtNome.Text, txtUsuario.Text, txtSenha.Text);
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
            if(txtSenha.Text == txtConfirmarSenha.Text)
            {
                Cadastro();
                this.Close();
            }
            else
            {
                MessageBox.Show("As senhas não batem!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void frmCadastroUsuario_Load(object sender, EventArgs e)
        {
            if(Editar)
            {
                string sql = String.Format(@"SELECT * FROM LOGIN WHERE ID = '{0}'", Cod);
                txtNome.Text = MetodosSql.GetField(sql, "NOME");
                txtUsuario.Text = MetodosSql.GetField(sql, "USUARIO");
                txtSenha.Text = MetodosSql.GetField(sql, "SENHA");
                txtConfirmarSenha.Text = MetodosSql.GetField(sql, "SENHA");
                txtCod.Text = Cod;
            }
            
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Cadastro();
        }
    }
}
