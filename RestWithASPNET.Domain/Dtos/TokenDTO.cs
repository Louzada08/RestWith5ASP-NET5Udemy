namespace RestWithASPNET.Domain.Dtos;

public class TokenDTO
{
    public bool Authenticated { get; set; }
    public string Created { get; set; }
    public string Expiration { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
