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
    public partial class frmCadastroPorcentagens : Form
    {
        public frmCadastroPorcentagens()
        {
            InitializeComponent();
        }

        private void frmCadastroPorcentagens_Load(object sender, EventArgs e)
        {
            string sql = String.Format(@"SELECT * FROM PORCENTAGENS");
            txtPorcentagemNota.Text = MetodosSql.GetField(sql, "PERCIMPOSTONOTA");
            txtPorcentagemCorretora.Text = MetodosSql.GetField(sql, "PERCSEGURALTA");
            txtCod.Text = MetodosSql.GetField(sql, "IDPORCENTAGEM");
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = String.Format(@"UPDATE PORCENTAGENS SET PERCIMPOSTONOTA = '{0}', PERCSEGURALTA = '{1}' WHERE IDPORCENTAGEM = '{2}'",
                /*{0}*/    txtPorcentagemNota.Text.Replace(".", "").Replace(",", "."),
                /*{1}*/    txtPorcentagemCorretora.Text.Replace(".", "").Replace(",", "."),
                /*{2}*/    txtCod.Text);

                MetodosSql.ExecQuery(sql);
                this.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
