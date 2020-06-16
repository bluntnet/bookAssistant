namespace BookAssistant {
    partial class DanDanForm {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.csvView = new System.Windows.Forms.DataGridView();
            this.openFile = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picChk = new System.Windows.Forms.CheckBox();
            this.contentChk = new System.Windows.Forms.CheckBox();
            this.eraseLogger = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.stopBtn = new System.Windows.Forms.Button();
            this.pauseBtn = new System.Windows.Forms.Button();
            this.logger = new System.Windows.Forms.TextBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.button9 = new System.Windows.Forms.Button();
            this.sameRecord = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.tbCsvPath = new System.Windows.Forms.TextBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.choseCSVfileBtn = new System.Windows.Forms.Button();
            this.tbRewrite = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.csvFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveCsvFile = new System.Windows.Forms.SaveFileDialog();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.dangdangAvailable = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.duplicateCSVOpenFile = new System.Windows.Forms.OpenFileDialog();
            this.rmDuplicateBtn = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.duplicateFileName = new System.Windows.Forms.TextBox();
            this.chkName = new System.Windows.Forms.CheckBox();
            this.chkPrice = new System.Windows.Forms.CheckBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button10 = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.csvView)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.sameRecord.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(745, 409);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.csvView);
            this.tabPage1.Controls.Add(this.openFile);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(737, 383);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "导入数据";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(6, 8);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(538, 21);
            this.textBox1.TabIndex = 3;
            // 
            // csvView
            // 
            this.csvView.AllowUserToAddRows = false;
            this.csvView.AllowUserToDeleteRows = false;
            this.csvView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.csvView.Location = new System.Drawing.Point(6, 35);
            this.csvView.Name = "csvView";
            this.csvView.RowTemplate.Height = 23;
            this.csvView.Size = new System.Drawing.Size(725, 343);
            this.csvView.TabIndex = 2;
            // 
            // openFile
            // 
            this.openFile.Location = new System.Drawing.Point(550, 7);
            this.openFile.Name = "openFile";
            this.openFile.Size = new System.Drawing.Size(181, 23);
            this.openFile.TabIndex = 0;
            this.openFile.Text = "选择文件CSV文件";
            this.openFile.UseVisualStyleBackColor = true;
            this.openFile.Click += new System.EventHandler(this.openFile_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.eraseLogger);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Controls.Add(this.stopBtn);
            this.tabPage2.Controls.Add(this.pauseBtn);
            this.tabPage2.Controls.Add(this.logger);
            this.tabPage2.Controls.Add(this.progressBar1);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(737, 383);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "抓取数据";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picChk);
            this.groupBox1.Controls.Add(this.contentChk);
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(725, 53);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "抓取选项";
            // 
            // picChk
            // 
            this.picChk.AutoSize = true;
            this.picChk.Checked = true;
            this.picChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.picChk.Location = new System.Drawing.Point(87, 20);
            this.picChk.Name = "picChk";
            this.picChk.Size = new System.Drawing.Size(72, 16);
            this.picChk.TabIndex = 15;
            this.picChk.Text = "抓取图片";
            this.picChk.UseVisualStyleBackColor = true;
            // 
            // contentChk
            // 
            this.contentChk.AutoSize = true;
            this.contentChk.Checked = true;
            this.contentChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.contentChk.Location = new System.Drawing.Point(15, 20);
            this.contentChk.Name = "contentChk";
            this.contentChk.Size = new System.Drawing.Size(72, 16);
            this.contentChk.TabIndex = 14;
            this.contentChk.Text = "抓取内容";
            this.contentChk.UseVisualStyleBackColor = true;
            // 
            // eraseLogger
            // 
            this.eraseLogger.Location = new System.Drawing.Point(252, 91);
            this.eraseLogger.Name = "eraseLogger";
            this.eraseLogger.Size = new System.Drawing.Size(75, 23);
            this.eraseLogger.TabIndex = 12;
            this.eraseLogger.Text = "清除日志";
            this.eraseLogger.UseVisualStyleBackColor = true;
            this.eraseLogger.Click += new System.EventHandler(this.eraseLogger_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(613, 91);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(118, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "保存失败结果";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(171, 91);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(75, 23);
            this.stopBtn.TabIndex = 10;
            this.stopBtn.Text = "停止";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // pauseBtn
            // 
            this.pauseBtn.Location = new System.Drawing.Point(90, 91);
            this.pauseBtn.Name = "pauseBtn";
            this.pauseBtn.Size = new System.Drawing.Size(75, 23);
            this.pauseBtn.TabIndex = 9;
            this.pauseBtn.Text = "暂停";
            this.pauseBtn.UseVisualStyleBackColor = true;
            this.pauseBtn.Click += new System.EventHandler(this.pauseBtn_Click);
            // 
            // logger
            // 
            this.logger.Location = new System.Drawing.Point(6, 120);
            this.logger.Multiline = true;
            this.logger.Name = "logger";
            this.logger.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.logger.Size = new System.Drawing.Size(725, 258);
            this.logger.TabIndex = 8;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 62);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(725, 23);
            this.progressBar1.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(489, 91);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(118, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "保存成功结果";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 91);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(78, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "抓取";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.groupBox5);
            this.tabPage4.Controls.Add(this.groupBox4);
            this.tabPage4.Controls.Add(this.sameRecord);
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(737, 383);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "特殊处理";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button9);
            this.groupBox4.Location = new System.Drawing.Point(6, 301);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(234, 77);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "描述的价格写入到宝贝价格";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(8, 35);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 0;
            this.button9.Text = "生成文件";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // sameRecord
            // 
            this.sameRecord.Controls.Add(this.label3);
            this.sameRecord.Controls.Add(this.button7);
            this.sameRecord.Controls.Add(this.button8);
            this.sameRecord.Controls.Add(this.tbCsvPath);
            this.sameRecord.Controls.Add(this.checkBox1);
            this.sameRecord.Controls.Add(this.checkBox5);
            this.sameRecord.Location = new System.Drawing.Point(6, 6);
            this.sameRecord.Name = "sameRecord";
            this.sameRecord.Size = new System.Drawing.Size(725, 107);
            this.sameRecord.TabIndex = 4;
            this.sameRecord.TabStop = false;
            this.sameRecord.Text = "去除重复";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(353, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "先在导入数据选项卡中导入原始文件，再导入需要去除重复的文件";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(563, 67);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 4;
            this.button7.Text = "选择文件";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(644, 67);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 0;
            this.button8.Text = "导出";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // tbCsvPath
            // 
            this.tbCsvPath.Location = new System.Drawing.Point(6, 67);
            this.tbCsvPath.Name = "tbCsvPath";
            this.tbCsvPath.Size = new System.Drawing.Size(547, 21);
            this.tbCsvPath.TabIndex = 5;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(6, 45);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(72, 16);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "宝贝名称";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Enabled = false;
            this.checkBox5.Location = new System.Drawing.Point(84, 45);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(72, 16);
            this.checkBox5.TabIndex = 2;
            this.checkBox5.Text = "宝贝价格";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.button6);
            this.groupBox3.Controls.Add(this.choseCSVfileBtn);
            this.groupBox3.Controls.Add(this.tbRewrite);
            this.groupBox3.Location = new System.Drawing.Point(6, 222);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(725, 73);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "导入文件重写\"商家编码\"";
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.Location = new System.Drawing.Point(6, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(605, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "先在导入数据界面导入淘宝助理格式的文件，然后再这里选择原始数据文件，就可以生成一个新的文件。";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(644, 37);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 2;
            this.button6.Text = "重写并导出";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // choseCSVfileBtn
            // 
            this.choseCSVfileBtn.Location = new System.Drawing.Point(563, 37);
            this.choseCSVfileBtn.Name = "choseCSVfileBtn";
            this.choseCSVfileBtn.Size = new System.Drawing.Size(75, 23);
            this.choseCSVfileBtn.TabIndex = 1;
            this.choseCSVfileBtn.Text = "选择文件";
            this.choseCSVfileBtn.UseVisualStyleBackColor = true;
            this.choseCSVfileBtn.Click += new System.EventHandler(this.choseCSVfileBtn_Click);
            // 
            // tbRewrite
            // 
            this.tbRewrite.Location = new System.Drawing.Point(6, 37);
            this.tbRewrite.Name = "tbRewrite";
            this.tbRewrite.Size = new System.Drawing.Size(547, 21);
            this.tbRewrite.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Location = new System.Drawing.Point(6, 119);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(725, 97);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "生成淘宝格式文件";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(563, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "在导入数据选项卡中导入第一行是书名，第二行是ISBN号的csv文件，生成可以导入到淘宝助里中的数据。";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(6, 52);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 0;
            this.button5.Text = "生成文件";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click_1);
            // 
            // saveCsvFile
            // 
            this.saveCsvFile.Filter = "csv文件|*.csv";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(108, 20);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 16);
            this.checkBox2.TabIndex = 1;
            this.checkBox2.Text = "亚马逊";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // dangdangAvailable
            // 
            this.dangdangAvailable.AutoSize = true;
            this.dangdangAvailable.Checked = true;
            this.dangdangAvailable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.dangdangAvailable.Location = new System.Drawing.Point(6, 20);
            this.dangdangAvailable.Name = "dangdangAvailable";
            this.dangdangAvailable.Size = new System.Drawing.Size(96, 16);
            this.dangdangAvailable.TabIndex = 0;
            this.dangdangAvailable.Text = "在当当网上抓";
            this.dangdangAvailable.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(6, 20);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(72, 16);
            this.checkBox3.TabIndex = 2;
            this.checkBox3.Text = "图书图片";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Location = new System.Drawing.Point(84, 20);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(72, 16);
            this.checkBox4.TabIndex = 3;
            this.checkBox4.Text = "图书说明";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // duplicateCSVOpenFile
            // 
            this.duplicateCSVOpenFile.FileName = "openFileDialog1";
            // 
            // rmDuplicateBtn
            // 
            this.rmDuplicateBtn.Location = new System.Drawing.Point(431, 42);
            this.rmDuplicateBtn.Name = "rmDuplicateBtn";
            this.rmDuplicateBtn.Size = new System.Drawing.Size(178, 23);
            this.rmDuplicateBtn.TabIndex = 4;
            this.rmDuplicateBtn.Text = "选择需要去除重复的数据文件";
            this.rmDuplicateBtn.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(615, 42);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(104, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "导出";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // duplicateFileName
            // 
            this.duplicateFileName.Location = new System.Drawing.Point(6, 42);
            this.duplicateFileName.Name = "duplicateFileName";
            this.duplicateFileName.Size = new System.Drawing.Size(419, 21);
            this.duplicateFileName.TabIndex = 5;
            this.duplicateFileName.Text = "ddddddddddddd";
            // 
            // chkName
            // 
            this.chkName.AutoSize = true;
            this.chkName.Checked = true;
            this.chkName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkName.Location = new System.Drawing.Point(6, 20);
            this.chkName.Name = "chkName";
            this.chkName.Size = new System.Drawing.Size(72, 16);
            this.chkName.TabIndex = 1;
            this.chkName.Text = "宝贝名称";
            this.chkName.UseVisualStyleBackColor = true;
            // 
            // chkPrice
            // 
            this.chkPrice.AutoSize = true;
            this.chkPrice.Enabled = false;
            this.chkPrice.Location = new System.Drawing.Point(84, 20);
            this.chkPrice.Name = "chkPrice";
            this.chkPrice.Size = new System.Drawing.Size(72, 16);
            this.chkPrice.TabIndex = 2;
            this.chkPrice.Text = "宝贝价格";
            this.chkPrice.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button10);
            this.groupBox5.Location = new System.Drawing.Point(246, 301);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(200, 76);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "生成不重复的文件";
            // 
            // button10
            // 
            this.button10.Location = new System.Drawing.Point(6, 20);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(75, 23);
            this.button10.TabIndex = 0;
            this.button10.Text = "button10";
            this.button10.UseVisualStyleBackColor = true;
            this.button10.Click += new System.EventHandler(this.button10_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 433);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "当当网书店";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.csvView)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.sameRecord.ResumeLayout(false);
            this.sameRecord.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button openFile;
        private System.Windows.Forms.OpenFileDialog csvFileDialog;
        private System.Windows.Forms.DataGridView csvView;
        private System.Windows.Forms.SaveFileDialog saveCsvFile;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox dangdangAvailable;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.TextBox logger;
        private System.Windows.Forms.Button pauseBtn;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button eraseLogger;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox picChk;
        private System.Windows.Forms.CheckBox contentChk;
        private System.Windows.Forms.OpenFileDialog duplicateCSVOpenFile;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tbRewrite;
        private System.Windows.Forms.Button choseCSVfileBtn;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox sameRecord;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox tbCsvPath;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Button rmDuplicateBtn;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox duplicateFileName;
        private System.Windows.Forms.CheckBox chkName;
        private System.Windows.Forms.CheckBox chkPrice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button10;

    }
}

