using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNETUdemy.Services
{
    public interface ILoginService
    {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
