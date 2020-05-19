using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AltfErp
{
    public partial class relComissoes : DevExpress.XtraReports.UI.XtraReport
    {
        public relComissoes(string Inicio, string Fim)
        {
            InitializeComponent();
            sqlDataSource1.Connection.ConnectionString = MetodosSql.GetConnectionString();
            sqlDataSource1.Queries[0].Parameters[0].Value = Inicio;
            sqlDataSource1.Queries[0].Parameters[1].Value = Fim;
        }

    }
}
