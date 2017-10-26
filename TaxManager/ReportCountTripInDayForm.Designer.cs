namespace TaxManager
{
    partial class ReportCountTripInDayForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReportCountTripInDayForm));
            this.reportCountTripInDayBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new TaxManager.DataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btn_create = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_lastYear = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.reportCountTripInDayBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // reportCountTripInDayBindingSource
            // 
            this.reportCountTripInDayBindingSource.DataMember = "reportCountTripInDay";
            this.reportCountTripInDayBindingSource.DataSource = this.DataSet1;
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
            reportDataSource1.Value = this.reportCountTripInDayBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TaxManager.ReportCountTripInDay.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 58);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(907, 413);
            this.reportViewer1.TabIndex = 0;
            // 
            // btn_create
            // 
            this.btn_create.Location = new System.Drawing.Point(503, 6);
            this.btn_create.Name = "btn_create";
            this.btn_create.Size = new System.Drawing.Size(168, 42);
            this.btn_create.TabIndex = 2;
            this.btn_create.Text = "Сформировать";
            this.btn_create.UseVisualStyleBackColor = true;
            this.btn_create.Click += new System.EventHandler(this.btn_create_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(104, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Выбрать месяц";
            // 
            // cb_lastYear
            // 
            this.cb_lastYear.AutoSize = true;
            this.cb_lastYear.Location = new System.Drawing.Point(15, 35);
            this.cb_lastYear.Name = "cb_lastYear";
            this.cb_lastYear.Size = new System.Drawing.Size(100, 17);
            this.cb_lastYear.TabIndex = 6;
            this.cb_lastYear.Text = "Прошлый год?";
            this.cb_lastYear.UseVisualStyleBackColor = true;
            // 
            // ReportCountTripInDayForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 483);
            this.Controls.Add(this.cb_lastYear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.btn_create);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReportCountTripInDayForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Отчет количество рейсов в разрезе суток";
            this.Load += new System.EventHandler(this.ReportCountTripInDayForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.reportCountTripInDayBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource reportCountTripInDayBindingSource;
        private DataSet1 DataSet1;
        private System.Windows.Forms.Button btn_create;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_lastYear;
    }
}