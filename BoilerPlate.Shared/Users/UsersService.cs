using Microsoft.EntityFrameworkCore;

namespace BoilerPlate.Shared.Users;

[RegisterScoped]
public class UsersService
{
    private readonly ApplicationDbContext _context;

    public UsersService(IDbContextFactory<ApplicationDbContext> factory)
    {
        _context = factory.CreateDbContext();
    }
    
    // TODO: Delete me
    
    public async Task<UserResponse[]> GetUsers(CancellationToken cancellationToken = default )
    {
        return await _context
            .Users
            .Select(x => new UserResponse()
            {
                Email = x.Email,
                UserName = x.UserName,
            })
            .ToArrayAsync(cancellationToken: cancellationToken);
    }
}