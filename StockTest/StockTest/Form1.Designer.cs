namespace StockTest
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_dealAnalysis = new System.Windows.Forms.Button();
            this.btn_Analysis = new System.Windows.Forms.Button();
            this.btn_Quotec = new System.Windows.Forms.Button();
            this.btn_trade = new System.Windows.Forms.Button();
            this.btn_pankou = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_dealAnalysis);
            this.panel1.Controls.Add(this.btn_Analysis);
            this.panel1.Controls.Add(this.btn_Quotec);
            this.panel1.Controls.Add(this.btn_trade);
            this.panel1.Controls.Add(this.btn_pankou);
            this.panel1.Controls.Add(this.comboBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox1);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(970, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(199, 568);
            this.panel1.TabIndex = 0;
            // 
            // btn_dealAnalysis
            // 
            this.btn_dealAnalysis.Location = new System.Drawing.Point(8, 145);
            this.btn_dealAnalysis.Name = "btn_dealAnalysis";
            this.btn_dealAnalysis.Size = new System.Drawing.Size(75, 23);
            this.btn_dealAnalysis.TabIndex = 10;
            this.btn_dealAnalysis.Text = "数据分析";
            this.btn_dealAnalysis.UseVisualStyleBackColor = true;
            this.btn_dealAnalysis.Click += new System.EventHandler(this.btn_dealAnalysis_Click);
            // 
            // btn_Analysis
            // 
            this.btn_Analysis.Location = new System.Drawing.Point(8, 105);
            this.btn_Analysis.Name = "btn_Analysis";
            this.btn_Analysis.Size = new System.Drawing.Size(75, 23);
            this.btn_Analysis.TabIndex = 9;
            this.btn_Analysis.Text = "打开曲线图";
            this.btn_Analysis.UseVisualStyleBackColor = true;
            this.btn_Analysis.Click += new System.EventHandler(this.btn_Analysis_Click);
            // 
            // btn_Quotec
            // 
            this.btn_Quotec.Location = new System.Drawing.Point(111, 163);
            this.btn_Quotec.Name = "btn_Quotec";
            this.btn_Quotec.Size = new System.Drawing.Size(75, 23);
            this.btn_Quotec.TabIndex = 8;
            this.btn_Quotec.Text = "Quotec";
            this.btn_Quotec.UseVisualStyleBackColor = true;
            this.btn_Quotec.Click += new System.EventHandler(this.btn_Quotec_Click);
            // 
            // btn_trade
            // 
            this.btn_trade.Location = new System.Drawing.Point(111, 134);
            this.btn_trade.Name = "btn_trade";
            this.btn_trade.Size = new System.Drawing.Size(75, 23);
            this.btn_trade.TabIndex = 7;
            this.btn_trade.Text = "trade";
            this.btn_trade.UseVisualStyleBackColor = true;
            this.btn_trade.Click += new System.EventHandler(this.btn_trade_Click);
            // 
            // btn_pankou
            // 
            this.btn_pankou.Location = new System.Drawing.Point(111, 105);
            this.btn_pankou.Name = "btn_pankou";
            this.btn_pankou.Size = new System.Drawing.Size(75, 23);
            this.btn_pankou.TabIndex = 6;
            this.btn_pankou.Text = "pankou";
            this.btn_pankou.UseVisualStyleBackColor = true;
            this.btn_pankou.Click += new System.EventHandler(this.btn_pankou_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(65, 52);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 5;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "编码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "显示级别";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "5",
            "10"});
            this.comboBox1.Location = new System.Drawing.Point(65, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 2;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.richTextBox1.Location = new System.Drawing.Point(0, 197);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(199, 371);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // panel3
            // 
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 407);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(808, 161);
            this.panel3.TabIndex = 2;
            this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(this.panel3_Paint);
            // 
            // panel2
            // 
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(808, 407);
            this.panel2.TabIndex = 3;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            this.panel2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseMove);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.panel7);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel4.Location = new System.Drawing.Point(808, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(162, 568);
            this.panel4.TabIndex = 4;
            // 
            // panel7
            // 
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(162, 332);
            this.panel7.TabIndex = 1;
            this.panel7.Paint += new System.Windows.Forms.PaintEventHandler(this.panel7_Paint);
            // 
            // panel6
            // 
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 332);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(162, 236);
            this.panel6.TabIndex = 0;
            this.panel6.Paint += new System.Windows.Forms.PaintEventHandler(this.panel6_Paint);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel2);
            this.panel5.Controls.Add(this.panel3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(808, 568);
            this.panel5.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1169, 568);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button btn_pankou;
        private System.Windows.Forms.Button btn_trade;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btn_Quotec;
        private System.Windows.Forms.Button btn_Analysis;
        private System.Windows.Forms.Button btn_dealAnalysis;
    }
}

