namespace AltfErp
{
    partial class frmFiltroComissoes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFiltroComissoes));
            this.label2 = new System.Windows.Forms.Label();
            this.txtDthFim = new DevExpress.XtraEditors.DateEdit();
            this.txtDthInicio = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.btnFiltrar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthFim.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthFim.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthInicio.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthInicio.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(56, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 18;
            this.label2.Text = "Data Inicio";
            // 
            // txtDthFim
            // 
            this.txtDthFim.EditValue = null;
            this.txtDthFim.Location = new System.Drawing.Point(59, 93);
            this.txtDthFim.Name = "txtDthFim";
            this.txtDthFim.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDthFim.Properties.Appearance.Options.UseFont = true;
            this.txtDthFim.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDthFim.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDthFim.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic;
            this.txtDthFim.Properties.DisplayFormat.FormatString = "";
            this.txtDthFim.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDthFim.Properties.EditFormat.FormatString = "";
            this.txtDthFim.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDthFim.Properties.Mask.EditMask = "99-99-0000";
            this.txtDthFim.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtDthFim.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtDthFim.Size = new System.Drawing.Size(102, 24);
            this.txtDthFim.TabIndex = 17;
            // 
            // txtDthInicio
            // 
            this.txtDthInicio.EditValue = null;
            this.txtDthInicio.Location = new System.Drawing.Point(59, 50);
            this.txtDthInicio.Name = "txtDthInicio";
            this.txtDthInicio.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDthInicio.Properties.Appearance.Options.UseFont = true;
            this.txtDthInicio.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDthInicio.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtDthInicio.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.Classic;
            this.txtDthInicio.Properties.DisplayFormat.FormatString = "";
            this.txtDthInicio.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDthInicio.Properties.EditFormat.FormatString = "";
            this.txtDthInicio.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.txtDthInicio.Properties.Mask.EditMask = "99-99-0000";
            this.txtDthInicio.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.txtDthInicio.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.False;
            this.txtDthInicio.Size = new System.Drawing.Size(102, 24);
            this.txtDthInicio.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 19;
            this.label1.Text = "Data Fim";
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(59, 160);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(72, 25);
            this.btnFiltrar.TabIndex = 22;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(137, 160);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 25);
            this.btnCancelar.TabIndex = 21;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmFiltroComissoes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(221, 197);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDthFim);
            this.Controls.Add(this.txtDthInicio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFiltroComissoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            ((System.ComponentModel.ISupportInitialize)(this.txtDthFim.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthFim.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthInicio.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthInicio.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit txtDthFim;
        private DevExpress.XtraEditors.DateEdit txtDthInicio;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnFiltrar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
    }
}