namespace StockTest
{
    partial class TargetAnalysis
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.combox_target = new System.Windows.Forms.ComboBox();
            this.btn_getHistory = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel_Kline = new System.Windows.Forms.Panel();
            this.panel_volumn = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.combox_target);
            this.panel1.Controls.Add(this.btn_getHistory);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1121, 100);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(903, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "指标切换";
            // 
            // combox_target
            // 
            this.combox_target.FormattingEnabled = true;
            this.combox_target.Location = new System.Drawing.Point(962, 33);
            this.combox_target.Name = "combox_target";
            this.combox_target.Size = new System.Drawing.Size(121, 20);
            this.combox_target.TabIndex = 1;
            // 
            // btn_getHistory
            // 
            this.btn_getHistory.Location = new System.Drawing.Point(24, 24);
            this.btn_getHistory.Name = "btn_getHistory";
            this.btn_getHistory.Size = new System.Drawing.Size(117, 36);
            this.btn_getHistory.TabIndex = 0;
            this.btn_getHistory.Text = "获取历史数据";
            this.btn_getHistory.UseVisualStyleBackColor = true;
            this.btn_getHistory.Click += new System.EventHandler(this.btn_getHistory_Click);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 528);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel_Kline);
            this.panel3.Controls.Add(this.panel_volumn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(200, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(921, 528);
            this.panel3.TabIndex = 2;
            // 
            // panel_Kline
            // 
            this.panel_Kline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Kline.Location = new System.Drawing.Point(0, 0);
            this.panel_Kline.Name = "panel_Kline";
            this.panel_Kline.Size = new System.Drawing.Size(921, 458);
            this.panel_Kline.TabIndex = 1;
            this.panel_Kline.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Kline_Paint);
            // 
            // panel_volumn
            // 
            this.panel_volumn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_volumn.Location = new System.Drawing.Point(0, 458);
            this.panel_volumn.Name = "panel_volumn";
            this.panel_volumn.Size = new System.Drawing.Size(921, 70);
            this.panel_volumn.TabIndex = 0;
            this.panel_volumn.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_volumn_Paint);
            // 
            // TargetAnalysis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1121, 628);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "TargetAnalysis";
            this.Text = "指标分析";
            this.Load += new System.EventHandler(this.TargetAnalysis_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel_Kline;
        private System.Windows.Forms.Panel panel_volumn;
        private System.Windows.Forms.Button btn_getHistory;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox combox_target;
    }
}