
namespace WindowsFormsApp1.TimeDelete
{
    partial class SetTimeDelete
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
            this.label1 = new System.Windows.Forms.Label();
            this.txt_setOriginalSaveTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_setCompressSaveTime = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_Set = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(184, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "本地原始图像保存周期：";
            // 
            // txt_setOriginalSaveTime
            // 
            this.txt_setOriginalSaveTime.Location = new System.Drawing.Point(190, 24);
            this.txt_setOriginalSaveTime.Name = "txt_setOriginalSaveTime";
            this.txt_setOriginalSaveTime.Size = new System.Drawing.Size(48, 21);
            this.txt_setOriginalSaveTime.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(244, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "天";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(244, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(24, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "天";
            // 
            // txt_setCompressSaveTime
            // 
            this.txt_setCompressSaveTime.Location = new System.Drawing.Point(190, 60);
            this.txt_setCompressSaveTime.Name = "txt_setCompressSaveTime";
            this.txt_setCompressSaveTime.Size = new System.Drawing.Size(48, 21);
            this.txt_setCompressSaveTime.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(184, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "本地压缩图像保存周期：";
            // 
            // btn_Set
            // 
            this.btn_Set.Location = new System.Drawing.Point(190, 100);
            this.btn_Set.Name = "btn_Set";
            this.btn_Set.Size = new System.Drawing.Size(103, 41);
            this.btn_Set.TabIndex = 6;
            this.btn_Set.Text = "确定";
            this.btn_Set.UseVisualStyleBackColor = true;
            this.btn_Set.Click += new System.EventHandler(this.btn_Set_Click);
            // 
            // SetTimeDelete
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(302, 169);
            this.Controls.Add(this.btn_Set);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_setCompressSaveTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_setOriginalSaveTime);
            this.Controls.Add(this.label1);
            this.Name = "SetTimeDelete";
            this.Text = "设置保存图片周期";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_setOriginalSaveTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_setCompressSaveTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_Set;
    }
}