
namespace WindowsFormsApp1.HalconTools
{
    partial class HalconTools_CreateModelForm
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
            this.cboRoiNo = new System.Windows.Forms.ComboBox();
            this.cboPolarity = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdoCircle = new System.Windows.Forms.RadioButton();
            this.rdoRect = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClrEraser = new System.Windows.Forms.Button();
            this.trbEraserSize = new System.Windows.Forms.TrackBar();
            this.label7 = new System.Windows.Forms.Label();
            this.btnDrawROI1 = new System.Windows.Forms.Button();
            this.btnTakePic = new System.Windows.Forms.Button();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.nudThreshold = new System.Windows.Forms.NumericUpDown();
            this.nudMinScore = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnSetEraser = new System.Windows.Forms.Button();
            this.btnDrawRect = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnSaveModel = new System.Windows.Forms.Button();
            this.btnOpenImg = new System.Windows.Forms.Button();
            this.txtPixelLocation = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txt_MinContrast = new System.Windows.Forms.TextBox();
            this.txt_Contrast = new System.Windows.Forms.TextBox();
            this.txt_AngleStep = new System.Windows.Forms.TextBox();
            this.txt_AngleExtent = new System.Windows.Forms.TextBox();
            this.txt_AngleStart = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cmb_Metric = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.cmb_Optimization = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_NumLevels = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbEraserSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinScore)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cboRoiNo
            // 
            this.cboRoiNo.FormattingEnabled = true;
            this.cboRoiNo.Items.AddRange(new object[] {
            "胶片1",
            "胶片2",
            "胶片3",
            "胶片4"});
            this.cboRoiNo.Location = new System.Drawing.Point(698, 75);
            this.cboRoiNo.Name = "cboRoiNo";
            this.cboRoiNo.Size = new System.Drawing.Size(94, 20);
            this.cboRoiNo.TabIndex = 79;
            // 
            // cboPolarity
            // 
            this.cboPolarity.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPolarity.FormattingEnabled = true;
            this.cboPolarity.Items.AddRange(new object[] {
            "黑底白点",
            "白底黑点"});
            this.cboPolarity.Location = new System.Drawing.Point(821, 470);
            this.cboPolarity.Margin = new System.Windows.Forms.Padding(2);
            this.cboPolarity.Name = "cboPolarity";
            this.cboPolarity.Size = new System.Drawing.Size(92, 22);
            this.cboPolarity.TabIndex = 77;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoCircle);
            this.groupBox1.Controls.Add(this.rdoRect);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClrEraser);
            this.groupBox1.Controls.Add(this.trbEraserSize);
            this.groupBox1.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(1145, 12);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(250, 189);
            this.groupBox1.TabIndex = 73;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "橡皮擦工具";
            // 
            // rdoCircle
            // 
            this.rdoCircle.AutoSize = true;
            this.rdoCircle.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoCircle.Location = new System.Drawing.Point(120, 114);
            this.rdoCircle.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rdoCircle.Name = "rdoCircle";
            this.rdoCircle.Size = new System.Drawing.Size(59, 19);
            this.rdoCircle.TabIndex = 4;
            this.rdoCircle.TabStop = true;
            this.rdoCircle.Text = "圆形";
            this.rdoCircle.UseVisualStyleBackColor = true;
            // 
            // rdoRect
            // 
            this.rdoRect.AutoSize = true;
            this.rdoRect.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdoRect.Location = new System.Drawing.Point(120, 81);
            this.rdoRect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.rdoRect.Name = "rdoRect";
            this.rdoRect.Size = new System.Drawing.Size(59, 19);
            this.rdoRect.TabIndex = 3;
            this.rdoRect.TabStop = true;
            this.rdoRect.Text = "矩形";
            this.rdoRect.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(15, 31);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 15);
            this.label4.TabIndex = 2;
            this.label4.Text = "橡皮擦大小:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(16, 82);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "橡皮擦形状:";
            // 
            // btnClrEraser
            // 
            this.btnClrEraser.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnClrEraser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnClrEraser.Location = new System.Drawing.Point(137, 146);
            this.btnClrEraser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnClrEraser.Name = "btnClrEraser";
            this.btnClrEraser.Size = new System.Drawing.Size(107, 38);
            this.btnClrEraser.TabIndex = 2;
            this.btnClrEraser.Text = "清除橡皮擦";
            this.btnClrEraser.UseVisualStyleBackColor = true;
            this.btnClrEraser.Click += new System.EventHandler(this.btnClrEraser_Click);
            // 
            // trbEraserSize
            // 
            this.trbEraserSize.Location = new System.Drawing.Point(120, 30);
            this.trbEraserSize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.trbEraserSize.Name = "trbEraserSize";
            this.trbEraserSize.Size = new System.Drawing.Size(122, 45);
            this.trbEraserSize.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(742, 474);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 15);
            this.label7.TabIndex = 64;
            this.label7.Text = "电池极性:";
            // 
            // btnDrawROI1
            // 
            this.btnDrawROI1.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawROI1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDrawROI1.Location = new System.Drawing.Point(842, 66);
            this.btnDrawROI1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDrawROI1.Name = "btnDrawROI1";
            this.btnDrawROI1.Size = new System.Drawing.Size(94, 38);
            this.btnDrawROI1.TabIndex = 65;
            this.btnDrawROI1.Text = "设置ROI";
            this.btnDrawROI1.UseVisualStyleBackColor = true;
            this.btnDrawROI1.Click += new System.EventHandler(this.btnDrawROI1_Click);
            // 
            // btnTakePic
            // 
            this.btnTakePic.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTakePic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTakePic.Location = new System.Drawing.Point(842, 12);
            this.btnTakePic.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTakePic.Name = "btnTakePic";
            this.btnTakePic.Size = new System.Drawing.Size(94, 38);
            this.btnTakePic.TabIndex = 66;
            this.btnTakePic.Text = "打开相机";
            this.btnTakePic.UseVisualStyleBackColor = true;
            this.btnTakePic.Click += new System.EventHandler(this.btnTakePic_Click);
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(12, 12);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(677, 602);
            this.hWindowControl1.TabIndex = 78;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(677, 602);
            this.hWindowControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.hWindowControl1_MouseMove);
            // 
            // nudThreshold
            // 
            this.nudThreshold.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudThreshold.Location = new System.Drawing.Point(821, 545);
            this.nudThreshold.Margin = new System.Windows.Forms.Padding(2);
            this.nudThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudThreshold.Name = "nudThreshold";
            this.nudThreshold.Size = new System.Drawing.Size(96, 24);
            this.nudThreshold.TabIndex = 75;
            this.nudThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudMinScore
            // 
            this.nudMinScore.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudMinScore.Location = new System.Drawing.Point(821, 507);
            this.nudMinScore.Margin = new System.Windows.Forms.Padding(2);
            this.nudMinScore.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinScore.Name = "nudMinScore";
            this.nudMinScore.Size = new System.Drawing.Size(96, 24);
            this.nudMinScore.TabIndex = 74;
            this.nudMinScore.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudMinScore.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(710, 548);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 15);
            this.label3.TabIndex = 69;
            this.label3.Text = "模板灰度阈值:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(695, 511);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 70;
            this.label2.Text = "模板最低匹配值:";
            // 
            // btnSetEraser
            // 
            this.btnSetEraser.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetEraser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSetEraser.Location = new System.Drawing.Point(1036, 390);
            this.btnSetEraser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSetEraser.Name = "btnSetEraser";
            this.btnSetEraser.Size = new System.Drawing.Size(94, 38);
            this.btnSetEraser.TabIndex = 71;
            this.btnSetEraser.Text = "设置掩膜";
            this.btnSetEraser.UseVisualStyleBackColor = true;
            this.btnSetEraser.Click += new System.EventHandler(this.btnSetEraser_Click);
            // 
            // btnDrawRect
            // 
            this.btnDrawRect.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawRect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDrawRect.Location = new System.Drawing.Point(702, 390);
            this.btnDrawRect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDrawRect.Name = "btnDrawRect";
            this.btnDrawRect.Size = new System.Drawing.Size(94, 38);
            this.btnDrawRect.TabIndex = 72;
            this.btnDrawRect.Text = "框选模板";
            this.btnDrawRect.UseVisualStyleBackColor = true;
            this.btnDrawRect.Click += new System.EventHandler(this.btnDrawRect_Click);
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTest.Location = new System.Drawing.Point(698, 594);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(94, 38);
            this.btnTest.TabIndex = 67;
            this.btnTest.Text = "检测测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSaveModel
            // 
            this.btnSaveModel.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveModel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveModel.Location = new System.Drawing.Point(844, 594);
            this.btnSaveModel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSaveModel.Name = "btnSaveModel";
            this.btnSaveModel.Size = new System.Drawing.Size(94, 38);
            this.btnSaveModel.TabIndex = 63;
            this.btnSaveModel.Text = "保存模板";
            this.btnSaveModel.UseVisualStyleBackColor = true;
            this.btnSaveModel.Click += new System.EventHandler(this.btnSaveModel_Click);
            // 
            // btnOpenImg
            // 
            this.btnOpenImg.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenImg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenImg.Location = new System.Drawing.Point(698, 12);
            this.btnOpenImg.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnOpenImg.Name = "btnOpenImg";
            this.btnOpenImg.Size = new System.Drawing.Size(94, 38);
            this.btnOpenImg.TabIndex = 68;
            this.btnOpenImg.Text = "打开图片";
            this.btnOpenImg.UseVisualStyleBackColor = true;
            this.btnOpenImg.Click += new System.EventHandler(this.btnOpenImg_Click);
            // 
            // txtPixelLocation
            // 
            this.txtPixelLocation.Location = new System.Drawing.Point(12, 619);
            this.txtPixelLocation.Margin = new System.Windows.Forms.Padding(2);
            this.txtPixelLocation.Name = "txtPixelLocation";
            this.txtPixelLocation.Size = new System.Drawing.Size(678, 21);
            this.txtPixelLocation.TabIndex = 76;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txt_MinContrast);
            this.groupBox2.Controls.Add(this.txt_Contrast);
            this.groupBox2.Controls.Add(this.txt_AngleStep);
            this.groupBox2.Controls.Add(this.txt_AngleExtent);
            this.groupBox2.Controls.Add(this.txt_AngleStart);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.cmb_Metric);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.cmb_Optimization);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmb_NumLevels);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(690, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(445, 264);
            this.groupBox2.TabIndex = 80;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "模板建立参数设置";
            // 
            // txt_MinContrast
            // 
            this.txt_MinContrast.Location = new System.Drawing.Point(284, 233);
            this.txt_MinContrast.Name = "txt_MinContrast";
            this.txt_MinContrast.Size = new System.Drawing.Size(156, 21);
            this.txt_MinContrast.TabIndex = 100;
            this.txt_MinContrast.Text = "auto";
            // 
            // txt_Contrast
            // 
            this.txt_Contrast.Location = new System.Drawing.Point(284, 206);
            this.txt_Contrast.Name = "txt_Contrast";
            this.txt_Contrast.Size = new System.Drawing.Size(156, 21);
            this.txt_Contrast.TabIndex = 99;
            this.txt_Contrast.Text = "auto";
            // 
            // txt_AngleStep
            // 
            this.txt_AngleStep.Location = new System.Drawing.Point(284, 116);
            this.txt_AngleStep.Name = "txt_AngleStep";
            this.txt_AngleStep.Size = new System.Drawing.Size(156, 21);
            this.txt_AngleStep.TabIndex = 98;
            this.txt_AngleStep.Text = "2";
            // 
            // txt_AngleExtent
            // 
            this.txt_AngleExtent.Location = new System.Drawing.Point(284, 86);
            this.txt_AngleExtent.Name = "txt_AngleExtent";
            this.txt_AngleExtent.Size = new System.Drawing.Size(156, 21);
            this.txt_AngleExtent.TabIndex = 97;
            this.txt_AngleExtent.Text = "360";
            // 
            // txt_AngleStart
            // 
            this.txt_AngleStart.Location = new System.Drawing.Point(284, 56);
            this.txt_AngleStart.Name = "txt_AngleStart";
            this.txt_AngleStart.Size = new System.Drawing.Size(156, 21);
            this.txt_AngleStart.TabIndex = 96;
            this.txt_AngleStart.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(5, 237);
            this.label13.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(231, 15);
            this.label13.TabIndex = 95;
            this.label13.Text = "(MinContrast)设置最小对比度:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(9, 207);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(175, 15);
            this.label12.TabIndex = 93;
            this.label12.Text = "(Contrast)设置对比度:";
            // 
            // cmb_Metric
            // 
            this.cmb_Metric.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Metric.FormattingEnabled = true;
            this.cmb_Metric.Items.AddRange(new object[] {
            "use_polarity",
            "ignore_color_polarity",
            "ignore_global_polarity",
            "ignore_local_polarity"});
            this.cmb_Metric.Location = new System.Drawing.Point(284, 174);
            this.cmb_Metric.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_Metric.Name = "cmb_Metric";
            this.cmb_Metric.Size = new System.Drawing.Size(156, 22);
            this.cmb_Metric.TabIndex = 92;
            this.cmb_Metric.Text = "use_polarity";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(9, 177);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(175, 15);
            this.label11.TabIndex = 91;
            this.label11.Text = "(Metric)匹配方法设置:";
            // 
            // cmb_Optimization
            // 
            this.cmb_Optimization.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_Optimization.FormattingEnabled = true;
            this.cmb_Optimization.Items.AddRange(new object[] {
            "auto",
            "no_pregeneration",
            "none",
            "point_reduction_high",
            "point_reduction_low",
            "point_reduction_medium",
            "pregeneration"});
            this.cmb_Optimization.Location = new System.Drawing.Point(284, 147);
            this.cmb_Optimization.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_Optimization.Name = "cmb_Optimization";
            this.cmb_Optimization.Size = new System.Drawing.Size(156, 22);
            this.cmb_Optimization.TabIndex = 90;
            this.cmb_Optimization.Text = "auto";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(9, 147);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(271, 15);
            this.label10.TabIndex = 89;
            this.label10.Text = "(Optimization)模板优化和创建方法:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(9, 117);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(199, 15);
            this.label9.TabIndex = 87;
            this.label9.Text = "(AngleStep)旋转角度步长:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(9, 87);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(255, 15);
            this.label8.TabIndex = 85;
            this.label8.Text = "(AngleExtent)模板旋转角度范围：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(9, 57);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(255, 15);
            this.label6.TabIndex = 83;
            this.label6.Text = "(AngleStart)模板旋转的起始角度:";
            // 
            // cmb_NumLevels
            // 
            this.cmb_NumLevels.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmb_NumLevels.FormattingEnabled = true;
            this.cmb_NumLevels.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmb_NumLevels.Location = new System.Drawing.Point(284, 24);
            this.cmb_NumLevels.Margin = new System.Windows.Forms.Padding(2);
            this.cmb_NumLevels.Name = "cmb_NumLevels";
            this.cmb_NumLevels.Size = new System.Drawing.Size(156, 22);
            this.cmb_NumLevels.TabIndex = 82;
            this.cmb_NumLevels.Text = "2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(9, 27);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(183, 15);
            this.label5.TabIndex = 81;
            this.label5.Text = "(NumLevels)金字塔层数:";
            // 
            // HalconTools_CreateModelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1438, 674);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cboRoiNo);
            this.Controls.Add(this.cboPolarity);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnDrawROI1);
            this.Controls.Add(this.btnTakePic);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.nudThreshold);
            this.Controls.Add(this.nudMinScore);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSetEraser);
            this.Controls.Add(this.btnDrawRect);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSaveModel);
            this.Controls.Add(this.btnOpenImg);
            this.Controls.Add(this.txtPixelLocation);
            this.Name = "HalconTools_CreateModelForm";
            this.Text = "HalconTools_CreateModelForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.HalconTools_CreateModelForm_FormClosed);
            this.Load += new System.EventHandler(this.HalconTools_CreateModelForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbEraserSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinScore)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRoiNo;
        private System.Windows.Forms.ComboBox cboPolarity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdoCircle;
        private System.Windows.Forms.RadioButton rdoRect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClrEraser;
        private System.Windows.Forms.TrackBar trbEraserSize;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnDrawROI1;
        private System.Windows.Forms.Button btnTakePic;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.NumericUpDown nudThreshold;
        private System.Windows.Forms.NumericUpDown nudMinScore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSetEraser;
        private System.Windows.Forms.Button btnDrawRect;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnSaveModel;
        private System.Windows.Forms.Button btnOpenImg;
        private System.Windows.Forms.TextBox txtPixelLocation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txt_MinContrast;
        private System.Windows.Forms.TextBox txt_Contrast;
        private System.Windows.Forms.TextBox txt_AngleStep;
        private System.Windows.Forms.TextBox txt_AngleExtent;
        private System.Windows.Forms.TextBox txt_AngleStart;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmb_Metric;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmb_Optimization;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmb_NumLevels;
        private System.Windows.Forms.Label label5;
    }
}