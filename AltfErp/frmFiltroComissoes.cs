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
    public partial class frmFiltroComissoes : Form
    {
        public string dtInicio, dtFim;
        public frmFiltroComissoes()
        {
            InitializeComponent();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDthInicio.Text))
            {
                MessageBox.Show("Selecione uma data de inicio.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (String.IsNullOrWhiteSpace(txtDthFim.Text))
            {
                MessageBox.Show("Selecione uma data para o final.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                dtInicio = txtDthInicio.DateTime.ToString("dd/MM/yyyy");
                dtFim = txtDthFim.DateTime.ToString("dd/MM/yyyy");
                this.Close();
            }
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
