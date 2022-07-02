namespace StockTest
{
    partial class UserControl1
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
            this.components = new System.ComponentModel.Container();
            this.targetMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
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
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_lowVolumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_higeVolumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ableOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_abelIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_sell)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_buy)).BeginInit();
            this.SuspendLayout();
            // 
            // targetMenu
            // 
            this.targetMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.targetMenu.Name = "targetMenu";
            this.targetMenu.Size = new System.Drawing.Size(61, 4);
            this.targetMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.targetMenu_ItemClicked);
            this.targetMenu.MouseClick += new System.Windows.Forms.MouseEventHandler(this.targetMenu_MouseClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Controls.Add(this.check_lowVolumn);
            this.panel1.Controls.Add(this.check_higeVolumn);
            this.panel1.Controls.Add(this.check_in);
            this.panel1.Controls.Add(this.check_out);
            this.panel1.Controls.Add(this.check_sell);
            this.panel1.Controls.Add(this.check_buy);
            this.panel1.Controls.Add(this.pic_lowVolumn);
            this.panel1.Controls.Add(this.pic_higeVolumn);
            this.panel1.Controls.Add(this.pic_ableOut);
            this.panel1.Controls.Add(this.pic_abelIn);
            this.panel1.Controls.Add(this.pic_sell);
            this.panel1.Controls.Add(this.pic_buy);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(165, 334);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // check_lowVolumn
            // 
            this.check_lowVolumn.AutoSize = true;
            this.check_lowVolumn.Checked = true;
            this.check_lowVolumn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_lowVolumn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_lowVolumn.Location = new System.Drawing.Point(11, 287);
            this.check_lowVolumn.Name = "check_lowVolumn";
            this.check_lowVolumn.Size = new System.Drawing.Size(90, 20);
            this.check_lowVolumn.TabIndex = 25;
            this.check_lowVolumn.Text = "量能低位";
            this.check_lowVolumn.UseVisualStyleBackColor = true;
            this.check_lowVolumn.Visible = false;
            // 
            // check_higeVolumn
            // 
            this.check_higeVolumn.AutoSize = true;
            this.check_higeVolumn.Checked = true;
            this.check_higeVolumn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_higeVolumn.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_higeVolumn.Location = new System.Drawing.Point(11, 260);
            this.check_higeVolumn.Name = "check_higeVolumn";
            this.check_higeVolumn.Size = new System.Drawing.Size(90, 20);
            this.check_higeVolumn.TabIndex = 24;
            this.check_higeVolumn.Text = "量能高位";
            this.check_higeVolumn.UseVisualStyleBackColor = true;
            this.check_higeVolumn.Visible = false;
            // 
            // check_in
            // 
            this.check_in.AutoSize = true;
            this.check_in.Checked = true;
            this.check_in.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_in.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_in.Location = new System.Drawing.Point(11, 195);
            this.check_in.Name = "check_in";
            this.check_in.Size = new System.Drawing.Size(74, 20);
            this.check_in.TabIndex = 22;
            this.check_in.Text = "入场点";
            this.check_in.UseVisualStyleBackColor = true;
            this.check_in.Visible = false;
            // 
            // check_out
            // 
            this.check_out.AutoSize = true;
            this.check_out.Checked = true;
            this.check_out.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_out.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_out.Location = new System.Drawing.Point(11, 226);
            this.check_out.Name = "check_out";
            this.check_out.Size = new System.Drawing.Size(74, 20);
            this.check_out.TabIndex = 23;
            this.check_out.Text = "出场点";
            this.check_out.UseVisualStyleBackColor = true;
            this.check_out.Visible = false;
            // 
            // check_sell
            // 
            this.check_sell.AutoSize = true;
            this.check_sell.Checked = true;
            this.check_sell.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_sell.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_sell.Location = new System.Drawing.Point(11, 163);
            this.check_sell.Name = "check_sell";
            this.check_sell.Size = new System.Drawing.Size(58, 20);
            this.check_sell.TabIndex = 21;
            this.check_sell.Text = "卖点";
            this.check_sell.UseVisualStyleBackColor = true;
            this.check_sell.Visible = false;
            // 
            // check_buy
            // 
            this.check_buy.AutoSize = true;
            this.check_buy.Checked = true;
            this.check_buy.CheckState = System.Windows.Forms.CheckState.Checked;
            this.check_buy.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.check_buy.Location = new System.Drawing.Point(11, 130);
            this.check_buy.Name = "check_buy";
            this.check_buy.Size = new System.Drawing.Size(58, 20);
            this.check_buy.TabIndex = 20;
            this.check_buy.Text = "买点";
            this.check_buy.UseVisualStyleBackColor = true;
            this.check_buy.Visible = false;
            this.check_buy.Click += new System.EventHandler(this.check_buy_Click);
            this.check_buy.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.check_buy_PreviewKeyDown);
            // 
            // pic_lowVolumn
            // 
            this.pic_lowVolumn.BackColor = System.Drawing.Color.Navy;
            this.pic_lowVolumn.Location = new System.Drawing.Point(114, 287);
            this.pic_lowVolumn.Name = "pic_lowVolumn";
            this.pic_lowVolumn.Size = new System.Drawing.Size(24, 24);
            this.pic_lowVolumn.TabIndex = 19;
            this.pic_lowVolumn.TabStop = false;
            this.pic_lowVolumn.Click += new System.EventHandler(this.pic_lowVolumn_Click);
            // 
            // pic_higeVolumn
            // 
            this.pic_higeVolumn.BackColor = System.Drawing.Color.IndianRed;
            this.pic_higeVolumn.Location = new System.Drawing.Point(114, 256);
            this.pic_higeVolumn.Name = "pic_higeVolumn";
            this.pic_higeVolumn.Size = new System.Drawing.Size(24, 24);
            this.pic_higeVolumn.TabIndex = 17;
            this.pic_higeVolumn.TabStop = false;
            this.pic_higeVolumn.Click += new System.EventHandler(this.pic_higeVolumn_Click);
            // 
            // pic_ableOut
            // 
            this.pic_ableOut.BackColor = System.Drawing.Color.Red;
            this.pic_ableOut.Location = new System.Drawing.Point(114, 222);
            this.pic_ableOut.Name = "pic_ableOut";
            this.pic_ableOut.Size = new System.Drawing.Size(24, 24);
            this.pic_ableOut.TabIndex = 15;
            this.pic_ableOut.TabStop = false;
            this.pic_ableOut.Click += new System.EventHandler(this.pic_ableOut_Click);
            // 
            // pic_abelIn
            // 
            this.pic_abelIn.BackColor = System.Drawing.Color.SpringGreen;
            this.pic_abelIn.Location = new System.Drawing.Point(114, 191);
            this.pic_abelIn.Name = "pic_abelIn";
            this.pic_abelIn.Size = new System.Drawing.Size(24, 24);
            this.pic_abelIn.TabIndex = 14;
            this.pic_abelIn.TabStop = false;
            this.pic_abelIn.Click += new System.EventHandler(this.pic_abelIn_Click);
            // 
            // pic_sell
            // 
            this.pic_sell.Location = new System.Drawing.Point(114, 159);
            this.pic_sell.Name = "pic_sell";
            this.pic_sell.Size = new System.Drawing.Size(24, 24);
            this.pic_sell.TabIndex = 13;
            this.pic_sell.TabStop = false;
            this.pic_sell.Click += new System.EventHandler(this.pic_sell_Click);
            // 
            // pic_buy
            // 
            this.pic_buy.Location = new System.Drawing.Point(114, 130);
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
            this.label8.Location = new System.Drawing.Point(87, 96);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 16);
            this.label8.TabIndex = 7;
            this.label8.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(14, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(39, 16);
            this.label7.TabIndex = 6;
            this.label7.Text = "日期";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(87, 69);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "label6";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(14, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "点位";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(87, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(87, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(14, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "收盘价";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(14, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "开盘价";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Controls.Add(this.panel1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(798, 411);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserControl1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserControl1_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseMove);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.UserControl1_PreviewKeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pic_lowVolumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_higeVolumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_ableOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_abelIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_sell)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_buy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip targetMenu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pic_ableOut;
        private System.Windows.Forms.PictureBox pic_abelIn;
        private System.Windows.Forms.PictureBox pic_sell;
        private System.Windows.Forms.PictureBox pic_buy;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.PictureBox pic_lowVolumn;
        private System.Windows.Forms.PictureBox pic_higeVolumn;
        private System.Windows.Forms.CheckBox check_lowVolumn;
        private System.Windows.Forms.CheckBox check_higeVolumn;
        private System.Windows.Forms.CheckBox check_in;
        private System.Windows.Forms.CheckBox check_out;
        private System.Windows.Forms.CheckBox check_sell;
        private System.Windows.Forms.CheckBox check_buy;
    }
}
