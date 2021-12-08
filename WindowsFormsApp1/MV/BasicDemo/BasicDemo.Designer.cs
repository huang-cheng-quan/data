namespace BasicDemo
{
    partial class MV_CameraSettingForm
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
            this.cbDeviceList = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bnClose = new System.Windows.Forms.Button();
            this.bnOpen = new System.Windows.Forms.Button();
            this.bnEnum = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bnTriggerExec = new System.Windows.Forms.Button();
            this.cbSoftTrigger = new System.Windows.Forms.CheckBox();
            this.bnStopGrab = new System.Windows.Forms.Button();
            this.bnStartGrab = new System.Windows.Forms.Button();
            this.bnTriggerMode = new System.Windows.Forms.RadioButton();
            this.bnContinuesMode = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.bnSavePng = new System.Windows.Forms.Button();
            this.bnSaveTiff = new System.Windows.Forms.Button();
            this.bnSaveJpg = new System.Windows.Forms.Button();
            this.bnSaveBmp = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.bnSetParam = new System.Windows.Forms.Button();
            this.bnGetParam = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbFrameRate = new System.Windows.Forms.TextBox();
            this.tbGain = new System.Windows.Forms.TextBox();
            this.tbExposure = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txt_LongSideB = new System.Windows.Forms.TextBox();
            this.btn_LongSideB = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_ShortSideB = new System.Windows.Forms.TextBox();
            this.btn_ShortSideB = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_Positive = new System.Windows.Forms.TextBox();
            this.btn_Positive = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txt_LongSideA = new System.Windows.Forms.TextBox();
            this.btn_LongSideA = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_Negative = new System.Windows.Forms.TextBox();
            this.btn_Negative = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_ShortSideA = new System.Windows.Forms.TextBox();
            this.btn_ShortSideA = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_ConnectorPositive = new System.Windows.Forms.TextBox();
            this.btn_ConnectorPositive = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_FeedPositive = new System.Windows.Forms.TextBox();
            this.btn_FeedPositive = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_ConnectorNegative = new System.Windows.Forms.TextBox();
            this.btn_ConnectorNegative = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_CamFeedBelt = new System.Windows.Forms.TextBox();
            this.btn_SetCam_FeedBelt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbDeviceList
            // 
            this.cbDeviceList.FormattingEnabled = true;
            this.cbDeviceList.Location = new System.Drawing.Point(17, 12);
            this.cbDeviceList.Name = "cbDeviceList";
            this.cbDeviceList.Size = new System.Drawing.Size(632, 20);
            this.cbDeviceList.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBox1.Location = new System.Drawing.Point(17, 49);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(632, 483);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bnClose);
            this.groupBox1.Controls.Add(this.bnOpen);
            this.groupBox1.Controls.Add(this.bnEnum);
            this.groupBox1.Location = new System.Drawing.Point(666, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(243, 109);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "初始化";
            // 
            // bnClose
            // 
            this.bnClose.Enabled = false;
            this.bnClose.Location = new System.Drawing.Point(142, 69);
            this.bnClose.Name = "bnClose";
            this.bnClose.Size = new System.Drawing.Size(88, 23);
            this.bnClose.TabIndex = 2;
            this.bnClose.Text = "关闭相机";
            this.bnClose.UseVisualStyleBackColor = true;
            this.bnClose.Click += new System.EventHandler(this.bnClose_Click);
            // 
            // bnOpen
            // 
            this.bnOpen.Location = new System.Drawing.Point(20, 69);
            this.bnOpen.Name = "bnOpen";
            this.bnOpen.Size = new System.Drawing.Size(83, 23);
            this.bnOpen.TabIndex = 1;
            this.bnOpen.Text = "打开相机";
            this.bnOpen.UseVisualStyleBackColor = true;
            this.bnOpen.Click += new System.EventHandler(this.bnOpen_Click);
            // 
            // bnEnum
            // 
            this.bnEnum.Location = new System.Drawing.Point(20, 20);
            this.bnEnum.Name = "bnEnum";
            this.bnEnum.Size = new System.Drawing.Size(210, 23);
            this.bnEnum.TabIndex = 0;
            this.bnEnum.Text = "查找设备";
            this.bnEnum.UseVisualStyleBackColor = true;
            this.bnEnum.Click += new System.EventHandler(this.bnEnum_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bnTriggerExec);
            this.groupBox2.Controls.Add(this.cbSoftTrigger);
            this.groupBox2.Controls.Add(this.bnStopGrab);
            this.groupBox2.Controls.Add(this.bnStartGrab);
            this.groupBox2.Controls.Add(this.bnTriggerMode);
            this.groupBox2.Controls.Add(this.bnContinuesMode);
            this.groupBox2.Location = new System.Drawing.Point(666, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(243, 134);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "图像获取";
            // 
            // bnTriggerExec
            // 
            this.bnTriggerExec.Enabled = false;
            this.bnTriggerExec.Location = new System.Drawing.Point(150, 99);
            this.bnTriggerExec.Name = "bnTriggerExec";
            this.bnTriggerExec.Size = new System.Drawing.Size(87, 23);
            this.bnTriggerExec.TabIndex = 5;
            this.bnTriggerExec.Text = "触发一次";
            this.bnTriggerExec.UseVisualStyleBackColor = true;
            this.bnTriggerExec.Click += new System.EventHandler(this.bnTriggerExec_Click);
            // 
            // cbSoftTrigger
            // 
            this.cbSoftTrigger.AutoSize = true;
            this.cbSoftTrigger.Enabled = false;
            this.cbSoftTrigger.Location = new System.Drawing.Point(6, 103);
            this.cbSoftTrigger.Name = "cbSoftTrigger";
            this.cbSoftTrigger.Size = new System.Drawing.Size(60, 16);
            this.cbSoftTrigger.TabIndex = 4;
            this.cbSoftTrigger.Text = "软触发";
            this.cbSoftTrigger.UseVisualStyleBackColor = true;
            this.cbSoftTrigger.CheckedChanged += new System.EventHandler(this.cbSoftTrigger_CheckedChanged);
            // 
            // bnStopGrab
            // 
            this.bnStopGrab.Enabled = false;
            this.bnStopGrab.Location = new System.Drawing.Point(142, 62);
            this.bnStopGrab.Name = "bnStopGrab";
            this.bnStopGrab.Size = new System.Drawing.Size(95, 23);
            this.bnStopGrab.TabIndex = 3;
            this.bnStopGrab.Text = "停止";
            this.bnStopGrab.UseVisualStyleBackColor = true;
            this.bnStopGrab.Click += new System.EventHandler(this.bnStopGrab_Click);
            // 
            // bnStartGrab
            // 
            this.bnStartGrab.Enabled = false;
            this.bnStartGrab.Location = new System.Drawing.Point(20, 62);
            this.bnStartGrab.Name = "bnStartGrab";
            this.bnStartGrab.Size = new System.Drawing.Size(83, 23);
            this.bnStartGrab.TabIndex = 2;
            this.bnStartGrab.Text = "开始";
            this.bnStartGrab.UseVisualStyleBackColor = true;
            this.bnStartGrab.Click += new System.EventHandler(this.bnStartGrab_Click);
            // 
            // bnTriggerMode
            // 
            this.bnTriggerMode.AutoSize = true;
            this.bnTriggerMode.Enabled = false;
            this.bnTriggerMode.Location = new System.Drawing.Point(142, 26);
            this.bnTriggerMode.Name = "bnTriggerMode";
            this.bnTriggerMode.Size = new System.Drawing.Size(71, 16);
            this.bnTriggerMode.TabIndex = 1;
            this.bnTriggerMode.TabStop = true;
            this.bnTriggerMode.Text = "触发方式";
            this.bnTriggerMode.UseMnemonic = false;
            this.bnTriggerMode.UseVisualStyleBackColor = true;
            this.bnTriggerMode.CheckedChanged += new System.EventHandler(this.bnTriggerMode_CheckedChanged);
            // 
            // bnContinuesMode
            // 
            this.bnContinuesMode.AutoSize = true;
            this.bnContinuesMode.Enabled = false;
            this.bnContinuesMode.Location = new System.Drawing.Point(20, 26);
            this.bnContinuesMode.Name = "bnContinuesMode";
            this.bnContinuesMode.Size = new System.Drawing.Size(71, 16);
            this.bnContinuesMode.TabIndex = 0;
            this.bnContinuesMode.TabStop = true;
            this.bnContinuesMode.Text = "连续采集";
            this.bnContinuesMode.UseVisualStyleBackColor = true;
            this.bnContinuesMode.CheckedChanged += new System.EventHandler(this.bnContinuesMode_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.bnSavePng);
            this.groupBox3.Controls.Add(this.bnSaveTiff);
            this.groupBox3.Controls.Add(this.bnSaveJpg);
            this.groupBox3.Controls.Add(this.bnSaveBmp);
            this.groupBox3.Location = new System.Drawing.Point(666, 272);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(243, 86);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "图像保存";
            // 
            // bnSavePng
            // 
            this.bnSavePng.Location = new System.Drawing.Point(142, 57);
            this.bnSavePng.Name = "bnSavePng";
            this.bnSavePng.Size = new System.Drawing.Size(95, 23);
            this.bnSavePng.TabIndex = 2;
            this.bnSavePng.Text = "保存PNG";
            this.bnSavePng.UseVisualStyleBackColor = true;
            this.bnSavePng.Click += new System.EventHandler(this.bnSavePng_Click);
            // 
            // bnSaveTiff
            // 
            this.bnSaveTiff.Location = new System.Drawing.Point(20, 57);
            this.bnSaveTiff.Name = "bnSaveTiff";
            this.bnSaveTiff.Size = new System.Drawing.Size(92, 23);
            this.bnSaveTiff.TabIndex = 1;
            this.bnSaveTiff.Text = "保存TIFF";
            this.bnSaveTiff.UseVisualStyleBackColor = true;
            this.bnSaveTiff.Click += new System.EventHandler(this.bnSaveTiff_Click);
            // 
            // bnSaveJpg
            // 
            this.bnSaveJpg.Enabled = false;
            this.bnSaveJpg.Location = new System.Drawing.Point(142, 20);
            this.bnSaveJpg.Name = "bnSaveJpg";
            this.bnSaveJpg.Size = new System.Drawing.Size(95, 23);
            this.bnSaveJpg.TabIndex = 0;
            this.bnSaveJpg.Text = "保存JPG";
            this.bnSaveJpg.UseVisualStyleBackColor = true;
            this.bnSaveJpg.Click += new System.EventHandler(this.bnSaveJpg_Click);
            // 
            // bnSaveBmp
            // 
            this.bnSaveBmp.Enabled = false;
            this.bnSaveBmp.Location = new System.Drawing.Point(20, 20);
            this.bnSaveBmp.Name = "bnSaveBmp";
            this.bnSaveBmp.Size = new System.Drawing.Size(92, 23);
            this.bnSaveBmp.TabIndex = 0;
            this.bnSaveBmp.Text = "保存BMP";
            this.bnSaveBmp.UseVisualStyleBackColor = true;
            this.bnSaveBmp.Click += new System.EventHandler(this.bnSaveBmp_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.bnSetParam);
            this.groupBox4.Controls.Add(this.bnGetParam);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.tbFrameRate);
            this.groupBox4.Controls.Add(this.tbGain);
            this.groupBox4.Controls.Add(this.tbExposure);
            this.groupBox4.Location = new System.Drawing.Point(666, 364);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(243, 168);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "参数设置";
            // 
            // bnSetParam
            // 
            this.bnSetParam.Enabled = false;
            this.bnSetParam.Location = new System.Drawing.Point(137, 137);
            this.bnSetParam.Name = "bnSetParam";
            this.bnSetParam.Size = new System.Drawing.Size(93, 23);
            this.bnSetParam.TabIndex = 7;
            this.bnSetParam.Text = "设置参数";
            this.bnSetParam.UseVisualStyleBackColor = true;
            this.bnSetParam.Click += new System.EventHandler(this.bnSetParam_Click);
            // 
            // bnGetParam
            // 
            this.bnGetParam.Enabled = false;
            this.bnGetParam.Location = new System.Drawing.Point(18, 137);
            this.bnGetParam.Name = "bnGetParam";
            this.bnGetParam.Size = new System.Drawing.Size(85, 23);
            this.bnGetParam.TabIndex = 6;
            this.bnGetParam.Text = "获得参数";
            this.bnGetParam.UseVisualStyleBackColor = true;
            this.bnGetParam.Click += new System.EventHandler(this.bnGetParam_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "帧率";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "增益";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "曝光时间";
            // 
            // tbFrameRate
            // 
            this.tbFrameRate.Enabled = false;
            this.tbFrameRate.Location = new System.Drawing.Point(137, 95);
            this.tbFrameRate.Name = "tbFrameRate";
            this.tbFrameRate.Size = new System.Drawing.Size(100, 21);
            this.tbFrameRate.TabIndex = 2;
            // 
            // tbGain
            // 
            this.tbGain.Enabled = false;
            this.tbGain.Location = new System.Drawing.Point(137, 59);
            this.tbGain.Name = "tbGain";
            this.tbGain.Size = new System.Drawing.Size(100, 21);
            this.tbGain.TabIndex = 1;
            // 
            // tbExposure
            // 
            this.tbExposure.Enabled = false;
            this.tbExposure.Location = new System.Drawing.Point(137, 25);
            this.tbExposure.Name = "tbExposure";
            this.tbExposure.Size = new System.Drawing.Size(100, 21);
            this.tbExposure.TabIndex = 0;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.txt_LongSideB);
            this.groupBox5.Controls.Add(this.btn_LongSideB);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.txt_ShortSideB);
            this.groupBox5.Controls.Add(this.btn_ShortSideB);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.txt_Positive);
            this.groupBox5.Controls.Add(this.btn_Positive);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txt_LongSideA);
            this.groupBox5.Controls.Add(this.btn_LongSideA);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.txt_Negative);
            this.groupBox5.Controls.Add(this.btn_Negative);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txt_ShortSideA);
            this.groupBox5.Controls.Add(this.btn_ShortSideA);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txt_ConnectorPositive);
            this.groupBox5.Controls.Add(this.btn_ConnectorPositive);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.txt_FeedPositive);
            this.groupBox5.Controls.Add(this.btn_FeedPositive);
            this.groupBox5.Controls.Add(this.label5);
            this.groupBox5.Controls.Add(this.txt_ConnectorNegative);
            this.groupBox5.Controls.Add(this.btn_ConnectorNegative);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Controls.Add(this.txt_CamFeedBelt);
            this.groupBox5.Controls.Add(this.btn_SetCam_FeedBelt);
            this.groupBox5.Location = new System.Drawing.Point(915, 12);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(485, 398);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "设置区域";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label13.Location = new System.Drawing.Point(6, 284);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 12);
            this.label13.TabIndex = 29;
            this.label13.Text = "侧面长边B相机：";
            // 
            // txt_LongSideB
            // 
            this.txt_LongSideB.Location = new System.Drawing.Point(135, 281);
            this.txt_LongSideB.Name = "txt_LongSideB";
            this.txt_LongSideB.Size = new System.Drawing.Size(251, 21);
            this.txt_LongSideB.TabIndex = 28;
            // 
            // btn_LongSideB
            // 
            this.btn_LongSideB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_LongSideB.Location = new System.Drawing.Point(402, 274);
            this.btn_LongSideB.Name = "btn_LongSideB";
            this.btn_LongSideB.Size = new System.Drawing.Size(77, 28);
            this.btn_LongSideB.TabIndex = 27;
            this.btn_LongSideB.Text = "确定";
            this.btn_LongSideB.UseVisualStyleBackColor = true;
            this.btn_LongSideB.Click += new System.EventHandler(this.btn_LongSideB_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label12.Location = new System.Drawing.Point(6, 185);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(95, 12);
            this.label12.TabIndex = 26;
            this.label12.Text = "侧面短边B相机：";
            // 
            // txt_ShortSideB
            // 
            this.txt_ShortSideB.Location = new System.Drawing.Point(135, 182);
            this.txt_ShortSideB.Name = "txt_ShortSideB";
            this.txt_ShortSideB.Size = new System.Drawing.Size(251, 21);
            this.txt_ShortSideB.TabIndex = 25;
            // 
            // btn_ShortSideB
            // 
            this.btn_ShortSideB.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_ShortSideB.Location = new System.Drawing.Point(402, 175);
            this.btn_ShortSideB.Name = "btn_ShortSideB";
            this.btn_ShortSideB.Size = new System.Drawing.Size(77, 28);
            this.btn_ShortSideB.TabIndex = 24;
            this.btn_ShortSideB.Text = "确定";
            this.btn_ShortSideB.UseVisualStyleBackColor = true;
            this.btn_ShortSideB.Click += new System.EventHandler(this.btn_ShortSideB_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label11.Location = new System.Drawing.Point(6, 317);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 23;
            this.label11.Text = "本体正面相机：";
            // 
            // txt_Positive
            // 
            this.txt_Positive.Location = new System.Drawing.Point(135, 314);
            this.txt_Positive.Name = "txt_Positive";
            this.txt_Positive.Size = new System.Drawing.Size(251, 21);
            this.txt_Positive.TabIndex = 22;
            // 
            // btn_Positive
            // 
            this.btn_Positive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Positive.Location = new System.Drawing.Point(402, 307);
            this.btn_Positive.Name = "btn_Positive";
            this.btn_Positive.Size = new System.Drawing.Size(77, 28);
            this.btn_Positive.TabIndex = 21;
            this.btn_Positive.Text = "确定";
            this.btn_Positive.UseVisualStyleBackColor = true;
            this.btn_Positive.Click += new System.EventHandler(this.btn_Positive_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label10.Location = new System.Drawing.Point(6, 251);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 12);
            this.label10.TabIndex = 20;
            this.label10.Text = "侧面长边A相机：";
            // 
            // txt_LongSideA
            // 
            this.txt_LongSideA.Location = new System.Drawing.Point(135, 248);
            this.txt_LongSideA.Name = "txt_LongSideA";
            this.txt_LongSideA.Size = new System.Drawing.Size(251, 21);
            this.txt_LongSideA.TabIndex = 19;
            // 
            // btn_LongSideA
            // 
            this.btn_LongSideA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_LongSideA.Location = new System.Drawing.Point(402, 241);
            this.btn_LongSideA.Name = "btn_LongSideA";
            this.btn_LongSideA.Size = new System.Drawing.Size(77, 28);
            this.btn_LongSideA.TabIndex = 18;
            this.btn_LongSideA.Text = "确定";
            this.btn_LongSideA.UseVisualStyleBackColor = true;
            this.btn_LongSideA.Click += new System.EventHandler(this.btn_LongSideA_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label9.Location = new System.Drawing.Point(6, 218);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 12);
            this.label9.TabIndex = 17;
            this.label9.Text = "本体反面/麦拉相机：";
            // 
            // txt_Negative
            // 
            this.txt_Negative.Location = new System.Drawing.Point(135, 215);
            this.txt_Negative.Name = "txt_Negative";
            this.txt_Negative.Size = new System.Drawing.Size(251, 21);
            this.txt_Negative.TabIndex = 16;
            // 
            // btn_Negative
            // 
            this.btn_Negative.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_Negative.Location = new System.Drawing.Point(402, 208);
            this.btn_Negative.Name = "btn_Negative";
            this.btn_Negative.Size = new System.Drawing.Size(77, 28);
            this.btn_Negative.TabIndex = 15;
            this.btn_Negative.Text = "确定";
            this.btn_Negative.UseVisualStyleBackColor = true;
            this.btn_Negative.Click += new System.EventHandler(this.btn_Negative_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label8.Location = new System.Drawing.Point(6, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 12);
            this.label8.TabIndex = 14;
            this.label8.Text = "侧面短边A相机：";
            // 
            // txt_ShortSideA
            // 
            this.txt_ShortSideA.Location = new System.Drawing.Point(135, 149);
            this.txt_ShortSideA.Name = "txt_ShortSideA";
            this.txt_ShortSideA.Size = new System.Drawing.Size(251, 21);
            this.txt_ShortSideA.TabIndex = 13;
            // 
            // btn_ShortSideA
            // 
            this.btn_ShortSideA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_ShortSideA.Location = new System.Drawing.Point(402, 142);
            this.btn_ShortSideA.Name = "btn_ShortSideA";
            this.btn_ShortSideA.Size = new System.Drawing.Size(77, 28);
            this.btn_ShortSideA.TabIndex = 12;
            this.btn_ShortSideA.Text = "确定";
            this.btn_ShortSideA.UseVisualStyleBackColor = true;
            this.btn_ShortSideA.Click += new System.EventHandler(this.btn_ShortSideA_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label7.Location = new System.Drawing.Point(6, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 12);
            this.label7.TabIndex = 11;
            this.label7.Text = "连接器金属表面相机：";
            // 
            // txt_ConnectorPositive
            // 
            this.txt_ConnectorPositive.Location = new System.Drawing.Point(135, 116);
            this.txt_ConnectorPositive.Name = "txt_ConnectorPositive";
            this.txt_ConnectorPositive.Size = new System.Drawing.Size(251, 21);
            this.txt_ConnectorPositive.TabIndex = 10;
            // 
            // btn_ConnectorPositive
            // 
            this.btn_ConnectorPositive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_ConnectorPositive.Location = new System.Drawing.Point(402, 109);
            this.btn_ConnectorPositive.Name = "btn_ConnectorPositive";
            this.btn_ConnectorPositive.Size = new System.Drawing.Size(77, 28);
            this.btn_ConnectorPositive.TabIndex = 9;
            this.btn_ConnectorPositive.Text = "确定";
            this.btn_ConnectorPositive.UseVisualStyleBackColor = true;
            this.btn_ConnectorPositive.Click += new System.EventHandler(this.btn_ConnectorPositive_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label6.Location = new System.Drawing.Point(6, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "本体定位相机：";
            // 
            // txt_FeedPositive
            // 
            this.txt_FeedPositive.Location = new System.Drawing.Point(135, 83);
            this.txt_FeedPositive.Name = "txt_FeedPositive";
            this.txt_FeedPositive.Size = new System.Drawing.Size(251, 21);
            this.txt_FeedPositive.TabIndex = 7;
            // 
            // btn_FeedPositive
            // 
            this.btn_FeedPositive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_FeedPositive.Location = new System.Drawing.Point(402, 76);
            this.btn_FeedPositive.Name = "btn_FeedPositive";
            this.btn_FeedPositive.Size = new System.Drawing.Size(77, 28);
            this.btn_FeedPositive.TabIndex = 6;
            this.btn_FeedPositive.Text = "确定";
            this.btn_FeedPositive.UseVisualStyleBackColor = true;
            this.btn_FeedPositive.Click += new System.EventHandler(this.btn_FeedPositive_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label5.Location = new System.Drawing.Point(6, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 12);
            this.label5.TabIndex = 5;
            this.label5.Text = "连接器检测钢片相机：";
            // 
            // txt_ConnectorNegative
            // 
            this.txt_ConnectorNegative.Location = new System.Drawing.Point(135, 50);
            this.txt_ConnectorNegative.Name = "txt_ConnectorNegative";
            this.txt_ConnectorNegative.Size = new System.Drawing.Size(251, 21);
            this.txt_ConnectorNegative.TabIndex = 4;
            // 
            // btn_ConnectorNegative
            // 
            this.btn_ConnectorNegative.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn_ConnectorNegative.Location = new System.Drawing.Point(402, 43);
            this.btn_ConnectorNegative.Name = "btn_ConnectorNegative";
            this.btn_ConnectorNegative.Size = new System.Drawing.Size(77, 28);
            this.btn_ConnectorNegative.TabIndex = 3;
            this.btn_ConnectorNegative.Text = "确定";
            this.btn_ConnectorNegative.UseVisualStyleBackColor = true;
            this.btn_ConnectorNegative.Click += new System.EventHandler(this.btn_ConnectorNegative_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(113, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "皮带上料点位相机：";
            // 
            // txt_CamFeedBelt
            // 
            this.txt_CamFeedBelt.Location = new System.Drawing.Point(135, 17);
            this.txt_CamFeedBelt.Name = "txt_CamFeedBelt";
            this.txt_CamFeedBelt.Size = new System.Drawing.Size(251, 21);
            this.txt_CamFeedBelt.TabIndex = 1;
            // 
            // btn_SetCam_FeedBelt
            // 
            this.btn_SetCam_FeedBelt.Location = new System.Drawing.Point(402, 10);
            this.btn_SetCam_FeedBelt.Name = "btn_SetCam_FeedBelt";
            this.btn_SetCam_FeedBelt.Size = new System.Drawing.Size(77, 28);
            this.btn_SetCam_FeedBelt.TabIndex = 0;
            this.btn_SetCam_FeedBelt.Text = "确定";
            this.btn_SetCam_FeedBelt.UseVisualStyleBackColor = true;
            this.btn_SetCam_FeedBelt.Click += new System.EventHandler(this.btn_SetCam_FeedBelt_Click);
            // 
            // MV_CameraSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 549);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbDeviceList);
            this.Name = "MV_CameraSettingForm";
            this.Text = "Machine Vision Camera SDK Basic Demo (C#)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDeviceList;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bnClose;
        private System.Windows.Forms.Button bnOpen;
        private System.Windows.Forms.Button bnEnum;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton bnTriggerMode;
        private System.Windows.Forms.RadioButton bnContinuesMode;
        private System.Windows.Forms.CheckBox cbSoftTrigger;
        private System.Windows.Forms.Button bnStopGrab;
        private System.Windows.Forms.Button bnStartGrab;
        private System.Windows.Forms.Button bnTriggerExec;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button bnSaveJpg;
        private System.Windows.Forms.Button bnSaveBmp;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox tbFrameRate;
        private System.Windows.Forms.TextBox tbGain;
        private System.Windows.Forms.TextBox tbExposure;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bnSetParam;
        private System.Windows.Forms.Button bnGetParam;
        private System.Windows.Forms.Button bnSavePng;
        private System.Windows.Forms.Button bnSaveTiff;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txt_LongSideB;
        private System.Windows.Forms.Button btn_LongSideB;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txt_ShortSideB;
        private System.Windows.Forms.Button btn_ShortSideB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_Positive;
        private System.Windows.Forms.Button btn_Positive;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txt_LongSideA;
        private System.Windows.Forms.Button btn_LongSideA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txt_Negative;
        private System.Windows.Forms.Button btn_Negative;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_ShortSideA;
        private System.Windows.Forms.Button btn_ShortSideA;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_ConnectorPositive;
        private System.Windows.Forms.Button btn_ConnectorPositive;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_FeedPositive;
        private System.Windows.Forms.Button btn_FeedPositive;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_ConnectorNegative;
        private System.Windows.Forms.Button btn_ConnectorNegative;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_CamFeedBelt;
        private System.Windows.Forms.Button btn_SetCam_FeedBelt;
    }
}

