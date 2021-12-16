namespace Camera_Capture_demo.VisionFrms
{
    partial class CalibrationFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnJogPX = new System.Windows.Forms.Button();
            this.btnJogPY = new System.Windows.Forms.Button();
            this.btnJogNZ = new System.Windows.Forms.Button();
            this.btnJogPR = new System.Windows.Forms.Button();
            this.btnJogNR = new System.Windows.Forms.Button();
            this.btnJogPZ = new System.Windows.Forms.Button();
            this.btnJogNY = new System.Windows.Forms.Button();
            this.btnJogNX = new System.Windows.Forms.Button();
            this.btnFindCalibP = new System.Windows.Forms.Button();
            this.btnSaveMat2D = new System.Windows.Forms.Button();
            this.txtPixelLocation = new System.Windows.Forms.TextBox();
            this.btnCreateCalibModel = new System.Windows.Forms.Button();
            this.btnTakePic = new System.Windows.Forms.Button();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCamPos = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox8.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(464, 2);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(338, 290);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnJogPX);
            this.groupBox8.Controls.Add(this.btnJogPY);
            this.groupBox8.Controls.Add(this.btnJogNZ);
            this.groupBox8.Controls.Add(this.btnJogPR);
            this.groupBox8.Controls.Add(this.btnJogNR);
            this.groupBox8.Controls.Add(this.btnJogPZ);
            this.groupBox8.Controls.Add(this.btnJogNY);
            this.groupBox8.Controls.Add(this.btnJogNX);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(465, 297);
            this.groupBox8.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox8.Size = new System.Drawing.Size(208, 180);
            this.groupBox8.TabIndex = 56;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Robot Movement";
            // 
            // btnJogPX
            // 
            this.btnJogPX.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJogPX.Location = new System.Drawing.Point(114, 21);
            this.btnJogPX.Margin = new System.Windows.Forms.Padding(2);
            this.btnJogPX.Name = "btnJogPX";
            this.btnJogPX.Size = new System.Drawing.Size(91, 40);
            this.btnJogPX.TabIndex = 24;
            this.btnJogPX.Text = "+X ←";
            this.btnJogPX.UseVisualStyleBackColor = true;
            this.btnJogPX.Click += new System.EventHandler(this.btnJog_Click);
            // 
            // btnJogPY
            // 
            this.btnJogPY.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJogPY.Location = new System.Drawing.Point(114, 61);
            this.btnJogPY.Margin = new System.Windows.Forms.Padding(2);
            this.btnJogPY.Name = "btnJogPY";
            this.btnJogPY.Size = new System.Drawing.Size(91, 40);
            this.btnJogPY.TabIndex = 24;
            this.btnJogPY.Text = "+Y ↓";
            this.btnJogPY.UseVisualStyleBackColor = true;
            this.btnJogPY.Click += new System.EventHandler(this.btnJog_Click);
            // 
            // btnJogNZ
            // 
            this.btnJogNZ.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJogNZ.Location = new System.Drawing.Point(2, 101);
            this.btnJogNZ.Margin = new System.Windows.Forms.Padding(2);
            this.btnJogNZ.Name = "btnJogNZ";
            this.btnJogNZ.Size = new System.Drawing.Size(91, 40);
            this.btnJogNZ.TabIndex = 24;
            this.btnJogNZ.Text = " ↑ -Z";
            this.btnJogNZ.UseVisualStyleBackColor = true;
            this.btnJogNZ.Click += new System.EventHandler(this.btnJog_Click);
            // 
            // btnJogPR
            // 
            this.btnJogPR.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJogPR.Location = new System.Drawing.Point(114, 141);
            this.btnJogPR.Margin = new System.Windows.Forms.Padding(2);
            this.btnJogPR.Name = "btnJogPR";
            this.btnJogPR.Size = new System.Drawing.Size(91, 40);
            this.btnJogPR.TabIndex = 24;
            this.btnJogPR.Text = "+R ↷";
            this.btnJogPR.UseVisualStyleBackColor = true;
            this.btnJogPR.Click += new System.EventHandler(this.btnJog_Click);
            // 
            // btnJogNR
            // 
            this.btnJogNR.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJogNR.Location = new System.Drawing.Point(2, 141);
            this.btnJogNR.Margin = new System.Windows.Forms.Padding(2);
            this.btnJogNR.Name = "btnJogNR";
            this.btnJogNR.Size = new System.Drawing.Size(91, 40);
            this.btnJogNR.TabIndex = 24;
            this.btnJogNR.Text = "↶ -R";
            this.btnJogNR.UseVisualStyleBackColor = true;
            this.btnJogNR.Click += new System.EventHandler(this.btnJog_Click);
            // 
            // btnJogPZ
            // 
            this.btnJogPZ.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJogPZ.Location = new System.Drawing.Point(114, 101);
            this.btnJogPZ.Margin = new System.Windows.Forms.Padding(2);
            this.btnJogPZ.Name = "btnJogPZ";
            this.btnJogPZ.Size = new System.Drawing.Size(91, 40);
            this.btnJogPZ.TabIndex = 24;
            this.btnJogPZ.Text = "+Z ↓";
            this.btnJogPZ.UseVisualStyleBackColor = true;
            this.btnJogPZ.Click += new System.EventHandler(this.btnJog_Click);
            // 
            // btnJogNY
            // 
            this.btnJogNY.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJogNY.Location = new System.Drawing.Point(2, 61);
            this.btnJogNY.Margin = new System.Windows.Forms.Padding(2);
            this.btnJogNY.Name = "btnJogNY";
            this.btnJogNY.Size = new System.Drawing.Size(91, 40);
            this.btnJogNY.TabIndex = 24;
            this.btnJogNY.Text = "↑ -Y";
            this.btnJogNY.UseVisualStyleBackColor = true;
            this.btnJogNY.Click += new System.EventHandler(this.btnJog_Click);
            // 
            // btnJogNX
            // 
            this.btnJogNX.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnJogNX.Location = new System.Drawing.Point(2, 21);
            this.btnJogNX.Margin = new System.Windows.Forms.Padding(2);
            this.btnJogNX.Name = "btnJogNX";
            this.btnJogNX.Size = new System.Drawing.Size(91, 40);
            this.btnJogNX.TabIndex = 24;
            this.btnJogNX.Text = "→  -X";
            this.btnJogNX.UseVisualStyleBackColor = true;
            this.btnJogNX.Click += new System.EventHandler(this.btnJog_Click);
            // 
            // btnFindCalibP
            // 
            this.btnFindCalibP.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFindCalibP.Location = new System.Drawing.Point(710, 404);
            this.btnFindCalibP.Margin = new System.Windows.Forms.Padding(2);
            this.btnFindCalibP.Name = "btnFindCalibP";
            this.btnFindCalibP.Size = new System.Drawing.Size(86, 44);
            this.btnFindCalibP.TabIndex = 2;
            this.btnFindCalibP.Text = "寻找标定点";
            this.btnFindCalibP.UseVisualStyleBackColor = true;
            this.btnFindCalibP.Click += new System.EventHandler(this.btnFindCalibP_Click);
            // 
            // btnSaveMat2D
            // 
            this.btnSaveMat2D.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSaveMat2D.Location = new System.Drawing.Point(710, 457);
            this.btnSaveMat2D.Margin = new System.Windows.Forms.Padding(2);
            this.btnSaveMat2D.Name = "btnSaveMat2D";
            this.btnSaveMat2D.Size = new System.Drawing.Size(86, 44);
            this.btnSaveMat2D.TabIndex = 2;
            this.btnSaveMat2D.Text = "保存标定结果";
            this.btnSaveMat2D.UseVisualStyleBackColor = true;
            this.btnSaveMat2D.Click += new System.EventHandler(this.btnSaveMat2D_Click);
            // 
            // txtPixelLocation
            // 
            this.txtPixelLocation.Location = new System.Drawing.Point(2, 494);
            this.txtPixelLocation.Margin = new System.Windows.Forms.Padding(2);
            this.txtPixelLocation.Name = "txtPixelLocation";
            this.txtPixelLocation.Size = new System.Drawing.Size(462, 21);
            this.txtPixelLocation.TabIndex = 57;
            // 
            // btnCreateCalibModel
            // 
            this.btnCreateCalibModel.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCreateCalibModel.Location = new System.Drawing.Point(710, 354);
            this.btnCreateCalibModel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateCalibModel.Name = "btnCreateCalibModel";
            this.btnCreateCalibModel.Size = new System.Drawing.Size(86, 41);
            this.btnCreateCalibModel.TabIndex = 2;
            this.btnCreateCalibModel.Text = "生成标定点阵图";
            this.btnCreateCalibModel.UseVisualStyleBackColor = true;
            this.btnCreateCalibModel.Click += new System.EventHandler(this.btnCreateCalibModel_Click);
            // 
            // btnTakePic
            // 
            this.btnTakePic.Font = new System.Drawing.Font("楷体", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTakePic.Location = new System.Drawing.Point(710, 310);
            this.btnTakePic.Margin = new System.Windows.Forms.Padding(2);
            this.btnTakePic.Name = "btnTakePic";
            this.btnTakePic.Size = new System.Drawing.Size(86, 35);
            this.btnTakePic.TabIndex = 2;
            this.btnTakePic.Text = "拍照";
            this.btnTakePic.UseVisualStyleBackColor = true;
            this.btnTakePic.Click += new System.EventHandler(this.btnTakePic_Click);
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(2, 2);
            this.hWindowControl1.Margin = new System.Windows.Forms.Padding(2);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(461, 487);
            this.hWindowControl1.TabIndex = 58;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(461, 487);
            this.hWindowControl1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.hWindowControl1_MouseMove);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(468, 489);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 16);
            this.label3.TabIndex = 23;
            this.label3.Text = "相机当前坐标：";
            // 
            // lblCamPos
            // 
            this.lblCamPos.AutoSize = true;
            this.lblCamPos.Location = new System.Drawing.Point(590, 492);
            this.lblCamPos.Name = "lblCamPos";
            this.lblCamPos.Size = new System.Drawing.Size(29, 12);
            this.lblCamPos.TabIndex = 59;
            this.lblCamPos.Text = "null";
            // 
            // CalibrationFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 516);
            this.Controls.Add(this.lblCamPos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.txtPixelLocation);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSaveMat2D);
            this.Controls.Add(this.btnFindCalibP);
            this.Controls.Add(this.btnTakePic);
            this.Controls.Add(this.btnCreateCalibModel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CalibrationFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "坐标标定";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CalibrationFrm_FormClosed);
            this.Load += new System.EventHandler(this.CalibrationFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox8.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnJogPX;
        private System.Windows.Forms.Button btnJogPY;
        private System.Windows.Forms.Button btnJogNZ;
        private System.Windows.Forms.Button btnJogPR;
        private System.Windows.Forms.Button btnJogNR;
        private System.Windows.Forms.Button btnJogPZ;
        private System.Windows.Forms.Button btnJogNY;
        private System.Windows.Forms.Button btnJogNX;
        private System.Windows.Forms.Button btnFindCalibP;
        private System.Windows.Forms.Button btnSaveMat2D;
        private System.Windows.Forms.TextBox txtPixelLocation;
        private System.Windows.Forms.Button btnCreateCalibModel;
        private System.Windows.Forms.Button btnTakePic;
        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCamPos;
    }
}