using Microsoft.EntityFrameworkCore;
using UrlShortenerService.Domain.Entities;

namespace UrlShortenerService.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    /// <summary>
    /// DbSet of Urls.
    /// </summary>
    DbSet<Domain.Entities.Url> Urls { get; }

    /// <summary>
    /// Save changes to the database. 
    /// </summary>
    /// <param name="cancellationToken">The cancellation token for the request.</param>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
