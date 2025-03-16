using RestWithASPNET.Domain.ValueObjects;

namespace RestWithASPNET.FrameWrkDrivers.Gateways
{
    public interface ILoginService
    {
        TokenVO ValidateCredentials(UserVO user);
        TokenVO ValidateCredentials(TokenVO token);
        bool RevokeToken(string userName);
    }
}
