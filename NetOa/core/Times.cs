using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetOa.core
{
    public class Times
    {

        /// <summary>  
        /// 获取当前时间2018-07-13 20:06:07.105
        /// </summary>  
        /// <returns></returns>  
        public static string GetMsTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        }

        /// <summary>  
        /// 获取当前时间2018-07-13 20:06:07.105
        /// </summary>  
        /// <returns></returns>  
        public static string GetTime()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        /// <summary>  
        /// 获取当前时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static Int64 GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }



        /// <summary>  
        /// 时间戳转为C#格式时间
        /// </summary>  
        /// <returns></returns>  
        public static DateTime StampToDateTime(string timeStamp)
        {
            #pragma warning disable CS0618 // 类型或成员已过时
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            #pragma warning restore CS0618 // 类型或成员已过时
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }

    }
}
