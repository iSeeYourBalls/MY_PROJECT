namespace TaxManager
{
    partial class users
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(users));
            this.dgv_editUsers = new System.Windows.Forms.DataGridView();
            this.editUsersBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new TaxManager.DataSet1();
            this.editDepotBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_login = new System.Windows.Forms.TextBox();
            this.label_regionCode = new System.Windows.Forms.Label();
            this.txt_pass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.login = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pass = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_editUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editUsersBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.editDepotBindingSource)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_editUsers
            // 
            this.dgv_editUsers.AllowUserToAddRows = false;
            this.dgv_editUsers.AllowUserToDeleteRows = false;
            this.dgv_editUsers.AllowUserToResizeRows = false;
            this.dgv_editUsers.AutoGenerateColumns = false;
            this.dgv_editUsers.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_editUsers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_editUsers.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.login,
            this.pass,
            this.name});
            this.dgv_editUsers.DataSource = this.editUsersBindingSource;
            this.dgv_editUsers.Location = new System.Drawing.Point(12, 12);
            this.dgv_editUsers.Name = "dgv_editUsers";
            this.dgv_editUsers.ReadOnly = true;
            this.dgv_editUsers.RowHeadersVisible = false;
            this.dgv_editUsers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_editUsers.Size = new System.Drawing.Size(408, 316);
            this.dgv_editUsers.TabIndex = 0;
            this.dgv_editUsers.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_editDepot_CellMouseDown);
            this.dgv_editUsers.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_editDepot_MouseClick);
            // 
            // editUsersBindingSource
            // 
            this.editUsersBindingSource.DataMember = "EditUsers";
            this.editUsersBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // editDepotBindingSource
            // 
            this.editDepotBindingSource.DataMember = "editDepot";
            this.editDepotBindingSource.DataSource = this.dataSet1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.txt_login);
            this.flowLayoutPanel1.Controls.Add(this.label_regionCode);
            this.flowLayoutPanel1.Controls.Add(this.txt_pass);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.txt_name);
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
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Логин";
            // 
            // txt_login
            // 
            this.txt_login.Location = new System.Drawing.Point(3, 30);
            this.txt_login.Name = "txt_login";
            this.txt_login.Size = new System.Drawing.Size(134, 20);
            this.txt_login.TabIndex = 1;
            // 
            // label_regionCode
            // 
            this.label_regionCode.AutoSize = true;
            this.label_regionCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_regionCode.Location = new System.Drawing.Point(3, 58);
            this.label_regionCode.Margin = new System.Windows.Forms.Padding(3, 5, 3, 10);
            this.label_regionCode.Name = "label_regionCode";
            this.label_regionCode.Size = new System.Drawing.Size(57, 17);
            this.label_regionCode.TabIndex = 0;
            this.label_regionCode.Text = "Пароль";
            // 
            // txt_pass
            // 
            this.txt_pass.Location = new System.Drawing.Point(3, 88);
            this.txt_pass.Name = "txt_pass";
            this.txt_pass.Size = new System.Drawing.Size(134, 20);
            this.txt_pass.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 116);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "ФИО";
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(3, 146);
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(134, 20);
            this.txt_name.TabIndex = 1;
            // 
            // btn_save
            // 
            this.btn_save.Location = new System.Drawing.Point(6, 270);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(167, 36);
            this.btn_save.TabIndex = 2;
            this.btn_save.Text = "Добавить";
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
            this.groupBox1.Text = "Добавить пользователя";
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            this.id.Visible = false;
            // 
            // login
            // 
            this.login.DataPropertyName = "login";
            this.login.HeaderText = "Логин";
            this.login.Name = "login";
            this.login.ReadOnly = true;
            // 
            // pass
            // 
            this.pass.DataPropertyName = "pass";
            this.pass.HeaderText = "Пароль";
            this.pass.Name = "pass";
            this.pass.ReadOnly = true;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "ФИО";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 336);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgv_editUsers);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "users";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Управление пользователями";
            this.Load += new System.EventHandler(this.editDepot_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_editUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editUsersBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.editDepotBindingSource)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_editUsers;
        private System.Windows.Forms.BindingSource editDepotBindingSource;
        private DataSet1 dataSet1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_login;
        private System.Windows.Forms.Label label_regionCode;
        private System.Windows.Forms.TextBox txt_pass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.BindingSource editUsersBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn login;
        private System.Windows.Forms.DataGridViewTextBoxColumn pass;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
    }
}