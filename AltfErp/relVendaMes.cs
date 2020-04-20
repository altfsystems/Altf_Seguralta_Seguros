using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace AltfErp
{
    public partial class relVendaMes : DevExpress.XtraReports.UI.XtraReport
    {
        public relVendaMes(string Inicio, string Fim)
        {
            InitializeComponent();
            sqlDataSource1.Queries[0].Parameters[0].Value = Inicio;
            sqlDataSource1.Queries[0].Parameters[1].Value = Fim;
        }

    }
}
