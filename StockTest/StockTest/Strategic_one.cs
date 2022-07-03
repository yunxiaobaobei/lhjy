using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skender.Stock.Indicators;

namespace StockTest
{

    //策略分析一
    public class Strategic_one
    {
        public DealInfo Analysis(List<Quote> tempList)
        {

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


            int analysisCount = 0; //分析计数
           
            bool moveDirection = true; //默认正向移动

            bool isBuy = false; //当前是否已经买入

            DealInfo dealInfo = new DealInfo();
            dealInfo.RateOfDeal = new List<double>();
            dealInfo.InitMoney = 10000;
            dealInfo.DealCount = 0;



            for (int i = 60; i < tempList.Count; i++)
            {

                if(dealInfo.DealCount  > 0)
                Console.WriteLine("交易天数:" +  (tempList[i].Date - tempList[59].Date).TotalDays +    "   交易次数:" +  dealInfo.DealCount +
                    "上次收益:" + dealInfo.RateOfDeal[dealInfo.RateOfDeal.Count -1] + "   当前资金：" + dealInfo.InitMoney);

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
                    double rate = (double)((tempList[i].Close - dealInfo.Buy.Close) / dealInfo.Buy.Close) * 100;

                    if (rate < -3)  //下跌三个点，止损
                    {

                        if (DateTime.Parse(dealInfo.Buy.Date.ToString("d")) < DateTime.Parse(tempList[i].Date.ToString("d")))
                        {
                            dealInfo.RateOfDeal.Add(rate);
                            dealInfo.InitMoney = dealInfo.InitMoney * (1 + rate / 100);
                            isBuy = false;
                            dealInfo.DealCount++;


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

                        }
                    }

                }

                //判断当前量能是否开始放量
                if (tempList[i].Volume >= maxFiveVolumn[maxFiveVolumn.Count - 1].Volume)
                {

                    analysisCount++; //标记这里可以开始策略分析

                    //趋势判断（上涨或下跌），找到爆量点的最高和最低位置，判断离当前点位最近的一个，如果近点小于30分钟，
                    //则用另一个判断（另一个小于30分钟的概率较低，如果不行，只能去当天的最高和最低，来继续分析趋势）
                    //下跌趋势

                    //先保证时间间隔大于30分钟
                    List<Quote> timspan30List = new List<Quote>();
                    maxFiveVolumn.ForEach(x =>
                    {
                        if (tempList[i].Date - x.Date >= TimeSpan.FromMinutes(20))
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
                            tempAnalysisList.Sort(delegate (Quote x, Quote y) { return x.Low.CompareTo(y.Low); });

                            //股价不新低 且收红盘  买点
                            if (tempList[i].Low >= tempAnalysisList[0].Low && tempList[i].Open <= tempList[i].Close)
                            {

                                if (isBuy == false)
                                {
                                    isBuy = true;
                                    dealInfo.Buy = tempList[i];

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

                            //analysisCount = 0;
                            //moveDirection = false;

                            if (isBuy == true) //已经买入，止盈
                            {

                                if (DateTime.Parse(dealInfo.Buy.Date.ToString("d")) < DateTime.Parse(tempList[i].Date.ToString("d"))) //判断时间，交易规则不能当天买卖（A股股票） 
                                {
                                    double rate = (double)((tempList[i].Close - dealInfo.Buy.Close) / dealInfo.Buy.Close) * 100;
                                    dealInfo.RateOfDeal.Add(rate);
                                    dealInfo.InitMoney = dealInfo.InitMoney * (1 + rate / 100);
                                    isBuy = false;
                                    dealInfo.DealCount++;

                                }
                            }

                        }
                        else //继续放量
                        {
                            //此区间下跌 （放量下跌）  出场
                            if (tempList[i].Open >= tempList[i].Close)
                            {

                                if (isBuy == true)//如果已经买入，卖出止盈
                                {

                                    if (DateTime.Parse(dealInfo.Buy.Date.ToString("d")) < DateTime.Parse(tempList[i].Date.ToString("d"))) //判断时间，交易规则不能当天买卖（A股股票） 
                                    {
                                        double rate = (double)((tempList[i].Close - dealInfo.Buy.Close) / dealInfo.Buy.Close) * 100;
                                        dealInfo.RateOfDeal.Add(rate);
                                        dealInfo.InitMoney = dealInfo.InitMoney * (1 + rate / 100);
                                        isBuy = false;
                                        dealInfo.DealCount++;

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

                                    if (isBuy == false)
                                    {
                                        isBuy = true;
                                        dealInfo.Buy = tempList[i];

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
                            if (tempList[i].Close > tempList[i].Open && tempList[i].Low > tempList[i - 1].Low) //股价必须上涨，股价不新低，上一场收十子星或红K ( 形成一个V型反转)
                            {
                                //判断上一次的收盘情况
                                if ((tempList[i - 1].Open <= tempList[i - 1].Close) &&
                                    ((tempList[i - 1].High - tempList[i - 1].Low) / tempList[i - 1].Low < 0.02m)) //十字星 或红盘 买点 ,振幅小于2
                                {

                                    if (isBuy == false)
                                    {
                                        isBuy = true;
                                        dealInfo.Buy = tempList[i];

                                    }

                                }
                            }
                            else //继续下跌 继续观察
                            {

                            }
                        }
                    }
                }
            }

            return dealInfo;
        }


    }
}
