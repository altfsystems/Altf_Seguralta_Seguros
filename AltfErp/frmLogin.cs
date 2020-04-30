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
        }
        
        private void btnLogin_Click(object sender, EventArgs e)
        {
            valida = MetodosSql.ChecaLogin(txtLogin.Text, txtSenha.Text);
            
           if(valida)
           {
                string sql = String.Format(@"SELECT NOME, USUARIO FROM LOGIN WHERE USUARIO = '{0}'", txtLogin.Text);
                string nome = MetodosSql.GetField(sql, "NOME");
                string usuario = MetodosSql.GetField(sql, "USUARIO");

                frmPrincipal frm = new frmPrincipal(nome, usuario);
                frm.FormClosed += (s, args) => this.Close();
                frm.Show();
                this.Visible = false;


           }
           else
            {
                MessageBox.Show("Úsuaio ou Senha inválidos"); 
            }
        }
    }
}
