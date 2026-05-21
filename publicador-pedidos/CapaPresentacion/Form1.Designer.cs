namespace CapaPresentacion
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtSku = new System.Windows.Forms.TextBox();
            this.nudCantidad = new System.Windows.Forms.NumericUpDown();
            this.txtTalla = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.btnEnviar = new System.Windows.Forms.Button();
            this.lblEstado = new System.Windows.Forms.Label();

            // Etiquetas visuales para guiar al usuario
            System.Windows.Forms.Label lblSku = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblCantidad = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblTalla = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblColor = new System.Windows.Forms.Label();
            System.Windows.Forms.Label lblTitulo = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).BeginInit();
            this.SuspendLayout();

            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            lblTitulo.Location = new System.Drawing.Point(30, 20);
            lblTitulo.Text = "Publicador de Pedidos - C#";

            // 
            // lblSku
            // 
            lblSku.AutoSize = true;
            lblSku.Location = new System.Drawing.Point(35, 75);
            lblSku.Text = "SKU del Producto:";
            // 
            // txtSku
            // 
            this.txtSku.Location = new System.Drawing.Point(35, 100);
            this.txtSku.Name = "txtSku";
            this.txtSku.Size = new System.Drawing.Size(200, 27);

            // 
            // lblCantidad
            // 
            lblCantidad.AutoSize = true;
            lblCantidad.Location = new System.Drawing.Point(35, 145);
            lblCantidad.Text = "Cantidad:";
            // 
            // nudCantidad
            // 
            this.nudCantidad.Location = new System.Drawing.Point(35, 170);
            this.nudCantidad.Name = "nudCantidad";
            this.nudCantidad.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudCantidad.Size = new System.Drawing.Size(200, 27);

            // 
            // lblTalla
            // 
            lblTalla.AutoSize = true;
            lblTalla.Location = new System.Drawing.Point(35, 215);
            lblTalla.Text = "Talla:";
            // 
            // txtTalla
            // 
            this.txtTalla.Location = new System.Drawing.Point(35, 240);
            this.txtTalla.Name = "txtTalla";
            this.txtTalla.Size = new System.Drawing.Size(200, 27);

            // 
            // lblColor
            // 
            lblColor.AutoSize = true;
            lblColor.Location = new System.Drawing.Point(35, 285);
            lblColor.Text = "Color:";
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(35, 310);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(200, 27);

            // 
            // btnEnviar
            // 
            this.btnEnviar.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnEnviar.ForeColor = System.Drawing.Color.White;
            this.btnEnviar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnEnviar.Location = new System.Drawing.Point(35, 360);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(200, 45);
            this.btnEnviar.Text = "Publicar Pedido";
            this.btnEnviar.UseVisualStyleBackColor = false;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);

            // 
            // lblEstado
            // 
            this.lblEstado.AutoSize = true;
            this.lblEstado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.lblEstado.Location = new System.Drawing.Point(35, 420);
            this.lblEstado.Name = "lblEstado";
            this.lblEstado.Text = "Conectando al bróker CloudAMQP...";
            this.lblEstado.ForeColor = System.Drawing.Color.Orange;

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 470);

            // Agregamos todos los controles al contenedor del formulario
            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblSku);
            this.Controls.Add(this.txtSku);
            this.Controls.Add(lblCantidad);
            this.Controls.Add(this.nudCantidad);
            this.Controls.Add(lblTalla);
            this.Controls.Add(this.txtTalla);
            this.Controls.Add(lblColor);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.lblEstado);

            this.Name = "Form1";
            this.Text = "Ecosistema Políglota - Panel de Control";
            this.Load += new System.EventHandler(this.Form1_Load);

            ((System.ComponentModel.ISupportInitialize)(this.nudCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Declaración formal de las variables requeridas por Form1.cs
        private System.Windows.Forms.TextBox txtSku;
        private System.Windows.Forms.NumericUpDown nudCantidad;
        private System.Windows.Forms.TextBox txtTalla;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.Label lblEstado;
    }
}