using Mqb.Descriptors.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now { get { return DateTime.Now; } }
        public DateTime NowUtc { get { return DateTime.UtcNow; } }
    }
}
