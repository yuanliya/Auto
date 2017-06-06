using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace NJUST.AUTO06.Utility
{
    public sealed class KeyboardInputHelper
    {
        #region 构造函数和实例

        // 私有构造函数
        KeyboardInputHelper()
        {        
        }

        // 利用单件模式构造线程安全的独立类实例
        public static readonly KeyboardInputHelper Instance = new KeyboardInputHelper();

        #endregion

        #region 键盘数据的有效性判断

        /// <summary>
        /// 控制可编辑控件的键盘输入，该方法限定控件只可以接收表示非负十进制数的数字
        /// </summary>
        /// <param name="e">为 KeyPress 事件提供数据</param>
        /// <param name="con">可编辑文本控件</param>
        public void InputNumeric(KeyPressEventArgs e, Control con)
        {
            //在可编辑控件的Text属性为空的情况下，不允许输入".字符"
            if (String.IsNullOrEmpty(con.Text) && e.KeyChar.ToString() == ".")
            {
                //把Handled设为true，取消KeyPress事件，防止控件处理按键
                e.Handled = true;
            }

            //可编辑控件不允许输入多个"."字符
            if (con.Text.Contains(".") && e.KeyChar.ToString() == ".")
            {
                e.Handled = true;
            }

            // 在可编辑控件中，只可以输入“数字字符”、".字符" 、"字符"(删除键对应的字符)
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar.ToString() != "." && e.KeyChar.ToString() != "")
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 控制可编辑控件的键盘输入，该方法限定控件只可以接收表示非负十进制数的数字和字母
        /// </summary>
        /// <param name="e">为 KeyPress 事件提供数据</param>
        /// <param name="con">可编辑文本控件</param>
        public void InputNumericAndChar(KeyPressEventArgs e, Control con)
        {
            //在可编辑控件的Text属性为空的情况下，不允许输入".字符"
            if (String.IsNullOrEmpty(con.Text) && e.KeyChar.ToString() == ".")
            {
                //把Handled设为true，取消KeyPress事件，防止控件处理按键
                e.Handled = true;
            }

            //可编辑控件不允许输入多个"."字符
            if (con.Text.Contains(".") && e.KeyChar.ToString() == ".")
            {
                e.Handled = true;
            }

            // 在可编辑控件中，只可以输入“数字字符”、".字符" 、"字符"(删除键对应的字符)
            if (!Char.IsLetterOrDigit(e.KeyChar) && e.KeyChar.ToString() != "." && e.KeyChar.ToString() != "")
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 控制可编辑控件的键盘输入，该方法限定控件只可以接收表示非负整数的字符
        /// </summary>
        /// <param name="e">为 KeyPress 事件提供数据</param>
        public void InputInteger(KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar.ToString() != "")
            {
                // 把Handled设为true，取消KeyPress事件，防止控件处理按键
                e.Handled = true;
            }
        }

        /// <summary>
        /// 密码格式校验
        /// </summary>
        /// <param name="pwd">密码文本</param>
        /// <returns>符合数字+字母，返回true</returns>
        public bool CheckPasswordFormat(string pwd)
        {
            // 正则表达式设计为只允许数字和字母输入
            Regex myReg = new Regex(@"^[A-Za-z0-9]+$");
            return myReg.IsMatch(pwd);
        }

        #endregion
    }
}
