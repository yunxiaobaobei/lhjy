using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Serilog;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Skender.Stock.Indicators;

namespace AutoDeal
{
    public class AutoClickDeal
    {
        //全局唯一的交易模型
        public DealInfo dealInfo = new DealInfo();

        public bool isBuy = false; //是否已经买入

        public string stockCode = "300087"; //默认荃银高科

        public bool triggerDeal = false; //是否触发交易

        public DealType dealType = DealType.buy; //交易类型

        public static AutoClickDeal instance = null;

        public static AutoClickDeal GetInstance()
        {
            if (instance == null)
            {
                instance = new AutoClickDeal();
            }

            return instance;
        }


        private AutoClickDeal()
        {

            dealInfo.InitMoney = 10000;
            dealInfo.DealCount = 0;


            //加载配置文件
            string fileName = AppDomain.CurrentDomain.BaseDirectory + "deal.json";
            if (File.Exists(fileName))
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fs);
                string res = streamReader.ReadToEnd();

                if (res != "")
                {
                    dealInfo = JsonConvert.DeserializeObject<DealInfo>(res);

                    isBuy = true;
                }
                else
                    isBuy = false;

                streamReader.Close();
                fs.Close(); 
            }

            Strategic_one.TriggerDealEvent += TraggleDeal;
        }

        /// <summary>
        /// 开始交易线程
        /// </summary>
        public void StartDeal()
        {
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Log.Information("3s后开始模拟点击");
                Thread.Sleep(3000);

                IntPtr mainHandle = WinAPI.FindWindow(null, "专业版下单");


                if (mainHandle == IntPtr.Zero)
                {
                    Log.Warning("未查找到交易界面");
                    return;
                }
                else
                {
                    Log.Information("查找到交易界面");

                    //前置交易窗体
                    WinAPI.Rect rect = new WinAPI.Rect();
                    WinAPI.SetForegroundWindow(mainHandle);

                    Log.Information("置顶交易界面");
                    WinAPI.GetWindowRect(mainHandle, out rect);
                    WinAPI.SetWindowPos(mainHandle, (IntPtr)WinAPI.HWND_TOPMOST, rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top, 0);

                    while (true)
                    {
                        //是否开始交易
                        if (triggerDeal == true)
                        {

                            //找到输入框，清空输入框
                            Log.Information("清空股票代码");
                            WinAPI.GetWindowRect((IntPtr)0x00040A56, out rect);//123456就是spy++得到的句柄，10进制
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
                                           //Log.Information("给卖出按钮发送点击事件");
                                            //发送点击事件
                                            //WinAPI.SendMessage2(childHandle, 0xF5, 0, 0);     //发送点击按钮的消息

                                            //输入代码
                                            Thread.Sleep(100);
                                            Log.Information($"输入交易的股票代码:{stockCode}");
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
                                                
                                                Thread.Sleep(1000);
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
                                            //Log.Information("给买入按钮发送点击事件");
                                            //发送点击事件
                                           // WinAPI.SendMessage2(childHandle, 0xF5, 0, 0);     //发送点击按钮的消息

                                            //输入代码
                                           
                                            Log.Information($"输入交易的股票代码:{stockCode}");
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
                                                Log.Information("模拟鼠标点击全仓");
                                                Thread.Sleep(1000);
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


                            //撤销交易
                            //查找撤销按钮句柄
                            IntPtr cancelDealHandle = WinAPI.FindWindowEx(mainHandle, IntPtr.Zero, "Button", "全撤[Z /]"); //WinAPI.FindWindowExByDimStrIntoChildWindow(mainHandle, "全撤", "Button");
                            if (cancelDealHandle != IntPtr.Zero)
                            {
                                Log.Information("2s后撤单");
                                Thread.Sleep(2000);
                                WinAPI.SendMessage2(cancelDealHandle, 0xF5, 0, 0);
                            }


                                triggerDeal = false; //交易结束
                        }
                        else
                        {
                            Thread.Sleep(1000); //休眠1s
                        }
                    }
                }




            }, null);

        }



        public void TraggleDeal(string code, DealType type, Quote quote)
        {

            Log.Information("收到交易指令");

            if (triggerDeal == false)
            {
                triggerDeal = true;
                dealType = type;
                stockCode = code;
                
                ThreadPool.QueueUserWorkItem( (o) =>
                {
                    Log.Information("更新交易文件");
                    //更新交易文件
                    string fileName = AppDomain.CurrentDomain.BaseDirectory + "deal.json";
                    FileStream fs = null;
                    if (File.Exists(fileName))
                    {
                        fs = new FileStream(fileName, FileMode.Truncate, FileAccess.Write);
                    }
                    else
                    {
                        fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                    }

                    StreamWriter sw = new StreamWriter(fs);
                    string res = JsonConvert.SerializeObject(dealInfo);

                    if (type == DealType.buy)
                        sw.Write(res);
                    else if (type == DealType.sell)
                        sw.Write("");
                        

                    sw.Close();
                    fs.Close();

                }, null);

            }
            else
            {
                Log.Information("交易中，忽略本次指令....");
            }
        }

    }
}
