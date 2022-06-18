using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using Skender.Stock.Indicators;

namespace 历史数据分析
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        DataTable dt = new DataTable();

        private void Form1_Load(object sender, EventArgs e)
        {



        }


        /// <summary>
        /// 获取历史数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_getHistoryData_Click(object sender, EventArgs e)
        {
            dt = OperExcel.ExcelToDataTable("D:/个人空间/LHJY/历史数据存储/300087-05mouth.csv", "300087-05mouth");

            dataGridView1.DataSource = dt;

            List<Quote> list  = new List<Quote>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Quote s = new Quote();
                
                s.Volume = Decimal.Parse( dt.Rows[i]["volume"].ToString());
                s.Open = Decimal.Parse(dt.Rows[i]["open"].ToString());
                s.Close = Decimal.Parse(dt.Rows[i]["close"].ToString());
                s.High = Decimal.Parse(dt.Rows[i]["high"].ToString());
                s.Low = Decimal.Parse(dt.Rows[i]["low"].ToString());
                s.Date = (DateTime)dt.Rows[i]["date"];

                list.Add(s);
            }

            IEnumerable<Quote> quotes = list;

            IEnumerable<SmaResult> results = quotes.GetSma(20);

            // use results as needed for your use case (example only)
            foreach (SmaResult r in results)
            {
                Console.WriteLine($"SMA on {r.Date:d} was ${r.Sma:N4}");
            }
        }
    }
}
