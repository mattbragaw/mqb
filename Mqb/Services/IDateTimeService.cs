using System;
using System.Collections.Generic;
using System.Text;

namespace Mqb.Services
{
    public interface IDateTimeService : IService
    {
        DateTime Now { get; }
        DateTime NowUtc { get; }
    }
}
