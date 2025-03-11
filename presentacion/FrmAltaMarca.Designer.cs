namespace presentacion
{
    partial class FrmAltaMarca
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
            this.gbxMarcaAlta = new System.Windows.Forms.GroupBox();
            this.lblDescripcionMarca = new System.Windows.Forms.Label();
            this.btnCancelarMarca = new System.Windows.Forms.Button();
            this.txtDescripcionMarca = new System.Windows.Forms.TextBox();
            this.btnAceptarMarca = new System.Windows.Forms.Button();
            this.gbxMarcaAlta.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbxMarcaAlta
            // 
            this.gbxMarcaAlta.Controls.Add(this.lblDescripcionMarca);
            this.gbxMarcaAlta.Controls.Add(this.btnCancelarMarca);
            this.gbxMarcaAlta.Controls.Add(this.txtDescripcionMarca);
            this.gbxMarcaAlta.Controls.Add(this.btnAceptarMarca);
            this.gbxMarcaAlta.Font = new System.Drawing.Font("Arial Black", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxMarcaAlta.Location = new System.Drawing.Point(49, 25);
            this.gbxMarcaAlta.Name = "gbxMarcaAlta";
            this.gbxMarcaAlta.Size = new System.Drawing.Size(396, 194);
            this.gbxMarcaAlta.TabIndex = 5;
            this.gbxMarcaAlta.TabStop = false;
            this.gbxMarcaAlta.Text = "Datos de Marca";
            // 
            // lblDescripcionMarca
            // 
            this.lblDescripcionMarca.AutoSize = true;
            this.lblDescripcionMarca.Location = new System.Drawing.Point(39, 39);
            this.lblDescripcionMarca.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescripcionMarca.Name = "lblDescripcionMarca";
            this.lblDescripcionMarca.Size = new System.Drawing.Size(129, 26);
            this.lblDescripcionMarca.TabIndex = 0;
            this.lblDescripcionMarca.Text = "Descripción";
            // 
            // btnCancelarMarca
            // 
            this.btnCancelarMarca.Location = new System.Drawing.Point(198, 98);
            this.btnCancelarMarca.Name = "btnCancelarMarca";
            this.btnCancelarMarca.Size = new System.Drawing.Size(148, 45);
            this.btnCancelarMarca.TabIndex = 3;
            this.btnCancelarMarca.Text = "Cancelar";
            this.btnCancelarMarca.UseVisualStyleBackColor = true;
            this.btnCancelarMarca.Click += new System.EventHandler(this.btnCancelarMarca_Click);
            // 
            // txtDescripcionMarca
            // 
            this.txtDescripcionMarca.Location = new System.Drawing.Point(198, 33);
            this.txtDescripcionMarca.Margin = new System.Windows.Forms.Padding(4);
            this.txtDescripcionMarca.Name = "txtDescripcionMarca";
            this.txtDescripcionMarca.Size = new System.Drawing.Size(148, 33);
            this.txtDescripcionMarca.TabIndex = 1;
            // 
            // btnAceptarMarca
            // 
            this.btnAceptarMarca.Location = new System.Drawing.Point(55, 98);
            this.btnAceptarMarca.Name = "btnAceptarMarca";
            this.btnAceptarMarca.Size = new System.Drawing.Size(128, 45);
            this.btnAceptarMarca.TabIndex = 2;
            this.btnAceptarMarca.Text = "Aceptar";
            this.btnAceptarMarca.UseVisualStyleBackColor = true;
            this.btnAceptarMarca.Click += new System.EventHandler(this.btnAceptarMarca_Click);
            // 
            // FrmAltaMarca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 450);
            this.Controls.Add(this.gbxMarcaAlta);
            this.Name = "FrmAltaMarca";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alta de Marca";
            this.gbxMarcaAlta.ResumeLayout(false);
            this.gbxMarcaAlta.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbxMarcaAlta;
        private System.Windows.Forms.Label lblDescripcionMarca;
        private System.Windows.Forms.Button btnCancelarMarca;
        private System.Windows.Forms.TextBox txtDescripcionMarca;
        private System.Windows.Forms.Button btnAceptarMarca;
    }
}