using DotBelt.QueriesMutations;

namespace DotBelt.CMS.Shared.Users;

[ExtendObjectType<DotBeltQuery>]
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
            .ProjectToUserResponse();
    }

}