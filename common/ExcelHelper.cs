using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;

namespace QN._115.Common
{
    public class ExcelHelper
    {
        public static IWorkbook DataTableToExcel(DataTable table)
        {
            //创建工作簿
            IWorkbook workbook = new XSSFWorkbook();
            //创建工作簿里的表
            ISheet sheet = workbook.CreateSheet(); ;
            //创建表里的行
            IRow row = null;
            //创建行里的列
            ICell cell = null;
            ICellStyle style = workbook.CreateCellStyle();
            style.BorderTop = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.Alignment = HorizontalAlignment.Center;
            try
            {
                //创建内存流  保存excel
                MemoryStream ms = new MemoryStream();
                //创建一个excel表的一行 放的是标题列
                row = sheet.CreateRow(0);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    //创建单元格
                    cell = row.CreateCell(i);
                    //给单元格进行赋值
                    cell.SetCellValue(table.Columns[i].ColumnName);
                    cell.CellStyle = style;
                    row.Cells.Add(cell);

                }
                //从excel表格的第二行开始
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    //在excel中创建一行
                    row = sheet.CreateRow(i + 1);
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        //创建单元格
                        cell = row.CreateCell(j);
                        //给单元格进行赋值
                        cell.SetCellValue(table.Rows[i][j].ToString());
                        cell.CellStyle = style;
                        row.Cells.Add(cell);
                    }
                }
                //将工作簿放入内存流中
                //workbook.Write(ms);
                return workbook;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataTable ExcelToDataTable(string path)
        {
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            DataTable table = new DataTable();
            DataRow dataRow = null;
            try
            {
                using (FileStream fs = File.OpenRead(path))
                {
                    if (Path.GetExtension(path) == ".xlsx")
                    {
                        workbook = new XSSFWorkbook(fs);
                    }
                    else  //xls
                    {
                        workbook = new HSSFWorkbook(fs);
                    }
                    sheet = workbook.GetSheetAt(0);
                    for (int i = 0; i < sheet.GetRow(0).LastCellNum; i++)
                    {
                        table.Columns.Add(new DataColumn());
                    }
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        dataRow = table.NewRow();
                        row = sheet.GetRow(i);
                        for (int j = 0; j < row.LastCellNum; j++)
                        {
                            cell = row.GetCell(j);
                            
                            switch (cell.CellType)
                            {
                                case CellType.Numeric:
                                    if (DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = cell.DateCellValue.ToString("yyyy/MM/dd");
                                    }
                                    else
                                    {
                                        dataRow[j] = cell.NumericCellValue;
                                    }
                                    break;
                                case CellType.String:
                                    dataRow[j] = cell.StringCellValue;
                                    break;
                                case CellType.Blank:
                                    dataRow[j] = "";
                                    break;
                                default:
                                    throw new Exception("格式错误");
                            }
                        }
                        table.Rows.Add(dataRow);
                    }
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                  
                File.Delete(path);
            }
        }
    }
}
