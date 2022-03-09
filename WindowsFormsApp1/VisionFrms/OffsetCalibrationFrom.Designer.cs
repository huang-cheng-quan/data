
namespace WindowsFormsApp1.VisionFrms
{
    partial class OffsetCalibrationFrom
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
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txt_modelWord_U = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_modelWord_Y = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txt_modelWord_X = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.trb_setLineWidth = new System.Windows.Forms.TrackBar();
            this.trb_setSigma = new System.Windows.Forms.TrackBar();
            this.trb_SetThreshold = new System.Windows.Forms.TrackBar();
            this.txt_MeasureWidthValue = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_sigmalValue = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_OnePixelAccuracy = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_SetOnePixelAccuracy = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_WordDistance = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Measurement = new System.Windows.Forms.Button();
            this.txt_ThresholdValue = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_DrwaRect = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.btnTakePic = new System.Windows.Forms.Button();
            this.btnOpenImg = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_GenCircleCenter = new System.Windows.Forms.Button();
            this.btn_SetPixelCoordinates = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_StartCalibration = new System.Windows.Forms.Button();
            this.txt_Angle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_CalNum = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txt_CircleCenterColumn = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txt_CircleCenterRow = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_SaveCalibrationResult = new System.Windows.Forms.Button();
            this.btn_SelectModel = new System.Windows.Forms.Button();
            this.btn_IsFitCircleCenter = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trb_setLineWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trb_setSigma)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trb_SetThreshold)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(3, 1);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(511, 461);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(511, 461);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_modelWord_U);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.txt_modelWord_Y);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.txt_modelWord_X);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.trb_setLineWidth);
            this.groupBox1.Controls.Add(this.trb_setSigma);
            this.groupBox1.Controls.Add(this.trb_SetThreshold);
            this.groupBox1.Controls.Add(this.txt_MeasureWidthValue);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_sigmalValue);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lbl_OnePixelAccuracy);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btn_SetOnePixelAccuracy);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_WordDistance);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btn_Measurement);
            this.groupBox1.Controls.Add(this.txt_ThresholdValue);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_DrwaRect);
            this.groupBox1.Location = new System.Drawing.Point(519, 56);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(317, 507);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "测量区域";
            // 
            // txt_modelWord_U
            // 
            this.txt_modelWord_U.Location = new System.Drawing.Point(123, 405);
            this.txt_modelWord_U.Name = "txt_modelWord_U";
            this.txt_modelWord_U.Size = new System.Drawing.Size(53, 21);
            this.txt_modelWord_U.TabIndex = 32;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(8, 408);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(95, 12);
            this.label17.TabIndex = 31;
            this.label17.Text = "模板实际坐标U：";
            // 
            // txt_modelWord_Y
            // 
            this.txt_modelWord_Y.Location = new System.Drawing.Point(123, 382);
            this.txt_modelWord_Y.Name = "txt_modelWord_Y";
            this.txt_modelWord_Y.Size = new System.Drawing.Size(53, 21);
            this.txt_modelWord_Y.TabIndex = 29;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(8, 385);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(95, 12);
            this.label15.TabIndex = 28;
            this.label15.Text = "模板实际坐标Y：";
            // 
            // txt_modelWord_X
            // 
            this.txt_modelWord_X.Location = new System.Drawing.Point(123, 359);
            this.txt_modelWord_X.Name = "txt_modelWord_X";
            this.txt_modelWord_X.Size = new System.Drawing.Size(53, 21);
            this.txt_modelWord_X.TabIndex = 26;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(8, 362);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(95, 12);
            this.label13.TabIndex = 25;
            this.label13.Text = "模板实际坐标X：";
            // 
            // trb_setLineWidth
            // 
            this.trb_setLineWidth.Location = new System.Drawing.Point(131, 159);
            this.trb_setLineWidth.Maximum = 1000;
            this.trb_setLineWidth.Minimum = 1;
            this.trb_setLineWidth.Name = "trb_setLineWidth";
            this.trb_setLineWidth.Size = new System.Drawing.Size(93, 45);
            this.trb_setLineWidth.TabIndex = 24;
            this.trb_setLineWidth.Value = 1;
            this.trb_setLineWidth.Scroll += new System.EventHandler(this.trb_setLineWidth_Scroll);
            // 
            // trb_setSigma
            // 
            this.trb_setSigma.Location = new System.Drawing.Point(131, 108);
            this.trb_setSigma.Maximum = 320;
            this.trb_setSigma.Minimum = 4;
            this.trb_setSigma.Name = "trb_setSigma";
            this.trb_setSigma.Size = new System.Drawing.Size(93, 45);
            this.trb_setSigma.TabIndex = 23;
            this.trb_setSigma.Value = 4;
            this.trb_setSigma.Scroll += new System.EventHandler(this.trb_setSigma_Scroll);
            // 
            // trb_SetThreshold
            // 
            this.trb_SetThreshold.Location = new System.Drawing.Point(132, 71);
            this.trb_SetThreshold.Maximum = 100;
            this.trb_SetThreshold.Minimum = 1;
            this.trb_SetThreshold.Name = "trb_SetThreshold";
            this.trb_SetThreshold.Size = new System.Drawing.Size(93, 45);
            this.trb_SetThreshold.TabIndex = 22;
            this.trb_SetThreshold.Value = 1;
            this.trb_SetThreshold.Scroll += new System.EventHandler(this.trb_SetThreshold_Scroll);
            // 
            // txt_MeasureWidthValue
            // 
            this.txt_MeasureWidthValue.Location = new System.Drawing.Point(100, 164);
            this.txt_MeasureWidthValue.Name = "txt_MeasureWidthValue";
            this.txt_MeasureWidthValue.Size = new System.Drawing.Size(26, 21);
            this.txt_MeasureWidthValue.TabIndex = 21;
            this.txt_MeasureWidthValue.Text = "40";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(8, 173);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(41, 12);
            this.label11.TabIndex = 20;
            this.label11.Text = "宽度：";
            // 
            // txt_sigmalValue
            // 
            this.txt_sigmalValue.Location = new System.Drawing.Point(99, 117);
            this.txt_sigmalValue.Name = "txt_sigmalValue";
            this.txt_sigmalValue.Size = new System.Drawing.Size(26, 21);
            this.txt_sigmalValue.TabIndex = 19;
            this.txt_sigmalValue.Text = "4";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(47, 12);
            this.label9.TabIndex = 18;
            this.label9.Text = "Sigma：";
            // 
            // lbl_OnePixelAccuracy
            // 
            this.lbl_OnePixelAccuracy.AutoSize = true;
            this.lbl_OnePixelAccuracy.Location = new System.Drawing.Point(90, 330);
            this.lbl_OnePixelAccuracy.Name = "lbl_OnePixelAccuracy";
            this.lbl_OnePixelAccuracy.Size = new System.Drawing.Size(35, 12);
            this.lbl_OnePixelAccuracy.TabIndex = 17;
            this.lbl_OnePixelAccuracy.Text = "Pixel";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 330);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 16;
            this.label10.Text = "单像素精度：";
            // 
            // btn_SetOnePixelAccuracy
            // 
            this.btn_SetOnePixelAccuracy.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SetOnePixelAccuracy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_SetOnePixelAccuracy.Location = new System.Drawing.Point(196, 463);
            this.btn_SetOnePixelAccuracy.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_SetOnePixelAccuracy.Name = "btn_SetOnePixelAccuracy";
            this.btn_SetOnePixelAccuracy.Size = new System.Drawing.Size(117, 38);
            this.btn_SetOnePixelAccuracy.TabIndex = 15;
            this.btn_SetOnePixelAccuracy.Text = "确定";
            this.btn_SetOnePixelAccuracy.UseVisualStyleBackColor = true;
            this.btn_SetOnePixelAccuracy.Click += new System.EventHandler(this.btn_SetOnePixelAccuracy_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 298);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 14;
            this.label4.Text = "mm";
            // 
            // txt_WordDistance
            // 
            this.txt_WordDistance.Location = new System.Drawing.Point(73, 295);
            this.txt_WordDistance.Name = "txt_WordDistance";
            this.txt_WordDistance.Size = new System.Drawing.Size(53, 21);
            this.txt_WordDistance.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 12;
            this.label5.Text = "实际尺寸：";
            // 
            // btn_Measurement
            // 
            this.btn_Measurement.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Measurement.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_Measurement.Location = new System.Drawing.Point(60, 251);
            this.btn_Measurement.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_Measurement.Name = "btn_Measurement";
            this.btn_Measurement.Size = new System.Drawing.Size(117, 38);
            this.btn_Measurement.TabIndex = 8;
            this.btn_Measurement.Text = "测量";
            this.btn_Measurement.UseVisualStyleBackColor = true;
            this.btn_Measurement.Click += new System.EventHandler(this.btn_Measurement_Click);
            // 
            // txt_ThresholdValue
            // 
            this.txt_ThresholdValue.Location = new System.Drawing.Point(100, 71);
            this.txt_ThresholdValue.Name = "txt_ThresholdValue";
            this.txt_ThresholdValue.Size = new System.Drawing.Size(26, 21);
            this.txt_ThresholdValue.TabIndex = 7;
            this.txt_ThresholdValue.Text = "40";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "最小边缘幅度：";
            // 
            // btn_DrwaRect
            // 
            this.btn_DrwaRect.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_DrwaRect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_DrwaRect.Location = new System.Drawing.Point(5, 20);
            this.btn_DrwaRect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_DrwaRect.Name = "btn_DrwaRect";
            this.btn_DrwaRect.Size = new System.Drawing.Size(117, 38);
            this.btn_DrwaRect.TabIndex = 5;
            this.btn_DrwaRect.Text = "绘制测量线";
            this.btn_DrwaRect.UseVisualStyleBackColor = true;
            this.btn_DrwaRect.Click += new System.EventHandler(this.btn_DrwaRect_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 465);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "测量结果：";
            // 
            // btnTakePic
            // 
            this.btnTakePic.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTakePic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTakePic.Location = new System.Drawing.Point(627, 12);
            this.btnTakePic.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTakePic.Name = "btnTakePic";
            this.btnTakePic.Size = new System.Drawing.Size(94, 38);
            this.btnTakePic.TabIndex = 3;
            this.btnTakePic.Text = "拍照";
            this.btnTakePic.UseVisualStyleBackColor = true;
            this.btnTakePic.Click += new System.EventHandler(this.btnTakePic_Click);
            // 
            // btnOpenImg
            // 
            this.btnOpenImg.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenImg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenImg.Location = new System.Drawing.Point(519, 12);
            this.btnOpenImg.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnOpenImg.Name = "btnOpenImg";
            this.btnOpenImg.Size = new System.Drawing.Size(94, 38);
            this.btnOpenImg.TabIndex = 4;
            this.btnOpenImg.Text = "打开图片";
            this.btnOpenImg.UseVisualStyleBackColor = true;
            this.btnOpenImg.Click += new System.EventHandler(this.btnOpenImg_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(842, 56);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(386, 471);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "旋转标定区域";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(14, 293);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(366, 113);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_GenCircleCenter);
            this.tabPage1.Controls.Add(this.btn_SetPixelCoordinates);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(358, 87);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "手动标定";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_GenCircleCenter
            // 
            this.btn_GenCircleCenter.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_GenCircleCenter.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_GenCircleCenter.Location = new System.Drawing.Point(202, 21);
            this.btn_GenCircleCenter.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_GenCircleCenter.Name = "btn_GenCircleCenter";
            this.btn_GenCircleCenter.Size = new System.Drawing.Size(117, 38);
            this.btn_GenCircleCenter.TabIndex = 16;
            this.btn_GenCircleCenter.Text = "生成圆心";
            this.btn_GenCircleCenter.UseVisualStyleBackColor = true;
            this.btn_GenCircleCenter.Click += new System.EventHandler(this.btn_GenCircleCenter_Click);
            // 
            // btn_SetPixelCoordinates
            // 
            this.btn_SetPixelCoordinates.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SetPixelCoordinates.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_SetPixelCoordinates.Location = new System.Drawing.Point(19, 21);
            this.btn_SetPixelCoordinates.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_SetPixelCoordinates.Name = "btn_SetPixelCoordinates";
            this.btn_SetPixelCoordinates.Size = new System.Drawing.Size(117, 38);
            this.btn_SetPixelCoordinates.TabIndex = 15;
            this.btn_SetPixelCoordinates.Text = "识别像素坐标";
            this.btn_SetPixelCoordinates.UseVisualStyleBackColor = true;
            this.btn_SetPixelCoordinates.Click += new System.EventHandler(this.btn_SetPixelCoordinates_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btn_StartCalibration);
            this.tabPage2.Controls.Add(this.txt_Angle);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txt_CalNum);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(358, 87);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "自动标定";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btn_StartCalibration
            // 
            this.btn_StartCalibration.Location = new System.Drawing.Point(217, 28);
            this.btn_StartCalibration.Name = "btn_StartCalibration";
            this.btn_StartCalibration.Size = new System.Drawing.Size(118, 36);
            this.btn_StartCalibration.TabIndex = 19;
            this.btn_StartCalibration.Text = "开始标定";
            this.btn_StartCalibration.UseVisualStyleBackColor = true;
            this.btn_StartCalibration.Click += new System.EventHandler(this.btn_StartCalibration_Click);
            // 
            // txt_Angle
            // 
            this.txt_Angle.Location = new System.Drawing.Point(111, 49);
            this.txt_Angle.Name = "txt_Angle";
            this.txt_Angle.Size = new System.Drawing.Size(53, 21);
            this.txt_Angle.TabIndex = 18;
            this.txt_Angle.Text = "45";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 12);
            this.label7.TabIndex = 17;
            this.label7.Text = "每次运动旋转度数：";
            // 
            // txt_CalNum
            // 
            this.txt_CalNum.Location = new System.Drawing.Point(86, 18);
            this.txt_CalNum.Name = "txt_CalNum";
            this.txt_CalNum.Size = new System.Drawing.Size(53, 21);
            this.txt_CalNum.TabIndex = 16;
            this.txt_CalNum.Text = "9";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 12);
            this.label6.TabIndex = 15;
            this.label6.Text = "旋转标定点数：";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txt_CircleCenterColumn);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.txt_CircleCenterRow);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(358, 87);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "手工填写圆心坐标";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txt_CircleCenterColumn
            // 
            this.txt_CircleCenterColumn.Location = new System.Drawing.Point(89, 52);
            this.txt_CircleCenterColumn.Name = "txt_CircleCenterColumn";
            this.txt_CircleCenterColumn.Size = new System.Drawing.Size(53, 21);
            this.txt_CircleCenterColumn.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(17, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "圆心Column：";
            // 
            // txt_CircleCenterRow
            // 
            this.txt_CircleCenterRow.Location = new System.Drawing.Point(89, 12);
            this.txt_CircleCenterRow.Name = "txt_CircleCenterRow";
            this.txt_CircleCenterRow.Size = new System.Drawing.Size(53, 21);
            this.txt_CircleCenterRow.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "圆心Row：";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Location = new System.Drawing.Point(6, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(370, 258);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "序号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "像素坐标Row";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "像素坐标Column";
            this.Column3.Name = "Column3";
            this.Column3.Width = 120;
            // 
            // btn_SaveCalibrationResult
            // 
            this.btn_SaveCalibrationResult.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SaveCalibrationResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_SaveCalibrationResult.Location = new System.Drawing.Point(528, 599);
            this.btn_SaveCalibrationResult.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_SaveCalibrationResult.Name = "btn_SaveCalibrationResult";
            this.btn_SaveCalibrationResult.Size = new System.Drawing.Size(167, 38);
            this.btn_SaveCalibrationResult.TabIndex = 15;
            this.btn_SaveCalibrationResult.Text = "保存标定结果";
            this.btn_SaveCalibrationResult.UseVisualStyleBackColor = true;
            this.btn_SaveCalibrationResult.Click += new System.EventHandler(this.btn_SaveCalibrationResult_Click);
            // 
            // btn_SelectModel
            // 
            this.btn_SelectModel.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SelectModel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btn_SelectModel.Location = new System.Drawing.Point(738, 12);
            this.btn_SelectModel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btn_SelectModel.Name = "btn_SelectModel";
            this.btn_SelectModel.Size = new System.Drawing.Size(94, 38);
            this.btn_SelectModel.TabIndex = 6;
            this.btn_SelectModel.Text = "选择模板";
            this.btn_SelectModel.UseVisualStyleBackColor = true;
            this.btn_SelectModel.Click += new System.EventHandler(this.btn_SelectModel_Click);
            // 
            // btn_IsFitCircleCenter
            // 
            this.btn_IsFitCircleCenter.Location = new System.Drawing.Point(592, 569);
            this.btn_IsFitCircleCenter.Name = "btn_IsFitCircleCenter";
            this.btn_IsFitCircleCenter.Size = new System.Drawing.Size(108, 24);
            this.btn_IsFitCircleCenter.TabIndex = 7;
            this.btn_IsFitCircleCenter.Text = "是否拟合圆心";
            this.btn_IsFitCircleCenter.UseVisualStyleBackColor = true;
            this.btn_IsFitCircleCenter.Click += new System.EventHandler(this.btn_IsFitCircleCenter_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(17, 481);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(461, 136);
            this.richTextBox1.TabIndex = 18;
            this.richTextBox1.Text = "";
            // 
            // OffsetCalibrationFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 649);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btn_SaveCalibrationResult);
            this.Controls.Add(this.btn_IsFitCircleCenter);
            this.Controls.Add(this.btn_SelectModel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnTakePic);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOpenImg);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.hWindowControl1);
            this.Name = "OffsetCalibrationFrom";
            this.Text = "基于偏移量的标定";
            this.Load += new System.EventHandler(this.OffsetCalibrationFrom_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trb_setLineWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trb_setSigma)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trb_SetThreshold)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTakePic;
        private System.Windows.Forms.Button btnOpenImg;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Measurement;
        private System.Windows.Forms.TextBox txt_ThresholdValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_DrwaRect;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btn_SetPixelCoordinates;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btn_StartCalibration;
        private System.Windows.Forms.TextBox txt_Angle;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_CalNum;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btn_SelectModel;
        private System.Windows.Forms.TextBox txt_WordDistance;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Button btn_SaveCalibrationResult;
        private System.Windows.Forms.Button btn_GenCircleCenter;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txt_CircleCenterColumn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txt_CircleCenterRow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_OnePixelAccuracy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_SetOnePixelAccuracy;
        private System.Windows.Forms.Button btn_IsFitCircleCenter;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txt_MeasureWidthValue;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txt_sigmalValue;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TrackBar trb_setLineWidth;
        private System.Windows.Forms.TrackBar trb_setSigma;
        private System.Windows.Forms.TrackBar trb_SetThreshold;
        private System.Windows.Forms.TextBox txt_modelWord_U;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txt_modelWord_Y;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txt_modelWord_X;
        private System.Windows.Forms.Label label13;
    }
}