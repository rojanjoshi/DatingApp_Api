using DatingApp.Models;


namespace DatingApp.Db.Interface
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
