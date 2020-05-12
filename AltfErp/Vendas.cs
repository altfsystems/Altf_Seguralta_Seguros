using DevExpress.XtraRichEdit.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace AltfErp
{
    public class Vendas
    {
        List<Produto> produtos = new List<Produto>();
        private string DATAINCLUSAO { get; set; }
        private string IDVENDEDOR { get; set; }
        private string NOMEVENDEDOR { get; set; }
        private string SOBRENOMEVENDEDOR { get; set; }
        private string DATAVENCIMENTO { get; set; }
        private string STATUS { get; set; }
        private string IDCLIENTE { get; set; }
        private string NOMECLIENTE { get; set; }
        private string SOBRENOMECLIENTE { get; set; }
        private string IDPRODUTO { get; set; }
        private string DESCPRODUTO { get; set; }
        private string CIASEGURADORA { get; set; }
        private string VALORLIQUIDO { get; set; }
        private string IOF { get; set; }
        private string VALORTOTAL { get; set; }
        private string COMISSAO { get; set; }
        private string COMISSAOVENDA { get; set; }
        public string IDTIPOPAGAMENTO { get; set; }
        private string DESCTIPOPAG { get; set; }
        private string DESCONTO { get; set; }
        private string TOTALVENDA { get; set; }
        private string TOTALCOMDESCONTO { get; set; }
        private string OBSERVACAO { get; set; }
        private string CODIGO { get; set; }
        private string IDORDEM { get; set; }
        private string DATA1 { get; set; }


        public class Produto
        {
            public String IDPRODUTO { get; set; }
            public String DESCRICAO { get; set; }
            public String CIASEGURADORA { get; set; }
            public String OBSERVACAO { get; set; }
            //public String PRECOUNITARIO { get; set; }
            //public String QUANTIDADE { get; set; }
            //public String VALORTOTAL { get; set; }
            //public String CODPARCELA { get; set; }
        }

       



       
    }
}
