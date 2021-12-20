namespace Dyestripping.DeviceSettingFrms
{
    partial class OmronFinsTcp
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtIpAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtPlcUnit = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtPcUnit = new System.Windows.Forms.TextBox();
            this.btnConn = new System.Windows.Forms.Button();
            this.btnDisConn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtxResultMsg = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btnWordWrite = new System.Windows.Forms.Button();
            this.btnBoolWrite = new System.Windows.Forms.Button();
            this.btnWordRead = new System.Windows.Forms.Button();
            this.btnBoolRead = new System.Windows.Forms.Button();
            this.txtWriteValue = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP地址：";
            // 
            // txtIpAddress
            // 
            this.txtIpAddress.Location = new System.Drawing.Point(68, 19);
            this.txtIpAddress.Name = "txtIpAddress";
            this.txtIpAddress.Size = new System.Drawing.Size(113, 21);
            this.txtIpAddress.TabIndex = 1;
            this.txtIpAddress.Text = "192.168.1.100";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(186, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "端口号：";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(235, 19);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(41, 21);
            this.txtPort.TabIndex = 1;
            this.txtPort.Text = "9600";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "PLC单元号：";
            // 
            // txtPlcUnit
            // 
            this.txtPlcUnit.Location = new System.Drawing.Point(86, 57);
            this.txtPlcUnit.Name = "txtPlcUnit";
            this.txtPlcUnit.Size = new System.Drawing.Size(34, 21);
            this.txtPlcUnit.TabIndex = 1;
            this.txtPlcUnit.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(143, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(77, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "本机网络号：";
            // 
            // txtPcUnit
            // 
            this.txtPcUnit.Location = new System.Drawing.Point(215, 57);
            this.txtPcUnit.Name = "txtPcUnit";
            this.txtPcUnit.Size = new System.Drawing.Size(34, 21);
            this.txtPcUnit.TabIndex = 1;
            this.txtPcUnit.Text = "90";
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(327, 22);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(75, 23);
            this.btnConn.TabIndex = 2;
            this.btnConn.Text = "连接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Visible = false;
            // 
            // btnDisConn
            // 
            this.btnDisConn.Location = new System.Drawing.Point(327, 51);
            this.btnDisConn.Name = "btnDisConn";
            this.btnDisConn.Size = new System.Drawing.Size(75, 23);
            this.btnDisConn.TabIndex = 3;
            this.btnDisConn.Text = "断开连接";
            this.btnDisConn.UseVisualStyleBackColor = true;
            this.btnDisConn.Visible = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxResultMsg);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnWordWrite);
            this.groupBox1.Controls.Add(this.btnBoolWrite);
            this.groupBox1.Controls.Add(this.btnWordRead);
            this.groupBox1.Controls.Add(this.btnBoolRead);
            this.groupBox1.Controls.Add(this.txtWriteValue);
            this.groupBox1.Controls.Add(this.txtAddress);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Location = new System.Drawing.Point(21, 95);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(381, 248);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据读取";
            // 
            // rtxResultMsg
            // 
            this.rtxResultMsg.Location = new System.Drawing.Point(47, 117);
            this.rtxResultMsg.Name = "rtxResultMsg";
            this.rtxResultMsg.Size = new System.Drawing.Size(223, 119);
            this.rtxResultMsg.TabIndex = 2;
            this.rtxResultMsg.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(141, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "长度：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 60);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "写入值：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 119);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "结果：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "地址：";
            // 
            // btnWordWrite
            // 
            this.btnWordWrite.Location = new System.Drawing.Point(300, 117);
            this.btnWordWrite.Name = "btnWordWrite";
            this.btnWordWrite.Size = new System.Drawing.Size(75, 23);
            this.btnWordWrite.TabIndex = 2;
            this.btnWordWrite.Text = "Word写入";
            this.btnWordWrite.UseVisualStyleBackColor = true;
            this.btnWordWrite.Click += new System.EventHandler(this.btnWordWrite_Click);
            // 
            // btnBoolWrite
            // 
            this.btnBoolWrite.Location = new System.Drawing.Point(300, 49);
            this.btnBoolWrite.Name = "btnBoolWrite";
            this.btnBoolWrite.Size = new System.Drawing.Size(75, 23);
            this.btnBoolWrite.TabIndex = 2;
            this.btnBoolWrite.Text = "Bool写入";
            this.btnBoolWrite.UseVisualStyleBackColor = true;
            this.btnBoolWrite.Click += new System.EventHandler(this.btnBoolWrite_Click);
            // 
            // btnWordRead
            // 
            this.btnWordRead.Location = new System.Drawing.Point(300, 88);
            this.btnWordRead.Name = "btnWordRead";
            this.btnWordRead.Size = new System.Drawing.Size(75, 23);
            this.btnWordRead.TabIndex = 2;
            this.btnWordRead.Text = "Word读取";
            this.btnWordRead.UseVisualStyleBackColor = true;
            this.btnWordRead.Click += new System.EventHandler(this.btnWordRead_Click);
            // 
            // btnBoolRead
            // 
            this.btnBoolRead.Location = new System.Drawing.Point(300, 20);
            this.btnBoolRead.Name = "btnBoolRead";
            this.btnBoolRead.Size = new System.Drawing.Size(75, 23);
            this.btnBoolRead.TabIndex = 2;
            this.btnBoolRead.Text = "Bool读取";
            this.btnBoolRead.UseVisualStyleBackColor = true;
            this.btnBoolRead.Click += new System.EventHandler(this.btnBoolRead_Click);
            // 
            // txtWriteValue
            // 
            this.txtWriteValue.Location = new System.Drawing.Point(59, 58);
            this.txtWriteValue.Name = "txtWriteValue";
            this.txtWriteValue.Size = new System.Drawing.Size(76, 21);
            this.txtWriteValue.TabIndex = 1;
            this.txtWriteValue.Text = "1";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(59, 22);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(76, 21);
            this.txtAddress.TabIndex = 1;
            this.txtAddress.Text = "1";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(188, 22);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(76, 21);
            this.txtLength.TabIndex = 1;
            this.txtLength.Text = "1";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(327, 349);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(300, 213);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "清空结果";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // OmronFinsTcp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnDisConn);
            this.Controls.Add(this.txtIpAddress);
            this.Controls.Add(this.txtPlcUnit);
            this.Controls.Add(this.btnConn);
            this.Controls.Add(this.txtPcUnit);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "OmronFinsTcp";
            this.Size = new System.Drawing.Size(428, 385);
            this.Load += new System.EventHandler(this.OmronFinsTcp_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtIpAddress;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtPlcUnit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPcUnit;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.Button btnDisConn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtxResultMsg;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnWordWrite;
        private System.Windows.Forms.Button btnBoolWrite;
        private System.Windows.Forms.Button btnWordRead;
        private System.Windows.Forms.Button btnBoolRead;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.TextBox txtLength;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtWriteValue;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
    }
}
