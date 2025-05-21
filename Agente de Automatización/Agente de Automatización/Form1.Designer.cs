namespace Agente_de_Automatización
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox txtTema;
        private System.Windows.Forms.Button btnInvestigar;
        private System.Windows.Forms.RichTextBox rtbResultado;
        private System.Windows.Forms.Button btnGuardar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            txtTema = new TextBox();
            btnInvestigar = new Button();
            rtbResultado = new RichTextBox();
            btnGuardar = new Button();
            SuspendLayout();
            // 
            // txtTema
            // 
            txtTema.Location = new Point(28, 75);
            txtTema.Name = "txtTema";
            txtTema.Size = new Size(601, 23);
            txtTema.TabIndex = 0;
            // 
            // btnInvestigar
            // 
            btnInvestigar.Location = new Point(667, 75);
            btnInvestigar.Name = "btnInvestigar";
            btnInvestigar.Size = new Size(100, 23);
            btnInvestigar.TabIndex = 1;
            btnInvestigar.Text = "Investigar";
            btnInvestigar.UseVisualStyleBackColor = true;
            btnInvestigar.Click += btnInvestigar_Click;
            // 
            // rtbResultado
            // 
            rtbResultado.Location = new Point(12, 128);
            rtbResultado.Name = "rtbResultado";
            rtbResultado.Size = new Size(801, 303);
            rtbResultado.TabIndex = 2;
            rtbResultado.Text = "";
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(12, 465);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(175, 30);
            btnGuardar.TabIndex = 3;
            btnGuardar.Text = "Guardar Word y PowerPoint";
            btnGuardar.UseVisualStyleBackColor = true;
            btnGuardar.Click += btnGuardar_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.MenuHighlight;
            ClientSize = new Size(825, 507);
            Controls.Add(btnGuardar);
            Controls.Add(rtbResultado);
            Controls.Add(btnInvestigar);
            Controls.Add(txtTema);
            Name = "Form1";
            Text = "Agente de Investigación";
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

