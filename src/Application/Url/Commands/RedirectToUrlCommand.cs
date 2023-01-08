using FluentValidation;
using HashidsNet;
using MediatR;
using UrlShortenerService.Application.Common.Interfaces;

namespace UrlShortenerService.Application.Url.Commands;

public record RedirectToUrlCommand : IRequest<string>
{
    /// <summary>
    /// Obfuscated id that refers to the original url.
    /// </summary>
    public string Id { get; init; } = default!;
}

/// <summary>
/// Validator for <see cref="RedirectToUrlCommand"/>.
/// </summary>
public class RedirectToUrlCommandValidator : AbstractValidator<RedirectToUrlCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RedirectToUrlCommandValidator"/> class.
    /// </summary>
    public RedirectToUrlCommandValidator()
    {
        _ = RuleFor(v => v.Id)
          .NotEmpty()
          .WithMessage("Id is required.");
    }
}

/// <summary>
/// Handler for <see cref="RedirectToUrlCommand"/>.
/// </summary>
public class RedirectToUrlCommandHandler : IRequestHandler<RedirectToUrlCommand, string>
{
    /// <summary>
    /// Application database context. 
    /// </summary>
    private readonly IApplicationDbContext _context;
    /// <summary>
    /// Hashids is used to obfuscate the id of the original url.
    /// </summary>
    private readonly IHashids _hashids;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedirectToUrlCommandHandler"/> class.
    /// </summary>
    /// <param name="context">Injected instance of <see cref="IApplicationDbContext"/></param>
    public RedirectToUrlCommandHandler(IApplicationDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }

    /// <summary>
    /// Handles the <see cref="RedirectToUrlCommand"/>.
    /// </summary>
    /// <param name="request">The request model.</param>
    /// <param name="cancellationToken">The cancellation token for the request.</param>
    /// <returns>The original url.</returns>
    public async Task<string> Handle(RedirectToUrlCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
