﻿using System;
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
    public partial class frmVisualizaPdf : Form
    {
        public frmVisualizaPdf(string fileName)
        {
            InitializeComponent();
            pdfViewer.LoadDocument(fileName);
        }
    }
}
