namespace Conversor
{
    partial class ExportarPage
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
            this.dgvGeometrias = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvPontos = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbXls = new System.Windows.Forms.RadioButton();
            this.rbShp = new System.Windows.Forms.RadioButton();
            this.rbTxt = new System.Windows.Forms.RadioButton();
            this.cbGeometria = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.cbSrid = new System.Windows.Forms.ComboBox();
            this.rbKml = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeometrias)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPontos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvGeometrias
            // 
            this.dgvGeometrias.AllowUserToAddRows = false;
            this.dgvGeometrias.AllowUserToDeleteRows = false;
            this.dgvGeometrias.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGeometrias.Location = new System.Drawing.Point(43, 95);
            this.dgvGeometrias.Name = "dgvGeometrias";
            this.dgvGeometrias.ReadOnly = true;
            this.dgvGeometrias.RowTemplate.Height = 24;
            this.dgvGeometrias.Size = new System.Drawing.Size(261, 108);
            this.dgvGeometrias.TabIndex = 0;
            this.dgvGeometrias.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGeometrias_CellClick);
            this.dgvGeometrias.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvGeometrias_CellContentClick);
            this.dgvGeometrias.SelectionChanged += new System.EventHandler(this.dgvGeometrias_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 72);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(160, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Selecione a Geometria :";
            // 
            // dgvPontos
            // 
            this.dgvPontos.AllowUserToAddRows = false;
            this.dgvPontos.AllowUserToDeleteRows = false;
            this.dgvPontos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPontos.Location = new System.Drawing.Point(43, 225);
            this.dgvPontos.Name = "dgvPontos";
            this.dgvPontos.ReadOnly = true;
            this.dgvPontos.RowTemplate.Height = 24;
            this.dgvPontos.Size = new System.Drawing.Size(466, 150);
            this.dgvPontos.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbKml);
            this.groupBox1.Controls.Add(this.rbXls);
            this.groupBox1.Controls.Add(this.rbShp);
            this.groupBox1.Controls.Add(this.rbTxt);
            this.groupBox1.Location = new System.Drawing.Point(43, 428);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 57);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Arquivo";
            // 
            // rbXls
            // 
            this.rbXls.AutoSize = true;
            this.rbXls.Location = new System.Drawing.Point(129, 22);
            this.rbXls.Name = "rbXls";
            this.rbXls.Size = new System.Drawing.Size(48, 21);
            this.rbXls.TabIndex = 2;
            this.rbXls.TabStop = true;
            this.rbXls.Text = "Xls";
            this.rbXls.UseVisualStyleBackColor = true;
            // 
            // rbShp
            // 
            this.rbShp.AutoSize = true;
            this.rbShp.Location = new System.Drawing.Point(69, 22);
            this.rbShp.Name = "rbShp";
            this.rbShp.Size = new System.Drawing.Size(54, 21);
            this.rbShp.TabIndex = 1;
            this.rbShp.TabStop = true;
            this.rbShp.Text = "Shp";
            this.rbShp.UseVisualStyleBackColor = true;
            // 
            // rbTxt
            // 
            this.rbTxt.AutoSize = true;
            this.rbTxt.Checked = true;
            this.rbTxt.Location = new System.Drawing.Point(7, 22);
            this.rbTxt.Name = "rbTxt";
            this.rbTxt.Size = new System.Drawing.Size(56, 21);
            this.rbTxt.TabIndex = 0;
            this.rbTxt.TabStop = true;
            this.rbTxt.Text = "Text";
            this.rbTxt.UseVisualStyleBackColor = true;
            // 
            // cbGeometria
            // 
            this.cbGeometria.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbGeometria.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGeometria.FormattingEnabled = true;
            this.cbGeometria.Items.AddRange(new object[] {
            "Ponto",
            "Linha",
            "Poligono"});
            this.cbGeometria.Location = new System.Drawing.Point(288, 30);
            this.cbGeometria.Name = "cbGeometria";
            this.cbGeometria.Size = new System.Drawing.Size(221, 24);
            this.cbGeometria.TabIndex = 4;
            this.cbGeometria.SelectedIndexChanged += new System.EventHandler(this.cbGeometria_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(40, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(204, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Selecione o tipo de geometria: ";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(345, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(164, 115);
            this.button1.TabIndex = 9;
            this.button1.Text = "Exportar para Arquivo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cbSrid
            // 
            this.cbSrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbSrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrid.FormattingEnabled = true;
            this.cbSrid.Location = new System.Drawing.Point(317, 447);
            this.cbSrid.Name = "cbSrid";
            this.cbSrid.Size = new System.Drawing.Size(192, 24);
            this.cbSrid.TabIndex = 10;
            // 
            // rbKml
            // 
            this.rbKml.AutoSize = true;
            this.rbKml.Location = new System.Drawing.Point(183, 22);
            this.rbKml.Name = "rbKml";
            this.rbKml.Size = new System.Drawing.Size(52, 21);
            this.rbKml.TabIndex = 3;
            this.rbKml.TabStop = true;
            this.rbKml.Text = "Kml";
            this.rbKml.UseVisualStyleBackColor = true;
            // 
            // ExportarPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 587);
            this.Controls.Add(this.cbSrid);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbGeometria);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvPontos);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvGeometrias);
            this.Name = "ExportarPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exportar Dados";
            ((System.ComponentModel.ISupportInitialize)(this.dgvGeometrias)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPontos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvGeometrias;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvPontos;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbXls;
        private System.Windows.Forms.RadioButton rbShp;
        private System.Windows.Forms.RadioButton rbTxt;
        private System.Windows.Forms.ComboBox cbGeometria;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ComboBox cbSrid;
        private System.Windows.Forms.RadioButton rbKml;
    }
}