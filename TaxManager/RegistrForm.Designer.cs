namespace TaxManager
{
    partial class RegistrForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrForm));
            this.dgv_registrData = new System.Windows.Forms.DataGridView();
            this.registrTTNBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new TaxManager.DataSet1();
            this.txt_registrCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_registrSearch = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dateTimePicker_registr = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label_countTTN = new System.Windows.Forms.Label();
            this.dateInsert = new DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn();
            this.depotNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.truckNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTTNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inRegistrOrNot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shiftcodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countTTNDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tripDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subcontractorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.registrNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_registrData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrTTNBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_registrData
            // 
            this.dgv_registrData.AllowUserToAddRows = false;
            this.dgv_registrData.AllowUserToDeleteRows = false;
            this.dgv_registrData.AllowUserToResizeRows = false;
            this.dgv_registrData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_registrData.AutoGenerateColumns = false;
            this.dgv_registrData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_registrData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dateInsert,
            this.depotNameDataGridViewTextBoxColumn,
            this.truckNumberDataGridViewTextBoxColumn,
            this.dateTTNDataGridViewTextBoxColumn,
            this.inRegistrOrNot,
            this.shiftcodeDataGridViewTextBoxColumn,
            this.countTTNDataGridViewTextBoxColumn,
            this.tripDataGridViewTextBoxColumn,
            this.subcontractorDataGridViewTextBoxColumn,
            this.registrNumberDataGridViewTextBoxColumn});
            this.dgv_registrData.DataSource = this.registrTTNBindingSource;
            this.dgv_registrData.Location = new System.Drawing.Point(13, 57);
            this.dgv_registrData.Name = "dgv_registrData";
            this.dgv_registrData.ReadOnly = true;
            this.dgv_registrData.RowHeadersVisible = false;
            this.dgv_registrData.Size = new System.Drawing.Size(785, 369);
            this.dgv_registrData.TabIndex = 0;
            this.dgv_registrData.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgv_registrData_DataBindingComplete);
            // 
            // registrTTNBindingSource
            // 
            this.registrTTNBindingSource.DataMember = "registrTTN";
            this.registrTTNBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txt_registrCode
            // 
            this.txt_registrCode.Location = new System.Drawing.Point(114, 6);
            this.txt_registrCode.Name = "txt_registrCode";
            this.txt_registrCode.Size = new System.Drawing.Size(100, 20);
            this.txt_registrCode.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Номер регистра :";
            // 
            // btn_registrSearch
            // 
            this.btn_registrSearch.Location = new System.Drawing.Point(529, 4);
            this.btn_registrSearch.Name = "btn_registrSearch";
            this.btn_registrSearch.Size = new System.Drawing.Size(75, 23);
            this.btn_registrSearch.TabIndex = 3;
            this.btn_registrSearch.Text = "Поиск";
            this.btn_registrSearch.UseVisualStyleBackColor = true;
            this.btn_registrSearch.Click += new System.EventHandler(this.btn_registrSearch_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(220, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "или выбрать дату";
            // 
            // dateTimePicker_registr
            // 
            this.dateTimePicker_registr.Location = new System.Drawing.Point(323, 6);
            this.dateTimePicker_registr.Name = "dateTimePicker_registr";
            this.dateTimePicker_registr.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_registr.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(610, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Количество ТТН :";
            // 
            // label_countTTN
            // 
            this.label_countTTN.AutoSize = true;
            this.label_countTTN.Location = new System.Drawing.Point(713, 9);
            this.label_countTTN.Name = "label_countTTN";
            this.label_countTTN.Size = new System.Drawing.Size(35, 13);
            this.label_countTTN.TabIndex = 7;
            this.label_countTTN.Text = "label4";
            // 
            // dateInsert
            // 
            this.dateInsert.DataPropertyName = "dateInsret";
            this.dateInsert.HeaderText = "Дата внесения";
            this.dateInsert.Name = "dateInsert";
            this.dateInsert.ReadOnly = true;
            this.dateInsert.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // depotNameDataGridViewTextBoxColumn
            // 
            this.depotNameDataGridViewTextBoxColumn.DataPropertyName = "depotName";
            this.depotNameDataGridViewTextBoxColumn.HeaderText = "Номер РЦ";
            this.depotNameDataGridViewTextBoxColumn.Name = "depotNameDataGridViewTextBoxColumn";
            this.depotNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // truckNumberDataGridViewTextBoxColumn
            // 
            this.truckNumberDataGridViewTextBoxColumn.DataPropertyName = "truckNumber";
            this.truckNumberDataGridViewTextBoxColumn.HeaderText = "Гос.номер авто";
            this.truckNumberDataGridViewTextBoxColumn.Name = "truckNumberDataGridViewTextBoxColumn";
            this.truckNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dateTTNDataGridViewTextBoxColumn
            // 
            this.dateTTNDataGridViewTextBoxColumn.DataPropertyName = "dateTTN";
            this.dateTTNDataGridViewTextBoxColumn.HeaderText = "Дата ТТН";
            this.dateTTNDataGridViewTextBoxColumn.Name = "dateTTNDataGridViewTextBoxColumn";
            this.dateTTNDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // inRegistrOrNot
            // 
            this.inRegistrOrNot.DataPropertyName = "inRegistrOrNot";
            this.inRegistrOrNot.HeaderText = "inRegistrOrNot";
            this.inRegistrOrNot.Name = "inRegistrOrNot";
            this.inRegistrOrNot.ReadOnly = true;
            this.inRegistrOrNot.Visible = false;
            // 
            // shiftcodeDataGridViewTextBoxColumn
            // 
            this.shiftcodeDataGridViewTextBoxColumn.DataPropertyName = "shiftcode";
            this.shiftcodeDataGridViewTextBoxColumn.HeaderText = "Номер ТТН";
            this.shiftcodeDataGridViewTextBoxColumn.Name = "shiftcodeDataGridViewTextBoxColumn";
            this.shiftcodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // countTTNDataGridViewTextBoxColumn
            // 
            this.countTTNDataGridViewTextBoxColumn.DataPropertyName = "countTTN";
            this.countTTNDataGridViewTextBoxColumn.HeaderText = "Кол-во ТТН";
            this.countTTNDataGridViewTextBoxColumn.Name = "countTTNDataGridViewTextBoxColumn";
            this.countTTNDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tripDataGridViewTextBoxColumn
            // 
            this.tripDataGridViewTextBoxColumn.DataPropertyName = "trip";
            this.tripDataGridViewTextBoxColumn.HeaderText = "Маршрут";
            this.tripDataGridViewTextBoxColumn.Name = "tripDataGridViewTextBoxColumn";
            this.tripDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // subcontractorDataGridViewTextBoxColumn
            // 
            this.subcontractorDataGridViewTextBoxColumn.DataPropertyName = "subcontractor";
            this.subcontractorDataGridViewTextBoxColumn.HeaderText = "Заказчик";
            this.subcontractorDataGridViewTextBoxColumn.Name = "subcontractorDataGridViewTextBoxColumn";
            this.subcontractorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // registrNumberDataGridViewTextBoxColumn
            // 
            this.registrNumberDataGridViewTextBoxColumn.DataPropertyName = "registrNumber";
            this.registrNumberDataGridViewTextBoxColumn.HeaderText = "Код";
            this.registrNumberDataGridViewTextBoxColumn.Name = "registrNumberDataGridViewTextBoxColumn";
            this.registrNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.RosyBrown;
            this.pictureBox1.Location = new System.Drawing.Point(15, 29);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(42, 22);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(61, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(205, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "- в реестре есть, в таксированных нет.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.GreenYellow;
            this.pictureBox2.Location = new System.Drawing.Point(272, 29);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(42, 22);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "- протаксирован и в реестре есть";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.Color.Red;
            this.pictureBox3.Location = new System.Drawing.Point(502, 29);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(42, 22);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(548, 33);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(206, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "- протаксирован, но в реестре ТТН нет";
            // 
            // RegistrForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(810, 438);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_countTTN);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker_registr);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_registrSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_registrCode);
            this.Controls.Add(this.dgv_registrData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "RegistrForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Реестр переданных ТТН";
            this.Load += new System.EventHandler(this.RegistrForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_registrData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.registrTTNBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_registrData;
        private System.Windows.Forms.TextBox txt_registrCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_registrSearch;
        private System.Windows.Forms.BindingSource registrTTNBindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateTimePicker_registr;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_countTTN;
        private DataGridViewAutoFilter.DataGridViewAutoFilterTextBoxColumn dateInsert;
        private System.Windows.Forms.DataGridViewTextBoxColumn depotNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn truckNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTTNDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn inRegistrOrNot;
        private System.Windows.Forms.DataGridViewTextBoxColumn shiftcodeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countTTNDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tripDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subcontractorDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn registrNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label6;
    }
}