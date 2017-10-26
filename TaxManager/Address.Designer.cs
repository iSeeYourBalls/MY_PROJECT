namespace TaxManager
{
    partial class Address
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Address));
            this.txt_search = new System.Windows.Forms.TextBox();
            this.btn_search = new System.Windows.Forms.Button();
            this.dgv_address = new System.Windows.Forms.DataGridView();
            this.dataSet1 = new TaxManager.DataSet1();
            this.addressBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.addressCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_address)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_search
            // 
            this.txt_search.Location = new System.Drawing.Point(13, 13);
            this.txt_search.Name = "txt_search";
            this.txt_search.Size = new System.Drawing.Size(166, 20);
            this.txt_search.TabIndex = 0;
            this.txt_search.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_search_KeyDown);
            // 
            // btn_search
            // 
            this.btn_search.Location = new System.Drawing.Point(185, 11);
            this.btn_search.Name = "btn_search";
            this.btn_search.Size = new System.Drawing.Size(75, 23);
            this.btn_search.TabIndex = 1;
            this.btn_search.Text = "Поиск";
            this.btn_search.UseVisualStyleBackColor = true;
            this.btn_search.Click += new System.EventHandler(this.btn_search_Click);
            // 
            // dgv_address
            // 
            this.dgv_address.AllowUserToAddRows = false;
            this.dgv_address.AllowUserToDeleteRows = false;
            this.dgv_address.AllowUserToResizeRows = false;
            this.dgv_address.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_address.AutoGenerateColumns = false;
            this.dgv_address.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_address.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_address.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.addressCodeDataGridViewTextBoxColumn,
            this.addressNameDataGridViewTextBoxColumn,
            this.cityNameDataGridViewTextBoxColumn,
            this.stateNameDataGridViewTextBoxColumn});
            this.dgv_address.DataSource = this.addressBindingSource;
            this.dgv_address.Location = new System.Drawing.Point(13, 40);
            this.dgv_address.Name = "dgv_address";
            this.dgv_address.ReadOnly = true;
            this.dgv_address.RowHeadersVisible = false;
            this.dgv_address.Size = new System.Drawing.Size(549, 390);
            this.dgv_address.TabIndex = 2;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // addressBindingSource
            // 
            this.addressBindingSource.DataMember = "Address";
            this.addressBindingSource.DataSource = this.dataSet1;
            // 
            // addressCodeDataGridViewTextBoxColumn
            // 
            this.addressCodeDataGridViewTextBoxColumn.DataPropertyName = "addressCode";
            this.addressCodeDataGridViewTextBoxColumn.HeaderText = "Код адреса";
            this.addressCodeDataGridViewTextBoxColumn.Name = "addressCodeDataGridViewTextBoxColumn";
            this.addressCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // addressNameDataGridViewTextBoxColumn
            // 
            this.addressNameDataGridViewTextBoxColumn.DataPropertyName = "addressName";
            this.addressNameDataGridViewTextBoxColumn.HeaderText = "Название адреса";
            this.addressNameDataGridViewTextBoxColumn.Name = "addressNameDataGridViewTextBoxColumn";
            this.addressNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cityNameDataGridViewTextBoxColumn
            // 
            this.cityNameDataGridViewTextBoxColumn.DataPropertyName = "cityName";
            this.cityNameDataGridViewTextBoxColumn.HeaderText = "Город";
            this.cityNameDataGridViewTextBoxColumn.Name = "cityNameDataGridViewTextBoxColumn";
            this.cityNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateNameDataGridViewTextBoxColumn
            // 
            this.stateNameDataGridViewTextBoxColumn.DataPropertyName = "stateName";
            this.stateNameDataGridViewTextBoxColumn.HeaderText = "Улица";
            this.stateNameDataGridViewTextBoxColumn.Name = "stateNameDataGridViewTextBoxColumn";
            this.stateNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // Address
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 442);
            this.Controls.Add(this.dgv_address);
            this.Controls.Add(this.btn_search);
            this.Controls.Add(this.txt_search);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Address";
            this.Text = "Справочник адресов";
            this.Load += new System.EventHandler(this.Address_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_address)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_search;
        private System.Windows.Forms.Button btn_search;
        private System.Windows.Forms.DataGridView dgv_address;
        private System.Windows.Forms.BindingSource addressBindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressCodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateNameDataGridViewTextBoxColumn;
    }
}