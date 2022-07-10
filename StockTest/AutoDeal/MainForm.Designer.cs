namespace AutoDeal
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
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_testClick = new System.Windows.Forms.Button();
            this.btn_testGetData = new System.Windows.Forms.Button();
            this.btn_testOldData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(22, 23);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(102, 46);
            this.btn_start.TabIndex = 0;
            this.btn_start.Text = "启动自动交易";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_testClick
            // 
            this.btn_testClick.Location = new System.Drawing.Point(22, 113);
            this.btn_testClick.Name = "btn_testClick";
            this.btn_testClick.Size = new System.Drawing.Size(102, 46);
            this.btn_testClick.TabIndex = 1;
            this.btn_testClick.Text = "测试点击流程";
            this.btn_testClick.UseVisualStyleBackColor = true;
            this.btn_testClick.Click += new System.EventHandler(this.btn_testClick_Click);
            // 
            // btn_testGetData
            // 
            this.btn_testGetData.Location = new System.Drawing.Point(22, 199);
            this.btn_testGetData.Name = "btn_testGetData";
            this.btn_testGetData.Size = new System.Drawing.Size(102, 46);
            this.btn_testGetData.TabIndex = 2;
            this.btn_testGetData.Text = "测试数据获取";
            this.btn_testGetData.UseVisualStyleBackColor = true;
            this.btn_testGetData.Click += new System.EventHandler(this.btn_testGetData_Click);
            // 
            // btn_testOldData
            // 
            this.btn_testOldData.Location = new System.Drawing.Point(22, 264);
            this.btn_testOldData.Name = "btn_testOldData";
            this.btn_testOldData.Size = new System.Drawing.Size(102, 46);
            this.btn_testOldData.TabIndex = 3;
            this.btn_testOldData.Text = "测试历史数据";
            this.btn_testOldData.UseVisualStyleBackColor = true;
            this.btn_testOldData.Click += new System.EventHandler(this.btn_testOldData_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1022, 533);
            this.Controls.Add(this.btn_testOldData);
            this.Controls.Add(this.btn_testGetData);
            this.Controls.Add(this.btn_testClick);
            this.Controls.Add(this.btn_start);
            this.Name = "MainForm";
            this.Text = "自动化交易";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_testClick;
        private System.Windows.Forms.Button btn_testGetData;
        private System.Windows.Forms.Button btn_testOldData;
    }
}

