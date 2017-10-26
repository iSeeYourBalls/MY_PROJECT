namespace TaxManager
{
    partial class InsertRegistrName
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsertRegistrName));
            this.txt_registrName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_registrSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_registrName
            // 
            this.txt_registrName.Location = new System.Drawing.Point(21, 24);
            this.txt_registrName.Name = "txt_registrName";
            this.txt_registrName.Size = new System.Drawing.Size(130, 20);
            this.txt_registrName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Внесите номер регистра";
            // 
            // btn_registrSave
            // 
            this.btn_registrSave.Location = new System.Drawing.Point(21, 50);
            this.btn_registrSave.Name = "btn_registrSave";
            this.btn_registrSave.Size = new System.Drawing.Size(130, 28);
            this.btn_registrSave.TabIndex = 2;
            this.btn_registrSave.Text = "Сохранить";
            this.btn_registrSave.UseVisualStyleBackColor = true;
            this.btn_registrSave.Click += new System.EventHandler(this.btn_registrSave_Click);
            // 
            // InsertRegistrName
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(170, 84);
            this.Controls.Add(this.btn_registrSave);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_registrName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsertRegistrName";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Создание регистра";
            this.Load += new System.EventHandler(this.InsertRegistrName_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_registrName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_registrSave;
    }
}