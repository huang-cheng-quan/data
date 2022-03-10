
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
            this.components = new System.ComponentModel.Container();
            this.hWindowControl1 = new HalconDotNet.HWindowControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.相机设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLC设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.plc设置上料ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLC设置本体定位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.标定设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标标定ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标标定本体定位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标标定本体定位2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标标定钢片检测AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标标定钢片检测BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板建立ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板建立本体定位ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板建立本体定位2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板建立钢片检测AToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.模板建立钢片能检测BToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.生成标定图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置补偿值ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.定时删除文件设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.切换项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新建项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.hWindowControl2 = new HalconDotNet.HWindowControl();
            this.hWindowControl3 = new HalconDotNet.HWindowControl();
            this.hWindowControl4 = new HalconDotNet.HWindowControl();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btn_ResetFeedBelt = new System.Windows.Forms.Button();
            this.lblToolNo = new System.Windows.Forms.Label();
            this.lblPosition = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lbl_NGSampleNums = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.lbl_OKSampleNums = new System.Windows.Forms.Label();
            this.label34 = new System.Windows.Forms.Label();
            this.lbl_TotalSampleNums = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslblNowProj = new System.Windows.Forms.ToolStripStatusLabel();
            this.pic5 = new System.Windows.Forms.PictureBox();
            this.pic_4 = new System.Windows.Forms.PictureBox();
            this.pic_2 = new System.Windows.Forms.PictureBox();
            this.pic_1 = new System.Windows.Forms.PictureBox();
            this.pic_3 = new System.Windows.Forms.PictureBox();
            this.pic_5 = new System.Windows.Forms.PictureBox();
            this.pic_6 = new System.Windows.Forms.PictureBox();
            this.pic_7 = new System.Windows.Forms.PictureBox();
            this.lbl_NGSampleNums2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_OKSampleNums2 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.lbl_TotalSampleNums2 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.picShow = new System.Windows.Forms.PictureBox();
            this.rtxLog = new System.Windows.Forms.RichTextBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OKorNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_Start = new System.Windows.Forms.Button();
            this.btn_Reset = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox8 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.hWindowControl5 = new HalconDotNet.HWindowControl();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.label44 = new System.Windows.Forms.Label();
            this.label45 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.label47 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label49 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.txt_ProjectName = new System.Windows.Forms.Label();
            this.hWindowControl6 = new HalconDotNet.HWindowControl();
            this.hWindowControl7 = new HalconDotNet.HWindowControl();
            this.label6 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox9 = new System.Windows.Forms.PictureBox();
            this.label17 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).BeginInit();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(2, 28);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(306, 264);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(306, 264);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem,
            this.项目ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1815, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.相机设置ToolStripMenuItem,
            this.pLC设置ToolStripMenuItem,
            this.标定设置ToolStripMenuItem,
            this.定时删除文件设置ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.设置ToolStripMenuItem.Text = "设置";
            // 
            // 相机设置ToolStripMenuItem
            // 
            this.相机设置ToolStripMenuItem.Name = "相机设置ToolStripMenuItem";
            this.相机设置ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.相机设置ToolStripMenuItem.Text = "相机设置";
            this.相机设置ToolStripMenuItem.Click += new System.EventHandler(this.相机设置ToolStripMenuItem_Click);
            // 
            // pLC设置ToolStripMenuItem
            // 
            this.pLC设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.plc设置上料ToolStripMenuItem,
            this.pLC设置本体定位ToolStripMenuItem});
            this.pLC设置ToolStripMenuItem.Name = "pLC设置ToolStripMenuItem";
            this.pLC设置ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.pLC设置ToolStripMenuItem.Text = "PLC设置";
            // 
            // plc设置上料ToolStripMenuItem
            // 
            this.plc设置上料ToolStripMenuItem.Name = "plc设置上料ToolStripMenuItem";
            this.plc设置上料ToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.plc设置上料ToolStripMenuItem.Text = "PLC设置(上料)";
            this.plc设置上料ToolStripMenuItem.Click += new System.EventHandler(this.plc设置上料ToolStripMenuItem_Click);
            // 
            // pLC设置本体定位ToolStripMenuItem
            // 
            this.pLC设置本体定位ToolStripMenuItem.Name = "pLC设置本体定位ToolStripMenuItem";
            this.pLC设置本体定位ToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.pLC设置本体定位ToolStripMenuItem.Text = "PLC设置(本体定位)";
            this.pLC设置本体定位ToolStripMenuItem.Click += new System.EventHandler(this.pLC设置本体定位ToolStripMenuItem_Click);
            // 
            // 标定设置ToolStripMenuItem
            // 
            this.标定设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.坐标标定ToolStripMenuItem,
            this.坐标标定本体定位ToolStripMenuItem,
            this.坐标标定本体定位2ToolStripMenuItem,
            this.坐标标定钢片检测AToolStripMenuItem,
            this.坐标标定钢片检测BToolStripMenuItem,
            this.模板建立ToolStripMenuItem,
            this.模板建立本体定位ToolStripMenuItem,
            this.模板建立本体定位2ToolStripMenuItem,
            this.模板建立钢片检测AToolStripMenuItem,
            this.模板建立钢片能检测BToolStripMenuItem,
            this.生成标定图ToolStripMenuItem,
            this.设置补偿值ToolStripMenuItem});
            this.标定设置ToolStripMenuItem.Name = "标定设置ToolStripMenuItem";
            this.标定设置ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.标定设置ToolStripMenuItem.Text = "标定设置";
            this.标定设置ToolStripMenuItem.Click += new System.EventHandler(this.标定设置ToolStripMenuItem_Click);
            // 
            // 坐标标定ToolStripMenuItem
            // 
            this.坐标标定ToolStripMenuItem.Name = "坐标标定ToolStripMenuItem";
            this.坐标标定ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.坐标标定ToolStripMenuItem.Text = "坐标标定(上料)";
            this.坐标标定ToolStripMenuItem.Click += new System.EventHandler(this.坐标标定ToolStripMenuItem_Click);
            // 
            // 坐标标定本体定位ToolStripMenuItem
            // 
            this.坐标标定本体定位ToolStripMenuItem.Name = "坐标标定本体定位ToolStripMenuItem";
            this.坐标标定本体定位ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.坐标标定本体定位ToolStripMenuItem.Text = "坐标标定(本体定位1)";
            this.坐标标定本体定位ToolStripMenuItem.Click += new System.EventHandler(this.坐标标定本体定位ToolStripMenuItem_Click);
            // 
            // 坐标标定本体定位2ToolStripMenuItem
            // 
            this.坐标标定本体定位2ToolStripMenuItem.Name = "坐标标定本体定位2ToolStripMenuItem";
            this.坐标标定本体定位2ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.坐标标定本体定位2ToolStripMenuItem.Text = "坐标标定(本体定位2)";
            this.坐标标定本体定位2ToolStripMenuItem.Click += new System.EventHandler(this.坐标标定本体定位2ToolStripMenuItem_Click);
            // 
            // 坐标标定钢片检测AToolStripMenuItem
            // 
            this.坐标标定钢片检测AToolStripMenuItem.Name = "坐标标定钢片检测AToolStripMenuItem";
            this.坐标标定钢片检测AToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.坐标标定钢片检测AToolStripMenuItem.Text = "坐标标定(钢片检测A)";
            this.坐标标定钢片检测AToolStripMenuItem.Click += new System.EventHandler(this.坐标标定钢片检测AToolStripMenuItem_Click);
            // 
            // 坐标标定钢片检测BToolStripMenuItem
            // 
            this.坐标标定钢片检测BToolStripMenuItem.Name = "坐标标定钢片检测BToolStripMenuItem";
            this.坐标标定钢片检测BToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.坐标标定钢片检测BToolStripMenuItem.Text = "坐标标定(钢片检测B)";
            this.坐标标定钢片检测BToolStripMenuItem.Click += new System.EventHandler(this.坐标标定钢片检测BToolStripMenuItem_Click);
            // 
            // 模板建立ToolStripMenuItem
            // 
            this.模板建立ToolStripMenuItem.Name = "模板建立ToolStripMenuItem";
            this.模板建立ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.模板建立ToolStripMenuItem.Text = "模板建立(上料)";
            this.模板建立ToolStripMenuItem.Click += new System.EventHandler(this.模板建立ToolStripMenuItem_Click);
            // 
            // 模板建立本体定位ToolStripMenuItem
            // 
            this.模板建立本体定位ToolStripMenuItem.Name = "模板建立本体定位ToolStripMenuItem";
            this.模板建立本体定位ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.模板建立本体定位ToolStripMenuItem.Text = "模板建立(本体定位1)";
            this.模板建立本体定位ToolStripMenuItem.Click += new System.EventHandler(this.模板建立本体定位ToolStripMenuItem_Click);
            // 
            // 模板建立本体定位2ToolStripMenuItem
            // 
            this.模板建立本体定位2ToolStripMenuItem.Name = "模板建立本体定位2ToolStripMenuItem";
            this.模板建立本体定位2ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.模板建立本体定位2ToolStripMenuItem.Text = "模板建立(本体定位2)";
            this.模板建立本体定位2ToolStripMenuItem.Click += new System.EventHandler(this.模板建立本体定位2ToolStripMenuItem_Click);
            // 
            // 模板建立钢片检测AToolStripMenuItem
            // 
            this.模板建立钢片检测AToolStripMenuItem.Name = "模板建立钢片检测AToolStripMenuItem";
            this.模板建立钢片检测AToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.模板建立钢片检测AToolStripMenuItem.Text = "模板建立(钢片检测A)";
            this.模板建立钢片检测AToolStripMenuItem.Click += new System.EventHandler(this.模板建立钢片检测AToolStripMenuItem_Click);
            // 
            // 模板建立钢片能检测BToolStripMenuItem
            // 
            this.模板建立钢片能检测BToolStripMenuItem.Name = "模板建立钢片能检测BToolStripMenuItem";
            this.模板建立钢片能检测BToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.模板建立钢片能检测BToolStripMenuItem.Text = "模板建立(钢片能检测B)";
            // 
            // 生成标定图ToolStripMenuItem
            // 
            this.生成标定图ToolStripMenuItem.Name = "生成标定图ToolStripMenuItem";
            this.生成标定图ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.生成标定图ToolStripMenuItem.Text = "生成标定图";
            // 
            // 设置补偿值ToolStripMenuItem
            // 
            this.设置补偿值ToolStripMenuItem.Name = "设置补偿值ToolStripMenuItem";
            this.设置补偿值ToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.设置补偿值ToolStripMenuItem.Text = "设置补偿值";
            this.设置补偿值ToolStripMenuItem.Click += new System.EventHandler(this.设置补偿值ToolStripMenuItem_Click);
            // 
            // 定时删除文件设置ToolStripMenuItem
            // 
            this.定时删除文件设置ToolStripMenuItem.Name = "定时删除文件设置ToolStripMenuItem";
            this.定时删除文件设置ToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.定时删除文件设置ToolStripMenuItem.Text = "定时删除文件设置";
            // 
            // 项目ToolStripMenuItem
            // 
            this.项目ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.切换项目ToolStripMenuItem,
            this.新建项目ToolStripMenuItem});
            this.项目ToolStripMenuItem.Name = "项目ToolStripMenuItem";
            this.项目ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.项目ToolStripMenuItem.Text = "项目";
            // 
            // 切换项目ToolStripMenuItem
            // 
            this.切换项目ToolStripMenuItem.Name = "切换项目ToolStripMenuItem";
            this.切换项目ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.切换项目ToolStripMenuItem.Text = "切换项目";
            this.切换项目ToolStripMenuItem.Click += new System.EventHandler(this.切换项目ToolStripMenuItem_Click_1);
            // 
            // 新建项目ToolStripMenuItem
            // 
            this.新建项目ToolStripMenuItem.Name = "新建项目ToolStripMenuItem";
            this.新建项目ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新建项目ToolStripMenuItem.Text = "新建项目";
            this.新建项目ToolStripMenuItem.Click += new System.EventHandler(this.新建项目ToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(2, 30);
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
            this.hWindowControl2.Location = new System.Drawing.Point(309, 28);
            this.hWindowControl2.Name = "hWindowControl2";
            this.hWindowControl2.Size = new System.Drawing.Size(306, 264);
            this.hWindowControl2.TabIndex = 4;
            this.hWindowControl2.WindowSize = new System.Drawing.Size(306, 264);
            // 
            // hWindowControl3
            // 
            this.hWindowControl3.BackColor = System.Drawing.Color.Black;
            this.hWindowControl3.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl3.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl3.Location = new System.Drawing.Point(616, 28);
            this.hWindowControl3.Name = "hWindowControl3";
            this.hWindowControl3.Size = new System.Drawing.Size(306, 264);
            this.hWindowControl3.TabIndex = 6;
            this.hWindowControl3.WindowSize = new System.Drawing.Size(306, 264);
            // 
            // hWindowControl4
            // 
            this.hWindowControl4.BackColor = System.Drawing.Color.Black;
            this.hWindowControl4.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl4.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl4.Location = new System.Drawing.Point(93, 179);
            this.hWindowControl4.Name = "hWindowControl4";
            this.hWindowControl4.Size = new System.Drawing.Size(80, 79);
            this.hWindowControl4.TabIndex = 8;
            this.hWindowControl4.WindowSize = new System.Drawing.Size(80, 79);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(879, 327);
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
            this.label3.Location = new System.Drawing.Point(-2, 327);
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
            this.label4.Location = new System.Drawing.Point(879, 30);
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
            this.label5.Location = new System.Drawing.Point(647, 460);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "侧面(短A)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label8.Location = new System.Drawing.Point(377, 460);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 12);
            this.label8.TabIndex = 20;
            this.label8.Text = "正面";
            // 
            // btn_ResetFeedBelt
            // 
            this.btn_ResetFeedBelt.Location = new System.Drawing.Point(1135, 743);
            this.btn_ResetFeedBelt.Name = "btn_ResetFeedBelt";
            this.btn_ResetFeedBelt.Size = new System.Drawing.Size(55, 61);
            this.btn_ResetFeedBelt.TabIndex = 23;
            this.btn_ResetFeedBelt.Text = "复位";
            this.btn_ResetFeedBelt.UseVisualStyleBackColor = true;
            this.btn_ResetFeedBelt.Click += new System.EventHandler(this.btn_ResetFeedBelt_Click);
            // 
            // lblToolNo
            // 
            this.lblToolNo.AutoSize = true;
            this.lblToolNo.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblToolNo.Location = new System.Drawing.Point(164, 10);
            this.lblToolNo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblToolNo.Name = "lblToolNo";
            this.lblToolNo.Size = new System.Drawing.Size(34, 21);
            this.lblToolNo.TabIndex = 25;
            this.lblToolNo.Text = "01";
            // 
            // lblPosition
            // 
            this.lblPosition.AutoSize = true;
            this.lblPosition.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPosition.ForeColor = System.Drawing.Color.Blue;
            this.lblPosition.Location = new System.Drawing.Point(33, 30);
            this.lblPosition.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblPosition.Name = "lblPosition";
            this.lblPosition.Size = new System.Drawing.Size(75, 72);
            this.lblPosition.TabIndex = 26;
            this.lblPosition.Text = "X:xxx\r\nY:xxx\r\nR:xxx";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(12, 10);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(132, 21);
            this.label9.TabIndex = 27;
            this.label9.Text = "机械手序号:";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "电池序号";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "连接器检测结果";
            this.Column2.Name = "Column2";
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "本体检测";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "检测结果";
            this.Column4.Name = "Column4";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label10.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label10.Location = new System.Drawing.Point(739, 460);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 30;
            this.label10.Text = "侧面(短B)";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.lblPosition);
            this.panel1.Controls.Add(this.lblToolNo);
            this.panel1.Location = new System.Drawing.Point(1074, 634);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(210, 103);
            this.panel1.TabIndex = 32;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label11.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label11.Location = new System.Drawing.Point(464, 460);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 36;
            this.label11.Text = "侧面(长A)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label12.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label12.Location = new System.Drawing.Point(556, 460);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 37;
            this.label12.Text = "侧面(长B)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label13.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label13.Location = new System.Drawing.Point(379, 546);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(29, 12);
            this.label13.TabIndex = 39;
            this.label13.Text = "麦拉";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label14.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label14.Location = new System.Drawing.Point(466, 546);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(29, 12);
            this.label14.TabIndex = 41;
            this.label14.Text = "反面";
            // 
            // lbl_NGSampleNums
            // 
            this.lbl_NGSampleNums.AutoSize = true;
            this.lbl_NGSampleNums.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_NGSampleNums.ForeColor = System.Drawing.Color.Crimson;
            this.lbl_NGSampleNums.Location = new System.Drawing.Point(735, 597);
            this.lbl_NGSampleNums.Name = "lbl_NGSampleNums";
            this.lbl_NGSampleNums.Size = new System.Drawing.Size(22, 21);
            this.lbl_NGSampleNums.TabIndex = 47;
            this.lbl_NGSampleNums.Text = "0";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label36.ForeColor = System.Drawing.Color.Red;
            this.label36.Location = new System.Drawing.Point(727, 559);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(56, 21);
            this.label36.TabIndex = 46;
            this.label36.Text = "NG：";
            // 
            // lbl_OKSampleNums
            // 
            this.lbl_OKSampleNums.AutoSize = true;
            this.lbl_OKSampleNums.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_OKSampleNums.ForeColor = System.Drawing.Color.Lime;
            this.lbl_OKSampleNums.Location = new System.Drawing.Point(658, 597);
            this.lbl_OKSampleNums.Name = "lbl_OKSampleNums";
            this.lbl_OKSampleNums.Size = new System.Drawing.Size(22, 21);
            this.lbl_OKSampleNums.TabIndex = 45;
            this.lbl_OKSampleNums.Text = "0";
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label34.ForeColor = System.Drawing.Color.Lime;
            this.label34.Location = new System.Drawing.Point(649, 559);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(56, 21);
            this.label34.TabIndex = 44;
            this.label34.Text = "OK：";
            // 
            // lbl_TotalSampleNums
            // 
            this.lbl_TotalSampleNums.AutoSize = true;
            this.lbl_TotalSampleNums.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_TotalSampleNums.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbl_TotalSampleNums.Location = new System.Drawing.Point(577, 597);
            this.lbl_TotalSampleNums.Name = "lbl_TotalSampleNums";
            this.lbl_TotalSampleNums.Size = new System.Drawing.Size(22, 21);
            this.lbl_TotalSampleNums.TabIndex = 43;
            this.lbl_TotalSampleNums.Text = "0";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label32.ForeColor = System.Drawing.Color.DarkBlue;
            this.label32.Location = new System.Drawing.Point(558, 559);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(76, 21);
            this.label32.TabIndex = 42;
            this.label32.Text = "总数：";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.tslblNowProj});
            this.statusStrip1.Location = new System.Drawing.Point(0, 810);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1291, 22);
            this.statusStrip1.TabIndex = 48;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(59, 17);
            this.toolStripStatusLabel1.Text = "当前项目:";
            // 
            // tslblNowProj
            // 
            this.tslblNowProj.Name = "tslblNowProj";
            this.tslblNowProj.Size = new System.Drawing.Size(131, 17);
            this.tslblNowProj.Text = "toolStripStatusLabel2";
            // 
            // pic5
            // 
            this.pic5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pic5.Location = new System.Drawing.Point(739, 459);
            this.pic5.Name = "pic5";
            this.pic5.Size = new System.Drawing.Size(86, 80);
            this.pic5.TabIndex = 13;
            this.pic5.TabStop = false;
            // 
            // pic_4
            // 
            this.pic_4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pic_4.Location = new System.Drawing.Point(6, 97);
            this.pic_4.Name = "pic_4";
            this.pic_4.Size = new System.Drawing.Size(80, 79);
            this.pic_4.TabIndex = 12;
            this.pic_4.TabStop = false;
            // 
            // pic_2
            // 
            this.pic_2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pic_2.Location = new System.Drawing.Point(93, 15);
            this.pic_2.Name = "pic_2";
            this.pic_2.Size = new System.Drawing.Size(80, 79);
            this.pic_2.TabIndex = 11;
            this.pic_2.TabStop = false;
            // 
            // pic_1
            // 
            this.pic_1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pic_1.Location = new System.Drawing.Point(6, 15);
            this.pic_1.Name = "pic_1";
            this.pic_1.Size = new System.Drawing.Size(80, 79);
            this.pic_1.TabIndex = 10;
            this.pic_1.TabStop = false;
            // 
            // pic_3
            // 
            this.pic_3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pic_3.Location = new System.Drawing.Point(177, 15);
            this.pic_3.Name = "pic_3";
            this.pic_3.Size = new System.Drawing.Size(80, 79);
            this.pic_3.TabIndex = 13;
            this.pic_3.TabStop = false;
            // 
            // pic_5
            // 
            this.pic_5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pic_5.Location = new System.Drawing.Point(93, 97);
            this.pic_5.Name = "pic_5";
            this.pic_5.Size = new System.Drawing.Size(80, 79);
            this.pic_5.TabIndex = 14;
            this.pic_5.TabStop = false;
            // 
            // pic_6
            // 
            this.pic_6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pic_6.Location = new System.Drawing.Point(177, 97);
            this.pic_6.Name = "pic_6";
            this.pic_6.Size = new System.Drawing.Size(80, 79);
            this.pic_6.TabIndex = 15;
            this.pic_6.TabStop = false;
            // 
            // pic_7
            // 
            this.pic_7.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pic_7.Location = new System.Drawing.Point(6, 179);
            this.pic_7.Name = "pic_7";
            this.pic_7.Size = new System.Drawing.Size(80, 79);
            this.pic_7.TabIndex = 16;
            this.pic_7.TabStop = false;
            // 
            // lbl_NGSampleNums2
            // 
            this.lbl_NGSampleNums2.AutoSize = true;
            this.lbl_NGSampleNums2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_NGSampleNums2.ForeColor = System.Drawing.Color.Crimson;
            this.lbl_NGSampleNums2.Location = new System.Drawing.Point(505, 70);
            this.lbl_NGSampleNums2.Name = "lbl_NGSampleNums2";
            this.lbl_NGSampleNums2.Size = new System.Drawing.Size(22, 21);
            this.lbl_NGSampleNums2.TabIndex = 34;
            this.lbl_NGSampleNums2.Text = "0";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.ForeColor = System.Drawing.Color.Red;
            this.label7.Location = new System.Drawing.Point(499, 38);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 21);
            this.label7.TabIndex = 33;
            this.label7.Text = "NG：";
            // 
            // lbl_OKSampleNums2
            // 
            this.lbl_OKSampleNums2.AutoSize = true;
            this.lbl_OKSampleNums2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_OKSampleNums2.ForeColor = System.Drawing.Color.Lime;
            this.lbl_OKSampleNums2.Location = new System.Drawing.Point(407, 69);
            this.lbl_OKSampleNums2.Name = "lbl_OKSampleNums2";
            this.lbl_OKSampleNums2.Size = new System.Drawing.Size(22, 21);
            this.lbl_OKSampleNums2.TabIndex = 32;
            this.lbl_OKSampleNums2.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.ForeColor = System.Drawing.Color.Lime;
            this.label16.Location = new System.Drawing.Point(401, 38);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(56, 21);
            this.label16.TabIndex = 31;
            this.label16.Text = "OK：";
            // 
            // lbl_TotalSampleNums2
            // 
            this.lbl_TotalSampleNums2.AutoSize = true;
            this.lbl_TotalSampleNums2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_TotalSampleNums2.ForeColor = System.Drawing.Color.DarkBlue;
            this.lbl_TotalSampleNums2.Location = new System.Drawing.Point(295, 69);
            this.lbl_TotalSampleNums2.Name = "lbl_TotalSampleNums2";
            this.lbl_TotalSampleNums2.Size = new System.Drawing.Size(22, 21);
            this.lbl_TotalSampleNums2.TabIndex = 30;
            this.lbl_TotalSampleNums2.Text = "0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.ForeColor = System.Drawing.Color.DarkBlue;
            this.label18.Location = new System.Drawing.Point(281, 38);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(76, 21);
            this.label18.TabIndex = 29;
            this.label18.Text = "总数：";
            // 
            // picShow
            // 
            this.picShow.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.picShow.Location = new System.Drawing.Point(1235, 28);
            this.picShow.Name = "picShow";
            this.picShow.Size = new System.Drawing.Size(306, 264);
            this.picShow.TabIndex = 35;
            this.picShow.TabStop = false;
            // 
            // rtxLog
            // 
            this.rtxLog.Location = new System.Drawing.Point(1235, 601);
            this.rtxLog.Name = "rtxLog";
            this.rtxLog.Size = new System.Drawing.Size(470, 221);
            this.rtxLog.TabIndex = 36;
            this.rtxLog.Text = "";
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNumber,
            this.Sample,
            this.OKorNG,
            this.Code,
            this.Score});
            this.dataGridView2.Location = new System.Drawing.Point(15, 146);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(519, 144);
            this.dataGridView2.TabIndex = 37;
            // 
            // SerialNumber
            // 
            this.SerialNumber.HeaderText = "序号";
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.Width = 80;
            // 
            // Sample
            // 
            this.Sample.HeaderText = "样本名称";
            this.Sample.Name = "Sample";
            this.Sample.Width = 150;
            // 
            // OKorNG
            // 
            this.OKorNG.HeaderText = "缺陷";
            this.OKorNG.Name = "OKorNG";
            this.OKorNG.Width = 80;
            // 
            // Code
            // 
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.Width = 80;
            // 
            // Score
            // 
            this.Score.HeaderText = "Score";
            this.Score.Name = "Score";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(1738, 644);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(63, 56);
            this.btn_Start.TabIndex = 38;
            this.btn_Start.Text = "启动";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // btn_Reset
            // 
            this.btn_Reset.Location = new System.Drawing.Point(1738, 766);
            this.btn_Reset.Name = "btn_Reset";
            this.btn_Reset.Size = new System.Drawing.Size(63, 56);
            this.btn_Reset.TabIndex = 40;
            this.btn_Reset.Text = "复位";
            this.btn_Reset.UseVisualStyleBackColor = true;
            this.btn_Reset.Click += new System.EventHandler(this.btn_Reset_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox1.Location = new System.Drawing.Point(2, 558);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(306, 264);
            this.pictureBox1.TabIndex = 41;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox2.Location = new System.Drawing.Point(616, 293);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(306, 264);
            this.pictureBox2.TabIndex = 42;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox3.Location = new System.Drawing.Point(309, 558);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(306, 264);
            this.pictureBox3.TabIndex = 44;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox4.Location = new System.Drawing.Point(616, 558);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(306, 264);
            this.pictureBox4.TabIndex = 35;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox8
            // 
            this.pictureBox8.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox8.Location = new System.Drawing.Point(2, 293);
            this.pictureBox8.Name = "pictureBox8";
            this.pictureBox8.Size = new System.Drawing.Size(306, 264);
            this.pictureBox8.TabIndex = 35;
            this.pictureBox8.TabStop = false;
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox5.Location = new System.Drawing.Point(309, 293);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(306, 264);
            this.pictureBox5.TabIndex = 45;
            this.pictureBox5.TabStop = false;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label19.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label19.Location = new System.Drawing.Point(312, 30);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(95, 12);
            this.label19.TabIndex = 46;
            this.label19.Text = "循环线上料定位A";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label20.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label20.Location = new System.Drawing.Point(619, 30);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(95, 12);
            this.label20.TabIndex = 47;
            this.label20.Text = "循环线上料定位B";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label21.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label21.Location = new System.Drawing.Point(2, 297);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(65, 12);
            this.label21.TabIndex = 48;
            this.label21.Text = "连接器正面";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label22.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label22.Location = new System.Drawing.Point(312, 297);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(65, 12);
            this.label22.TabIndex = 49;
            this.label22.Text = "连接器钢片";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label23.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label23.Location = new System.Drawing.Point(619, 298);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(53, 12);
            this.label23.TabIndex = 50;
            this.label23.Text = "本体反面";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label24.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label24.Location = new System.Drawing.Point(619, 562);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(53, 12);
            this.label24.TabIndex = 51;
            this.label24.Text = "本体正面";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label25.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label25.Location = new System.Drawing.Point(4, 562);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(53, 12);
            this.label25.TabIndex = 52;
            this.label25.Text = "本体短边";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label26.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label26.Location = new System.Drawing.Point(312, 562);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(53, 12);
            this.label26.TabIndex = 53;
            this.label26.Text = "本体长边";
            // 
            // hWindowControl5
            // 
            this.hWindowControl5.BackColor = System.Drawing.Color.Black;
            this.hWindowControl5.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl5.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl5.Location = new System.Drawing.Point(177, 179);
            this.hWindowControl5.Name = "hWindowControl5";
            this.hWindowControl5.Size = new System.Drawing.Size(80, 79);
            this.hWindowControl5.TabIndex = 8;
            this.hWindowControl5.WindowSize = new System.Drawing.Size(80, 79);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pic_1);
            this.groupBox1.Controls.Add(this.pic_3);
            this.groupBox1.Controls.Add(this.hWindowControl4);
            this.groupBox1.Controls.Add(this.pic_2);
            this.groupBox1.Controls.Add(this.hWindowControl5);
            this.groupBox1.Controls.Add(this.pic_4);
            this.groupBox1.Controls.Add(this.pic_5);
            this.groupBox1.Controls.Add(this.pic_6);
            this.groupBox1.Controls.Add(this.pic_7);
            this.groupBox1.Location = new System.Drawing.Point(1547, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(268, 262);
            this.groupBox1.TabIndex = 54;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "合成图";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label27.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label27.Location = new System.Drawing.Point(1235, 30);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(65, 12);
            this.label27.TabIndex = 55;
            this.label27.Text = "合成图预览";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(23, 38);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(77, 12);
            this.label28.TabIndex = 57;
            this.label28.Text = "上料定位坐标";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(23, 63);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(95, 12);
            this.label29.TabIndex = 57;
            this.label29.Text = "循环线上料定位A";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(23, 88);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(95, 12);
            this.label30.TabIndex = 57;
            this.label30.Text = "循环线上料定位B";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(124, 20);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(11, 12);
            this.label31.TabIndex = 58;
            this.label31.Text = "X";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(174, 20);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(11, 12);
            this.label33.TabIndex = 58;
            this.label33.Text = "Y";
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(224, 20);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(11, 12);
            this.label35.TabIndex = 58;
            this.label35.Text = "R";
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(124, 38);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(23, 12);
            this.label41.TabIndex = 59;
            this.label41.Text = "...";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(172, 38);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(23, 12);
            this.label42.TabIndex = 59;
            this.label42.Text = "...";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(224, 38);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(23, 12);
            this.label43.TabIndex = 59;
            this.label43.Text = "...";
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(124, 65);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(23, 12);
            this.label44.TabIndex = 59;
            this.label44.Text = "...";
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(172, 65);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(23, 12);
            this.label45.TabIndex = 59;
            this.label45.Text = "...";
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(224, 65);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(23, 12);
            this.label46.TabIndex = 59;
            this.label46.Text = "...";
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(124, 92);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(23, 12);
            this.label47.TabIndex = 59;
            this.label47.Text = "...";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(172, 92);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(23, 12);
            this.label48.TabIndex = 59;
            this.label48.Text = "...";
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(224, 92);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(23, 12);
            this.label49.TabIndex = 59;
            this.label49.Text = "...";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(31, 113);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(53, 12);
            this.label37.TabIndex = 60;
            this.label37.Text = "二维码：";
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label38.Location = new System.Drawing.Point(75, 114);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(11, 12);
            this.label38.TabIndex = 61;
            this.label38.Text = "0";
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label39.Location = new System.Drawing.Point(437, 9);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(75, 14);
            this.label39.TabIndex = 62;
            this.label39.Text = "当前项目:";
            // 
            // txt_ProjectName
            // 
            this.txt_ProjectName.AutoSize = true;
            this.txt_ProjectName.Font = new System.Drawing.Font("楷体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_ProjectName.Location = new System.Drawing.Point(508, 9);
            this.txt_ProjectName.Name = "txt_ProjectName";
            this.txt_ProjectName.Size = new System.Drawing.Size(37, 14);
            this.txt_ProjectName.TabIndex = 63;
            this.txt_ProjectName.Text = "待定";
            // 
            // hWindowControl6
            // 
            this.hWindowControl6.BackColor = System.Drawing.Color.Black;
            this.hWindowControl6.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl6.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl6.Location = new System.Drawing.Point(923, 28);
            this.hWindowControl6.Name = "hWindowControl6";
            this.hWindowControl6.Size = new System.Drawing.Size(306, 264);
            this.hWindowControl6.TabIndex = 64;
            this.hWindowControl6.WindowSize = new System.Drawing.Size(306, 264);
            // 
            // hWindowControl7
            // 
            this.hWindowControl7.BackColor = System.Drawing.Color.Black;
            this.hWindowControl7.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl7.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl7.Location = new System.Drawing.Point(923, 293);
            this.hWindowControl7.Name = "hWindowControl7";
            this.hWindowControl7.Size = new System.Drawing.Size(306, 264);
            this.hWindowControl7.TabIndex = 65;
            this.hWindowControl7.WindowSize = new System.Drawing.Size(306, 264);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label6.Location = new System.Drawing.Point(926, 30);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 12);
            this.label6.TabIndex = 66;
            this.label6.Text = "钢片定位A";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label15.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label15.Location = new System.Drawing.Point(926, 298);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(59, 12);
            this.label15.TabIndex = 67;
            this.label15.Text = "钢片定位B";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label28);
            this.groupBox2.Controls.Add(this.label29);
            this.groupBox2.Controls.Add(this.label30);
            this.groupBox2.Controls.Add(this.label31);
            this.groupBox2.Controls.Add(this.label33);
            this.groupBox2.Controls.Add(this.label35);
            this.groupBox2.Controls.Add(this.label41);
            this.groupBox2.Controls.Add(this.label38);
            this.groupBox2.Controls.Add(this.label42);
            this.groupBox2.Controls.Add(this.label37);
            this.groupBox2.Controls.Add(this.label44);
            this.groupBox2.Controls.Add(this.label49);
            this.groupBox2.Controls.Add(this.label47);
            this.groupBox2.Controls.Add(this.label46);
            this.groupBox2.Controls.Add(this.label43);
            this.groupBox2.Controls.Add(this.label48);
            this.groupBox2.Controls.Add(this.label45);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.lbl_TotalSampleNums2);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.lbl_OKSampleNums2);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.lbl_NGSampleNums2);
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Location = new System.Drawing.Point(1235, 295);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(580, 298);
            this.groupBox2.TabIndex = 68;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "显示区域";
            // 
            // pictureBox9
            // 
            this.pictureBox9.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.pictureBox9.Location = new System.Drawing.Point(923, 558);
            this.pictureBox9.Name = "pictureBox9";
            this.pictureBox9.Size = new System.Drawing.Size(306, 264);
            this.pictureBox9.TabIndex = 69;
            this.pictureBox9.TabStop = false;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label17.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label17.Location = new System.Drawing.Point(926, 562);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 12);
            this.label17.TabIndex = 70;
            this.label17.Text = "FPC检测";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1815, 831);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.pictureBox9);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.hWindowControl7);
            this.Controls.Add(this.hWindowControl6);
            this.Controls.Add(this.txt_ProjectName);
            this.Controls.Add(this.label39);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label25);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btn_Reset);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.rtxLog);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.pictureBox8);
            this.Controls.Add(this.picShow);
            this.Controls.Add(this.hWindowControl3);
            this.Controls.Add(this.hWindowControl2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "苹果电芯检测";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox9)).EndInit();
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
        private System.Windows.Forms.Label label1;
        private HalconDotNet.HWindowControl hWindowControl2;
        private HalconDotNet.HWindowControl hWindowControl3;
        private HalconDotNet.HWindowControl hWindowControl4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;


        private System.Windows.Forms.Button btn_ResetFeedBelt;
       
        private System.Windows.Forms.ToolStripMenuItem 坐标标定本体定位ToolStripMenuItem;
        private System.Windows.Forms.Label lblToolNo;
        private System.Windows.Forms.Label lblPosition;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripMenuItem 模板建立本体定位ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem plc设置上料ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pLC设置本体定位ToolStripMenuItem;

        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lbl_NGSampleNums;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label lbl_OKSampleNums;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label lbl_TotalSampleNums;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ToolStripMenuItem 定时删除文件设置ToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel tslblNowProj;
        private System.Windows.Forms.ToolStripMenuItem 项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 切换项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新建项目ToolStripMenuItem;

        private System.Windows.Forms.PictureBox pic5;
        private System.Windows.Forms.PictureBox pic_4;
        private System.Windows.Forms.PictureBox pic_2;
        private System.Windows.Forms.PictureBox pic_1;
        private System.Windows.Forms.PictureBox pic_3;
        private System.Windows.Forms.PictureBox pic_5;
        private System.Windows.Forms.PictureBox pic_6;
        private System.Windows.Forms.PictureBox pic_7;
        private System.Windows.Forms.Label lbl_NGSampleNums2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_OKSampleNums2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label lbl_TotalSampleNums2;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox picShow;
        private System.Windows.Forms.RichTextBox rtxLog;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sample;
        private System.Windows.Forms.DataGridViewTextBoxColumn OKorNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.Button btn_Start;
        private System.Windows.Forms.Button btn_Reset;
        private System.Windows.Forms.ToolStripMenuItem 坐标标定本体定位2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板建立本体定位2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置补偿值ToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox8;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private HalconDotNet.HWindowControl hWindowControl5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label txt_ProjectName;
        private System.Windows.Forms.ToolStripMenuItem 坐标标定钢片检测AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 坐标标定钢片检测BToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板建立钢片检测AToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 模板建立钢片能检测BToolStripMenuItem;
        private HalconDotNet.HWindowControl hWindowControl6;
        private HalconDotNet.HWindowControl hWindowControl7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox pictureBox9;
        private System.Windows.Forms.Label label17;
    }
}

