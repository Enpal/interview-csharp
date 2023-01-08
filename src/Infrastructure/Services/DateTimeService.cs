using UrlShortenerService.Application.Common.Interfaces;

namespace UrlShortenerService.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
