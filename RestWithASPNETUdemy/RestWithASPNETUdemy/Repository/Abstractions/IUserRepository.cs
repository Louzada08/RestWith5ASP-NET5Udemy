using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Model;

namespace RestWithASPNETUdemy.Repository.Abstractions
{
    public interface IUserRepository
    {
        User? ValidateCredentials(UserVO user);
        User? ValidateCredentials(string username);
        bool RevokeToken(string userName);
        User? RefreshUserInfo(User user);
    }
}
