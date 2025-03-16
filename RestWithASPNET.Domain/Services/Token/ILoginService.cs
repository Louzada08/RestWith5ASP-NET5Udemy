using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.Domain.Services.Token
{
    public interface ILoginService
    {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
