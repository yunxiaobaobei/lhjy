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
                DealInfo dealResult= strategic_One.Analysis(quoteList);

                MessageBox.Show("交易结果分析  \r\n交易次数：" + dealResult.DealCount + "\r\n交易收益:" + dealResult.RateOfDeal.Max().ToString("0.##") + "%"  + "\r\n总金额:" + dealResult.InitMoney) ;

                panel_Kline.Controls.Clear();

                //UserControl1 klineControl = new UserControl1(quoteList, colorConfig);
                //klineControl.Dock = DockStyle.Fill;
                //panel_Kline.Controls.Add(klineControl);
                //klineControl.ShowDialog();
            }
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
    }
}
