using Microsoft.AspNetCore.Identity;

namespace TrafficReport.Helpers;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError DefaultError() => new IdentityError()
    {
        Code = nameof(DefaultError),
    };
}