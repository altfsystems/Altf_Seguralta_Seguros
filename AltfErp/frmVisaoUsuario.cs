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
    public partial class frmVisaoUsuario : Form
    {
        public frmVisaoUsuario()
        {
            InitializeComponent();
            gridView1.OptionsBehavior.Editable = false;
            grdVisaoUsuario.EmbeddedNavigator.Buttons.Append.Visible = false;
            grdVisaoUsuario.EmbeddedNavigator.Buttons.Remove.Visible = false;
            AtualizaGrid();
        }

        private void AtualizaGrid()
        {
            grdVisaoUsuario.DataSource = MetodosSql.GetDT(@"SELECT ID, NOME, USUARIO, DATAINCLUSAO FROM LOGIN");
            
        }




        private void grdVisaoUsuario_DoubleClick(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var cod = gridView1.GetRowCellValue(rowHandle, "ID");

            if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir este usuário?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                string sql = String.Format(@"DELETE FROM LOGIN WHERE ID = '{0}'", cod);
                MetodosSql.ExecQuery(sql);
            }
            AtualizaGrid();
        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            frmCadastroUsuario frm = new frmCadastroUsuario(false, null);
            frm.ShowDialog();
            AtualizaGrid();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var cod = gridView1.GetRowCellValue(rowHandle, "ID");
            var user = gridView1.GetRowCellValue(rowHandle, "USUARIO");
            if (cod == null)
            {
                MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            else if (user.ToString() == "mestre")
            {
                MessageBox.Show("Você não pode alterar o usuário mestre!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                frmCadastroUsuario frm = new frmCadastroUsuario(true, cod.ToString());
                frm.ShowDialog();
                AtualizaGrid();
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            var rowHandle = gridView1.FocusedRowHandle;
            var cod = gridView1.GetRowCellValue(rowHandle, "ID");
            var user = gridView1.GetRowCellValue(rowHandle, "USUARIO");

            if (cod == null)
            {
                MessageBox.Show("Por favor, selecione um registro", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if(user.ToString() == "mestre")
            {
                MessageBox.Show("Você não pode excluir o usuário mestre!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (DialogResult.Yes == MessageBox.Show("Tem certeza que deseja excluir este usuário?", "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                string sql = String.Format(@"DELETE FROM LOGIN WHERE ID = '{0}'", cod);
                MetodosSql.ExecQuery(sql);
                AtualizaGrid();
            }
        }
    }
}




