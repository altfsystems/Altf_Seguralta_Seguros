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
            
        }

     

        private void btnOk_Click(object sender, EventArgs e)
        {
            
            string dia, mes, ano;
            dia = txtDia.Text;
            mes = txtMes.Text;
            ano = txtAno.Text;

            dataVencimento = dia + "/" + mes + "/" + ano;
            this.Close();
        }
    }
}
           
           
            
            
