namespace TaxManager
{
    partial class ReportRegistr
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportRegistr));
            this.ForReportRegistryBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new TaxManager.DataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.btn_result = new System.Windows.Forms.Button();
            this.radioButton_allColums = new System.Windows.Forms.RadioButton();
            this.radioButton_short = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rb_registr = new System.Windows.Forms.RadioButton();
            this.rB_ttnDate = new System.Windows.Forms.RadioButton();
            this.rb_createDate = new System.Windows.Forms.RadioButton();
            this.txt_registrName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.ForReportRegistryBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ForReportRegistryBindingSource
            // 
            this.ForReportRegistryBindingSource.DataMember = "ForReportRegistry";
            this.ForReportRegistryBindingSource.DataSource = this.DataSet1;
            // 
            // DataSet1
            // 
            this.DataSet1.DataSetName = "DataSet1";
            this.DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.ForReportRegistryBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TaxManager.ReportRegistr.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(13, 66);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(890, 332);
            this.reportViewer1.TabIndex = 10;
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(13, 12);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(143, 20);
            this.dateFrom.TabIndex = 11;
            this.dateFrom.ValueChanged += new System.EventHandler(this.dateFrom_ValueChanged);
            // 
            // dateTo
            // 
            this.dateTo.Location = new System.Drawing.Point(162, 12);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(137, 20);
            this.dateTo.TabIndex = 11;
            // 
            // btn_result
            // 
            this.btn_result.Location = new System.Drawing.Point(801, 9);
            this.btn_result.Name = "btn_result";
            this.btn_result.Size = new System.Drawing.Size(102, 23);
            this.btn_result.TabIndex = 12;
            this.btn_result.Text = "Сформировать";
            this.btn_result.UseVisualStyleBackColor = true;
            this.btn_result.Click += new System.EventHandler(this.btn_result_Click);
            // 
            // radioButton_allColums
            // 
            this.radioButton_allColums.AutoSize = true;
            this.radioButton_allColums.Location = new System.Drawing.Point(3, 3);
            this.radioButton_allColums.Name = "radioButton_allColums";
            this.radioButton_allColums.Size = new System.Drawing.Size(89, 17);
            this.radioButton_allColums.TabIndex = 13;
            this.radioButton_allColums.Text = "Все колонки";
            this.radioButton_allColums.UseVisualStyleBackColor = true;
            // 
            // radioButton_short
            // 
            this.radioButton_short.AutoSize = true;
            this.radioButton_short.Checked = true;
            this.radioButton_short.Location = new System.Drawing.Point(98, 3);
            this.radioButton_short.Name = "radioButton_short";
            this.radioButton_short.Size = new System.Drawing.Size(73, 17);
            this.radioButton_short.TabIndex = 13;
            this.radioButton_short.TabStop = true;
            this.radioButton_short.Text = "Короткий";
            this.radioButton_short.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(3, 3);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(89, 17);
            this.radioButton1.TabIndex = 13;
            this.radioButton1.Text = "Все колонки";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rb_registr);
            this.panel1.Controls.Add(this.radioButton_short);
            this.panel1.Controls.Add(this.radioButton_allColums);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Location = new System.Drawing.Point(305, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(253, 23);
            this.panel1.TabIndex = 14;
            // 
            // rb_registr
            // 
            this.rb_registr.AutoSize = true;
            this.rb_registr.Location = new System.Drawing.Point(177, 3);
            this.rb_registr.Name = "rb_registr";
            this.rb_registr.Size = new System.Drawing.Size(61, 17);
            this.rb_registr.TabIndex = 13;
            this.rb_registr.Text = "Реестр";
            this.rb_registr.UseVisualStyleBackColor = true;
            // 
            // rB_ttnDate
            // 
            this.rB_ttnDate.AutoSize = true;
            this.rB_ttnDate.Location = new System.Drawing.Point(583, 12);
            this.rB_ttnDate.Name = "rB_ttnDate";
            this.rB_ttnDate.Size = new System.Drawing.Size(90, 17);
            this.rB_ttnDate.TabIndex = 15;
            this.rB_ttnDate.Text = "По дате ТТН";
            this.rB_ttnDate.UseVisualStyleBackColor = true;
            // 
            // rb_createDate
            // 
            this.rb_createDate.AutoSize = true;
            this.rb_createDate.Checked = true;
            this.rb_createDate.Location = new System.Drawing.Point(679, 12);
            this.rb_createDate.Name = "rb_createDate";
            this.rb_createDate.Size = new System.Drawing.Size(116, 17);
            this.rb_createDate.TabIndex = 15;
            this.rb_createDate.TabStop = true;
            this.rb_createDate.Text = "По дате создания";
            this.rb_createDate.UseVisualStyleBackColor = true;
            // 
            // txt_registrName
            // 
            this.txt_registrName.Location = new System.Drawing.Point(106, 40);
            this.txt_registrName.Name = "txt_registrName";
            this.txt_registrName.Size = new System.Drawing.Size(100, 20);
            this.txt_registrName.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Номер реестра :";
            // 
            // ReportRegistr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 410);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_registrName);
            this.Controls.Add(this.rb_createDate);
            this.Controls.Add(this.rB_ttnDate);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_result);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportRegistr";
            this.Text = "Реестр поездок";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReportRegistr_FormClosing);
            this.Load += new System.EventHandler(this.ReportRegistr_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ForReportRegistryBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.Button btn_result;
        private System.Windows.Forms.BindingSource ForReportRegistryBindingSource;
        private DataSet1 DataSet1;
        private System.Windows.Forms.RadioButton radioButton_allColums;
        private System.Windows.Forms.RadioButton radioButton_short;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton rB_ttnDate;
        private System.Windows.Forms.RadioButton rb_createDate;
        private System.Windows.Forms.RadioButton rb_registr;
        private System.Windows.Forms.TextBox txt_registrName;
        private System.Windows.Forms.Label label1;
    }
}