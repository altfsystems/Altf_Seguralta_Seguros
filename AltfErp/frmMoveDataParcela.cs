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
    public partial class frmMoveDataParcela : Form
    {
        public string dataRetorno;
        public frmMoveDataParcela()
        {
            InitializeComponent();
        }

        private void btnMove1_Click(object sender, EventArgs e)
        {
            dataRetorno = "01/03";
            this.Close();
        }

        private void btnMove2_Click(object sender, EventArgs e)
        {
            dataRetorno = "28/02";
            this.Close();
        }
    }
}
