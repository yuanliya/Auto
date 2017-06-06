using System;
using System.Collections.Generic;
using System.Text;

namespace ZY.EntityFrameWork.Core.DBHelper
{
    /// <summary>
    /// Common validation routines for argument validation.(来自Enterprise Library2005的Common项目)
    /// </summary>
    public sealed class ArgumentValidationHelper
    {
        #region 常规报错信息

        private const string ExceptionEmptyString             = "参数 '{0}'的值不能为空字符串。";
        private const string ExceptionInvalidNullNameArgument = "参数'{0}'的名称不能为空引用或空字符串。";
        private const string ExceptionByteArrayValueMustBeGreaterThanZeroBytes = "数值'{0}'必须大于0字节.";
        private const string ExceptionExpectedType          = "无效的类型，期待的类型必须为'{0}'。";
        private const string ExceptionEnumerationNotDefined = "{0}不是{1}的一个有效值";

        #endregion

        private ArgumentValidationHelper()
        {
        }

        /// <summary>
        /// 检查参数是否为空字符串
        /// </summary>
        /// <param name="variable">字符串变量对象.</param>
        /// <param name="variableName">字符串变量名.</param>
        /// <remarks>在检查字符串变量对象之前，首先检查变量名不能为空</remarks>
        /// <exception cref="ArgumentNullException">当字符串变量长度为0，抛出参数为空字符串的异常/// </exception>
        public static void CheckForEmptyString(string variable, string variableName)
        {
            // 校验字符串变量名是否为空
            CheckForNullReference(variable, variableName);
            CheckForNullReference(variableName, "variableName");

            // 校验字符串变量是否长度为0
            if (variable.Length == 0)
            {
                string message = string.Format(ExceptionEmptyString, variableName);

                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// 检查参数是否为空引用(Null).
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="variableName">变量名</param>
        /// <exception cref="ArgumentNullException">当变量名为空，或变量为空引用，抛出异常</exception>
        public static void CheckForNullReference(object variable, string variableName)
        {
            if (string.IsNullOrEmpty(variableName))
            {
                throw new ArgumentNullException("variableName");
            }

            // 校验变量是否为空引用
            if (variable == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionInvalidNullNameArgument, variableName));
            }
        }

        /// <summary>
        /// 验证输入的参数messageName非空字符串，也非空引用
        /// </summary>
        /// <param name="name">参数名字</param>
        /// <param name="messageName">参数值</param>
        public static void CheckForInvalidNullNameReference(string name, string messageName)
        {
            if ((string.IsNullOrEmpty(name)) || (name.Length == 0))
            {
                string message = string.Format(ExceptionInvalidNullNameArgument, messageName);

                throw new InvalidOperationException(message);
            }
        }

        /// <summary>
        /// 验证参数非零长度，如果为零长度，则抛出异常
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <param name="variableName">变量名</param>
        /// <exception cref="ArgumentNullException">如果字节数组长度为0，则抛出异常</exception>
        public static void CheckForZeroBytes(byte[] bytes, string variableName)
        {
            CheckForNullReference(bytes, "bytes");
            CheckForNullReference(variableName, "variableName");

            if (bytes.Length == 0)
            {
                string message = string.Format(ExceptionByteArrayValueMustBeGreaterThanZeroBytes, variableName);

                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// 检查参数是否符合指定的类型.
        /// </summary>
        /// <param name="variable">变量</param>
        /// <param name="type">校验的类型</param>
        /// <exception cref="ArgumentNullException">当变量不是指定类型，抛出异常</exception>
        public static void CheckExpectedType(object variable, Type type)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(type, "type");

            if (!type.IsAssignableFrom(variable.GetType()))
            {
                string message = string.Format(ExceptionExpectedType, type.FullName);

                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// 校验变量是否是枚举中的有效值
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="variable">变量</param>
        /// <param name="variableName">变量名</param>
        /// <exception cref="ArgumentNullException">当变量不是枚举中的有效类型时，抛出异常</exception>
        public static void CheckEnumeration(Type enumType, object variable, string variableName)
        {
            CheckForNullReference(variable, "variable");
            CheckForNullReference(enumType, "enumType");
            CheckForNullReference(variableName, "variableName");

            if (!Enum.IsDefined(enumType, variable))
            {
                string message = string.Format(ExceptionEnumerationNotDefined, variable.ToString(), enumType.FullName, variableName);

                throw new ArgumentException(message);
            }
        }
    }
}
