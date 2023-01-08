using MediatR;
using IMapper = AutoMapper.IMapper;

namespace UrlShortenerService.Api.Endpoints;

/// <summary>
/// Base endpoint class with request and response types.
/// </summary>
public abstract class BaseEndpoint<TRequest, TResponse> : Endpoint<TRequest, TResponse>
    where TRequest : notnull, new()
    where TResponse : notnull, new()
{
    /// <summary>
    /// Mediator instance.
    /// </summary>
    protected ISender Mediator { get; init; }
    /// <summary>
    /// Automapper instance.
    /// </summary>
    protected IMapper Mapper { get; init; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="mediator">Injected mediator instance.</param>
    /// <param name="mapper">Injected automapper instance.</param>
    protected BaseEndpoint(ISender mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

    /// <summary>
    /// Can be used to provide base functionality for all endpoints.
    /// </summary>
    public override void Configure()
    {
    }
}

/// <summary>
/// Base endpoint class with request type.
/// </summary>
public abstract class BaseEndpoint<TRequest> : Endpoint<TRequest> where TRequest : notnull, new()
{
    /// <summary>
    /// Mediator instance.
    /// </summary>
    protected ISender Mediator { get; init; }
    /// <summary>
    /// Automapper instance.
    /// </summary>
    protected IMapper Mapper { get; init; }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="mediator">Injected mediator instance.</param>
    /// <param name="mapper">Injected automapper instance.</param>
    protected BaseEndpoint(ISender mediator, IMapper mapper)
    {
        Mediator = mediator;
        Mapper = mapper;
    }

    /// <summary>
    /// Can be used to provide base functionality for all endpoints.
    /// </summary>
    public override void Configure()
    {
    }
}
