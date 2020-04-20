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
    public partial class frmFiltroServico : Form
    {

        public string RETORNO { get; set; }
        public string RETORNONOME { get; set; }
        public string RETORNODATA { get; set; }
        public string NULO { get; set; }
        public frmFiltroServico()
        {
            InitializeComponent();
            
        }


        
        
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void btnExecutar_Click(object sender, EventArgs e)
        {
            if(rbtnTodos.Checked == true)
            {
                RETORNO = "ORDEM.STATUS --";
            }
            else
            {
                if (rbtnFaturados.Checked == true)
                {
                    RETORNO = "'F'--";
                }
                else
                {
                    if (rbtnEmAberto.Checked == true)
                    {
                        RETORNO = "'A'--";
                    }
                    else
                    {
                        if (rbtnData.Checked == true)
                        {

                            RETORNODATA = txtData.Text;
                            RETORNO = ("ORDEM.STATUS");
                        }
                        else
                        {
                            if (rbtnNome.Checked == true)
                            {
                                RETORNONOME = "AND FCFO.NOME = '" + txtNome.Text + "' --";
                                RETORNO = "ORDEM.STATUS ";
                            }
                            else
                            {

                             NULO = "ORDEM.DATAINCLUSAO --";

                            }


                        }
                    }

                    
                }
                
            }
           

        


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

        private void frmFiltroServico_Load(object sender, EventArgs e)
        {
            rbtnTodos.Checked = true;
        }
    }
}
