namespace DojoPayControl
{
    partial class FrmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.pnlLogin = new System.Windows.Forms.Panel();
            this.lblAvisoPrivacidad = new System.Windows.Forms.Label();
            this.btnIngresar = new System.Windows.Forms.Button();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblInstrucciones = new System.Windows.Forms.Label();
            this.lblSistemaNombre = new System.Windows.Forms.Label();
            this.picLogoDojo = new System.Windows.Forms.PictureBox();
            this.lblTituloApp = new System.Windows.Forms.Label();
            this.picEstrella = new System.Windows.Forms.PictureBox();
            this.pnlLogin.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoDojo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEstrella)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLogin
            // 
            this.pnlLogin.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pnlLogin.BackColor = System.Drawing.Color.White;
            this.pnlLogin.Controls.Add(this.lblAvisoPrivacidad);
            this.pnlLogin.Controls.Add(this.btnIngresar);
            this.pnlLogin.Controls.Add(this.txtPassword);
            this.pnlLogin.Controls.Add(this.lblPassword);
            this.pnlLogin.Controls.Add(this.txtUsuario);
            this.pnlLogin.Controls.Add(this.lblUsuario);
            this.pnlLogin.Controls.Add(this.panel1);
            this.pnlLogin.Location = new System.Drawing.Point(212, 7);
            this.pnlLogin.Name = "pnlLogin";
            this.pnlLogin.Size = new System.Drawing.Size(400, 450);
            this.pnlLogin.TabIndex = 0;
            // 
            // lblAvisoPrivacidad
            // 
            this.lblAvisoPrivacidad.AutoSize = true;
            this.lblAvisoPrivacidad.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAvisoPrivacidad.ForeColor = System.Drawing.Color.DarkGray;
            this.lblAvisoPrivacidad.Location = new System.Drawing.Point(105, 409);
            this.lblAvisoPrivacidad.Name = "lblAvisoPrivacidad";
            this.lblAvisoPrivacidad.Size = new System.Drawing.Size(183, 13);
            this.lblAvisoPrivacidad.TabIndex = 6;
            this.lblAvisoPrivacidad.Text = "Solo personal autorizado del dojo";
            this.lblAvisoPrivacidad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnIngresar
            // 
            this.btnIngresar.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIngresar.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIngresar.ForeColor = System.Drawing.Color.Black;
            this.btnIngresar.Location = new System.Drawing.Point(25, 366);
            this.btnIngresar.Name = "btnIngresar";
            this.btnIngresar.Size = new System.Drawing.Size(340, 40);
            this.btnIngresar.TabIndex = 5;
            this.btnIngresar.Text = "Ingresar al sistema";
            this.btnIngresar.UseVisualStyleBackColor = true;
            // 
            // txtPassword
            // 
            this.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPassword.ForeColor = System.Drawing.Color.Gray;
            this.txtPassword.Location = new System.Drawing.Point(25, 309);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(340, 29);
            this.txtPassword.TabIndex = 4;
            this.txtPassword.Text = "   ************";
            this.txtPassword.TextChanged += new System.EventHandler(this.txtPassword_TextChanged);
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(91)))), ((int)(((byte)(125)))));
            this.lblPassword.Location = new System.Drawing.Point(22, 291);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(69, 15);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "Contraseña";
            // 
            // txtUsuario
            // 
            this.txtUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUsuario.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuario.ForeColor = System.Drawing.Color.Gray;
            this.txtUsuario.Location = new System.Drawing.Point(25, 243);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(340, 29);
            this.txtUsuario.TabIndex = 2;
            this.txtUsuario.Text = "    Ej.instructor01";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(79)))), ((int)(((byte)(91)))), ((int)(((byte)(125)))));
            this.lblUsuario.Location = new System.Drawing.Point(22, 225);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(49, 15);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuario";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(42)))), ((int)(((byte)(94)))));
            this.panel1.Controls.Add(this.lblInstrucciones);
            this.panel1.Controls.Add(this.lblSistemaNombre);
            this.panel1.Controls.Add(this.picLogoDojo);
            this.panel1.Controls.Add(this.lblTituloApp);
            this.panel1.Controls.Add(this.picEstrella);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 200);
            this.panel1.TabIndex = 0;
            // 
            // lblInstrucciones
            // 
            this.lblInstrucciones.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInstrucciones.AutoSize = true;
            this.lblInstrucciones.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstrucciones.ForeColor = System.Drawing.Color.LightGray;
            this.lblInstrucciones.Location = new System.Drawing.Point(47, 168);
            this.lblInstrucciones.Name = "lblInstrucciones";
            this.lblInstrucciones.Size = new System.Drawing.Size(308, 15);
            this.lblInstrucciones.TabIndex = 4;
            this.lblInstrucciones.Text = "Dojo Pay Control-Ingrese sus credenciales para continuar";
            this.lblInstrucciones.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSistemaNombre
            // 
            this.lblSistemaNombre.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSistemaNombre.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSistemaNombre.ForeColor = System.Drawing.Color.White;
            this.lblSistemaNombre.Location = new System.Drawing.Point(-1, 135);
            this.lblSistemaNombre.Name = "lblSistemaNombre";
            this.lblSistemaNombre.Size = new System.Drawing.Size(401, 33);
            this.lblSistemaNombre.TabIndex = 3;
            this.lblSistemaNombre.Text = "Sistema de Control De Mensualidades";
            this.lblSistemaNombre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLogoDojo
            // 
            this.picLogoDojo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.picLogoDojo.BackColor = System.Drawing.Color.Transparent;
            this.picLogoDojo.Image = ((System.Drawing.Image)(resources.GetObject("picLogoDojo.Image")));
            this.picLogoDojo.Location = new System.Drawing.Point(140, 47);
            this.picLogoDojo.Name = "picLogoDojo";
            this.picLogoDojo.Size = new System.Drawing.Size(123, 85);
            this.picLogoDojo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogoDojo.TabIndex = 2;
            this.picLogoDojo.TabStop = false;
            // 
            // lblTituloApp
            // 
            this.lblTituloApp.AutoSize = true;
            this.lblTituloApp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloApp.ForeColor = System.Drawing.Color.White;
            this.lblTituloApp.Location = new System.Drawing.Point(95, 12);
            this.lblTituloApp.Name = "lblTituloApp";
            this.lblTituloApp.Size = new System.Drawing.Size(114, 17);
            this.lblTituloApp.TabIndex = 1;
            this.lblTituloApp.Text = "Dojo Pay Control";
            // 
            // picEstrella
            // 
            this.picEstrella.Image = ((System.Drawing.Image)(resources.GetObject("picEstrella.Image")));
            this.picEstrella.Location = new System.Drawing.Point(11, 10);
            this.picEstrella.Name = "picEstrella";
            this.picEstrella.Size = new System.Drawing.Size(79, 21);
            this.picEstrella.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEstrella.TabIndex = 0;
            this.picEstrella.TabStop = false;
            // 
            // FrmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(225)))), ((int)(((byte)(230)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(792, 515);
            this.Controls.Add(this.pnlLogin);
            this.MinimumSize = new System.Drawing.Size(480, 550);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.FrmLogin_Load);
            this.pnlLogin.ResumeLayout(false);
            this.pnlLogin.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogoDojo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picEstrella)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLogin;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picEstrella;
        private System.Windows.Forms.Label lblTituloApp;
        private System.Windows.Forms.PictureBox picLogoDojo;
        private System.Windows.Forms.Label lblInstrucciones;
        private System.Windows.Forms.Label lblSistemaNombre;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.TextBox txtUsuario;
        private System.Windows.Forms.Label lblAvisoPrivacidad;
        private System.Windows.Forms.Button btnIngresar;
    }
}

