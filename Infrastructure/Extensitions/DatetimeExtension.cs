using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Extensitions
{
    public static class DatetimeExtension
    {
        public static DateTime ToLocalTime(this DateTimeOffset? value)
        {
            return value?.LocalDateTime ?? default(DateTime);
        }
    }
}
