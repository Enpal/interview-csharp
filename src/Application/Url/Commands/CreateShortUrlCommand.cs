using FluentValidation;
using HashidsNet;
using MediatR;
using UrlShortenerService.Application.Common.Interfaces;

namespace UrlShortenerService.Application.Url.Commands;

public record CreateShortUrlCommand : IRequest<string>
{
    /// <summary>
    /// The URL to be shorteneda.
    /// </summary>
    public string Url { get; init; } = default!;
}

/// <summary>
/// Validator for <see cref="CreateShortUrlCommand"/>.
/// </summary>
public class CreateShortUrlCommandValidator : AbstractValidator<RedirectToUrlCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateShortUrlCommandValidator"/> class.
    /// </summary>
    public CreateShortUrlCommandValidator()
    {
        _ = RuleFor(v => v.Id)
          .NotEmpty()
          .WithMessage("Id is required.");
    }
}

/// <summary>
/// Handler for <see cref="CreateShortUrlCommand"/>.
/// </summary>
public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, string>
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
    /// Initializes a new instance of the <see cref="CreateShortUrlCommandHandler"/> class.
    /// </summary>
    /// <param name="context">Injected instance of <see cref="IApplicationDbContext"/></param>
    /// <param name="context">Injected instance of <see cref="IHashids"/></param>
    public CreateShortUrlCommandHandler(IApplicationDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }

    /// <summary>
    /// Handles the <see cref="CreateShortUrlCommand"/>.
    /// </summary>
    /// <param name="request">The request model.</param>
    /// <param name="cancellationToken">The cancellation token for the request.</param>
    /// <returns>The original url.</returns>
    public async Task<string> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}
