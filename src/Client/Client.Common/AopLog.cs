using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Afx.Aop;
using Afx.Utils;

namespace Client.Common
{
    /// <summary>
    /// 异常AOP
    /// </summary>
    public class AopLog : IAop
    {
        /// <summary>
        /// 方法执行前
        /// </summary>
        /// <param name="context"></param>
        public void OnExecuting(AopContext context)
        {

        }

        /// <summary>
        /// 方法执行后
        /// </summary>
        /// <param name="context"></param>
        /// <param name="returnValue"></param>
        public void OnResult(AopContext context, object returnValue)
        {

        }

        /// <summary>
        /// 方法异常
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        public void OnException(AopContext context, Exception ex)
        {
            StringBuilder msg = new StringBuilder();
            msg.AppendFormat("【AOP.OnException】Class: {0},\r\nMethod: {1}\r\n", context.TagetType.FullName, context.Method.Name);
            var param = context.Method.GetParameters();
            object[] args = context.Parameters ?? new object[0];
            for (int i = 0; i < param.Length; i++)
            {
                var p = param[i];
                msg.AppendFormat("{0}: {1}\r\n", p.Name, args.Length > i ? (args[i] == null ? "null" : JsonUtils.Serialize(args[i])) : "");
            }
            msg.Append("异常：");

            LogUtils.Error(msg.ToString(), ex);
        }
    }
}
