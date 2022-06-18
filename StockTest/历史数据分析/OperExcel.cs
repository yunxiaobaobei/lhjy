using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.Windows.Forms;
using System.IO;

namespace 历史数据分析
{
    public static class OperExcel
    {
        ///数据返回到DataSet


        //    这就是将文件放入datatable的方法
        public static DataTable ExcelToDataTable(string pathName, string sheetName)
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
                    strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + sheetName + ";Extended Properties='Text;FMT=Delimited;HDR=YES;'";
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


        public static DataSet ToDataSet(string filePath)
        {
            string connStr = "" ;

            string fileType = System.IO.Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(fileType)) return null;

            if (fileType.ToLower() == ".xls")
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + filePath + ";" + ";Extended Properties=\"Excel 8.0; HDR = YES; IMEX = 1; \"";
            }
            else if (fileType.ToLower() == ".xlsx")
            {
                connStr = "Provider=Microsoft.ACE.OLEDB.12.0;" + "Data Source=" + filePath + ";" + ";Extended Properties=\"Excel 12.0; HDR = YES; IMEX = 1; \"";
            }
            else if (fileType.ToLower() == ".csv")
            {
                connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath.Remove(filePath.LastIndexOf("\\") + 1) + "; Extended Properties = 'Text;FMT=Delimited;HDR=YES;'";
            }
            else
            {
                MessageBox.Show("文件格式不不符合要求，此系统只支持导入xls，xlsx，csv 三种格式，详情咨询软件供应商");
            }
            string sql_F = "Select * FROM [{0}]";

            OleDbConnection conn = new OleDbConnection();
            OleDbDataAdapter da = null;
            DataTable dtSheetName = null;

            DataSet ds = new DataSet();
            try
            {
                conn.ConnectionString = connStr;
                conn.Open();
                string SheetName = "";
                dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

                da = new OleDbDataAdapter();
                for (int i = 0; i < dtSheetName.Rows.Count; i++)
                {
                    SheetName = Convert.ToString(dtSheetName.Rows[i]["TABLE_NAME"]);

                    if (SheetName.Contains("$") && !SheetName.Replace("'", "").EndsWith("$"))
                    {
                        continue;
                    }
                    da.SelectCommand = new OleDbCommand(String.Format(sql_F, SheetName), conn);
                    DataSet dsItem = new DataSet();
                    da.Fill(ds, "Mdt1");
                    DataTable table = ds.Tables["Mdt1"];
                    DataRow row = table.NewRow();
                    da.Update(ds, "Mdt1");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                // 关闭连接
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                    da.Dispose();
                    conn.Dispose();
                }
            }
            return ds;
        }

    }
}
