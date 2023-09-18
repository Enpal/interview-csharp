using System.Security.Cryptography;
using System.Text;
using System.Web;
using FluentValidation;
using HashidsNet;
using MediatR;
using UrlShortenerService.Application.Common.Interfaces;
using UrlShortenerService.Domain.Entities;
namespace UrlShortenerService.Application.Url.Commands;

public record CreateShortUrlCommand : IRequest<string>
{
    public string Url { get; init; } = default!;
}

public class CreateShortUrlCommandValidator : AbstractValidator<CreateShortUrlCommand>
{
    public CreateShortUrlCommandValidator()
    {
        _ = RuleFor(v => v.Url)
          .NotEmpty()
          .WithMessage("Url is required.");
    }
}

public class CreateShortUrlCommandHandler : IRequestHandler<CreateShortUrlCommand, string>
{
    private readonly IApplicationDbContext _context;
    private readonly IHashids _hashids;

    public CreateShortUrlCommandHandler(IApplicationDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }
    public async Task<string> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var id = CalculateShortId(request.Url);
        var shortUrl = "http://localhost:5246/u/" + id;
        _context.Urls.Add(new Domain.Entities.Url() 
        { 
            Id = id,
            OriginalUrl = request.Url
        });
        await _context.SaveChangesAsync(cancellationToken);
        return shortUrl;
    }
    private string CalculateShortId(string originalUrl)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(originalUrl));
            return BitConverter.ToString(hashBytes).Replace("-", "").Substring(0, 8); // Use the first 8 characters as the short ID
        }
    }
}
