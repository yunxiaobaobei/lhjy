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

        TargetEnum targetType = TargetEnum.Adl;



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
            int kwidth = 5; //K线宽度
            int kPandding = 2; //K点间隔

            //绘制边框
            g.DrawRectangle(Pens.Black, new Rectangle(0, 0, this.Width - 1, this.Height - 1 ));

            int maxCount = this.Width / kwidth; //5个橡树一个点

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


            //单独绘制k线
            for (int i = 0; i < tempList.Count; i++)
            {
                if (tempList[i].Open > tempList[i].Close)
                {
                    RectangleF temp = new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Open) * pricePerPix, kwidth - kPandding, (float)(tempList[i].Open - tempList[i].Close) * pricePerPix);


                    //绘制影线
                    g.DrawLine(Pens.Green, new PointF(i * kwidth + kwidth / 2, disMargion + (float)(maxPrice - tempList[i].High) * pricePerPix), new PointF(i * kwidth + kwidth / 2, disMargion +  (float)(maxPrice - tempList[i].Low) * pricePerPix));


                    //绘制
                    //g.DrawRectangles(Pens.Green, new RectangleF[] { temp });
                    g.FillRectangles(new SolidBrush(Color.Green), new RectangleF[] { temp});


                    temp = new RectangleF(i * kwidth, this.Height - volumnHeight + (float)(maxVolum - tempList[i].Volume) * volumnPerPix, kwidth - kPandding,  (float)tempList[i].Volume * volumnPerPix);
                    //绘制量能
                    g.FillRectangles(new SolidBrush(Color.Green), new RectangleF[] { temp});
                   
                }
                else
                {
                    RectangleF temp = new RectangleF(i * kwidth, disMargion + (float)(maxPrice - tempList[i].Close) * pricePerPix, kwidth - kPandding, (float)(tempList[i].Close - tempList[i].Open) * pricePerPix);

                    //绘制影线
                    g.DrawLine(Pens.Red, new PointF(i * kwidth + kwidth / 2,disMargion +  (float)(maxPrice - tempList[i].High) * pricePerPix), new PointF(i * kwidth + kwidth / 2, disMargion  + (float)(maxPrice - tempList[i].Low) * pricePerPix));


                    //绘制
                    //g.DrawRectangles(Pens.Red, new RectangleF[] { temp });
                    g.FillRectangles(new SolidBrush(Color.Red), new RectangleF[] { temp });

                    temp = new RectangleF(i * kwidth, this.Height - volumnHeight + (float)(maxVolum - tempList[i].Volume) * volumnPerPix, kwidth - kPandding, (float)tempList[i].Volume * volumnPerPix);
                    //绘制量能
                    g.FillRectangles(new SolidBrush(Color.Red), new RectangleF[] { temp });
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


            //绘制指标
           // double maxAdx = 0;
           // double minAdx = 0;
           // tempTarget.ForEach((x) =>
           //{
           //    if (x.Adx > maxAdx)
           //        maxAdx = (double)x.Adx;
           //} );

           // minAdx = maxAdx;
           // tempTarget.ForEach((x) =>
           //{
           //    if (x.Adx < minAdx)
           //        minAdx = (double)x.Adx;
           //});

           // double adxRange = maxAdx - minAdx; 
           // float adxPerPix = (this.Height - volumnHeight - disMargion * 2) * 1F / (float)adxRange;
            //List<PointF> adxPoints = new List<PointF>();
            // for (int i = 0; i < tempTarget.Count; i++)
            // {

            //     if (tempTarget[i].Adx != null)
            //     {
            //         adxPoints.Add(new PointF(i * kwidth, (float)(maxAdx - tempTarget[i].Adx) * adxPerPix));
            //     }
            //     else
            //     {
            //         adxPoints.Add(new PointF(i * kwidth, 0));
            //     }
            // }

            List<PointF> adxPoints = new List<PointF>();
            adxPoints = CalcTargetPoints((this.Height - volumnHeight - disMargion * 2) * 1F, kwidth, maxCount, targetType);
            g.DrawLines(Pens.Blue, adxPoints.ToArray());

        }

        //计算指标坐标点
        private List<PointF> CalcTargetPoints(float drawHeight, float kwidth, int maxcount,  TargetEnum targetType)
        {

            Console.WriteLine("当前指标:" + targetType);

            List<PointF> points = new List<PointF>();

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

        private void targetMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string name = e.ClickedItem.ToString();

            targetType = (TargetEnum)Enum.Parse(typeof(TargetEnum), name);

            this.Invalidate(true);

        }
    }
}
