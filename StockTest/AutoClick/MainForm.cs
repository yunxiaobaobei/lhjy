using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace AutoClick
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        //查找指定窗口句柄
        private void btn_searchWindow_Click(object sender, EventArgs e)
        {
            if (txtBox_windowName.Text.Trim() == "")
            {
                MessageBox.Show("请输入需要查找的窗口名!");
                return;
            }

            string windowName = txtBox_windowName.Text;

            IntPtr mainHandle = WinAPI.FindWindow(null, windowName);


            if (mainHandle == IntPtr.Zero)
            {
                MessageBox.Show("未查找到指定的窗口");
                return;
            }
            else
            {
                MessageBox.Show($"查询到指定窗口!{windowName}");

                //获取窗口大小
                WinAPI.Rect rect = new WinAPI.Rect();
                WinAPI.GetWindowRect(mainHandle, out rect);

                richTextBox1.AppendText("窗口大小:" + rect);

                //查找子按钮
               //设置当前窗口位置和大小
               this.Location = new Point(rect.Left, rect.Top);

                //设置大小
                this.Size  = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);


                WinAPI.SendText("", mainHandle);
            }


        }

        bool isGetPos = false; //获取标志

        //获取鼠标的全局坐标
        private void btn_getMousePos_Click(object sender, EventArgs e)
        {

            isGetPos = !isGetPos;


        }

        //鼠标移动时间
        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (isGetPos == true)
            {
                Point tempP = new Point(e.X, e.Y);

                Point screenPos = PointToScreen(tempP);

                //richTextBox1.AppendText("控件坐标:" + tempP + "   全局坐标:" +   screenPos + "\r\n");

            }


        }

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {

            if (isGetPos) //记录下
            {
                richTextBox1.AppendText("记录坐标:" + e.X + " :" + e.Y);

                txtBox_X.Text = e.X.ToString();
                txtBox_Y.Text = e.Y.ToString();
            }

        }

        /// <summary>
        /// 发送事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_sendMsg_Click(object sender, EventArgs e)
        {
            WinAPI.TestNotepad();

            //int x = int.Parse(txtBox_X.Text)+ this.Location.X;
            //int y = int.Parse(txtBox_Y.Text)+ this.Location.Y;

            // //Point screen = PointToScreen(new Point( x, y));
            //Point screen = new Point(x + this.Location.X, y + this.Location.Y);
            //richTextBox1.AppendText("当前位置:" + this.Location  + "    鼠标点位:" + screen + "\r\n");
            ////richTextBox1.AppendText("鼠标点位:" + screen);

            ////移动
            //WinAPI.mouse_event((int)WinAPI.MouseEventFlags.Move | (int)WinAPI.MouseEventFlags.Absolute, x * 65535 / 1920, y * 65535 / 1080, 0, IntPtr.Zero);


            //WinAPI.mouse_event((int)WinAPI.MouseEventFlags.Move | (int)WinAPI.MouseEventFlags.Absolute, x , y, 0, IntPtr.Zero);

            //WinAPI.mouse_event((int)WinAPI.MouseEventFlags.Move, 20, 20, 0 ,IntPtr.Zero);  //相对于鼠标当前位置移动
            //WinAPI.mouse_event((int)WinAPI.MouseEventFlags.LeftDown, x, y, 0, IntPtr.Zero);//点击
            //WinAPI.mouse_event((int)WinAPI.MouseEventFlags.LeftUp, x , y , 0, IntPtr.Zero);//抬起


            //WinAPI.SendText("hello 123");
        }



       

    }
}
