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
    public partial class frmEditarData : Form
    {
        private string Cod;
        public frmEditarData(string cod)
        {
            InitializeComponent();
            Cod = cod;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEditarData_Load(object sender, EventArgs e)
        {
            string sql = String.Format(@"SELECT CONVERT(VARCHAR, DATAPAGAMENTO, 103) AS DATAPAGAMENTO FROM PARCELA WHERE IDPARCELA = '{0}'", Cod);
            txtDataVencimentoParcela.Text = MetodosSql.GetField(sql, "DATAPAGAMENTO");
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Format(@"UPDATE PARCELA SET DATAPAGAMENTO = CONVERT(DATETIME, CONVERT(VARCHAR, '{0}', 121), 103) WHERE IDPARCELA = '{1}'", txtDataVencimentoParcela.Text, Cod);
                MetodosSql.ExecQuery(sql);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
