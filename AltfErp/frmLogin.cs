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
    public partial class frmLogin : Form
    {
        Boolean valida;
        public frmLogin()
        {
            InitializeComponent();
            string data = DateTime.Now.ToString();
            string[] vet = data.Split('/', ' ');
            string dia, mes, ano;
           
            dia = vet[0];
            mes = vet[1];
            ano = vet[2];
            lblVersao.Text = "Version "+ano + "." + mes + "." + dia + ".00032";

            SetUser();
            if(!String.IsNullOrWhiteSpace(txtLogin.Text))
            {
                txtSenha.Focus();
                txtSenha.Select();
            }

        }

        private void Login()
        {
            toolTip1.ShowAlways = false;

            valida = MetodosSql.ChecaLogin(txtLogin.Text, txtSenha.Text);

            if (valida)
            {
                string sql = String.Format(@"SELECT NOME, USUARIO FROM LOGIN WHERE USUARIO = '{0}'", txtLogin.Text);
                string nome = MetodosSql.GetField(sql, "NOME");
                string usuario = MetodosSql.GetField(sql, "USUARIO");

                frmPrincipal frm = new frmPrincipal(nome, usuario);
                frm.FormClosed += (s, args) => this.Close();
                frm.bhiVersao.Caption = lblVersao.Text;
                frm.Show();
                this.Visible = false;


            }
            else
            {
                MessageBox.Show("Usuário ou Senha inválidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Text = string.Empty;
            }
            SaveUser(txtLogin.Text);
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                Login();
            }
        }
                

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13)
            {
                Login();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            toolTip1.ShowAlways = false;
            this.Close();
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            toolTip1.Show("Contatos:\nFabio Coquieri...19997077758\nGabriel Aceti......19992743066 ", pictureBox1);
            
        }

        private  void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            toolTip1.RemoveAll();
        }

        private void SaveUser(string _usuario)
        {
            Properties.Settings.Default.User = _usuario;
            Properties.Settings.Default.Save();
        }

        private void SetUser()
        {
            txtLogin.Text = Properties.Settings.Default.User;
        }
    }
}
