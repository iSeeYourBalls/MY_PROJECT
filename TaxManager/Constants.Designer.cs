namespace TaxManager
{
    partial class Constants
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Constants));
            this.dgv_constants = new System.Windows.Forms.DataGridView();
            this.editConstantsBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new TaxManager.DataSet1();
            this.btn_save = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uvalue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_constants)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editConstantsBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_constants
            // 
            this.dgv_constants.AllowUserToAddRows = false;
            this.dgv_constants.AllowUserToDeleteRows = false;
            this.dgv_constants.AllowUserToResizeRows = false;
            this.dgv_constants.AutoGenerateColumns = false;
            this.dgv_constants.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_constants.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_constants.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.id,
            this.uvalue});
            this.dgv_constants.DataSource = this.editConstantsBindingSource;
            this.dgv_constants.Location = new System.Drawing.Point(13, 13);
            this.dgv_constants.Name = "dgv_constants";
            this.dgv_constants.RowHeadersVisible = false;
            this.dgv_constants.Size = new System.Drawing.Size(501, 251);
            this.dgv_constants.TabIndex = 0;
            // 
            // editConstantsBindingSource
            // 
            this.editConstantsBindingSource.DataMember = "EditConstants";
            this.editConstantsBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(315, 270);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(199, 26);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Сохранить";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.FillWeight = 149.2386F;
            this.name.HeaderText = "Название";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // uvalue
            // 
            this.uvalue.DataPropertyName = "uvalue";
            this.uvalue.FillWeight = 50.76142F;
            this.uvalue.HeaderText = "Значение";
            this.uvalue.Name = "uvalue";
            // 
            // Constants
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(526, 308);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.dgv_constants);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Constants";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Константы";
            this.Load += new System.EventHandler(this.Constants_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_constants)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editConstantsBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_constants;
        private System.Windows.Forms.BindingSource editConstantsBindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn uvalue;
    }
}