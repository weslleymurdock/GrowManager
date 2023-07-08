using GrowManager.Application.Interfaces.Services;
using System;

namespace GrowManager.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}