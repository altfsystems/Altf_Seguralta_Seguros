namespace AltfErp
{
    partial class frmFiltroVenda
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFiltroVenda));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.txtNome = new System.Windows.Forms.TextBox();
            this.rbtnNome = new System.Windows.Forms.RadioButton();
            this.txtData = new System.Windows.Forms.DateTimePicker();
            this.rbtnData = new System.Windows.Forms.RadioButton();
            this.rbtnEmAberto = new System.Windows.Forms.RadioButton();
            this.rbtnPagos = new System.Windows.Forms.RadioButton();
            this.rbtnTodos = new System.Windows.Forms.RadioButton();
            this.btnExecutar = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancelar = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.lblNome);
            this.groupBox1.Controls.Add(this.txtNome);
            this.groupBox1.Controls.Add(this.rbtnNome);
            this.groupBox1.Controls.Add(this.txtData);
            this.groupBox1.Controls.Add(this.rbtnData);
            this.groupBox1.Controls.Add(this.rbtnEmAberto);
            this.groupBox1.Controls.Add(this.rbtnPagos);
            this.groupBox1.Controls.Add(this.rbtnTodos);
            this.groupBox1.Location = new System.Drawing.Point(9, 8);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(302, 210);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Filtro";
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Location = new System.Drawing.Point(12, 162);
            this.lblNome.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(82, 13);
            this.lblNome.TabIndex = 1010;
            this.lblNome.Text = "Digite um Nome";
            this.lblNome.Visible = false;
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(12, 178);
            this.txtNome.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(246, 20);
            this.txtNome.TabIndex = 1009;
            this.txtNome.Visible = false;
            // 
            // rbtnNome
            // 
            this.rbtnNome.AutoSize = true;
            this.rbtnNome.Location = new System.Drawing.Point(12, 139);
            this.rbtnNome.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtnNome.Name = "rbtnNome";
            this.rbtnNome.Size = new System.Drawing.Size(53, 17);
            this.rbtnNome.TabIndex = 1008;
            this.rbtnNome.TabStop = true;
            this.rbtnNome.Text = "Nome";
            this.rbtnNome.UseVisualStyleBackColor = true;
            this.rbtnNome.CheckedChanged += new System.EventHandler(this.rbtnNome_CheckedChanged);
            // 
            // txtData
            // 
            this.txtData.Enabled = false;
            this.txtData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtData.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtData.Location = new System.Drawing.Point(12, 115);
            this.txtData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(113, 22);
            this.txtData.TabIndex = 1007;
            // 
            // rbtnData
            // 
            this.rbtnData.AutoSize = true;
            this.rbtnData.Location = new System.Drawing.Point(12, 93);
            this.rbtnData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtnData.Name = "rbtnData";
            this.rbtnData.Size = new System.Drawing.Size(48, 17);
            this.rbtnData.TabIndex = 1006;
            this.rbtnData.TabStop = true;
            this.rbtnData.Text = "Data";
            this.rbtnData.UseVisualStyleBackColor = true;
            this.rbtnData.CheckedChanged += new System.EventHandler(this.rbtnData_CheckedChanged);
            // 
            // rbtnEmAberto
            // 
            this.rbtnEmAberto.AutoSize = true;
            this.rbtnEmAberto.Location = new System.Drawing.Point(12, 72);
            this.rbtnEmAberto.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtnEmAberto.Name = "rbtnEmAberto";
            this.rbtnEmAberto.Size = new System.Drawing.Size(74, 17);
            this.rbtnEmAberto.TabIndex = 5;
            this.rbtnEmAberto.TabStop = true;
            this.rbtnEmAberto.Text = "Em Aberto";
            this.rbtnEmAberto.UseVisualStyleBackColor = true;
            // 
            // rbtnPagos
            // 
            this.rbtnPagos.AutoSize = true;
            this.rbtnPagos.Location = new System.Drawing.Point(12, 50);
            this.rbtnPagos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtnPagos.Name = "rbtnPagos";
            this.rbtnPagos.Size = new System.Drawing.Size(55, 17);
            this.rbtnPagos.TabIndex = 4;
            this.rbtnPagos.TabStop = true;
            this.rbtnPagos.Text = "Pagos";
            this.rbtnPagos.UseVisualStyleBackColor = true;
            // 
            // rbtnTodos
            // 
            this.rbtnTodos.AutoSize = true;
            this.rbtnTodos.Location = new System.Drawing.Point(12, 28);
            this.rbtnTodos.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbtnTodos.Name = "rbtnTodos";
            this.rbtnTodos.Size = new System.Drawing.Size(55, 17);
            this.rbtnTodos.TabIndex = 3;
            this.rbtnTodos.TabStop = true;
            this.rbtnTodos.Text = "Todos";
            this.rbtnTodos.UseVisualStyleBackColor = true;
            // 
            // btnExecutar
            // 
            this.btnExecutar.Location = new System.Drawing.Point(146, 223);
            this.btnExecutar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnExecutar.Name = "btnExecutar";
            this.btnExecutar.Size = new System.Drawing.Size(80, 27);
            this.btnExecutar.TabIndex = 4;
            this.btnExecutar.Text = "Executar";
            this.btnExecutar.Click += new System.EventHandler(this.btnExecutar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Location = new System.Drawing.Point(230, 223);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(80, 27);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // frmFiltroVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 259);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnExecutar);
            this.Controls.Add(this.btnCancelar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "frmFiltroVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Filtrar Vendas";
            this.Load += new System.EventHandler(this.frmFiltroVenda_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbtnEmAberto;
        private System.Windows.Forms.RadioButton rbtnPagos;
        private System.Windows.Forms.RadioButton rbtnTodos;
        private DevExpress.XtraEditors.SimpleButton btnExecutar;
        private DevExpress.XtraEditors.SimpleButton btnCancelar;
        private System.Windows.Forms.RadioButton rbtnData;
        private System.Windows.Forms.DateTimePicker txtData;
        private System.Windows.Forms.RadioButton rbtnNome;
        private System.Windows.Forms.TextBox txtNome;
        private System.Windows.Forms.Label lblNome;
    }
}