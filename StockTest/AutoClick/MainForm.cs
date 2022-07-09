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
using System.Threading;

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
               // MessageBox.Show($"查询到指定窗口!{windowName}");

                //获取窗口大小
                WinAPI.Rect rect = new WinAPI.Rect();
                //WinAPI.GetWindowRect(mainHandle, out rect);

                richTextBox1.AppendText("窗口大小:" + rect);

                //查找子按钮
                //设置当前窗口位置和大小
                // this.Location = new Point(rect.Left, rect.Top);

                //设置大小
                //   this.Size  = new Size(rect.Right - rect.Left, rect.Bottom - rect.Top);


                //WinAPI.SendText("", mainHandle);

                //查找子窗口 // #32770 (对话框)

                // IntPtr childHandle = WinAPI.FindWindowEx(mainHandle, 0, "#32770 (对话框)", null);


               IntPtr dealButtonHandle = WinAPI.FindWindowExMy(mainHandle, "买入[B]", true);

                if (dealButtonHandle != IntPtr.Zero)
                {
                    //MessageBox.Show("找到交易按钮");

                    //发送点击事件
                    WinAPI.SendMessage2(dealButtonHandle, 0xF5, 0, 0);
                    return;

                    //激活窗口

                    WinAPI.SetForegroundWindow(mainHandle);
                    WinAPI.ShowWindow(mainHandle, 1);


                    //发送F1 F2 
                    WinAPI.keybd_event((byte)Keys.F1, 0, 0, 0);
                    WinAPI.keybd_event((byte)Keys.F1, 0, 2, 0);

                    Thread.Sleep(3000);
                   // MessageBox.Show("切换模式");

                    //WinAPI.SetForegroundWindow(mainHandle);
                    //WinAPI.ShowWindow(mainHandle, 1);

                    WinAPI.keybd_event((byte)Keys.F2, 0, 0, 0);
                    WinAPI.keybd_event((byte)Keys.F2, 0, 2, 0);

                    //发送点击事件
                     WinAPI.SendMessage2(dealButtonHandle, 0xF5, 0, 0);

                    System.Threading.Thread.Sleep(1000);

                    //WinAPI.keybd_event((byte)Keys.NumPad3 , 17, 0, 0);
                    // WinAPI.keybd_event((byte)Keys.NumPad3, 145, 2, 0);
                    char[] chars = "300087".ToCharArray();
                    for (int i = 0; i < chars.Length; i++)
                    {
                        Keys key = (Keys)chars[i]; //(Keys)Enum.Parse(typeof(Keys), chars[i].ToString());
                        Thread.Sleep(50);
                        WinAPI.keybd_event((byte)key, 0, 0, 0);
                        WinAPI.keybd_event((byte)key, 0, 2, 0);
                    }

                    ////匹配代码名称     匹配成功

                    //Thread.Sleep(1000);
                    ////发送点击事件
                    //WinAPI.SendMessage2(dealButtonHandle, 0xF5, 0, 0);
                }
                else
                {
                    MessageBox.Show("未找到交易按钮");
                }

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

        private void button1_Click(object sender, EventArgs e)
        {
            IntPtr prt = (IntPtr)0x00030652;

            WinAPI.SendMessage(prt, WinAPI.WM_SETTEXT, IntPtr.Zero, new StringBuilder("300064"));

            WinAPI.TestNotepad();

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        //自动交易按钮
        private void btn_autoDeal_Click(object sender, EventArgs e)
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
                //激活窗口
               WinAPI.SetForegroundWindow(mainHandle);
               WinAPI.ShowWindow(mainHandle, 1); //显示窗口， 正常大小

                //发送F1 切换换到买入模式
                //WinAPI.keybd_event((byte)Keys.F1, 0, 0, 0);
                //WinAPI.keybd_event((byte)Keys.F1, 0, 2, 0);


                //查看买入按钮
               Tuple<IntPtr, IntPtr> buyButton = WinAPI.FindWindowExMyWithParentHandle(mainHandle, "买入[B]", true);
                if (buyButton.Item1 != IntPtr.Zero)
                {

                    //模式切换正确
                    //发送点击事件
                    WinAPI.SendMessage2(buyButton.Item1, 0xF5, 0, 0);

                    Thread.Sleep(500);

                    //输入股票代码
                    char[] chars = "300087".ToCharArray();
                    for (int i = 0; i < chars.Length; i++)
                    {
                        Keys key = (Keys)chars[i]; //(Keys)Enum.Parse(typeof(Keys), chars[i].ToString());
                        Thread.Sleep(50);
                        WinAPI.keybd_event((byte)key, 0, 0, 0);
                        WinAPI.keybd_event((byte)key, 0, 2, 0);
                    }


                    //发送全仓买入事件
                    IntPtr fivtyPercent = WinAPI.FindWindowExByDimStrIntoWindow("100(全仓)");
                    fivtyPercent = WinAPI.FindWindowExMy(mainHandle, "1/2", true);
                    if (fivtyPercent != IntPtr.Zero)
                    {
                        ////发送点击事件
                        WinAPI.SendMessage2(buyButton.Item1, 0xF5, 0, 0);
                       // WinAPI.SendMessage2(buyButton.Item1, 0xF5, 2, 0);
                      //  WinAPI.SendMessage2(buyButton.Item1, 0xF5, 0, 0);
                        WinAPI.SendMessage2(buyButton.Item1, 0xF5,0, 0);

                    }


                }

            }


        }

    }
}
