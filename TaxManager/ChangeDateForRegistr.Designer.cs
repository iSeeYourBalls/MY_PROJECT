namespace TaxManager
{
    partial class ChangeDateForRegistr
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChangeDateForRegistr));
            this.dateTimePicker_changeDate = new System.Windows.Forms.DateTimePicker();
            this.btn_save = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dateTimePicker_changeDate
            // 
            this.dateTimePicker_changeDate.Location = new System.Drawing.Point(12, 12);
            this.dateTimePicker_changeDate.Name = "dateTimePicker_changeDate";
            this.dateTimePicker_changeDate.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker_changeDate.TabIndex = 0;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(12, 38);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(200, 42);
            this.btn_save.TabIndex = 1;
            this.btn_save.Text = "Сохранить";
            this.btn_save.UseVisualStyleBackColor = true;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // ChangeDateForRegistr
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 86);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.dateTimePicker_changeDate);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChangeDateForRegistr";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Изменить дату";
            this.Load += new System.EventHandler(this.ChangeDateForRegistr_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dateTimePicker_changeDate;
        private System.Windows.Forms.Button btn_save;
    }
}