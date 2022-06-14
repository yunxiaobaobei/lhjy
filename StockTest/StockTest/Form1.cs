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


namespace StockTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        OneMinuteDraw oneMinuteDraw = new OneMinuteDraw();

        OneMinute min = null;

        Record stockList = null;


        private List<Tuple<float?, int>> SSpankouList = new List<Tuple<float?, int>>();

        private List<Tuple<string, float, int>> MMtrade = new List<Tuple<string, float, int>>();

        private void Form1_Load(object sender, EventArgs e)
        {
            oneMinuteDraw.Prices = new List<double>();
            oneMinuteDraw.Ave_prices = new List<double>();
            oneMinuteDraw.Amount = new List<double>();

            //ThreadPool.QueueUserWorkItem( (o) =>  LoadBaseInfo(), null);
            LoadBaseInfo();


            string html =  GetStockOnlineInfo("SZ300087");

            CalcPrice(html);




        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(Color.White);

            if (oneMinuteDraw.Prices.Count > 0)
            {
                double length =  Math.Max(oneMinuteDraw.MaxPrice - min.data.last_close, Math.Abs(oneMinuteDraw.MinPrice - min.data.last_close));

                int le = panel2.Height / 2;

                double dis = length / le;

                double disW = panel2.Width /  oneMinuteDraw.Prices.Count; //应该动态扩展

                g.DrawLine(Pens.Black, new Point(0, le), new Point(panel2.Width, le));

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
                    g.DrawLines(Pens.Green, points.ToArray());
                    g.DrawLines(Pens.Red, ave_points.ToArray());
                }

                Font f = new Font("Arial", 12, FontStyle.Regular);

                g.DrawString(min.data.last_close.ToString(), f, new SolidBrush(Color.Black), new Point(0, le));

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

                for (int i = 0; i < oneMinuteDraw.Amount.Count; i++)
                {
                    pointFs.Add(new PointF((float)(i * aveDisX), panel3.Height - baseLine  -  (float)(oneMinuteDraw.Amount[i] / maxAmount * (panel3.Height - baseLine * 2)  )));
                    g.DrawLine(Pens.Red, new PointF(pointFs[i].X,  panel3.Height - baseLine), pointFs[i]);
                }


                //绘制基准线
                g.DrawLine(Pens.Black, new Point(0, panel3.Height - baseLine), new Point(panel3.Width, panel3.Height - baseLine));

                g.DrawLines(Pens.Green, pointFs.ToArray());
                


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

                return html;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void CalcPrice(string data)
        {
            min = JsonConvert.DeserializeObject<OneMinute>(data);

            if (min.data.items.Count > 0)
            {
                oneMinuteDraw.Prices.Clear();
                oneMinuteDraw.Ave_prices.Clear();
                oneMinuteDraw.Amount.Clear();

                for (int i = 0; i < min.data.items.Count; i++)
                {
                    oneMinuteDraw.Prices.Add(min.data.items[i].current);
                    oneMinuteDraw.Ave_prices.Add(min.data.items[i].avg_price);
                    oneMinuteDraw.Amount.Add(min.data.items[i].amount);
                }

                oneMinuteDraw.MaxPrice = oneMinuteDraw.Prices.Max();
                oneMinuteDraw.MinPrice = oneMinuteDraw.Prices.Min();
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
                request = (HttpWebRequest)WebRequest.Create($"https://stock.xueqiu.com/v5/stock/realtime/pankou.json?symbol=SZ300261"); //300364    //300087
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
               richTextBox1.AppendText(html);
                
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
              //  return "";
            }

        }

        private void btn_trade_Click(object sender, EventArgs e)
        {
            try
            {
                HttpWebRequest request = null;
                HttpWebResponse response = null;

                CookieContainer cc = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create($"https://stock.xueqiu.com/v5/stock/history/trade.json?symbol=SZ300261&count=10"); //300364    //300087
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
                richTextBox1.AppendText(html);



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

                request = (HttpWebRequest)WebRequest.Create($"https://stock.xueqiu.com/v5/stock/realtime/quotec.json?symbol=SZ300261&_={timeSpan}"); //300364    //300087
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

                return html;

            }
            catch (Exception ex)
            {
                return "";
            }
        }

        private void btn_Quotec_Click(object sender, EventArgs e)
        {
          string str =   QuotecInfo("ssss");

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
    }
}
