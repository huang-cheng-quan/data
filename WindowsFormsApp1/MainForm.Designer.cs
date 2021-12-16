
namespace WindowsFormsApp1
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.相机设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLC设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标标定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标标定本体定位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板建立ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成标定图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btn_OpenPlc = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.hWindowControl2 = new HalconDotNet.HWindowControl();
            this.hWindowControl3 = new HalconDotNet.HWindowControl();
            this.hWindowControl4 = new HalconDotNet.HWindowControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn_FeedBelt = new System.Windows.Forms.Button();
            this.btn_ResetFeedBelt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(0, 28);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(371, 293);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(371, 293);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1489, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.相机设置ToolStripMenuItem,
            this.pLC设置ToolStripMenuItem,
            this.标定设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 相机设置ToolStripMenuItem
            // 
            this.相机设置ToolStripMenuItem.Name = "相机设置ToolStripMenuItem";
            this.相机设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.相机设置ToolStripMenuItem.Text = "相机设置";
            this.相机设置ToolStripMenuItem.Click += new System.EventHandler(this.相机设置ToolStripMenuItem_Click);
            // 
            // pLC设置ToolStripMenuItem
            // 
            this.pLC设置ToolStripMenuItem.Name = "pLC设置ToolStripMenuItem";
            this.pLC设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.pLC设置ToolStripMenuItem.Text = "PLC设置";
            this.pLC设置ToolStripMenuItem.Click += new System.EventHandler(this.pLC设置ToolStripMenuItem_Click);
            // 
            // 标定设置ToolStripMenuItem
            // 
            this.标定设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.坐标标定ToolStripMenuItem,
            this.坐标标定本体定位ToolStripMenuItem,
            this.模板建立ToolStripMenuItem,
            this.生成标定图ToolStripMenuItem});
            this.标定设置ToolStripMenuItem.Name = "标定设置ToolStripMenuItem";
            this.标定设置ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.标定设置ToolStripMenuItem.Text = "标定设置";
            this.标定设置ToolStripMenuItem.Click += new System.EventHandler(this.标定设置ToolStripMenuItem_Click);
            // 
            // 坐标标定ToolStripMenuItem
            // 
            this.坐标标定ToolStripMenuItem.Name = "坐标标定ToolStripMenuItem";
            this.坐标标定ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.坐标标定ToolStripMenuItem.Text = "坐标标定(上料)";
            this.坐标标定ToolStripMenuItem.Click += new System.EventHandler(this.坐标标定ToolStripMenuItem_Click);
            // 
            // 坐标标定本体定位ToolStripMenuItem
            // 
            this.坐标标定本体定位ToolStripMenuItem.Name = "坐标标定本体定位ToolStripMenuItem";
            this.坐标标定本体定位ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.坐标标定本体定位ToolStripMenuItem.Text = "坐标标定(本体定位)";
            this.坐标标定本体定位ToolStripMenuItem.Click += new System.EventHandler(this.坐标标定本体定位ToolStripMenuItem_Click);
            // 
            // 模板建立ToolStripMenuItem
            // 
            this.模板建立ToolStripMenuItem.Name = "模板建立ToolStripMenuItem";
            this.模板建立ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.模板建立ToolStripMenuItem.Text = "模板建立";
            this.模板建立ToolStripMenuItem.Click += new System.EventHandler(this.模板建立ToolStripMenuItem_Click);
            // 
            // 生成标定图ToolStripMenuItem
            // 
            this.生成标定图ToolStripMenuItem.Name = "生成标定图ToolStripMenuItem";
            this.生成标定图ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.生成标定图ToolStripMenuItem.Text = "生成标定图";
            // 
            // btn_OpenPlc
            // 
            this.btn_OpenPlc.Location = new System.Drawing.Point(461, 0);
            this.btn_OpenPlc.Name = "btn_OpenPlc";
            this.btn_OpenPlc.Size = new System.Drawing.Size(77, 25);
            this.btn_OpenPlc.TabIndex = 2;
            this.btn_OpenPlc.Text = "开启PLC";
            this.btn_OpenPlc.UseVisualStyleBackColor = true;
            this.btn_OpenPlc.Click += new System.EventHandler(this.btn_OpenPlc_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(-2, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "皮带上料";
            // 
            // hWindowControl2
            // 
            this.hWindowControl2.BackColor = System.Drawing.Color.Black;
            this.hWindowControl2.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl2.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl2.Location = new System.Drawing.Point(372, 28);
            this.hWindowControl2.Name = "hWindowControl2";
            this.hWindowControl2.Size = new System.Drawing.Size(371, 293);
            this.hWindowControl2.TabIndex = 4;
            this.hWindowControl2.WindowSize = new System.Drawing.Size(371, 293);
            // 
            // hWindowControl3
            // 
            this.hWindowControl3.BackColor = System.Drawing.Color.Black;
            this.hWindowControl3.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl3.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl3.Location = new System.Drawing.Point(744, 28);
            this.hWindowControl3.Name = "hWindowControl3";
            this.hWindowControl3.Size = new System.Drawing.Size(371, 293);
            this.hWindowControl3.TabIndex = 6;
            this.hWindowControl3.WindowSize = new System.Drawing.Size(371, 293);
            // 
            // hWindowControl4
            // 
            this.hWindowControl4.BackColor = System.Drawing.Color.Black;
            this.hWindowControl4.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl4.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl4.Location = new System.Drawing.Point(1116, 28);
            this.hWindowControl4.Name = "hWindowControl4";
            this.hWindowControl4.Size = new System.Drawing.Size(371, 293);
            this.hWindowControl4.TabIndex = 8;
            this.hWindowControl4.WindowSize = new System.Drawing.Size(371, 293);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(0, 322);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(371, 293);
            this.pictureBox1.TabIndex = 10;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox2.Location = new System.Drawing.Point(372, 322);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(371, 293);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox3.Location = new System.Drawing.Point(744, 322);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(371, 293);
            this.pictureBox3.TabIndex = 12;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox4.Location = new System.Drawing.Point(1116, 322);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(371, 293);
            this.pictureBox4.TabIndex = 13;
            this.pictureBox4.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(370, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "连接器检测(反面)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(742, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 15;
            this.label3.Text = "本体定位";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(1114, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 16;
            this.label4.Text = "连接器检测(正面)";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(-2, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "侧面(短)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(370, 327);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 18;
            this.label6.Text = "反面/麦拉";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label7.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label7.Location = new System.Drawing.Point(742, 327);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 19;
            this.label7.Text = "侧面(长)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(1114, 327);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "正面";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(0, 619);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(506, 176);
            this.richTextBox1.TabIndex = 21;
            this.richTextBox1.Text = "";
            // 
            // btn_FeedBelt
            // 
            this.btn_FeedBelt.Location = new System.Drawing.Point(234, 28);
            this.btn_FeedBelt.Name = "btn_FeedBelt";
            this.btn_FeedBelt.Size = new System.Drawing.Size(45, 30);
            this.btn_FeedBelt.TabIndex = 22;
            this.btn_FeedBelt.Text = "启动";
            this.btn_FeedBelt.UseVisualStyleBackColor = true;
            this.btn_FeedBelt.Click += new System.EventHandler(this.btn_FeedBelt_Click);
            // 
            // btn_ResetFeedBelt
            // 
            this.btn_ResetFeedBelt.Location = new System.Drawing.Point(280, 28);
            this.btn_ResetFeedBelt.Name = "btn_ResetFeedBelt";
            this.btn_ResetFeedBelt.Size = new System.Drawing.Size(45, 30);
            this.btn_ResetFeedBelt.TabIndex = 23;
            this.btn_ResetFeedBelt.Text = "复位";
            this.btn_ResetFeedBelt.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(326, 28);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(45, 30);
            this.button1.TabIndex = 24;
            this.button1.Text = "清料";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1489, 801);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_ResetFeedBelt);
            this.Controls.Add(this.btn_FeedBelt);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.hWindowControl4);
            this.Controls.Add(this.hWindowControl3);
            this.Controls.Add(this.hWindowControl2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_OpenPlc);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "上料机";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HalconDotNet.HWindowControl hWindowControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 相机设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pLC设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 标定设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 坐标标定ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板建立ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 生成标定图ToolStripMenuItem;
        private System.Windows.Forms.Button btn_OpenPlc;
        private System.Windows.Forms.Label label1;
        private HalconDotNet.HWindowControl hWindowControl2;
        private HalconDotNet.HWindowControl hWindowControl3;
        private HalconDotNet.HWindowControl hWindowControl4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn_FeedBelt;
        private System.Windows.Forms.Button btn_ResetFeedBelt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem 坐标标定本体定位ToolStripMenuItem;
    }
}

