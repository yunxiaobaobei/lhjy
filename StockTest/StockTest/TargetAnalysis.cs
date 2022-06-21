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

        private void TargetAnalysis_Load(object sender, EventArgs e)
        {

           
            

        }


        private void btn_getHistory_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string dataFile = openFileDialog.FileName;
                string cardName = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                dt = Common.ExcelToDataTable(dataFile, cardName);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Quote s = new Quote();

                    s.Volume = Decimal.Parse(dt.Rows[i]["volume"].ToString());
                    s.Open = Decimal.Parse(dt.Rows[i]["open"].ToString());
                    s.Close = Decimal.Parse(dt.Rows[i]["close"].ToString());
                    s.High = Decimal.Parse(dt.Rows[i]["high"].ToString());
                    s.Low = Decimal.Parse(dt.Rows[i]["low"].ToString());
                    s.Date = (DateTime)dt.Rows[i]["date"];
                    quoteList.Add(s);
                }

                //IEnumerable<Quote> quotes = quoteList;
                //IEnumerable<SmaResult> results = quotes.GetSma(20);

                UserControl1 klineControl = new UserControl1(quoteList);
                klineControl.Dock = DockStyle.Fill;
                panel_Kline.Controls.Add(klineControl);

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



    
    }
}
