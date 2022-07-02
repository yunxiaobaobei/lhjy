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
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(798, 411);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UserControl1_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.UserControl1_KeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseClick);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.UserControl1_MouseMove);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.UserControl1_PreviewKeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip targetMenu;
    }
}
