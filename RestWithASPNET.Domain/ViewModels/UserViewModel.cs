using RestWithASPNET.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Adapter.ViewModels;

public class UserVM
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
    [Column("refresh_token")]
    public string? RefreshToken { get; set; } = string.Empty;
    [Column("refresh_token_expiry_time")]
    public DateTime RefreshTokenExpiryTime { get; set; }
    public UserRolesEnum UserRole { get; set; }
}
