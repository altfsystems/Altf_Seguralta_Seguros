using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AltfErp
{
    public partial class relInfoCliente : DevExpress.XtraReports.UI.XtraReport
    {
        public relInfoCliente(string cod)
        {
            InitializeComponent();
            sqlDataSource1.Connection.ConnectionString = MetodosSql.GetConnectionString();
            sqlDataSource1.Queries[0].Parameters[0].Value = cod;
        }

    }
}
