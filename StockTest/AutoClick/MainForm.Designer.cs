namespace AutoClick
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
            this.btn_searchWindow = new System.Windows.Forms.Button();
            this.btn_getMousePos = new System.Windows.Forms.Button();
            this.txtBox_windowName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBox_X = new System.Windows.Forms.TextBox();
            this.txtBox_Y = new System.Windows.Forms.TextBox();
            this.btn_sendMsg = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_setStockCode = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_autoDeal = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_searchWindow
            // 
            this.btn_searchWindow.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_searchWindow.Location = new System.Drawing.Point(27, 32);
            this.btn_searchWindow.Name = "btn_searchWindow";
            this.btn_searchWindow.Size = new System.Drawing.Size(75, 23);
            this.btn_searchWindow.TabIndex = 0;
            this.btn_searchWindow.Text = "查找窗口";
            this.btn_searchWindow.UseVisualStyleBackColor = true;
            this.btn_searchWindow.Click += new System.EventHandler(this.btn_searchWindow_Click);
            // 
            // btn_getMousePos
            // 
            this.btn_getMousePos.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_getMousePos.Location = new System.Drawing.Point(27, 96);
            this.btn_getMousePos.Name = "btn_getMousePos";
            this.btn_getMousePos.Size = new System.Drawing.Size(123, 23);
            this.btn_getMousePos.TabIndex = 1;
            this.btn_getMousePos.Text = "获取鼠标位置";
            this.btn_getMousePos.UseVisualStyleBackColor = true;
            this.btn_getMousePos.Click += new System.EventHandler(this.btn_getMousePos_Click);
            // 
            // txtBox_windowName
            // 
            this.txtBox_windowName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_windowName.Location = new System.Drawing.Point(231, 32);
            this.txtBox_windowName.Name = "txtBox_windowName";
            this.txtBox_windowName.Size = new System.Drawing.Size(273, 26);
            this.txtBox_windowName.TabIndex = 2;
            this.txtBox_windowName.Text = "专业版下单";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(144, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "窗口名称";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(420, 424);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(328, 150);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(200, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 16);
            this.label2.TabIndex = 5;
            this.label2.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(365, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Y:";
            // 
            // txtBox_X
            // 
            this.txtBox_X.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_X.Location = new System.Drawing.Point(231, 96);
            this.txtBox_X.Name = "txtBox_X";
            this.txtBox_X.ReadOnly = true;
            this.txtBox_X.Size = new System.Drawing.Size(100, 26);
            this.txtBox_X.TabIndex = 7;
            // 
            // txtBox_Y
            // 
            this.txtBox_Y.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBox_Y.Location = new System.Drawing.Point(404, 96);
            this.txtBox_Y.Name = "txtBox_Y";
            this.txtBox_Y.ReadOnly = true;
            this.txtBox_Y.Size = new System.Drawing.Size(100, 26);
            this.txtBox_Y.TabIndex = 8;
            // 
            // btn_sendMsg
            // 
            this.btn_sendMsg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_sendMsg.Location = new System.Drawing.Point(27, 172);
            this.btn_sendMsg.Name = "btn_sendMsg";
            this.btn_sendMsg.Size = new System.Drawing.Size(123, 25);
            this.btn_sendMsg.TabIndex = 9;
            this.btn_sendMsg.Text = "发送键盘事件";
            this.btn_sendMsg.UseVisualStyleBackColor = true;
            this.btn_sendMsg.Click += new System.EventHandler(this.btn_sendMsg_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(27, 225);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 25);
            this.button2.TabIndex = 10;
            this.button2.Text = "发送鼠标事件";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_setStockCode
            // 
            this.btn_setStockCode.Location = new System.Drawing.Point(214, 167);
            this.btn_setStockCode.Name = "btn_setStockCode";
            this.btn_setStockCode.Size = new System.Drawing.Size(117, 30);
            this.btn_setStockCode.TabIndex = 11;
            this.btn_setStockCode.Text = "修改文本内容";
            this.btn_setStockCode.UseVisualStyleBackColor = true;
            this.btn_setStockCode.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(27, 277);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 25);
            this.button1.TabIndex = 12;
            this.button1.Text = "发送键盘事件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // btn_autoDeal
            // 
            this.btn_autoDeal.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_autoDeal.Location = new System.Drawing.Point(27, 343);
            this.btn_autoDeal.Name = "btn_autoDeal";
            this.btn_autoDeal.Size = new System.Drawing.Size(206, 88);
            this.btn_autoDeal.TabIndex = 13;
            this.btn_autoDeal.Text = "自动交易";
            this.btn_autoDeal.UseVisualStyleBackColor = true;
            this.btn_autoDeal.Click += new System.EventHandler(this.btn_autoDeal_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 603);
            this.Controls.Add(this.btn_autoDeal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_setStockCode);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_sendMsg);
            this.Controls.Add(this.txtBox_Y);
            this.Controls.Add(this.txtBox_X);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtBox_windowName);
            this.Controls.Add(this.btn_getMousePos);
            this.Controls.Add(this.btn_searchWindow);
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_searchWindow;
        private System.Windows.Forms.Button btn_getMousePos;
        private System.Windows.Forms.TextBox txtBox_windowName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBox_X;
        private System.Windows.Forms.TextBox txtBox_Y;
        private System.Windows.Forms.Button btn_sendMsg;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_setStockCode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btn_autoDeal;
    }
}

