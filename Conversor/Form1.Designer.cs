namespace Conversor
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.cbGeometria = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbXls = new System.Windows.Forms.RadioButton();
            this.rbShp = new System.Windows.Forms.RadioButton();
            this.rbTxt = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.tbNome = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sfMap1 = new EGIS.Controls.SFMap();
            this.cbSrid = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Selecione o tipo de geometria: ";
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
            this.cbGeometria.Location = new System.Drawing.Point(278, 41);
            this.cbGeometria.Name = "cbGeometria";
            this.cbGeometria.Size = new System.Drawing.Size(221, 24);
            this.cbGeometria.TabIndex = 1;
            this.cbGeometria.SelectedIndexChanged += new System.EventHandler(this.cbGeometria_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbXls);
            this.groupBox1.Controls.Add(this.rbShp);
            this.groupBox1.Controls.Add(this.rbTxt);
            this.groupBox1.Location = new System.Drawing.Point(31, 87);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(261, 57);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Arquivo";
            // 
            // rbXls
            // 
            this.rbXls.AutoSize = true;
            this.rbXls.Location = new System.Drawing.Point(171, 22);
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
            this.rbShp.Location = new System.Drawing.Point(80, 22);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(302, 109);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(197, 84);
            this.button1.TabIndex = 3;
            this.button1.Text = "Carregar Arquivo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Location = new System.Drawing.Point(31, 290);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.RowTemplate.Height = 24;
            this.dgvData.Size = new System.Drawing.Size(468, 213);
            this.dgvData.TabIndex = 4;
            // 
            // tbNome
            // 
            this.tbNome.Location = new System.Drawing.Point(182, 238);
            this.tbNome.Name = "tbNome";
            this.tbNome.Size = new System.Drawing.Size(195, 22);
            this.tbNome.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 241);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "Nome da geometria";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // sfMap1
            // 
            this.sfMap1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sfMap1.CentrePoint2D = ((EGIS.ShapeFileLib.PointD)(resources.GetObject("sfMap1.CentrePoint2D")));
            this.sfMap1.Location = new System.Drawing.Point(530, 39);
            this.sfMap1.MapBackColor = System.Drawing.SystemColors.Control;
            this.sfMap1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.sfMap1.Name = "sfMap1";
            this.sfMap1.PanSelectMode = EGIS.Controls.PanSelectMode.Pan;
            this.sfMap1.RenderQuality = EGIS.ShapeFileLib.RenderQuality.Auto;
            this.sfMap1.Size = new System.Drawing.Size(605, 464);
            this.sfMap1.TabIndex = 12;
            this.sfMap1.UseMercatorProjection = false;
            this.sfMap1.ZoomLevel = 1D;
            this.sfMap1.ZoomToSelectedExtentWhenCtrlKeydown = false;
            // 
            // cbSrid
            // 
            this.cbSrid.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.cbSrid.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSrid.FormattingEnabled = true;
            this.cbSrid.Items.AddRange(new object[] {
            "Ponto",
            "Linha",
            "Poligono"});
            this.cbSrid.Location = new System.Drawing.Point(31, 181);
            this.cbSrid.Name = "cbSrid";
            this.cbSrid.Size = new System.Drawing.Size(221, 24);
            this.cbSrid.TabIndex = 13;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1166, 554);
            this.Controls.Add(this.cbSrid);
            this.Controls.Add(this.sfMap1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbNome);
            this.Controls.Add(this.dgvData);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbGeometria);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Importar arquivos";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbGeometria;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbXls;
        private System.Windows.Forms.RadioButton rbShp;
        private System.Windows.Forms.RadioButton rbTxt;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.TextBox tbNome;
        private System.Windows.Forms.Label label2;
        private EGIS.Controls.SFMap sfMap1;
        private System.Windows.Forms.ComboBox cbSrid;
    }
}

