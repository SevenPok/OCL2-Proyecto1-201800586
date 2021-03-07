
namespace OCL2_Proyecto1_201800586
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
            this.contenido = new System.Windows.Forms.RichTextBox();
            this.Run = new System.Windows.Forms.Button();
            this.richtextbox1 = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // contenido
            // 
            this.contenido.Location = new System.Drawing.Point(12, 62);
            this.contenido.Name = "contenido";
            this.contenido.Size = new System.Drawing.Size(967, 252);
            this.contenido.TabIndex = 1;
            this.contenido.Text = "";
            // 
            // Run
            // 
            this.Run.Location = new System.Drawing.Point(12, 21);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(75, 23);
            this.Run.TabIndex = 2;
            this.Run.Text = "Run";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // richtextbox1
            // 
            this.richtextbox1.BackColor = System.Drawing.SystemColors.InfoText;
            this.richtextbox1.ForeColor = System.Drawing.SystemColors.Menu;
            this.richtextbox1.Location = new System.Drawing.Point(12, 341);
            this.richtextbox1.Name = "richtextbox1";
            this.richtextbox1.Size = new System.Drawing.Size(967, 175);
            this.richtextbox1.TabIndex = 3;
            this.richtextbox1.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(991, 568);
            this.Controls.Add(this.richtextbox1);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.contenido);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CompiPascal";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox contenido;
        private System.Windows.Forms.Button Run;
        public System.Windows.Forms.RichTextBox richtextbox1;
      
    }
}

