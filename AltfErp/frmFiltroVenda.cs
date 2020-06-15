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
                    RETORNO = "(SELECT SUM(VALOR) FROM PARCELA WHERE IDVENDA = VD.IDVENDA) - (X.TOTAL_RECEBIMENTO) > 0 or (X.TOTAL_RECEBIMENTO) is null--";
                }
                else
                {
                    if (rbtnPagos.Checked == true)
                    {
                        RETORNO = "(SELECT SUM(VALOR) FROM PARCELA WHERE IDVENDA = VD.IDVENDA) - (X.TOTAL_RECEBIMENTO) <= 0--";
                    }
                    else
                    {
                        if (rbtnData.Checked == true)
                        {
                            RETORNODATA = "'" + txtDataInicio.Text+ "' AND CONVERT(VARCHAR, CONVERT(DATETIME, VD.DATAINCLUSAO , 121) , 103) <= '" +txtDataFim.Text+ "'";
                        }
                        else
                        {
                            if(rbtnNome.Checked == true)
                            {
                                RETORNO = "FC.NOME LIKE '" +txtNome.Text+ "%' --";
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
                txtDataInicio.Enabled = true;
                txtDataFim.Enabled = true;
            }
            else
            {
                txtDataInicio.Enabled = false;
                txtDataFim.Enabled = false;
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
