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
    public partial class frmFiltroVenda : Form
    {
        public string RETORNO { get; set; }
        public string RETORNODATA { get; set; }
        public string NULO { get; set; }

        public frmFiltroVenda()
        {
            InitializeComponent();
        }

        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if(rbtnTodos.Checked == true)
            {
                RETORNO = "1=1 --";
            }
            else
            {
                if (rbtnEmAberto.Checked == true)
                {
                    RETORNO = "ROUND(((IT.QUANTIDADE * IT.VALOR - VD.DESCONTO) - ROUND(X.TOTAL_RECEBIMENTO, -1)), -1) > 0 or ROUND(X.TOTAL_RECEBIMENTO, -1) is null--";
                }
                else
                {
                    if (rbtnPagos.Checked == true)
                    {
                        RETORNO = "ROUND(cast((IT.VALOR * IT.QUANTIDADE) - VD.DESCONTO as numeric(20,2)) - ROUND(X.TOTAL_RECEBIMENTO, -1), -1) <= 0--";
                    }
                    else
                    {
                        if (rbtnData.Checked == true)
                        {
                            RETORNODATA = txtData.Text;
                        }
                        else
                        {
                            if(rbtnNome.Checked == true)
                            {
                                RETORNO = "FC.NOME = '" +txtNome.Text+ "' --";
                            }
                            else
                            {
                              NULO = "VD.DATAINCLUSAO --";
                            }

                        }
                    }
                 
                }
             


            }
           
            
           
          

            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnData_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnData.Checked == true)
            {
                txtData.Enabled = true;
            }
            else
            {
                txtData.Enabled = false;
            }

        }

        private void rbtnNome_CheckedChanged(object sender, EventArgs e)
        {
            if(rbtnNome.Checked == true)
            {
                txtNome.Visible = true;
                lblNome.Visible = true;
            }
            else
            {
                txtNome.Visible = false;
                lblNome.Visible = false;
            }
        }

        private void frmFiltroVenda_Load(object sender, EventArgs e)
        {
            rbtnTodos.Checked = true;
        }
    }
}
