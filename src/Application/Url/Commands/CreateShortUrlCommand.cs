using System.Security.Cryptography;
using System.Text;
using System.Web;
using FluentValidation;
using HashidsNet;
using MediatR;
using UrlShortenerService.Application.Common.Interfaces;

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

    //private List<string> hashedUrls = new List<string>();

    public CreateShortUrlCommandHandler(IApplicationDbContext context, IHashids hashids)
    {
        _context = context;
        _hashids = hashids;
    }
    public async Task<string> Handle(CreateShortUrlCommand request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var shortUrl = "http://localhost:5246/u/" + encode(request.Url);
        //hashedUrls.Add(shortUrl);
        return shortUrl;
    }
    public string encode(string url) {
        byte[] bytes = Encoding.UTF8.GetBytes(url);
        var hex = Convert.ToHexString(bytes);
        return _hashids.EncodeHex(hex);
    }
}
