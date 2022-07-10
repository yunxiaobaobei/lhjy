using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Threading;
using Serilog;
using System.Threading.Tasks;
using Skender.Stock.Indicators;

namespace StockTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Log = new LoggerConfiguration()
         .WriteTo.Async(w => w.File("Logs/log-.log", rollingInterval: RollingInterval.Day))
         .MinimumLevel.Debug()
         .CreateLogger();
        }

        public static ILogger Log = null;

        OneMinuteDraw oneMinuteDraw = new OneMinuteDraw();

        OneMinute min = null;

        Record stockList = null;


        private List<Tuple<float?, int>> SSpankouList = new List<Tuple<float?, int>>();

        private List<Tuple<string, float, int>> MMtrade = new List<Tuple<string, float, int>>();


        Bitmap klinePic = null;

        Graphics klineG = null;

        string stockNum = "SZ300087";


        private void Form1_Load(object sender, EventArgs e)
        {


            oneMinuteDraw.Prices = new List<double>();
            oneMinuteDraw.Ave_prices = new List<double>();
            oneMinuteDraw.Amount = new List<double>();
            oneMinuteDraw.Ave_amout = new List<double>();
            oneMinuteDraw.KeyPointSel = new List<int>();
            oneMinuteDraw.KeyPointBuy = new List<int>();
            //ThreadPool.QueueUserWorkItem( (o) =>  LoadBaseInfo(), null);


            klinePic = new Bitmap(panel2.Width, panel2.Height);
            klineG = Graphics.FromImage(klinePic);
            klineG.Clear(Color.White);

           // LoadBaseInfo();




           // string html = GetStockOnlineInfo(stockNum);

           // CalcPrice(html);


            comboBox1.SelectedIndex = 0;


            //2s后执行
           // richTextBox1.AppendText("2s后执行数据抓取！间隔5s");
           // System.Threading.Timer time = new System.Threading.Timer((o) =>
           //{
           //    QuotecInfo(stockNum);
           //}, null, 2000, 5000);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(Color.White);

           
            g.DrawImage(klinePic, new RectangleF(PointF.Empty, klinePic.Size));

            if (isDrawStandLine == true)
            {
                //PointF vP = new PointF(

                //纵向
                g.DrawLine(Pens.Black, new PointF(basePoinf.X, panel2.Height), new PointF(basePoinf.X, 0));
                g.DrawLine(Pens.Black, new PointF(0, basePoinf.Y), new PointF(panel2.Width, basePoinf.Y));

            }
        }


        private void DrawKlinePic(Graphics tempG)
        {

            klineG.Clear(Color.White);

            if (oneMinuteDraw.Prices.Count > 0)
            {
                double length = Math.Max(oneMinuteDraw.MaxPrice - min.data.last_close, Math.Abs(oneMinuteDraw.MinPrice - min.data.last_close));

                int le = panel2.Height / 2;

                double dis = length / le;

                double disW = panel2.Width / oneMinuteDraw.Prices.Count; //应该动态扩展

                klineG.DrawLine(Pens.Black, new Point(0, le), new Point(panel2.Width, le));

                List<PointF> points = new List<PointF>();
                List<PointF> ave_points = new List<PointF>();

                for (int i = 0; i < oneMinuteDraw.Prices.Count; i++)
                {
                    //上面
                    if (oneMinuteDraw.Prices[i] > min.data.last_close)
                    {
                        points.Add(new PointF((float)(i * disW), le - (float)((oneMinuteDraw.Prices[i] - min.data.last_close) / length) * le));
                    }
                    else
                        points.Add(new PointF((float)(i * disW), le + (float)Math.Abs((oneMinuteDraw.Prices[i] - min.data.last_close) / length) * le));

                    //下面
                    ave_points.Add(new PointF((float)(i * disW), le - (float)((oneMinuteDraw.Ave_prices[i] - min.data.last_close) / length) * le));

                    //Console.WriteLine(points[i].Y);
                }

                if (points.Count > 1)
                {
                    klineG.DrawLines(Pens.Green, points.ToArray());
                    klineG.DrawLines(Pens.Red, ave_points.ToArray());
                }


                List<PointF> keyPointsBuy = new List<PointF>(); //关键点位1

                List<PointF> keyPointsSel = new List<PointF>();

                if (oneMinuteDraw.KeyPointBuy.Count > 0)
                {

                    for (int i = 0; i < oneMinuteDraw.KeyPointBuy.Count; i++)
                    {
                        int index = oneMinuteDraw.KeyPointBuy[i];
                        RectangleF rect = new RectangleF(points[index].X - 5, points[index].Y - 5, 10, 10);
                        klineG.DrawEllipse(Pens.Red, rect);
                    }

                }

                if (oneMinuteDraw.KeyPointSel.Count > 0)
                {

                    for (int i = 0; i < oneMinuteDraw.KeyPointSel.Count; i++)
                    {
                        int index = oneMinuteDraw.KeyPointSel[i];
                        RectangleF rect = new RectangleF(points[index].X - 5, points[index].Y - 5, 10, 10);
                        klineG.DrawEllipse(Pens.Black, rect);
                    }

                }


                Font f = new Font("Arial", 12, FontStyle.Regular);

                klineG.DrawString(min.data.last_close.ToString(), f, new SolidBrush(Color.Black), new Point(0, le));

            }

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

            Graphics g = e.Graphics;

            //g.Clear(Color.Black);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            if (oneMinuteDraw.Amount.Count > 1)
            {
                 //距离顶底部的距离
                int baseLine = 20;

                //最大值
                double maxAmount = oneMinuteDraw.Amount.Max();

               
                double aveDisY = maxAmount / (panel3.Height - baseLine * 2);

                double aveDisX = panel3.Width / oneMinuteDraw.Amount.Count;

                List<PointF> pointFs = new List<PointF>();
                List<PointF> aveAmountPoints = new List<PointF>();
                for (int i = 0; i < oneMinuteDraw.Amount.Count; i++)
                {
                    pointFs.Add(new PointF((float)(i * aveDisX), panel3.Height - baseLine  -  (float)(oneMinuteDraw.Amount[i] / maxAmount * (panel3.Height - baseLine * 2)  )));

                    aveAmountPoints.Add(new PointF((float)(i * aveDisX), panel3.Height - baseLine - (float)(oneMinuteDraw.Ave_amout[i] / maxAmount * (panel3.Height - baseLine * 2))));

                    g.DrawLine(Pens.Red, new PointF(pointFs[i].X,  panel3.Height - baseLine), pointFs[i]);
                }


                //绘制基准线
                g.DrawLine(Pens.Black, new Point(0, panel3.Height - baseLine), new Point(panel3.Width, panel3.Height - baseLine));

                g.DrawLines(Pens.Green, pointFs.ToArray());


                //绘制均量

                g.DrawLines(Pens.Black, aveAmountPoints.ToArray());


                //分析画圈






            }
        }

        /// <summary>
        /// 加载基本数据
        /// </summary>
        private async void LoadBaseInfo()
        {
            //加载基本数据

            FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/stock_info.json", FileMode.Open, FileAccess.Read);

            StreamReader sr = new StreamReader(fileStream);

            string stocks = await sr.ReadToEndAsync();

            stockList = JsonConvert.DeserializeObject<Record>(stocks);


            sr.Close();
            fileStream.Close();
            fileStream.Dispose();

            for (int i = 0; i < stockList.stockInfos.Count; i++)
            {
                comboBox2.Items.Add(stockList.stockInfos[i].Name);
            }

            richTextBox1.AppendText("基本数据加载完成!\r\n");

        }

        private string GetStockOnlineInfo(string stockNum)
        {
            try
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;

                CookieContainer cc = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create($"https://stock.xueqiu.com/v5/stock/chart/minute.json?symbol={stockNum}&period=1d"); //300364    //300087
                request.Method = "Get";
                request.ContentType = "*/*";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                request.Headers.Add("cookie", @"device_id = 2d659df136d37948fe2cf5e7aa6a3120; s = dl1axjiys0; bid = aa9301ecf553300f6d34c465195c5698_l4cm843q; Hm_lvt_1db88642e346389874251b5a1eded6e3 = 1654688963,1654773558,1655117485,1655347102; xq_a_token = 71618bed86fb8c37819bc7e19fe00d51ec2386f8; xqat = 71618bed86fb8c37819bc7e19fe00d51ec2386f8; xq_r_token = 88812666b17c6dc5f6bdbf432fda4b49b23f7924; xq_id_token = eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJ1aWQiOi0xLCJpc3MiOiJ1YyIsImV4cCI6MTY1NzkyNzc0MywiY3RtIjoxNjU1MzYwMjI1ODQwLCJjaWQiOiJkOWQwbjRBWnVwIn0.qlvEGHIY9wFR70K9Fu_h2C5YxwPSJOAXzHCLft3bXqgW_sRVtNmzvyYvZ_OqIQluCYRiXAQya6JTlTaLAcT - dDapKL1U2 - FCfftzcn4bXP5VVFEXlJJganQWIfzTopsD80SdlZBeXM6lxosZA6H8NbxFLgSyt2Hw_jiBezZReS7qrzarC4GAugZ4TM2GQLrI_EsQlfOaYusxIG3QUA5FjJFThjMCjJTziboKquc3CVfAVs8G7oAeT - Df46KJi9ZunJ7jvWsMC7IET0E2otR9rlYSnxYKA2K2XHFk9RoTKOgFqpMwJtR9XjVQececsk32Tt1IZ18xBAHLKPdMO49Wag; u = 351655360279548; Hm_lpvt_1db88642e346389874251b5a1eded6e3 = 1655361549");


                CookieCollection co = new CookieCollection();
                co.Add(new Cookie("xq_a_token", "83886f7ef4add65155e8ef54dfc3e739afa7472a", "", "stock.xueqiu.com"));
                //co.Add(new Cookie("xqat", "83886f7ef4add65155e8ef54dfc3e739afa7472a"));
                cc.Add(co);

                request.AllowAutoRedirect = false;
              //  request.CookieContainer = cc;
                request.KeepAlive = true;
                request.Timeout = 10000;//设置HttpWebRequest获取响应的超时时间为10000毫秒，即10秒


                Stream ss = request.GetResponse().GetResponseStream();
                StreamReader streamReader = new StreamReader(ss, Encoding.UTF8);
                string html = streamReader.ReadToEnd();

                ss.Close();
                streamReader.Close();

                return html;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void CalcPrice(string data)
        {
            try
            {
                min = JsonConvert.DeserializeObject<OneMinute>(data);

                if (min.data.items.Count > 0)
                {
                    oneMinuteDraw.Prices.Clear();
                    oneMinuteDraw.Ave_prices.Clear();
                    oneMinuteDraw.Amount.Clear();

                    double totalAmount = 0;

                    oneMinuteDraw.Ave_amout.Clear();


                    bool isKeyWave = false;


                    double firstPrice = 0;

                    for (int i = 0; i < min.data.items.Count; i++)
                    {
                        oneMinuteDraw.Prices.Add(min.data.items[i].current);
                        oneMinuteDraw.Ave_prices.Add(min.data.items[i].avg_price);
                        oneMinuteDraw.Amount.Add(min.data.items[i].amount);

                        totalAmount += min.data.items[i].amount;


                        oneMinuteDraw.Ave_amout.Add(totalAmount / oneMinuteDraw.Amount.Count);



                        if (i > 15) //忽略前15分钟
                        {
                            //必要条件
                            if (oneMinuteDraw.Amount[i] > oneMinuteDraw.Ave_amout[i])
                            {

                                if (isKeyWave == false) //首次进入做标记
                                {
                                    isKeyWave = true;

                                    firstPrice = oneMinuteDraw.Prices[i];

                                }
                                else
                                {

                                    if (isKeyWave == true)
                                    {

                                        if (oneMinuteDraw.Prices[i] < firstPrice) //下跌
                                        {
                                            if (oneMinuteDraw.Amount[i] > oneMinuteDraw.Amount[i - 1] && oneMinuteDraw.Prices[i] > oneMinuteDraw.Prices[i - 1]) //量增大
                                            {

                                                oneMinuteDraw.KeyPointBuy.Add(i);
                                            }
                                            else
                                            {

                                            }
                                        }
                                        else
                                        {
                                            if (oneMinuteDraw.Amount[i] > oneMinuteDraw.Amount[i - 1] && oneMinuteDraw.Prices[i] < oneMinuteDraw.Prices[i - 1])
                                            {
                                                oneMinuteDraw.KeyPointSel.Add(i);
                                            }
                                        }
                                    }

                                }
                            }
                            else //回落则恢复
                            {
                                if (isKeyWave == true)
                                    isKeyWave = false;
                            }

                        }

                        //均量


                        oneMinuteDraw.MaxPrice = oneMinuteDraw.Prices.Max();
                        oneMinuteDraw.MinPrice = oneMinuteDraw.Prices.Min();


                    }

                    DrawKlinePic(klineG);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox2.Text;

            if (str.Contains("退市"))
                return; 

            StockInfo info =  stockList.stockInfos.Find(x => x.Name == str);

            if (info != null)
            {
                string stockN = info.Exchange.ToUpper() + info.Code;

                stockNum = stockN;
              
                string html = GetStockOnlineInfo(stockN);
                CalcPrice(html);

                

                panel2.Invalidate();
                panel3.Invalidate();
                
            }

        }

        //
        private void btn_pankou_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;

                CookieContainer cc = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create($"https://stock.xueqiu.com/v5/stock/realtime/pankou.json?symbol={stockNum}"); //300364    //300087
                request.Method = "Get";
                request.ContentType = "*/*";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate; 


                CookieCollection co = new CookieCollection();
                co.Add(new Cookie("xq_a_token", "83886f7ef4add65155e8ef54dfc3e739afa7472a", "", "stock.xueqiu.com"));
                //co.Add(new Cookie("xqat", "83886f7ef4add65155e8ef54dfc3e739afa7472a"));
                cc.Add(co);

                request.AllowAutoRedirect = false;
                request.CookieContainer = cc;
                request.KeepAlive = true;
                request.Timeout = 10000;//设置HttpWebRequest获取响应的超时时间为10000毫秒，即10秒


                Stream ss = request.GetResponse().GetResponseStream();
                StreamReader streamReader = new StreamReader(ss, Encoding.UTF8);
                string html = streamReader.ReadToEnd();

                ss.Close();
                streamReader.Close();

               // return html;
               //richTextBox1.AppendText(html);
                
                //5档盘口
                Pankou pankou  = JsonConvert.DeserializeObject<Pankou>(html);

                 DateTime dt = Common.GetDateTime(pankou.Data.timestamp);

                long sss = Common.GetTimeStamp(DateTime.Now);

                SSpankouList.Clear();


                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Bp1, pankou.Data.Bc1));
                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Bp2, pankou.Data.Bc2));
                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Bp3, pankou.Data.Bc3));
                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Bp4, pankou.Data.Bc4));
                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Bp5, pankou.Data.Bc5));

                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Sp1, pankou.Data.Sc1));
                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Sp1, pankou.Data.Sc1));
                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Sp1, pankou.Data.Sc1));
                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Sp1, pankou.Data.Sc1));
                SSpankouList.Add(new Tuple<float?, int>(pankou.Data.Sp1, pankou.Data.Sc1));

                panel7.Invalidate();
            }
            catch (Exception ex)
            {
                richTextBox1.AppendText(ex.Message);

            }

        }

        private void btn_trade_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;

                CookieContainer cc = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create($"https://stock.xueqiu.com/v5/stock/history/trade.json?symbol={stockNum}&count=10"); //300364    //300087
                request.Method = "Get";
                request.ContentType = "*/*";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;


                CookieCollection co = new CookieCollection();
                co.Add(new Cookie("xq_a_token", "83886f7ef4add65155e8ef54dfc3e739afa7472a", "", "stock.xueqiu.com"));
                //co.Add(new Cookie("xqat", "83886f7ef4add65155e8ef54dfc3e739afa7472a"));
                cc.Add(co);

                request.AllowAutoRedirect = false;
                request.CookieContainer = cc;
                request.KeepAlive = true;
                request.Timeout = 10000;//设置HttpWebRequest获取响应的超时时间为10000毫秒，即10秒


                Stream ss = request.GetResponse().GetResponseStream();
                StreamReader streamReader = new StreamReader(ss, Encoding.UTF8);
                string html = streamReader.ReadToEnd();

                ss.Close();
                streamReader.Close();

                // return html;
                //richTextBox1.AppendText(html);



                Trade trade = JsonConvert.DeserializeObject<Trade>(html);

                MMtrade.Clear();
                for (int i = 0; i < trade.Data.Items.Count(); i++)
                {
                    string time = Common.GetDateTime(trade.Data.Items[i].Timestamp).Hour + ":" + Common.GetDateTime(trade.Data.Items[i].Timestamp).Minute;
                    Tuple<string, float, int> item = new Tuple<string, float, int>(time, trade.Data.Items[i].Current, trade.Data.Items[i].Trade_volume / 100);

                    MMtrade.Add(item);
                }

                panel6.Invalidate();
            }
            catch (Exception ex)
            {
                //  return "";
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stockNum"></param>
        /// <returns></returns>
        private string QuotecInfo(string stockNum)
        {
            try
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;

                CookieContainer cc = new CookieContainer();

                long timeSpan = Common.GetTimeStamp(DateTime.Now);

                request = (HttpWebRequest)WebRequest.Create($"https://stock.xueqiu.com/v5/stock/realtime/quotec.json?symbol=SZ300364&_={timeSpan}"); //300364    //300087
                request.Method = "Get";
                request.ContentType = "*/*";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;


                CookieCollection co = new CookieCollection();
                co.Add(new Cookie("xq_a_token", "83886f7ef4add65155e8ef54dfc3e739afa7472a", "", "stock.xueqiu.com"));
                //co.Add(new Cookie("xqat", "83886f7ef4add65155e8ef54dfc3e739afa7472a"));
                cc.Add(co);

                request.AllowAutoRedirect = false;
                request.CookieContainer = cc;
                request.KeepAlive = true;
                request.Timeout = 10000;//设置HttpWebRequest获取响应的超时时间为10000毫秒，即10秒


                Stream ss = request.GetResponse().GetResponseStream();
                StreamReader streamReader = new StreamReader(ss, Encoding.UTF8);
                string html = streamReader.ReadToEnd();

                ss.Close();
                streamReader.Close();

                //保留数据
                Log.Information(html);

                return html;



            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void btn_Quotec_Click(object sender, EventArgs e)
        {
            string str =   QuotecInfo(stockNum);

            richTextBox1.AppendText(Common.ConvertJsonString(str));
        }


        /// <summary>
        /// 绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel7_Paint(object sender, PaintEventArgs e)
        {

            if (SSpankouList.Count > 0)
            {
                //上面

                Graphics graphics = e.Graphics;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

                int baseHeiht = panel7.Height / 2;

                Font f = new Font("微软雅黑", 12, FontStyle.Regular);

                graphics.DrawLine(Pens.Black, new Point(0, baseHeiht), new Point(panel7.Width, baseHeiht));

                for (int i = 0; i < 5; i++)
                {

                    string tempStr = "mm" + (i + 1) + "   " + SSpankouList[i].Item1 + "   " + SSpankouList[i].Item2 / 100;

                    SizeF s = graphics.MeasureString(tempStr, f);

                    PointF drawP = new PointF( 0,  baseHeiht - i * s.Height  - (s.Height + 2));


                    graphics.DrawString(tempStr, f, new SolidBrush(Color.Green), drawP);

                }

                for (int i = 0; i < 5; i++)
                {
                    string tempStr = "mm" + (i + 1) + "   " + SSpankouList[i + 5].Item1 + "   " + SSpankouList[i + 5].Item2 / 100;

                    SizeF s = graphics.MeasureString(tempStr, f);

                    PointF drawP = new PointF(0, baseHeiht + i * s.Height + (s.Height + 2));


                    graphics.DrawString(tempStr, f, new SolidBrush(Color.Green), drawP);
                }


            }



        }

        //绘制
        private void panel6_Paint(object sender, PaintEventArgs e)
        {
            if (MMtrade.Count > 0)
            {
                Graphics graphics = e.Graphics;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

                Font f = new Font("微软雅黑", 12, FontStyle.Regular);

                graphics.DrawLine(Pens.Black, new Point(0, 2), new Point(panel6.Width, 2));

                for (int i = 0; i < MMtrade.Count; i++)
                {
                    string tempStr = MMtrade[i].Item1 + "   " + MMtrade[i].Item2 + "   " + MMtrade[i].Item3 / 100;

                    SizeF s = graphics.MeasureString(tempStr, f);

                    PointF drawP = new PointF(0,  i * s.Height  + 2);


                    graphics.DrawString(tempStr, f, new SolidBrush(Color.Green), drawP);
                }

            }


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        PointF basePoinf = new PointF();

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawStandLine == true)
            {
                basePoinf = new PointF(e.X, e.Y);
                panel2.Invalidate();
            }
        }

        bool isDrawStandLine = false;
        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            isDrawStandLine = !isDrawStandLine;
            panel2.Invalidate(true);
        }

        private void btn_Analysis_Click(object sender, EventArgs e)
        {
            TargetAnalysis targetAnalysis = new TargetAnalysis();
            targetAnalysis.StartPosition = FormStartPosition.CenterScreen;

            targetAnalysis.WindowState = FormWindowState.Maximized;
            targetAnalysis.ShowDialog();

        }

        private void btn_dealAnalysis_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string dataFile = openFileDialog.FileName;
                string cardName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);

                DataTable dt = new DataTable();

                List<Quote> quoteList = new List<Quote>();

                dt = Common.ExcelToDataTable(dataFile, cardName);

                if (dt == null)
                    return;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Quote s = new Quote();

                    s.Volume = Decimal.Parse(dt.Rows[i]["volume"].ToString());
                    s.Open = Decimal.Parse(dt.Rows[i]["open"].ToString());
                    s.Close = Decimal.Parse(dt.Rows[i]["close"].ToString());
                    s.High = Decimal.Parse(dt.Rows[i]["high"].ToString());
                    s.Low = Decimal.Parse(dt.Rows[i]["low"].ToString());
                    //s.Date = (DateTime)dt.Rows[i]["date"];
                    s.Date =
                        DateTime.ParseExact(dt.Rows[i]["time"].ToString(), "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.CurrentCulture);
                    quoteList.Add(s);
                }

                //IEnumerable<Quote> quotes = quoteList;
                //IEnumerable<SmaResult> results = quotes.GetSma(20);

                Strategic_one strategic_One = new Strategic_one();
                DealInfo dealResult = strategic_One.Analysis(quoteList);

                MessageBox.Show("交易结果分析  \r\n交易次数：" + dealResult.DealCount + "\r\n交易收益:" + dealResult.RateOfDeal.Max().ToString("0.##") + "%" + "\r\n总金额:" + dealResult.InitMoney);

                
            }
        }
    }
}
