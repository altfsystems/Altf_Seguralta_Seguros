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
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           if ((txtLogin.Text == "x" || txtLogin.Text == "1" ) && (txtSenha.Text == "x" || txtSenha.Text == "1"))

            {
                frmPrincipal frm = new frmPrincipal();
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
