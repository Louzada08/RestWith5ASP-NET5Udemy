using RestWithASPNET.IOC;

namespace RestWithASPNET.Adapter.ViewModels;

public class TokenViewModel
{
    public TokenViewModel(bool authenticated, string created, string expiration, string accessToken, string refreshToken)
    {
        Authenticated = authenticated;
        Created = created;
        Expiration = expiration;
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }
    public bool Authenticated { get; set; }
    public string Created { get; set; }
    public string Expiration { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

    TokenViewModel ITokenViewModel<TokenViewModel>.TokenViewModel()
    {
        throw new NotImplementedException();
    }
}
