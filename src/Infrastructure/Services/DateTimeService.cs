using Cook_Log.Application.Common.Interfaces;
using System;

namespace Cook_Log.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
