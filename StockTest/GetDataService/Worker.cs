using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using Serilog;
using System.IO;


namespace GetDataService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            SaveBasePath = AppDomain.CurrentDomain.BaseDirectory + "\\stockQuotecData\\";
        }


        List<string> stockList = new List<string>();

        List< Tuple<string, FileStream, StreamWriter>> stockFileDel = new List<Tuple<string, FileStream, StreamWriter>>();

        //文件保存的基本路径
        string SaveBasePath = "";

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            stockList.Add("SZ300364");

            Program.Log.Information($"基本路径:{SaveBasePath}");

            if(!Directory.Exists(SaveBasePath))
                Directory.CreateDirectory(SaveBasePath);

            string todayFilePath = Path.Combine(SaveBasePath, DateTime.Now.ToString("yyy-MM-dd"));
            if(!Directory.Exists(todayFilePath))
                Directory.CreateDirectory(todayFilePath);

            for (int i = 0; i < stockList.Count; i++)
            {
                string stockFileName = todayFilePath + "\\" +stockList[i] + ".txt";
                Program.Log.Information($"{stockList[i]}数据将保存在一下文件中:{stockFileName}");

                FileStream fs = new FileStream(stockFileName, FileMode.OpenOrCreate, FileAccess.Write);
                StreamWriter streamWriter = new StreamWriter(fs);

               // await streamWriter.WriteAsync("sssss");
                
                stockFileDel.Add(new Tuple<string, FileStream, StreamWriter>(stockList[i], fs, streamWriter));
            }

            Program.Log.Information("");

            while (!stoppingToken.IsCancellationRequested)
            {

                DateTime startTime_AM  = new DateTime(2022, DateTime.Now.Month, DateTime.Now.Day, 9, 30, 0);
                DateTime endTime_AM = new DateTime(2022, DateTime.Now.Month, DateTime.Now.Day, 11, 30, 0);

                DateTime startTime_PM = new DateTime(2022, DateTime.Now.Month, DateTime.Now.Day, 13, 0, 0);
                DateTime endTime_PM = new DateTime(2022, DateTime.Now.Month, DateTime.Now.Day, 15, 0, 0);


                //Program.Log.Information(startTime_AM.ToString("F"));
                //Program.Log.Information(endTime_AM.ToString("F"));
                //Program.Log.Information(startTime_PM.ToString("F"));
                //Program.Log.Information(endTime_PM.ToString("F"));


                if ((DateTime.Now >= startTime_AM && DateTime.Now <= endTime_AM) ||
                    (DateTime.Now >= startTime_PM && DateTime.Now <= endTime_PM))
                {
                    //Program.Log.Information("上午时段");

                    for (int i = 0; i < stockFileDel.Count; i++)
                    {

                        string data = QuotecInfo(stockFileDel[i].Item1) + "\r\n";

                        if (data != "")
                        {
                            //写入文件
                            await stockFileDel[i].Item3.WriteAsync(data);
                            stockFileDel[i].Item3.Flush();
                        }
                        else
                        {

                        }


                    }
                }

                await Task.Delay(2000, stoppingToken);

                //if (DateTime.Now >= startTime_PM && DateTime.Now <= endTime_PM)
                //{
                //    Program.Log.Information("下午时段");
                //}



                //if (DateTime.Now.Hour > 9 && DateTime.Now.Hour < 11)
                //{
                //    for (int i = 0; i < stockList.Count; i++)
                //    {
                //        QuotecInfo(stockList[i]);
                //    }

                //    //延迟3s
                //    await Task.Delay(3000, stoppingToken);
                //}
                //else
                //{
                //    //延迟20s
                //    await Task.Delay(20000, stoppingToken);
                //}
            }

        }



        private string QuotecInfo(string stockNum)
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

                Program.Log.Information(html);

                return html;



            }
            catch (Exception ex)
            {
                Program.Log.Error(ex.Message);
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

    }
}
