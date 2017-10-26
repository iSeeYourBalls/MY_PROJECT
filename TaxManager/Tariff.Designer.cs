namespace TaxManager
{
    partial class Tariff
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Tariff));
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker_periodTariffFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker_periodTariffTo = new System.Windows.Forms.DateTimePicker();
            this.dgv_tariffTable = new System.Windows.Forms.DataGridView();
            this.typeIdInTariffTableDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeCodeTariffTableDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateInsertDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tarifWithRefInCityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tarifInCityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tarifPerKmWithRefDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tarifPerKmDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tarifPerKmFreightWithRefDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tarifPerKmFreightDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fixTariffRef = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fixTarif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.active = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.commentDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tariffTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new TaxManager.DataSet1();
            this.btn_newTariff = new System.Windows.Forms.Button();
            this.btn_toSeeTariff = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tariffTable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tariffTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Период действия тарифов";
            // 
            // dateTimePicker_periodTariffFrom
            // 
            this.dateTimePicker_periodTariffFrom.Location = new System.Drawing.Point(160, 12);
            this.dateTimePicker_periodTariffFrom.Name = "dateTimePicker_periodTariffFrom";
            this.dateTimePicker_periodTariffFrom.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_periodTariffFrom.TabIndex = 2;
            // 
            // dateTimePicker_periodTariffTo
            // 
            this.dateTimePicker_periodTariffTo.Location = new System.Drawing.Point(366, 12);
            this.dateTimePicker_periodTariffTo.Name = "dateTimePicker_periodTariffTo";
            this.dateTimePicker_periodTariffTo.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_periodTariffTo.TabIndex = 2;
            // 
            // dgv_tariffTable
            // 
            this.dgv_tariffTable.AllowUserToAddRows = false;
            this.dgv_tariffTable.AllowUserToDeleteRows = false;
            this.dgv_tariffTable.AllowUserToOrderColumns = true;
            this.dgv_tariffTable.AllowUserToResizeRows = false;
            this.dgv_tariffTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_tariffTable.AutoGenerateColumns = false;
            this.dgv_tariffTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_tariffTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.typeIdInTariffTableDataGridViewTextBoxColumn,
            this.id,
            this.typeCodeTariffTableDataGridViewTextBoxColumn,
            this.dateInsertDataGridViewTextBoxColumn,
            this.tarifWithRefInCityDataGridViewTextBoxColumn,
            this.tarifInCityDataGridViewTextBoxColumn,
            this.tarifPerKmWithRefDataGridViewTextBoxColumn,
            this.tarifPerKmDataGridViewTextBoxColumn,
            this.tarifPerKmFreightWithRefDataGridViewTextBoxColumn,
            this.tarifPerKmFreightDataGridViewTextBoxColumn,
            this.fixTariffRef,
            this.fixTarif,
            this.active,
            this.commentDataGridViewTextBoxColumn});
            this.dgv_tariffTable.DataSource = this.tariffTableBindingSource;
            this.dgv_tariffTable.Location = new System.Drawing.Point(16, 45);
            this.dgv_tariffTable.Name = "dgv_tariffTable";
            this.dgv_tariffTable.ReadOnly = true;
            this.dgv_tariffTable.RowHeadersVisible = false;
            this.dgv_tariffTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_tariffTable.Size = new System.Drawing.Size(948, 490);
            this.dgv_tariffTable.TabIndex = 3;
            this.dgv_tariffTable.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_tariffTable_CellMouseDoubleClick);
            this.dgv_tariffTable.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_tariffTable_CellMouseDown);
            this.dgv_tariffTable.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_tariffTable_MouseClick);
            // 
            // typeIdInTariffTableDataGridViewTextBoxColumn
            // 
            this.typeIdInTariffTableDataGridViewTextBoxColumn.DataPropertyName = "typeIdInTariffTable";
            this.typeIdInTariffTableDataGridViewTextBoxColumn.HeaderText = "Код тарифа";
            this.typeIdInTariffTableDataGridViewTextBoxColumn.Name = "typeIdInTariffTableDataGridViewTextBoxColumn";
            this.typeIdInTariffTableDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // typeCodeTariffTableDataGridViewTextBoxColumn
            // 
            this.typeCodeTariffTableDataGridViewTextBoxColumn.DataPropertyName = "typeCodeTariffTable";
            this.typeCodeTariffTableDataGridViewTextBoxColumn.HeaderText = "Идентификатор";
            this.typeCodeTariffTableDataGridViewTextBoxColumn.Name = "typeCodeTariffTableDataGridViewTextBoxColumn";
            this.typeCodeTariffTableDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateInsertDataGridViewTextBoxColumn
            // 
            this.dateInsertDataGridViewTextBoxColumn.DataPropertyName = "dateInsert";
            this.dateInsertDataGridViewTextBoxColumn.HeaderText = "Дата начала действия";
            this.dateInsertDataGridViewTextBoxColumn.Name = "dateInsertDataGridViewTextBoxColumn";
            this.dateInsertDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tarifWithRefInCityDataGridViewTextBoxColumn
            // 
            this.tarifWithRefInCityDataGridViewTextBoxColumn.DataPropertyName = "tarifWithRefInCity";
            this.tarifWithRefInCityDataGridViewTextBoxColumn.HeaderText = "По городу +5";
            this.tarifWithRefInCityDataGridViewTextBoxColumn.Name = "tarifWithRefInCityDataGridViewTextBoxColumn";
            this.tarifWithRefInCityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tarifInCityDataGridViewTextBoxColumn
            // 
            this.tarifInCityDataGridViewTextBoxColumn.DataPropertyName = "tarifInCity";
            this.tarifInCityDataGridViewTextBoxColumn.HeaderText = "По городу";
            this.tarifInCityDataGridViewTextBoxColumn.Name = "tarifInCityDataGridViewTextBoxColumn";
            this.tarifInCityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tarifPerKmWithRefDataGridViewTextBoxColumn
            // 
            this.tarifPerKmWithRefDataGridViewTextBoxColumn.DataPropertyName = "tarifPerKmWithRef";
            this.tarifPerKmWithRefDataGridViewTextBoxColumn.HeaderText = "Межгород +5";
            this.tarifPerKmWithRefDataGridViewTextBoxColumn.Name = "tarifPerKmWithRefDataGridViewTextBoxColumn";
            this.tarifPerKmWithRefDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tarifPerKmDataGridViewTextBoxColumn
            // 
            this.tarifPerKmDataGridViewTextBoxColumn.DataPropertyName = "tarifPerKm";
            this.tarifPerKmDataGridViewTextBoxColumn.HeaderText = "Межгород";
            this.tarifPerKmDataGridViewTextBoxColumn.Name = "tarifPerKmDataGridViewTextBoxColumn";
            this.tarifPerKmDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tarifPerKmFreightWithRefDataGridViewTextBoxColumn
            // 
            this.tarifPerKmFreightWithRefDataGridViewTextBoxColumn.DataPropertyName = "tarifPerKmFreightWithRef";
            this.tarifPerKmFreightWithRefDataGridViewTextBoxColumn.HeaderText = "Фрахт +5";
            this.tarifPerKmFreightWithRefDataGridViewTextBoxColumn.Name = "tarifPerKmFreightWithRefDataGridViewTextBoxColumn";
            this.tarifPerKmFreightWithRefDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tarifPerKmFreightDataGridViewTextBoxColumn
            // 
            this.tarifPerKmFreightDataGridViewTextBoxColumn.DataPropertyName = "tarifPerKmFreight";
            this.tarifPerKmFreightDataGridViewTextBoxColumn.HeaderText = "Фрахт";
            this.tarifPerKmFreightDataGridViewTextBoxColumn.Name = "tarifPerKmFreightDataGridViewTextBoxColumn";
            this.tarifPerKmFreightDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // fixTariffRef
            // 
            this.fixTariffRef.DataPropertyName = "fixTariffRef";
            this.fixTariffRef.HeaderText = "Фиксированный +5";
            this.fixTariffRef.Name = "fixTariffRef";
            this.fixTariffRef.ReadOnly = true;
            // 
            // fixTarif
            // 
            this.fixTarif.DataPropertyName = "fixTariff";
            this.fixTarif.HeaderText = "Фиксированный";
            this.fixTarif.Name = "fixTarif";
            this.fixTarif.ReadOnly = true;
            // 
            // active
            // 
            this.active.DataPropertyName = "active";
            this.active.HeaderText = "Активность";
            this.active.Name = "active";
            this.active.ReadOnly = true;
            // 
            // commentDataGridViewTextBoxColumn
            // 
            this.commentDataGridViewTextBoxColumn.DataPropertyName = "comment";
            this.commentDataGridViewTextBoxColumn.HeaderText = "Комментарий";
            this.commentDataGridViewTextBoxColumn.Name = "commentDataGridViewTextBoxColumn";
            this.commentDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tariffTableBindingSource
            // 
            this.tariffTableBindingSource.DataMember = "tariffTable";
            this.tariffTableBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btn_newTariff
            // 
            this.btn_newTariff.Location = new System.Drawing.Point(730, 10);
            this.btn_newTariff.Name = "btn_newTariff";
            this.btn_newTariff.Size = new System.Drawing.Size(234, 23);
            this.btn_newTariff.TabIndex = 4;
            this.btn_newTariff.Text = "Создать новый";
            this.btn_newTariff.UseVisualStyleBackColor = true;
            this.btn_newTariff.Click += new System.EventHandler(this.btn_newTariff_Click);
            // 
            // btn_toSeeTariff
            // 
            this.btn_toSeeTariff.Location = new System.Drawing.Point(572, 10);
            this.btn_toSeeTariff.Name = "btn_toSeeTariff";
            this.btn_toSeeTariff.Size = new System.Drawing.Size(152, 23);
            this.btn_toSeeTariff.TabIndex = 5;
            this.btn_toSeeTariff.Text = "Сформировать";
            this.btn_toSeeTariff.UseVisualStyleBackColor = true;
            this.btn_toSeeTariff.Click += new System.EventHandler(this.btn_toSeeTariff_Click);
            // 
            // Tariff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 547);
            this.Controls.Add(this.btn_toSeeTariff);
            this.Controls.Add(this.btn_newTariff);
            this.Controls.Add(this.dgv_tariffTable);
            this.Controls.Add(this.dateTimePicker_periodTariffTo);
            this.Controls.Add(this.dateTimePicker_periodTariffFrom);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Tariff";
            this.Text = "Редактирование тарифов";
            this.Load += new System.EventHandler(this.Tariff_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_tariffTable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tariffTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dateTimePicker_periodTariffFrom;
        private System.Windows.Forms.DateTimePicker dateTimePicker_periodTariffTo;
        private System.Windows.Forms.DataGridView dgv_tariffTable;
        private System.Windows.Forms.BindingSource tariffTableBindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.Button btn_newTariff;
        private System.Windows.Forms.Button btn_toSeeTariff;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeIdInTariffTableDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn typeCodeTariffTableDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateInsertDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tarifWithRefInCityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tarifInCityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tarifPerKmWithRefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tarifPerKmDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tarifPerKmFreightWithRefDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tarifPerKmFreightDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fixTariffRef;
        private System.Windows.Forms.DataGridViewTextBoxColumn fixTarif;
        private System.Windows.Forms.DataGridViewTextBoxColumn active;
        private System.Windows.Forms.DataGridViewTextBoxColumn commentDataGridViewTextBoxColumn;
    }
}