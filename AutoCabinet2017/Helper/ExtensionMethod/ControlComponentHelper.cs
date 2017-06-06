using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.XtraEditors;

using ZY.EntityFrameWork.Core.Model.Dto;

namespace AutoCabinet2017.Helper
{
    public static class ControlComponentHelper
    {
        /// <summary>
        /// 设置TextEdit控件的水印显示
        /// </summary>
        /// <param name="textEdit">控件实体</param>
        /// <param name="watermark">水印显示值</param>
        public static void SetWatermark(this TextEdit textEdit, string watermark)
        {
            // TextEdit控件的Text属性为空时显示水印
            textEdit.Properties.NullValuePromptShowForEmptyValue = true;
            // 设置水印值
            textEdit.Properties.NullValuePrompt = watermark;
        }

        /// <summary>
        /// 清除TextEdit控件的水印显示
        /// </summary>
        /// <param name="textEdit"></param>
        public static void ClearWatermark(this TextEdit textEdit)
        {
            if (textEdit.Properties.NullValuePromptShowForEmptyValue)
            {
                textEdit.Properties.NullValuePrompt = string.Empty;
            }
        }

        /// <summary>
        /// ComboboxEdit控件的扩展方法-下拉框中添加从数据字典中读取的明细
        /// </summary>
        /// <param name="cbxEdit">combobox控件</param>
        /// <param name="fieldType">数据字典查询的关键字</param>
        public static void AddItems(this ComboBoxEdit cbxEdit, string fieldType)
        {
            if (PropertyHelper.DataDictItems == null) return;

            // 部门分类信息
            List<DataDictDto> Items = PropertyHelper.DataDictItems.Where<DataDictDto>(item => item.FieldTypeName == fieldType)
                                                                    .OrderBy(item => item.FieldTypeSerialNo)
                                                                    .ToList<DataDictDto>();

            // 配置部门信息下拉框
            if (Items.Count != 0)
            {
                foreach (DataDictDto item in Items)
                {
                    cbxEdit.Properties.Items.Add(item.FieldTypeValue);
                }
            }
        }
    }
}
