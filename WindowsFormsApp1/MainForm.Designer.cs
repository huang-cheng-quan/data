
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
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // hWindowControl1
            // 
            this.hWindowControl1.BackColor = System.Drawing.Color.Black;
            this.hWindowControl1.BorderColor = System.Drawing.Color.Black;
            this.hWindowControl1.ImagePart = new System.Drawing.Rectangle(0, 0, 640, 480);
            this.hWindowControl1.Location = new System.Drawing.Point(1, 28);
            this.hWindowControl1.Name = "hWindowControl1";
            this.hWindowControl1.Size = new System.Drawing.Size(431, 349);
            this.hWindowControl1.TabIndex = 0;
            this.hWindowControl1.WindowSize = new System.Drawing.Size(431, 349);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1421, 25);
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
            this.相机设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.相机设置ToolStripMenuItem.Text = "相机设置";
            this.相机设置ToolStripMenuItem.Click += new System.EventHandler(this.相机设置ToolStripMenuItem_Click);
            // 
            // pLC设置ToolStripMenuItem
            // 
            this.pLC设置ToolStripMenuItem.Name = "pLC设置ToolStripMenuItem";
            this.pLC设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.pLC设置ToolStripMenuItem.Text = "PLC设置";
            this.pLC设置ToolStripMenuItem.Click += new System.EventHandler(this.pLC设置ToolStripMenuItem_Click);
            // 
            // 标定设置ToolStripMenuItem
            // 
            this.标定设置ToolStripMenuItem.Name = "标定设置ToolStripMenuItem";
            this.标定设置ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.标定设置ToolStripMenuItem.Text = "标定设置";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1421, 608);
            this.Controls.Add(this.hWindowControl1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "上料机";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
    }
}

