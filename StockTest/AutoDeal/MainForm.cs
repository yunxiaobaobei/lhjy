using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Serilog;
using System.Threading;

namespace AutoDeal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        AutoClickDeal autoClickDeal = AutoClickDeal.GetInstance();

        GetRealTimeData realTimeData = new GetRealTimeData();

        Strategic_one strategic_One = new Strategic_one();

        private void MainForm_Load(object sender, EventArgs e)
        {

            Log.Logger = new LoggerConfiguration()
          .WriteTo.Console()
          .WriteTo.Async(w => w.File("Logs/log-.log", rollingInterval: RollingInterval.Day))
          .MinimumLevel.Debug()
          .CreateLogger();


            //加载json文件





            //添加股票池
            realTimeData.stockList.Add("sz300064");
            realTimeData.stockList.Add("sh601778");

        }



        private void btn_start_Click(object sender, EventArgs e)
        {

            IntPtr mainHandle = WinAPI.FindWindow(null, "专业版下单");


            if (mainHandle == IntPtr.Zero)
            {
                MessageBox.Show("请先打开交易软件");
           
                return;
            }


            //启动获取数据的线程
            realTimeData.StartGetRealDate();
          
            //启动交易
            autoClickDeal.StartDeal();


            btn_start.Enabled = false;

        }

        /// <summary>
        /// 测试点击流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_testClick_Click(object sender, EventArgs e)
        {
            //启动交易
            autoClickDeal.StartDeal();

            ThreadPool.QueueUserWorkItem( (o) =>
            {
                while (true)
                {
                    Thread.Sleep(5000);
                    autoClickDeal.TraggleDeal("300364", DealType.buy, new Skender.Stock.Indicators.Quote());
                }

            }, null);

        }

        private void btn_testGetData_Click(object sender, EventArgs e)
        {
            //realTimeData.StartGetRealDate();
        }

        private void btn_testOldData_Click(object sender, EventArgs e)
        {
            realTimeData.TestStartGetRealDate();

            //启动交易
            autoClickDeal.StartDeal();
        }
    }
}
