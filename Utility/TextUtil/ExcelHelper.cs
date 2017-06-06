using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.IO;
using System.Data;
using System.Linq;
using System.Collections;
using System.Reflection;
using System.Diagnostics;

using Aspose.Cells;

namespace NJUST.AUTO06.Utility
{
    public sealed class ExcelHelper
    {
        #region 构造函数和实例

        // 私有构造函数
        ExcelHelper()
        {
        }

        // 利用单件模式构造线程安全的独立类实例
        public static readonly ExcelHelper Instance = new ExcelHelper();

        #endregion

        /// <summary>
        /// DataGridView导出到Excel
        /// </summary>
        /// <param name="dgv">DataGridView控件</param>
        /// <param name="strTitle">导出的Excel标题</param>
        public void DataGridViewExportToExcel(DataGridView dgv, string strTitle)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.CreatePrompt = false;
            saveFileDialog.FileName = strTitle + ".xls" + System.DateTime.Now.ToString("yyyy-MM-dd");

            if (saveFileDialog.ShowDialog() == DialogResult.Cancel) // 导出时，点击【取消】按钮
            {
                return;
            }

            Stream myStream = saveFileDialog.OpenFile();
            StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));

            string strHeaderText = "";

            try
            {
                //写标题
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        strHeaderText += "\t";
                    }

                    strHeaderText += dgv.Columns[i].HeaderText;
                }

                sw.WriteLine(strHeaderText);

                //写内容
                string strItemValue = "";

                for (int j = 0; j < dgv.RowCount; j++)
                {
                    strItemValue = "";

                    for (int k = 0; k < dgv.ColumnCount; k++)
                    {
                        if (k > 0)
                        {
                            strItemValue += "\t";
                        }

                        strItemValue += dgv.Rows[j].Cells[k].Value.ToString();
                    }

                    sw.WriteLine(strItemValue); //把dgv的每一行的信息写为sw的每一行
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sw.Close();
                myStream.Close();
            }
        }

        /// <summary>
        /// 打开Excel文件导出对话框
        /// </summary>
        /// <param name="dlg">对话框对象</param>
        /// <param name="title">文件名</param>
        /// <returns></returns>
        private DialogResult OpenExcelExportDialog(SaveFileDialog dlg, string title)
        {
            // 选择保存的文件类型
            dlg.Filter = "Execl files (*.xls)|*.xls";
            dlg.FilterIndex = 0;
            dlg.RestoreDirectory = true;
            dlg.CreatePrompt = true;
            dlg.Title = "保存为Excel文件";
            // 设置名称格式
            dlg.FileName = title + System.DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
            
            // 显示保存文件对话框
            return dlg.ShowDialog();
        }

        #region 基于文件流和数据库的方法，实现DataTable和Excel的数据导入导出

        /// <summary>
        /// 将数据导出至Excel文件,将Excel作为数据库处理
        /// </summary>
        /// <param name="Table">DataTable对象</param>
        /// <param name="trTitle">导出文件名</param>
        //public bool DataTableExportToExcel3(DataTable Table, string strTitle)
        //{
        //    SaveFileDialog dlg = new SaveFileDialog();

        //    // 选择用户保存的位置
        //    if (OpenExcelExportDialog(dlg, strTitle) == DialogResult.Cancel)
        //    {
        //        return false;
        //    }

        //    string fileName = dlg.FileName;

        //    if ((Table.TableName.Trim().Length == 0) || (Table.TableName.ToLower() == "table"))
        //    {
        //        Table.TableName = "Sheet1";
        //    }

        //    //数据表的列数
        //    int ColCount = Table.Columns.Count;

        //    //用于记数，实例化参数时的序号
        //    int i = 0;

        //    //创建参数
        //    OleDbParameter[] para = new OleDbParameter[ColCount];

        //    //创建表结构的SQL语句
        //    string TableStructStr = @"Create Table " + Table.TableName + "(";

        //    //连接字符串
        //    string connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties=Excel 8.0;";
        //    OleDbConnection objConn = new OleDbConnection(connString);

        //    //创建表结构
        //    OleDbCommand objCmd = new OleDbCommand();

        //    //遍历数据表的所有列，用于创建表结构
        //    foreach (DataColumn col in Table.Columns)
        //    {
        //        //每列都以文本格式保存，防止因数据列过长而导致显示格式为科学计数法
        //        para[i] = new OleDbParameter("@" + col.ColumnName, OleDbType.VarChar);
        //        objCmd.Parameters.Add(para[i]);

        //        //如果是最后一列
        //        if (i + 1 == ColCount)
        //        {
        //            TableStructStr += col.ColumnName + " varchar)";
        //        }
        //        else
        //        {
        //            TableStructStr += col.ColumnName + " varchar,";
        //        }
        //        i++;
        //    }

        //    //创建Excel文件及文件结构
        //    try
        //    {
        //        objCmd.Connection = objConn;
        //        objCmd.CommandText = TableStructStr;

        //        if (objConn.State == ConnectionState.Closed)
        //        {
        //            objConn.Open();
        //        }
        //        objCmd.ExecuteNonQuery();
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }

        //    //插入记录的SQL语句
        //    string InsertSql_1 = "Insert into " + Table.TableName + " (";
        //    string InsertSql_2 = " Values (";
        //    string InsertSql = "";

        //    //遍历所有列，用于插入记录，在此创建插入记录的SQL语句
        //    for (int colID = 0; colID < ColCount; colID++)
        //    {
        //        if (colID + 1 == ColCount)  //最后一列
        //        {
        //            InsertSql_1 += Table.Columns[colID].ColumnName + ")";
        //            InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ")";
        //        }
        //        else
        //        {
        //            InsertSql_1 += Table.Columns[colID].ColumnName + ",";
        //            InsertSql_2 += "@" + Table.Columns[colID].ColumnName + ",";
        //        }
        //    }

        //    InsertSql = InsertSql_1 + InsertSql_2;

        //    //遍历数据表的所有数据行
        //    for (int rowID = 0; rowID < Table.Rows.Count; rowID++)
        //    {
        //        for (int colID = 0; colID < ColCount; colID++)
        //        {
        //            if (para[colID].DbType == DbType.Double && Table.Rows[rowID][colID].ToString().Trim() == "")
        //            {
        //                para[colID].Value = 0;
        //            }
        //            else
        //            {
        //                para[colID].Value = Table.Rows[rowID][colID].ToString().Trim();
        //            }
        //        }
        //        try
        //        {
        //            objCmd.CommandText = InsertSql;
        //            objCmd.ExecuteNonQuery();
        //        }
        //        catch (Exception exp)
        //        {
        //            string str = exp.Message;
        //        }
        //    }
        //    try
        //    {
        //        if (objConn.State == ConnectionState.Open)
        //        {
        //            objConn.Close();
        //        }
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }

        //    return true;
        //}

        /// <summary>
        /// DataTable数据导出到Excel，基于流的实现
        /// </summary>
        /// <param name="dtNew">数据表</param>
        /// <param name="strTitle">Excel标题</param>
        /// <returns></returns>
        //public int DataTableExportToExcel2(DataTable dtNew, string strTitle)
        //{
        //    SaveFileDialog dlg = new SaveFileDialog();

        //    // 选择用户保存的位置
        //    if (OpenExcelExportDialog(dlg, strTitle) == DialogResult.Cancel)
        //    {
        //        return -1;
        //    }

        //    string fileName = dlg.FileName;

        //    // 对应保存文件的流
        //    Stream myStream;

        //    try
        //    {
        //        // 关联到指定文件
        //        myStream = dlg.OpenFile();
        //    }
        //    catch (Exception)
        //    {
        //        MessageUtil.ShowWarning("文件打开错误！");
        //        return -1;
        //    }

        //    // 定义对流的Writer
        //    StreamWriter sw = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(-0));
        //    string columnTitle = "";

        //    try
        //    {
        //        // 写入列标题   
        //        for (int i = 0; i < dtNew.Columns.Count; i++)
        //        {
        //            if (i > 0)
        //            {
        //                columnTitle += "\t";
        //            }

        //            columnTitle += dtNew.Columns[i].ColumnName;
        //        }

        //        // 写入标题行
        //        sw.WriteLine(columnTitle);

        //        // 成功导出到Excel的记录数
        //        int mCount = 0;

        //        // 写入列内容   
        //        for (int j = 0; j < dtNew.Rows.Count; j++)
        //        {
        //            mCount++;

        //            string columnValue = "";
        //            for (int k = 0; k < dtNew.Columns.Count; k++)
        //            {
        //                if (k > 0)
        //                {
        //                    columnValue += "\t";
        //                }

        //                if (dtNew.Rows[j][k].ToString() == "")
        //                {
        //                    columnValue += "";
        //                }
        //                else
        //                {
        //                    columnValue += dtNew.Rows[j][k].ToString().Trim();
        //                }
        //            }

        //            sw.WriteLine(columnValue);
        //        }

        //        sw.Close();
        //        myStream.Close();

        //        return mCount;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        /// <summary>
        /// 将Excel文件内容读入数据表，使用数据库方式
        /// </summary>
        /// <returns>数据表</returns>
        //public DataTable OpenExcelAsDataTable(string filePath)
        //{
        //    if (string.IsNullOrEmpty(filePath)) return null;

        //    // 将Excel文件作为数据库模式打开 
        //    // 导入时包含Excel中的第一行数据，并且将数字和字符混合的单元格视为文本进行导入    
        //    string strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = {0};Extended Properties ='Excel 8.0;HDR=yes;IMEX=1;'", filePath);

        //    string extension = Path.GetExtension(filePath);
        //    switch (extension)
        //    {
        //        case ".xls":
        //            strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = {0};Extended Properties ='Excel 8.0;HDR=yes;IMEX=1;'", filePath);
        //            break;
        //        case ".xlsx":
        //            strConn = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source = {0};Extended Properties ='Excel 12.0;HDR=yes;IMEX=1;'", filePath);
        //            break;
        //        default:
        //            strConn = string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source = {0};Extended Properties ='Excel 8.0;HDR=yes;IMEX=1;'", filePath);
        //            break;
        //    }

        //    // 建立数据库连接
        //    OleDbConnection conn = new OleDbConnection(strConn);
        //    try
        //    {
        //        conn.Open();

        //        // 利用Adapter从数据库提取数据
        //        DataTable dtSheetName = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "Table" });
        //        // 包含excel中表名的字符串数组
        //        string[] strTableNames = new string[dtSheetName.Rows.Count];
        //        for (int k = 0; k < dtSheetName.Rows.Count; k++)
        //        {
        //            strTableNames[k] = dtSheetName.Rows[k]["TABLE_NAME"].ToString();
        //        }

        //        OleDbDataAdapter myCommand = new OleDbDataAdapter("select * from [" + strTableNames[0] + "]", conn);

        //        // 根据路径打开一个Excel文件并将数据填充到DataSet中
        //        DataSet ds = new DataSet();
        //        myCommand.Fill(ds, "table1");
        //        return ds.Tables[0];
        //    }
        //    catch (Exception)
        //    {
        //        MessageUtil.ShowWarning("打开出错，请关闭文件后重试！");
        //        return null;
        //    }
        //}

        #endregion

        /// <summary>
        /// DataTable数据导出到Excel，基于Apose.Cell功能组件实现 
        /// </summary>
        /// <param name="dtNew">数据表</param>
        /// <param name="strTitle">Excel标题</param>
        /// <returns></returns>      
        public void DataTableExportToExcel(DataTable dt, string strTitle)
        {
            SaveFileDialog dlg = new SaveFileDialog();

            // 选择用户保存的位置
            if (OpenExcelExportDialog(dlg, strTitle) == DialogResult.Cancel)
            {
                return;
            }

            string fileName = dlg.FileName;

            try
            {
                // Excel的一页
                Workbook book = new Workbook();
                // Excel一页中的Sheet
                Worksheet sheet = book.Worksheets[0];
                sheet.Name = strTitle;
                // Excel的单元格
                Cells cells = sheet.Cells;

                int Colnum = dt.Columns.Count; // 表格列数 
                int Rownum = dt.Rows.Count;    // 表格行数 

                // 生成行列名
                for (int i = 0; i < Colnum; i++)
                {
                    if (!string.IsNullOrEmpty(dt.Columns[i].Caption))
                    {
                        cells[0, i].PutValue(dt.Columns[i].Caption);
                    }      
                    else
                    {
                        cells[0, i].PutValue(dt.Columns[i].ColumnName);
                    }                       
                }

                // 生成数据行 
                for (int i = 0; i < Rownum; i++)
                {
                    for (int k = 0; k < Colnum; k++)
                    {
                        cells[1 + i, k].PutValue(dt.Rows[i][k].ToString());
                    }
                }

                // 保存Excel文件
                book.Save(fileName);

                if (MessageUtil.ShowYesNoAndTips("导出成功，是否打开文件？") == System.Windows.Forms.DialogResult.Yes)
                {
                    Process.Start(fileName);
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.ToString());
            }
        }

        /// <summary>
        /// 将Excel文件内容读入数据表，使用Apose.Cells组件
        /// </summary>
        /// <returns>数据表</returns>
        public DataTable OpenExcelAsDataTable(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) return null;

            // 检查扩展名
            string extension = Path.GetExtension(filePath);
            if((extension != ".xls") && (extension != ".xlsx"))
            {
                throw new Exception("不正确的Excel文件类型！");
            }

            Workbook workbook = new Workbook(filePath);
            Cells cells = workbook.Worksheets[0].Cells;

            // 把Excel全部内容包括标题栏导入DataTable
            return cells.ExportDataTableAsString(0, 0, cells.MaxDataRow, cells.MaxDataColumn, true);
        }

        /// <summary>
        /// 查找指定字段内容相同的记录
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="sortField">字段名</param>
        public ArrayList FindRepeatedRecords(DataTable dt, string sortField)
        {
            // 需要添加引用system.data,datasetexpansion
            // 按档案编号分组，获得重复项;Linq可以有效缩短处理时间
            if (!dt.Columns.Contains(sortField))
            {
                throw new Exception("导入表格中不含“档案编号”列，请检查表格格式！");
            }
            var result = from q in dt.AsEnumerable()                     // 表按照枚举格式访问
                         //group q by q.Field<T>(sortField) into r
                         // 按“档案编号”分组
                         group q by q[sortField] into r
                         where r.Count() > 1                             // 分组中元素个数大于1（有重复记录）
                         select r.Key;                                   // 提取主键（档案编号）     

            ArrayList repeatedItems = new ArrayList();
            repeatedItems.Clear();
            repeatedItems.AddRange(result.ToArray());

            return repeatedItems;
        }

        /// <summary>
        /// 过滤空记录（key为空）
        /// </summary>
        /// <param name="dt">过滤空记录后的数据表</param>
        public DataTable FilterNullRecords(DataTable dt)
        {
            if (dt == null) return null;

            // 筛选出档案编号不为空的记录
            // dataTable中只能通过这种方式判断列值为空(Row.ItemArray[index]!=null没用)
            var result = dt.AsEnumerable().Where(q => !q.IsNull(0));
            // 得到筛选结果的自定义视图
            DataView dv = result.AsDataView();

            // 视图转换为数据表
            return dv.ToTable();
        }

        /// <summary>
        /// 数据表转换为List
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="dt">数据表</param>
        /// <returns>实体对象List</returns>
        public List<T> TableToEntity<T>(DataTable dt) where T : class,new()
        {
            Type type = typeof(T);            // 对象类型
            List<T> list = new List<T>();        // 对象LIST

            // 遍历表的所有记录
            foreach (DataRow row in dt.Rows)
            {
                // 获得对象的属性集
                PropertyInfo[] pi = type.GetProperties();
                // 生成对象实例
                T entity = new T();

                // 遍历对象的属性
                foreach (PropertyInfo p in pi)
                {
                    // 如果对象属性值是整数
                    if (row[p.Name] is Int64)
                    {
                        // 设置对象实例中对应该属性的值
                        p.SetValue(entity, Convert.ToInt32(row[p.Name]), null);
                        continue;
                    }

                    // 设置对象实例中对应该属性的值
                    p.SetValue(entity, row[p.Name], null);
                }

                // 添加对象集合
                list.Add(entity);
            }

            return list;
        }

        /// <summary>
        /// 集合转换成数据表
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="collection">集合类</param>
        /// <returns>数据表</returns>
        public DataTable ToDataTable<T>(IEnumerable<T> collection)
        {
            PropertyInfo[] pis = typeof(T).GetProperties();
            var dt = new DataTable();

            // 生成表头
            dt.Columns.AddRange(pis.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

            // 加入数据
            if (collection.Count() > 0)
            {
                // 遍历数据集
                for (int i = 0; i < collection.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();

                    // 遍历实体对象属性
                    foreach (PropertyInfo pi in pis)
                    {
                        object obj = pi.GetValue(collection.ElementAt(i), null);
                        tempList.Add(obj);
                    }

                    object[] array = tempList.ToArray();

                    // 加载一行数据到数据表
                    dt.LoadDataRow(array, true);
                }
            }

            return dt;
        }
    }
}
