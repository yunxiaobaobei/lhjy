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
            this.panel4 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.check_lowVolumn = new System.Windows.Forms.CheckBox();
            this.check_higeVolumn = new System.Windows.Forms.CheckBox();
            this.check_in = new System.Windows.Forms.CheckBox();
            this.check_out = new System.Windows.Forms.CheckBox();
            this.check_sell = new System.Windows.Forms.CheckBox();
            this.check_buy = new System.Windows.Forms.CheckBox();
            this.pic_lowVolumn = new System.Windows.Forms.PictureBox();
            this.pic_higeVolumn = new System.Windows.Forms.PictureBox();
            this.pic_ableOut = new System.Windows.Forms.PictureBox();
            this.pic_abelIn = new System.Windows.Forms.PictureBox();
            this.pic_sell = new System.Windows.Forms.PictureBox();
            this.pic_buy = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel_Kline = new System.Windows.Forms.Panel();
            this.panel_volumn = new System.Windows.Forms.Panel();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_lowVolumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_higeVolumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ableOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_abelIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_sell)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_buy)).BeginInit();
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
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(245, 528);
            this.panel2.TabIndex = 1;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.label17);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.check_lowVolumn);
            this.panel4.Controls.Add(this.check_higeVolumn);
            this.panel4.Controls.Add(this.check_in);
            this.panel4.Controls.Add(this.check_out);
            this.panel4.Controls.Add(this.check_sell);
            this.panel4.Controls.Add(this.check_buy);
            this.panel4.Controls.Add(this.pic_lowVolumn);
            this.panel4.Controls.Add(this.pic_higeVolumn);
            this.panel4.Controls.Add(this.pic_ableOut);
            this.panel4.Controls.Add(this.pic_abelIn);
            this.panel4.Controls.Add(this.pic_sell);
            this.panel4.Controls.Add(this.pic_buy);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(239, 467);
            this.panel4.TabIndex = 2;
            this.panel4.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4_Paint);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(86, 218);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 16);
            this.label16.TabIndex = 33;
            this.label16.Text = "label16";
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(10, 218);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(58, 18);
            this.label17.TabIndex = 32;
            this.label17.Text = "振  幅";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(86, 186);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 16);
            this.label14.TabIndex = 31;
            this.label14.Text = "label14";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(10, 186);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(58, 18);
            this.label15.TabIndex = 30;
            this.label15.Text = "涨  幅";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(86, 156);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 16);
            this.label12.TabIndex = 29;
            this.label12.Text = "label12";
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label13.Location = new System.Drawing.Point(10, 156);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(58, 18);
            this.label13.TabIndex = 28;
            this.label13.Text = "最低价";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(86, 125);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 16);
            this.label10.TabIndex = 27;
            this.label10.Text = "label10";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(10, 125);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 18);
            this.label11.TabIndex = 26;
            this.label11.Text = "最高价";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // check_lowVolumn
            // 
            this.check_lowVolumn.AutoSize = true;
            this.check_lowVolumn.Checked = true;
            this.check_lowVolumn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_lowVolumn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_lowVolumn.Location = new System.Drawing.Point(17, 420);
            this.check_lowVolumn.Name = "check_lowVolumn";
            this.check_lowVolumn.Size = new System.Drawing.Size(90, 20);
            this.check_lowVolumn.TabIndex = 25;
            this.check_lowVolumn.Text = "量能低位";
            this.check_lowVolumn.UseVisualStyleBackColor = true;
            this.check_lowVolumn.Click += new System.EventHandler(this.check_lowVolumn_Click);
            // 
            // check_higeVolumn
            // 
            this.check_higeVolumn.AutoSize = true;
            this.check_higeVolumn.Checked = true;
            this.check_higeVolumn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_higeVolumn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_higeVolumn.Location = new System.Drawing.Point(17, 393);
            this.check_higeVolumn.Name = "check_higeVolumn";
            this.check_higeVolumn.Size = new System.Drawing.Size(90, 20);
            this.check_higeVolumn.TabIndex = 24;
            this.check_higeVolumn.Text = "量能高位";
            this.check_higeVolumn.UseVisualStyleBackColor = true;
            this.check_higeVolumn.Click += new System.EventHandler(this.check_higeVolumn_Click);
            // 
            // check_in
            // 
            this.check_in.AutoSize = true;
            this.check_in.Checked = true;
            this.check_in.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_in.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_in.Location = new System.Drawing.Point(17, 328);
            this.check_in.Name = "check_in";
            this.check_in.Size = new System.Drawing.Size(74, 20);
            this.check_in.TabIndex = 22;
            this.check_in.Text = "入场点";
            this.check_in.UseVisualStyleBackColor = true;
            this.check_in.Click += new System.EventHandler(this.check_in_Click);
            // 
            // check_out
            // 
            this.check_out.AutoSize = true;
            this.check_out.Checked = true;
            this.check_out.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_out.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_out.Location = new System.Drawing.Point(17, 359);
            this.check_out.Name = "check_out";
            this.check_out.Size = new System.Drawing.Size(74, 20);
            this.check_out.TabIndex = 23;
            this.check_out.Text = "出场点";
            this.check_out.UseVisualStyleBackColor = true;
            this.check_out.Click += new System.EventHandler(this.check_out_Click);
            // 
            // check_sell
            // 
            this.check_sell.AutoSize = true;
            this.check_sell.Checked = true;
            this.check_sell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_sell.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_sell.Location = new System.Drawing.Point(17, 296);
            this.check_sell.Name = "check_sell";
            this.check_sell.Size = new System.Drawing.Size(58, 20);
            this.check_sell.TabIndex = 21;
            this.check_sell.Text = "卖点";
            this.check_sell.UseVisualStyleBackColor = true;
            this.check_sell.Click += new System.EventHandler(this.check_sell_Click);
            // 
            // check_buy
            // 
            this.check_buy.AutoSize = true;
            this.check_buy.Checked = true;
            this.check_buy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_buy.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_buy.Location = new System.Drawing.Point(17, 263);
            this.check_buy.Name = "check_buy";
            this.check_buy.Size = new System.Drawing.Size(58, 20);
            this.check_buy.TabIndex = 20;
            this.check_buy.Text = "买点";
            this.check_buy.UseVisualStyleBackColor = true;
            this.check_buy.Click += new System.EventHandler(this.check_buy_Click);
            // 
            // pic_lowVolumn
            // 
            this.pic_lowVolumn.BackColor = System.Drawing.Color.Navy;
            this.pic_lowVolumn.Location = new System.Drawing.Point(120, 420);
            this.pic_lowVolumn.Name = "pic_lowVolumn";
            this.pic_lowVolumn.Size = new System.Drawing.Size(24, 24);
            this.pic_lowVolumn.TabIndex = 19;
            this.pic_lowVolumn.TabStop = false;
            this.pic_lowVolumn.Click += new System.EventHandler(this.pic_lowVolumn_Click);
            // 
            // pic_higeVolumn
            // 
            this.pic_higeVolumn.BackColor = System.Drawing.Color.IndianRed;
            this.pic_higeVolumn.Location = new System.Drawing.Point(120, 389);
            this.pic_higeVolumn.Name = "pic_higeVolumn";
            this.pic_higeVolumn.Size = new System.Drawing.Size(24, 24);
            this.pic_higeVolumn.TabIndex = 17;
            this.pic_higeVolumn.TabStop = false;
            this.pic_higeVolumn.Click += new System.EventHandler(this.pic_higeVolumn_Click);
            // 
            // pic_ableOut
            // 
            this.pic_ableOut.BackColor = System.Drawing.Color.Red;
            this.pic_ableOut.Location = new System.Drawing.Point(120, 355);
            this.pic_ableOut.Name = "pic_ableOut";
            this.pic_ableOut.Size = new System.Drawing.Size(24, 24);
            this.pic_ableOut.TabIndex = 15;
            this.pic_ableOut.TabStop = false;
            this.pic_ableOut.Click += new System.EventHandler(this.pic_ableOut_Click);
            // 
            // pic_abelIn
            // 
            this.pic_abelIn.BackColor = System.Drawing.Color.SpringGreen;
            this.pic_abelIn.Location = new System.Drawing.Point(120, 324);
            this.pic_abelIn.Name = "pic_abelIn";
            this.pic_abelIn.Size = new System.Drawing.Size(24, 24);
            this.pic_abelIn.TabIndex = 14;
            this.pic_abelIn.TabStop = false;
            this.pic_abelIn.Click += new System.EventHandler(this.pic_abelIn_Click);
            // 
            // pic_sell
            // 
            this.pic_sell.Location = new System.Drawing.Point(120, 292);
            this.pic_sell.Name = "pic_sell";
            this.pic_sell.Size = new System.Drawing.Size(24, 24);
            this.pic_sell.TabIndex = 13;
            this.pic_sell.TabStop = false;
            this.pic_sell.Click += new System.EventHandler(this.pic_sell_Click);
            // 
            // pic_buy
            // 
            this.pic_buy.Location = new System.Drawing.Point(120, 263);
            this.pic_buy.Name = "pic_buy";
            this.pic_buy.Size = new System.Drawing.Size(24, 24);
            this.pic_buy.TabIndex = 12;
            this.pic_buy.TabStop = false;
            this.pic_buy.Click += new System.EventHandler(this.pic_buy_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label8.Location = new System.Drawing.Point(86, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(10, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 18);
            this.label7.TabIndex = 6;
            this.label7.Text = "日  期";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(86, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "label6";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(10, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "点  位";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(86, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(86, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(10, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(58, 18);
            this.label3.TabIndex = 1;
            this.label3.Text = "收盘价";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(10, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(58, 18);
            this.label9.TabIndex = 0;
            this.label9.Text = "开盘价";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.panel_Kline);
            this.panel3.Controls.Add(this.panel_volumn);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(245, 100);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(876, 528);
            this.panel3.TabIndex = 2;
            // 
            // panel_Kline
            // 
            this.panel_Kline.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_Kline.Location = new System.Drawing.Point(0, 0);
            this.panel_Kline.Name = "panel_Kline";
            this.panel_Kline.Size = new System.Drawing.Size(876, 458);
            this.panel_Kline.TabIndex = 1;
            this.panel_Kline.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Kline_Paint);
            // 
            // panel_volumn
            // 
            this.panel_volumn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel_volumn.Location = new System.Drawing.Point(0, 458);
            this.panel_volumn.Name = "panel_volumn";
            this.panel_volumn.Size = new System.Drawing.Size(876, 70);
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
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_lowVolumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_higeVolumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ableOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_abelIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_sell)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_buy)).EndInit();
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
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox check_lowVolumn;
        private System.Windows.Forms.CheckBox check_higeVolumn;
        private System.Windows.Forms.CheckBox check_in;
        private System.Windows.Forms.CheckBox check_out;
        private System.Windows.Forms.CheckBox check_sell;
        private System.Windows.Forms.CheckBox check_buy;
        private System.Windows.Forms.PictureBox pic_lowVolumn;
        private System.Windows.Forms.PictureBox pic_higeVolumn;
        private System.Windows.Forms.PictureBox pic_ableOut;
        private System.Windows.Forms.PictureBox pic_abelIn;
        private System.Windows.Forms.PictureBox pic_sell;
        private System.Windows.Forms.PictureBox pic_buy;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ColorDialog colorDialog1;
    }
}