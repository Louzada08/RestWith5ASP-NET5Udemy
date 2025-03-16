using RestWithASPNET.Domain.Entities;
using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.FrameWrkDrivers.Gateways
{
    public interface IUserRepository
    {
        User? ValidateCredentials(UserVO user);
        User? ValidateCredentials(string username);
        bool RevokeToken(string userName);
        User? RefreshUserInfo(User user);
    }
}
