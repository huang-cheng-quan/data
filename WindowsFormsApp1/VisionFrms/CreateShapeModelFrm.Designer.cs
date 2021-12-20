namespace Camera_Capture_demo.VisionFrms
{
    partial class CreateShapeModelFrm
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
            this.txtPixelLocation = new System.Windows.Forms.TextBox();
            this.nudThreshold = new System.Windows.Forms.NumericUpDown();
            this.nudMinScore = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.btnOpenImg = new System.Windows.Forms.Button();
            this.btnSetEraser = new System.Windows.Forms.Button();
            this.btnDrawRect = new System.Windows.Forms.Button();
            this.btnSaveModel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.trbEraserSize = new System.Windows.Forms.TrackBar();
            this.btnClrEraser = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rdoRect = new System.Windows.Forms.RadioButton();
            this.rdoCircle = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnTakePic = new System.Windows.Forms.Button();
            this.btnDrawROI1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.cboPolarity = new System.Windows.Forms.ComboBox();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.cboRoiNo = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbEraserSize)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtPixelLocation
            // 
            this.txtPixelLocation.Location = new System.Drawing.Point(2, 619);
            this.txtPixelLocation.Margin = new System.Windows.Forms.Padding(2);
            this.txtPixelLocation.Name = "txtPixelLocation";
            this.txtPixelLocation.Size = new System.Drawing.Size(678, 24);
            this.txtPixelLocation.TabIndex = 58;
            // 
            // nudThreshold
            // 
            this.nudThreshold.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudThreshold.Location = new System.Drawing.Point(811, 545);
            this.nudThreshold.Margin = new System.Windows.Forms.Padding(2);
            this.nudThreshold.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.nudThreshold.Name = "nudThreshold";
            this.nudThreshold.Size = new System.Drawing.Size(96, 24);
            this.nudThreshold.TabIndex = 5;
            this.nudThreshold.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudMinScore
            // 
            this.nudMinScore.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nudMinScore.Location = new System.Drawing.Point(811, 507);
            this.nudMinScore.Margin = new System.Windows.Forms.Padding(2);
            this.nudMinScore.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinScore.Name = "nudMinScore";
            this.nudMinScore.Size = new System.Drawing.Size(96, 24);
            this.nudMinScore.TabIndex = 5;
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
            this.label3.Location = new System.Drawing.Point(700, 548);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(111, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "模板灰度阈值:";
            // 
            // btnTest
            // 
            this.btnTest.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTest.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTest.Location = new System.Drawing.Point(688, 594);
            this.btnTest.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(94, 38);
            this.btnTest.TabIndex = 2;
            this.btnTest.Text = "检测测试";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnOpenImg
            // 
            this.btnOpenImg.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnOpenImg.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOpenImg.Location = new System.Drawing.Point(688, 12);
            this.btnOpenImg.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnOpenImg.Name = "btnOpenImg";
            this.btnOpenImg.Size = new System.Drawing.Size(94, 38);
            this.btnOpenImg.TabIndex = 2;
            this.btnOpenImg.Text = "打开图片";
            this.btnOpenImg.UseVisualStyleBackColor = true;
            this.btnOpenImg.Click += new System.EventHandler(this.btnOpenImg_Click);
            // 
            // btnSetEraser
            // 
            this.btnSetEraser.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSetEraser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSetEraser.Location = new System.Drawing.Point(835, 133);
            this.btnSetEraser.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSetEraser.Name = "btnSetEraser";
            this.btnSetEraser.Size = new System.Drawing.Size(94, 38);
            this.btnSetEraser.TabIndex = 2;
            this.btnSetEraser.Text = "设置掩膜";
            this.btnSetEraser.UseVisualStyleBackColor = true;
            this.btnSetEraser.Click += new System.EventHandler(this.btnSetEraser_Click);
            // 
            // btnDrawRect
            // 
            this.btnDrawRect.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawRect.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDrawRect.Location = new System.Drawing.Point(688, 133);
            this.btnDrawRect.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDrawRect.Name = "btnDrawRect";
            this.btnDrawRect.Size = new System.Drawing.Size(94, 38);
            this.btnDrawRect.TabIndex = 2;
            this.btnDrawRect.Text = "框选模板";
            this.btnDrawRect.UseVisualStyleBackColor = true;
            this.btnDrawRect.Click += new System.EventHandler(this.btnDrawRect_Click);
            // 
            // btnSaveModel
            // 
            this.btnSaveModel.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveModel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSaveModel.Location = new System.Drawing.Point(834, 594);
            this.btnSaveModel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnSaveModel.Name = "btnSaveModel";
            this.btnSaveModel.Size = new System.Drawing.Size(94, 38);
            this.btnSaveModel.TabIndex = 2;
            this.btnSaveModel.Text = "保存模板";
            this.btnSaveModel.UseVisualStyleBackColor = true;
            this.btnSaveModel.Click += new System.EventHandler(this.btnSaveModel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(685, 511);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 15);
            this.label2.TabIndex = 2;
            this.label2.Text = "模板最低匹配值:";
            // 
            // trbEraserSize
            // 
            this.trbEraserSize.Location = new System.Drawing.Point(120, 30);
            this.trbEraserSize.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.trbEraserSize.Name = "trbEraserSize";
            this.trbEraserSize.Size = new System.Drawing.Size(122, 45);
            this.trbEraserSize.TabIndex = 0;
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdoCircle);
            this.groupBox1.Controls.Add(this.rdoRect);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnClrEraser);
            this.groupBox1.Controls.Add(this.trbEraserSize);
            this.groupBox1.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(684, 242);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.groupBox1.Size = new System.Drawing.Size(250, 189);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "橡皮擦工具";
            // 
            // btnTakePic
            // 
            this.btnTakePic.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTakePic.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnTakePic.Location = new System.Drawing.Point(832, 12);
            this.btnTakePic.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnTakePic.Name = "btnTakePic";
            this.btnTakePic.Size = new System.Drawing.Size(94, 38);
            this.btnTakePic.TabIndex = 2;
            this.btnTakePic.Text = "拍照";
            this.btnTakePic.UseVisualStyleBackColor = true;
            this.btnTakePic.Click += new System.EventHandler(this.btnTakePic_Click);
            // 
            // btnDrawROI1
            // 
            this.btnDrawROI1.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDrawROI1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnDrawROI1.Location = new System.Drawing.Point(832, 66);
            this.btnDrawROI1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnDrawROI1.Name = "btnDrawROI1";
            this.btnDrawROI1.Size = new System.Drawing.Size(94, 38);
            this.btnDrawROI1.TabIndex = 2;
            this.btnDrawROI1.Text = "设置ROI";
            this.btnDrawROI1.UseVisualStyleBackColor = true;
            this.btnDrawROI1.Click += new System.EventHandler(this.btnDrawROI_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(732, 474);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 15);
            this.label7.TabIndex = 2;
            this.label7.Text = "电池极性:";
            // 
            // cboPolarity
            // 
            this.cboPolarity.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cboPolarity.FormattingEnabled = true;
            this.cboPolarity.Items.AddRange(new object[] {
            "黑底白点",
            "白底黑点"});
            this.cboPolarity.Location = new System.Drawing.Point(811, 470);
            this.cboPolarity.Margin = new System.Windows.Forms.Padding(2);
            this.cboPolarity.Name = "cboPolarity";
            this.cboPolarity.Size = new System.Drawing.Size(92, 22);
            this.cboPolarity.TabIndex = 59;
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(2, 12);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(677, 602);
            this.hWindowControl1.TabIndex = 61;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(677, 602);
            // 
            // cboRoiNo
            // 
            this.cboRoiNo.FormattingEnabled = true;
            this.cboRoiNo.Items.AddRange(new object[] {
            "胶片1",
            "胶片2",
            "胶片3",
            "胶片4"});
            this.cboRoiNo.Location = new System.Drawing.Point(688, 75);
            this.cboRoiNo.Name = "cboRoiNo";
            this.cboRoiNo.Size = new System.Drawing.Size(94, 22);
            this.cboRoiNo.TabIndex = 62;
            // 
            // CreateShapeModelFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(937, 646);
            this.Controls.Add(this.cboRoiNo);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.cboPolarity);
            this.Controls.Add(this.txtPixelLocation);
            this.Controls.Add(this.nudThreshold);
            this.Controls.Add(this.nudMinScore);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnDrawROI1);
            this.Controls.Add(this.btnSetEraser);
            this.Controls.Add(this.btnDrawRect);
            this.Controls.Add(this.btnTakePic);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnSaveModel);
            this.Controls.Add(this.btnOpenImg);
            this.Font = new System.Drawing.Font("宋体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "CreateShapeModelFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "模板制作";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CreateShapeModelFrm_FormClosed);
            this.Load += new System.EventHandler(this.CreateShapeModelFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudThreshold)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbEraserSize)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtPixelLocation;
        private System.Windows.Forms.NumericUpDown nudThreshold;
        private System.Windows.Forms.NumericUpDown nudMinScore;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.Button btnOpenImg;
        private System.Windows.Forms.Button btnSetEraser;
        private System.Windows.Forms.Button btnDrawRect;
        private System.Windows.Forms.Button btnSaveModel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trbEraserSize;
        private System.Windows.Forms.Button btnClrEraser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdoRect;
        private System.Windows.Forms.RadioButton rdoCircle;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnTakePic;
        private System.Windows.Forms.Button btnDrawROI1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboPolarity;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.ComboBox cboRoiNo;
    }
}