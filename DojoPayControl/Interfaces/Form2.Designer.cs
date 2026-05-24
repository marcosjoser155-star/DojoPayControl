namespace DojoPayControl.Interfaces
{
    partial class FrmDashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmDashboard));
            this.pnlHeader = new System.Windows.Forms.FlowLayoutPanel();
            this.picEstrella = new System.Windows.Forms.PictureBox();
            this.lblTituloApp = new System.Windows.Forms.Label();
            this.tbcNavegacionPrincipal = new System.Windows.Forms.TabControl();
            this.tabDashboard = new System.Windows.Forms.TabPage();
            this.pnlAcciones = new System.Windows.Forms.Panel();
            this.btnNuevoEstudiante = new System.Windows.Forms.Button();
            this.pnlEstados = new System.Windows.Forms.Panel();
            this.lblCountRevision = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblCountPausado = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblCountRestringido = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCountPendiente = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCountAlDia = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlHerramientas = new System.Windows.Forms.Panel();
            this.picLupa = new System.Windows.Forms.PictureBox();
            this.txtBusquedaEstudiante = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpFechaFiltro = new System.Windows.Forms.DateTimePicker();
            this.tabEstudiantes = new System.Windows.Forms.TabPage();
            this.tabPagos = new System.Windows.Forms.TabPage();
            this.btnRegistrarPago = new System.Windows.Forms.Button();
            this.btnPausarEstudiante = new System.Windows.Forms.Button();
            this.btnReactivarEstudiante = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEstrella)).BeginInit();
            this.tbcNavegacionPrincipal.SuspendLayout();
            this.tabDashboard.SuspendLayout();
            this.pnlAcciones.SuspendLayout();
            this.pnlEstados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlHerramientas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLupa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(42)))), ((int)(((byte)(94)))));
            this.pnlHeader.Controls.Add(this.picEstrella);
            this.pnlHeader.Controls.Add(this.lblTituloApp);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(834, 30);
            this.pnlHeader.TabIndex = 0;
            // 
            // picEstrella
            // 
            this.picEstrella.Image = ((System.Drawing.Image)(resources.GetObject("picEstrella.Image")));
            this.picEstrella.Location = new System.Drawing.Point(3, 3);
            this.picEstrella.Name = "picEstrella";
            this.picEstrella.Size = new System.Drawing.Size(79, 21);
            this.picEstrella.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picEstrella.TabIndex = 2;
            this.picEstrella.TabStop = false;
            // 
            // lblTituloApp
            // 
            this.lblTituloApp.AutoSize = true;
            this.lblTituloApp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloApp.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTituloApp.ForeColor = System.Drawing.Color.White;
            this.lblTituloApp.Location = new System.Drawing.Point(88, 0);
            this.lblTituloApp.Name = "lblTituloApp";
            this.lblTituloApp.Size = new System.Drawing.Size(127, 27);
            this.lblTituloApp.TabIndex = 3;
            this.lblTituloApp.Text = "Dojo Pay Control";
            this.lblTituloApp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbcNavegacionPrincipal
            // 
            this.tbcNavegacionPrincipal.Controls.Add(this.tabDashboard);
            this.tbcNavegacionPrincipal.Controls.Add(this.tabEstudiantes);
            this.tbcNavegacionPrincipal.Controls.Add(this.tabPagos);
            this.tbcNavegacionPrincipal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcNavegacionPrincipal.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbcNavegacionPrincipal.ItemSize = new System.Drawing.Size(150, 30);
            this.tbcNavegacionPrincipal.Location = new System.Drawing.Point(0, 30);
            this.tbcNavegacionPrincipal.Name = "tbcNavegacionPrincipal";
            this.tbcNavegacionPrincipal.SelectedIndex = 0;
            this.tbcNavegacionPrincipal.Size = new System.Drawing.Size(834, 581);
            this.tbcNavegacionPrincipal.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tbcNavegacionPrincipal.TabIndex = 1;
            // 
            // tabDashboard
            // 
            this.tabDashboard.Controls.Add(this.pnlAcciones);
            this.tabDashboard.Controls.Add(this.pnlEstados);
            this.tabDashboard.Controls.Add(this.pnlHerramientas);
            this.tabDashboard.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabDashboard.Location = new System.Drawing.Point(4, 34);
            this.tabDashboard.Name = "tabDashboard";
            this.tabDashboard.Padding = new System.Windows.Forms.Padding(3);
            this.tabDashboard.Size = new System.Drawing.Size(826, 543);
            this.tabDashboard.TabIndex = 0;
            this.tabDashboard.Text = "DASHBOARD";
            this.tabDashboard.UseVisualStyleBackColor = true;
            // 
            // pnlAcciones
            // 
            this.pnlAcciones.BackColor = System.Drawing.Color.White;
            this.pnlAcciones.Controls.Add(this.pictureBox3);
            this.pnlAcciones.Controls.Add(this.btnReactivarEstudiante);
            this.pnlAcciones.Controls.Add(this.btnPausarEstudiante);
            this.pnlAcciones.Controls.Add(this.btnRegistrarPago);
            this.pnlAcciones.Controls.Add(this.btnNuevoEstudiante);
            this.pnlAcciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAcciones.Location = new System.Drawing.Point(3, 108);
            this.pnlAcciones.Name = "pnlAcciones";
            this.pnlAcciones.Size = new System.Drawing.Size(820, 46);
            this.pnlAcciones.TabIndex = 2;
            // 
            // btnNuevoEstudiante
            // 
            this.btnNuevoEstudiante.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnNuevoEstudiante.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevoEstudiante.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNuevoEstudiante.Location = new System.Drawing.Point(24, 6);
            this.btnNuevoEstudiante.Name = "btnNuevoEstudiante";
            this.btnNuevoEstudiante.Size = new System.Drawing.Size(155, 32);
            this.btnNuevoEstudiante.TabIndex = 0;
            this.btnNuevoEstudiante.Text = "+ Nuevo estudiante";
            this.btnNuevoEstudiante.UseVisualStyleBackColor = true;
            // 
            // pnlEstados
            // 
            this.pnlEstados.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(249)))), ((int)(((byte)(255)))));
            this.pnlEstados.Controls.Add(this.lblCountRevision);
            this.pnlEstados.Controls.Add(this.label12);
            this.pnlEstados.Controls.Add(this.lblCountPausado);
            this.pnlEstados.Controls.Add(this.label10);
            this.pnlEstados.Controls.Add(this.lblCountRestringido);
            this.pnlEstados.Controls.Add(this.label8);
            this.pnlEstados.Controls.Add(this.lblCountPendiente);
            this.pnlEstados.Controls.Add(this.label6);
            this.pnlEstados.Controls.Add(this.lblCountAlDia);
            this.pnlEstados.Controls.Add(this.label3);
            this.pnlEstados.Controls.Add(this.pictureBox2);
            this.pnlEstados.Controls.Add(this.label2);
            this.pnlEstados.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlEstados.Location = new System.Drawing.Point(3, 43);
            this.pnlEstados.Name = "pnlEstados";
            this.pnlEstados.Size = new System.Drawing.Size(820, 65);
            this.pnlEstados.TabIndex = 1;
            this.pnlEstados.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // lblCountRevision
            // 
            this.lblCountRevision.AutoSize = true;
            this.lblCountRevision.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountRevision.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.lblCountRevision.Location = new System.Drawing.Point(573, 43);
            this.lblCountRevision.Name = "lblCountRevision";
            this.lblCountRevision.Size = new System.Drawing.Size(111, 15);
            this.lblCountRevision.TabIndex = 5;
            this.lblCountRevision.Text = "Revisar anualidad: 5";
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Peru;
            this.label12.Location = new System.Drawing.Point(555, 44);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(12, 12);
            this.label12.TabIndex = 4;
            // 
            // lblCountPausado
            // 
            this.lblCountPausado.AutoSize = true;
            this.lblCountPausado.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountPausado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.lblCountPausado.Location = new System.Drawing.Point(445, 42);
            this.lblCountPausado.Name = "lblCountPausado";
            this.lblCountPausado.Size = new System.Drawing.Size(64, 15);
            this.lblCountPausado.TabIndex = 5;
            this.lblCountPausado.Text = "Pausado: 3";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.RoyalBlue;
            this.label10.Location = new System.Drawing.Point(427, 43);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(12, 12);
            this.label10.TabIndex = 4;
            // 
            // lblCountRestringido
            // 
            this.lblCountRestringido.AutoSize = true;
            this.lblCountRestringido.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountRestringido.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.lblCountRestringido.Location = new System.Drawing.Point(294, 41);
            this.lblCountRestringido.Name = "lblCountRestringido";
            this.lblCountRestringido.Size = new System.Drawing.Size(79, 15);
            this.lblCountRestringido.TabIndex = 5;
            this.lblCountRestringido.Text = "Restringido: 2";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.Firebrick;
            this.label8.Location = new System.Drawing.Point(276, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(12, 12);
            this.label8.TabIndex = 4;
            // 
            // lblCountPendiente
            // 
            this.lblCountPendiente.AutoSize = true;
            this.lblCountPendiente.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountPendiente.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.lblCountPendiente.Location = new System.Drawing.Point(151, 40);
            this.lblCountPendiente.Name = "lblCountPendiente";
            this.lblCountPendiente.Size = new System.Drawing.Size(72, 15);
            this.lblCountPendiente.TabIndex = 5;
            this.lblCountPendiente.Text = "Pendiente: 4";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Goldenrod;
            this.label6.Location = new System.Drawing.Point(133, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 12);
            this.label6.TabIndex = 4;
            // 
            // lblCountAlDia
            // 
            this.lblCountAlDia.AutoSize = true;
            this.lblCountAlDia.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCountAlDia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(64)))), ((int)(((byte)(96)))));
            this.lblCountAlDia.Location = new System.Drawing.Point(42, 39);
            this.lblCountAlDia.Name = "lblCountAlDia";
            this.lblCountAlDia.Size = new System.Drawing.Size(52, 15);
            this.lblCountAlDia.TabIndex = 3;
            this.lblCountAlDia.Text = "Al día:18";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.ForestGreen;
            this.label3.Location = new System.Drawing.Point(24, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 12);
            this.label3.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(24, 7);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(21, 18);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(42)))), ((int)(((byte)(94)))));
            this.label2.Location = new System.Drawing.Point(51, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(296, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Sistema de Control de Mensualidades-Dojo Pay Control";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // pnlHerramientas
            // 
            this.pnlHerramientas.BackColor = System.Drawing.Color.White;
            this.pnlHerramientas.Controls.Add(this.picLupa);
            this.pnlHerramientas.Controls.Add(this.txtBusquedaEstudiante);
            this.pnlHerramientas.Controls.Add(this.label1);
            this.pnlHerramientas.Controls.Add(this.dtpFechaFiltro);
            this.pnlHerramientas.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHerramientas.Location = new System.Drawing.Point(3, 3);
            this.pnlHerramientas.Name = "pnlHerramientas";
            this.pnlHerramientas.Size = new System.Drawing.Size(820, 40);
            this.pnlHerramientas.TabIndex = 0;
            // 
            // picLupa
            // 
            this.picLupa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picLupa.BackColor = System.Drawing.Color.Transparent;
            this.picLupa.Image = ((System.Drawing.Image)(resources.GetObject("picLupa.Image")));
            this.picLupa.Location = new System.Drawing.Point(749, 13);
            this.picLupa.Name = "picLupa";
            this.picLupa.Size = new System.Drawing.Size(18, 20);
            this.picLupa.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLupa.TabIndex = 3;
            this.picLupa.TabStop = false;
            // 
            // txtBusquedaEstudiante
            // 
            this.txtBusquedaEstudiante.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBusquedaEstudiante.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBusquedaEstudiante.Location = new System.Drawing.Point(563, 11);
            this.txtBusquedaEstudiante.Name = "txtBusquedaEstudiante";
            this.txtBusquedaEstudiante.Size = new System.Drawing.Size(180, 23);
            this.txtBusquedaEstudiante.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(96)))), ((int)(((byte)(117)))));
            this.label1.Location = new System.Drawing.Point(489, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 29);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar estudiante:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dtpFechaFiltro
            // 
            this.dtpFechaFiltro.CustomFormat = "dd.MM.yyyy";
            this.dtpFechaFiltro.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFechaFiltro.Location = new System.Drawing.Point(10, 7);
            this.dtpFechaFiltro.Name = "dtpFechaFiltro";
            this.dtpFechaFiltro.Size = new System.Drawing.Size(140, 22);
            this.dtpFechaFiltro.TabIndex = 0;
            // 
            // tabEstudiantes
            // 
            this.tabEstudiantes.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabEstudiantes.Location = new System.Drawing.Point(4, 34);
            this.tabEstudiantes.Name = "tabEstudiantes";
            this.tabEstudiantes.Padding = new System.Windows.Forms.Padding(3);
            this.tabEstudiantes.Size = new System.Drawing.Size(826, 543);
            this.tabEstudiantes.TabIndex = 1;
            this.tabEstudiantes.Text = "ESTUDIANTES";
            this.tabEstudiantes.UseVisualStyleBackColor = true;
            // 
            // tabPagos
            // 
            this.tabPagos.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPagos.Location = new System.Drawing.Point(4, 34);
            this.tabPagos.Name = "tabPagos";
            this.tabPagos.Size = new System.Drawing.Size(826, 543);
            this.tabPagos.TabIndex = 2;
            this.tabPagos.Text = "PAGOS";
            this.tabPagos.UseVisualStyleBackColor = true;
            // 
            // btnRegistrarPago
            // 
            this.btnRegistrarPago.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnRegistrarPago.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrarPago.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrarPago.Location = new System.Drawing.Point(185, 6);
            this.btnRegistrarPago.Name = "btnRegistrarPago";
            this.btnRegistrarPago.Size = new System.Drawing.Size(155, 32);
            this.btnRegistrarPago.TabIndex = 1;
            this.btnRegistrarPago.Text = "Registrar pago";
            this.btnRegistrarPago.UseVisualStyleBackColor = true;
            // 
            // btnPausarEstudiante
            // 
            this.btnPausarEstudiante.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnPausarEstudiante.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPausarEstudiante.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPausarEstudiante.Location = new System.Drawing.Point(346, 6);
            this.btnPausarEstudiante.Name = "btnPausarEstudiante";
            this.btnPausarEstudiante.Size = new System.Drawing.Size(104, 32);
            this.btnPausarEstudiante.TabIndex = 2;
            this.btnPausarEstudiante.Text = "Pausar";
            this.btnPausarEstudiante.UseVisualStyleBackColor = true;
            // 
            // btnReactivarEstudiante
            // 
            this.btnReactivarEstudiante.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnReactivarEstudiante.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReactivarEstudiante.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReactivarEstudiante.Location = new System.Drawing.Point(456, 6);
            this.btnReactivarEstudiante.Name = "btnReactivarEstudiante";
            this.btnReactivarEstudiante.Size = new System.Drawing.Size(104, 32);
            this.btnReactivarEstudiante.TabIndex = 3;
            this.btnReactivarEstudiante.Text = "Reactivar";
            this.btnReactivarEstudiante.UseVisualStyleBackColor = true;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(461, 15);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(17, 13);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox3.TabIndex = 4;
            this.pictureBox3.TabStop = false;
            // 
            // FrmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(834, 611);
            this.Controls.Add(this.tbcNavegacionPrincipal);
            this.Controls.Add(this.pnlHeader);
            this.Name = "FrmDashboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEstrella)).EndInit();
            this.tbcNavegacionPrincipal.ResumeLayout(false);
            this.tabDashboard.ResumeLayout(false);
            this.pnlAcciones.ResumeLayout(false);
            this.pnlEstados.ResumeLayout(false);
            this.pnlEstados.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlHerramientas.ResumeLayout(false);
            this.pnlHerramientas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLupa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlHeader;
        private System.Windows.Forms.Label lblTituloApp;
        private System.Windows.Forms.PictureBox picEstrella;
        private System.Windows.Forms.TabControl tbcNavegacionPrincipal;
        private System.Windows.Forms.TabPage tabDashboard;
        private System.Windows.Forms.TabPage tabEstudiantes;
        private System.Windows.Forms.TabPage tabPagos;
        private System.Windows.Forms.Panel pnlHerramientas;
        private System.Windows.Forms.DateTimePicker dtpFechaFiltro;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBusquedaEstudiante;
        private System.Windows.Forms.PictureBox picLupa;
        private System.Windows.Forms.Panel pnlEstados;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCountAlDia;
        private System.Windows.Forms.Label lblCountPausado;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCountRestringido;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblCountPendiente;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCountRevision;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel pnlAcciones;
        private System.Windows.Forms.Button btnNuevoEstudiante;
        private System.Windows.Forms.Button btnReactivarEstudiante;
        private System.Windows.Forms.Button btnPausarEstudiante;
        private System.Windows.Forms.Button btnRegistrarPago;
        private System.Windows.Forms.PictureBox pictureBox3;
    }
}