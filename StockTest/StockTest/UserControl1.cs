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

namespace StockTest
{
    public partial class UserControl1 : UserControl
    {
        //颜色配置方案
        private ColorConfig colorconfig = null;

        public UserControl1(List<Quote> quoteList, ColorConfig con)
        {
            InitializeComponent();

            this.SetStyle(
               ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

            SetStyle(
          ControlStyles.OptimizedDoubleBuffer, true);

            this.UpdateStyles();


            this.quoteList = quoteList;
            colorconfig = con;


            TargetAnalysis.ChangeColorConfigEvent += ChangeColorConfig;
        }


        Bitmap backImg = null;
        Graphics backImgraphics = null;

        bool isMoveDraw = false;

        List<Quote> quoteList = new List<Quote>();

        PointF basePoinf = new PointF();

        //临时指标
        List<AdxResult> tempTarget = new List<AdxResult>();

        //adx指标
        List<AdxResult> adxResults = null;
        //adl指标
        List<AdlResult> adlResults = null;

        TargetEnum targetType = TargetEnum.None;

        //显示当前K线详细信息
        public static event Action<Quote, int> ShowKinfoEvent;


        //均量线
        List<double> aveVolumn = new List<double>();

        static int kwidth = 4; //K线宽度
        static int disMargion = 20;  //绘制区域的上下间距
        static int volumnHeight = 200; //量能图的高度                    
        static int kPandding = 1; //K点间隔

        int startCalcIndex = 0; //绘制参考起点

        private void UserControl1_Load(object sender, EventArgs e)
        {
            //获取指标曲线
            adxResults = quoteList.GetAdx(14).ToList();
            adlResults = quoteList.GetAdl().ToList();

            foreach (var s in Enum.GetNames(typeof(TargetEnum)))
            {
                targetMenu.Items.Add(s);
            }

            Console.WriteLine("当前指标:" + targetType);

            this.MouseWheel += My_MouseWheel;
            this.Focus();
        }

        private void My_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if (kwidth >= 14)
                    kwidth = 14;
                else
                    kwidth++;
            }

            if (e.Delta < 0)
            {
                if (kwidth <= 3)
                    kwidth = 3;
                else
                    kwidth--;
            }

            this.Invalidate();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
        }



        protected override void OnPaint(PaintEventArgs e)
        {

            try
            {
                //base.OnPaint(e);
                Graphics g = e.Graphics;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                g.Clear(Color.White);

                //绘制边框
                g.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1));

                int maxCount = this.Width / kwidth; //5个橡树一个点

                if (maxCount > quoteList.Count)
                    maxCount = quoteList.Count;

                List<Quote> tempList = new List<Quote>();
                Quote[] tempArray = new Quote[maxCount];


                Array.Copy(quoteList.ToArray(), startCalcIndex, tempArray, 0, maxCount);
                tempList.AddRange(tempArray);

                for (int i = 0; i < maxCount; i++)
                {
                    tempTarget.Add(adxResults[i]);
                }


                #region K线绘制

                //找到最高价
                decimal maxPrice = 0;
                decimal minPrice = 0;

                tempList.ForEach(x =>
                {
                    if (x.High > maxPrice)
                        maxPrice = x.High;
                });

                minPrice = maxPrice;
                tempList.ForEach(x =>
                {
                    if (x.Low < minPrice)
                        minPrice = x.Low;
                });

                //价格波动范围
                decimal priceRange = maxPrice - minPrice;

                //绘制上边距
                g.DrawLine(Pens.Green, new Point(0, disMargion), new Point(this.Width, disMargion));

                //绘制下边距
                g.DrawLine(Pens.Green, new Point(0, this.Height - volumnHeight - disMargion), new Point(this.Width, this.Height - volumnHeight - disMargion));

                //绘制量能边距
                g.DrawLine(Pens.Black, new Point(0, this.Height - volumnHeight), new Point(this.Width, this.Height - volumnHeight));


                //确定价格高度占用像素偏移
                float pricePerPix = (float)((this.Height - volumnHeight - disMargion * 2) * 1F / (float)priceRange);


                ////绘制价格波动区间
                //for (int i = 0; i < Math.Ceiling(priceRange); i++)
                //{

                //    g.DrawLine(Pens.Red, new PointF(0, i * (float)priceRange), new PointF(40, i * (float) priceRange)); 

                //}


                decimal maxVolum = 0;
                tempList.ForEach((x) =>
                {
                    if (x.Volume > maxVolum)
                        maxVolum = x.Volume;
                }
                    );

                decimal minVolumn = maxVolum;
                tempList.ForEach((x) =>
                {
                    if (x.Volume < minVolumn)
                        minVolumn = x.Volume;
                });

                //量能区间
                decimal volumnRange = maxVolum - minVolumn;


                float volumnPerPix = volumnHeight * 1F / (float)maxVolum;

                //基准指标量
                Quote baseQuote = new Quote();
                int analysisCount = 0; //分析计数
                float pricePercent = 0.1F; //价格偏离值
                bool moveDirection = true; //默认正向移动

                bool isBuy = false; //当前是否已经买入

                DealInfo dealInfo = new DealInfo();
                dealInfo.RateOfDeal = new List<double>();
                dealInfo.InitMoney = 100000;
                dealInfo.DealCount = 0;
                int keyPointWidth = 10;


                //单独绘制k线
                for (int i = 0; i < tempList.Count; i++)
                {
                    //this.Refresh();
                    if (tempList[i].Open > tempList[i].Close)
                    {
                        RectangleF temp = new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, kwidth - kPandding, (float)(tempList[i].Open - tempList[i].Close) * pricePerPix);

                        //绘制影线
                        g.DrawLine(Pens.Gray, new PointF(i * kwidth + kwidth / 2, disMargion + (float)(maxPrice - tempList[i].High) * pricePerPix), new PointF(i * kwidth + kwidth / 2, disMargion + (float)(maxPrice - tempList[i].Low) * pricePerPix));

                        //绘制
                        //g.DrawRectangles(Pens.Green, new RectangleF[] { temp });
                        g.FillRectangles(new SolidBrush(Color.Gray), new RectangleF[] { temp });

                        temp = new RectangleF(i * kwidth, this.Height - volumnHeight + (float)(maxVolum - tempList[i].Volume) * volumnPerPix, kwidth - kPandding, (float)tempList[i].Volume * volumnPerPix);
                        //绘制量能
                        g.FillRectangles(new SolidBrush(Color.Gray), new RectangleF[] { temp });

                    }
                    else
                    {
                        RectangleF temp = new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, kwidth - kPandding, (float)(tempList[i].Close - tempList[i].Open) * pricePerPix);

                        //绘制影线
                        g.DrawLine(Pens.Coral, new PointF(i * kwidth + kwidth / 2, disMargion + (float)(maxPrice - tempList[i].High) * pricePerPix), new PointF(i * kwidth + kwidth / 2, disMargion + (float)(maxPrice - tempList[i].Low) * pricePerPix));


                        //绘制
                        //g.DrawRectangles(Pens.Red, new RectangleF[] { temp });
                        if (tempList[i].Close == tempList[i].Open)
                            g.DrawLine(Pens.Coral, new PointF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix), new PointF(i * kwidth + kwidth - kPandding, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix));
                        g.FillRectangles(new SolidBrush(Color.Coral), new RectangleF[] { temp });

                        temp = new RectangleF(i * kwidth, this.Height - volumnHeight + (float)(maxVolum - tempList[i].Volume) * volumnPerPix, kwidth - kPandding, (float)tempList[i].Volume * volumnPerPix);
                        //绘制量能
                        g.FillRectangles(new SolidBrush(Color.Coral), new RectangleF[] { temp });
                    }

                    //基于量)
                    #region 策略1
                    //if (i > 0)
                    //{
                    //    //终止
                    //    if (isBuy == true)
                    //    {
                    //        double rate = (double)(tempList[i].Close - dealInfo.Buy.Close) / ((double)dealInfo.Buy.Close * .1);
                    //        if (rate < -0.1)
                    //        {
                    //            isBuy = false;
                    //            dealInfo.RateOfDeal.Add(rate);

                    //            //绘制当前点位
                    //            g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));
                    //        }

                    //        //止盈
                    //        if (rate > 0.3)
                    //        {

                    //            isBuy = false;

                    //            g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));


                    //        }



                    //    }

                    //    if (tempList[i].Volume > tempList[i - 1].Volume)
                    //    {
                    //        if (analysisCount == 0)
                    //        {
                    //            if (tempList[i].Close >= tempList[i - 1].Close)
                    //                moveDirection = true;
                    //            else
                    //                moveDirection = false;

                    //            baseQuote = tempList[i - 1];


                    //            if (i == 57)
                    //                Console.WriteLine();
                    //            g.FillEllipse(new SolidBrush(Color.Pink), new RectangleF((i - 1) * kwidth, disMargion + (float)(maxPrice - tempList[i - 1].Close) * pricePerPix, keyPointWidth, keyPointWidth));


                    //        }
                    //        else
                    //        {
                    //            if (moveDirection == true)
                    //            {
                    //                //反向 且 偏离值符合预期
                    //                if (tempList[i].Close < tempList[i - 1].Close && (((float)(tempList[i].Close - baseQuote.Close) / ((float)baseQuote.Close * .1)) > .1))
                    //                {
                    //                    //绘制当前节点
                    //                    //  g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                    //                    if (isBuy == true)
                    //                    {
                    //                        isBuy = false;
                    //                        g.FillEllipse(new SolidBrush(Color.Blue), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                    //                        //计算比例

                    //                        double rate = (double)(tempList[i].Close - dealInfo.Buy.Close) / ((double)dealInfo.Buy.Close * .1);

                    //                        dealInfo.RateOfDeal.Add(rate);

                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (tempList[i].Close > tempList[i - 1].Close) // && ((Math.Abs((float)(tempList[i].Close - baseQuote.Close))/ ((float)baseQuote.Close * .1)) > .2)) // 偏移值大于.2
                    //                {
                    //                    //绘制当前节点
                    //                    //g.FillEllipse(new SolidBrush(Color.Green), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                    //                    if (isBuy == false)
                    //                    {
                    //                        isBuy = true;
                    //                        g.FillEllipse(new SolidBrush(Color.Black), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                    //                        dealInfo.Buy = tempList[i];
                    //                        Console.WriteLine("买入点:" + i);
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        analysisCount++;
                    //    }
                    //    else
                    //    {
                    //        if (analysisCount > 0)
                    //        {

                    //            if (moveDirection == true)
                    //            {
                    //                if (((float)(tempList[i].Close - baseQuote.Close) / ((float)baseQuote.Close * .1)) > .1)
                    //                {
                    //                    //g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                    //                    if (isBuy == true)
                    //                    {
                    //                        isBuy = false;
                    //                        //绘制当前点位
                    //                        g.FillEllipse(new SolidBrush(Color.Blue), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                    //                        double rate = (double)(tempList[i].Close - dealInfo.Buy.Close) / ((double)dealInfo.Buy.Close * .1);

                    //                        dealInfo.RateOfDeal.Add(rate);
                    //                    }
                    //                }
                    //            }
                    //            else  //反向移动, 量能反向，价格也应该反向，才是买点
                    //            {
                    //                if ((tempList[i].Close > tempList[i - 1].Close))// &&   (Math.Abs((float)(tempList[i].Close - baseQuote.Close)) / ((float)baseQuote.Close * .1)) > .2)
                    //                {
                    //                    //g.FillEllipse(new SolidBrush(Color.Green), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                    //                    if (isBuy == false)
                    //                    {
                    //                        isBuy = true;
                    //                        g.FillEllipse(new SolidBrush(Color.Black), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));
                    //                        dealInfo.Buy = tempList[i];

                    //                        Console.WriteLine("买入点:" + i);
                    //                    }
                    //                }
                    //            }

                    //            analysisCount = 0;
                    //        }
                    //        else //不具备分析条件
                    //        {


                    //        }
                    //    }

                    //}
                    //else
                    //{
                    //    baseQuote = tempList[i];
                    //}
                    #endregion


                    #region  策略2

                    /*
                     交易逻辑:
                        核心： 量价关系   股价的快速波动与成交量有密切关系，所以只在爆量的时候参与买进和卖出

                        1.找到量能快速增加的位置
                        2.判断当前是上涨趋势还是下跌趋势
                        3.下跌趋势买进 ，上涨趋势卖出
                        4.做止损和止盈
                     

                        1进场点选择：
                            1.首先是量能放大后缩量
                            2.股价不新低
                            3.上次放量位置与本次放量位置是否形成下跌趋势，且下跌幅度大于3%（可调）
                            4.上次量能与本次量能的差异 （待验证）
                     */


                    if (i > 59)  //将前一日的K线纳入分析
                    {

                        //判断量能是否在高位
                        List<Quote> oneDayVolumnList = new List<Quote>();
                        Quote[] oneDayTempVolumn = new Quote[60]; //5个小时，包含开盘和收盘半小时

                        Array.Copy(tempList.ToArray(), i - oneDayTempVolumn.Length, oneDayTempVolumn, 0, oneDayTempVolumn.Length);

                        oneDayVolumnList.AddRange(oneDayTempVolumn);

                        //最大6根K线
                        List<Quote> maxFiveVolumn = new List<Quote>();
                        List<Quote> minFiveVolumn = new List<Quote>();

                        oneDayVolumnList.Sort(delegate (Quote x, Quote y)
                        {
                            return x.Volume.CompareTo(y.Volume);
                        });

                        for (int j = 0; j < 10; j++)
                        {
                            minFiveVolumn.Add(oneDayVolumnList[j]);
                            maxFiveVolumn.Add(oneDayVolumnList[oneDayTempVolumn.Length - j - 1]);
                        }

                        //绘制一个起始量能点
                        // int indexMaxvolumnStart = maxFiveVolumn.Min(x => x.Date).Date;
                        int startIndexVolumn = tempList.FindIndex(x => x.Date == maxFiveVolumn.Min(y => y.Date));
                       // if (startIndexVolumn != -1)
                           // g.FillEllipse(new SolidBrush(Color.YellowGreen), new RectangleF(startIndexVolumn * kwidth, disMargion + (float)(maxPrice - tempList[startIndexVolumn].Close) * pricePerPix, keyPointWidth, keyPointWidth));



                        //按时间再次排序
                        //maxFiveVolumn.Sort(delegate (Quote x, Quote y)
                        //{
                        //    return x.Date.CompareTo(y.Date);
                        //});


                        //计算出当前区间的最大值和最小值（一天）
                        decimal maxPriceOf48 = 0;
                        decimal minPriceOf48 = 0;

                        oneDayVolumnList.ForEach(x => { if (x.High > maxPriceOf48) maxPriceOf48 = x.High; });
                        minPriceOf48 = maxPriceOf48;
                        oneDayVolumnList.ForEach(x => { if (x.Low < minPriceOf48) minPriceOf48 = x.Low; });


                        //止盈止损策略
                        //地量中股价走低，做止损
                        if (isBuy == true)
                        {
                            double rate = (double)((tempList[i].Close - dealInfo.Buy.Close) / dealInfo.Buy.Close)  * 100;

                            if (rate < -3)  //下跌三个点，止损
                            {

                                if (DateTime.Parse(dealInfo.Buy.Date.ToString("d")) < DateTime.Parse(tempList[i].Date.ToString("d")))
                                {
                                    dealInfo.RateOfDeal.Add(rate);
                                    dealInfo.InitMoney = dealInfo.InitMoney * (1 + rate / 100);
                                    isBuy = false;
                                    dealInfo.DealCount++;

                                    if (colorconfig.AbleSell)
                                    {
                                        g.FillEllipse(new SolidBrush(colorconfig.SellOutColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                                    }
                                }

                            }

                            if (rate > 5) //大于5个点， 止盈
                            {

                                if (DateTime.Parse(dealInfo.Buy.Date.ToString("d")) < DateTime.Parse(tempList[i].Date.ToString("d")))
                                {
                                    dealInfo.RateOfDeal.Add(rate);
                                    dealInfo.InitMoney = dealInfo.InitMoney * (1 + rate / 100);
                                    isBuy = false;
                                    dealInfo.DealCount++;
                                    if (colorconfig.AbleSell)
                                    {
                                        g.FillEllipse(new SolidBrush(colorconfig.SellOutColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));
                                    }
                                }
                            }

                        }

                        //判断当前量能是否开始放量
                        if (tempList[i].Volume >= maxFiveVolumn[maxFiveVolumn.Count - 1].Volume)
                        {
                            if (colorconfig.AbleHeigVolumn)
                                g.FillEllipse(new SolidBrush(colorconfig.HeigVolumnColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                            analysisCount++; //标记这里可以开始策略分析

                            //趋势判断（上涨或下跌），找到爆量点的最高和最低位置，判断离当前点位最近的一个，如果近点小于30分钟，
                            //则用另一个判断（另一个小于30分钟的概率较低，如果不行，只能去当天的最高和最低，来继续分析趋势）
                            //下跌趋势

                            //先保证时间间隔大于30分钟
                            List<Quote> timspan30List = new List<Quote>();
                            maxFiveVolumn.ForEach(x => {
                                if (tempList[i].Date - x.Date >= TimeSpan.FromMinutes(30))
                                    timspan30List.Add(x);
                            });

                            decimal maxPriceofMaxVolumns = timspan30List.Max(x => x.High);
                            decimal minPriceOfMaxVolumns = timspan30List.Min(x => x.Low);

                            DateTime maxPrcieTime = timspan30List.Find(x => x.High == maxPriceofMaxVolumns).Date;
                            DateTime minPriceTime = timspan30List.Find(x => x.Low == minPriceOfMaxVolumns).Date;



                            //判断较近的那个 ,同时大于20分钟
                            if (minPriceTime > maxPrcieTime)  //低价距离当前点位较近
                            {
                                if (tempList[i].Date - minPriceTime >= TimeSpan.FromMinutes(30))
                                {
                                    if (tempList[i].Low > minPriceOfMaxVolumns) //大于最低价
                                    {
                                        moveDirection = true;
                                    }
                                    else
                                    {
                                        moveDirection = false;
                                    }
                                }
                                else  // 时间间隔不够，以较远的那个做参考
                                {

                                    if (tempList[i].High > maxPriceofMaxVolumns)
                                    {
                                        moveDirection = true;
                                    }
                                    else
                                        moveDirection = false;

                                }
                            }
                            else   //高位距离当前点位较近
                            {
                                if (tempList[i].Date - maxPrcieTime >= TimeSpan.FromMinutes(30)) //时间间隔合理
                                {
                                    if (tempList[i].High > maxPriceofMaxVolumns)
                                        moveDirection = true;
                                    else
                                        moveDirection = false;
                                }
                                else //时间间隔不够，以较远的为参考
                                {

                                    if (tempList[i].Low > minPriceOfMaxVolumns) //大于最低价
                                    {
                                        moveDirection = true;
                                    }
                                    else
                                    {
                                        moveDirection = false;
                                    }
                                }

                            }
                               
                        }
                        else
                        {
                            
                            //如果分析不为0 ，根据下跌趋势可以寻找合适的买点(反转点位)
                            if (analysisCount >= 1)
                            {
                                if (moveDirection == false) //下跌趋势
                                {
                                    //下跌缩量到地量，股价不新低 如果没有买入，此时为一个买点

                                    //找到当前放量区间的最低价

                                    List<Quote> tempAnalysisList = new List<Quote>();

                                    for (int k = 1; k <= analysisCount; k++)
                                    {
                                        tempAnalysisList.Add(tempList[i - k]);
                                    }

                                    //按照最低价排序
                                    tempAnalysisList.Sort(delegate(Quote x, Quote y) { return x.Low.CompareTo(y.Low); });

                                    //股价不新低 且收红盘  买点
                                    if (tempList[i].Low >= tempAnalysisList[0].Low  && tempList[i].Open <= tempList[i].Close)
                                    {
                                        if (colorconfig.AbleEnterPoint) //是否绘制当前节点
                                        {
                                            g.FillEllipse(new SolidBrush(colorconfig.EnterPointColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix + 10, keyPointWidth, keyPointWidth));

                                            if (isBuy == false)
                                            {
                                                isBuy = true;
                                                dealInfo.Buy = tempList[i];
                                                if (colorconfig.AbleBuy)
                                                {
                                                    g.FillEllipse(new SolidBrush(colorconfig.BuyInColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                                                }
                                            }
                                        }
                                    }

                                }

                            }

                            //量能萎缩到不足以进入分析时 归0分析标记
                            analysisCount = 0;
                        }

                        //进出场点位策略分析
                        if (analysisCount > 1)   //首次放量不做分析，作为分析开始的标记
                        {
                            //上涨趋势，找出场点
                            if (moveDirection == true)
                            {
                                //缩量 可以出场
                                if (tempList[i].Volume < tempList[i - 1].Volume)
                                {
                                    if (colorconfig.AbleExitPoint)
                                    {
                                        g.FillEllipse(new SolidBrush(colorconfig.ExitPointColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix + 10, keyPointWidth, keyPointWidth));
                                    }

                                    //analysisCount = 0;
                                    //moveDirection = false;

                                    if (isBuy == true) //已经买入，止盈
                                    {

                                        if ( DateTime.Parse(dealInfo.Buy.Date.ToString("d")) < DateTime.Parse(tempList[i].Date.ToString("d"))) //判断时间，交易规则不能当天买卖（A股股票） 
                                        {
                                            double rate = (double)((tempList[i].Close - dealInfo.Buy.Close) / dealInfo.Buy.Close) * 100;
                                            dealInfo.RateOfDeal.Add(rate);
                                            dealInfo.InitMoney =  dealInfo.InitMoney *(1 + rate / 100);
                                            isBuy = false;
                                            dealInfo.DealCount++;
                                            if (colorconfig.AbleSell)
                                            {
                                                g.FillEllipse(new SolidBrush(colorconfig.SellOutColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                                            }
                                        }
                                    }

                                }
                                else //继续放量
                                {
                                    //此区间下跌 （放量下跌）  出场
                                    if (tempList[i].Open >= tempList[i].Close)
                                    {
                                        if (colorconfig.AbleExitPoint)
                                            g.FillEllipse(new SolidBrush(colorconfig.ExitPointColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix + 10, keyPointWidth, keyPointWidth));

                                       
                                        if (isBuy == true)//如果已经买入，卖出止盈
                                        {

                                            if (DateTime.Parse(dealInfo.Buy.Date.ToString("d")) < DateTime.Parse(tempList[i].Date.ToString("d"))) //判断时间，交易规则不能当天买卖（A股股票） 
                                            {
                                                double rate = (double)((tempList[i].Close - dealInfo.Buy.Close) / dealInfo.Buy.Close) * 100;
                                                dealInfo.RateOfDeal.Add(rate);
                                                dealInfo.InitMoney = dealInfo.InitMoney * (1 + rate / 100);
                                                isBuy = false;
                                                dealInfo.DealCount++;
                                                if (colorconfig.AbleSell)
                                                {
                                                    g.FillEllipse(new SolidBrush(colorconfig.SellOutColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                                                }
                                            }
                                        }

                                    }
                                    else  //放量上涨  持有
                                    {

                                    }
                                }

                            }
                            else//下跌趋势，寻找入场点
                            {
                                //缩量，可能存在买点
                                if (tempList[i].Volume < tempList[i - 1].Volume)
                                {
                                    //股价不新低
                                    if (tempList[i].Low > tempList[i - 1].Low)
                                    {
                                        //股价收红 买点，风险偏大
                                        if (tempList[i].Open < tempList[i].Close)
                                        {
                                            if (colorconfig.AbleEnterPoint) //是否绘制当前节点
                                            {
                                                g.FillEllipse(new SolidBrush(colorconfig.EnterPointColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix + 10, keyPointWidth, keyPointWidth));

                                                if (isBuy == false)
                                                {
                                                    isBuy = true;
                                                    dealInfo.Buy = tempList[i];
                                                    if (colorconfig.AbleBuy)
                                                    {
                                                        g.FillEllipse(new SolidBrush(colorconfig.BuyInColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //股价不新低，收下跌K, 继续观察
                                        }
                                    }
                                    else
                                    {
                                        //股价新低，其他情况 继续观察
                                    }

                                }
                                else //继续放量，
                                {
                                    //有可能前一次缩放十字星，再次放量上涨，也可以作为买点
                                    if (tempList[i].Close > tempList[i].Open  && tempList[i].Low > tempList[i - 1].Low ) //股价必须上涨，股价不新低，上一场收十子星或红K ( 形成一个V型反转)
                                    {
                                        //判断上一次的收盘情况
                                        if ((tempList[i - 1].Open <= tempList[i - 1].Close) &&  
                                            ((tempList[i - 1].High - tempList[i -1].Low )/ tempList[i - 1].Low < 0.02m ) ) //十字星 或红盘 买点 ,振幅小于2
                                        {
                                            if (colorconfig.AbleEnterPoint) //是否绘制当前节点
                                            {
                                                g.FillEllipse(new SolidBrush(colorconfig.EnterPointColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix + 10, keyPointWidth, keyPointWidth));

                                                if (isBuy == false)
                                                {
                                                    isBuy = true;
                                                    dealInfo.Buy = tempList[i];
                                                    if (colorconfig.AbleBuy)
                                                    {
                                                        g.FillEllipse(new SolidBrush(colorconfig.BuyInColor), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));

                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else //继续下跌 继续观察
                                    {

                                    }
                                }
                            }
                        }

                        //低量能分析（暂不分析，地量能分析意义不大）
                        //index = minFiveVolumn.FindIndex(x => x.Volume == tempList[i].Volume);
                        //if (index != -1)
                        //    g.FillEllipse(new SolidBrush(Color.Black), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, keyPointWidth, keyPointWidth));
                    }
                    else
                    {
                        baseQuote = tempList[0];
                    }



                    #endregion
                }
                #endregion


               // Console.WriteLine("收益率：" + Math.Round(dealInfo.RateOfDeal.Sum(), 2) + "   总资产:" + dealInfo.InitMoney + "   交易次数:" + dealInfo.DealCount);



                //List<PointF> aveVolumnPoints = new List<PointF>();
                //aveVolumnPoints = CalcAveVolumnPoints(maxCount, kwidth, this.Height - volumnHeight, (double)maxVolum, volumnPerPix, 5);
                //if (aveVolumnPoints.Count > 0)
                //    g.DrawLines(Pens.Blue, aveVolumnPoints.ToArray());

                List<PointF> adxPoints = new List<PointF>();
                adxPoints = CalcTargetPoints((this.Height - volumnHeight - disMargion * 2) * 1F, kwidth, maxCount, targetType);

                if (adxPoints.Count > 0)
                    g.DrawLines(Pens.Blue, adxPoints.ToArray());


                //绘制观察线
                if (isMoveDraw)
                {
                    //纵向
                    g.DrawLine(Pens.Black, new PointF(basePoinf.X, this.Height), new PointF(basePoinf.X, 0));
                    g.DrawLine(Pens.Black, new PointF(0, basePoinf.Y), new PointF(this.Width, basePoinf.Y));

                    //显示当前K线信息
                    ShowKinfoEvent?.Invoke(tempList[(int)Math.Floor(basePoinf.X / kwidth)], (int)Math.Floor(basePoinf.X / kwidth));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        /// <summary>
        /// 计算均量线
        /// </summary>
        /// <param name="maxCount">当前k线点数</param>
        /// <param name="aveRange">均量级别 默认1日  5则为5日均量</param>
        private List<PointF> CalcAveVolumnPoints(int maxCount, int kwdith, float drawHeght, double maxVolumn, float volumnPerPix, int aveRange = 1)
        {

            List<PointF> points = new List<PointF>();

            List<double> aveVolumn = new List<double>();

            double totalVolumn = 0;
            for (int i = 0; i < maxCount; i++)
            {
                if (aveRange == 1)
                {
                    totalVolumn += (double)quoteList[i + startCalcIndex].Volume;
                    aveVolumn.Add(totalVolumn / (i + 1));

                    points.Add(new PointF(i * kwdith, drawHeght + (float)(maxVolumn - aveVolumn[i]) * volumnPerPix));
                }
                else
                {
                    if (i < aveRange)
                    {
                        aveVolumn.Add(0);  //不具备参考价值
                        totalVolumn += (double)quoteList[i + startCalcIndex].Volume;
                        points.Add(new PointF(i * kwdith, this.Height));

                    }
                    else
                    {
                        totalVolumn += (double)quoteList[i + startCalcIndex].Volume;
                        totalVolumn -= (double)quoteList[i - aveRange + startCalcIndex].Volume;
                        aveVolumn.Add(totalVolumn / aveRange);

                        //计算坐标点
                        points.Add(new PointF(i * kwdith, drawHeght + (float)(maxVolumn - aveVolumn[i]) * volumnPerPix));

                    }
                }
            }

            return points;
        }

        //计算指标坐标点
        private List<PointF> CalcTargetPoints(float drawHeight, float kwidth, int maxcount, TargetEnum targetType)
        {
            List<PointF> points = new List<PointF>();

            if (targetType == TargetEnum.None)
                return points;

            try
            {
                List<Quote> tempList = new List<Quote>();
                Quote[] tempArray = new Quote[maxcount];
                Array.Copy(quoteList.ToArray(), 0, tempArray, 0, maxcount);
                tempList.AddRange(tempArray);

                List<double?> targetRes = new List<double?>();

                for (int i = 0; i < maxcount; i++)
                {
                    switch (targetType)
                    {
                        case TargetEnum.Adx:
                            targetRes.Add(adxResults[i].Adx);
                            break;
                        case TargetEnum.Adl:
                            targetRes.Add(adlResults[i].Adl);
                            break;
                    }
                }

                double maxAdx = 0;
                double minAdx = 0;
                targetRes.ForEach((x) =>
                {
                    if (x > maxAdx)
                        maxAdx = (double)x;
                });

                minAdx = maxAdx;
                targetRes.ForEach((x) =>
                {
                    if (x < minAdx)
                        minAdx = (double)x;
                });

                double adxRange = maxAdx - minAdx;

                float adxPerPix = drawHeight / (float)adxRange;

                for (int i = 0; i < targetRes.Count; i++)
                {

                    if (targetRes[i] != null)
                    {
                        points.Add(new PointF(i * kwidth, (float)(maxAdx - targetRes[i]) * adxPerPix));
                    }
                    else
                    {
                        points.Add(new PointF(i * kwidth, 0));
                    }
                }

            }
            catch (Exception ex)
            {
            }

            return points;
        }

        private void UserControl1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void UserControl1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isMoveDraw)
            {
                basePoinf = new PointF(e.X, e.Y);
                this.Invalidate();
            }
        }

        private void UserControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMoveDraw = !isMoveDraw;
            }
            else if (e.Button == MouseButtons.Right) //右键菜单
            {
                Point temp = new Point(e.X, e.Y);
                targetMenu.Show(PointToScreen(temp));
            }
        }

        private List<RectangleF> getKlineRect(List<Quote> list)
        {
            List<RectangleF> ret = new List<RectangleF>();

            try
            {
                if (list.Count > 0)
                {
                }
                else
                {
                    MessageBox.Show("数据集为空");
                }

            }
            catch (Exception ex)
            {

            }

            return ret;
        }


        /// <summary>
        /// 修改叠加指标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void targetMenu_MouseClick(object sender, MouseEventArgs e)
        {
            //string name = targetMenu.
            //targetType = TargetEnum.Adl;
            //ContextMenuStrip menu = sender as ContextMenuStrip;
            //MessageBox.Show(menu.Text);
        }

        /// <summary>
        /// 修改叠加指标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void targetMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.ToString();

            targetType = (TargetEnum)Enum.Parse(typeof(TargetEnum), name);

            Console.WriteLine("当前指标:" + targetType);

            this.Invalidate(true);

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        //移动信息标签页
        bool moveSetting = false;
        Point oldInofLoc = new Point();
        Point startMovePoint = new Point();

        //处理键盘事件
        private void UserControl1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void UserControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                if (startCalcIndex < quoteList.Count - 100)
                    startCalcIndex++;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (startCalcIndex > 0)
                    startCalcIndex--;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (startCalcIndex < quoteList.Count - 100)
                    startCalcIndex += 4;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (startCalcIndex >= 4)
                    startCalcIndex -= 4;
            }

            this.Invalidate();
        }

        private void UserControl1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
                e.IsInputKey = true;
        }


        private void ChangeColorConfig(ColorConfig config)
        {
            colorconfig = config;
            this.Invalidate();
        }
    }
}
