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
    public partial class frmDataVencimento : Form
    {

        public string dataVencimento { get; set; }

        public frmDataVencimento()
        {
            InitializeComponent();
            txtDataVencimento.Select();
        }

     

        private void btnOk_Click(object sender, EventArgs e)
        {
            string[] vet = txtDataVencimento.Text.Split('/');
            int dia, mes, ano;
            dia = int.Parse(vet[0]);
            mes = int.Parse(vet[1]);
            ano = int.Parse(vet[2]);

            if(dia > 31 || mes > 12 || ano < 2020)
            {
                MessageBox.Show("Por favor, selecione uma data valida", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                dataVencimento = txtDataVencimento.Text;
                this.Close();
            }
            
        }
    }
}
