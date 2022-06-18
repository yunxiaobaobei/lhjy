﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Data.OleDb;


namespace StockTest
{
    public static class Common
    {
        public static string ConvertJsonString(string str)
        {
            try
            {
                //格式化json字符串
                JsonSerializer serializer = new JsonSerializer();
                TextReader tr = new StringReader(str);
                JsonTextReader jtr = new JsonTextReader(tr);
                object obj = serializer.Deserialize(jtr);
                if (obj != null)
                {
                    StringWriter textWriter = new StringWriter();
                    JsonTextWriter jsonWriter = new JsonTextWriter(textWriter)
                    {
                        Formatting = Formatting.Indented,
                        Indentation = 4,
                        IndentChar = ' '
                    };
                    serializer.Serialize(jsonWriter, obj);
                    return textWriter.ToString();
                }

                return str;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        
        /// <summary>
        /// 返回指定时间的时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static long GetTimeStamp(DateTime dt)
        {
            DateTime dateStart = new DateTime(1970, 1, 1, 8, 0, 0);
            long timestamp =Convert.ToInt32((dt -dateStart).TotalSeconds);

            return timestamp;
        }

        public static DateTime GetDateTime(long timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = timeStamp * 10000;
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime targetDT = dtStart.Add(toNow);

            return targetDT;
        }


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


    }
}
