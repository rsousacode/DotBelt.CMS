using DotBelt.QueriesMutations;

namespace DotBelt.CMS.Shared.Users;

[ExtendObjectType<GraphQLQuery>]
public class UsersQueries
{
        
    [UsePaging(IncludeTotalCount = true)]
    [UseProjection]
    [UseFiltering]
    [UseSorting]

    public IQueryable<UserResponse> GetUsers(ApplicationDbContext context)
    {
        return context
            .Users
            .Select(x => new UserResponse()
            {
                Id = x.Id,
                AccessFailedCount = x.AccessFailedCount,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                LockoutEnabled = x.LockoutEnabled,
                PhoneNumber = x.PhoneNumber,
                TwoFactorEnabled = x.TwoFactorEnabled,
                UserName = x.UserName
            });
    }

}