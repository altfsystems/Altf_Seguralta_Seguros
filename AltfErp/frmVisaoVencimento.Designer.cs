namespace AltfErp
{
    partial class frmVisaoVencimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVisaoVencimento));
            this.grdVisaoDataVencimento = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnVizu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdVisaoDataVencimento)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // grdVisaoDataVencimento
            // 
            this.grdVisaoDataVencimento.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdVisaoDataVencimento.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.grdVisaoDataVencimento.Location = new System.Drawing.Point(1, 48);
            this.grdVisaoDataVencimento.MainView = this.gridView1;
            this.grdVisaoDataVencimento.Margin = new System.Windows.Forms.Padding(2);
            this.grdVisaoDataVencimento.Name = "grdVisaoDataVencimento";
            this.grdVisaoDataVencimento.Size = new System.Drawing.Size(798, 401);
            this.grdVisaoDataVencimento.TabIndex = 14;
            this.grdVisaoDataVencimento.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.DetailHeight = 284;
            this.gridView1.GridControl = this.grdVisaoDataVencimento;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.BestFitMode = DevExpress.XtraGrid.Views.Grid.GridBestFitMode.Full;
            this.gridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.gridView1.OptionsView.ShowAutoFilterRow = true;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(800, 46);
            this.toolStrip1.TabIndex = 15;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnVizu
            // 
            this.btnVizu.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnVizu.ImageOptions.SvgImage")));
            this.btnVizu.Location = new System.Drawing.Point(12, 8);
            this.btnVizu.Name = "btnVizu";
            this.btnVizu.Size = new System.Drawing.Size(92, 33);
            this.btnVizu.TabIndex = 17;
            this.btnVizu.Text = "Visualizar";
            this.btnVizu.Click += new System.EventHandler(this.btnVizu_Click);
            // 
            // frmVisaoVencimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnVizu);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grdVisaoDataVencimento);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVisaoVencimento";
            this.Text = "Vigência";
            ((System.ComponentModel.ISupportInitialize)(this.grdVisaoDataVencimento)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl grdVisaoDataVencimento;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private DevExpress.XtraEditors.SimpleButton btnVizu;
    }
}