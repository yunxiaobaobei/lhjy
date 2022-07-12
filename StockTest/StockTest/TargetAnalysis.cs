using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Skender.Stock.Indicators;
using System.Data;
using System.IO;
using System.Net;
using Serilog;
using Newtonsoft.Json;

namespace StockTest
{
    public partial class TargetAnalysis : Form
    {
        public TargetAnalysis()
        {
            InitializeComponent();
        }

      
        DataTable dt = new DataTable();

        List<Quote> quoteList = new List<Quote>();

        ColorConfig colorConfig = new ColorConfig();

        //颜色修改事件
        public static event Action<ColorConfig> ChangeColorConfigEvent;

        private void TargetAnalysis_Load(object sender, EventArgs e)
        {

            UserControl1.ShowKinfoEvent += ShowKLineInfo;

            //初始化配色方案
            colorConfig.BuyInColor = pic_buy.BackColor;
            colorConfig.SellOutColor =pic_sell.BackColor;
            colorConfig.HeigVolumnColor = pic_higeVolumn.BackColor;
            colorConfig.LowVolumnColor = pic_lowVolumn.BackColor;
            colorConfig.EnterPointColor = pic_abelIn.BackColor;
            colorConfig.ExitPointColor = pic_ableOut.BackColor;
            colorConfig.AbleBuy = check_buy.Checked;
            colorConfig.AbleSell = check_sell.Checked;
            colorConfig.AbleHeigVolumn = check_higeVolumn.Checked;
            colorConfig.AbleLowVolumn = check_lowVolumn.Checked;
            colorConfig.AbleEnterPoint = check_in.Checked;
            colorConfig.AbleExitPoint = check_out.Checked;




        }


        private void btn_getHistory_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string dataFile = openFileDialog.FileName;
                string cardName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);


                dt = Common.ExcelToDataTable(dataFile, cardName);

                if (dt == null)
                    return;

                quoteList.Clear();

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

                //Strategic_one strategic_One = new Strategic_one();
                //DealInfo dealResult= strategic_One.Analysis(quoteList);

                //MessageBox.Show("交易结果分析  \r\n交易次数：" + dealResult.DealCount + "\r\n交易收益:" + dealResult.RateOfDeal.Max().ToString("0.##") + "%"  + "\r\n总金额:" + dealResult.InitMoney) ;

                panel_Kline.Controls.Clear();

                UserControl1 klineControl = new UserControl1(quoteList, colorConfig);
                klineControl.Dock = DockStyle.Fill;
                panel_Kline.Controls.Add(klineControl);
                //klineControl.ShowDialog();
            }
        }

        private void btn_loadQHdata_Click(object sender, EventArgs e)
        {

            string res = SinaGet_QH_OneMinute("", 0);

            Log.Information(res);

            List<QHquote> queues = JsonConvert.DeserializeObject<List<QHquote>>(res);

            quoteList.Clear();

            for (int i = 0; i < queues.Count; i++)
            {
                Quote s = new Quote();

                s.Volume = queues[i].Volume;
                s.Open = queues[i].Open;
                s.Close = queues[i].Close;
                s.High = queues[i].High;
                s.Low = queues[i].Low;

                s.Date = queues[i].Date;
                    
                quoteList.Add(s);
            }

            panel_Kline.Controls.Clear();

            UserControl1 klineControl = new UserControl1(quoteList, colorConfig);
            klineControl.Dock = DockStyle.Fill;
            panel_Kline.Controls.Add(klineControl);

        }



        private void panel_Kline_Paint(object sender, PaintEventArgs e)
        {

            Graphics graphics = e.Graphics;
            graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            //最大绘制节点数
            int maxCount = panel_Kline.Width / 5; 

        }





        private void panel_volumn_Paint(object sender, PaintEventArgs e)
        {

        }


        private void ShowKLineInfo(Quote info, int index)
        {
            label4.Text = info.Open.ToString();
            label5.Text = info.Close.ToString();
            label6.Text = index.ToString();
            label8.Text = info.Date.ToString();
            label10.Text = info.High.ToString();
            label12.Text = info.Low.ToString();
            
            //涨幅
            label14.Text = (((info.Close - info.Open)  / (info.Open  * 0.1m)) * 10).ToString("0.##") + "%";
            label16.Text =  Math.Abs(((info.High - info.Low)  / (info.Low * 0.1m)) * 10).ToString("0.##") + "%";
            panel4.Invalidate();
        }


        private void check_buy_Click(object sender, EventArgs e)
        {
            colorConfig.AbleBuy = ((CheckBox)sender).Checked;
            ChangeColorConfigEvent?.Invoke(colorConfig);
        }

        private void check_sell_Click(object sender, EventArgs e)
        {
            colorConfig.AbleSell = ((CheckBox)sender).Checked;
            ChangeColorConfigEvent?.Invoke(colorConfig);

        }

        private void check_in_Click(object sender, EventArgs e)
        {
            colorConfig.AbleEnterPoint = ((CheckBox)sender).Checked;
            ChangeColorConfigEvent?.Invoke(colorConfig);

        }

        private void check_out_Click(object sender, EventArgs e)
        {
            colorConfig.AbleExitPoint = ((CheckBox)sender).Checked;
            ChangeColorConfigEvent?.Invoke(colorConfig);

        }

        private void check_higeVolumn_Click(object sender, EventArgs e)
        {
            colorConfig.AbleHeigVolumn = ((CheckBox)sender).Checked;
            ChangeColorConfigEvent?.Invoke(colorConfig);

        }

        private void check_lowVolumn_Click(object sender, EventArgs e)
        {
            colorConfig.AbleLowVolumn = ((CheckBox)sender).Checked;
            ChangeColorConfigEvent?.Invoke(colorConfig);

        }

        private void pic_buy_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorConfig.BuyInColor = pic_buy.BackColor = colorDialog1.Color;
                ChangeColorConfigEvent?.Invoke(colorConfig);

            }
        }

        private void pic_sell_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorConfig.SellOutColor = pic_sell.BackColor = colorDialog1.Color;
                ChangeColorConfigEvent?.Invoke(colorConfig);

            }
        }

        private void pic_abelIn_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorConfig.EnterPointColor = pic_abelIn.BackColor = colorDialog1.Color;
                ChangeColorConfigEvent?.Invoke(colorConfig);

            }
        }

        private void pic_ableOut_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorConfig.ExitPointColor = pic_ableOut.BackColor = colorDialog1.Color;
                ChangeColorConfigEvent?.Invoke(colorConfig);

            }
        }

        private void pic_higeVolumn_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorConfig.HeigVolumnColor = pic_higeVolumn.BackColor = colorDialog1.Color;
                ChangeColorConfigEvent?.Invoke(colorConfig);

            }
        }

        private void pic_lowVolumn_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorConfig.LowVolumnColor = pic_lowVolumn.BackColor = colorDialog1.Color;
                ChangeColorConfigEvent?.Invoke(colorConfig);

            }
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {
            //绘制边框
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;


            Rectangle broder = new Rectangle(1, 0, panel4.Width - 2, panel4.Height - 1);

            g.DrawRectangle(Pens.Black, broder);

        }


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="code">代码</param>
        /// <param name="dataCount">数据量</param>
        /// <returns></returns>
        public string SinaGet_QH_OneMinute(string code, int dataCount)
        {
            string html = "";

            try
            {
                HttpWebRequest request = null;

                long timeSpan = Common.GetTimeStamp(DateTime.Now);

                code = "RM0";
                string var = $"_{code}_5_{timeSpan}";

                //修改 scale 的值可以获取不同级别的数据 scale=15 ---- 15 分钟 scale=240 ---日K   datalen 获取的节点数
                request = (HttpWebRequest)WebRequest.Create($"https://stock2.finance.sina.com.cn/futures/api/jsonp.php/var {var}=/InnerFuturesNewService.getFewMinLine?symbol={code}&type=1&datalen=100"); //300364    //300087
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

    }
}
