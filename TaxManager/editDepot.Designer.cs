namespace TaxManager
{
    partial class editDepot
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(editDepot));
            this.dgv_editDepot = new System.Windows.Forms.DataGridView();
            this.regionCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.regionNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.depotNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.editDepotBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new TaxManager.DataSet1();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_depotCode = new System.Windows.Forms.TextBox();
            this.label_regionCode = new System.Windows.Forms.Label();
            this.txt_depotName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_regionName = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_editDepot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editDepotBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_editDepot
            // 
            this.dgv_editDepot.AllowUserToAddRows = false;
            this.dgv_editDepot.AllowUserToDeleteRows = false;
            this.dgv_editDepot.AllowUserToResizeRows = false;
            this.dgv_editDepot.AutoGenerateColumns = false;
            this.dgv_editDepot.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_editDepot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_editDepot.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.regionCode,
            this.regionNameDataGridViewTextBoxColumn,
            this.depotNameDataGridViewTextBoxColumn});
            this.dgv_editDepot.DataSource = this.editDepotBindingSource;
            this.dgv_editDepot.Location = new System.Drawing.Point(12, 12);
            this.dgv_editDepot.Name = "dgv_editDepot";
            this.dgv_editDepot.ReadOnly = true;
            this.dgv_editDepot.RowHeadersVisible = false;
            this.dgv_editDepot.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_editDepot.Size = new System.Drawing.Size(408, 316);
            this.dgv_editDepot.TabIndex = 0;
            this.dgv_editDepot.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_editDepot_CellMouseDown);
            this.dgv_editDepot.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_editDepot_MouseClick);
            // 
            // regionCode
            // 
            this.regionCode.DataPropertyName = "regionCode";
            this.regionCode.HeaderText = "Код РЦ";
            this.regionCode.Name = "regionCode";
            this.regionCode.ReadOnly = true;
            // 
            // regionNameDataGridViewTextBoxColumn
            // 
            this.regionNameDataGridViewTextBoxColumn.DataPropertyName = "regionName";
            this.regionNameDataGridViewTextBoxColumn.HeaderText = "Название региона";
            this.regionNameDataGridViewTextBoxColumn.Name = "regionNameDataGridViewTextBoxColumn";
            this.regionNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // depotNameDataGridViewTextBoxColumn
            // 
            this.depotNameDataGridViewTextBoxColumn.DataPropertyName = "depotName";
            this.depotNameDataGridViewTextBoxColumn.HeaderText = "Название РЦ";
            this.depotNameDataGridViewTextBoxColumn.Name = "depotNameDataGridViewTextBoxColumn";
            this.depotNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // editDepotBindingSource
            // 
            this.editDepotBindingSource.DataMember = "editDepot";
            this.editDepotBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.txt_depotCode);
            this.flowLayoutPanel1.Controls.Add(this.label_regionCode);
            this.flowLayoutPanel1.Controls.Add(this.txt_regionName);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.txt_depotName);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(167, 208);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Код РЦ";
            // 
            // txt_depotCode
            // 
            this.txt_depotCode.Location = new System.Drawing.Point(3, 30);
            this.txt_depotCode.Name = "txt_depotCode";
            this.txt_depotCode.Size = new System.Drawing.Size(134, 20);
            this.txt_depotCode.TabIndex = 1;
            // 
            // label_regionCode
            // 
            this.label_regionCode.AutoSize = true;
            this.label_regionCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_regionCode.Location = new System.Drawing.Point(3, 58);
            this.label_regionCode.Margin = new System.Windows.Forms.Padding(3, 5, 3, 10);
            this.label_regionCode.Name = "label_regionCode";
            this.label_regionCode.Size = new System.Drawing.Size(129, 17);
            this.label_regionCode.TabIndex = 0;
            this.label_regionCode.Text = "Название региона";
            // 
            // txt_regionName
            // 
            this.txt_regionName.Location = new System.Drawing.Point(3, 88);
            this.txt_regionName.Name = "txt_regionName";
            this.txt_regionName.Size = new System.Drawing.Size(134, 20);
            this.txt_regionName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Название РЦ";
            // 
            // txt_depotName
            // 
            this.txt_depotName.Location = new System.Drawing.Point(3, 146);
            this.txt_depotName.Name = "txt_depotName";
            this.txt_depotName.Size = new System.Drawing.Size(134, 20);
            this.txt_depotName.TabIndex = 1;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(6, 270);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(167, 36);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Сохранить";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel1);
            this.groupBox1.Controls.Add(this.btn_save);
            this.groupBox1.Location = new System.Drawing.Point(426, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(181, 312);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Добавить РЦ";
            // 
            // editDepot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 336);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_editDepot);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "editDepot";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Список РЦ";
            this.Load += new System.EventHandler(this.editDepot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_editDepot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editDepotBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_editDepot;
        private System.Windows.Forms.BindingSource editDepotBindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_depotCode;
        private System.Windows.Forms.Label label_regionCode;
        private System.Windows.Forms.TextBox txt_regionName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_depotName;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridViewTextBoxColumn regionCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn regionNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn depotNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}