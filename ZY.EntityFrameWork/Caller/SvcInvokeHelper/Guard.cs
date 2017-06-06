using System;
namespace ZY.EntityFrameWork.Caller.SvcInvokeHelper
{
    /// <summary>
    /// 异常状态处理类
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// 参数为空的异常
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="argumentName"></param>
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// 参数为空或长度为0的异常
        /// </summary>
        /// <param name="argumentValue"></param>
        /// <param name="argumentName"></param>
        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
            if (argumentValue.Length == 0)
            {
                throw new ArgumentException("The provided string argument must not be empty.", argumentName);
            }
        }
    }
}
