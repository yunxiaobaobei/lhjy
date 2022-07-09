using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Serilog;
using System.Windows.Forms;

namespace AutoDeal
{
    public class AutoClickDeal
    {

        public enum DealType
        {
            buy = 0,  //买入
            sell = 1,  //卖出
            cancell = 2,    //撤单


        }


        public string stockCode = "300087"; //默认荃银高科

        public bool triggerDeal = false; //是否触发交易

        public DealType dealType = DealType.buy; //交易类型

        /// <summary>
        /// 开始交易线程
        /// </summary>
        public void StartDeal()
        {


            ThreadPool.QueueUserWorkItem((o) =>
            {

                Thread.Sleep(3000);



                IntPtr mainHandle = WinAPI.FindWindow(null, "专业版下单");


                if (mainHandle == IntPtr.Zero)
                {
                    Log.Warning("未查找到交易界面");
                    return;
                }
                else
                {
                    while (true)
                    {
                        Log.Information("查找到交易界面");
                        //前置交易窗体
                        WinAPI.SetForegroundWindow(mainHandle);
                        WinAPI.Rect rect = new WinAPI.Rect();

                        //找到输入框，清空输入框
                        Log.Information("清空股票代码");
                        WinAPI.GetWindowRect((IntPtr)00080660, out rect);//123456就是spy++得到的句柄，10进制
                        WinAPI.SetCursorPos(rect.Left + (rect.Right - rect.Left) / 2, rect.Top + (rect.Bottom - rect.Top) / 2);//鼠标操作
                        WinAPI.mouse_event((int)WinAPI.MouseEventFlags.LeftDown, 0, 0, 0, 0);//鼠标操作
                        WinAPI.mouse_event((int)WinAPI.MouseEventFlags.LeftUp, 0, 0, 0, 0);
                        Thread.Sleep(50);
                        WinAPI.keybd_event((byte)Keys.End, 0, 0, 0);
                        WinAPI.keybd_event((byte)Keys.End, 0, 2, 0);
                        for (int i = 0; i < 10; i++)
                        {
                            WinAPI.keybd_event((byte)Keys.Back, 0, 0, 0);
                            WinAPI.keybd_event((byte)Keys.Back, 0, 2, 0);

                            Thread.Sleep(50);
                        }

                        //是否开始交易
                        if (triggerDeal == true)
                        {
                            lock (stockCode)
                            {
                                switch (dealType)
                                {

                                    case DealType.sell:

                                        Log.Information("F2切换到卖出模式");
                                        Thread.Sleep(500);
                                        WinAPI.keybd_event((byte)Keys.F2, 0, 0, 0);
                                        WinAPI.keybd_event((byte)Keys.F2, 0, 2, 0);

                                        Log.Information("查找卖出按钮");
                                        IntPtr childHandle = WinAPI.FindWindowExMy(mainHandle, "卖出[S]", true);

                                        if (childHandle != IntPtr.Zero)
                                        {
                                            Log.Information("查找到卖出按钮");
                                            Log.Information("给卖出按钮发送点击事件");
                                            //发送点击事件
                                            WinAPI.SendMessage2(childHandle, 0xF5, 0, 0);     //发送点击按钮的消息

                                            //输入代码
                                            Log.Information("输入交易的股票代码");
                                            char[] code = stockCode.ToCharArray();

                                            foreach (char c in code)
                                            {
                                                WinAPI.keybd_event((byte)c, 0, 0, 0);
                                                WinAPI.keybd_event((byte)c, 0, 2, 0);
                                            }

                                            Thread.Sleep(1000);

                                            //模糊匹配全仓,找到其句柄
                                            IntPtr allInHandle = WinAPI.FindWindowExByDimStrIntoChildWindow(mainHandle, "全仓");

                                            if (allInHandle != IntPtr.Zero)
                                            {
                                                Log.Information("找到全仓按钮！\n");


                                                rect = new WinAPI.Rect();
                                                WinAPI.GetWindowRect(allInHandle, out rect);
                                                WinAPI.SetCursorPos(rect.Left + (rect.Right - rect.Left) / 2, rect.Top + (rect.Bottom - rect.Top) / 2);//鼠标操作

                                                Thread.Sleep(1000);
                                                Log.Information("模拟鼠标点击全仓");
                                                WinAPI.mouse_event((int)WinAPI.MouseEventFlags.LeftDown, 0, 0, 0, 0);//鼠标操作
                                                WinAPI.mouse_event((int)WinAPI.MouseEventFlags.LeftUp, 0, 0, 0, 0);

                                                Thread.Sleep(1000);
                                                Log.Information("给买入按钮发送确认消息");
                                                WinAPI.SendMessage2(childHandle, 0xF5, 0, 0);
                                            }
                                        }
                                        break;
                                    case DealType.buy:


                                        Log.Information("F1切换到买入模式");
                                        Thread.Sleep(500);
                                        WinAPI.keybd_event((byte)Keys.F1, 0, 0, 0);
                                        WinAPI.keybd_event((byte)Keys.F1, 0, 2, 0);

                                        Log.Information("查找买入按钮");
                                        childHandle = WinAPI.FindWindowExMy(mainHandle, "买入[B]", true);

                                        if (childHandle != IntPtr.Zero)
                                        {
                                            Log.Information("查找到买入按钮");
                                            Log.Information("给买入按钮发送点击事件");
                                            //发送点击事件
                                            WinAPI.SendMessage2(childHandle, 0xF5, 0, 0);     //发送点击按钮的消息

                                            //输入代码
                                            Log.Information("输入交易的股票代码");
                                            char[] code = stockCode.ToCharArray();

                                            foreach (char c in code)
                                            {
                                                WinAPI.keybd_event((byte)c, 0, 0, 0);
                                                WinAPI.keybd_event((byte)c, 0, 2, 0);
                                            }

                                            Thread.Sleep(1000);

                                            //模糊匹配全仓,找到其句柄
                                            IntPtr allInHandle = WinAPI.FindWindowExByDimStrIntoChildWindow(mainHandle, "全仓");

                                            if (allInHandle != IntPtr.Zero)
                                            {
                                                Log.Information("找到全仓按钮！\n");


                                                rect = new WinAPI.Rect();
                                                WinAPI.GetWindowRect(allInHandle, out rect);
                                                WinAPI.SetCursorPos(rect.Left + (rect.Right - rect.Left) / 2, rect.Top + (rect.Bottom - rect.Top) / 2);//鼠标操作

                                                Thread.Sleep(1000);
                                                Log.Information("模拟鼠标点击全仓");
                                                WinAPI.mouse_event((int)WinAPI.MouseEventFlags.LeftDown, 0, 0, 0, 0);//鼠标操作
                                                WinAPI.mouse_event((int)WinAPI.MouseEventFlags.LeftUp, 0, 0, 0, 0);

                                                Thread.Sleep(1000);
                                                Log.Information("给买入按钮发送确认消息");
                                                WinAPI.SendMessage2(childHandle, 0xF5, 0, 0);
                                            }
                                        }

                                        break;
                                    case DealType.cancell:
                                        break;
                                    default:
                                        break;
                                }
                            }
                            
                            triggerDeal = false; //交易结束
                        }
                        else
                        {
                            Thread.Sleep(2000); //休眠2s
                        }
                    }
                }




            }, null);

        }



    }
}
