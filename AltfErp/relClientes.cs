using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AltfErp
{
    public partial class relClientes : DevExpress.XtraReports.UI.XtraReport
    {
        public relClientes(string cod)
        {
            InitializeComponent();
            sqlDataSource1.Connection.ConnectionString = MetodosSql.GetConnectionString();
            sqlDataSource1.Queries[1].Parameters[0].Value = cod; 
        }

    }
}
            
