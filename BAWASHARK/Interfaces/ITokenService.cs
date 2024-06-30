using BAWASHARK.Models;

namespace BAWASHARK.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
