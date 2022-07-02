using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace StockTest
{
    public class ColorConfig
    {
        #region 颜色配置
        public Color BuyInColor { get; set; }

        public Color SellOutColor { get; set; }

        public Color HeigVolumnColor { get; set; }

        public Color LowVolumnColor { get; set; }

        public Color EnterPointColor { get; set; }

        public Color ExitPointColor { get; set; }
        #endregion


        #region 是否启用相关颜色
        public bool AbleBuy { get; set; }


        public bool AbleSell{ get; set; }

        public bool AbleHeigVolumn { get; set; }

        public bool AbleLowVolumn { get; set; }

        public bool AbleEnterPoint { get; set; }

        public bool AbleExitPoint { get; set; }
        #endregion
    }
}
