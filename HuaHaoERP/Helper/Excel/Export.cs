using HuaHaoERP.Model.Warehouse;
using System;
using System.Collections.Generic;
using xls = Microsoft.Office.Interop.Excel;

namespace HuaHaoERP.Helper.Excel
{
    class Export
    {
        public void ExportData(List<WarehouseProductNumModel> dn, bool isPrint)
        {
            try
            {

                xls.Application xlApp = new xls.Application();
                xls.Workbook xlWorkBook = xlApp.Workbooks.Add(true);
                xls.Worksheet xlWorkSheet = (xls.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                //这里设置行高
                // ((xls.Range)xlWorkSheet.Rows).RowHeight = 11;
                xlWorkSheet.PageSetup.TopMargin = xlApp.InchesToPoints(0.19685);
                xlWorkSheet.PageSetup.BottomMargin = xlApp.InchesToPoints(0.19685);
                xlWorkSheet.PageSetup.LeftMargin = xlApp.InchesToPoints(0.19685);
                xlWorkSheet.PageSetup.RightMargin = xlApp.InchesToPoints(0.19685);
                xlWorkSheet.PageSetup.HeaderMargin = xlApp.InchesToPoints(0.19685);
                xlWorkSheet.PageSetup.FooterMargin = xlApp.InchesToPoints(0.19685);
                //xlWorkSheet.Columns.AutoFit();
                xlWorkSheet.Cells.Font.Size = 10;
                ((xls.Range)xlWorkSheet.Cells[1, 2]).HorizontalAlignment = xls.XlVAlign.xlVAlignCenter;
                xlWorkSheet.Cells[1, 2] = "库存情况清单";

                xlWorkSheet.Cells[2, 1] = "'序号";
                xlWorkSheet.Cells[2, 2] = "'编号";
                xlWorkSheet.Cells[2, 3] = "'名称";
                xlWorkSheet.Cells[2, 4] = "'数量";
                xlWorkSheet.Cells[2, 5] = "'包装数";

                int rowid = 3;
                foreach (WarehouseProductNumModel m in dn)
                {
                    xlWorkSheet.Cells[rowid, 1] = "'" + (rowid - 1);
                    xlWorkSheet.Cells[rowid, 2] = "'" + m.ProductNumber;
                    xlWorkSheet.Cells[rowid, 3] = "'" + m.ProductName;
                    xlWorkSheet.Cells[rowid, 4] = "'" + m.Quantity;
                    xlWorkSheet.Cells[rowid, 5] = "'" + m.PackageNum;
                    rowid++;
                }
                xlApp.Visible = true;
                if(isPrint)
                {
                    xlWorkSheet.PrintPreview();
                }

                releaseObject(xlWorkSheet);
                releaseObject(xlWorkBook);
                releaseObject(xlApp);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
