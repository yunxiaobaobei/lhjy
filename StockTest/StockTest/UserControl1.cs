﻿using System;
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
        public UserControl1(List<Quote> quoteList)
        {
            InitializeComponent();

            this.SetStyle(
               ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint, true);

            SetStyle(
          ControlStyles.OptimizedDoubleBuffer,
                                              
           true);

            this.UpdateStyles();


            this.quoteList = quoteList;
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

        //均量线
        List<double> aveVolumn = new List<double>();

      static  int kwidth = 4; //K线宽度

        private void UserControl1_Load(object sender, EventArgs e)
        {
            //backImg = new Bitmap(this.Width, this.Height);
            //backImgraphics = Graphics.FromImage(backImg);
            //backImgraphics.Clear(Color.GreenYellow);

            //获取指标曲线
            adxResults = quoteList.GetAdx(14).ToList();
            adlResults = quoteList.GetAdl().ToList();
            
           foreach(var s in Enum.GetNames(typeof(TargetEnum)))
            {
                targetMenu.Items.Add(s);
            }


            Console.WriteLine("当前指标:" + targetType);

            this.MouseWheel += My_MouseWheel;
        }

        private void My_MouseWheel(object sender, MouseEventArgs e)
        {
            Console.WriteLine(e.Delta);

            if (e.Delta > 0)
            {
                if (kwidth >= 6)
                    kwidth = 6;
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
            //base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            g.Clear(Color.White);

            int disMargion = 20;  //绘制区域的上下间距
            int volumnHeight = 200; //量能图的高度
           // int kwidth = 5; //K线宽度
            int kPandding = 1; //K点间隔

            //绘制边框
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1 ));

            int maxCount = this.Width / kwidth; //5个橡树一个点

            if(maxCount > quoteList.Count)
                maxCount = quoteList.Count;

            List<Quote> tempList = new List<Quote>();
            Quote[] tempArray = new Quote[maxCount];
            Array.Copy(quoteList.ToArray(), 0, tempArray, 0, maxCount);
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
            { if(x.High > maxPrice)
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
            float pricePerPix = (float)((this.Height - volumnHeight  - disMargion * 2) *1F / (float)priceRange );


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
            dealInfo.InitMoney = 10000;
            

            //单独绘制k线
            for (int i = 0; i < tempList.Count; i++)
            {
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
                    g.DrawLine(Pens.Silver, new PointF(i * kwidth + kwidth / 2, disMargion + (float)(maxPrice - tempList[i].High) * pricePerPix), new PointF(i * kwidth + kwidth / 2, disMargion + (float)(maxPrice - tempList[i].Low) * pricePerPix));


                    //绘制
                    //g.DrawRectangles(Pens.Red, new RectangleF[] { temp });
                    g.FillRectangles(new SolidBrush(Color.Silver), new RectangleF[] { temp });

                    temp = new RectangleF(i * kwidth, this.Height - volumnHeight + (float)(maxVolum - tempList[i].Volume) * volumnPerPix, kwidth - kPandding, (float)tempList[i].Volume * volumnPerPix);
                    //绘制量能
                    g.FillRectangles(new SolidBrush(Color.Silver), new RectangleF[] { temp });
                }

                //基于量

                if (i > 0)
                {
                    //终止
                    if (isBuy == true)
                    {
                        double rate = (double)(tempList[i].Close - dealInfo.Buy.Close) / ((double)dealInfo.Buy.Close * .1);
                        if (rate < -0.1)
                        {
                            isBuy = false;
                            dealInfo.RateOfDeal.Add(rate);

                            //绘制当前点位
                            g.FillEllipse(new SolidBrush(Color.Yellow), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, 5, 5));
                        }
                    }

                    if (tempList[i].Volume > tempList[i - 1].Volume)
                    {
                        if (analysisCount == 0)
                        {
                            if (tempList[i].Close >= tempList[i - 1].Close)
                                moveDirection = true;
                            else
                                moveDirection = false;

                            baseQuote = tempList[i - 1];
                        }
                        else
                        {
                            if (moveDirection == true)
                            {
                                //反向 且 偏离值符合预期
                                if (tempList[i].Close < tempList[i - 1].Close && (((float)(tempList[i].Close - baseQuote.Close) / ((float)baseQuote.Close * .1)) > .1))
                                {
                                    //绘制当前节点
                                    g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, 5, 5));

                                    if (isBuy == true)
                                    {
                                        isBuy = false;
                                        g.FillEllipse(new SolidBrush(Color.Blue), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, 5, 5));

                                        //计算比例

                                        double rate = (double)(tempList[i].Close - dealInfo.Buy.Close) / ((double)dealInfo.Buy.Close * .1);
                                        
                                        dealInfo.RateOfDeal.Add(rate);

                                    }
                                }
                            }
                            else
                            {
                                if (tempList[i].Close > tempList[i - 1].Close && ((Math.Abs((float)(tempList[i].Close - baseQuote.Close))/ ((float)baseQuote.Close * .1)) > .2)) // 偏移值大于.2
                                {
                                    //绘制当前节点
                                    g.FillEllipse(new SolidBrush(Color.Green), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, 5, 5));

                                    if (isBuy == false)
                                    {
                                        isBuy = true;
                                        g.FillEllipse(new SolidBrush(Color.Pink), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, 5, 5));

                                        dealInfo.Buy = tempList[i];
                                    }
                                }
                            }
                        }
                        analysisCount++;
                    }
                    else
                    {
                        if (analysisCount > 0)
                        {

                            if (moveDirection == true)
                            {
                                if (((float)(tempList[i].Close - baseQuote.Close) / ((float)baseQuote.Close * .1)) > .1)
                                {
                                    g.FillEllipse(new SolidBrush(Color.Red), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, 5, 5));

                                    if (isBuy == true)
                                    {
                                        isBuy = false;
                                        //绘制当前点位
                                        g.FillEllipse(new SolidBrush(Color.Blue), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, 5, 5));

                                        double rate = (double)(tempList[i].Close - dealInfo.Buy.Close) / ((double)dealInfo.Buy.Close * .1);

                                        dealInfo.RateOfDeal.Add(rate);
                                    }
                                }
                            }
                            else  //反向移动
                            {
                                if ((Math.Abs((float)(tempList[i].Close - baseQuote.Close)) / ((float)baseQuote.Close * .1)) > .2)
                                {
                                    g.FillEllipse(new SolidBrush(Color.Green), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, 5, 5));

                                    if (isBuy == false)
                                    {
                                        isBuy = true;
                                        g.FillEllipse(new SolidBrush(Color.Pink), new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, 5, 5));
                                        dealInfo.Buy = tempList[i];
                                    }
                                }
                            }

                            analysisCount = 0;
                        }
                        else //不具备分析条件
                        { 


                        }
                    }

                }
                else
                {
                    baseQuote = tempList[i];
                }
            }
            #endregion

            //绘制观察线
            if (isMoveDraw)
            {
                //纵向
                g.DrawLine(Pens.Black, new PointF(basePoinf.X, this.Height), new PointF(basePoinf.X, 0));
                g.DrawLine(Pens.Black, new PointF(0, basePoinf.Y), new PointF(this.Width, basePoinf.Y));

            }

            List<PointF> adxPoints = new List<PointF>();

            adxPoints = CalcAveVolumnPoints(maxCount, kwidth, this.Height - volumnHeight ,(double)maxVolum, volumnPerPix, 5);
            if(adxPoints.Count > 0)
                g.DrawLines(Pens.Blue, adxPoints.ToArray());

            adxPoints = CalcTargetPoints((this.Height - volumnHeight - disMargion * 2) * 1F, kwidth, maxCount, targetType);
          
            if(adxPoints.Count > 0)
                g.DrawLines(Pens.Blue, adxPoints.ToArray());

        }


        /// <summary>
        /// 计算均量线
        /// </summary>
        /// <param name="maxCount">当前k线点数</param>
        /// <param name="aveRange">均量级别 默认1日  5则为5日均量</param>
        private List<PointF> CalcAveVolumnPoints(int maxCount, int kwdith, float drawHeght ,double maxVolumn, float volumnPerPix, int aveRange = 1)
        {

            List<PointF> points = new List<PointF>();

            List<double> aveVolumn = new List<double>();

            double totalVolumn = 0;
            for (int i = 0; i < maxCount; i++)
            {
                if (aveRange == 1)
                {
                    totalVolumn += (double)quoteList[i].Volume;
                    aveVolumn.Add(totalVolumn / (i + 1));

                    points.Add(new PointF(i * kwdith, drawHeght + (float)(maxVolumn - aveVolumn[i]) * volumnPerPix));
                }
                else
                {
                    if (i < aveRange)
                    {
                        aveVolumn.Add(0);  //不具备参考价值
                        totalVolumn += (double)quoteList[i].Volume;
                        points.Add(new PointF(i * kwdith, 0));

                    }
                    else
                    {
                        totalVolumn += (double)quoteList[i].Volume;
                        totalVolumn -= (double)quoteList[i - aveRange].Volume;
                        aveVolumn.Add( totalVolumn / aveRange);

                        //计算坐标点
                        points.Add(new PointF(i * kwdith, drawHeght + (float)(maxVolumn - aveVolumn[i]) * volumnPerPix));

                    }
                }
            }

            return points;
        }

        //计算指标坐标点
        private List<PointF> CalcTargetPoints(float drawHeight, float kwidth, int maxcount,  TargetEnum targetType)
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
               // Console.WriteLine("sss");
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

    }
}
