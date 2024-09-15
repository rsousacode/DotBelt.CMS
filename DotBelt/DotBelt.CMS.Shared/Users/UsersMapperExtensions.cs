using Riok.Mapperly.Abstractions;

namespace DotBelt.CMS.Shared.Users;

[Mapper]
public static partial class UsersMapperExtensions
{
    public static partial IQueryable<UserResponse> ProjectToUserResponse(this IQueryable<ApplicationUser> q);

}