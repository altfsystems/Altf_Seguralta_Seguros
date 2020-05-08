namespace AltfErp
{
    partial class frmVisaoRelCliente
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisaoRelCliente));
            this.grdVisaoRelCliente = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRelatorioTodos = new DevExpress.XtraEditors.SimpleButton();
            this.btnInformacoesCliente = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdVisaoRelCliente)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdVisaoRelCliente
            // 
            this.grdVisaoRelCliente.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdVisaoRelCliente.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdVisaoRelCliente.Location = new System.Drawing.Point(2, 48);
            this.grdVisaoRelCliente.MainView = this.gridView1;
            this.grdVisaoRelCliente.Margin = new System.Windows.Forms.Padding(2);
            this.grdVisaoRelCliente.Name = "grdVisaoRelCliente";
            this.grdVisaoRelCliente.Size = new System.Drawing.Size(799, 401);
            this.grdVisaoRelCliente.TabIndex = 14;
            this.grdVisaoRelCliente.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 284;
            this.gridView1.GridControl = this.grdVisaoRelCliente;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 46);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 46);
            // 
            // btnRelatorioTodos
            // 
            this.btnRelatorioTodos.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.btnRelatorioTodos.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.Full;
            this.btnRelatorioTodos.Location = new System.Drawing.Point(169, 9);
            this.btnRelatorioTodos.Name = "btnRelatorioTodos";
            this.btnRelatorioTodos.Size = new System.Drawing.Size(127, 31);
            this.btnRelatorioTodos.TabIndex = 16;
            this.btnRelatorioTodos.Text = "Relatório Todos";
            this.btnRelatorioTodos.Click += new System.EventHandler(this.btnRelatorioTodos_Click);
            // 
            // btnInformacoesCliente
            // 
            this.btnInformacoesCliente.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage1")));
            this.btnInformacoesCliente.ImageOptions.SvgImageColorizationMode = DevExpress.Utils.SvgImageColorizationMode.Full;
            this.btnInformacoesCliente.Location = new System.Drawing.Point(21, 9);
            this.btnInformacoesCliente.Name = "btnInformacoesCliente";
            this.btnInformacoesCliente.Size = new System.Drawing.Size(142, 31);
            this.btnInformacoesCliente.TabIndex = 17;
            this.btnInformacoesCliente.Text = "Informações Cliente";
            this.btnInformacoesCliente.Click += new System.EventHandler(this.btnInformacoesCliente_Click_1);
            // 
            // frmVisaoRelCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnInformacoesCliente);
            this.Controls.Add(this.btnRelatorioTodos);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grdVisaoRelCliente);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVisaoRelCliente";
            this.Text = "Visão Clientes";
            ((System.ComponentModel.ISupportInitialize)(this.grdVisaoRelCliente)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdVisaoRelCliente;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private DevExpress.XtraEditors.SimpleButton btnRelatorioTodos;
        private DevExpress.XtraEditors.SimpleButton btnInformacoesCliente;
    }
}