using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using Serilog;
using AutoDeal.model.sina;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Data;
using Skender.Stock.Indicators;


namespace AutoDeal
{

    /// <summary>
    /// 获取实时数据
    /// </summary>
    public class GetRealTimeData
    {

        //获取股票池中实时数据

        public List<string> stockList = new List<string>();


        public static event Action<List<Minute>, string> GetMinuteDataEvent;

        public void StartGetRealDate()
        {
            Log.Information("开始获取新浪数据");
            //立即执行，间隔5秒

            ThreadPool.QueueUserWorkItem((o) =>
           {
               try
               {
                   while (true)
                   {

                       DateTime startTime_AM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 9, 26, 0);
                       DateTime endTime_AM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 31, 0);

                       DateTime startTime_PM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 59, 0);
                       DateTime endTime_PM = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 15, 1, 0);

                       if ((DateTime.Now >= startTime_AM && DateTime.Now <= endTime_AM) ||
                        (DateTime.Now >= startTime_PM && DateTime.Now <= endTime_PM))
                       {

                           for (int i = 0; i < stockList.Count; i++)
                           {
                               ThreadPool.QueueUserWorkItem((x) =>
                               {
                                   //获取新浪数据
                                   string msg = SinaGetFiveminute(stockList[(int)x], 100);// XiuQiuQuotecInfo(stockList[i]);
                                   Log.Information("获取数据:" + stockList[(int)x] + "   " + msg);
                                   //传递到后台，写入缓存
                                   List<Minute> sina = JsonConvert.DeserializeObject<List<Minute>>(msg);

                                   if (sina.Count > 0)
                                   {
                                       GetMinuteDataEvent?.Invoke(sina, stockList[(int)x].Substring(2));
                                   }
                               }, i);
                           }

                       }

                       Thread.Sleep(10000); //每10秒获取一次数据
                   }
               }
               catch (Exception ex)
               {

                   Log.Error("获取实时数据线程退出!" + ex.Message);
               }

           }, null);


        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="dataCount">数据量</param>
        /// <returns></returns>
        private string SinaGetFiveminute(string code, int dataCount)
        {
            string html = "";

            try
            {
                HttpWebRequest request = null;

                long timeSpan = GetTimeStamp(DateTime.Now);

                string var = $"_{code}_5_{timeSpan}";

                //修改 scale 的值可以获取不同级别的数据 scale=15 ---- 15 分钟 scale=240 ---日K   datalen 获取的节点数
                request = (HttpWebRequest)WebRequest.Create($"https://quotes.sina.cn/cn/api/jsonp_v2.php/var {var}=/CN_MarketDataService.getKLineData?symbol={code}&scale=5&ma=no&datalen={dataCount}"); //300364    //300087
                request.Method = "Get";
                request.ContentType = "*/*";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                request.AllowAutoRedirect = false;
                request.KeepAlive = true;
                request.Timeout = 10000;//设置HttpWebRequest获取响应的超时时间为10000毫秒，即10秒


                Stream ss = request.GetResponse().GetResponseStream();
                StreamReader streamReader = new StreamReader(ss, Encoding.UTF8);
                html = streamReader.ReadToEnd();


                int index = html.IndexOf(var);

                html = html.Substring(index + var.Length + 2);
                html = html.Substring(0, html.Length - 2);
                html.Replace(")", "");
                html.Replace("(", "");

                ss.Close();
                streamReader.Close();

            }
            catch (Exception ex)
            {
                Log.Error("获取新浪数据失败!" + ex.Message);
            }

            return html;
        }


        private string XiuQiuQuotecInfo(string stockNum)
        {
            try
            {
                HttpWebRequest request = null;
                //HttpWebResponse response = null;

                CookieContainer cc = new CookieContainer();

                long timeSpan = GetTimeStamp(DateTime.Now);

                request = (HttpWebRequest)WebRequest.Create($"https://stock.xueqiu.com/v5/stock/realtime/quotec.json?symbol={stockNum}&_={timeSpan}"); //300364    //300087
                request.Method = "Get";
                request.ContentType = "*/*";
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                // request.Headers.Add("cookie", "");  //备用方法

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

                Log.Information(html);

                return html;



            }
            catch (Exception ex)
            {
                Log.Error("抓取数据失败!" + ex.Message);
                return "";
            }
        }


        /// <summary>
        /// 返回指定时间的时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            long timestamp = Convert.ToInt32((dt - dateStart).TotalSeconds);

            return timestamp;
        }



        public void TestStartGetRealDate()
        {
            Log.Information("开始发送测试数据");
          
            List<Minute> quoteList = new List<Minute>();

            string dataFile = "";
            string cardName = "";

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                dataFile = openFileDialog.FileName;
                cardName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
            }
            DataTable dt = Common.ExcelToDataTable(dataFile, cardName);

            if (dt == null)
            {
                Log.Warning("未读取到任何测试数据！");
                return;
            }

            ThreadPool.QueueUserWorkItem((o) =>
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Minute s = new Minute();

                    s.Volume = Decimal.Parse(dt.Rows[i]["volume"].ToString());
                    s.Open = Decimal.Parse(dt.Rows[i]["open"].ToString());
                    s.Close = Decimal.Parse(dt.Rows[i]["close"].ToString());
                    s.High = Decimal.Parse(dt.Rows[i]["high"].ToString());
                    s.Low = Decimal.Parse(dt.Rows[i]["low"].ToString());
                    s.Date = DateTime.ParseExact(dt.Rows[i]["time"].ToString(), "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.CurrentCulture);
                    quoteList.Add(s);
                }

                Log.Information("测试数据发送完成！");
                
                List<Minute> testQuotes = new List<Minute>();

                for (int i = 0; i < 60; i++)
                {
                    testQuotes.Add(quoteList[i]);
                }

                for (int i = 60; i < quoteList.Count; i++)
                {

                    testQuotes.Add((Minute)quoteList[i]);

                    GetMinuteDataEvent?.Invoke(testQuotes, "300364");

                    Log.Information($"第{i}次发送测试数据,本次数据量:{testQuotes.Count}");
                    Thread.Sleep(50);

                }

                Log.Error("测试数据发送完成!");

            }, null);


        }

    }
}
