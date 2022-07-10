using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using System.Data.OleDb;

namespace AutoDeal
{
    public static class Common
    {

        //    这就是将文件放入datatable的方法
        public static DataTable ExcelToDataTable(string pathName, string sheetName)
        {
            try
            {
                int symbol = 0;
                DataTable tbContainer = new DataTable();
                string strConn = string.Empty; //创建一个空的 string（字段）对象 strConn；
                if (string.IsNullOrEmpty(sheetName)) { sheetName = "Sheet1"; }
                FileInfo file = new FileInfo(pathName);
                if (!file.Exists) { throw new Exception("文件不存在"); }
                string extension = file.Extension;
                switch (extension)
                {
                    case ".xls":
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        break;
                    case ".xlsx":
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        //strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + pathName + ";Extended Properties=\"Excel 8.0;HDR=YES;IMEX=1\"";
                        break;
                    case ".CSV":
                        strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sheetName + ";Extended Properties='Excel 8.0;FMT=Delimited;HDR=YES;'";
                        symbol = 1;
                        break;
                    default:
                        strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathName + ";Extended Properties='Excel 8.0;HDR=Yes;IMEX=1;'";
                        break;
                }
                //链接Excel
                OleDbConnection cnnxls = new OleDbConnection(strConn);

                if (symbol == 0)
                {
                    OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}$]", sheetName), cnnxls);

                    DataSet ds = new DataSet();
                    //将Excel里面有表内容装载到内存表中！
                    oda.Fill(tbContainer);
                }
                else
                {
                    OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from " + pathName, strConn); ;


                    DataSet ds = new DataSet();
                    myCommand.Fill(tbContainer);

                }
                //读取Excel里面有 表Sheet1
                //OleDbDataAdapter oda = new OleDbDataAdapter(string.Format("select * from [{0}$]", sheetName), cnnxls);

                //DataSet ds = new DataSet();
                //将Excel里面有表内容装载到内存表中！
                //oda.Fill(tbContainer);
                return tbContainer;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return null;
            }
        }

    }
}
