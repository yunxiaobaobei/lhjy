using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Net;
using Serilog;

namespace AutoDeal
{

    /// <summary>
    /// 获取实时数据
    /// </summary>
    public class GetRealTimeData
    {

        //获取股票池中实时数据

        List<string>  stockList = new List<string>();



        public void StartGetRealDate()
        {

            //立即执行，间隔5秒
            Timer realDataThread = new Timer((o) => {



                for (int i = 0; i < stockList.Count; i++)
                {

                    QuotecInfo(stockList[i]);

                    //传递到后台，写入缓存




                }
            
            
            
            }, null, 0, 5000);


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


    }
}
