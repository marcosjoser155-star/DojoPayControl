namespace DojoPayControl.Interfaces
{
    partial class FrmRegistrarPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRegistrarPago));
            this.pnlHeaderSub = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblRegistrarNuevoPago = new System.Windows.Forms.Label();
            this.pnLHeaderTop = new System.Windows.Forms.Panel();
            this.lblTituloHeader = new System.Windows.Forms.Label();
            this.picIconoHeader = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pnlFooter = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlDivisionPersonal = new System.Windows.Forms.Panel();
            this.lblSeccionPersonal = new System.Windows.Forms.Label();
            this.pnlHeaderSub.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnLHeaderTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIconoHeader)).BeginInit();
            this.panel2.SuspendLayout();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeaderSub
            // 
            this.pnlHeaderSub.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(255)))), ((int)(((byte)(244)))));
            this.pnlHeaderSub.Controls.Add(this.pictureBox2);
            this.pnlHeaderSub.Controls.Add(this.lblRegistrarNuevoPago);
            this.pnlHeaderSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeaderSub.Location = new System.Drawing.Point(0, 45);
            this.pnlHeaderSub.Name = "pnlHeaderSub";
            this.pnlHeaderSub.Size = new System.Drawing.Size(834, 40);
            this.pnlHeaderSub.TabIndex = 5;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(24, 6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 21);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 2;
            this.pictureBox2.TabStop = false;
            // 
            // lblRegistrarNuevoPago
            // 
            this.lblRegistrarNuevoPago.AutoSize = true;
            this.lblRegistrarNuevoPago.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRegistrarNuevoPago.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(84)))), ((int)(((byte)(61)))));
            this.lblRegistrarNuevoPago.Location = new System.Drawing.Point(51, 7);
            this.lblRegistrarNuevoPago.Name = "lblRegistrarNuevoPago";
            this.lblRegistrarNuevoPago.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblRegistrarNuevoPago.Size = new System.Drawing.Size(140, 20);
            this.lblRegistrarNuevoPago.TabIndex = 2;
            this.lblRegistrarNuevoPago.Text = "Registrar nuevo pago";
            // 
            // pnLHeaderTop
            // 
            this.pnLHeaderTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(42)))), ((int)(((byte)(94)))));
            this.pnLHeaderTop.Controls.Add(this.lblTituloHeader);
            this.pnLHeaderTop.Controls.Add(this.picIconoHeader);
            this.pnLHeaderTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnLHeaderTop.Location = new System.Drawing.Point(0, 0);
            this.pnLHeaderTop.Name = "pnLHeaderTop";
            this.pnLHeaderTop.Size = new System.Drawing.Size(834, 45);
            this.pnLHeaderTop.TabIndex = 4;
            // 
            // lblTituloHeader
            // 
            this.lblTituloHeader.AutoSize = true;
            this.lblTituloHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloHeader.ForeColor = System.Drawing.Color.White;
            this.lblTituloHeader.Location = new System.Drawing.Point(39, 9);
            this.lblTituloHeader.Name = "lblTituloHeader";
            this.lblTituloHeader.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.lblTituloHeader.Size = new System.Drawing.Size(121, 24);
            this.lblTituloHeader.TabIndex = 3;
            this.lblTituloHeader.Text = "Registrar Pago";
            // 
            // picIconoHeader
            // 
            this.picIconoHeader.Image = ((System.Drawing.Image)(resources.GetObject("picIconoHeader.Image")));
            this.picIconoHeader.Location = new System.Drawing.Point(12, 12);
            this.picIconoHeader.Name = "picIconoHeader";
            this.picIconoHeader.Size = new System.Drawing.Size(21, 21);
            this.picIconoHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIconoHeader.TabIndex = 3;
            this.picIconoHeader.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.pnlDivisionPersonal);
            this.panel2.Controls.Add(this.lblSeccionPersonal);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.pnlFooter);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 85);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(25);
            this.panel2.Size = new System.Drawing.Size(834, 526);
            this.panel2.TabIndex = 6;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // pnlFooter
            // 
            this.pnlFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFooter.Controls.Add(this.btnGuardar);
            this.pnlFooter.Controls.Add(this.btnCancelar);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(25, 421);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(784, 80);
            this.pnlFooter.TabIndex = 0;
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelar.BackColor = System.Drawing.Color.White;
            this.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelar.Location = new System.Drawing.Point(434, 23);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 45);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            this.btnGuardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGuardar.BackColor = System.Drawing.Color.White;
            this.btnGuardar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGuardar.Location = new System.Drawing.Point(560, 23);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(180, 45);
            this.btnGuardar.TabIndex = 2;
            this.btnGuardar.Text = "Registrar Pago";
            this.btnGuardar.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Estudiante";
            // 
            // pnlDivisionPersonal
            // 
            this.pnlDivisionPersonal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDivisionPersonal.BackColor = System.Drawing.Color.LightGray;
            this.pnlDivisionPersonal.Location = new System.Drawing.Point(25, 50);
            this.pnlDivisionPersonal.Name = "pnlDivisionPersonal";
            this.pnlDivisionPersonal.Size = new System.Drawing.Size(784, 1);
            this.pnlDivisionPersonal.TabIndex = 3;
            // 
            // lblSeccionPersonal
            // 
            this.lblSeccionPersonal.AutoSize = true;
            this.lblSeccionPersonal.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSeccionPersonal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(133)))), ((int)(((byte)(90)))));
            this.lblSeccionPersonal.Location = new System.Drawing.Point(22, 34);
            this.lblSeccionPersonal.Name = "lblSeccionPersonal";
            this.lblSeccionPersonal.Size = new System.Drawing.Size(77, 19);
            this.lblSeccionPersonal.TabIndex = 2;
            this.lblSeccionPersonal.Text = "Estudiante\r\n";
            // 
            // FrmRegistrarPago
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 611);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlHeaderSub);
            this.Controls.Add(this.pnLHeaderTop);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FrmRegistrarPago";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form4";
            this.pnlHeaderSub.ResumeLayout(false);
            this.pnlHeaderSub.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnLHeaderTop.ResumeLayout(false);
            this.pnLHeaderTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIconoHeader)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeaderSub;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblRegistrarNuevoPago;
        private System.Windows.Forms.Panel pnLHeaderTop;
        private System.Windows.Forms.Label lblTituloHeader;
        private System.Windows.Forms.PictureBox picIconoHeader;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlFooter;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlDivisionPersonal;
        private System.Windows.Forms.Label lblSeccionPersonal;
    }
}