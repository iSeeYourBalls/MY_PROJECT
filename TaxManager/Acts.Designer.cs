namespace TaxManager
{
    partial class Acts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Acts));
            this.ForReportActsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSet1 = new TaxManager.DataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dateFrom = new System.Windows.Forms.DateTimePicker();
            this.dateTo = new System.Windows.Forms.DateTimePicker();
            this.btn_result = new System.Windows.Forms.Button();
            this.btn_changeStatus = new System.Windows.Forms.Button();
            this.rB_ttnDate = new System.Windows.Forms.RadioButton();
            this.rB_createDate = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.dateTimePicker_actDate = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_actNumber = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.ForReportActsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // ForReportActsBindingSource
            // 
            this.ForReportActsBindingSource.DataMember = "ForReportActs";
            this.ForReportActsBindingSource.DataSource = this.DataSet1;
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
            reportDataSource1.Value = this.ForReportActsBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "TaxManager.Acts.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(12, 77);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.ReportPath = "/TaxManager/acts";
            this.reportViewer1.ServerReport.ReportServerUrl = new System.Uri("http://otdrep/ReportServer", System.UriKind.Absolute);
            this.reportViewer1.Size = new System.Drawing.Size(1046, 533);
            this.reportViewer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Дата с :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(225, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Дата по :";
            // 
            // dateFrom
            // 
            this.dateFrom.Location = new System.Drawing.Point(79, 9);
            this.dateFrom.Name = "dateFrom";
            this.dateFrom.Size = new System.Drawing.Size(140, 20);
            this.dateFrom.TabIndex = 2;
            this.dateFrom.ValueChanged += new System.EventHandler(this.dateFrom_ValueChanged);
            // 
            // dateTo
            // 
            this.dateTo.Location = new System.Drawing.Point(301, 9);
            this.dateTo.Name = "dateTo";
            this.dateTo.Size = new System.Drawing.Size(139, 20);
            this.dateTo.TabIndex = 2;
            // 
            // btn_result
            // 
            this.btn_result.Location = new System.Drawing.Point(711, 6);
            this.btn_result.Name = "btn_result";
            this.btn_result.Size = new System.Drawing.Size(100, 23);
            this.btn_result.TabIndex = 3;
            this.btn_result.Text = "Сформировать";
            this.btn_result.UseVisualStyleBackColor = true;
            this.btn_result.Click += new System.EventHandler(this.btn_result_Click);
            // 
            // btn_changeStatus
            // 
            this.btn_changeStatus.Enabled = false;
            this.btn_changeStatus.Location = new System.Drawing.Point(817, 6);
            this.btn_changeStatus.Name = "btn_changeStatus";
            this.btn_changeStatus.Size = new System.Drawing.Size(241, 23);
            this.btn_changeStatus.TabIndex = 4;
            this.btn_changeStatus.Text = "Присвоить выбранным статус распечатан";
            this.btn_changeStatus.UseVisualStyleBackColor = true;
            this.btn_changeStatus.Click += new System.EventHandler(this.btn_changeStatus_Click);
            // 
            // rB_ttnDate
            // 
            this.rB_ttnDate.AutoSize = true;
            this.rB_ttnDate.Location = new System.Drawing.Point(447, 9);
            this.rB_ttnDate.Name = "rB_ttnDate";
            this.rB_ttnDate.Size = new System.Drawing.Size(90, 17);
            this.rB_ttnDate.TabIndex = 5;
            this.rB_ttnDate.Text = "По дате ТТН";
            this.rB_ttnDate.UseVisualStyleBackColor = true;
            // 
            // rB_createDate
            // 
            this.rB_createDate.AutoSize = true;
            this.rB_createDate.Checked = true;
            this.rB_createDate.Location = new System.Drawing.Point(543, 9);
            this.rB_createDate.Name = "rB_createDate";
            this.rB_createDate.Size = new System.Drawing.Size(116, 17);
            this.rB_createDate.TabIndex = 5;
            this.rB_createDate.TabStop = true;
            this.rB_createDate.Text = "По дате создания";
            this.rB_createDate.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(9, 41);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Дата акта :";
            // 
            // dateTimePicker_actDate
            // 
            this.dateTimePicker_actDate.Location = new System.Drawing.Point(99, 40);
            this.dateTimePicker_actDate.Name = "dateTimePicker_actDate";
            this.dateTimePicker_actDate.Size = new System.Drawing.Size(140, 20);
            this.dateTimePicker_actDate.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(257, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Номер акта :";
            // 
            // txt_actNumber
            // 
            this.txt_actNumber.Location = new System.Drawing.Point(356, 40);
            this.txt_actNumber.Name = "txt_actNumber";
            this.txt_actNumber.Size = new System.Drawing.Size(100, 20);
            this.txt_actNumber.TabIndex = 7;
            // 
            // Acts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1070, 622);
            this.Controls.Add(this.txt_actNumber);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.rB_createDate);
            this.Controls.Add(this.rB_ttnDate);
            this.Controls.Add(this.btn_changeStatus);
            this.Controls.Add(this.btn_result);
            this.Controls.Add(this.dateTo);
            this.Controls.Add(this.dateTimePicker_actDate);
            this.Controls.Add(this.dateFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.reportViewer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Acts";
            this.Text = "Акты";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Acts_FormClosing);
            this.Load += new System.EventHandler(this.Acts_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ForReportActsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSet1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ForReportActsBindingSource;
        private DataSet1 DataSet1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dateFrom;
        private System.Windows.Forms.DateTimePicker dateTo;
        private System.Windows.Forms.Button btn_result;
        private System.Windows.Forms.Button btn_changeStatus;
        private System.Windows.Forms.RadioButton rB_ttnDate;
        private System.Windows.Forms.RadioButton rB_createDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dateTimePicker_actDate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_actNumber;
    }
}