
namespace WindowsFormsApp1.SqlServer
{
    partial class SqlConnectionForm
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_SaveSqlParam = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSQLOK = new System.Windows.Forms.Button();
            this.txtPwd = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDataBase = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetData = new System.Windows.Forms.Button();
            this.GridView1 = new System.Windows.Forms.DataGridView();
            this.SerialNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sample = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OKorNG = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_SaveSqlParam);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnSQLOK);
            this.groupBox1.Controls.Add(this.txtPwd);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtUser);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtDataBase);
            this.groupBox1.Location = new System.Drawing.Point(4, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(525, 127);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "SQL Server 身份验证";
            // 
            // btn_SaveSqlParam
            // 
            this.btn_SaveSqlParam.Location = new System.Drawing.Point(409, 98);
            this.btn_SaveSqlParam.Name = "btn_SaveSqlParam";
            this.btn_SaveSqlParam.Size = new System.Drawing.Size(100, 23);
            this.btn_SaveSqlParam.TabIndex = 3;
            this.btn_SaveSqlParam.Text = "保存连接参数";
            this.btn_SaveSqlParam.UseVisualStyleBackColor = true;
            this.btn_SaveSqlParam.Click += new System.EventHandler(this.btn_SaveSqlParam_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "服务器IP：";
            // 
            // btnSQLOK
            // 
            this.btnSQLOK.Location = new System.Drawing.Point(289, 98);
            this.btnSQLOK.Name = "btnSQLOK";
            this.btnSQLOK.Size = new System.Drawing.Size(75, 23);
            this.btnSQLOK.TabIndex = 0;
            this.btnSQLOK.Text = "测试连接";
            this.btnSQLOK.UseVisualStyleBackColor = true;
            this.btnSQLOK.Click += new System.EventHandler(this.btnSQLOK_Click);
            // 
            // txtPwd
            // 
            this.txtPwd.Location = new System.Drawing.Point(334, 62);
            this.txtPwd.Name = "txtPwd";
            this.txtPwd.Size = new System.Drawing.Size(175, 21);
            this.txtPwd.TabIndex = 2;
            this.txtPwd.Text = "123456";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(274, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "密码：";
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(80, 32);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(175, 21);
            this.txtIP.TabIndex = 2;
            this.txtIP.Text = "DESKTOP-90KHT63";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "数据库名：";
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(334, 32);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(175, 21);
            this.txtUser.TabIndex = 2;
            this.txtUser.Text = "sa";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(274, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 1;
            this.label3.Text = "用户名：";
            // 
            // txtDataBase
            // 
            this.txtDataBase.Location = new System.Drawing.Point(80, 62);
            this.txtDataBase.Name = "txtDataBase";
            this.txtDataBase.Size = new System.Drawing.Size(175, 21);
            this.txtDataBase.TabIndex = 2;
            this.txtDataBase.Text = "nvtData";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetData);
            this.groupBox2.Controls.Add(this.GridView1);
            this.groupBox2.Location = new System.Drawing.Point(7, 145);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(520, 296);
            this.groupBox2.TabIndex = 45;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据库列表";
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(527, 318);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(75, 23);
            this.btnGetData.TabIndex = 4;
            this.btnGetData.Text = "获得数据库数据";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // GridView1
            // 
            this.GridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SerialNumber,
            this.Sample,
            this.OKorNG,
            this.Code,
            this.Score});
            this.GridView1.Location = new System.Drawing.Point(0, 20);
            this.GridView1.Name = "GridView1";
            this.GridView1.RowTemplate.Height = 23;
            this.GridView1.Size = new System.Drawing.Size(513, 269);
            this.GridView1.TabIndex = 38;
            // 
            // SerialNumber
            // 
            this.SerialNumber.DataPropertyName = "SerialNumber";
            this.SerialNumber.HeaderText = "SerialNumber";
            this.SerialNumber.Name = "SerialNumber";
            this.SerialNumber.Width = 80;
            // 
            // Sample
            // 
            this.Sample.DataPropertyName = "Sample";
            this.Sample.HeaderText = "Sample";
            this.Sample.Name = "Sample";
            this.Sample.Width = 150;
            // 
            // OKorNG
            // 
            this.OKorNG.DataPropertyName = "OKorNG";
            this.OKorNG.HeaderText = "OKorNG";
            this.OKorNG.Name = "OKorNG";
            this.OKorNG.Width = 80;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "Code";
            this.Code.Name = "Code";
            this.Code.Width = 80;
            // 
            // Score
            // 
            this.Score.DataPropertyName = "Score";
            this.Score.HeaderText = "Score";
            this.Score.Name = "Score";
            // 
            // SqlConnectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 442);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "SqlConnectionForm";
            this.Text = "SqlConnectionForm";
            this.Load += new System.EventHandler(this.SqlConnectionForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSQLOK;
        private System.Windows.Forms.TextBox txtPwd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDataBase;
        private System.Windows.Forms.Button btn_SaveSqlParam;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.DataGridView GridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn SerialNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sample;
        private System.Windows.Forms.DataGridViewTextBoxColumn OKorNG;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
    }
}