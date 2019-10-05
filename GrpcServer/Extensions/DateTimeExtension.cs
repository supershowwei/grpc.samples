using System;

namespace GrpcServer.Extensions
{
    internal static class DateTimeExtension
    {
        private static readonly System.DateTime InitialTime = new System.DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimeMilliseconds(this System.DateTime me)
        {
            return Convert.ToInt64(me.ToUniversalTime().Subtract(InitialTime).TotalMilliseconds);
        }

        public static System.DateTime ToDateTime(this long me)
        {
            return InitialTime.AddMilliseconds(me).ToLocalTime();
        }

    }
}