namespace AltfErp
{
    partial class frmEdiçãoParcela
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEdiçãoParcela));
            this.btnReceber = new DevExpress.XtraEditors.SimpleButton();
            this.txtValorDinheiro = new System.Windows.Forms.TextBox();
            this.txtValorCredito = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtValorCheque = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtValorDebito = new System.Windows.Forms.TextBox();
            this.txtValorRestante = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValorParcela = new System.Windows.Forms.TextBox();
            this.txtCodigoParcela = new System.Windows.Forms.TextBox();
            this.txtCodigoVenda = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReceber
            // 
            this.btnReceber.Location = new System.Drawing.Point(202, 298);
            this.btnReceber.Margin = new System.Windows.Forms.Padding(2);
            this.btnReceber.Name = "btnReceber";
            this.btnReceber.Size = new System.Drawing.Size(128, 32);
            this.btnReceber.TabIndex = 1005;
            this.btnReceber.Text = " Receber";
            this.btnReceber.Click += new System.EventHandler(this.btnReceber_Click);
            // 
            // txtValorDinheiro
            // 
            this.txtValorDinheiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorDinheiro.Location = new System.Drawing.Point(7, 197);
            this.txtValorDinheiro.Margin = new System.Windows.Forms.Padding(2);
            this.txtValorDinheiro.Name = "txtValorDinheiro";
            this.txtValorDinheiro.Size = new System.Drawing.Size(180, 23);
            this.txtValorDinheiro.TabIndex = 3;
            // 
            // txtValorCredito
            // 
            this.txtValorCredito.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorCredito.Location = new System.Drawing.Point(7, 117);
            this.txtValorCredito.Margin = new System.Windows.Forms.Padding(2);
            this.txtValorCredito.Name = "txtValorCredito";
            this.txtValorCredito.Size = new System.Drawing.Size(180, 23);
            this.txtValorCredito.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 61);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Valor Cheque";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtValorDinheiro);
            this.groupBox1.Controls.Add(this.txtValorCredito);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtValorCheque);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtValorDebito);
            this.groupBox1.Controls.Add(this.txtValorRestante);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(11, 62);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(319, 232);
            this.groupBox1.TabIndex = 1012;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pagamento";
            // 
            // txtValorCheque
            // 
            this.txtValorCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorCheque.Location = new System.Drawing.Point(7, 77);
            this.txtValorCheque.Margin = new System.Windows.Forms.Padding(2);
            this.txtValorCheque.Name = "txtValorCheque";
            this.txtValorCheque.Size = new System.Drawing.Size(180, 23);
            this.txtValorCheque.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 101);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(116, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Valor Cartão de Crédito";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 21);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "Valor Restante";
            // 
            // txtValorDebito
            // 
            this.txtValorDebito.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorDebito.Location = new System.Drawing.Point(7, 157);
            this.txtValorDebito.Margin = new System.Windows.Forms.Padding(2);
            this.txtValorDebito.Name = "txtValorDebito";
            this.txtValorDebito.Size = new System.Drawing.Size(180, 23);
            this.txtValorDebito.TabIndex = 2;
            // 
            // txtValorRestante
            // 
            this.txtValorRestante.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorRestante.Location = new System.Drawing.Point(7, 37);
            this.txtValorRestante.Margin = new System.Windows.Forms.Padding(2);
            this.txtValorRestante.Name = "txtValorRestante";
            this.txtValorRestante.ReadOnly = true;
            this.txtValorRestante.Size = new System.Drawing.Size(180, 23);
            this.txtValorRestante.TabIndex = 12;
            this.txtValorRestante.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 141);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(114, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Valor Cartão de Débito";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 180);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Valor em Dinheiro";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 10);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(74, 13);
            this.label8.TabIndex = 1010;
            this.label8.Text = "Código Venda";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(119, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 13);
            this.label1.TabIndex = 1003;
            this.label1.Text = "Código Parcela";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(202, 10);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 1004;
            this.label2.Text = "Valor Parcela";
            // 
            // txtValorParcela
            // 
            this.txtValorParcela.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorParcela.Location = new System.Drawing.Point(202, 26);
            this.txtValorParcela.Margin = new System.Windows.Forms.Padding(2);
            this.txtValorParcela.Name = "txtValorParcela";
            this.txtValorParcela.ReadOnly = true;
            this.txtValorParcela.Size = new System.Drawing.Size(102, 23);
            this.txtValorParcela.TabIndex = 1008;
            this.txtValorParcela.TabStop = false;
            // 
            // txtCodigoParcela
            // 
            this.txtCodigoParcela.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoParcela.Location = new System.Drawing.Point(119, 26);
            this.txtCodigoParcela.Margin = new System.Windows.Forms.Padding(2);
            this.txtCodigoParcela.Name = "txtCodigoParcela";
            this.txtCodigoParcela.ReadOnly = true;
            this.txtCodigoParcela.Size = new System.Drawing.Size(79, 23);
            this.txtCodigoParcela.TabIndex = 1007;
            this.txtCodigoParcela.TabStop = false;
            // 
            // txtCodigoVenda
            // 
            this.txtCodigoVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoVenda.Location = new System.Drawing.Point(11, 26);
            this.txtCodigoVenda.Margin = new System.Windows.Forms.Padding(2);
            this.txtCodigoVenda.Name = "txtCodigoVenda";
            this.txtCodigoVenda.ReadOnly = true;
            this.txtCodigoVenda.Size = new System.Drawing.Size(104, 23);
            this.txtCodigoVenda.TabIndex = 1006;
            this.txtCodigoVenda.TabStop = false;
            // 
            // frmEdiçãoParcela
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(346, 341);
            this.Controls.Add(this.btnReceber);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtValorParcela);
            this.Controls.Add(this.txtCodigoParcela);
            this.Controls.Add(this.txtCodigoVenda);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEdiçãoParcela";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editar Parcela";
            this.Load += new System.EventHandler(this.frmEdiçãoParcela_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnReceber;
        private System.Windows.Forms.TextBox txtValorDinheiro;
        private System.Windows.Forms.TextBox txtValorCredito;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtValorCheque;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtValorDebito;
        public System.Windows.Forms.TextBox txtValorRestante;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValorParcela;
        private System.Windows.Forms.TextBox txtCodigoParcela;
        public System.Windows.Forms.TextBox txtCodigoVenda;
    }
}