namespace TaxManager
{
    partial class Main
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.gb_workTransport = new System.Windows.Forms.GroupBox();
            this.dgv_mainWindow = new System.Windows.Forms.DataGridView();
            this.addKm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.shiftcode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.distanceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kg = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.finishTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sort = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.takeShiftcodeDataBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet1 = new TaxManager.DataSet1();
            this.label_shiftcode = new System.Windows.Forms.Label();
            this.tb_shiftcode = new System.Windows.Forms.TextBox();
            this.btn_import_ttn = new System.Windows.Forms.Button();
            this.btn_saveTrip = new System.Windows.Forms.Button();
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.инструментыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.topMenuTariffButton = new System.Windows.Forms.ToolStripMenuItem();
            this.списокРЦToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.константыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.пользователиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.реестрToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.реестрПереданныхТТНToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.справочникАдресовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отчетыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.реестрТТНToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.реестрТТНВсеПоляToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.колвоРейсовВСуткахToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.импортToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.импортРеестраToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.импортТарифовToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.помощьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.обновитьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.версияПОToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cb_matrix = new System.Windows.Forms.CheckBox();
            this.dateTimePicker_matrixDate = new System.Windows.Forms.ComboBox();
            this.label_dateWork = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cb_update = new System.Windows.Forms.CheckBox();
            this.label_saved = new System.Windows.Forms.Label();
            this.rB_manualy = new System.Windows.Forms.RadioButton();
            this.rB_automatic = new System.Windows.Forms.RadioButton();
            this.groupBox_resultTax = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_addPointSum = new System.Windows.Forms.TextBox();
            this.txt_sumKm = new System.Windows.Forms.TextBox();
            this.txt_hourSum = new System.Windows.Forms.TextBox();
            this.txt_allsum = new System.Windows.Forms.TextBox();
            this.txt_addPointTariff = new System.Windows.Forms.TextBox();
            this.txt_tariffKm = new System.Windows.Forms.TextBox();
            this.txt_tariffHour = new System.Windows.Forms.TextBox();
            this.txt_fixToPay = new System.Windows.Forms.TextBox();
            this.txt_howKm = new System.Windows.Forms.TextBox();
            this.txt_howHour = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label_howAddPoint = new System.Windows.Forms.Label();
            this.label_fixTariff = new System.Windows.Forms.Label();
            this.label_kmToPay = new System.Windows.Forms.Label();
            this.label_hourToPay = new System.Windows.Forms.Label();
            this.groupBox_info = new System.Windows.Forms.GroupBox();
            this.btn_delCurTTN = new System.Windows.Forms.Button();
            this.info_dateDoc = new System.Windows.Forms.DateTimePicker();
            this.info_tripNumber = new System.Windows.Forms.TextBox();
            this.info_region = new System.Windows.Forms.TextBox();
            this.info_cityOrNot = new System.Windows.Forms.TextBox();
            this.info_addPoint = new System.Windows.Forms.TextBox();
            this.info_ttn = new System.Windows.Forms.TextBox();
            this.info_trailer = new System.Windows.Forms.TextBox();
            this.info_truckNumber = new System.Windows.Forms.TextBox();
            this.cb_ref = new System.Windows.Forms.CheckBox();
            this.labelAddPoint = new System.Windows.Forms.Label();
            this.label_ttnNumber = new System.Windows.Forms.Label();
            this.label_region = new System.Windows.Forms.Label();
            this.label_cityOrNot = new System.Windows.Forms.Label();
            this.label_dateDocument = new System.Windows.Forms.Label();
            this.label_tripNumber = new System.Windows.Forms.Label();
            this.label_trailerNumber = new System.Windows.Forms.Label();
            this.label_truckNumber = new System.Windows.Forms.Label();
            this.txt_registrName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cB_return = new System.Windows.Forms.CheckBox();
            this.gb_workTransport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_mainWindow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.takeShiftcodeDataBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).BeginInit();
            this.topMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox_resultTax.SuspendLayout();
            this.groupBox_info.SuspendLayout();
            this.SuspendLayout();
            // 
            // gb_workTransport
            // 
            this.gb_workTransport.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gb_workTransport.Controls.Add(this.dgv_mainWindow);
            this.gb_workTransport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gb_workTransport.Location = new System.Drawing.Point(6, 29);
            this.gb_workTransport.Name = "gb_workTransport";
            this.gb_workTransport.Size = new System.Drawing.Size(700, 662);
            this.gb_workTransport.TabIndex = 0;
            this.gb_workTransport.TabStop = false;
            this.gb_workTransport.Text = "Работа транспорта по маршруту";
            // 
            // dgv_mainWindow
            // 
            this.dgv_mainWindow.AllowUserToAddRows = false;
            this.dgv_mainWindow.AllowUserToDeleteRows = false;
            this.dgv_mainWindow.AllowUserToResizeRows = false;
            this.dgv_mainWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_mainWindow.AutoGenerateColumns = false;
            this.dgv_mainWindow.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_mainWindow.BackgroundColor = System.Drawing.Color.SeaShell;
            this.dgv_mainWindow.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_mainWindow.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.addKm,
            this.shiftcode,
            this.id,
            this.distanceDataGridViewTextBoxColumn,
            this.cityNameDataGridViewTextBoxColumn,
            this.addressCode,
            this.kg,
            this.pal,
            this.startTime,
            this.finishTime,
            this.sort});
            this.dgv_mainWindow.DataSource = this.takeShiftcodeDataBindingSource;
            this.dgv_mainWindow.Location = new System.Drawing.Point(3, 19);
            this.dgv_mainWindow.Name = "dgv_mainWindow";
            this.dgv_mainWindow.RowHeadersVisible = false;
            this.dgv_mainWindow.Size = new System.Drawing.Size(694, 637);
            this.dgv_mainWindow.TabIndex = 0;
            this.dgv_mainWindow.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_mainWindow_CellEndEdit);
            this.dgv_mainWindow.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_mainWindow_CellMouseDown);
            this.dgv_mainWindow.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dgv_mainWindow_DataError_1);
            this.dgv_mainWindow.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_mainWindow_EditingControlShowing);
            this.dgv_mainWindow.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_mainWindow_KeyDown);
            this.dgv_mainWindow.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dgv_mainWindow_MouseClick);
            // 
            // addKm
            // 
            this.addKm.DataPropertyName = "addKm";
            this.addKm.FillWeight = 78.90765F;
            this.addKm.HeaderText = "Объезд";
            this.addKm.Name = "addKm";
            // 
            // shiftcode
            // 
            this.shiftcode.DataPropertyName = "shiftcode";
            this.shiftcode.HeaderText = "shiftcode";
            this.shiftcode.Name = "shiftcode";
            this.shiftcode.Visible = false;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "id";
            this.id.Name = "id";
            this.id.Visible = false;
            // 
            // distanceDataGridViewTextBoxColumn
            // 
            this.distanceDataGridViewTextBoxColumn.DataPropertyName = "distance";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightGray;
            this.distanceDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.distanceDataGridViewTextBoxColumn.FillWeight = 69.70782F;
            this.distanceDataGridViewTextBoxColumn.HeaderText = "КМ";
            this.distanceDataGridViewTextBoxColumn.Name = "distanceDataGridViewTextBoxColumn";
            // 
            // cityNameDataGridViewTextBoxColumn
            // 
            this.cityNameDataGridViewTextBoxColumn.DataPropertyName = "cityName";
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.LightGray;
            this.cityNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.cityNameDataGridViewTextBoxColumn.FillWeight = 203.0457F;
            this.cityNameDataGridViewTextBoxColumn.HeaderText = "Адресс объекта";
            this.cityNameDataGridViewTextBoxColumn.Name = "cityNameDataGridViewTextBoxColumn";
            // 
            // addressCode
            // 
            this.addressCode.DataPropertyName = "addressCode";
            this.addressCode.HeaderText = "Номер";
            this.addressCode.Name = "addressCode";
            // 
            // kg
            // 
            this.kg.DataPropertyName = "kg";
            this.kg.FillWeight = 89.66778F;
            this.kg.HeaderText = "Тонн";
            this.kg.Name = "kg";
            // 
            // pal
            // 
            this.pal.DataPropertyName = "pal";
            this.pal.FillWeight = 89.66778F;
            this.pal.HeaderText = "Паллет";
            this.pal.Name = "pal";
            // 
            // startTime
            // 
            this.startTime.DataPropertyName = "startTime";
            dataGridViewCellStyle3.Format = "t";
            dataGridViewCellStyle3.NullValue = null;
            this.startTime.DefaultCellStyle = dataGridViewCellStyle3;
            this.startTime.FillWeight = 89.66778F;
            this.startTime.HeaderText = "Заезд";
            this.startTime.Name = "startTime";
            // 
            // finishTime
            // 
            this.finishTime.DataPropertyName = "finishTime";
            this.finishTime.FillWeight = 89.66778F;
            this.finishTime.HeaderText = "Выезд";
            this.finishTime.Name = "finishTime";
            // 
            // sort
            // 
            this.sort.DataPropertyName = "sort";
            this.sort.HeaderText = "sort";
            this.sort.Name = "sort";
            this.sort.Visible = false;
            // 
            // takeShiftcodeDataBindingSource
            // 
            this.takeShiftcodeDataBindingSource.DataMember = "takeShiftcodeData";
            this.takeShiftcodeDataBindingSource.DataSource = this.dataSet1;
            // 
            // dataSet1
            // 
            this.dataSet1.DataSetName = "DataSet1";
            this.dataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label_shiftcode
            // 
            this.label_shiftcode.AutoSize = true;
            this.label_shiftcode.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_shiftcode.Location = new System.Drawing.Point(3, 4);
            this.label_shiftcode.Name = "label_shiftcode";
            this.label_shiftcode.Size = new System.Drawing.Size(83, 17);
            this.label_shiftcode.TabIndex = 1;
            this.label_shiftcode.Text = "Номер ТТН";
            // 
            // tb_shiftcode
            // 
            this.tb_shiftcode.Location = new System.Drawing.Point(93, 4);
            this.tb_shiftcode.Name = "tb_shiftcode";
            this.tb_shiftcode.Size = new System.Drawing.Size(124, 20);
            this.tb_shiftcode.TabIndex = 2;
            this.tb_shiftcode.TextChanged += new System.EventHandler(this.tb_shiftcode_TextChanged);
            this.tb_shiftcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_shiftcode_KeyDown);
            // 
            // btn_import_ttn
            // 
            this.btn_import_ttn.Location = new System.Drawing.Point(223, 2);
            this.btn_import_ttn.Name = "btn_import_ttn";
            this.btn_import_ttn.Size = new System.Drawing.Size(115, 23);
            this.btn_import_ttn.TabIndex = 3;
            this.btn_import_ttn.Text = "Импорт ТТН";
            this.btn_import_ttn.UseVisualStyleBackColor = true;
            this.btn_import_ttn.Click += new System.EventHandler(this.btn_import_ttn_Click);
            // 
            // btn_saveTrip
            // 
            this.btn_saveTrip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_saveTrip.Location = new System.Drawing.Point(242, 634);
            this.btn_saveTrip.Name = "btn_saveTrip";
            this.btn_saveTrip.Size = new System.Drawing.Size(151, 57);
            this.btn_saveTrip.TabIndex = 4;
            this.btn_saveTrip.Text = "Сохранить";
            this.btn_saveTrip.UseVisualStyleBackColor = true;
            this.btn_saveTrip.Click += new System.EventHandler(this.btn_saveTrip_Click);
            // 
            // topMenu
            // 
            this.topMenu.BackColor = System.Drawing.Color.Silver;
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.инструментыToolStripMenuItem,
            this.отчетыToolStripMenuItem,
            this.импортToolStripMenuItem,
            this.оПрограммеToolStripMenuItem});
            this.topMenu.Location = new System.Drawing.Point(0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(1137, 24);
            this.topMenu.TabIndex = 5;
            this.topMenu.Text = "menuStrip1";
            // 
            // инструментыToolStripMenuItem
            // 
            this.инструментыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.topMenuTariffButton,
            this.списокРЦToolStripMenuItem,
            this.константыToolStripMenuItem,
            this.пользователиToolStripMenuItem,
            this.реестрToolStripMenuItem,
            this.реестрПереданныхТТНToolStripMenuItem,
            this.справочникАдресовToolStripMenuItem});
            this.инструментыToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("инструментыToolStripMenuItem.Image")));
            this.инструментыToolStripMenuItem.Name = "инструментыToolStripMenuItem";
            this.инструментыToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.инструментыToolStripMenuItem.Text = "Инструменты";
            // 
            // topMenuTariffButton
            // 
            this.topMenuTariffButton.Image = global::TaxManager.Properties.Resources.tariff1;
            this.topMenuTariffButton.Name = "topMenuTariffButton";
            this.topMenuTariffButton.Size = new System.Drawing.Size(206, 22);
            this.topMenuTariffButton.Text = "Тарифы";
            this.topMenuTariffButton.Click += new System.EventHandler(this.topMenuTariffButton_Click);
            // 
            // списокРЦToolStripMenuItem
            // 
            this.списокРЦToolStripMenuItem.Image = global::TaxManager.Properties.Resources.loading;
            this.списокРЦToolStripMenuItem.Name = "списокРЦToolStripMenuItem";
            this.списокРЦToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.списокРЦToolStripMenuItem.Text = "Список РЦ";
            this.списокРЦToolStripMenuItem.Click += new System.EventHandler(this.списокРЦToolStripMenuItem_Click);
            // 
            // константыToolStripMenuItem
            // 
            this.константыToolStripMenuItem.Image = global::TaxManager.Properties.Resources.fact;
            this.константыToolStripMenuItem.Name = "константыToolStripMenuItem";
            this.константыToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.константыToolStripMenuItem.Text = "Константы";
            this.константыToolStripMenuItem.Click += new System.EventHandler(this.константыToolStripMenuItem_Click);
            // 
            // пользователиToolStripMenuItem
            // 
            this.пользователиToolStripMenuItem.Image = global::TaxManager.Properties.Resources.users;
            this.пользователиToolStripMenuItem.Name = "пользователиToolStripMenuItem";
            this.пользователиToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.пользователиToolStripMenuItem.Text = "Пользователи";
            this.пользователиToolStripMenuItem.Click += new System.EventHandler(this.пользователиToolStripMenuItem_Click);
            // 
            // реестрToolStripMenuItem
            // 
            this.реестрToolStripMenuItem.Image = global::TaxManager.Properties.Resources.changestatus;
            this.реестрToolStripMenuItem.Name = "реестрToolStripMenuItem";
            this.реестрToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.реестрToolStripMenuItem.Text = "Реестр";
            this.реестрToolStripMenuItem.Click += new System.EventHandler(this.реестрToolStripMenuItem_Click);
            // 
            // реестрПереданныхТТНToolStripMenuItem
            // 
            this.реестрПереданныхТТНToolStripMenuItem.Image = global::TaxManager.Properties.Resources.product;
            this.реестрПереданныхТТНToolStripMenuItem.Name = "реестрПереданныхТТНToolStripMenuItem";
            this.реестрПереданныхТТНToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.реестрПереданныхТТНToolStripMenuItem.Text = "Реестр переданных ТТН";
            this.реестрПереданныхТТНToolStripMenuItem.Click += new System.EventHandler(this.реестрПереданныхТТНToolStripMenuItem_Click);
            // 
            // справочникАдресовToolStripMenuItem
            // 
            this.справочникАдресовToolStripMenuItem.Image = global::TaxManager.Properties.Resources.address;
            this.справочникАдресовToolStripMenuItem.Name = "справочникАдресовToolStripMenuItem";
            this.справочникАдресовToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.справочникАдресовToolStripMenuItem.Text = "Справочник адресов";
            this.справочникАдресовToolStripMenuItem.Click += new System.EventHandler(this.справочникАдресовToolStripMenuItem_Click);
            // 
            // отчетыToolStripMenuItem
            // 
            this.отчетыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.реестрТТНToolStripMenuItem,
            this.реестрТТНВсеПоляToolStripMenuItem,
            this.колвоРейсовВСуткахToolStripMenuItem});
            this.отчетыToolStripMenuItem.Image = global::TaxManager.Properties.Resources.orderGreed;
            this.отчетыToolStripMenuItem.Name = "отчетыToolStripMenuItem";
            this.отчетыToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            this.отчетыToolStripMenuItem.Text = "Отчеты";
            // 
            // реестрТТНToolStripMenuItem
            // 
            this.реестрТТНToolStripMenuItem.Image = global::TaxManager.Properties.Resources.orders;
            this.реестрТТНToolStripMenuItem.Name = "реестрТТНToolStripMenuItem";
            this.реестрТТНToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.реестрТТНToolStripMenuItem.Text = "Акты";
            this.реестрТТНToolStripMenuItem.Click += new System.EventHandler(this.реестрТТНToolStripMenuItem_Click);
            // 
            // реестрТТНВсеПоляToolStripMenuItem
            // 
            this.реестрТТНВсеПоляToolStripMenuItem.Image = global::TaxManager.Properties.Resources.shiftGridReport;
            this.реестрТТНВсеПоляToolStripMenuItem.Name = "реестрТТНВсеПоляToolStripMenuItem";
            this.реестрТТНВсеПоляToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.реестрТТНВсеПоляToolStripMenuItem.Text = "Реестр ТТН";
            this.реестрТТНВсеПоляToolStripMenuItem.Click += new System.EventHandler(this.реестрТТНВсеПоляToolStripMenuItem_Click);
            // 
            // колвоРейсовВСуткахToolStripMenuItem
            // 
            this.колвоРейсовВСуткахToolStripMenuItem.Image = global::TaxManager.Properties.Resources.count;
            this.колвоРейсовВСуткахToolStripMenuItem.Name = "колвоРейсовВСуткахToolStripMenuItem";
            this.колвоРейсовВСуткахToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.колвоРейсовВСуткахToolStripMenuItem.Text = "Кол-во рейсов в сутках";
            this.колвоРейсовВСуткахToolStripMenuItem.Click += new System.EventHandler(this.колвоРейсовВСуткахToolStripMenuItem_Click);
            // 
            // импортToolStripMenuItem
            // 
            this.импортToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.импортРеестраToolStripMenuItem,
            this.импортТарифовToolStripMenuItem});
            this.импортToolStripMenuItem.Image = global::TaxManager.Properties.Resources.import;
            this.импортToolStripMenuItem.Name = "импортToolStripMenuItem";
            this.импортToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.импортToolStripMenuItem.Text = "Импорт";
            // 
            // импортРеестраToolStripMenuItem
            // 
            this.импортРеестраToolStripMenuItem.Image = global::TaxManager.Properties.Resources.changestatus1;
            this.импортРеестраToolStripMenuItem.Name = "импортРеестраToolStripMenuItem";
            this.импортРеестраToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.импортРеестраToolStripMenuItem.Text = "Импорт реестра переданных ТТН";
            this.импортРеестраToolStripMenuItem.Click += new System.EventHandler(this.импортРеестраToolStripMenuItem_Click);
            // 
            // импортТарифовToolStripMenuItem
            // 
            this.импортТарифовToolStripMenuItem.Image = global::TaxManager.Properties.Resources.optimizator;
            this.импортТарифовToolStripMenuItem.Name = "импортТарифовToolStripMenuItem";
            this.импортТарифовToolStripMenuItem.Size = new System.Drawing.Size(259, 22);
            this.импортТарифовToolStripMenuItem.Text = "Импорт тарифов";
            this.импортТарифовToolStripMenuItem.Click += new System.EventHandler(this.импортТарифовToolStripMenuItem_Click);
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.помощьToolStripMenuItem,
            this.обновитьToolStripMenuItem,
            this.версияПОToolStripMenuItem});
            this.оПрограммеToolStripMenuItem.Image = global::TaxManager.Properties.Resources.about_program;
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(110, 20);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            // 
            // помощьToolStripMenuItem
            // 
            this.помощьToolStripMenuItem.Image = global::TaxManager.Properties.Resources.help1;
            this.помощьToolStripMenuItem.Name = "помощьToolStripMenuItem";
            this.помощьToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.помощьToolStripMenuItem.Text = "Помощь";
            this.помощьToolStripMenuItem.Click += new System.EventHandler(this.помощьToolStripMenuItem_Click);
            // 
            // обновитьToolStripMenuItem
            // 
            this.обновитьToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("обновитьToolStripMenuItem.Image")));
            this.обновитьToolStripMenuItem.Name = "обновитьToolStripMenuItem";
            this.обновитьToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.обновитьToolStripMenuItem.Text = "Обновить";
            this.обновитьToolStripMenuItem.Click += new System.EventHandler(this.обновитьToolStripMenuItem_Click);
            // 
            // версияПОToolStripMenuItem
            // 
            this.версияПОToolStripMenuItem.Image = global::TaxManager.Properties.Resources.about_program;
            this.версияПОToolStripMenuItem.Name = "версияПОToolStripMenuItem";
            this.версияПОToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.версияПОToolStripMenuItem.Text = "Версия ПО";
            this.версияПОToolStripMenuItem.Click += new System.EventHandler(this.версияПОToolStripMenuItem_Click_1);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.cb_matrix);
            this.panel1.Controls.Add(this.dateTimePicker_matrixDate);
            this.panel1.Controls.Add(this.gb_workTransport);
            this.panel1.Controls.Add(this.label_dateWork);
            this.panel1.Controls.Add(this.btn_import_ttn);
            this.panel1.Controls.Add(this.tb_shiftcode);
            this.panel1.Controls.Add(this.label_shiftcode);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 694);
            this.panel1.TabIndex = 6;
            // 
            // cb_matrix
            // 
            this.cb_matrix.AutoSize = true;
            this.cb_matrix.Checked = true;
            this.cb_matrix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_matrix.Location = new System.Drawing.Point(600, 5);
            this.cb_matrix.Name = "cb_matrix";
            this.cb_matrix.Size = new System.Drawing.Size(99, 17);
            this.cb_matrix.TabIndex = 7;
            this.cb_matrix.Text = "Согласно ТТН";
            this.cb_matrix.UseVisualStyleBackColor = true;
            this.cb_matrix.CheckedChanged += new System.EventHandler(this.cb_matrix_CheckedChanged);
            // 
            // dateTimePicker_matrixDate
            // 
            this.dateTimePicker_matrixDate.FormattingEnabled = true;
            this.dateTimePicker_matrixDate.Location = new System.Drawing.Point(469, 3);
            this.dateTimePicker_matrixDate.Name = "dateTimePicker_matrixDate";
            this.dateTimePicker_matrixDate.Size = new System.Drawing.Size(125, 21);
            this.dateTimePicker_matrixDate.TabIndex = 6;
            this.dateTimePicker_matrixDate.SelectedValueChanged += new System.EventHandler(this.dateTimePicker_matrixDate_ValueChanged);
            // 
            // label_dateWork
            // 
            this.label_dateWork.AutoSize = true;
            this.label_dateWork.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_dateWork.Location = new System.Drawing.Point(344, 4);
            this.label_dateWork.Name = "label_dateWork";
            this.label_dateWork.Size = new System.Drawing.Size(119, 17);
            this.label_dateWork.TabIndex = 5;
            this.label_dateWork.Text = "Матрица за дату";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 64.05038F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.94961F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 27);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1118, 700);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.cb_update);
            this.panel2.Controls.Add(this.label_saved);
            this.panel2.Controls.Add(this.rB_manualy);
            this.panel2.Controls.Add(this.rB_automatic);
            this.panel2.Controls.Add(this.groupBox_resultTax);
            this.panel2.Controls.Add(this.groupBox_info);
            this.panel2.Controls.Add(this.btn_saveTrip);
            this.panel2.Location = new System.Drawing.Point(719, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(396, 694);
            this.panel2.TabIndex = 7;
            // 
            // cb_update
            // 
            this.cb_update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cb_update.AutoSize = true;
            this.cb_update.Location = new System.Drawing.Point(204, 611);
            this.cb_update.Name = "cb_update";
            this.cb_update.Size = new System.Drawing.Size(189, 17);
            this.cb_update.TabIndex = 10;
            this.cb_update.Text = "Обновить дату и пользователя?";
            this.cb_update.UseVisualStyleBackColor = true;
            // 
            // label_saved
            // 
            this.label_saved.AutoSize = true;
            this.label_saved.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_saved.ForeColor = System.Drawing.Color.DarkOliveGreen;
            this.label_saved.Location = new System.Drawing.Point(14, 547);
            this.label_saved.Name = "label_saved";
            this.label_saved.Size = new System.Drawing.Size(109, 24);
            this.label_saved.TabIndex = 9;
            this.label_saved.Text = "Сохранено";
            // 
            // rB_manualy
            // 
            this.rB_manualy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rB_manualy.AutoSize = true;
            this.rB_manualy.Location = new System.Drawing.Point(94, 654);
            this.rB_manualy.Name = "rB_manualy";
            this.rB_manualy.Size = new System.Drawing.Size(67, 17);
            this.rB_manualy.TabIndex = 8;
            this.rB_manualy.Text = "Вручную";
            this.rB_manualy.UseVisualStyleBackColor = true;
            this.rB_manualy.CheckedChanged += new System.EventHandler(this.rB_manualy_CheckedChanged);
            // 
            // rB_automatic
            // 
            this.rB_automatic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rB_automatic.AutoSize = true;
            this.rB_automatic.Checked = true;
            this.rB_automatic.Location = new System.Drawing.Point(6, 654);
            this.rB_automatic.Name = "rB_automatic";
            this.rB_automatic.Size = new System.Drawing.Size(82, 17);
            this.rB_automatic.TabIndex = 8;
            this.rB_automatic.TabStop = true;
            this.rB_automatic.Text = "Автоматом";
            this.rB_automatic.UseVisualStyleBackColor = true;
            // 
            // groupBox_resultTax
            // 
            this.groupBox_resultTax.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_resultTax.Controls.Add(this.label4);
            this.groupBox_resultTax.Controls.Add(this.label3);
            this.groupBox_resultTax.Controls.Add(this.label2);
            this.groupBox_resultTax.Controls.Add(this.txt_addPointSum);
            this.groupBox_resultTax.Controls.Add(this.txt_sumKm);
            this.groupBox_resultTax.Controls.Add(this.txt_hourSum);
            this.groupBox_resultTax.Controls.Add(this.txt_allsum);
            this.groupBox_resultTax.Controls.Add(this.txt_addPointTariff);
            this.groupBox_resultTax.Controls.Add(this.txt_tariffKm);
            this.groupBox_resultTax.Controls.Add(this.txt_tariffHour);
            this.groupBox_resultTax.Controls.Add(this.txt_fixToPay);
            this.groupBox_resultTax.Controls.Add(this.txt_howKm);
            this.groupBox_resultTax.Controls.Add(this.txt_howHour);
            this.groupBox_resultTax.Controls.Add(this.label1);
            this.groupBox_resultTax.Controls.Add(this.label_howAddPoint);
            this.groupBox_resultTax.Controls.Add(this.label_fixTariff);
            this.groupBox_resultTax.Controls.Add(this.label_kmToPay);
            this.groupBox_resultTax.Controls.Add(this.label_hourToPay);
            this.groupBox_resultTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox_resultTax.Location = new System.Drawing.Point(3, 329);
            this.groupBox_resultTax.Name = "groupBox_resultTax";
            this.groupBox_resultTax.Size = new System.Drawing.Size(390, 199);
            this.groupBox_resultTax.TabIndex = 6;
            this.groupBox_resultTax.TabStop = false;
            this.groupBox_resultTax.Text = "Результат таксировки";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(282, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Сумма";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(210, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Тариф";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(138, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Кол-во";
            // 
            // txt_addPointSum
            // 
            this.txt_addPointSum.Enabled = false;
            this.txt_addPointSum.Location = new System.Drawing.Point(249, 129);
            this.txt_addPointSum.Name = "txt_addPointSum";
            this.txt_addPointSum.Size = new System.Drawing.Size(102, 23);
            this.txt_addPointSum.TabIndex = 1;
            // 
            // txt_sumKm
            // 
            this.txt_sumKm.Enabled = false;
            this.txt_sumKm.Location = new System.Drawing.Point(285, 71);
            this.txt_sumKm.Name = "txt_sumKm";
            this.txt_sumKm.Size = new System.Drawing.Size(66, 23);
            this.txt_sumKm.TabIndex = 1;
            // 
            // txt_hourSum
            // 
            this.txt_hourSum.Enabled = false;
            this.txt_hourSum.Location = new System.Drawing.Point(285, 42);
            this.txt_hourSum.Name = "txt_hourSum";
            this.txt_hourSum.Size = new System.Drawing.Size(66, 23);
            this.txt_hourSum.TabIndex = 1;
            // 
            // txt_allsum
            // 
            this.txt_allsum.Enabled = false;
            this.txt_allsum.Location = new System.Drawing.Point(141, 158);
            this.txt_allsum.Name = "txt_allsum";
            this.txt_allsum.Size = new System.Drawing.Size(102, 23);
            this.txt_allsum.TabIndex = 1;
            // 
            // txt_addPointTariff
            // 
            this.txt_addPointTariff.Enabled = false;
            this.txt_addPointTariff.Location = new System.Drawing.Point(141, 129);
            this.txt_addPointTariff.Name = "txt_addPointTariff";
            this.txt_addPointTariff.Size = new System.Drawing.Size(102, 23);
            this.txt_addPointTariff.TabIndex = 1;
            // 
            // txt_tariffKm
            // 
            this.txt_tariffKm.Enabled = false;
            this.txt_tariffKm.Location = new System.Drawing.Point(213, 71);
            this.txt_tariffKm.Name = "txt_tariffKm";
            this.txt_tariffKm.Size = new System.Drawing.Size(66, 23);
            this.txt_tariffKm.TabIndex = 1;
            // 
            // txt_tariffHour
            // 
            this.txt_tariffHour.Enabled = false;
            this.txt_tariffHour.Location = new System.Drawing.Point(213, 42);
            this.txt_tariffHour.Name = "txt_tariffHour";
            this.txt_tariffHour.Size = new System.Drawing.Size(66, 23);
            this.txt_tariffHour.TabIndex = 1;
            // 
            // txt_fixToPay
            // 
            this.txt_fixToPay.Enabled = false;
            this.txt_fixToPay.Location = new System.Drawing.Point(249, 100);
            this.txt_fixToPay.Name = "txt_fixToPay";
            this.txt_fixToPay.Size = new System.Drawing.Size(102, 23);
            this.txt_fixToPay.TabIndex = 1;
            // 
            // txt_howKm
            // 
            this.txt_howKm.Enabled = false;
            this.txt_howKm.Location = new System.Drawing.Point(141, 71);
            this.txt_howKm.Name = "txt_howKm";
            this.txt_howKm.Size = new System.Drawing.Size(66, 23);
            this.txt_howKm.TabIndex = 1;
            // 
            // txt_howHour
            // 
            this.txt_howHour.Enabled = false;
            this.txt_howHour.Location = new System.Drawing.Point(141, 42);
            this.txt_howHour.Name = "txt_howHour";
            this.txt_howHour.Size = new System.Drawing.Size(66, 23);
            this.txt_howHour.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Итого :";
            // 
            // label_howAddPoint
            // 
            this.label_howAddPoint.AutoSize = true;
            this.label_howAddPoint.Location = new System.Drawing.Point(11, 132);
            this.label_howAddPoint.Name = "label_howAddPoint";
            this.label_howAddPoint.Size = new System.Drawing.Size(89, 17);
            this.label_howAddPoint.TabIndex = 0;
            this.label_howAddPoint.Text = "Доп. точки :";
            // 
            // label_fixTariff
            // 
            this.label_fixTariff.AutoSize = true;
            this.label_fixTariff.Location = new System.Drawing.Point(11, 103);
            this.label_fixTariff.Name = "label_fixTariff";
            this.label_fixTariff.Size = new System.Drawing.Size(232, 17);
            this.label_fixTariff.TabIndex = 0;
            this.label_fixTariff.Text = "Фиксированный тариф к оплате :";
            // 
            // label_kmToPay
            // 
            this.label_kmToPay.AutoSize = true;
            this.label_kmToPay.Location = new System.Drawing.Point(11, 74);
            this.label_kmToPay.Name = "label_kmToPay";
            this.label_kmToPay.Size = new System.Drawing.Size(125, 17);
            this.label_kmToPay.TabIndex = 0;
            this.label_kmToPay.Text = "Пробег к оплате :";
            // 
            // label_hourToPay
            // 
            this.label_hourToPay.AutoSize = true;
            this.label_hourToPay.Location = new System.Drawing.Point(11, 45);
            this.label_hourToPay.Name = "label_hourToPay";
            this.label_hourToPay.Size = new System.Drawing.Size(113, 17);
            this.label_hourToPay.TabIndex = 0;
            this.label_hourToPay.Text = "Часы к оплате :";
            // 
            // groupBox_info
            // 
            this.groupBox_info.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox_info.Controls.Add(this.cB_return);
            this.groupBox_info.Controls.Add(this.btn_delCurTTN);
            this.groupBox_info.Controls.Add(this.info_dateDoc);
            this.groupBox_info.Controls.Add(this.info_tripNumber);
            this.groupBox_info.Controls.Add(this.info_region);
            this.groupBox_info.Controls.Add(this.info_cityOrNot);
            this.groupBox_info.Controls.Add(this.info_addPoint);
            this.groupBox_info.Controls.Add(this.info_ttn);
            this.groupBox_info.Controls.Add(this.info_trailer);
            this.groupBox_info.Controls.Add(this.info_truckNumber);
            this.groupBox_info.Controls.Add(this.cb_ref);
            this.groupBox_info.Controls.Add(this.labelAddPoint);
            this.groupBox_info.Controls.Add(this.label_ttnNumber);
            this.groupBox_info.Controls.Add(this.label_region);
            this.groupBox_info.Controls.Add(this.label_cityOrNot);
            this.groupBox_info.Controls.Add(this.label_dateDocument);
            this.groupBox_info.Controls.Add(this.label_tripNumber);
            this.groupBox_info.Controls.Add(this.label_trailerNumber);
            this.groupBox_info.Controls.Add(this.label_truckNumber);
            this.groupBox_info.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox_info.Location = new System.Drawing.Point(3, 5);
            this.groupBox_info.Name = "groupBox_info";
            this.groupBox_info.Size = new System.Drawing.Size(390, 318);
            this.groupBox_info.TabIndex = 5;
            this.groupBox_info.TabStop = false;
            this.groupBox_info.Text = "Общая информация";
            // 
            // btn_delCurTTN
            // 
            this.btn_delCurTTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_delCurTTN.BackColor = System.Drawing.SystemColors.Control;
            this.btn_delCurTTN.Location = new System.Drawing.Point(213, 227);
            this.btn_delCurTTN.Name = "btn_delCurTTN";
            this.btn_delCurTTN.Size = new System.Drawing.Size(151, 23);
            this.btn_delCurTTN.TabIndex = 11;
            this.btn_delCurTTN.Text = "Удалить";
            this.btn_delCurTTN.UseVisualStyleBackColor = false;
            this.btn_delCurTTN.Click += new System.EventHandler(this.btn_delCurTTN_Click);
            // 
            // info_dateDoc
            // 
            this.info_dateDoc.Location = new System.Drawing.Point(140, 111);
            this.info_dateDoc.Name = "info_dateDoc";
            this.info_dateDoc.Size = new System.Drawing.Size(194, 23);
            this.info_dateDoc.TabIndex = 4;
            this.info_dateDoc.ValueChanged += new System.EventHandler(this.info_dateDoc_ValueChanged);
            // 
            // info_tripNumber
            // 
            this.info_tripNumber.Enabled = false;
            this.info_tripNumber.Location = new System.Drawing.Point(140, 81);
            this.info_tripNumber.Name = "info_tripNumber";
            this.info_tripNumber.Size = new System.Drawing.Size(55, 23);
            this.info_tripNumber.TabIndex = 3;
            // 
            // info_region
            // 
            this.info_region.Enabled = false;
            this.info_region.Location = new System.Drawing.Point(140, 169);
            this.info_region.Name = "info_region";
            this.info_region.Size = new System.Drawing.Size(194, 23);
            this.info_region.TabIndex = 3;
            // 
            // info_cityOrNot
            // 
            this.info_cityOrNot.Enabled = false;
            this.info_cityOrNot.Location = new System.Drawing.Point(140, 140);
            this.info_cityOrNot.Name = "info_cityOrNot";
            this.info_cityOrNot.Size = new System.Drawing.Size(194, 23);
            this.info_cityOrNot.TabIndex = 3;
            // 
            // info_addPoint
            // 
            this.info_addPoint.Location = new System.Drawing.Point(140, 227);
            this.info_addPoint.Name = "info_addPoint";
            this.info_addPoint.Size = new System.Drawing.Size(55, 23);
            this.info_addPoint.TabIndex = 3;
            this.info_addPoint.TextChanged += new System.EventHandler(this.info_ttn_TextChanged);
            // 
            // info_ttn
            // 
            this.info_ttn.Enabled = false;
            this.info_ttn.Location = new System.Drawing.Point(140, 198);
            this.info_ttn.Name = "info_ttn";
            this.info_ttn.Size = new System.Drawing.Size(194, 23);
            this.info_ttn.TabIndex = 3;
            this.info_ttn.TextChanged += new System.EventHandler(this.info_ttn_TextChanged);
            // 
            // info_trailer
            // 
            this.info_trailer.Enabled = false;
            this.info_trailer.Location = new System.Drawing.Point(140, 52);
            this.info_trailer.Name = "info_trailer";
            this.info_trailer.Size = new System.Drawing.Size(194, 23);
            this.info_trailer.TabIndex = 3;
            // 
            // info_truckNumber
            // 
            this.info_truckNumber.Enabled = false;
            this.info_truckNumber.Location = new System.Drawing.Point(140, 22);
            this.info_truckNumber.Name = "info_truckNumber";
            this.info_truckNumber.Size = new System.Drawing.Size(194, 23);
            this.info_truckNumber.TabIndex = 3;
            // 
            // cb_ref
            // 
            this.cb_ref.AutoSize = true;
            this.cb_ref.Location = new System.Drawing.Point(14, 273);
            this.cb_ref.Name = "cb_ref";
            this.cb_ref.Size = new System.Drawing.Size(90, 21);
            this.cb_ref.TabIndex = 1;
            this.cb_ref.Text = "Режим +5";
            this.cb_ref.UseVisualStyleBackColor = true;
            // 
            // labelAddPoint
            // 
            this.labelAddPoint.AutoSize = true;
            this.labelAddPoint.Location = new System.Drawing.Point(11, 230);
            this.labelAddPoint.Name = "labelAddPoint";
            this.labelAddPoint.Size = new System.Drawing.Size(89, 17);
            this.labelAddPoint.TabIndex = 0;
            this.labelAddPoint.Text = "Доп. точки :";
            // 
            // label_ttnNumber
            // 
            this.label_ttnNumber.AutoSize = true;
            this.label_ttnNumber.Location = new System.Drawing.Point(11, 201);
            this.label_ttnNumber.Name = "label_ttnNumber";
            this.label_ttnNumber.Size = new System.Drawing.Size(91, 17);
            this.label_ttnNumber.TabIndex = 0;
            this.label_ttnNumber.Text = "Номер ТТН :";
            // 
            // label_region
            // 
            this.label_region.AutoSize = true;
            this.label_region.Location = new System.Drawing.Point(11, 172);
            this.label_region.Name = "label_region";
            this.label_region.Size = new System.Drawing.Size(62, 17);
            this.label_region.TabIndex = 0;
            this.label_region.Text = "Регион :";
            // 
            // label_cityOrNot
            // 
            this.label_cityOrNot.AutoSize = true;
            this.label_cityOrNot.Location = new System.Drawing.Point(11, 143);
            this.label_cityOrNot.Name = "label_cityOrNot";
            this.label_cityOrNot.Size = new System.Drawing.Size(123, 17);
            this.label_cityOrNot.TabIndex = 0;
            this.label_cityOrNot.Text = "Город/межгород :";
            // 
            // label_dateDocument
            // 
            this.label_dateDocument.AutoSize = true;
            this.label_dateDocument.Location = new System.Drawing.Point(11, 116);
            this.label_dateDocument.Name = "label_dateDocument";
            this.label_dateDocument.Size = new System.Drawing.Size(82, 17);
            this.label_dateDocument.TabIndex = 0;
            this.label_dateDocument.Text = "Дата ТТН :";
            // 
            // label_tripNumber
            // 
            this.label_tripNumber.AutoSize = true;
            this.label_tripNumber.Location = new System.Drawing.Point(11, 84);
            this.label_tripNumber.Name = "label_tripNumber";
            this.label_tripNumber.Size = new System.Drawing.Size(102, 17);
            this.label_tripNumber.TabIndex = 0;
            this.label_tripNumber.Text = "Номер рейса :";
            // 
            // label_trailerNumber
            // 
            this.label_trailerNumber.AutoSize = true;
            this.label_trailerNumber.Location = new System.Drawing.Point(11, 55);
            this.label_trailerNumber.Name = "label_trailerNumber";
            this.label_trailerNumber.Size = new System.Drawing.Size(119, 17);
            this.label_trailerNumber.TabIndex = 0;
            this.label_trailerNumber.Text = "Номер прицепа :";
            // 
            // label_truckNumber
            // 
            this.label_truckNumber.AutoSize = true;
            this.label_truckNumber.Location = new System.Drawing.Point(11, 26);
            this.label_truckNumber.Name = "label_truckNumber";
            this.label_truckNumber.Size = new System.Drawing.Size(93, 17);
            this.label_truckNumber.TabIndex = 0;
            this.label_truckNumber.Text = "Номер авто :";
            // 
            // txt_registrName
            // 
            this.txt_registrName.Location = new System.Drawing.Point(788, 1);
            this.txt_registrName.Name = "txt_registrName";
            this.txt_registrName.Size = new System.Drawing.Size(69, 20);
            this.txt_registrName.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(697, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Номер реестра";
            // 
            // cB_return
            // 
            this.cB_return.AutoSize = true;
            this.cB_return.Location = new System.Drawing.Point(141, 273);
            this.cB_return.Name = "cB_return";
            this.cB_return.Size = new System.Drawing.Size(166, 21);
            this.cB_return.TabIndex = 12;
            this.cB_return.Text = "Возвраты + 30 минут";
            this.cB_return.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1137, 739);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_registrName);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.topMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.topMenu;
            this.MinimumSize = new System.Drawing.Size(1153, 777);
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaxManager - Главная";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Load += new System.EventHandler(this.Main_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.gb_workTransport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_mainWindow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.takeShiftcodeDataBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet1)).EndInit();
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.groupBox_resultTax.ResumeLayout(false);
            this.groupBox_resultTax.PerformLayout();
            this.groupBox_info.ResumeLayout(false);
            this.groupBox_info.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gb_workTransport;
        private System.Windows.Forms.Label label_shiftcode;
        private System.Windows.Forms.TextBox tb_shiftcode;
        private System.Windows.Forms.Button btn_import_ttn;
        private DataSet1 dataSet1;
        private System.Windows.Forms.Button btn_saveTrip;
        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem инструментыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem topMenuTariffButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label_dateWork;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox_info;
        private System.Windows.Forms.CheckBox cb_ref;
        private System.Windows.Forms.Label label_tripNumber;
        private System.Windows.Forms.Label label_trailerNumber;
        private System.Windows.Forms.Label label_truckNumber;
        private System.Windows.Forms.Label label_cityOrNot;
        private System.Windows.Forms.Label label_dateDocument;
        private System.Windows.Forms.Label label_ttnNumber;
        private System.Windows.Forms.Label label_region;
        private System.Windows.Forms.GroupBox groupBox_resultTax;
        private System.Windows.Forms.DataGridView dgv_mainWindow;
        private System.Windows.Forms.BindingSource takeShiftcodeDataBindingSource;
        private System.Windows.Forms.ComboBox dateTimePicker_matrixDate;
        private System.Windows.Forms.RadioButton rB_manualy;
        private System.Windows.Forms.RadioButton rB_automatic;
        private System.Windows.Forms.TextBox info_truckNumber;
        private System.Windows.Forms.DateTimePicker info_dateDoc;
        private System.Windows.Forms.TextBox info_tripNumber;
        private System.Windows.Forms.TextBox info_trailer;
        private System.Windows.Forms.TextBox info_region;
        private System.Windows.Forms.TextBox info_cityOrNot;
        private System.Windows.Forms.TextBox info_ttn;
        private System.Windows.Forms.TextBox info_addPoint;
        private System.Windows.Forms.Label labelAddPoint;
        private System.Windows.Forms.TextBox txt_addPointSum;
        private System.Windows.Forms.TextBox txt_sumKm;
        private System.Windows.Forms.TextBox txt_hourSum;
        private System.Windows.Forms.TextBox txt_allsum;
        private System.Windows.Forms.TextBox txt_addPointTariff;
        private System.Windows.Forms.TextBox txt_tariffKm;
        private System.Windows.Forms.TextBox txt_tariffHour;
        private System.Windows.Forms.TextBox txt_fixToPay;
        private System.Windows.Forms.TextBox txt_howKm;
        private System.Windows.Forms.TextBox txt_howHour;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_howAddPoint;
        private System.Windows.Forms.Label label_fixTariff;
        private System.Windows.Forms.Label label_kmToPay;
        private System.Windows.Forms.Label label_hourToPay;
        private System.Windows.Forms.CheckBox cb_matrix;
        private System.Windows.Forms.ToolStripMenuItem списокРЦToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem константыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отчетыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem реестрТТНToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem реестрТТНВсеПоляToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem пользователиToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn addKm;
        private System.Windows.Forms.DataGridViewTextBoxColumn shiftcode;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn distanceDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn kg;
        private System.Windows.Forms.DataGridViewTextBoxColumn pal;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn finishTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn sort;
        private System.Windows.Forms.ToolStripMenuItem реестрToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem справочникАдресовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem помощьToolStripMenuItem;
        private System.Windows.Forms.Label label_saved;
        private System.Windows.Forms.TextBox txt_registrName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolStripMenuItem обновитьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem импортToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem импортРеестраToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem реестрПереданныхТТНToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem импортТарифовToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem версияПОToolStripMenuItem;
        private System.Windows.Forms.CheckBox cb_update;
        private System.Windows.Forms.Button btn_delCurTTN;
        private System.Windows.Forms.ToolStripMenuItem колвоРейсовВСуткахToolStripMenuItem;
        private System.Windows.Forms.CheckBox cB_return;
    }
}

