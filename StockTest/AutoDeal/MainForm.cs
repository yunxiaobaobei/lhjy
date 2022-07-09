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
namespace AutoDeal
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        AutoClickDeal autoClickDeal = new AutoClickDeal();

        private void MainForm_Load(object sender, EventArgs e)
        {

            Log.Logger = new LoggerConfiguration()
          .WriteTo.Console()
          .WriteTo.Async(w => w.File("Logs/log-.log", rollingInterval: RollingInterval.Day))
          .MinimumLevel.Debug()
          .CreateLogger();


            //启动获取数据的线程


            //启动分析模型

        }



        private void btn_start_Click(object sender, EventArgs e)
        {

           
            //启动交易
            autoClickDeal.StartDeal();


        }
    }
}
