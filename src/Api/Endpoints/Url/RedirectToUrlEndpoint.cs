using MediatR;
using UrlShortenerService.Api.Endpoints.Url.Requests;
using UrlShortenerService.Application.Url.Commands;
using IMapper = AutoMapper.IMapper;

namespace UrlShortenerService.Api.Endpoints.Url;

/// <summary>
/// Redirects to the original URL
/// </summary>
public class RedirectToUrlSummary : Summary<RedirectToUrlEndpoint>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RedirectToUrlSummary"/> class.
    /// </summary>
    public RedirectToUrlSummary()
    {
        Summary = "Redirect to the original url from the short url";
        Description =
            "This endpoint will redirect to the original url from the short url. If the short url is not found, it will return a 404.";
        Response(404, "No short url found.");
        Response(500, "Internal server error.");
    }
}

/// <summary>
/// Endpoint for redirecting to the original url from the short url.
/// </summary>
public class RedirectToUrlEndpoint : BaseEndpoint<RedirectToUrlRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RedirectToUrlEndpoint"/> class.
    /// </summary>
    /// <param name="mediator">Injected MediatR instance.</param>
    /// <param name="mapper">Injected AutoMapper instance.</param>
    public RedirectToUrlEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

    /// <summary>
    /// Endpoint configuration.
    /// </summary>
    public override void Configure()
    {
        base.Configure();
        Get("u/{Id}");
        AllowAnonymous();
        Description(
            d => d.WithTags("Url")
        );
        Summary(new RedirectToUrlSummary());
    }

    /// <summary>
    /// Endpoint handler.
    /// </summary>
    /// <param name="req">The request model.</param>
    /// <param name="ct">The cancellation token for the request.</param>
    public override async Task HandleAsync(RedirectToUrlRequest req, CancellationToken ct)
    {
        var result = await Mediator.Send(
            new RedirectToUrlCommand
            {
                Id = req.Id
            },
            ct
        );
        await SendRedirectAsync(result);
    }
}
