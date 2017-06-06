using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;

using AutoCabinet2017.Helper;
using ZY.EntityFrameWork.Core.Model.Dto;

namespace AutoCabinet2017.Helper
{
    public sealed class LinqHelper
    {
        // 私有构造函数
        private LinqHelper()
        { }

        // 单例模式的类对象
        public static readonly LinqHelper Instance = new LinqHelper();

        /// <summary>
        /// 根据字段显示名查找在数据库表的本名
        /// </summary>
        /// <param name="showName">显示名</param>
        /// <returns>本名</returns>
        public string GetFieldRawName(string showName)
        {
            if (PropertyHelper.FieldCfgItems == null)
            {              
                throw new Exception("目标数据集为空!");              
            }

            // 在字段配置表中检索指定字段
            List<FieldCfgDto> arvFieldItems = PropertyHelper.FieldCfgItems.Where<FieldCfgDto>(item => item.FieldShowName == showName).ToList<FieldCfgDto>();

            if (arvFieldItems.Count == 0)
            {
                throw new Exception("没有在目标数据集中检索到设定条目!");
            }

            if (arvFieldItems.Count > 1)
            {
                throw new Exception("满足的条件设定条目在目标数据集中不唯一!");
            }

            return arvFieldItems[0].FieldName.ToString();
        }

        /// <summary>
        /// 根据字段本名查找显示名
        /// </summary>
        /// <param name="showName">显示名</param>
        /// <returns>本名</returns>
        public string GetFieldShowName(string rawName)
        {
            if (PropertyHelper.FieldCfgItems == null)
            {
                throw new Exception("目标数据集为空!");
            }

            // 在字段配置表中检索指定字段
            List<FieldCfgDto> arvFieldItems = PropertyHelper.FieldCfgItems.Where<FieldCfgDto>(item => item.FieldName == rawName).ToList<FieldCfgDto>();

            if (arvFieldItems.Count == 0)
            {
                throw new Exception("没有在目标数据集中检索到设定条目!");
            }

            if (arvFieldItems.Count > 1)
            {
                throw new Exception("满足的条件设定条目在目标数据集中不唯一!");
            }

            return arvFieldItems[0].FieldShowName.ToString();
        }

        /// <summary>
        /// 根据字段类型在明细表中检索所有内容
        /// </summary>
        /// <param name="fieldType"></param>
        /// <returns></returns>
        public List<DataDictDto> GetFieldContentList(string fieldType)
        {
            if (PropertyHelper.DataDictItems == null)
            {
                throw new Exception("目标数据集为空!");
            }

            // 检索字段明细
            List<DataDictDto> items = null;//PropertyHelper.DataDictItems.Where<DataDictDto>(item => item.FieldTypeName == fieldType)
            //                                                         .OrderBy(item => item.FieldTypeSerialNo)
            //                                                         .ToList<DataDictDto>();

            if (items.Count == 0)
            {
                throw new Exception(string.Format("没有检索到设定条目:{0}!", fieldType));
            }

            return items;
        }

        /// <summary>
        /// 按关键字过滤数据集
        /// </summary>
        /// <typeparam name="T">数据集对象的泛型类型</typeparam>
        /// <param name="dataList">数据集</param>
        /// <param name="key">查询关键字</param>
        /// <param name="keyValue">查询内容</param>
        /// <param name="queryMode">模糊/精确查询</param>
        /// <returns>绑定数据集</returns>
        public BindingList<T> GetQueryList<T>(BindingList<T> dataList, string key, string keyValue, string queryMode)
        {
            BindingList<T> queryList = new BindingList<T>(); // 查询结果
            List<T> tempList         = new List<T>();        // 保存查询结果List

            if (queryMode == "模糊查询")
            {
                // 模糊查找
                tempList = dataList.Where(item => item.GetType().GetProperty(key)
                                   .GetValue(item, null) != null &&
                                   item.GetType().GetProperty(key)
                                   .GetValue(item, null).ToString()
                                   .Contains(keyValue)).ToList();
            }
            else
            {
                // 精确查找
                tempList = dataList.Where(item =>
                                    item.GetType().GetProperty(key)
                                   .GetValue(item, null) != null &&
                                   item.GetType().GetProperty(key)
                                   .GetValue(item, null).ToString() == keyValue).ToList();
            }

            // 构建绑定数据集
            foreach (T info in tempList)
            {
                queryList.Add(info);
            }

            return queryList;
        }

        /// <summary>
        /// 按关键字过滤数据表
        /// </summary>
        /// <param name="dataList">数据表</param>
        /// <param name="key">查询关键字</param>
        /// <param name="keyValue">查询内容</param>
        /// <param name="queryMode">模糊/精确查询</param>
        /// <returns>数据表</returns>
        public DataTable GetQueryDataTable(DataTable dt, string key, string keyValue, string queryMode)
        {
            if (queryMode == "模糊查询")
            {
                // 模糊查找
                var query = dt.AsEnumerable().Where(q => q.Field<string>(key).ToString().Contains(keyValue)).ToArray<DataRow>();
                if(query.Count()==0)
                {
                    return null;
                }
                return query.CopyToDataTable();
            }

            // 精确查找
            var query1 = dt.AsEnumerable().Where(q => q.Field<string>(key).ToString() == keyValue).ToArray<DataRow>();

            if (query1.Count() == 0)
            {
                return null;
            }
            return query1.CopyToDataTable();       
        }
    }
}
