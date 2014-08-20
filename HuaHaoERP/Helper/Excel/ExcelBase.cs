using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using xls = Microsoft.Office.Interop.Excel;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace HuaHaoERP.Helper.Excel
{
    class ExcelBase
    {
        xls.Application xlApp;
        xls.Workbook xlWorkBook;
        xls.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        public ExcelBase()
        {
            try
            {
                xlApp = new xls.Application();
            }
            catch (Exception)
            {
                Console.WriteLine("想不到EXCEL");
            }

        }

        public void ExcelTest()
        {
            CreateExcelFile();
        }
        private void CreateExcelFile()
        {
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (xls.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            xlWorkBook.SaveAs(Properties.Settings.Default.Path + "csharp-Excel.xls", xls.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, xls.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }
        private void ReadExcelFile(string FilePath)
        {
            xlWorkBook = xlApp.Workbooks.Open(FilePath);
            xlWorkSheet = (xls.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            releaseObject(xlWorkSheet);
            releaseObject(xlWorkBook);
            releaseObject(xlApp);
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Exception Occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
