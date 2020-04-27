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
    public partial class frmFiltroDataVencimento : Form
    {
       public string DataInicio, DataFim, todos, vencidos
            ;
        public frmFiltroDataVencimento()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnTodos_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnPeriodo.Checked == true)
            {
                txtDthInicio.Enabled = true;
                txtDthFim.Enabled = true;
            }
            else
            {
                txtDthInicio.Enabled = false;
                txtDthFim.Enabled = false;
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            if(rbtnTodos.Checked == true)
            {
                todos = "1=1 --";
            }
            else if(rbtnVencidos.Checked == true)
            {

                vencidos = "DATAVENCIMENTO <= GETDATE() --";
            }
            else
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
                    DataInicio = txtDthInicio.DateTime.ToString("dd/MM/yyyy");
                    DataFim = txtDthFim.DateTime.ToString("dd/MM/yyyy");
                    
                }
            }

            this.Close();

        }
    }
}
