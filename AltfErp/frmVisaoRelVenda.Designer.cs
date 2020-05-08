namespace AltfErp
{
    partial class frmVisaoRelVenda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisaoRelVenda));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.grdVisaoRelVendas = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnRelatorioVenda = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdVisaoRelVendas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 46);
            this.toolStrip1.TabIndex = 17;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // grdVisaoRelVendas
            // 
            this.grdVisaoRelVendas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdVisaoRelVendas.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdVisaoRelVendas.Location = new System.Drawing.Point(2, 49);
            this.grdVisaoRelVendas.MainView = this.gridView1;
            this.grdVisaoRelVendas.Margin = new System.Windows.Forms.Padding(2);
            this.grdVisaoRelVendas.Name = "grdVisaoRelVendas";
            this.grdVisaoRelVendas.Size = new System.Drawing.Size(799, 401);
            this.grdVisaoRelVendas.TabIndex = 16;
            this.grdVisaoRelVendas.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 284;
            this.gridView1.GridControl = this.grdVisaoRelVendas;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            // 
            // btnRelatorioVenda
            // 
            this.btnRelatorioVenda.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.btnRelatorioVenda.Location = new System.Drawing.Point(12, 9);
            this.btnRelatorioVenda.Name = "btnRelatorioVenda";
            this.btnRelatorioVenda.Size = new System.Drawing.Size(96, 30);
            this.btnRelatorioVenda.TabIndex = 18;
            this.btnRelatorioVenda.Text = "Relatório";
            this.btnRelatorioVenda.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // frmVisaoRelVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnRelatorioVenda);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grdVisaoRelVendas);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVisaoRelVenda";
            this.Text = "Visão Vendas";
            ((System.ComponentModel.ISupportInitialize)(this.grdVisaoRelVendas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevExpress.XtraGrid.GridControl grdVisaoRelVendas;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton btnRelatorioVenda;
    }
}