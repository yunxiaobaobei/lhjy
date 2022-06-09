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


namespace StockTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        OneMinuteDraw oneMinuteDraw = new OneMinuteDraw();

        private void Form1_Load(object sender, EventArgs e)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            CookieContainer cc = new CookieContainer();
            request = (HttpWebRequest)WebRequest.Create("https://stock.xueqiu.com/v5/stock/chart/minute.json?symbol=SZ300087&period=1d");
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


            OneMinute min = JsonConvert.DeserializeObject<OneMinute>(html);

            richTextBox1.AppendText(html);

           
            oneMinuteDraw.Prices = new List<double>();

            for (int i = 0; i < min.data.items.Count; i++)
            {

                oneMinuteDraw.Prices.Add(min.data.items[i].current);
            }


            oneMinuteDraw.MaxPrice = oneMinuteDraw.Prices.Max();
            oneMinuteDraw.MinPrice = oneMinuteDraw.Prices.Min();

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            if (oneMinuteDraw.Prices.Count > 0)
            {
                double length = Math.Max(oneMinuteDraw.MaxPrice, Math.Abs(oneMinuteDraw.MinPrice));

                int le = panel2.Height / 2;


                double dis = length / le;

                double disW = panel2.Width / oneMinuteDraw.Prices.Count;

                g.DrawLine(Pens.White, new Point(0, le), new Point(panel2.Width, le));

                List<PointF> points = new List<PointF>();


                for (int i = 0; i < oneMinuteDraw.Prices.Count; i++)
                {
                    points.Add(new PointF( (float)(i * disW), (float)oneMinuteDraw.Prices[i]));
                }

                if(points.Count > 1)
                    g.DrawLines(Pens.Red, points.ToArray());


            }

        }
    }
}
