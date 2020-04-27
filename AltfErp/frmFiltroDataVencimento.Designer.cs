namespace AltfErp
{
    partial class frmFiltroDataVencimento
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFiltroDataVencimento));
            this.txtDthInicio = new DevExpress.XtraEditors.DateEdit();
            this.txtDthFim = new DevExpress.XtraEditors.DateEdit();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.btnFiltrar = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rbtnPeriodo = new System.Windows.Forms.RadioButton();
            this.rbtnTodos = new System.Windows.Forms.RadioButton();
            this.rbtnVencidos = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthInicio.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthInicio.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthFim.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthFim.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // txtDthInicio
            // 
            this.txtDthInicio.EditValue = null;
            this.txtDthInicio.Enabled = false;
            this.txtDthInicio.Location = new System.Drawing.Point(43, 122);
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
            this.txtDthInicio.TabIndex = 8;
            // 
            // txtDthFim
            // 
            this.txtDthFim.EditValue = null;
            this.txtDthFim.Enabled = false;
            this.txtDthFim.Location = new System.Drawing.Point(180, 122);
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
            this.txtDthFim.TabIndex = 9;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(210, 162);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(72, 25);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.Location = new System.Drawing.Point(132, 162);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(72, 25);
            this.btnFiltrar.TabIndex = 12;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Inicio";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(151, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Fim";
            // 
            // rbtnPeriodo
            // 
            this.rbtnPeriodo.AutoSize = true;
            this.rbtnPeriodo.Location = new System.Drawing.Point(13, 100);
            this.rbtnPeriodo.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnPeriodo.Name = "rbtnPeriodo";
            this.rbtnPeriodo.Size = new System.Drawing.Size(63, 17);
            this.rbtnPeriodo.TabIndex = 15;
            this.rbtnPeriodo.TabStop = true;
            this.rbtnPeriodo.Text = "Período";
            this.rbtnPeriodo.UseVisualStyleBackColor = true;
            this.rbtnPeriodo.CheckedChanged += new System.EventHandler(this.rbtnTodos_CheckedChanged);
            // 
            // rbtnTodos
            // 
            this.rbtnTodos.AutoSize = true;
            this.rbtnTodos.Location = new System.Drawing.Point(13, 22);
            this.rbtnTodos.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnTodos.Name = "rbtnTodos";
            this.rbtnTodos.Size = new System.Drawing.Size(55, 17);
            this.rbtnTodos.TabIndex = 16;
            this.rbtnTodos.TabStop = true;
            this.rbtnTodos.Text = "Todos";
            this.rbtnTodos.UseVisualStyleBackColor = true;
            // 
            // rbtnVencidos
            // 
            this.rbtnVencidos.AutoSize = true;
            this.rbtnVencidos.Location = new System.Drawing.Point(13, 61);
            this.rbtnVencidos.Margin = new System.Windows.Forms.Padding(2);
            this.rbtnVencidos.Name = "rbtnVencidos";
            this.rbtnVencidos.Size = new System.Drawing.Size(69, 17);
            this.rbtnVencidos.TabIndex = 17;
            this.rbtnVencidos.TabStop = true;
            this.rbtnVencidos.Text = "Vencidos";
            this.rbtnVencidos.UseVisualStyleBackColor = true;
            // 
            // frmFiltroDataVencimento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 199);
            this.Controls.Add(this.rbtnVencidos);
            this.Controls.Add(this.rbtnTodos);
            this.Controls.Add(this.rbtnPeriodo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.txtDthFim);
            this.Controls.Add(this.txtDthInicio);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmFiltroDataVencimento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtro";
            ((System.ComponentModel.ISupportInitialize)(this.txtDthInicio.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthInicio.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthFim.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDthFim.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.DateEdit txtDthInicio;
        private DevExpress.XtraEditors.DateEdit txtDthFim;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private DevExpress.XtraEditors.SimpleButton btnFiltrar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbtnPeriodo;
        private System.Windows.Forms.RadioButton rbtnTodos;
        private System.Windows.Forms.RadioButton rbtnVencidos;
    }
}