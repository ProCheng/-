using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.Data;
using NPOI.HSSF.Util;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;

namespace common
{
    /*2020年10月31日 11点29分*/
    public static class ToExcel 
    {
        //将table数据转换为excel
        public static IWorkbook Excel(DataTable dataTable)
        {
            //创建工作薄
            IWorkbook workbook = new HSSFWorkbook();

            ICellStyle style = workbook.CreateCellStyle();
            ICellStyle styletitle = workbook.CreateCellStyle();

            style.Alignment = HorizontalAlignment.Center;
            styletitle.Alignment = HorizontalAlignment.Center;
            styletitle.FillPattern = FillPattern.SolidForeground;
            styletitle.FillForegroundColor = HSSFColor.Aqua.Index;

            //设置标题字体
            HSSFFont hSSFFont = (HSSFFont)workbook.CreateFont();
            hSSFFont.IsBold = true;
            styletitle.SetFont(hSSFFont);
            
            //创建工作表
            ISheet sheet = workbook.CreateSheet("no1");
            IRow row = null;
            ICell cell = null;

            //写入标题
            row = sheet.CreateRow(0);
            for (var i = 0; i < dataTable.Columns.Count; i++)
            {
                cell = row.CreateCell(i);
                cell.SetCellValue(dataTable.Columns[i].ColumnName);
                cell.CellStyle = style;
                cell.CellStyle = styletitle;
            }

            //写入内容
            List<int> len = new List<int>();               //每列最长的宽度
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                row = sheet.CreateRow(i+1); //因为第一行是标题，所以应该是i+1，从第二行开始
                for(var j = 0; j < dataTable.Columns.Count; j++)
                {
                    if (len.Count<j+1)
                    {
                        len.Add(1);
                    }
                    cell = row.CreateCell(j);
                    cell.SetCellValue(dataTable.Rows[i][j].ToString());
                    cell.CellStyle = style;
                    if (cell.ToString().Length > len[j])
                    {
                        //内容自适应宽度
                        

                        System.Text.RegularExpressions.Regex rex =
                        new System.Text.RegularExpressions.Regex(@"^\d+$");
                        if (rex.IsMatch(cell.ToString()))
                        {
                            len[j] = (int)Math.Ceiling(cell.ToString().Length * 1.3);
                        }
                        else
                        {
                            len[j] = (int)Math.Ceiling(cell.ToString().Length * 2.3);
                        }


                    }

                   
                    /*if (j == 1)
                        sheet.SetColumnWidth(j, 10 * 256);
                    else
                        sheet.SetColumnWidth(j, len * s * 256);*/
                }
                //len = 1;
                
            }
           
            for(var i = 0; i < len.Count-1; i++)
            {
                if (i == 1||i==2)
                    sheet.SetColumnWidth(i, len[i] * 8 * 256);
                else
                sheet.SetColumnWidth(i,len[i]*256);
            }
            //时间必要特殊也需要特殊处理宽度
            sheet.SetColumnWidth(len.Count-1, 22 * 256);

            return workbook;
        }
        //将excel转换为table数据
        public static DataTable Table(string path)
        {
            IWorkbook workbook= null;
            DataTable table = new DataTable() ;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    if (Path.GetExtension(path) == ".xlsx")
                    {
                        workbook = new XSSFWorkbook(fs);
                    }
                    else
                    {
                        workbook = new HSSFWorkbook(fs);
                    }
                    ISheet sheet = workbook.GetSheetAt(0);

                  
                    for(var i = 0; i < sheet.GetRow(0).LastCellNum; i++)
                    {
                        table.Columns.Add(new DataColumn());
                    }
                    for(var i = 1; i <= sheet.LastRowNum; i++)
                    {
                        DataRow dataRow = table.NewRow();
                        IRow row = sheet.GetRow(i);
                        for(var j=0; j < row.LastCellNum; j++)
                        {
                            ICell cell = row.GetCell(j);
                            switch (cell.CellType)
                            {
                                case CellType.Unknown:
                                    break;
                                case CellType.Numeric:
                                    if (DateUtil.IsCellInternalDateFormatted(cell))
                                        dataRow[j] = cell.DateCellValue.ToString("yyyy/MM/dd");
                                    else
                                        dataRow[j] = cell.NumericCellValue;
                                    break;
                                case CellType.String:
                                    dataRow[j] = cell.StringCellValue;
                                    break;
                                case CellType.Blank:
                                    dataRow[j] = "";
                                    break;
                                default:
                                    throw new Exception("表格的格式错误");
                            }
                        }

                        table.Rows.Add(dataRow);
                    } 
                    
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // File.Delete(path);
            }
            return table;
        }
    }
}
